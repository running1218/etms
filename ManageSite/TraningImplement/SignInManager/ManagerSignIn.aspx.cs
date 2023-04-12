using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using ETMS.Components.Basic.Implement.BLL;
using System.Data;
using ETMS.WebApp;
using ETMS.Controls;
using ETMS.Utility;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Course.Hours;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Course.Hours.Student;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Course;
using ETMS.Components.Basic.API.Entity.TrainingItem.Course;
using ETMS.Components.Basic.Implement.BLL.TrainingItem;
using ETMS.Components.Basic.API.Entity.TrainingItem;
using ETMS.Components.Basic.Implement.BLL.Course;
using ETMS.Components.Basic.API.Entity.Course;
using ETMS.Components.Basic.API.Entity.TrainingItem.Course.Hours;
using ETMS.Components.Basic.Implement.BLL.Security;
public partial class TraningImplement_SignInManager_ManagerSignIn : System.Web.UI.Page
{
    private static Tr_ItemCourseHoursLogic itemCourseHoursLogic = new Tr_ItemCourseHoursLogic();
    private static Tr_ItemCourseHoursStudentLogic itemCourseHoursStudentLogic = new Tr_ItemCourseHoursStudentLogic();
    private string ItemCourseHoursStudentIDs = string.Empty;
    /// <summary>
    /// 培训项目编码
    /// </summary>
    private Guid TrainingItemID
    {
        get { return Request.QueryString["TrainingItemID"].ToGuid(); }
    }
    /// <summary>
    /// 项目课时ID 
    /// </summary>
    public Guid ItemCourseHoursID
    {
        get { return Request.QueryString["ItemCourseHoursID"].ToGuid(); }
    }
    /// <summary>
    /// 项目课程ID
    /// </summary>
    public Guid TrainingItemCourseID
    {
        get { return Request.QueryString["TrainingItemCourseID"].ToGuid(); }
    }
    /// <summary>
    /// 老师名称
    /// </summary>
    public int TeacherID
    {
        get { return Request.QueryString["TeacherID"].ToInt(); }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        PageSet1.pageInit(this.CustomGridView1, PageDataSource);

        if (!Page.IsPostBack)
        {
            this.PageSet1.QueryChange();
            bind();
        }
        aBack.HRef = "TraningProjectList.aspx";
        //单机构版本隐藏“组织机构”列
        if (ETMS.Product.ProductDefine.IsSingleOrganizationVersion)
        {
            this.CustomGridView1.Columns[3].Visible = false;
        }
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
                lblItemCode.Text = item.ItemCode;
                lblItemName.Text = item.ItemName;
            }
            #endregion

            #region 课程相关信息
            Res_CourseLogic CourseLogic = new Res_CourseLogic();
            Res_Course Course = CourseLogic.GetById(ItemCourse.CourseID);
            if (Course != null)
            {
                lblCourseCode.Text = Course.CourseCode;
                lblCourseName.Text = Course.CourseName;
            }
            #endregion
            Tr_ItemCourseHours hours= itemCourseHoursLogic.GetById(ItemCourseHoursID);
            this.lblTrainingDate.Text = hours.TrainingDate.ToDate();
            this.lblTrainingTime.Text = hours.TrainingBeginTime.ToDateTime().ToString("HH:mm") + "-" + hours.TrainingEndTime.ToDateTime().ToString("HH:mm");

