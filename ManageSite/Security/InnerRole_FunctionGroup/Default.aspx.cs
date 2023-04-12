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
public partial class Admin_InnerRole_FunctionGroup_Default : ETMS.Controls.BasePage
{
    private static FunctionGroupLogic functionGroupLogic = new FunctionGroupLogic();
    private static RoleLogic roleLogic = new RoleLogic();

    private FunctionGroupFunctionRelationLogic functionGroupRelationManager = new FunctionGroupFunctionRelationLogic(null);
    private RoleFunctionRelationLogic roleFunctionRelationManager = new RoleFunctionRelationLogic(null);

    public int RoleID
    {
        get
        {
            return int.Parse(Request.QueryString["id"]);
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadMemberTree(sender, e);
        }
        //Tree控件全选功能的客户端脚本支持
        this.lsMember.Attributes.Add("onclick", "CheckEvent()");

    }
    #region 载入管理者Tree列表,成员Tree列表

    /// <summary>
    /// 载入被管理者（成员）层次列表
    /// </summary>
    protected void LoadMemberTree(object sender, EventArgs e)
    {
        this.lsMember.Nodes.Clear();

        //1、载入父角色对应的功能列表
        //2、根据当前角色所分配的功能列表勾选

        //载入全部的功能组列表
        {
            Node parentNode = new FunctionGroup();
            parentNode = functionGroupLogic.GetNodeTree(parentNode, true);

            TreeNode rootTreeNode = new TreeNode();
            rootTreeNode.Expanded = true;
            rootTreeNode.ImageUrl = ETMS.Utility.StaticResourceUtility.GetImageFullPath("default/functionSet.gif");
         
            this.lsMember.Nodes.Add(rootTreeNode);
            doLoadTree(rootTreeNode, parentNode.ChildNodes[0]);

            //2、载入各个功能组下的功能列表
            doloadFunction(this.lsMember.Nodes[0]);

        }
    }

    private void SortChildNode(TreeNode node)
    {

        if (node.ChildNodes.Count > 0)
        {
            //当前子节点排序
            //1、分离出功能节点及功能组节点
            System.Collections.Generic.List<TreeNode> groupchilds = new System.Collections.Generic.List<TreeNode>();
            System.Collections.Generic.List<TreeNode> funchilds = new System.Collections.Generic.List<TreeNode>();
            foreach (TreeNode childItem in node.ChildNodes)
            {
                if (childItem.Value.IndexOf("fid") != -1)
                {
                    funchilds.Add(childItem);
                }
                else
                {
                    groupchilds.Add(childItem);
                }
            }
            //功能组节点按照顺序排列，功能默认已经是顺序了
            groupchilds.Sort(new Comparison<TreeNode>(delegate(TreeNode A, TreeNode B)
            {
                int orderNoA = int.Parse(A.ToolTip.Split(':')[0]);
                int orderNoB = int.Parse(B.ToolTip.Split(':')[0]);
                int result = orderNoA.CompareTo(orderNoB);
                if (result == 0)//如果顺序相同，则比较NodeCode
                {
                    return A.Value.CompareTo(B.Value);
                }
                return result;
            }));
            //将排序后的功能组节点根功能节点合并
            groupchilds.AddRange(funchilds);

            //添加到当前子节点范围
            node.ChildNodes.Clear();
            foreach (TreeNode childItem in groupchilds)
            {
                node.ChildNodes.Add(childItem);
            }

            for (int i = 0; i < node.ChildNodes.Count; i++)
            {
                //子节点的子节点排序
                SortChildNode(node.ChildNodes[i]);
            }
        }
    }
    private void SelectTreeNode(TreeNode node)
    {
        node.Checked = true;
        if (node.Parent != null)
            SelectTreeNode(node.Parent);
    }
    private TreeNode GetTreeNodeByValue(TreeNode root, string value)
    {
        TreeNode findNode = null;
        //循环把目录结构显示出来
        ArrayList list = new ArrayList();
        int j = 0;
        for (int i = 0; i < value.Length; i++)
        {
            if ((i + 1) % 2 == 0)
            {
                string nodeValue = value.Substring(0, i + 1);
                TreeNode temp = doGetTreeNodeByValue(root, nodeValue);
                if (temp == null)
                {
                    temp = new TreeNode();
                    temp.Expanded = true;
                    FunctionGroup group = (FunctionGroup)functionGroupLogic.GetNodeByNodeCode(nodeValue);
                    temp.Value = group.NodeCode;
                    temp.Text = group.NodeName;
                    temp.ToolTip = string.Format("{0}:{1}", group.OrderNo, group.NodeName);
                    temp.ImageUrl = ETMS.Utility.StaticResourceUtility.GetImageFullPath("default/functionSet.gif");
                    if (j > 0)
                        ((TreeNode)list[j - 1]).ChildNodes.Add(temp);
                }
                list.Add(temp);
                j++;
            }
            if ((i + 1 >= value.Length))
            {
                findNode = (TreeNode)list[list.Count - 1];
            }
        }
        return findNode;
    }
    private TreeNode doGetTreeNodeByValue(TreeNode node, string value)
    {
        TreeNode findNode = null;
        if (node.Value == value)
        {
            findNode = node;
            return findNode;
        }
        else if (value.StartsWith(node.Value))
        {
            foreach (TreeNode childNode in node.ChildNodes)
            {
                findNode = doGetTreeNodeByValue(childNode, value);
                if (findNode != null)
                    return findNode;
            }
        }
        return null;
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

        for (int i = 0; i < node.ChildNodes.Count; i++)
        {
            TreeNode childTreeNode = new TreeNode();
            childTreeNode.Expanded = true;
            childTreeNode.ImageUrl = ETMS.Utility.StaticResourceUtility.GetImageFullPath("default/functionSet.gif");
            treeNode.ChildNodes.Add(childTreeNode);
            doLoadTree(childTreeNode, node.ChildNodes[i]);
        }
    }

