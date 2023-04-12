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
    public partial class OrganizationTree : System.Web.UI.UserControl
    {
        private static OrganizationLogic organizationLogic = new OrganizationLogic();
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
            if (!this.IsIncludeOrgID || Request.QueryString["orgid"] == "0")
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
            Node parentNode = organizationLogic.GetNodeTree(new Organization(), true);

            this.lsManager.Nodes.Clear();
            TreeNode rootTreeNode = new TreeNode();
            this.lsManager.Nodes.Add(rootTreeNode);
            parentNode.NodeName = "根机构";
            doLoadTree(rootTreeNode, parentNode);
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
            if (IsIncludeOrgID && int.Parse(Request.QueryString["orgid"]) == node.NodeID)
            {
                treeNode.Selected = true;
            }
            if (!string.IsNullOrEmpty(this.RedirectUrlFormat))
            {
                treeNode.NavigateUrl = this.ActionHref(string.Format(this.RedirectUrlFormat, node.NodeID));
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
                return !string.IsNullOrEmpty(Request.QueryString["orgid"]);
            }
        }
        private int m_OrganizationID;
        public int OrganizationID
        {
            get
            {
                return int.Parse(Request.QueryString["orgid"]);
            }
        }
    }

}