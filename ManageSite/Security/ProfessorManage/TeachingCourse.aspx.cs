using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ETMS.Components.Basic.Implement.BLL;
using ETMS.Controls;
using ETMS.Utility;
using ETMS.Components.Basic.Implement.BLL.Course.Teacher;
using ETMS.Components.Basic.API.Entity.Course;
using ETMS.Components.Basic.Implement.BLL.Teacher;
using ETMS.Components.Basic.API.Entity.Teacher;
using ETMS.Components.Basic.Implement.BLL.Security;
using ETMS.Components.Basic.API.Entity.Security;
namespace ETMS.WebApp.Manage.Resource.ProfessorManage
{   
    public partial class TeachingCourse : ETMS.Controls.BasePage
    {
        public static Res_TeacherCourseLogic teacherCourseLogic = new Res_TeacherCourseLogic();
        public static Site_TeacherLogic site_TeacherLogic = new Site_TeacherLogic();
        public int TeacherID
        {
            get { return Request.QueryString["TeacherID"].ToInt(); }
        }
        public bool IsInner
        {
            get
            {
                if (Request.QueryString["IsInner"].ToInt() == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            PageSet1.pageInit(this.GridViewList, PageDataSource);
            if (!Page.IsPostBack)
            {
                this.PageSet1.QueryChange();
                InititalControl();
            }
            if(IsInner)
            {
                aBack.HRef = "ProfessorListInner.aspx";
            }
            else
            {
                aBack.HRef = "ProfessorListOutSide.aspx";
            }
        }

        private void InititalControl()
        {
            try
            {
                Site_Teacher teacher = new Site_Teacher();
                User entity = new User();
                if (!IsInner)
                {
                    teacher = site_TeacherLogic.GetById(TeacherID);
                    this.lblTeacherCode.Text = teacher.TeacherCode;
                    this.lblTeacherLevelID.FieldIDValue = teacher.TeacherLevelID.ToString();
                    this.lblOrgID.FieldIDValue = teacher.OuterOrgID.ToString();
                    entity = new UserLogic().GetUserByID(TeacherID);
                    this.lblTeacherName.Text = entity.RealName;
                }
                else 
                {
                    teacher = site_TeacherLogic.GetById(TeacherID);
                    entity=new UserLogic().GetUserByID(TeacherID);
                    Site_Student student = new Site_StudentLogic().GetById(TeacherID);
                    this.lblWorkerNoInner.Text = student.WorkerNo;
                    this.lblRealNameInner.Text = entity.RealName;
                    this.lblDepartmentInner.FieldIDValue = entity.DepartmentID.ToString();
                    this.lblTeacherLevelInner.FieldIDValue = teacher.TeacherLevelID.ToString();
                }
            }
            catch {}
        }

        private System.Collections.IList PageDataSource(int pageIndex, int pageSize, out int totalRecordCount)
        {
            List<Res_Course> dt = teacherCourseLogic.GetTeacherTeachCourse(TeacherID,pageIndex,pageSize,out totalRecordCount);
            PageDataSourceProvider pageDataSource = new PageDataSourceProvider(dt, pageIndex, pageSize);
            return pageDataSource.PageDataSource;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.PageSet1.QueryChange();
            upList.Update();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            int isInnerTemp = IsInner ? 1 : 0;
            Response.Redirect(this.ActionHref(string.Format("TeachingCourseAdd.aspx?TeacherID={0}&IsInner={1}", TeacherID, isInnerTemp)));
        }

        protected void btnDeletes_Click(object sender, EventArgs e)
        {
            try
            {
                Guid[] courseIDs = CustomGridView.GetSelectedValues<Guid>(this.GridViewList);
                if (courseIDs.Length > 0)
                {
                    teacherCourseLogic.DeleteTeacherCourse(TeacherID, courseIDs);
                    ETMS.WebApp.Manage.Extention.SuccessMessageBox("取消授课成功！");
                    this.PageSet1.DataBind();
                }
                else
                {
                    ETMS.WebApp.Manage.Extention.AlertMessageBox("请选择课程！");
                }
            }
            catch (ETMS.AppContext.BusinessException bizEx)
            {
                ETMS.WebApp.Manage.Extention.ShowScriptManagerMessage(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
                return;
            }
        }        
    }
}