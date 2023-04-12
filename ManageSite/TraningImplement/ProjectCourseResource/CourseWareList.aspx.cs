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
using ETMS.Components.Basic.API.Entity.TrainingItem;
using ETMS.Components.Basic.API.Entity.Course;

public partial class TraningImplement_ProjectCourseResource_CourseWareList : System.Web.UI.Page
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
    /// 课程ID
    /// </summary>
    public Guid CourseID
    {
        get
        {
            return ViewState["CourseID"].ToGuid();
        }
        set
        {
            ViewState["CourseID"] = value;
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

    public string SortUrl;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        PageSet1.pageInit(this.CustomGridView1, PageDataSource);

        if (!Page.IsPostBack && !string.IsNullOrEmpty(BasePage.getSafeRequest(this.Page, "TrainingItemCourseID")))
        {
            TrainingItemCourseID = new Guid(BasePage.getSafeRequest(this.Page, "TrainingItemCourseID"));
            bind();
            this.PageSet1.QueryChange();
        }
        this.aBack.HRef = this.ActionHref(string.Format("CourseList.aspx?TrainingItemID={0}", TrainingItemID));
        this.btnAdd.PostBackUrl=this.ActionHref(string.Format("CourseWareAdd.aspx?TrainingItemCourseID={0}", TrainingItemCourseID));
        SortUrl = this.ActionHref(string.Format("CourseWareSort.aspx?TrainingItemCourseID={0}", TrainingItemCourseID));
    }

    /// <summary>
    /// 邦定基本信息
    /// </summary>
    private void bind() {
        Tr_ItemCourseLogic ItemCourseLogic = new Tr_ItemCourseLogic();
        Tr_ItemCourse ItemCourse=ItemCourseLogic.GetById(TrainingItemCourseID);
        if (ItemCourse != null)
        {
            Tr_Item item=new Tr_ItemLogic().GetById(ItemCourse.TrainingItemID);
            TrainingItemID = item.TrainingItemID.ToString();
            lblItemName.Text = item.ItemName;
            Res_Course course=new Res_CourseLogic().GetById(ItemCourse.CourseID);
            lblCourseName.Text = course.CourseName;
            CourseID = course.CourseID;
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
        Res_ItemCourse_CoursewareLogic CoursewareLogic = new Res_ItemCourse_CoursewareLogic();
        
        DataTable dt = CoursewareLogic.GetTrainingItemSelectResourcesList(TrainingItemCourseID, out totalRecordCount);
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
            ETMS.WebApp.Manage.Extention.AlertMessageBox("请选择要删除的课件！");
            return;
        }
        else
        {
            try
            {
                Res_ItemCourse_CoursewareLogic CoursewareLogic = new Res_ItemCourse_CoursewareLogic();
                CoursewareLogic.BatchRemoveCoursewareFromItemCourse(selectedValues);
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
        if (e.Row.RowType == DataControlRowType.DataRow) {
            LinkButton lbtnEidt = (LinkButton)e.Row.FindControl("lbtnEidt");
            if (lbtnEidt != null) {
                lbtnEidt.Attributes["onclick"] = string.Format("javascript:showWindow('编辑在线课件','{0}',450,300); javascript:return false;"
                    , this.ActionHref(string.Format("CourseWareEdit.aspx?TrainingItemCourseID={0}&ItemCourseResID={1}", TrainingItemCourseID, lbtnEidt.CommandArgument)));
            }
        }
    }

    /// <summary>
    /// 课件预览路径
    /// </summary>
    protected string GetViewUrl(object CoursewareID)
    {
       return this.ActionHref(string.Format("~/Courseware/OpenCourseware.aspx?CourseWareID={0}&CourseID={1}", CoursewareID, CourseID));
    }
}