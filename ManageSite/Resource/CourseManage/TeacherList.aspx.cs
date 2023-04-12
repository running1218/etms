using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ETMS.Controls;
using ETMS.WebApp;
using ETMS.Components.Basic.Implement.BLL.Course.Teacher;
using ETMS.Components.Basic.API.Entity.Teacher;
using ETMS.Utility;
using ETMS.Components.Basic.API.Entity.Course.Teacher;
using ETMS.Components.Basic.Implement.BLL.Course;
using ETMS.Components.Basic.API.Entity.Course;

namespace ETMS.WebApp.Manage
{
    public partial class CourseTeacherList : BasePage
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
        #endregion

        private static readonly Res_TeacherCourseLogic courseTeacherLogic = new Res_TeacherCourseLogic();

        protected void Page_Load(object sender, EventArgs e)
        {
            PageSet1.pageInit(this.gvList, PageDataSource);
            CourseID = this.Request.ToparamValue<Guid>("CourseID");
            btnAdd.Attributes.Add("onclick", string.Format("javascript:showWindow('添加讲师','{0}');javascript: return false;", this.ActionHref(string.Format("ChooseTeacher.aspx?CourseID={0}", CourseID))));
            if (!Page.IsPostBack)
            {
                this.PageSet1.QueryChange();
                InitialControl();
            }
        }

        private System.Collections.IList PageDataSource(int pageIndex, int pageSize, out int totalRecordCount)
        {
            totalRecordCount = 0;
            List<Site_Teacher> teacherList = courseTeacherLogic.GetPageList(pageIndex, pageSize, string.Empty, string.Format(" And CourseID = '{0}' ", CourseID),  out totalRecordCount);
            PageDataSourceProvider pageDataSource = new PageDataSourceProvider(teacherList, pageIndex, pageSize);
            return pageDataSource.PageDataSource;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.PageSet1.QueryChange();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            int[] teacherID = CustomGridView.GetSelectedValues<int>(this.gvList);
            if (teacherID.Length == 0)
            {
                JsUtility.AlertMessageBox("请选择要删除的讲师！");
                return;
            }

            new Res_TeacherCourseLogic().Delete(teacherID, CourseID);
            ETMS.Utility.JsUtility.SuccessMessageBoxAndTriggerSearchEvent("删除成功！");
        }

        private void InitialControl()
        {
            Res_Course entity = new Res_CourseLogic().GetById(CourseID);

            if (null != entity)
            {
                ltlProjectCode.Text = entity.CourseCode;
                lblProjectName.Text = entity.CourseName;
            }
        }
    }
}