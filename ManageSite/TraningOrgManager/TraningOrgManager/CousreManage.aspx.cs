using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Controls;
using ETMS.Utility;
using ETMS.Components.Basic.Implement.BLL.TraningOrgnization.Course;
using ETMS.Components.Basic.Implement.BLL.TraningOrgnization;

public partial class TraningOrgManager_TraningOrgManager_CousreManage :ETMS.Controls.BasePage
{
    public static Tr_OuterOrgLogic outerOrgLogic = new Tr_OuterOrgLogic();

    public static Tr_OuterOrgCourseLogic outerOrgCourseLogic = new Tr_OuterOrgCourseLogic();
    /// <summary>
    /// 外部培训机构ID
    /// </summary>
    public Guid OuterOrgID
    {
        get { return Request.QueryString["OuterOrgID"].ToGuid(); }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        PageSet1.pageInit(this.GridViewList, PageDataSource);

        if (!Page.IsPostBack)
        {
            this.PageSet1.QueryChange();
            Initial();
        }
        aBack.HRef = "TraningOrgList.aspx";
    }

    private void Initial()
    {
        lblOuterOrgName.Text = outerOrgLogic.GetById(OuterOrgID).OuterOrgName;
    }

    private System.Collections.IList PageDataSource(int pageIndex, int pageSize, out int totalRecordCount)
    {
        string strQuery = BasePage.getQueryConditionFromQueryControlList(tableQueryControlList);
        strQuery += string.Format(" and OuterOrgID='{0}'", OuterOrgID);
        var source = outerOrgCourseLogic.GetPagedList(pageIndex, pageSize, "", strQuery.ToString(), out totalRecordCount);
        PageDataSourceProvider pageDataSource = new PageDataSourceProvider(source, pageIndex, pageSize);
        return pageDataSource.PageDataSource;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        this.PageSet1.QueryChange();
    }
   
    protected void btnDeletes_Click(object sender, EventArgs e)
    {
        try
        {
            Guid[] outCourseIDs = CustomGridView.GetSelectedValues<Guid>(this.GridViewList);
            if (outCourseIDs.Length > 0)
            {
                for (int i = 0; i < outCourseIDs.Length; i++)
                {
                    outerOrgCourseLogic.doRemove(outCourseIDs[i]);
                }
                ETMS.WebApp.Manage.Extention.SuccessMessageBox("删除成功！");
                this.PageSet1.DataBind();
            }
            else
            {
                ETMS.WebApp.Manage.Extention.AlertMessageBox("请选择课程！");
            }
         }
        catch (ETMS.AppContext.BusinessException bizEx)
        {
            ETMS.WebApp.Manage.Extention.ShowScriptManagerMessage(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
        }
    }
}