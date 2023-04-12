﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Controls;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Course.Resources;
using System.Data;
using ETMS.Utility;
using ETMS.AppContext;

public partial class TraningImplement_ProjectCourseResourceBatch_Controls_ResourceStopList : System.Web.UI.UserControl
{
    #region 页面参数

    /// <summary>
    /// 查询条件 
    /// </summary>
    protected string Crieria
    {
        get
        {
            if (ViewState["Crieria"] == null)
                ViewState["Crieria"] = "";
            return (string)ViewState["Crieria"];
        }
        set
        {
            ViewState["Crieria"] = value;
        }
    }

    /// <summary>
    /// 排序条件
    /// </summary>
    protected string SortExpression
    {
        get
        {
            if (ViewState["SortExpression"] == null)
            {
                ViewState["SortExpression"] = "icr.CreateUser desc";
            }
            return (string)ViewState["SortExpression"];
        }
        set
        {
            ViewState["SortExpression"] = value;
        }
    }
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        PageSet1.pageInit(this.CustomGridView1, PageDataSource);
        PageSet1.PageSize = 20;
        if (!Page.IsPostBack)
        {
            this.PageSet1.QueryChange();
        }
    }

    private System.Collections.IList PageDataSource(int pageIndex, int pageSize, out int totalRecordCount)
    {
        Crieria = string.Format(" {0} ", BasePage.getQueryConditionFromQueryControlList(tableQueryControlList));
        Crieria += string.Format(" And i.OrgID = '{0}' ", UserContext.Current.OrganizationID);
        Tr_ItemCourseResLogic itemCourseRes = new Tr_ItemCourseResLogic();
        DataTable dt = itemCourseRes.GeItemCourseResNoUseInfo(pageIndex, pageSize, SortExpression, Crieria, out totalRecordCount);
        PageDataSourceProvider pageDataSource = new PageDataSourceProvider(dt, pageIndex, pageSize);
        return pageDataSource.PageDataSource;
    }

    /// <summary>
    /// 查询
    /// </summary>
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        this.PageSet1.QueryChange();
    }

    /// <summary>
    /// 跟据资源类型获得 各自浏览路径
    /// </summary>
    /// <param name="CourseResID">资源ID</param>
    /// <param name="CourseID">课件ID</param>
    /// <param name="CourseResTypeID">资源类型ID</param>
    /// <returns></returns>
    protected string getResUrl(string CourseResID, string CourseID, string CourseResTypeID)
    {
        string strUrl = string.Empty;
        switch (CourseResTypeID)
        {
            case "1": //在线课件
                strUrl = this.ActionHref(string.Format("~/Courseware/OpenCourseware.aspx?CourseWareID={0}&CourseID={1}", CourseResID, CourseID));
                break;
            case "2": //在线作业
                strUrl = this.ActionHref(string.Format("~/QuestionDB/Testpaper/TestpaperView.aspx?ExerciseType={0}&ExerciseID={1}", (int)ETMS.Components.Basic.Implement.BLL.BizExerciseType.ExOnlineHomework, CourseResID));
                break;
            case "5": //在线测试
                strUrl = this.ActionHref(string.Format("~/QuestionDB/Testpaper/TestpaperView.aspx?ExerciseType={0}&ExerciseID={1}", (int)ETMS.Components.Basic.Implement.BLL.BizExerciseType.ExOnlineTest, CourseResID));
                break;
            default:
                strUrl = "#";
                break;
        }
        return strUrl;
    }

    /// <summary>
    /// 启用
    /// </summary>
    protected void btn_Open_Click(object sender, EventArgs e)
    {
        Guid[] selectedValues = CustomGridView.GetSelectedValues<Guid>(this.CustomGridView1);
        if (selectedValues.Length == 0)
        {
            ETMS.Utility.JsUtility.AlertMessageBox("请选择要启用的资源！");
            return;
        }
        else
        {
            try
            {
                Tr_ItemCourseResLogic itemCourseRes = new Tr_ItemCourseResLogic();
                itemCourseRes.BatchSetResIsUse(selectedValues);
                this.PageSet1.DataBind();
                ETMS.Utility.JsUtility.SuccessMessageBox("提示", "资源启用成功！", "window.triggerRefreshEvent");
            }
            catch (ETMS.AppContext.BusinessException bizEx)
            {
                ETMS.Utility.JsUtility.FailedMessageBox(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
                this.PageSet1.DataBind();
                return;
            }
        }
    }

    /// <summary>
    /// 删除信息
    /// </summary>
    protected void cbtnDel_Click(object sender, EventArgs e)
    {
        Guid[] selectedValues = CustomGridView.GetSelectedValues<Guid>(this.CustomGridView1);
        if (selectedValues.Length == 0)
        {
            ETMS.Utility.JsUtility.AlertMessageBox("请选择要删除的资源！");
            return;
        }
        else
        {
            try
            {
                Tr_ItemCourseResLogic itemCourseRes = new Tr_ItemCourseResLogic();
                itemCourseRes.BatchRemove(selectedValues);
                ETMS.Utility.JsUtility.SuccessMessageBox(string.Format("成功删除 {0} 个资源！", selectedValues.Length));
                this.PageSet1.DataBind();
            }
            catch (ETMS.AppContext.BusinessException bizEx)
            {
                ETMS.Utility.JsUtility.FailedMessageBox(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
                return;
            }
        }
    }
}