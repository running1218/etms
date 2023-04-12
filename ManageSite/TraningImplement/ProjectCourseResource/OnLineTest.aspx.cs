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
using ETMS.Components.Basic.API.Entity.TrainingItem.Course;
using ETMS.Components.Basic.Implement.BLL.Course;
using ETMS.Utility;
using ETMS.Components.Courseware.Implement.BLL;
using ETMS.Components.ExOnlineTest.Implement.BLL;
using ETMS.Components.Basic.API.Entity.TrainingItem;

public partial class TraningImplement_ProjectCourseResource_OnLineTest : System.Web.UI.Page
{
    #region 页面参数
    /// <summary>
    /// 项目课程ID
    /// </summary>
    public Guid TrainingItemCourseID
    {
        get
        {
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
        this.btnAdd.PostBackUrl = this.ActionHref(string.Format("OnLineTestAdd.aspx?TrainingItemCourseID={0}", TrainingItemCourseID));
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
        Res_ItemCourse_OnLineTestLogic Logic = new Res_ItemCourse_OnLineTestLogic();
        DataTable dt = Logic.GetTrainingItemSelectResourcesList(TrainingItemCourseID, out totalRecordCount);
        PageDataSourceProvider pageDataSource = new PageDataSourceProvider(dt, pageIndex, pageSize);
        return pageDataSource.PageDataSource;
    }


    /// <summary>
    /// 删除信息
    /// </summary>
    protected void cbtnDel_Click(object sender, EventArgs e)
    {
        Guid[] selectedValues = CustomGridView.GetSelectedValues<Guid>(this.CustomGridView1);
        if (selectedValues.Length == 0)
        {
            ETMS.WebApp.Manage.Extention.AlertMessageBox("请选择要删除的在线测试！");
            return;
        }
        else
        {
            try
            {
                Res_ItemCourse_OnLineTestLogic Logic = new Res_ItemCourse_OnLineTestLogic();
                Logic.BatchRemoveResourceFromItemCourse(selectedValues);
                ETMS.WebApp.Manage.Extention.SuccessMessageBox("信息删除成功！");
                this.PageSet1.DataBind(); upList.Update();
            }
            catch (ETMS.AppContext.BusinessException bizEx)
            {
                ETMS.WebApp.Manage.Extention.AlertMessageBox(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
                this.PageSet1.DataBind(); upList.Update();
                return;
            }
        }
    }
    
    /// <summary>
    /// 行邦定
    /// </summary>
    protected void CustomGridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lbtnEidt = (LinkButton)e.Row.FindControl("lbtnEidt");
            if (lbtnEidt != null)
            {
                lbtnEidt.Attributes["onclick"] = string.Format("javascript:showWindow('编辑在线测试','{0}',450,300); javascript:return false;"
                    , this.ActionHref(string.Format("OnLineTestEdit.aspx?TrainingItemCourseID={0}&ItemCourseResID={1}", TrainingItemCourseID, lbtnEidt.CommandArgument)));
            }
        }
    }
}