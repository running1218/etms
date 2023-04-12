using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Controls;
using ETMS.Components.Basic.Implement.BLL.Course;
using System.Data;
using ETMS.AppContext;
using ETMS.Components.Basic.Implement.BLL.Course.Teacher;
using ETMS.Utility;

public partial class Resource_CourseOpenRange_CourseList : BasePage
{
        private static readonly Res_CourseLogic courseLogic = new Res_CourseLogic();
        protected void Page_Load(object sender, EventArgs e)
        {
            PageSet1.pageInit(gvList, PageDataSource);

            if (!Page.IsPostBack)
            {
                ddl_CourseStatus.SelectedValue = "1";
                this.PageSet1.QueryChange();
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
            DataTable dt = courseLogic.GetPagedList(pageIndex, pageSize, SortExpression, Crieria, out totalRecordCount);

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

        /// <summary>
        /// 返回开放机构数量
        /// </summary>
        /// <param name="courseID"></param>
        /// <returns></returns>
        protected int GetCourseOpenRangeCount(Guid courseID)
        {
            int orgCount = 0;
            CourseOpenRangeLogic courseOpenRanLogic = new CourseOpenRangeLogic();
            courseOpenRanLogic.GetList(courseID, out orgCount);
            return orgCount;
        }

        /// <summary>
        /// 返回开放机构连接
        /// </summary>
        /// <param name="courseID"></param>
        /// <returns></returns>
        protected string GetCourseOpenRangeUrl(string courseID)
        {
            return string.Format("javascript:showWindow(\"开放机构\",\"{0}\")"
                , this.ActionHref(string.Format("CourseOpenRange.aspx?CourseID={0}", courseID)));
        }
    }