using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using ETMS.Components.Basic.Implement.BLL;
using ETMS.Components.Basic.API.Entity.Course;
using ETMS.Controls;
using System.Data.SqlClient;
using ETMS.Components.Basic.Implement.BLL.Course;
using ETMS.Utility;
using ETMS.AppContext;
using ETMS.Components.Basic.Implement.BLL.Course.Teacher;

namespace ETMS.WebApp.Manage.Resource.CourseManage
{
    public partial class CourseQuery : BasePage
    {
        private static readonly Res_CourseLogic courseLogic = new Res_CourseLogic();
        protected void Page_Load(object sender, EventArgs e)
        {
            CourseID = this.Request.ToparamValue<Guid>("CourseID");
            PageSet1.pageInit(gvList, PageDataSource);

            if (!Page.IsPostBack)
            {
                this.PageSet1.QueryChange();

                if (ETMS.Product.ProductDefine.IsSingleOrganizationVersion)
                    gvList.HideColumn("CreateOrgID");
            }
        }

        #region 页面参数
        /// <summary>
        /// 查询条件 
        /// </summary>
        private string criteria;
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

        /// <summary>
        /// 排序条件
        /// </summary>
        private string sortExpression;
        protected string SortExpression
        {
            get
            {
                if (ViewState["SortExpression"] == null)
                {
                    ViewState["SortExpression"] = " CreateTime DESC ";
                }
                return (string)ViewState["SortExpression"];
            }
            set
            {
                ViewState["SortExpression"] = value;
            }
        }

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

        /// <summary>
        /// 查询结果
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalRecordCount"></param>
        /// <returns></returns>
        private System.Collections.IList PageDataSource(int pageIndex, int pageSize, out int totalRecordCount)
        {
            Crieria = BasePage.getQueryConditionFromQueryControlList(tableQueryControlList);
            Crieria += string.Format("{0} AND OrgID={1}", Crieria, UserContext.Current.OrganizationID);
            DataTable dt = courseLogic.GetQueryList(pageIndex, pageSize, SortExpression, Crieria, out totalRecordCount);

            PageDataSourceProvider pageDataSource = new PageDataSourceProvider(dt, pageIndex, pageSize);            
            return pageDataSource.PageDataSource;
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.PageSet1.QueryChange();
            upList.Update();
        }

        protected int GetCourseTeacherNum(Guid courseID)
        {
            return new Res_TeacherCourseLogic().GetCourseTeacherNum(courseID);
        }

        protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            LinkButton lbnTeacher = (LinkButton)e.Row.FindControl("lbnTeacher");
            if (null != lbnTeacher)
            {
                lbnTeacher.Attributes.Add("onclick", string.Format("javascript:showWindow('课程讲师','{0}');javascript: return false;", this.ActionHref(string.Format("~/Resource/CourseManage/CourseQueryTeacher.aspx?CourseID={0}", gvList.DataKeys[e.Row.RowIndex].ToGuid()))));
            }
        }
    }
}