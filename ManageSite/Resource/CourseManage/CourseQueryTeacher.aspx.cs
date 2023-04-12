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
using ETMS.Components.Basic.Implement.BLL;

namespace ETMS.WebApp.Manage
{
    public partial class CourseQueryTeacher : BasePage
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
            if (!Page.IsPostBack)
            {
                this.PageSet1.QueryChange();
                InitialControl();
            }
        }

        private System.Collections.IList PageDataSource(int pageIndex, int pageSize, out int totalRecordCount)
        {
            totalRecordCount = 0;
            List<Site_Teacher> teacherList = courseTeacherLogic.GetTeachersByCourseID(pageIndex, pageSize, CourseID, out totalRecordCount);

            foreach (var entity in teacherList)
            {
                if (entity.TeacherSourceID == 1 && entity.OrganizationID != 0)
                {
                    ETMS.Components.Basic.Implement.BLL.Security.OrganizationLogic organizationLogic = new Components.Basic.Implement.BLL.Security.OrganizationLogic();
                    ETMS.Components.Basic.API.Entity.Security.Organization organization = (ETMS.Components.Basic.API.Entity.Security.Organization)organizationLogic.GetNodeByID(entity.OrganizationID);
                    entity.BelongOrgName = organization != null ? organization.NodeName : string.Empty;
                }
                else if (entity.TeacherSourceID == 2 && entity.OuterOrgID != Guid.Empty)
                {
                    ETMS.Components.Basic.Implement.BLL.TraningOrgnization.Tr_OuterOrgLogic outerOrgLogic = new Components.Basic.Implement.BLL.TraningOrgnization.Tr_OuterOrgLogic();
                    ETMS.Components.Basic.API.Entity.TraningOrgnization.Tr_OuterOrg outerOrg = outerOrgLogic.GetOuterOrgById(entity.OuterOrgID);
                    entity.BelongOrgName = outerOrg != null ? outerOrg.OuterOrgName : string.Empty;
                }
            }

            PageDataSourceProvider pageDataSource = new PageDataSourceProvider(teacherList, pageIndex, pageSize);
            return pageDataSource.PageDataSource;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.PageSet1.QueryChange();
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