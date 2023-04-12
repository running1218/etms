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
public partial class Admin_Controls_FunctionGroupTree : System.Web.UI.UserControl
{
    private static FunctionGroupLogic functionGroupLogic = new FunctionGroupLogic();
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
        if (!this.IsIncludeGroupID || Request.QueryString["groupid"] == "0")
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
        Node parentNode = functionGroupLogic.GetNodeTree(new FunctionGroup(), true);

        this.lsManager.Nodes.Clear();
        TreeNode rootTreeNode = new TreeNode();
        this.lsManager.Nodes.Add(rootTreeNode);
        doLoadTree(rootTreeNode, parentNode.ChildNodes[0]);
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
        if (IsIncludeGroupID && int.Parse(Request.QueryString["groupid"]) == node.NodeID)
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
    private bool IsIncludeGroupID
    {
        get
        {
            return !string.IsNullOrEmpty(Request.QueryString["groupid"]);
        }
    }

    public int CurrentRoleID
    {
        get
        {
            return this.lsManager.SelectedValue.ToInt();
        }
    }

    public int ParentRoleID
    {
        get
        {
            if (this.lsManager.SelectedNode.Parent != null)
            {
                return this.lsManager.SelectedNode.Parent.Value.ToInt();
            }
            else
            {
                return 0;
            }
        }
    }
    private int m_GroupID;
    public int GroupID
    {
        get
        {
            if (Request.QueryString["groupid"] == "0")
            {
                return 1;
            }
            else
            {
                return Request.ToparamValue<int>("groupid");
            }
        }
    }
}
