using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Utility;
using ETMS.Controls;
using ETMS.Components.Basic.Implement.BLL.Course.Teacher;
using ETMS.Components.Basic.Implement.BLL.Teacher;
using ETMS.Components.Basic.API.Entity.Course;

namespace ETMS.WebApp.Manage.Resource.ProfessorManage
{
    public partial class TeachingCourseAdd : System.Web.UI.Page
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
            }
            int isInnerTemp = IsInner ? 1 : 0;
            aBack.HRef = this.ActionHref(string.Format("TeachingCourse.aspx?TeacherID={0}&IsInner={1}", TeacherID, isInnerTemp));
        }

        private System.Collections.IList PageDataSource(int pageIndex, int pageSize, out int totalRecordCount)
        {
            List<Res_Course> dt = teacherCourseLogic.ChooseTeacherTeachCourse(TeacherID, this.txtCourseCode.Text.Trim(), this.txtCourseName.Text.Trim(), pageIndex, pageSize, out totalRecordCount);
            PageDataSourceProvider pageDataSource = new PageDataSourceProvider(dt, pageIndex, pageSize);
            return pageDataSource.PageDataSource;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.PageSet1.QueryChange();
           
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                Guid[] courseIDs = CustomGridView.GetSelectedValues<Guid>(this.GridViewList);
                if (courseIDs.Length > 0)
                {
                    teacherCourseLogic.SaveTeacherCourse(TeacherID, courseIDs);
                    //ETMS.WebApp.Manage.Extention.SuccessMessageBox("保存成功！");
                    this.PageSet1.DataBind();
                    int isInnerTemp = IsInner ? 1 : 0;
                    ETMS.WebApp.Manage.Extention.SuccessMessageBox("提示", "保存成功！", "function(){window.location = '" + this.ActionHref(string.Format("TeachingCourse.aspx?TeacherID={0}&IsInner={1}", TeacherID, isInnerTemp)) + "'}");
                }
                else
                {
                    JsUtility.AlertMessageBox("请选择课程！");
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