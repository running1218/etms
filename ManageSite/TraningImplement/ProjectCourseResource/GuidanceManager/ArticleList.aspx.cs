using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Utility;
using ETMS.Components.Basic.Implement.BLL.Bulletin;
using System.Collections;
using System.Text;
using System.Data;
using ETMS.Controls;

public partial class QuestionDB_GuidanceManager_ArticleList : ETMS.Controls.BasePage
{
    /// <summary>
    /// 获取项目id
    /// </summary>
    protected Guid TrainingItemCourseID
    {
        get
        {
            if (ViewState["TrainingItemCourseID"] == null)
            {
                ViewState["TrainingItemCourseID"] = UrlParamDecode(Request.QueryString["TrainingItemCourseID"]).ToGuid();
            }
            return ViewState["TrainingItemCourseID"].ToGuid();
        }
        set
        {
            ViewState["TrainingItemCourseID"] = value;
        }
    }
 

    Inf_BulletinLogic Logic = new Inf_BulletinLogic();
    protected void Page_Load(object sender, EventArgs e)
    {
        this.PageSet1.pageInit(this.GridViewList, new IPageDataSource(this.getDataSource1));
        if (!Page.IsPostBack)
        {
            this.PageSet1.QueryChange();
            InitialControlers();
        }
        aBack.HRef = "ItemArticleList.aspx";
    }
    private void InitialControlers()
    {
        btnAdd.Attributes.Add("onclick", string.Format("javascript:showWindow('新增导学资料','{0}',600,400);javascript: return false;", this.ActionHref(string.Format("ArticleInfo.aspx?op={0}&id={1}&TrainingItemCourseID={2}", "add", "0", TrainingItemCourseID.ToString()))));

    }
    private IList getDataSource1(int pageIndex, int pageSize, out int totalRecords)
    {
        DataTable dataList = Logic.GetMentorDatabyItemCourse(TrainingItemCourseID);
        totalRecords = dataList.Rows.Count;
        PageDataSourceProvider psp = new PageDataSourceProvider(dataList, pageIndex, pageSize);
        return psp.PageDataSource;
    }
    protected void GridViewList_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow && !this.GridViewList.IsEmpty)
        {
            DataRowView dr = (DataRowView)e.Row.DataItem;
            LinkButton lbnModify = (LinkButton)e.Row.FindControl("btnModify");
            CustomLinkButton lbnDel = (CustomLinkButton)e.Row.FindControl("lbtn_Del");
            lbnModify.Attributes.Add("onclick", string.Format("javascript:showWindow('导学资料管理','{0}',600,400);javascript: return false;", this.ActionHref(string.Format("ArticleInfo.aspx?op={0}&id={1}&TrainingItemCourseID={2}", "edit", dr["ArticleID"], TrainingItemCourseID.ToString()))));
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        this.PageSet1.QueryChange();
    }

    protected void GridViewList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int articleID;
            switch (e.CommandName)
            {
                case "Del":
                    articleID = e.CommandArgument.ToInt();
                    Logic.RemoveItemCourseMentorData(articleID, TrainingItemCourseID);
                    this.PageSet1.DataBind();
                    break;
                case "IsUse":

                    string[] parm = e.CommandArgument.ToString().Split(',');
                    articleID = int.Parse(parm[1]);
                    int isUse = int.Parse(parm[0]) == 1 ? 0 : 1;
                    Logic.SetMontorDataIsUse(articleID, isUse);
                    this.PageSet1.DataBind();
                    break;
            }
        }
        catch (ETMS.AppContext.BusinessException bizEx)
        {
            ETMS.WebApp.Manage.Extention.ShowScriptManagerMessage(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
            return;
        }
    }
}