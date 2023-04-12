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

namespace ETMS.WebApp.Manage
{
    public partial class CourseResourceErrorList : BasePage
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
            DataTable dt = courseLogic.GetErrorResourcePageList(UserContext.Current.OrganizationID, pageIndex, pageSize, out totalRecordCount);

            PageDataSourceProvider pageDataSource = new PageDataSourceProvider(dt, pageIndex, pageSize);
            return pageDataSource.PageDataSource;
        }


        protected void gvList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Trans")
            {
                Guid contentID = e.CommandArgument.ToGuid();

                try
                {
                    Res_ContentLogic logic = new Res_ContentLogic();
                    logic.ReTranscode(contentID);
                    ETMS.Utility.JsUtility.AlertMessageBox("提交成功！");
                }
                catch (Exception ex)
                {
                    ETMS.Utility.JsUtility.AlertMessageBox(ex.Message);
                }
            }
        }
    }
}