namespace ETMS.WebApp.Manage.Security.Organization
{
    using System;
    using System.Data;
    using System.Configuration;
    using System.Collections;
    using System.Web;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.Web.UI.WebControls.WebParts;
    using System.Web.UI.HtmlControls;

    using ETMS.Utility;
    using ETMS.Components.Basic.API.Entity;
    using ETMS.Components.Basic.API.Entity.Common;
    using ETMS.Components.Basic.Implement.BLL.Common;
    using ETMS.Components.Basic.API.Entity.Security;
    using ETMS.Components.Basic.Implement.BLL.Security;
    using ETMS.Controls;

    public partial class Default : ETMS.Controls.BasePage
    {
        private static OrganizationLogic organizationLogic = new OrganizationLogic();
        protected override void OnInit(EventArgs e)
        {
            //如果没有传递orgid参数，则默认重定向到此
            if (string.IsNullOrEmpty(Request.QueryString["orgid"]))
            {
                Response.Redirect(this.ActionHref("default.aspx?orgid=0"));
            }
            base.OnInit(e);
        }
        /// <summary>
        /// 页面验证参数定义
        /// </summary>
        protected override RequestParameter[] PageRequestArgs
        {
            get
            {
                /*
                 * 需要验证的页面参数包含：
                 *    参数名  参数范围
                 * 1、parentid    {0,1,2..}
                 */
                return new RequestParameter[] 
            {
                RequestParameter.CreateRangeRequestParameter("orgid", Request.QueryString["orgid"],RequestParameter.NaturalInt32RangeVerify)
            };
            }
        }
        protected void OrganizationOpeator_Command(object sender, CommandEventArgs e)
        {
            try
            {
                int organizationID =e.CommandArgument.ToInt();
                switch (e.CommandName)
                {
                    case "SwitchStatus":
                        organizationLogic.SwitchOrganizationStatus(organizationID, ETMS.AppContext.UserContext.Current.RealName);
                        ETMS.Utility.JsUtility.SuccessMessageBox("设置机构状态操作成功！");
                        break;
                }
                //刷新
                this.PageSet1.DataBind();
            }
            catch (ETMS.AppContext.BusinessException bizEx)
            {
                ETMS.Utility.JsUtility.FailedMessageBox(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
            }          
        }
        protected void Page_Load(object sender, EventArgs e)
        {

            // /*
            //* 检查当前角色是否越权
            //*/
            // if (!WebSession.Administrator.IsSysAdmin)//仅系统管理员有此页访问权限
            // {
            //     ETMS.Utility.JsUtility.MessageBoxAndRedirect("访问被拒绝，原因：权限不足！", string.Format("{0}/Default.aspx", ETMS.Utility.WebUtility.AppPath), this.Page);
            //     return;
            // }
            this.PageSet1.pageInit(this.gvRoles, PageDataSource);
            this.gvRoles.EmptyData = new Organization[] { new Organization() };
            if (!Page.IsPostBack)
                this.PageSet1.QueryChange();

        }
        private IList PageDataSource(int pageIndex, int pageSize, out int totalRecordCount)
        {
            totalRecordCount = 0;
            Node root = new Organization();
            root.NodeID = this.OrganizationTree1.OrganizationID;
            IList dataList = (IList)organizationLogic.GetNodeTreeForManager(root, false).ChildNodes;
            PageDataSourceProvider pageDataSource = new PageDataSourceProvider(dataList, pageIndex, pageSize);
            totalRecordCount = dataList.Count;
            return pageDataSource.PageDataSource;
        }

        protected void gvRoles_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Node node = e.Row.DataItem as Node;
                organizationLogic.GetNodeTreeForManager(node, false);

                (e.Row.FindControl("HyperLink1") as HyperLink).Text = string.Format("[{0}]管理", node.ChildNodes.Count);
            }
        }
    }

}