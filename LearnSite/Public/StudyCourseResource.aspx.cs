using ETMS.Components.Basic.Implement.BLL.Course;
using ETMS.Components.Basic.Implement.BLL.Course.Resources;
using ETMS.Utility;
using System;
using System.Data;
using System.Linq;

namespace ETMS.Studying.Study
{
    public partial class StudyCourseResource : System.Web.UI.Page
    {

        /// <summary>
        /// 资源ID
        /// </summary>
        public Guid ContentID { get; set; }

        /// <summary>
        /// 课程名称
        /// </summary>
        public string CourseName { get; set; }
        /// <summary>
        /// 资源名称
        /// </summary>
        public string ResourceName { get; set; }
        /// <summary>
        /// 项目课程关系ID
        /// </summary>
        public Guid CourseID { get; set; }

        /// <summary>
        /// 资源类型
        /// </summary>
        public string ContentType { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            ContentID = Request["ContentID"]==null?"00000000-0000-0000-0000-000000000000".ToGuid():Request["ContentID"].ToGuid();
            CourseName= Request["CourseName"]==null?"": Request["CourseName"].ToString();
            ResourceName = Request["ResourceName"]==null?"" : Request["ResourceName"].ToString();
            CourseID = Request["CourseID"] == null ? "00000000-0000-0000-0000-000000000000".ToGuid() : Request["CourseID"].ToGuid();
            ContentType = Request["ContentType"] == null ? "" : Request["ContentType"].ToString();
            InitalControl();
        }

        private void InitalControl()
        {
            if (CourseID != Guid.Empty)
            {
                var dtResource = new Res_ContentVideoLogic().GetResourceByCourseID(CourseID);                 
                var openSource = new Res_ContentLogic().GetCourseOpenResource(BaseUtility.SiteOrganizationID, CourseID);
                foreach (DataRow row in dtResource.Rows)
                {
                    int count = openSource.Count(f => f.ResourceID == row["ContentID"].ToGuid());
                    if (count > 0)
                    {
                        row["IsOpen"] = 1;
                    }
                    else
                    {
                        row["IsOpen"] = 0;
                    }
                }
                ResourceList.DataSource = dtResource;
                ResourceList.DataBind();
            }
        }

        public string GetResourceStatus(Guid contentID)
        {
            if (ContentID == contentID)
                return "studying";
            return "";
        }
    }
}