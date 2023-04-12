using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;

using ETMS.Controls;
using ETMS.WebApp;
using ETMS.Utility;

using ETMS.Components.Basic.Implement.BLL;
using ETMS.Components.Basic.Implement.BLL.Teacher;
using ETMS.Components.Basic.Implement.BLL.TraningOrgnization;
using ETMS.Components.Basic.API.Entity.Teacher;
using ETMS.Components.Basic.API.Entity.TraningOrgnization;
using ETMS.Components.Basic.Implement.BLL.Course.Teacher;

namespace ETMS.WebApp.Manage
{
    public partial class ResourceProfessorListOutSide : ETMS.Controls.BasePage
    {
        public static Res_TeacherCourseLogic teacherCourseLogic = new Res_TeacherCourseLogic();
        public static Tr_OuterOrgLogic outerOrgLogic = new Tr_OuterOrgLogic();
        public static Site_TeacherLogic site_TeacherLogic = new Site_TeacherLogic();

        protected void Page_Load(object sender, EventArgs e)
        {
            PageSet1.pageInit(this.GridViewList, PageDataSource);

            if (!Page.IsPostBack)
            {
                this.PageSet1.QueryChange();
            }
        }

        private System.Collections.IList PageDataSource(int pageIndex, int pageSize, out int totalRecordCount)
        {
            string teacherName = txtTeacherName.Text.Trim();
            Guid outerOrgID = Dic_Organization.SelectedValue.ToGuid();
            int isUse = Dic_Status.SelectedValue.ToInt();
            int teacherLevelID = Dic_ProfessorGrade.SelectedValue.ToInt();
            int IsCollaborate = ddlCooperationRelation.SelectedValue.ToInt();
            DataTable dt = site_TeacherLogic.GetOuterTeacherList(ETMS.AppContext.UserContext.Current.OrganizationID, outerOrgID, isUse, teacherLevelID, teacherName, IsCollaborate);
            totalRecordCount = dt.Rows.Count;
            PageDataSourceProvider pageDataSource = new PageDataSourceProvider(dt, pageIndex, pageSize);
            return pageDataSource.PageDataSource;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.PageSet1.QueryChange();
            upList.Update();
        }

        protected void GridViewList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName)
                {
                    case "Del":
                            int TeacherID = int.Parse(e.CommandArgument.ToString());
                            if (teacherCourseLogic.GetTeacherTeachCourse(TeacherID).Count > 0)
                            {
                                this.ShowScriptManagerMessage("讲师已有授课，无法删除！");
                            }
                            else
                            {
                                site_TeacherLogic.Delete(TeacherID);
                                this.PageSet1.DataBind();
                                upList.Update();
                            }
                        break;
                    case "IsUse":
                        break;
                }
            }
            catch (ETMS.AppContext.BusinessException bizEx)
            {
                this.ShowScriptManagerMessage(ExceptionUtility.GetTranslateExceptionMessage(bizEx));
            }
        }
    }
}