using ETMS.Utility;
using System;

namespace ETMS.Studying.Study
{
    public partial class StudyProgress : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var TrainingItemCourseID = Request["TrainingItemCourseID"].ToGuid();



        }
    }
}