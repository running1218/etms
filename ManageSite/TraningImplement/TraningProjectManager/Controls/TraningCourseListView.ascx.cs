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
using ETMS.Components.Basic.Implement.BLL.TrainingItem;
using ETMS.Components.Basic.API.Entity.TrainingItem;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Course;
using ETMS.Utility;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Course.Teacher;

public partial class TraningImplement_TraningProjectManager_Controls_TraningCourseListView : System.Web.UI.UserControl
{
    #region 页面参数
    /// <summary>
    /// 项目ID
    /// </summary>
    public Guid TrainingItemID
    {
        get
        {
            return ViewState["TrainingItemID"].ToGuid();
        }
        set
        {
            ViewState["TrainingItemID"] = value;
        }
    }
    /// <summary>
    /// 列表中是否显示负责讲师 
    /// </summary>
    public bool IsTeacherTotal
    {
        get
        {
            if (ViewState["IsTeacherTotal"] == null)
                ViewState["IsTeacherTotal"] = false;
            return (bool)ViewState["IsTeacherTotal"];
        }
        set
        {
            ViewState["IsTeacherTotal"] = value;
        }
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        PageSet1.pageInit(this.CustomGridView1, PageDataSource);
        if (!Page.IsPostBack)
        {
            bind();
            this.PageSet1.QueryChange();
        }
    }

    /// <summary>
    /// 邦定项目信息
    /// </summary>
    private void bind()
    {
        Tr_ItemLogic itemLogic = new Tr_ItemLogic();
        Tr_Item item = itemLogic.GetById(TrainingItemID);
        if (item != null)
        {
            Lbl_ItemCode.Text = item.ItemCode;
            Lbl_ItemName.Text = item.ItemName;
        }
    }

    private System.Collections.IList PageDataSource(int pageIndex, int pageSize, out int totalRecordCount)
    {
        Tr_ItemCourseLogic itemCourseLogic = new Tr_ItemCourseLogic();
        DataTable dt = itemCourseLogic.GetItemCourseListByTrainingItemID(TrainingItemID, pageIndex, pageSize,"OrderNum", out totalRecordCount);

        PageDataSourceProvider pageDataSource = new PageDataSourceProvider(dt, pageIndex, pageSize);
        return pageDataSource.PageDataSource;
    }

    /// <summary>
    /// 行绑定
    /// </summary>
    protected void CustomGridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //列表中是否显示负责讲师
        e.Row.Cells[9].Visible = IsTeacherTotal;

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Guid TrainingItemCourseID =CustomGridView1.DataKeys[e.Row.RowIndex].Value.ToGuid();
                        
            LinkButton lbtnTeacherTotal = (LinkButton)e.Row.FindControl("lbtnTeacherTotal");
            lbtnTeacherTotal = lbtnTeacherTotal == null ? new LinkButton() : lbtnTeacherTotal;

            //讲师数
            lbtnTeacherTotal.Text = new Tr_ItemCourseTeacherLogic().GetTeacherTotal(TrainingItemCourseID).ToString();
            lbtnTeacherTotal.Attributes["onclick"] = string.Format("javascript:showWindow('项目课程讲师信息','{0}',650,500);javascript:return false;"
                , this.ActionHref(string.Format("../../TraningProjectQuery/ProjectCourseTeacherList.aspx?TrainingItemCourseID={0}", TrainingItemCourseID)));
        }
    }
}