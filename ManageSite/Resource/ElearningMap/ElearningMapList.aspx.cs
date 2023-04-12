using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using ETMS.Components.Basic.Implement.BLL;
using ETMS.Components.Basic.API.Entity.ELearningMap;
using ETMS.Controls;
using System.Data.SqlClient;
using ETMS.Components.Basic.Implement.BLL.ELearningMap;
using ETMS.Utility;
using ETMS.AppContext;
using System.Configuration;

namespace ETMS.WebApp.Manage
{
    public partial class ElearningMapList : BasePage
    {
        #region 页面参数
        /// <summary>
        /// 查询条件 
        /// </summary>
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

        protected string StudyMapType
        {
            get
            {
                if (null != ConfigurationManager.AppSettings["StudyMapType"])
                {
                    return ConfigurationManager.AppSettings["StudyMapType"];
                }
                else
                {
                    return "1";
                }
            }
        }
        #endregion

        protected static readonly Res_StudyMapLogic studyMapLogic = new Res_StudyMapLogic();
        protected void Page_Load(object sender, EventArgs e)
        {

            PageSet1.pageInit(this.gvList, PageDataSource);

            if (!Page.IsPostBack)
            {
                ddl_ELearningMapTypeID.DefaultValue = StudyMapType;
                this.PageSet1.QueryChange();
                SetDisVisibleColumn();
            }
        }

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

            var source = studyMapLogic.GetStudyMapList(pageIndex, pageSize, SortExpression, Crieria, out totalRecordCount);
            PageDataSourceProvider pageDataSource = new PageDataSourceProvider(source, pageIndex, pageSize);
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
            SetDisVisibleColumn();
        }

        void SetDisVisibleColumn()
        {
            if (StudyMapType == "1")
                gvList.Columns[2].HeaderStyle.CssClass = gvList.Columns[2].ItemStyle.CssClass = "hide";
            else if (StudyMapType == "2")
                gvList.Columns[3].HeaderStyle.CssClass = gvList.Columns[3].ItemStyle.CssClass = "hide";
        }

        /// <summary>
        /// 单个删除课程信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Del")
            {
                try
                {
                    studyMapLogic.RemoveCheck(new Guid(e.CommandArgument.ToString()));
                    this.PageSet1.DataBind();
                }
                catch (ETMS.AppContext.BusinessException bizEx)
                {
                    JsUtility.FailedMessageBox(ExceptionUtility.GetTranslateExceptionMessage(bizEx));
                    return;
                }
            }
        }
    }
}