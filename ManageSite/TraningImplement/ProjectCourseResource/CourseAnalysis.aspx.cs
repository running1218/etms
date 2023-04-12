using ETMS.Components.Basic.API.Entity.TrainingItem.Course;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Course;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Utility;
using ETMS.Components.Basic.Implement.BLL.Course;

public partial class TraningImplement_ProjectCourseResource_CourseAnalysis : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Guid courseID = Request.QueryString["CourseID"].ToGuid();
            var course = new Res_CourseLogic().GetById(courseID);
            ltlTitle.Text = string.Format("【{0}】",course.CourseName);

            Guid trainingItemCourseID = Request.QueryString["TrainingItemCourseID"].ToGuid();
            Tr_ItemCourseLogic logic = new Tr_ItemCourseLogic();
            TrainItemCourseStudyAnalysis result = logic.GetTrainItemCourseStudyAnalysis(trainingItemCourseID);

            ltlNum1.Text = result.ChooseCourseNum.ToString();
            ltlNum2.Text = result.CompletedNum.ToString();
            ltlNum3.Text = result.UnCompleteNum.ToString();
            ltlNum4.Text = result.UnStudyNum.ToString();
            ltlNum5.Text = string.Format("{0}", result.ContentCompleteRate);
            ltlNum6.Text = string.Format("{0}", result.JobCompleteRate);
            ltlNum7.Text = string.Format("{0}", result.CourseCompleteRate);
        }
    }
}