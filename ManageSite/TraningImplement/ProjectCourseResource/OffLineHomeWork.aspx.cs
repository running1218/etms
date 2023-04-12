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
using ETMS.Components.ExOfflineHomework.Implement.BLL;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Course;
using ETMS.Utility;
using System.Collections;
using ETMS.Components.ExOfflineHomework.API.Entity;
using ETMS.Components.Basic.API.Entity.TrainingItem.Course;
using ETMS.Components.Basic.API.Entity.TrainingItem;
using ETMS.Components.Basic.Implement.BLL.TrainingItem;
using ETMS.Components.Basic.API.Entity.Course;
using ETMS.Components.Basic.Implement.BLL.Course;
using ETMS.Components.Basic.Implement;

public partial class TraningImplement_ProjectCourseResource_OffLineHomeWork :BasePage
{
    private static Res_e_OffLineJobLogic Logic = new Res_e_OffLineJobLogic();
    private static Tr_ItemCourseLogic itemCourseLogic = new Tr_ItemCourseLogic();
    /// <summary>
    /// 获取项目id
    /// </summary>
    protected Guid TrainingItemID
    {
        get
        {
            if (ViewState["TrainingItemID"] == null)
                ViewState["TrainingItemID"] = Guid.Empty;
            return ViewState["TrainingItemID"].ToGuid();
        }
        set
        {
            ViewState["TrainingItemID"] = value;
        }
    }
    /// <summary>
    /// 获取项目课程id
    /// </summary>
    protected Guid TrainingItemCourseID
    {
        get
        {
            if (ViewState["TrainingItemCourseID"] == null)
                ViewState["TrainingItemCourseID"] = Guid.Empty;
            return ViewState["TrainingItemCourseID"].ToGuid();
        }
        set
        {
            ViewState["TrainingItemCourseID"] = value;
        }
    }

    /// <summary>
    /// 获取课程id
    /// </summary>
    protected Guid CourseID
    {
        get
        {
            if (ViewState["CourseID"] == null)
                ViewState["CourseID"] = Guid.Empty;
            return ViewState["CourseID"].ToGuid();
        }
        set
        {
            ViewState["CourseID"] = value;
        }
    }
    /// <summary>
    /// 查询条件 
    /// </summary>
    protected string Crieria
    {
        get
        {
            if (ViewState["Crieria"] == null)
            {
                ViewState["Crieria"] = "";
            }
            return (string)ViewState["Crieria"];
        }
        set
        {
            ViewState["Crieria"] = value;
        }
    }

    /// <summary>
    /// 排序条件 " CreateTime "
    /// </summary>
    protected string SortExpression
    {
        get
        {
            if (ViewState["SortExpression"] == null)
            {
                ViewState["SortExpression"] = " CreateTime ";
            }
            return (string)ViewState["SortExpression"];
        }
        set
        {
            ViewState["SortExpression"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        this.PageSet1.pageInit(this.GridViewList, new IPageDataSource(this.getDataSource1));

        if (!Page.IsPostBack && !string.IsNullOrEmpty(BasePage.getSafeRequest(this.Page, "TrainingItemCourseID")))
        {
            TrainingItemCourseID = BasePage.getSafeRequest(this.Page, "TrainingItemCourseID").ToGuid();
            bind();
            this.PageSet1.QueryChange();
        }
        this.aBack.HRef = this.ActionHref(string.Format("CourseList.aspx?TrainingItemID={0}", TrainingItemID));
        this.btnAdd.PostBackUrl = this.ActionHref(string.Format("CourseWareAdd.aspx?TrainingItemCourseID={0}", TrainingItemCourseID));
        btnAdd.Attributes.Add("onclick", string.Format("javascript:showWindow('离线作业管理','{0}',650,500);javascript: return false;", this.ActionHref(string.Format("ExOfflineHomework/ExerciseAdd.aspx?op={0}&id={1}&TrainingItemCourseID={2}", "add", "0", TrainingItemCourseID.ToString()))));
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
            CourseID = course.CourseID;
        }
    }

    private IList getDataSource1(int pageIndex, int pageSize, out int totalRecords)
    {
        //Crieria = BasePage.getQueryConditionFromQueryControlList(tableQueryControlList);
        DataTable dataList = itemCourseLogic.GetOffLineJobListByTrainingItemCourseIDAndCondition(TrainingItemCourseID, Crieria, pageIndex, pageSize, out totalRecords); //Logic.GetPagedList(pageIndex, pageSize, SortExpression, Crieria, out totalRecords);

        PageDataSourceProvider psp = new PageDataSourceProvider(dataList, pageIndex, pageSize);
        return psp.PageDataSource;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        this.PageSet1.QueryChange();
        upList.Update();
    }
    protected void GridViewList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow && !this.GridViewList.IsEmpty)
        {
            DictionaryLabel lblCreateUser = (DictionaryLabel)e.Row.FindControl("lblCreateUser");
            lblCreateUser = lblCreateUser == null ? new DictionaryLabel() : lblCreateUser;
            try
            {
                lblCreateUser.Text = new PublicFacade().GetTeacherInfo(lblCreateUser.FieldIDValue.ToInt()).UserInfo.LoginName;
            }
            catch { 
                //临时添加讲师被删除后处理，后续删除已添加限制
            }
            
            DataRowView view = (DataRowView)e.Row.DataItem;
            HyperLink hlLink = (HyperLink)e.Row.FindControl("lblJobFileName");
            string fullUrl = ETMS.Utility.StaticResourceUtility.GetFullPathByFileType("OfflineJob", view["JobFileURL"].ToString());
            hlLink.NavigateUrl = fullUrl;

            LinkButton lbnModify = (LinkButton)e.Row.FindControl("btnModify");
            //CustomLinkButton lbnDel = (CustomLinkButton)e.Row.FindControl("lbtn_Del");

            lbnModify.Attributes.Add("onclick", string.Format("javascript:showWindow('离线作业管理','{0}',650,500);javascript: return false;", this.ActionHref(string.Format("ExOfflineHomework/ExerciseEdit.aspx?op={0}&id={1}&TrainingItemCourseID={2}", "edit", view["JobID"], TrainingItemCourseID.ToString()))));
        }
    }
    protected void GridViewList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            Res_e_OffLineJob entity = new Res_e_OffLineJob();
            switch (e.CommandName)
            {
                case "Del":

                    //Logic.Remove(new Guid(e.CommandArgument.ToString()));
                    entity = Logic.GetById(new Guid(e.CommandArgument.ToString()));
                    Logic.RemoveItemCourseOffLineJob(entity, TrainingItemCourseID);
                    this.PageSet1.DataBind();
                    upList.Update();
                    break;
                case "IsUse":

                    //Logic.Remove(new Guid(e.CommandArgument.ToString()));
                    entity = Logic.GetById(new Guid(e.CommandArgument.ToString()));
                    entity.IsUse = entity.IsUse == 1 ? 0 : 1;
                    Logic.Save(entity);
                    this.PageSet1.DataBind();
                    upList.Update();
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
