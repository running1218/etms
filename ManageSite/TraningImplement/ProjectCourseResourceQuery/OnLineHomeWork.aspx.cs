﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Controls;
using ETMS.Utility;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Course;
using ETMS.Components.Basic.API.Entity.TrainingItem.Course;
using ETMS.Components.Basic.API.Entity.TrainingItem;
using ETMS.Components.Basic.Implement.BLL.TrainingItem;
using ETMS.Components.Basic.Implement.BLL.Course;
using ETMS.Components.ExOnlineJob.Implement.BLL;
using System.Data;

public partial class TraningImplement_ProjectCourseResourceQuery_OnLineHomeWork : System.Web.UI.Page
{
    #region 页面参数
    /// <summary>
    /// 项目课程ID
    /// </summary>
    public Guid TrainingItemCourseID
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
    /// 选择的项目ID
    /// </summary>
    public string TrainingItemID
    {
        get
        {
            if (ViewState["TrainingItemID"] == null)
                ViewState["TrainingItemID"] = "";
            return ViewState["TrainingItemID"].ToString();
        }
        set { ViewState["TrainingItemID"] = value; }
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        PageSet1.pageInit(this.CustomGridView1, PageDataSource);
        PageSet1.PageSize = int.MaxValue - 1;
        if (!Page.IsPostBack && !string.IsNullOrEmpty(BasePage.getSafeRequest(this.Page, "TrainingItemCourseID")))
        {
            TrainingItemCourseID = new Guid(BasePage.getSafeRequest(this.Page, "TrainingItemCourseID"));
            bind();
            this.PageSet1.QueryChange();
        }
        this.aBack.HRef = this.ActionHref(string.Format("CourseList.aspx?TrainingItemID={0}", TrainingItemID));
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
            TrainingItemID = item.TrainingItemID.ToString();
            lblItemName.Text = item.ItemName;
            lblCourseName.Text = new Res_CourseLogic().GetById(ItemCourse.CourseID).CourseName;
        }
    }

    /// <summary>
    /// 查询
    /// </summary>
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        this.PageSet1.QueryChange(); upList.Update();
    }

    private System.Collections.IList PageDataSource(int pageIndex, int pageSize, out int totalRecordCount)
    {
        Res_ItemCourse_OnLineJobLogic OnLineJobLogic = new Res_ItemCourse_OnLineJobLogic();
        DataTable dt = OnLineJobLogic.GetTrainingItemSelectResourcesList(TrainingItemCourseID, out totalRecordCount);
        PageDataSourceProvider pageDataSource = new PageDataSourceProvider(dt, pageIndex, pageSize);
        return pageDataSource.PageDataSource;
    }
}