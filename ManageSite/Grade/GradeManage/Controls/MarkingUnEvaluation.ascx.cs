using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Utility;
using ETMS.Controls;
using System.Data;
using System.Collections;
using ETMS.Components.ExOfflineHomework.Implement.BLL;

public partial class Grade_GradeManage_Controls_MarkingUnEvaluation : System.Web.UI.UserControl
{
    private static Res_e_OffLineJobLogic Logic = new Res_e_OffLineJobLogic();

    ///// <summary>
    ///// 培训项目编号
    ///// </summary>
    //public Guid TrainingItemID
    //{
    //    get
    //    {
    //        return (Guid)ViewState["TrainingItemID"];
    //    }
    //    set
    //    {
    //        ViewState["TrainingItemID"] = value;
    //    }
    //}



    ///// <summary>
    ///// 离线作业编码
    ///// </summary>
    //public Guid JobID
    //{
    //    get
    //    {
    //        return (Guid)ViewState["JobID"];
    //    }
    //    set
    //    {
    //        ViewState["JobID"] = value;
    //    }
    //}


    //public Guid ItemCourseOffLineJobID
    //{
    //    get
    //    {
    //        return (Guid)ViewState["ItemCourseOffLineJobID"];
    //    }
    //    set
    //    {
    //        ViewState["ItemCourseOffLineJobID"] = value;
    //    }
    //}
    /// <summary>
    /// 查询条件 
    /// </summary>
    public string JobName
    {
        get
        {
            if (ViewState["JobName"] == null)
            {
                ViewState["JobName"] = "";
            }
            return (string)ViewState["JobName"];
        }
        set
        {
            ViewState["JobName"] = value;
        }
    }
    /// <summary>
    /// 查询条件 
    /// </summary>
    public string ItemName
    {
        get
        {
            if (ViewState["ItemName"] == null)
            {
                ViewState["ItemName"] = "";
            }
            return (string)ViewState["ItemName"];
        }
        set
        {
            ViewState["ItemName"] = value;
        }
    }
    public void Page_Load(object sender, EventArgs e)
    {
        this.PageSet1.pageInit(this.GridViewList, new IPageDataSource(this.getDataSource1));
        if (!Page.IsPostBack)
        {
            this.PageSet1.QueryChange();
        }

    }
    public void ReLoad()
    {
        this.PageSet1.pageInit(this.GridViewList, new IPageDataSource(this.getDataSource1));
        this.PageSet1.QueryChange();
    }
    private IList getDataSource1(int pageIndex, int pageSize, out int totalRecords)
    {
        DataTable dataList = Logic.GetUnEvaluationStuList(JobName,ItemName);
        totalRecords = dataList.Rows.Count;
        PageDataSourceProvider psp = new PageDataSourceProvider(dataList, pageIndex, pageSize);
        return psp.PageDataSource;
    }

    protected void GridViewList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow && !this.GridViewList.IsEmpty)
        {
            DataRowView drv = e.Row.DataItem as DataRowView;
            //判断批阅是不是可用。当StudentJobStatus==1不可用
            LinkButton ltnEvaluation = (LinkButton)e.Row.FindControl("ltnEvaluation");
            if (drv["StudentJobStatus"].ToString().Equals("1"))
            {
                ltnEvaluation.Enabled = false;
            }
            ltnEvaluation.Attributes.Add("onclick", string.Format("javascript:showWindow('批阅管理','{0}',650,500);javascript: return false;", this.ActionHref(string.Format("{0}/Grade/GradeManage/Marking.aspx?JobID={1}&StudentJobID={2}&TrainingItemID={3}&UserID={4}&op=edit", WebUtility.AppPath, drv["JobID"].ToGuid(), drv["StudentJobID"].ToGuid(), drv["TrainingItemID"].ToGuid(), drv["UserID"].ToInt()))));

        }
    }
}