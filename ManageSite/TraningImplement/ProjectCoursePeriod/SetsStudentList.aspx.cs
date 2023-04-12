using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Controls;
using ETMS.Utility;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Course;
using ETMS.Components.Basic.API.Entity.TrainingItem.Course;
using ETMS.Components.Basic.Implement.BLL.TrainingItem;
using ETMS.Components.Basic.API.Entity.TrainingItem;
using ETMS.Components.Basic.Implement.BLL.Course;
using ETMS.Components.Basic.API.Entity.Course;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Course.Hours;
using ETMS.Components.Basic.API.Entity.TrainingItem.Course.Hours;
using ETMS.Components.Basic.Implement;
using ETMS.Components.Basic.Implement.BLL.ClassRoom;
using ETMS.Components.Basic.API.Entity.ClassRoom;
using System.Data;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Course.Hours.Student;
using ETMS.Components.Basic.Implement.BLL.Security;
using ETMS.Components.StudyClass.Implement.BLL.StudyClass;

public partial class TraningImplement_ProjectCoursePeriod_SetsStudentList : System.Web.UI.Page
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
    /// 项目课程课时ID
    /// </summary>
    public Guid ItemCourseHoursID
    {
        get
        {
            return ViewState["ItemCourseHoursID"].ToGuid();
        }
        set
        {
            ViewState["ItemCourseHoursID"] = value;
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
                ViewState["SortExpression"] = " u.OrganizationID,u.DepartmentID,u.RealName";
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

        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(BasePage.getSafeRequest(this.Page, "TrainingItemCourseID")))
            {
                TrainingItemCourseID = BasePage.getSafeRequest(this.Page, "TrainingItemCourseID").ToGuid();
                bind();
            }

            if (!string.IsNullOrEmpty(BasePage.getSafeRequest(this.Page, "ItemCourseHoursID")))
            {
                ItemCourseHoursID = new Guid(BasePage.getSafeRequest(this.Page, "ItemCourseHoursID"));
                bindHours();
            }
            this.PageSet1.QueryChange();
        }
        btnAdd.PostBackUrl = this.ActionHref(string.Format("SetsStudentAdd.aspx?TrainingItemCourseID={0}&ItemCourseHoursID={1}", TrainingItemCourseID, ItemCourseHoursID));
        lbtnReturn.PostBackUrl = this.ActionHref(string.Format("CoursePeriodList.aspx?TrainingItemCourseID={0}", TrainingItemCourseID));
    }

    /// <summary>
    /// 邦定信息
    /// </summary>
    private void bind() {
        //获得项目课程信息
        Tr_ItemCourseLogic ItemCourseLogic = new Tr_ItemCourseLogic();
        Tr_ItemCourse ItemCourse = ItemCourseLogic.GetById(TrainingItemCourseID);
        if (ItemCourse != null)
        {
            #region 项目名称
            Tr_ItemLogic itemLogic = new Tr_ItemLogic();
            Tr_Item item = itemLogic.GetById(ItemCourse.TrainingItemID);
            if (item != null)
                lblItemName.Text = item.ItemName;
            #endregion

            #region 课程相关信息
            Res_CourseLogic CourseLogic = new Res_CourseLogic();
            Res_Course Course = CourseLogic.GetById(ItemCourse.CourseID);
            if (Course != null)
                lblCourseName.Text = Course.CourseName;
            #endregion
        }
    }

    /// <summary>
    /// 邦定课时信息
    /// </summary>
    private void bindHours()
    {
        Tr_ItemCourseHoursLogic hoursLogic = new Tr_ItemCourseHoursLogic();
        Tr_ItemCourseHours courseHours = hoursLogic.GetById(ItemCourseHoursID);
        if (courseHours != null)
        {
            lblTrainingDate.Text = string.Format("{0}（{1}-{2}）"
                , courseHours.TrainingDate.ToDate()
                , courseHours.TrainingBeginTime.ToString("HH:mm")
                , courseHours.TrainingEndTime.ToString("HH:mm"));

            //讲师名称
            lblTeacher.Text = new PublicFacade().GetTeacherInfo(courseHours.TeacherID).UserInfo.RealName;

            //教室地址
            Res_ClassRoomLogic classRoomLogic = new Res_ClassRoomLogic();
            Res_ClassRoom classRoom = classRoomLogic.GetById(courseHours.ClassRoomID.ToGuid());
            if (classRoom != null)
            {
                lblClassRoomAddress.Text = string.Format("{0}", classRoom.ClassRoomName);
                if (!string.IsNullOrEmpty(classRoom.Address)) {
                    lblClassRoomAddress.Text += "（" + classRoom.Address + "）";
                }
            }
        }
    }
    
    private System.Collections.IList PageDataSource(int pageIndex, int pageSize, out int totalRecordCount)
    {
        Crieria = string.Format(" {0}", BasePage.getQueryConditionFromQueryControlList(tableQueryControlList));
        Tr_ItemCourseHoursStudentLogic hoursLogic = new Tr_ItemCourseHoursStudentLogic();
        DataTable dt = hoursLogic.GetItemCourseHoursStudentByItemCourseHoursID(ItemCourseHoursID, pageIndex, pageSize, SortExpression, Crieria, out totalRecordCount);
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
    /// 删除学员
    /// </summary>
    protected void btnDel_Click(object sender, EventArgs e)
    {
        Guid[] ItemCourseHoursStudentIDs = CustomGridView.GetSelectedValues<Guid>(this.CustomGridView1);
        if (ItemCourseHoursStudentIDs.Length == 0)
        {
            ETMS.Utility.JsUtility.AlertMessageBox("请选择要删除的学员！");
            return;
        }
        else
        {
            try
            {
                Tr_ItemCourseHoursStudentLogic hoursStudentLogic = new Tr_ItemCourseHoursStudentLogic();
                hoursStudentLogic.BatchDeleteStudentListFromCourseHours(ItemCourseHoursStudentIDs);
                ETMS.Utility.JsUtility.SuccessMessageBox("提示", "学员删除成功！");
                this.PageSet1.DataBind(); 
            }
            catch (ETMS.AppContext.BusinessException bizEx)
            {
                ETMS.Utility.JsUtility.FailedMessageBox(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
                return;
            }
        }
    }
    
    /// <summary>
    /// 行邦定
    /// </summary>
    protected void CustomGridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //单机构版本隐藏机构列
        if (ETMS.Product.ProductDefine.IsSingleOrganizationVersion)
        {
            e.Row.Cells[3].Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.DataRow) {
            ////班级
            //DictionaryLabel lblClassName = (DictionaryLabel)e.Row.FindControl("lblClassName");
            //lblClassName = lblClassName == null ? new DictionaryLabel() : lblClassName;

            //if (lblClassName.FieldIDValue.Trim() != "" && lblClassName.FieldIDValue.ToGuid() != Guid.Empty)
            //{
            //    lblClassName.Text = new Sty_ClassLogic().GetById(lblClassName.FieldIDValue.ToGuid()).ClassName;
            //}
        }
    }
}