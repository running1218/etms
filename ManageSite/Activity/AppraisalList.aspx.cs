using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Activity.Implement.BLL;
using ETMS.Activity.Entity;
using System.Data;
using ETMS.AppContext;
using ETMS.Utility;
using ETMS.Controls;

public partial class Activity_AppraisalList : System.Web.UI.Page
{
    private static readonly AppraisalLogic logic = new AppraisalLogic();

    protected void Page_Load(object sender, EventArgs e)
    {
        PageSet1.pageInit(gvList, PageDataSource);

        if (!Page.IsPostBack)
        {
            this.PageSet1.QueryChange();
        }
    }

    /// <summary>
    /// 查询结果
    /// </summary>
    /// <param name="pageIndex"></param>
    /// <param name="pageSize"></param>
    /// <param name="totalRecordCount"></param>
    /// <returns></returns>
    private System.Collections.IList PageDataSource(int pageIndex, int pageSize, out int totalRecordCount)
    {        
        DataTable dt = logic.GetPageList(UserContext.Current.OrganizationID, txt_AppraisalName.Text.Trim(), txtBeginTime.Text.ToStartDateTime(), txtEndTime.Text.ToEndDateTime(), pageIndex, pageSize, out totalRecordCount);

        PageDataSourceProvider pageDataSource = new PageDataSourceProvider(dt, pageIndex, pageSize);
        return pageDataSource.PageDataSource;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        this.PageSet1.QueryChange();
    }

    protected void gvList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Del")
            {
                if (logic.IsSingnUp(e.CommandArgument.ToGuid()))
                    throw new BusinessException("已有学员报名无法删除！");
                logic.Delete(e.CommandArgument.ToGuid());
            }
            else if (e.CommandName == "Deploy")
            {
                var entity = logic.GetAppraisalByID(e.CommandArgument.ToGuid());
                if (entity.Status == 0)
                    entity.Status = 1;
                else if (entity.Status == 1)
                    entity.Status = 0;
                logic.Update(entity);
            }
            else if(e.CommandName=="Top")
            {
                logic.Top(e.CommandArgument.ToGuid());
            }

            this.PageSet1.DataBind();
        }
        catch (ETMS.AppContext.BusinessException bizEx)
        {
            ETMS.Utility.JsUtility.FailedMessageBox(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
        }
    }
}