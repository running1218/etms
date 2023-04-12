using ETMS.Components.Basic.Implement.BLL.Security;
using ETMS.Components.Basic.Implement.BLL.Teacher;
using ETMS.Utility;
using System;
using System.Data;

namespace ETMS.Studying.Public
{
    public partial class TeacherInfo : System.Web.UI.Page
    {
        private  UserLogic userLogic = new UserLogic();
        private  Site_TeacherLogic site_TeacherLogic = new Site_TeacherLogic();
        private int TeacherID
        {
            get
            {
                int teacherID = 0;
                if (Request.QueryString["teacherid"] != null)
                {
                    teacherID = int.Parse(Request.QueryString["teacherid"]);
                }
                return teacherID;
            }
        }
        public string TeacherName
        {
            get {
                return ViewState["TeacherName"].ToString();
            }
            set {
                ViewState["TeacherName"] = value;
            }
        }
        public string TeacherLevelName
        {
            get
            {
                return ViewState["TeacherLevelName"].ToString();
            }
            set
            {
                ViewState["TeacherLevelName"] = value;
            }
        }
        public string TeacherPhotoUrl
        {
            get
            {
                return ViewState["TeacherPhotoUrl"].ToString();
            }
            set
            {
                ViewState["TeacherPhotoUrl"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitialControlers();
            }
        }
        /// <summary>
        /// 初始化页面
        /// </summary>
        private void InitialControlers()
        {
            DataTable dtTeacher = new Rec_TeacherLogic().GetFamousTeacherInfoByID(TeacherID);
            if (dtTeacher != null && dtTeacher.Rows.Count > 0)
            {
                TeacherName = dtTeacher.Rows[0]["RealName"].ToString();
                TeacherLevelName = dtTeacher.Rows[0]["TeacherLevelName"].ToString();
                TeacherPhotoUrl = StaticResourceUtility.GetFullPathByFileType("UserIcon", string.IsNullOrEmpty(dtTeacher.Rows[0]["PhotoUrl"].ToString()) ? "default.gif" : dtTeacher.Rows[0]["PhotoUrl"].ToString());
                lblExpertise.Text = dtTeacher.Rows[0]["Expertise"].ToString();
                lblWorkExperience.Text = dtTeacher.Rows[0]["WorkExperience"].ToString();
                lblBrife.Text = dtTeacher.Rows[0]["TeacherBrief"].ToString();

                //移除没有关联上课程的数据
                DataRow[] drNotCourse = dtTeacher.Select("CourseID IS NULL");
                foreach (DataRow dr in drNotCourse)
                {
                    dtTeacher.Rows.Remove(dr);
                }
                TeacherDataList.DataSource = dtTeacher;
                TeacherDataList.DataBind();
            }

        }
    }
}