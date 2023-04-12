using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Linq.SqlClient;
using ETMS.Controls;
using ETMS.WebApp;
using ETMS.Components.Basic.Implement.BLL.Course.Teacher;
using ETMS.Components.Basic.API.Entity.Teacher;
using ETMS.Utility;
using ETMS.Components.Basic.API.Entity.Course.Teacher;


namespace ETMS.WebApp.Manage
{
    public partial class CourseChooseTeacher : BasePage
    {
        #region fields
        public Guid CourseID
        {
            get
            {
                return (Guid)ViewState["CourseID"];
            }
            set
            {
                ViewState["CourseID"] = value;
            }
        }

        protected string Crieria
        {
            get
            {
                return (string)ViewState["Crieria"];
            }
            set
            {
                ViewState["Crieria"] = value;
            }
        }
        #endregion

        private static readonly Res_TeacherCourseLogic courseTeacherLogic = new Res_TeacherCourseLogic();

        protected void Page_Load(object sender, EventArgs e)
        {
            PageSet1.pageInit(this.gvList, PageDataSource);
            CourseID = this.Request.ToparamValue<Guid>("CourseID");
           
            if (!Page.IsPostBack)
            {
                this.PageSet1.QueryChange();
            }
        }

        private System.Collections.IList PageDataSource(int pageIndex, int pageSize, out int totalRecordCount)
        {
            totalRecordCount = 0;
            List<Site_Teacher> teacherList = courseTeacherLogic.GetTeacherChooseList(pageIndex, pageSize, txt_TeacherName.Text, string.Format(" And CourseID = '{0}' ", CourseID), out totalRecordCount);
            PageDataSourceProvider pageDataSource = new PageDataSourceProvider(teacherList, pageIndex, pageSize);
            return pageDataSource.PageDataSource;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.PageSet1.QueryChange();
        }

        protected void lbtnSave_Click(object sender, EventArgs e)
        {
            int[] teacherID = CustomGridView.GetSelectedValues<int>(this.gvList);
            if (teacherID.Length == 0)
            {
                JsUtility.AlertMessageBox("请选择讲师！");
                return;
            }

            new Res_TeacherCourseLogic().Save(teacherID, CourseID);
            ETMS.Utility.JsUtility.SuccessMessageBoxAndCloseWindowAndTriggerSearchEvent("添加讲师成功！");
        }
    }
}