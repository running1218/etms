namespace ETMS.WebApp.Manage.Security.Department
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
        private static DepartmentLogic departmentidLogic = new DepartmentLogic();
        public int OrgID
        {
            get
            {
                return ETMS.AppContext.UserContext.Current.OrganizationID;
            }
        }
        protected void DepartmentOpeator_Command(object sender, CommandEventArgs e)
        {
            try
            {
                int nodeID = e.CommandArgument.ToInt();
                switch (e.CommandName)
                {
                    case "SwitchStatus":
                        departmentidLogic.SwitchDepartmentStatus(nodeID, ETMS.AppContext.UserContext.Current.RealName);
                        ETMS.Utility.JsUtility.SuccessMessageBox("设置部门状态操作成功！");
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
            this.PageSet1.pageInit(this.gvRoles, new ETMS.Controls.IPageDataSource(delegate(int pageIndex, int pageSize, out int totalRecordCount)
            {
                totalRecordCount = 0;
                Node root = new Department();
                IList dataList = null;
                if (this.DepartmentTree1.DepartmentID == 0)//获取机构下根部门列表
                {
                    dataList = departmentidLogic.GetRootDepartmentsByOrganizationID(this.OrgID);
                }
                else//获取部门下的子部门列表
                {
                    root.NodeID = this.DepartmentTree1.DepartmentID;
                    dataList = (IList)departmentidLogic.GetNodeTreeForManager(root, false).ChildNodes;
                }
                PageDataSourceProvider pageDataSource = new PageDataSourceProvider(dataList, pageIndex, pageSize);
                totalRecordCount = dataList.Count;
                return pageDataSource.PageDataSource;
            }
            ));
            //如果强类型数据，为空时，则通过此类型实例化数据。DataTable类型的无需设置
            this.gvRoles.EmptyData = new Department[] { new Department() };
            if (!Page.IsPostBack)
                this.PageSet1.QueryChange();

        }

        protected void gvRoles_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Node node = e.Row.DataItem as Node;
                departmentidLogic.GetNodeTreeForManager(node, false);

                (e.Row.FindControl("HyperLink1") as HyperLink).Text = string.Format("[{0}]管理", node.ChildNodes.Count);
            }
        }
    }

}