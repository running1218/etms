using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Components.Basic.Implement.BLL;
using System.Data;
using ETMS.WebApp;
using ETMS.Controls;
using ETMS.Utility;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Student;
using ETMS.Components.Basic.Implement.BLL.Security;
using ETMS.Components.Basic.Implement.BLL.Dictionary;
using ETMS.Activity.Implement.BLL;
using ETMS.Activity.Entity;

public partial class Activity_SetPrize : BasePage
{
    #region 页面参数
    /// <summary>
    /// 项目ID
    /// </summary>
    public Guid AppraisalID
    {
        get
        {
            return Request.QueryString["AppraisalID"].ToGuid();
        }
    }

    #endregion

    private static readonly ProductionLogic logic = new ProductionLogic();

    protected void Page_Load(object sender, EventArgs e)
    {
        PageSet1.pageInit(this.CustomGridView1, PageDataSource);
        if (!Page.IsPostBack)
        {
            this.PageSet1.QueryChange();
        }        
    }

    /// <summary>
    /// 查询
    /// </summary>
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        this.PageSet1.QueryChange();
    }

    private System.Collections.IList PageDataSource(int pageIndex, int pageSize, out int totalRecordCount)
    {
        var data = logic.GetPrizeByAppraisalID(AppraisalID, txt_ProductionName.Text.Trim(), pageIndex, pageSize, out totalRecordCount);
        PageDataSourceProvider pageDataSource = new PageDataSourceProvider(data, pageIndex, pageSize);
        return pageDataSource.PageDataSource;
    }
    
    /// <summary>
    /// 添加
    /// </summary>
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        int[] selectedValues = CustomGridView.GetSelectedValues<int>(this.CustomGridView1);
        if (selectedValues.Length == 0)
        {
            ETMS.WebApp.Manage.Extention.AlertMessageBox("请选择要添加的学员！");
            return;
        }
        else
        {
            try
            {

                ETMS.WebApp.Manage.Extention.SuccessMessageBox("提示", "学员添加成功！");
                this.PageSet1.QueryChange(); 
            }
            catch (ETMS.AppContext.BusinessException bizEx)
            {
                ETMS.WebApp.Manage.Extention.ShowScriptManagerMessage(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
                return;
            }
        }
    }

    protected void CustomGridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Cancel")
            {
                var productionID = e.CommandArgument.ToGuid();               
                logic.CancelPrize(productionID);
            }
           
            this.PageSet1.DataBind();
        }
        catch (ETMS.AppContext.BusinessException bizEx)
        {
            ETMS.Utility.JsUtility.FailedMessageBox(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
        }
    }

    protected void CustomGridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Excellent row = e.Row.DataItem as Excellent;

            CustomLinkButton lkbnSetting = e.Row.FindControl("lbtnSetting") as CustomLinkButton;
            CustomLinkButton lkbnCancel = e.Row.FindControl("lbtnCancel") as CustomLinkButton;
            Guid productionID = lkbnSetting.CommandArgument.ToGuid();
            lkbnSetting.Attributes.Add("onclick", string.Format("javascript:showWindow('设置奖项','{0}',500,400);javascript:return false;", this.ActionHref("Prize.aspx?ProductionID=" + productionID)));

            if (row.PrizeName == null)
            {
                lkbnCancel.Style.Add("display", "none");
            }
            else
            {
                lkbnSetting.Style.Add("display", "none");
            }
        }
    }
}