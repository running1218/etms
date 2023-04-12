﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Controls;
using System.Data;
using System.Collections;
using ETMS.Components.ExOfflineHomework.Implement.BLL;
using ETMS.Utility;

public partial class TraningImplement_ProjectCourseResource_ExOfflineHomework_Controls_ExerciseEvaluated : System.Web.UI.UserControl
{
    private static Res_e_OffLineJobLogic Logic = new Res_e_OffLineJobLogic();   

    /// <summary>
    /// 培训项目编号
    /// </summary>
    public Guid TrainingItemID
    {
        get
        {
            return (Guid)ViewState["TrainingItemID"];
        }
        set
        {
            ViewState["TrainingItemID"] = value;
        }
    }

    /// <summary>
    /// 课程编号CourseID
    /// </summary>
    public Guid CourseID
    {
        get
        {
            return (Guid)ViewState["CourseID"];
        }
        set
        {
            ViewState["CourseID"] = value;
        }
    }
    /// <summary>
    /// 培训项目课程编号TrainingItemCourseID
    /// </summary>
    public Guid TrainingItemCourseID
    {
        get
        {
            return (Guid)ViewState["TrainingItemCourseID"];
        }
        set
        {
            ViewState["TrainingItemCourseID"] = value;
        }
    }
    /// <summary>
    /// 离线作业编码
    /// </summary>
    public Guid JobID
    {
        get
        {
            return (Guid)ViewState["JobID"];
        }
        set
        {
            ViewState["JobID"] = value;
        }
    }

    public Guid ItemCourseOffLineJobID
    {
        get
        {
            return (Guid)ViewState["ItemCourseOffLineJobID"];
        }
        set
        {
            ViewState["ItemCourseOffLineJobID"] = value;
        }
    }
   
    public void Page_Load(object sender, EventArgs e)
    {
        this.PageSet2.pageInit(this.GridViewList, new IPageDataSource(this.getDataSource1));
        if (!Page.IsPostBack)
        {
            this.PageSet2.QueryChange();
        }

    }

    public void ReLoad()
    {
        this.PageSet2.pageInit(this.GridViewList, new IPageDataSource(this.getDataSource1));
        this.PageSet2.QueryChange();
    }

    private IList getDataSource1(int pageIndex, int pageSize, out int totalRecords)
    {
        DataTable dataList = Logic.GetEvaluationStudentListbyItemCourseOffLineJobID(ItemCourseOffLineJobID);
        totalRecords = dataList.Rows.Count;
        PageDataSourceProvider psp = new PageDataSourceProvider(dataList, pageIndex, pageSize);
       
        return psp.PageDataSource;
    }

    protected void GridViewList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow && !this.GridViewList.IsEmpty)
        {
            DataRowView drv = e.Row.DataItem as DataRowView;
            LinkButton ltnEvaluation = (LinkButton)e.Row.FindControl("ltnEvaluation");
           // BasePage basePage=this.Page as BasePage;
            ltnEvaluation.Attributes.Add("onclick", string.Format("javascript:showWindow('批阅管理','{0}',600,400);javascript: return false;", this.ActionHref(string.Format("{0}/TraningImplement/ProjectCourseResource/ExOfflineHomework/EvaluationInfo.aspx?JobID={1}&StudentJobID={2}&CourseID={3}&TrainingItemID={4}&UserID={5}&op=view",WebUtility.AppPath, JobID, drv["StudentJobID"].ToGuid(), CourseID, TrainingItemID, drv["UserID"].ToInt()))));

        }
    }
}