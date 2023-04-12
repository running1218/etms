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
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Course;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Course.Resources;
using ETMS.Components.Basic.API.Entity.TrainingItem;
using ETMS.Components.Basic.API.Entity.TrainingItem.Course;
using ETMS.Components.Basic.API.Entity.TrainingItem.Course.Resources;
using ETMS.Utility;
using ETMS.Components.Basic.Implement.BLL.Course;


public partial class TraningImplement_TraningProjectManager_SetsCourse : BasePage
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
    /// 查询条件 
    /// </summary>
    protected string Crieria
    {
        get
        {
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
                ViewState["SortExpression"] = " OrderNum";
            }
            return (string)ViewState["SortExpression"];
        }
        set
        {
            ViewState["SortExpression"] = value;
        }
    }

    /// <summary>
    /// 排序URL
    /// </summary>
    protected string SortUrl {
        get {
            return this.ActionHref(string.Format("SetsCourseSort.aspx?TrainingItemID={0}", TrainingItemID.ToString()));
        }
    }
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        PageSet1.pageInit(this.CustomGridView1, PageDataSource);
        if (!Page.IsPostBack && !string.IsNullOrEmpty(BasePage.getSafeRequest(this.Page, "TrainingItemID")))
        {
            TrainingItemID = new Guid(BasePage.getSafeRequest(this.Page, "TrainingItemID"));
            bind();
            this.PageSet1.QueryChange();
        }
        lbtnreturn.PostBackUrl = this.ActionHref(string.Format("../ProjectCourseResource/CourseList.aspx?TrainingItemID={0}", TrainingItemID.ToString()));
        btnAdd.PostBackUrl=this.ActionHref(string.Format("SetsCourseAdd.aspx?TrainingItemID={0}", TrainingItemID.ToString()));
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

            //培训项目发布，未规档的培训项目可以对某些基本信息修改，也可以向项目中追加课程。
            if (item.ItemStatus != 90 && item.IsIssue) {
                cbtnDel.Enabled = false;
            }
        }
    }

    private System.Collections.IList PageDataSource(int pageIndex, int pageSize, out int totalRecordCount)
    {
        Tr_ItemCourseLogic itemCourseLogic = new Tr_ItemCourseLogic();
        DataTable dt = itemCourseLogic.GetItemCourseListByTrainingItemID(TrainingItemID, pageIndex, pageSize, SortExpression, out totalRecordCount);

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
    /// 行绑定
    /// </summary>
    protected void CustomGridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string TrainingItemCourseID = CustomGridView1.DataKeys[e.Row.RowIndex].Value.ToString();

            //教师
            Label lblTeacherID = (Label)e.Row.FindControl("lblTeacherID");
            if (lblTeacherID != null && lblTeacherID.Text.Trim() != "")
            {
                lblTeacherID.Text = new ETMS.Components.Basic.Implement.PublicFacade().GetTeacherNameByID(lblTeacherID.Text.ToInt());
            }

            LinkButton Lbtn_Edit = (LinkButton)e.Row.FindControl("Lbtn_Edit");
            if (Lbtn_Edit != null) {
                Lbtn_Edit.Attributes["onClick"] = "javascript:showWindow('编辑项目课程信息','" + this.ActionHref("SetsCourseEdit.aspx?TrainingItemCourseID=" + TrainingItemCourseID) + "',650,500);javascript:return false;";
            }
            LinkButton lbtn_View = (LinkButton)e.Row.FindControl("lbtn_View");
            if (lbtn_View != null)
            {
                lbtn_View.Attributes["onClick"] = "javascript:showWindow('查看项目课程信息','" + this.ActionHref("SetsCourseView.aspx?TrainingItemCourseID=" + TrainingItemCourseID) + "',650,530);javascript:return false;";
            }
        }
    }

    /// <summary>
    /// 删除课程关系信息
    /// </summary>
    protected void cbtnDel_Click(object sender, EventArgs e)
    {
        Guid[] selectedValues = CustomGridView.GetSelectedValues<Guid>(this.CustomGridView1);
        if (selectedValues.Length == 0)
        {
            ETMS.Utility.JsUtility.AlertMessageBox("请选择要删除的课程！");
            return;
        }
        else
        {
            try
            {
                Tr_ItemCourseLogic ItemCourseLogic = new Tr_ItemCourseLogic();
                ItemCourseLogic.BatchRemoveItemCourseAndCourseware(selectedValues);
                ETMS.Utility.JsUtility.SuccessMessageBox("信息删除成功！");
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