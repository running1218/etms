using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Course.Hours;
using ETMS.Components.Basic.API.Entity.TrainingItem.Course.Hours;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Course.Teacher;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Course;
using ETMS.Components.Basic.API.Entity.TrainingItem.Course;
using ETMS.Components.Basic.Implement.BLL.TrainingItem;
using ETMS.Components.Basic.API.Entity.TrainingItem;
using ETMS.Components.Basic.API.Entity.Common;
using ETMS.Components.Basic.Implement.BLL.Security;
using ETMS.Components.Basic.Implement.BLL.Course;
using ETMS.Components.Basic.API.Entity.Course;
using ETMS.Utility;
using ETMS.Components.Basic.Implement.BLL.ClassRoom;
using System.Data;
using ETMS.Components.Basic.API.Entity.ClassRoom;
using ETMS.Components.Basic.Implement;

public partial class TraningImplement_ProjectCoursePeriod_Controls_CoursePeriodView : System.Web.UI.UserControl
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

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //单机构版本隐藏机构查询条件
            if (ETMS.Product.ProductDefine.IsSingleOrganizationVersion)
            {
                this.trOrg.Visible = false;
            }

            bind();
            bindHours();
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
                Node node = new OrganizationLogic().GetNodeByID(item.OrgID);
                if (node != null)
                    lblOrg.Text = node.NodeName;
            }
            #endregion

            #region 课程相关信息
            Res_CourseLogic CourseLogic = new Res_CourseLogic();
            Res_Course Course = CourseLogic.GetById(ItemCourse.CourseID);
            if (Course != null)
            {
                lblCourseCode.Text = Course.CourseCode;
                lblCourseName.Text = Course.CourseName;
                dlblCourseType.FieldIDValue = Course.CourseTypeID.ToString();
                dlblCourseLevel.FieldIDValue = Course.CourseLevelID.ToString();
            }
            #endregion

            dlblTeachModel.FieldIDValue = ItemCourse.TeachModelID.ToString();
            dlblTrainingModel.FieldIDValue = ItemCourse.TrainingModelID.ToString();
            dlblCourseAttr.FieldIDValue = ItemCourse.CourseAttrID.ToString();
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
            lblTrainingDate.Text = courseHours.TrainingDate.ToDate();
            lblTrainingBeginTime.Text = courseHours.TrainingBeginTime.ToString("HH:mm");
            lblTrainingEndTime.Text = courseHours.TrainingEndTime.ToString("HH:mm");
            dlblCourseHoursStatus.FieldIDValue = courseHours.CourseHoursStatusID.ToString();
            //讲师名称
            lblTeacher.Text = new PublicFacade().GetTeacherInfo(courseHours.TeacherID).UserInfo.RealName;

            dlblTrainingTimeDesc.FieldIDValue = courseHours.TrainingTimeDescID.ToString();
            lblCourseHours.Text = courseHours.CourseHours.ToString();
            //教室地址
            Res_ClassRoomLogic classRoomLogic = new Res_ClassRoomLogic();
            Res_ClassRoom classRoom = classRoomLogic.GetById(courseHours.ClassRoomID.ToGuid());
            if (classRoom != null)
            {
                string Address = string.IsNullOrEmpty(classRoom.Address) ? "" : "（" + classRoom.Address + "）";
                lblClassRoomAddress.Text = classRoom.ClassRoomName + Address;
            }
            lblCourseHoursDesc.Text = courseHours.CourseHoursDesc;
        }
    }
}