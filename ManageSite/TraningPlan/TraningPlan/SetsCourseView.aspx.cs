using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Utility;
using ETMS.Controls;

using ETMS.Components.Basic.Implement.BLL.TrainingPlan.Course;
using ETMS.Components.Basic.Implement.BLL.TrainingPlan;
using ETMS.Components.Basic.Implement.BLL.Course;

using ETMS.Components.Basic.API.Entity.Course;
using ETMS.Components.Basic.API.Entity.TrainingPlan;
using ETMS.Components.Basic.API.Entity.TrainingPlan.Course;

namespace ETMS.WebApp.Manage.TraningPlan.TraningPlan
{
    public partial class SetsCourseView : System.Web.UI.Page
    {
    
        #region 页面参数

        /// <summary>
        /// 计划课程ID
        /// </summary>
        private Guid PlanCourseID
        {
            get
            {
                if (ViewState["PlanCourseID"] == null)
                    ViewState["PlanCourseID"] = Guid.Empty;
                return ViewState["PlanCourseID"].ToGuid();
            }
            set
            {
                ViewState["PlanCourseID"] = value;
            }
        }
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && !string.IsNullOrEmpty(BasePage.getSafeRequest(this.Page, "PlanCourseID")))
            {
                PlanCourseID = BasePage.getSafeRequest(this.Page, "PlanCourseID").ToGuid();
                bind();
            }
        }

        /// <summary>
        /// 邦定信息
        /// </summary>
        private void bind() {
            Tr_PlanCourseLogic planCourseLogic = new Tr_PlanCourseLogic();
            Tr_PlanCourse planCourse = planCourseLogic.GetById(PlanCourseID);
            if (planCourse != null) {

                //计划信息
                Tr_PlanLogic planLogic = new Tr_PlanLogic();
                Tr_Plan plan = planLogic.GetById(planCourse.PlanID);
                if (plan != null) {
                    lblPlanCode.Text = plan.PlanCode;
                    lblPlanName.Text = plan.PlanName;
                }

                //课程信息
                Res_CourseLogic courseLogic = new Res_CourseLogic();
                Res_Course course = courseLogic.GetById(planCourse.CourseID);
                if (course != null) {
                    lblCourseCode.Text = course.CourseCode;
                    lblCourseName.Text = course.CourseName;
                    lblCourseType.FieldIDValue = course.CourseTypeID.ToString();
                    lblForObject.Text = course.ForObject;
                    lblCourseIntroduction.Text = course.CourseIntroduction;
                    lblCourseOutline.Text = course.CourseOutline;
                    lblCourseStatus.FieldIDValue = course.CourseStatus.ToString();
                    lblCourseLevel.FieldIDValue = course.CourseLevelID.ToString();
                }

                lblTrainingModel.FieldIDValue = planCourse.TrainingModelID.ToString();
                lblTeachModel.FieldIDValue = planCourse.TeachModelID.ToString();
                dlblOuterOrg.FieldIDValue = planCourse.OuterOrgID.ToString();
                lblBudgetFee.Text = planCourse.BudgetFee.ToString();
            }
        }
    }
}