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
using ETMS.Components.OnlinePlaying.Implement.BLL;
using ETMS.Components.OnlinePlaying.API.Entity;

namespace ETMS.WebApp.Manage
{
    public partial class Living_CourseList : BasePage
    {
        private static readonly Res_CourseLogic courseLogic = new Res_CourseLogic();
        protected void Page_Load(object sender, EventArgs e)
        {
            PageSet1.pageInit(gvList, PageDataSource);
            
            if (!Page.IsPostBack)
            {
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
                    ViewState["SortExpression"] = " Sorting DESC, CreateTime DESC ";
                }
                return (string)ViewState["SortExpression"];
            }
            set
            {
                ViewState["SortExpression"] = value;
            }
        }

        protected string SortUrl
        {
            get
            {
                return this.ActionHref(string.Format("../Living/SetCourseSort.aspx"));
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
            Crieria += string.Format("{0} AND OrgID={1} And CourseModel = 2 ", Crieria, UserContext.Current.OrganizationID);
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
        }
       
        /// <summary>
        /// 删除课程信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void CustomGridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Del")
            {
                try
                {
                    courseLogic.Remove(e.CommandArgument.ToGuid());

                    var livings = new OnlinePlayingLogic().GetLivingByCourseID(e.CommandArgument.ToGuid());
                    foreach (var item in livings)
                    {
                        new OnlinePlayingLogic().DeleteLiving(item.LivingID);
                    }
                    this.PageSet1.DataBind();
                }
                catch (ETMS.AppContext.BusinessException bizEx)
                {
                    this.ShowScriptManagerMessage(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
                }
            }
        }

        protected int GetCourseTeacherNum(Guid courseID)
        {
            return new Res_TeacherCourseLogic().GetCourseTeacherNum(courseID);
        }
    }
}