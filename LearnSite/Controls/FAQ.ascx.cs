using ETMS.Utility;
using System;

namespace ETMS.Studying.Controls
{
    public partial class FAQ : System.Web.UI.UserControl
    {
        /// <summary>
        /// 资源ID
        /// </summary>
        public Guid ContentID { get; set; }

        /// <summary>
        /// 项目课程关系ID
        /// </summary>
        public Guid TrainingItemCourseID { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            ContentID = Request["ContentID"].ToGuid();
            TrainingItemCourseID = Request["TrainingItemCourseID"].ToGuid();
        }
    }
}