    /// <summary>
    /// FunctionGroup载入完成后，载入功能列表
    /// 
    /// 功能列表载入规则：载入父角色中功能组对应的全部功能列表，结合当前角色已分配的功能列表，实现默认选择！
    /// </summary>
    /// <param name="node"></param>
    private void doloadFunction(TreeNode functionGroupTreeNode)
    {
        //子功能组处理
        for (int i = 0; i < functionGroupTreeNode.ChildNodes.Count; i++)
        {
            doloadFunction(functionGroupTreeNode.ChildNodes[i]);
        }
        //将自身下的功能罗列出来
        FunctionGroup functionGroup = new FunctionGroup();
        functionGroup.NodeID = functionGroupTreeNode.Value.ToInt();
        functionGroup.KeyName = "FunctionGroupID";
        functionGroupRelationManager.Manager = functionGroup;
        Function[] functions = (Function[])functionGroupRelationManager.GetAllMembers(" AND Status=1 ");

        Role parentRole = new Role();
        //当前子角色对应的功能列表
        parentRole.RoleID = this.RoleID;
        roleFunctionRelationManager.Manager = parentRole;
        RoleFunction[] childRolefunctions = (RoleFunction[])roleFunctionRelationManager.GetAllMembers(string.Format(" AND FunctionGroupID={0}", functionGroup.NodeID));
        foreach (Function function in functions)
        {
            TreeNode functionTreeNode = new TreeNode();
            functionTreeNode.Text = function.FunctionName;
            functionTreeNode.Value = string.Format("fid:{0}:{1}", function.FunctionGroupID, function.FunctionID);
            functionTreeNode.ImageUrl = ETMS.Utility.StaticResourceUtility.GetImageFullPath("default/function.gif");
            functionGroupTreeNode.ChildNodes.Add(functionTreeNode);

            //默认选中子节点
            RoleFunction findRoleFunction = Array.Find<RoleFunction>(childRolefunctions, new Predicate<RoleFunction>(delegate(RoleFunction find) { return (find.FunctionID == function.FunctionID); }));
            if (findRoleFunction != null)
            {
                functionTreeNode.Value = string.Format("{0}:{1}", functionTreeNode.Value, findRoleFunction.RoleFunctionID);
                SelectTreeNode(functionTreeNode);
            }
        }
    }
    #endregion
    private class RoleFunctionCommand
    {
        public RoleFunction m_RoleFunction;
        public string m_Command;
    }
    System.Collections.Generic.List<RoleFunctionCommand> selectedRoles = new System.Collections.Generic.List<RoleFunctionCommand>();
    private void LoadSelectedRole(TreeNode node)
    {
        foreach (TreeNode childNode in node.ChildNodes)
        {
            LoadSelectedRole(childNode);
        }
        if (node.Value.StartsWith("fid"))
        {
            string[] parms = node.Value.Split(':');
            RoleFunction roleFunction = new RoleFunction();
            roleFunction.FunctionGroupID = parms[1].ToInt();
            roleFunction.FunctionID = parms[2].ToInt();
            roleFunction.CreateTime = DateTime.Now;
            roleFunction.Creator = ETMS.AppContext.UserContext.Current.RealName;
            roleFunction.RoleFunctionID = (parms.Length == 4) ? parms[3].ToInt() : 0;
            roleFunction.RoleID = this.RoleID;
            RoleFunctionCommand entity = new RoleFunctionCommand();
            entity.m_RoleFunction = roleFunction;
            if (node.Checked && roleFunction.RoleFunctionID == 0)
            {
                entity.m_Command = "add";
            }
            else if (!node.Checked)
            {
                entity.m_Command = "delete";
            }
            if (!string.IsNullOrEmpty(entity.m_Command))
                selectedRoles.Add(entity);
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        //1、提取分配的角色列表
        LoadSelectedRole(this.lsMember.Nodes[0]);

        //2、根据提取的角色权限情况分别操作
        Role parentRole = new Role();
        parentRole.RoleID = this.RoleID;
        roleFunctionRelationManager.Manager = parentRole;
        foreach (RoleFunctionCommand item in selectedRoles)
        {
            //1、case：RoleFunctionCommand.m_Command=="add"
            if (item.m_Command == "add")
                roleFunctionRelationManager.Associate(item.m_RoleFunction);
            //1、case：RoleFunctionCommand.m_Command=="delete"
            if (item.m_Command == "delete")
                roleFunctionRelationManager.ReleaseAssociate(item.m_RoleFunction);
        }

        //重新载入成员列表
        LoadMemberTree(sender, e);
        
        //清除相关缓存
        Node roleDefine = roleLogic.GetNodeByID(this.RoleID);
        string key = "RolePageUrls_" + roleDefine.NodeCode;
        ETMS.Utility.BizCache.BizCacheHelper.RemoveCacheItem(key);
        ETMS.Utility.JsUtility.SuccessMessageBox("角色授权完成！");
    }

}
