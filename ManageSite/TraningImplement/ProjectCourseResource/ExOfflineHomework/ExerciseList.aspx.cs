using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using ETMS.Controls;
using System.Data;
using ETMS.Components.ExOfflineHomework.Implement.BLL;
using ETMS.Components.ExOfflineHomework.API.Entity;
using ETMS.Utility;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Course;


public partial class QuestionDB_ExOfflineHomework_ExerciseList:ETMS.Controls.BasePage
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
            {
                //ViewState["TrainingItemID"] = new Guid("a7f97727-b0c2-4611-8060-ca05c3437227");
                ViewState["TrainingItemID"] = UrlParamDecode(Request.QueryString["TrainingItemID"]).ToGuid();
            }
            return ViewState["TrainingItemID"].ToGuid();
        }
        set
        {
            ViewState["TrainingItemID"] = value;
        }
    }
    /// <summary>
    /// 获取项目id
    /// </summary>
    protected Guid TrainingItemCourseID
    {
        get
        {
            if (ViewState["TrainingItemCourseID"] == null)
            {
                //ViewState["TrainingItemCourseID"] = new Guid("a7f97727-b0c2-4611-8060-ca05c3437227");
                ViewState["TrainingItemCourseID"] = UrlParamDecode(Request.QueryString["TrainingItemCourseID"]).ToGuid();
            }
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
            {
                ViewState["CourseID"] = UrlParamDecode(Request.QueryString["CourseID"]).ToGuid();
            }
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
    private string criteria;
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
    private string sortExpression;
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
        if (!Page.IsPostBack)
        {           
            this.PageSet1.QueryChange();
            InitialControlers();
        }
        aBack.HRef = "ItemExcerciseList.aspx";
    }
    private void InitialControlers()
    {
        btnAdd.Attributes.Add("onclick", string.Format("javascript:showWindow('新增离线作业','{0}',600,400);javascript: return false;", this.ActionHref(string.Format("ExerciseAdd.aspx?op={0}&id={1}&TrainingItemCourseID={2}", "add", "0", TrainingItemCourseID.ToString()))));
    }
    private IList getDataSource1(int pageIndex, int pageSize, out int totalRecords)
    {
        Crieria = BasePage.getQueryConditionFromQueryControlList(tableQueryControlList);
        //Crieria += string.Format(" AND JobID={0}", );
        //if (string.IsNullOrEmpty(this.txtSearchJobName.Text))
        //{
        //    Crieria = Crieria + string.Format(" and JobName='{0}'", this.txtSearchJobName.Text.Trim());
        //}
        //else if (this.ddlSearchStatus.SelectedValue != "-1")
        //{
        //    Crieria = Crieria + string.Format(" and IsUse={0}", this.ddlSearchStatus.SelectedValue);
        //}
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
            DataRowView view = (DataRowView)e.Row.DataItem;
            HyperLink hlLink = (HyperLink)e.Row.FindControl("lblJobFileName");
            string fullUrl = ETMS.Utility.StaticResourceUtility.GetFullPathByFileType("OfflineJob", view["JobFileURL"].ToString());
            hlLink.NavigateUrl = fullUrl;

            LinkButton lbnModify = (LinkButton)e.Row.FindControl("btnModify");
            //CustomLinkButton lbnDel = (CustomLinkButton)e.Row.FindControl("lbtn_Del");
            
            lbnModify.Attributes.Add("onclick", string.Format("javascript:showWindow('离线作业管理','{0}',600,400);javascript: return false;", this.ActionHref(string.Format("ExerciseEdit.aspx?op={0}&id={1}&TrainingItemCourseID={2}", "edit", view["JobID"], TrainingItemCourseID.ToString()))));
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
            //JsUtility.FailedMessageBox(ExceptionUtility.GetTranslateExceptionMessage(bizEx));
            return;
        }      
    }

}