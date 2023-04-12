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
using ETMS.AppContext;

public partial class TraningImplement_TraningProjectManager_TrainingProjectPracticeList : System.Web.UI.Page
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

    ///// <summary>
    ///// 获取课程id
    ///// </summary>
    //protected Guid CourseID
    //{
    //    get
    //    {
    //        if (ViewState["CourseID"] == null)
    //            ViewState["CourseID"] = Guid.Empty;
    //        return ViewState["CourseID"].ToGuid();
    //    }
    //    set
    //    {
    //        ViewState["CourseID"] = value;
    //    }
    //}
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
        PageSet1.PageSize = 5;
        if (!Page.IsPostBack )
        {                   
            this.PageSet1.QueryChange();
        }            
        btnAdd.Attributes.Add("onclick", string.Format("javascript:showWindow('实践管理','{0}',650,500);javascript: return false;", this.ActionHref(string.Format("TrainingProjectPracticeAdd.aspx?op={0}", "add"))));
    }
   

    private IList getDataSource1(int pageIndex, int pageSize, out int totalRecords)
    {

        var jobName = this.txt_JobName.Text.Trim();
        var itemName = this.txt_ItemName.Text.Trim();
        DataTable dataList =itemCourseLogic.GetPageList(jobName, itemName,UserContext.Current.OrganizationID);
        totalRecords = dataList.Rows.Count;
        //if (dataList.Rows.Count == 0)
        //{
        //    ltlNull.Visible = true;
        //    ltlNull.Text = "没有任何记录！";
        //}
        //else
        //{
        //    ltlNull.Visible = false;
        //}

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
            catch
            {
                //临时添加讲师被删除后处理，后续删除已添加限制
            }
            DataRowView view = (DataRowView)e.Row.DataItem;          
            LinkButton lbnModify = (LinkButton)e.Row.FindControl("btnModify");
            //CustomLinkButton lbnDel = (CustomLinkButton)e.Row.FindControl("lbtn_Del");      
            lbnModify.Attributes.Add("onclick", string.Format("javascript:showWindow('实践管理','{0}',650,500);javascript: return false;", this.ActionHref(string.Format("TrainingProjectPracticeAdd.aspx?op={0}&id={1}", "edit", view["JobID"]))));
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
                    entity = Logic.GetById(new Guid(e.CommandArgument.ToString().Split(',')[0]));
                    Logic.RemoveItemOffLineJob(entity, new Guid(e.CommandArgument.ToString().Split(',')[1]));
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