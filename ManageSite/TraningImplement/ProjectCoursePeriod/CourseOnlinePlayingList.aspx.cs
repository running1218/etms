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
using ETMS.Utility;
using ETMS.Components.OnlinePlaying.API.Entity;
using ETMS.Components.OnlinePlaying.Implement.BLL;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Course;
using ETMS.Components.Basic.API.Entity.TrainingItem.Course;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.StudentCourse;
using ETMS.Components.Basic.Implement.BLL.Course;
using ETMS.Components.Basic.Implement.BLL.TrainingItem;
using ETMS.Components.Basic.API.Entity.TrainingItem;
using ETMS.Components.Basic.API.Entity.Course;

public partial class TraningImplement_ProjectCoursePeriod_CourseOnlinePlayingList : System.Web.UI.Page
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
                ViewState["SortExpression"] = "";
            }
            return (string)ViewState["SortExpression"];
        }
        set
        {
            ViewState["SortExpression"] = value;
        }
    }
    #endregion

    /// <summary>
    /// 查询
    /// </summary>
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        this.PageSet1.QueryChange();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        PageSet1.pageInit(this.CustomGridView1, PageDataSource);

        if (!Page.IsPostBack && !string.IsNullOrEmpty(BasePage.getSafeRequest(this.Page, "TrainingItemCourseID")))
        {
            TrainingItemCourseID = BasePage.getSafeRequest(this.Page, "TrainingItemCourseID").ToGuid();
            bind();

            this.PageSet1.QueryChange();
        }
        btnAdd.Attributes["onclick"] = string.Format("javascript:showWindow('新增直播','{0}');javascript:return false", this.ActionHref(string.Format("OnlinePlayingAdd.aspx?TrainingItemCourseID={0}", TrainingItemCourseID)));
        lbtnReturn.PostBackUrl = this.ActionHref("CourseList.aspx");
    }

    /// <summary>
    /// 邦定
    /// </summary>
    private void bind()
    {
        //获得项目课程信息
        Tr_ItemCourseLogic ItemCourseLogic = new Tr_ItemCourseLogic();
        Tr_ItemCourse ItemCourse = ItemCourseLogic.GetById(TrainingItemCourseID);
        if (ItemCourse != null)
        {
            #region 项目代码与名称
            Tr_ItemLogic itemLogic = new Tr_ItemLogic();
            Tr_Item item = itemLogic.GetById(ItemCourse.TrainingItemID);
            if (item != null)
            {
                lblItemName.Text = item.ItemName;
            }
            #endregion

            #region 课程相关信息
            Res_CourseLogic CourseLogic = new Res_CourseLogic();
            Res_Course Course = CourseLogic.GetById(ItemCourse.CourseID);
            if (Course != null)
            {
                lblCourseName.Text = Course.CourseName;
            }
            #endregion
        }
    }

    private System.Collections.IList PageDataSource(int pageIndex, int pageSize, out int totalRecordCount)
    {
        OnlinePlayingLogic onlinePlayingLogic = new OnlinePlayingLogic();
        var source = onlinePlayingLogic.GetOnlinePlayingList(TrainingItemCourseID, pageIndex, pageSize, out totalRecordCount);
        PageDataSourceProvider pageDataSource = new PageDataSourceProvider(source, pageIndex, pageSize);
        return pageDataSource.PageDataSource;                
    }

    /// <summary>
    /// 行绑定
    /// </summary>
    protected void CustomGridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (CustomGridView1.DataKeys[e.Row.RowIndex].Value != null)
            {            
                Tr_CourseOnlinePlaying item = (Tr_CourseOnlinePlaying)e.Row.DataItem;
                string onlinePlayingID = item.OnlinePlayingID;

                LinkButton lbtnView = (LinkButton)e.Row.FindControl("lbtnView");
                lbtnView = lbtnView == null ? new LinkButton() : lbtnView;
                //编辑
                LinkButton lbtnEdit = (LinkButton)e.Row.FindControl("lbtnEdit");
                lbtnEdit = lbtnEdit == null ? new LinkButton() : lbtnEdit;

                CustomLinkButton lbnDel = (CustomLinkButton)e.Row.FindControl("clbtnDel");

                if (item.OnlineStatus)
                {
                    if (item.EndTime <= DateTime.Now)
                    {
                        lbnDel.Enabled = false;
                        lbnDel.CssClass = "link_colorGray";
                    }

                    lbtnEdit.Attributes["onclick"] = string.Format("javascript:showWindow('编辑直播信息','{0}');javascript:return false;"
                                    , this.ActionHref(string.Format("OnlinePlayingEdit.aspx?TrainingItemCourseID={0}&OnlinePlayingID={1}", TrainingItemCourseID, onlinePlayingID)));
                }
                else
                {
                    lbnDel.Enabled = lbtnEdit.Enabled = false;
                    lbtnEdit.CssClass = lbnDel.CssClass = "link_colorGray";
                }

                lbtnView.Attributes["onclick"] = string.Format("javascript:showWindow('直播信息','{0}');javascript:return false;"
             , this.ActionHref(string.Format("OnlinePlayingView.aspx?OnlinePlayingID={0}", onlinePlayingID)));
            }
        }
    }

    /// <summary>
    /// 行操作
    /// </summary>
    protected void CustomGridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //删除
        if (e.CommandName == "delonlineplaying")
        {
            try
            {
                OnlinePlayingLogic onlinePlayingLogic = new OnlinePlayingLogic();
                onlinePlayingLogic.DeleteOnlinePlaying(e.CommandArgument.ToString());
                ETMS.Utility.JsUtility.SuccessMessageBox("直播信息删除成功！");
                this.PageSet1.DataBind();
            }
            catch (ETMS.AppContext.BusinessException bizEx)
            {
                ETMS.Utility.JsUtility.AlertMessageBox(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
                return;
            }
        }
    }
}