            UserLogic userlogic = new UserLogic();            
            this.lblTeacherName.Text = userlogic.GetUserByID(TeacherID).RealName;

        }
    }

   
    private System.Collections.IList PageDataSource(int pageIndex, int pageSize, out int totalRecordCount)
    {
        string whereStr = string.Empty;
        whereStr = BasePage.getQueryConditionFromQueryControlList(tableQueryControlList);
        DataTable dt = itemCourseHoursStudentLogic.GetItemCourseHoursStudentByItemCourseHoursID(ItemCourseHoursID, pageIndex, pageSize, " u.OrganizationID,u.DepartmentID,u.RealName", whereStr, out totalRecordCount);

        PageDataSourceProvider pageDataSource = new PageDataSourceProvider(dt, pageIndex, pageSize);
        return pageDataSource.PageDataSource;
    }

    protected void CustomGridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow && !this.CustomGridView1.IsEmpty)
        {
            DataRowView drv = e.Row.DataItem as DataRowView;
            LinkButton btnModify = e.Row.FindControl("lblModify") as LinkButton;

            //屏蔽修改限制
            //if (drv["AuditStatus"].ToInt() == 20)//请假审核通过的
            //{
            //    btnModify.Enabled = false;
            //    btnModify.CssClass = "link_colorGray";
            //    btnModify.ForeColor = System.Drawing.Color.Gray;
            //}
            //else 
            //{
                 btnModify.Enabled = true;
                 btnModify.Attributes.Add("href", string.Format("javascript:showWindow('" + HttpContext.Current.Server.HtmlEncode("修改签到") + "','{0}',650,500)", this.ActionHref(string.Format("ManagerSignInEdit.aspx?ItemCourseHoursStudentID={0}&TrainingItemCourseID={1}&ItemCourseHoursID={2}&TeacherID={3}", drv["ItemCourseHoursStudentID"], TrainingItemCourseID, ItemCourseHoursID, TeacherID))));
            //}

        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        this.PageSet1.QueryChange();
        upList.Update();
    }
    //批量签到
    protected void btnMoreSign_Click(object sender, EventArgs e)
    {

        Guid[] selectNum = CustomGridView.GetSelectedValues<Guid>(this.CustomGridView1);
        if (selectNum.Length < 1)
        {
            ETMS.WebApp.Manage.Extention.AlertMessageBox("请选择学员！");
            return;
        }

        string title ="管理签到";
        Response.Redirect(string.Format("javascript:showCodeWindow('" + title.Escape() + "','{0}')", this.ActionHref(string.Format("ManagerSignInEdit.aspx?SignNumber={0}&ItemCourseHoursStudentIDs={1}&TrainingItemCourseID={2}&ItemCourseHoursID={3}&TeacherID={4}", GetGridSelectGroup(), ItemCourseHoursStudentIDs, TrainingItemCourseID, ItemCourseHoursID, TeacherID))));
    }    

    //导入签到
    protected void btnImport_Click(object sender, EventArgs e)
    {
        Guid[] selectNum = CustomGridView.GetSelectedValues<Guid>(this.CustomGridView1);
        if (selectNum.Length < 1)
        {
            ETMS.WebApp.Manage.Extention.AlertMessageBox("请选择学员！");
            return;
        }
        Response.Redirect(string.Format("javascript:showWindow('导入签到','{0}')", this.ActionHref(string.Format("ImportSignInInfo.aspx?TrainingItemCourseID={0}&ItemCourseHoursID={1}&TeacherID={2}",TrainingItemCourseID, ItemCourseHoursID, TeacherID))));
    }

    /// <summary>
    /// 获取列表中选中
    /// </summary>
    public int GetGridSelectGroup()
    {
        int j = 0;
        for (int i = 0; i < this.CustomGridView1.Rows.Count; i++)
        {
            CheckBox chkSelect = (CheckBox)CustomGridView1.Rows[i].FindControl("CheckBox1");
            if (chkSelect.Checked)
            {
                ItemCourseHoursStudentIDs += CustomGridView1.DataKeys[i].Values["ItemCourseHoursStudentID"].ToString();
                if (i < this.CustomGridView1.Rows.Count)
                {
                    ItemCourseHoursStudentIDs += ",";
                }
                j++;
            }
        }
        return j;
    }

    //全部签到
    protected void btnSignOn_Click(object sender, EventArgs e)
    {
        if (this.CustomGridView1.IsEmpty)
        {
            ETMS.WebApp.Manage.Extention.AlertMessageBox("对不起没有数据！");
            return;
        }
        try
        {
            itemCourseHoursStudentLogic.ItemCourseHoursStudent_SigninALL(ItemCourseHoursID, ETMS.AppContext.UserContext.Current.RealName);            
        }
        catch (ETMS.AppContext.BusinessException bizEx)
        {
            ETMS.Utility.JsUtility.FailedMessageBox(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
        }
        catch (Exception ex)
        {
            ETMS.Utility.JsUtility.FailedMessageBox(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(ex));
        }
        this.PageSet1.DataBind();
    }
    //全部清除签到
    protected void btnSignOff_Click(object sender, EventArgs e)
    {
        if (this.CustomGridView1.IsEmpty)
        {
            ETMS.WebApp.Manage.Extention.AlertMessageBox("对不起没有数据！");
            return;
        }
        try
        {
            itemCourseHoursStudentLogic.ItemCourseHoursStudent_CancelSigninALL(ItemCourseHoursID, ETMS.AppContext.UserContext.Current.RealName);            
        }
        catch (ETMS.AppContext.BusinessException bizEx)
        {
            ETMS.Utility.JsUtility.FailedMessageBox(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
        }
        catch (Exception ex)
        {
            ETMS.Utility.JsUtility.FailedMessageBox(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(ex));
        } 
        this.PageSet1.DataBind();
    }
}