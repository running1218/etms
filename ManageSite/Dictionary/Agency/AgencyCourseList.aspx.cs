using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Controls;
using ETMS.Components.Basic.Implement.BLL.Security;
using ETMS.Utility;

namespace ETMS.WebApp.Manage
{
    public partial class AgencyCourseList : BasePage
    {
        private static readonly Site_AgencyProductLogic agencyProductLogic = new Site_AgencyProductLogic();
        protected void Page_Load(object sender, EventArgs e)
        {
            PageSet1.pageInit(gvList, PageDataSource);
            
            if (!Page.IsPostBack)
            {
                this.PageSet1.QueryChange();
            }

            btnAdd.Attributes.Add("onclick", string.Format("javascript: showWindow('新增代理课程', '{0}', 420, 360)", this.ActionHref(string.Format("AgencyCourseAdd.aspx?AgencyID={0}", AgencyID))));
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

        private int agencyID;
        protected int AgencyID
        {
            get
            {
                return Request.ToparamValue<int>("AgencyID");
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
            var list = agencyProductLogic.GetAgencyCourses(AgencyID, pageIndex, pageSize, out totalRecordCount);

            PageDataSourceProvider pageDataSource = new PageDataSourceProvider(list, pageIndex, pageSize);
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
                    agencyProductLogic.Delete(e.CommandArgument.ToGuid());
                    this.PageSet1.DataBind();
                }
                catch (ETMS.AppContext.BusinessException bizEx)
                {
                    this.ShowScriptManagerMessage(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
                }
            }
        }
    }
}