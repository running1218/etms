using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Components.Basic.API.Entity.Teacher;
using ETMS.Utility;
using ETMS.Components.Basic.Implement.BLL.Teacher;
using ETMS.Components.Basic.Implement.BLL.Security;
using ETMS.Components.Basic.API.Entity.Security;

namespace ETMS.WebApp.Manage.Resource.ProfessorManage
{
    public partial class ProfessorEditInner : ETMS.Controls.BasePage
    {
        private static Site_TeacherLogic teacherLogic = new Site_TeacherLogic();
        private static Site_StudentLogic studentLogic = new Site_StudentLogic();
        /// <summary>
        /// operation 1:View
        /// </summary>
        public int Operation
        {
            get
            {
                if (Request.QueryString["op"] == null)
                    return 0;
                return Request.QueryString["op"].ToInt();
            }
        }
        public Site_Teacher Teacher
        {
            set
            {
                if (ViewState["Teacher"] == null)
                {
                    ViewState["Teacher"] = new Site_Teacher();
                }
                ViewState["Teacher"] = value;
            }
            get { return (Site_Teacher)ViewState["Teacher"]; }
        }

        public int TeacherID
        {
            get
            {
                return Request.QueryString["TeacherId"].ToInt();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitialControl();
            }
        }

        private void InitialControl()
        {
            Site_Student student = studentLogic.GetById(TeacherID);
            this.lblLoginName.Text = student.LoginName;
            this.lblWorkNo.Text = student.WorkerNo;
            this.lblTeacherName.Text = student.RealName;
            this.lblDepartment.FieldIDValue = student.DepartmentID.ToString();
            this.lblPost.FieldIDValue = student.PostID.ToString();

            Teacher = teacherLogic.GetById(TeacherID);
            this.rbStatus.SelectedValue = Teacher.IsUse.ToString();
            this.ddlTeacherLevel.SelectedValue = Teacher.TeacherLevelID.ToString();
            this.ddlTeacherType.SelectedValue = Teacher.TeacherTypeID.ToString();
            this.RblCourseDesinger.SelectedValue = Teacher.IsCourseDesigner.ToString();
            if (Operation == 1)
            {
                this.rbStatus.Enabled = false;
                this.LinkButton1.Visible = false;
                this.lblTeacherLevel.FieldIDValue = Teacher.TeacherLevelID.ToString();
                this.lblTeacherType.FieldIDValue = Teacher.TeacherTypeID.ToString();
                this.lblStatus.FieldIDValue = Teacher.IsUse.ToString();

            }
        }

        //初始化实例
        private void InitiEntity()
        {
            Teacher = teacherLogic.GetById(TeacherID);
            Teacher.TeacherLevelID = ddlTeacherLevel.SelectedValue.ToInt();
            Teacher.TeacherTypeID = ddlTeacherType.SelectedValue.ToInt();
            Teacher.IsUse = rbStatus.SelectedValue.ToInt();
            Teacher.IsCourseDesigner = RblCourseDesinger.SelectedValue.ToInt();
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            try
            {
                InitiEntity();
                teacherLogic.Update(Teacher);
                ETMS.Utility.JsUtility.SuccessMessageBoxAndCloseWindowAndTriggerRefreshEvent("保存成功");
            }
            catch (ETMS.AppContext.BusinessException bizEx)
            {
                ETMS.Utility.JsUtility.FailedMessageBox(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
            }
            catch (Exception ex)
            {
                ETMS.Utility.JsUtility.FailedMessageBox(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(ex));
            }
        }
    }



}