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
using ETMS.Components.Basic.Implement.BLL.ClassRoom;
using ETMS.Utility;

namespace ETMS.WebApp.Manage.Resource.ClassRoomManage
{
    public partial class ClassRoomList : ETMS.Controls.BasePage
    {
        private static Res_ClassRoomLogic classRoomLogic = new Res_ClassRoomLogic();
        #region 页面参数
        /// <summary>
        /// 查询条件 
        /// </summary>
        private string criteria;
        protected string Crieria
        {
            get
            {
                if (ViewState["Crieria"] == null)
                {
                    ViewState["Crieria"] = "";
                }
                return (string)ViewState["Crieria"];
            }
            set
            {
                ViewState["Crieria"] = value;
            }
        }
        
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            PageSet1.pageInit(this.CustomGridView1, PageDataSource);

            if (!Page.IsPostBack)
            {
                this.PageSet1.QueryChange();

            }

        }
        private System.Collections.IList PageDataSource(int pageIndex, int pageSize, out int totalRecordCount)
        {
            Crieria = BasePage.getQueryConditionFromQueryControlList(tableQueryControlList);
            Crieria += string.Format(" and OrgID={0}", ETMS.AppContext.UserContext.Current.OrganizationID);
            DataTable dt = classRoomLogic.GetPagedList(pageIndex, pageSize, string.Empty, Crieria, out totalRecordCount);

            PageDataSourceProvider pageDataSource = new PageDataSourceProvider(dt, pageIndex, pageSize);
            return pageDataSource.PageDataSource;
        }

        //查询
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.PageSet1.QueryChange();
            upList.Update();
        }

        //单个删除课程信息
        protected void CustomGridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Del")
            {
                try
                {
                    classRoomLogic.doRemove(e.CommandArgument.ToGuid());
                    this.PageSet1.DataBind();
                    upList.Update();
                }
                catch (ETMS.AppContext.BusinessException bizEx)
                {
                    ETMS.WebApp.Manage.Extention.ShowScriptManagerMessage(ExceptionUtility.GetTranslateExceptionMessage(bizEx));
                    return;
                }
            }
        }
    }
}