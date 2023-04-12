namespace ETMS.WebApp.Manage.Security.Controls
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
    using ETMS.Components.Basic.API.Entity.Security;
    using ETMS.Components.Basic.Implement.BLL.Security;
    public partial class DepartmentTree : System.Web.UI.UserControl
    {
        private static DepartmentLogic departmentLogic = new DepartmentLogic();
        private static object SelectIndexChangedEvent = new object();
        /// <summary>
        /// 角色改变时触发事件
        /// </summary>
        public event EventHandler SelectIndexChanged
        {
            add
            {
                this.Events.AddHandler(SelectIndexChangedEvent, value);
            }
            remove
            {
                this.Events.RemoveHandler(SelectIndexChangedEvent, value);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.LoadManagerTree();
            }
            if (!this.IsIncludeOrgID || Request.QueryString["departmentid"] == "0")
            {
                this.lsManager.Nodes[0].Selected = true;
            }
            this.lsManager_SelectedNodeChanged(sender, e);
        }
        protected void lsManager_SelectedNodeChanged(object sender, EventArgs e)
        {
            if (base.Events[SelectIndexChangedEvent] != null)
            {
                ((EventHandler)base.Events[SelectIndexChangedEvent])(this.lsManager, e);
            }
        }

        /// <summary>
        /// 载入管理者层次列表
        /// </summary>
        private void LoadManagerTree()
        {
            //1、载入机构下一级部门
            Node[] rootDepartments = departmentLogic.GetRootDepartmentsByOrganizationID(this.OrganizationID);
            OrganizationLogic orgLogic = new OrganizationLogic();
            //清除树节点
            this.lsManager.Nodes.Clear();
            TreeNode rootTreeNode = new TreeNode();
            rootTreeNode.Text = orgLogic.GetNodeByID(this.OrganizationID).NodeName;//机构名称做为部门根
            rootTreeNode.NavigateUrl = this.ActionHref(string.Format(this.RedirectUrlFormat, 0) + "&orgid=" + this.OrganizationID.ToString());
            this.lsManager.Nodes.Add(rootTreeNode);
            for (int i = 0; i < rootDepartments.Length; i++)
            {
                Node item = rootDepartments[i];
                //同时加载所有下级部门
                item = departmentLogic.GetNodeTreeForManager(item, true);
                //渲染树结构
                TreeNode treeNode = new TreeNode();
                rootTreeNode.ChildNodes.Add(treeNode);
                doLoadTree(treeNode, item);
            }
        }

        /// <summary>
        /// TreeNode 与业务 Node之间的挂接转化
        /// </summary>
        /// <param name="treeNode"></param>
        /// <param name="node"></param>
        private void doLoadTree(TreeNode treeNode, Node node)
        {
            treeNode.Text = node.NodeName;
            treeNode.Value = node.NodeID.ToString();
            treeNode.ImageUrl = ETMS.Utility.StaticResourceUtility.GetImageFullPath("default/functionSet.gif");
            if (IsIncludeOrgID && int.Parse(Request.QueryString["departmentid"]) == node.NodeID)
            {
                treeNode.Selected = true;
            }
            if (!string.IsNullOrEmpty(this.RedirectUrlFormat))
            {
                treeNode.NavigateUrl = this.ActionHref(string.Format(this.RedirectUrlFormat, node.NodeID) + "&orgid=" + this.OrganizationID.ToString());
            }
            for (int i = 0; i < node.ChildNodes.Count; i++)
            {
                TreeNode childTreeNode = new TreeNode();
                treeNode.ChildNodes.Add(childTreeNode);
                doLoadTree(childTreeNode, node.ChildNodes[i]);
            }
        }

        public string RedirectUrlFormat
        {
            get
            {
                if (ViewState["RedirectUrlFormat"] == null)
                {
                    ViewState["RedirectUrlFormat"] = string.Empty;// "default.aspx?groupid={0}";
                }
                return ((string)ViewState["RedirectUrlFormat"]).Replace("~", ETMS.Utility.WebUtility.AppPath);
            }
            set
            {
                ViewState["RedirectUrlFormat"] = value;
            }
        }
        private bool IsIncludeOrgID
        {
            get
            {
                return !string.IsNullOrEmpty(Request.QueryString["departmentid"]);
            }
        }

        /// <summary>
        /// 部门所属机构，默认通过用户所属机构属性获取！
        /// </summary>
        public int OrganizationID
        {
            get
            {
                return ETMS.AppContext.UserContext.Current.OrganizationID;
            }
        }
        /// <summary>
        /// 父部门ID
        /// </summary>
        public int DepartmentID
        {
            get
            {
                if (!string.IsNullOrEmpty(Request.QueryString["departmentid"]))
                {
                    return int.Parse(Request.QueryString["departmentid"]);
                }
                else
                {
                    return 0;
                }
            }
        }
    }

}