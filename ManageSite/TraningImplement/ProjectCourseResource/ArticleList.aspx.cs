using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Components.Basic.Implement.BLL.Bulletin;
using ETMS.Controls;
using System.Collections;
using System.Data;
using ETMS.Utility;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Course;
using ETMS.Components.Basic.API.Entity.TrainingItem.Course;
using ETMS.Components.Basic.API.Entity.TrainingItem;
using ETMS.Components.Basic.Implement.BLL.TrainingItem;
using ETMS.Components.Basic.API.Entity.Course;
using ETMS.Components.Basic.Implement.BLL.Course;
using ETMS.Components.Basic.API.Entity.Bulletin;

public partial class TraningImplement_ProjectCourseResource_ArticleList : BasePage
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
                ViewState["TrainingItemCourseID"] = Request.QueryString["TrainingItemCourseID"].ToGuid();
            }
            return ViewState["TrainingItemCourseID"].ToGuid();
        }
        set
        {
            ViewState["TrainingItemCourseID"] = value;
        }
    }
    
    /// <summary>
    /// 项目id
    /// </summary>
    protected Guid TrainingItemID
    {
        get
        {
            if (ViewState["TrainingItemID"] == null)
            {
                ViewState["TrainingItemID"] = Guid.Empty;
            }
            return ViewState["TrainingItemID"].ToGuid();
        }
        set
        {
            ViewState["TrainingItemID"] = value;
        }
    }

    protected BulletinTypeEnum ArticleType
    {
        get
        {
            return (BulletinTypeEnum)Request.QueryString["ArticleType"].ToInt();
        }
    }



    Inf_BulletinLogic Logic = new Inf_BulletinLogic();
    protected void Page_Load(object sender, EventArgs e)
    {
        this.PageSet1.pageInit(this.GridViewList, new IPageDataSource(this.getDataSource1));
        if (!Page.IsPostBack)
        {
            this.PageSet1.QueryChange();
            bind();
        }
        btnAdd.Attributes.Add("onclick", string.Format("javascript:showWindow('新增课程公告','{0}');javascript: return false;", this.ActionHref(string.Format("GuidanceManager/ArticleInfo.aspx?op={0}&id={1}&TrainingItemCourseID={2}&ArticleType=2", "add", "0", TrainingItemCourseID.ToString()))));

        aBack.HRef = this.ActionHref(string.Format("CourseList.aspx?TrainingItemID={0}", TrainingItemID));
    }

    /// <summary>
    /// 邦定基本信息
    /// </summary>
    private void bind()
    {
        Tr_ItemCourseLogic ItemCourseLogic = new Tr_ItemCourseLogic();
        Tr_ItemCourse ItemCourse = ItemCourseLogic.GetById(TrainingItemCourseID);
        if (ItemCourse != null)
        {
            Tr_Item item = new Tr_ItemLogic().GetById(ItemCourse.TrainingItemID);
            TrainingItemID = item.TrainingItemID;
            lblItemName.Text = item.ItemName;
            Res_Course course = new Res_CourseLogic().GetById(ItemCourse.CourseID);
            lblCourseName.Text = course.CourseName;
        }
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
            lbnModify.Attributes.Add("onclick", string.Format("javascript:showWindow('编辑课程公告','{0}');javascript: return false;", this.ActionHref(string.Format("GuidanceManager/ArticleInfo.aspx?op={0}&id={1}&TrainingItemCourseID={2}", "edit", dr["ArticleID"], TrainingItemCourseID.ToString()))));
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
                    articleID = parm[1].ToInt();
                    int isUse = parm[0].ToInt() == 1 ? 0 : 1;
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