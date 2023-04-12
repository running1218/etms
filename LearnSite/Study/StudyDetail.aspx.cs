using University.Mooc.AppContext;
using ETMS.Components.Basic.Implement.BLL.Course.Resources;
using ETMS.Utility;
using System;
using System.Data;
using System.Web.UI;

namespace ETMS.Studying.Study
{
    public partial class StudyDetail : System.Web.UI.Page
    {
        /// <summary>
        /// 资源ID
        /// </summary>
        public Guid ContentID { get; set; }

        /// <summary>
        /// 项目课程关系ID
        /// </summary>
        public Guid TrainingItemCourseID { get; set; }

        /// <summary>
        /// 课程名称
        /// </summary>
        public string CourseNameString { get; set; }
        /// <summary>
        /// 资源类型
        /// </summary>
        public string ContentType { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            ContentID = Request["ContentID"].ToGuid();
            TrainingItemCourseID = Request["TrainingItemCourseID"].ToGuid();
            ContentType = Request["ContentType"];
            if (!IsPostBack)
            {
                InitalControl();
                IninitStaticData();
            }
        }

        private void InitalControl()
        {
            if (TrainingItemCourseID != Guid.Empty)
            {
                DataTable dt= new Res_ContentVideoLogic().GetResourceByCourse(TrainingItemCourseID, UserContext.Current.UserID);
                ResourceList.DataSource = dt;
                ResourceList.DataBind();

                if (dt.Rows.Count > 0) {
                    CourseNameString = dt.Rows[0]["CourseName"].ToString();
                }
            }
        }

        protected void IninitStaticData()
        {
            string scriptKey = "SetCurrentStaticDataScriptKey";
            string scriptValue = string.Format("<script language='javascript' type='text/javascript'>currentUserStudyProgress = new UserStudyProgress('{0}', '{1}', null, '0', '0','{2}'); </script>", TrainingItemCourseID.ToString(), ContentID.ToString(), ContentType);
            if (!Page.ClientScript.IsStartupScriptRegistered(scriptKey))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), scriptKey, scriptValue);
            }
        }

        public string GetResourceStatus(string status) {
            //if (ContentID == contentID)
            //    return "studying";
            string result = "";
            switch (status)
            {
                case "1":
                    result = "study_end";
                    break;
                case "0":
                    result = "studying";
                    break;
                default:
                    result = "status_icon";
                    break;
            }
            return result;
        }
    }
}