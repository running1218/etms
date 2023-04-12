using System;
using ETMS.Components.Basic.Implement.BLL.Course;
using ETMS.Utility;
using ETMS.Components.NoteQuestion.Implement.BLL;

namespace ETMS.Studying.Controls.Course
{
    public partial class LeftChapterTree : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var TrainingItemCourseID = Request["TrainingItemCourseID"].ToGuid();
            var logic = new UserNotesLogic();
            var CoursewareID = logic.GetCoursewareIDByTrainingItemCourseID(TrainingItemCourseID);
            var Res_ContentLogic = new Res_ContentLogic();
            ChapterTree.DataSource = Res_ContentLogic.GetCourseContentList(CoursewareID);
            ChapterTree.DataBind();
        }
    }
}