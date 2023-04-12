using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Controls;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Course;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Course.Resources;
using ETMS.Components.Basic.API.Entity.TrainingItem.Course;
using ETMS.Components.Basic.API.Entity.TrainingItem.Course.Resources;
using ETMS.Components.Basic.Implement.BLL.TrainingItem;
using ETMS.Components.Basic.API.Entity.TrainingItem;
using ETMS.Components.Basic.Implement.BLL.Course;
using ETMS.Components.Basic.API.Entity.Course;
using ETMS.Components.Basic.Implement.BLL.Teacher;
using ETMS.Utility;
using ETMS.Components.Basic.API.Entity.Teacher;
using ETMS.Components.Basic.Implement.BLL.TraningOrgnization;
using System.Data;
using ETMS.Components.Basic.API.Entity.TraningOrgnization;

public partial class TraningImplement_TraningProjectManager_SetsCourseView : BasePage
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
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack && !string.IsNullOrEmpty(BasePage.getSafeRequest(this.Page, "TrainingItemCourseID")))
        {
            TrainingItemCourseID = new Guid(BasePage.getSafeRequest(this.Page, "TrainingItemCourseID"));
            bind();
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
                DictionaryLabelCourseType.FieldIDValue = Course.CourseTypeID.ToString();
                DictionaryLabelCourseLevel.FieldIDValue = Course.CourseLevelID.ToString();
            }
            #endregion

            DictionaryLabelTeachModel.FieldIDValue = ItemCourse.TeachModelID.ToString();
            lblCourseStatus.Text = ItemCourse.CourseStatus == 1 ? "启用" : "停用";
            lblCourseBeginTime.Text = ItemCourse.CourseBeginTime.ToDate();
            lblCourseEndTime.Text = ItemCourse.CourseEndTime.ToDate();
            DictionaryLabelTrainingModel.FieldIDValue = ItemCourse.TrainingModelID.ToString();
            try
            {
                lblOuterOrg.Text = new Tr_OuterOrgLogic().GetById(ItemCourse.OuterOrgID).OuterOrgName;
            }
            catch { }

            lblCourseHours.Text = ItemCourse.CourseHours.ToString();
            lblOuterOrgDutyUser.Text = ItemCourse.OuterOrgDutyUser;
            lblScore.Text = ItemCourse.Score.ToString();
            lblOuterOrgEMAIL.Text = ItemCourse.OuterOrgEMAIL;

            DictionaryLabelCourseAttr.FieldIDValue = ItemCourse.CourseAttrID.ToString();
            lblBudgetFee.Text = ItemCourse.BudgetFee.ToString();
            lblRemark.Text = ItemCourse.Remark;
            lblPassLine.Text = string.Format("{0:N0}", ItemCourse.PassLine);
        }
    }
}