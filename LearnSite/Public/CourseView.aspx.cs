using ETMS.Components.Basic.API.Entity.Course;
using ETMS.Components.Basic.API.Entity.Teacher;
using ETMS.Components.Basic.Implement.BLL.Course;
using ETMS.Components.Basic.Implement.BLL.Course.Teacher;
using ETMS.Utility;
using ETMS.Utility.Service.FileUpload;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace ETMS.Studying.Public
{
    public partial class CourseView : System.Web.UI.Page
    {
        public Guid CourseID
        {
            get
            {
                Guid CourseID = Guid.Empty;
                if (Request.QueryString["courseid"] != null)
                {
                    CourseID = Request.QueryString["courseid"].ToGuid();
                }
                return CourseID;
            }
        }

        public string CourseName
        {
            get
            {
                return ViewState["CourseName"].ToString();
            }
            set
            {
                ViewState["CourseName"] = value;
            }
        }
        public string CourseImageUrl
        {
            get
            {
                return ViewState["CourseImageUrl"].ToString();
            }
            set
            {
                ViewState["CourseImageUrl"] = value;
            }
        }
        public string TeacherName
        {
            get
            {
                return ViewState["TeacherName"].ToString();
            }
            set
            {
                ViewState["TeacherName"] = value;
            }
        }
        public string FocusCount
        {
            get
            {
                return ViewState["FocusCount"].ToString();
            }
            set
            {
                ViewState["FocusCount"] = value;
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
            //set teahcer info
            GetTeacherInfo();
            //绑定课程信息
            Res_Course course= new Res_CourseLogic().GetById(CourseID);
            CourseName = course.CourseName;
            CourseImageUrl = StaticResourceUtility.GetFullPathByFileType(FileUploadFunctionType.CourseLogo, string.IsNullOrEmpty(course.ThumbnailURL) ? "default.jpg" : course.ThumbnailURL);
            FocusCount = course.FocusCount.ToString(); //new Sty_StudentCourseLogic().GetStudentCourseUserTotal(CourseID).ToString();
            this.lblCourseOutline.Text = course.CourseIntroduction;
            this.lblForObject.Text = course.ForObject;
            //绑定老师
            int totalRecords = 0;
            List<Site_Teacher> listTeacher=new Res_TeacherCourseLogic().GetTeachersByCourseID(1, int.MaxValue - 1, CourseID,out totalRecords);
            this.CourseTeacherList.DataSource = listTeacher;
            this.CourseTeacherList.DataBind();

            //绑定教学资源
            int totalRecordCount = 0;
            DataTable dtResource=new Res_ContentLogic().GetPagedList(1, int.MaxValue - 1, " RCCR.[Sort] ASC ", " AND RCCR.[Status]=1 ", CourseID, out totalRecordCount);
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
            this.CourseResourceList.DataSource = dtResource;
            this.CourseResourceList.DataBind();

        }
        /// <summary>
        /// 获取学习的路径
        /// </summary>
        /// <param name="isOpen"></param>
        /// <param name="ContentID"></param>
        /// <returns></returns>
        protected string getStudyUrl(bool isOpen,Guid ContentID,string CourseName,string ResourceName,string Type,Guid CourseID)
        {
            if (!isOpen)
            {
                return "#";
            }
            else
            {
                return this.ActionHref(string.Format("~/Public/StudyCourseResource.aspx?ContentID={0}&CourseName={1}&ResourceName={2}&ContentType={3}&CourseID={4}", ContentID, CourseName, ResourceName, Type,CourseID));
            }
        }

        public void GetTeacherInfo()
        {
            List<Site_Teacher> teacherList = new Res_TeacherCourseLogic().GetCourseTeachers(CourseID);

            foreach (Site_Teacher item in teacherList)
            {
                item.PhotoUrl = StaticResourceUtility.GetFullPathByFileType(FileUploadFunctionType.UserIcon, string.IsNullOrEmpty(item.PhotoUrl) ? "default.gif" : item.PhotoUrl);
            }
            this.rptTeacher.DataSource = teacherList;
            this.rptTeacher.DataBind();
        }
    }
}