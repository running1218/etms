﻿using System;
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
using System.Collections.Generic;
using ETMS.Components.Basic.API.Entity;
using ETMS.Components.Basic.API.Entity.Common;
using ETMS.Components.Basic.Implement.BLL.Common;
using ETMS.Components.Basic.API.Entity.Security;
using ETMS.Components.Basic.Implement.BLL.Security;
public partial class Admin_Role_FunctionGroup_Default : ETMS.Controls.BasePage
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

        //根据角色与功能分配关系载入角色对应的功能组列表
        {
            //当前角色对应的功能列表  
            roleFunctionRelationManager.Manager = new Role() { RoleID = this.RoleID };
            RoleFunction[] rolefunctions = (RoleFunction[])roleFunctionRelationManager.GetAllMembers("");


            //载入机构管理员角色权限
            RoleFunction[] rootRoleFuntions = null;
            {
                Role parentRole = new Role();
                //当前子角色对应的功能列表
                parentRole.RoleID = 2;//内置机构管理员角色
                roleFunctionRelationManager.Manager = parentRole;
                rootRoleFuntions = (RoleFunction[])roleFunctionRelationManager.GetAllMembers("");
            }
            List<Node> parentFunctionGroups = new List<Node>();
            //功能组Distinct化
            for (int i = 0; i < rootRoleFuntions.Length; i++)
            {
                if (null == parentFunctionGroups.Find(new Predicate<Node>(delegate(Node findNode) { return (rootRoleFuntions[i].FunctionGroupID == findNode.NodeID); })))
                {
                    Node nodeItem = functionGroupLogic.GetNodeByID(rootRoleFuntions[i].FunctionGroupID);
                    if (nodeItem.IsStateOpen)//关闭的功能组则不显示
                        parentFunctionGroups.Add(nodeItem);
                }
            }
            //功能组排序化  按照功能组编码继续排序（由小到大）
            parentFunctionGroups.Sort(new Comparison<Node>(delegate(Node nodeA, Node nodeB) { return nodeA.NodeCode.CompareTo(nodeB.NodeCode); }));

            //1、构造父角色对应的功能组列表
            TreeNode root = new TreeNode();
            root.Value = "00";
            root.Text = functionGroupLogic.GetNodeByNodeCode(root.Value).NodeName;
            root.ImageUrl = ETMS.Utility.StaticResourceUtility.GetImageFullPath("default/functionSet.gif");
            root.Expanded = true;
            for (int i = 0; i < parentFunctionGroups.Count; i++)
            {

                TreeNode findNode = GetTreeNodeByValue(root, parentFunctionGroups[i].NodeCode);
                if (findNode == null)
                {
                    continue;
                }
                //载入根角色对应的功能

                /*
                 * 载入父角色在此功能组下分配的功能列表
                 */
                RoleFunction[] findFunctions = Array.FindAll<RoleFunction>(rootRoleFuntions, new Predicate<RoleFunction>(delegate(RoleFunction find)
                {
                    return (find.FunctionGroupID == parentFunctionGroups[i].NodeID);
                }));

                foreach (RoleFunction item in findFunctions)
                {
                    Function fun = new Function();
                    fun.FunctionID = item.FunctionID;
                    fun = (Function)functionGroupRelationManager.GetMemberByPkValue(fun);

                    if (fun.Status == 0)//关闭的功能则不显示
                        continue;

                    //如果功能设置了组件ID，并且组件ID是枚举ETMS.Product.ExtendComponentType成员
                    ETMS.Product.ExtendComponentType componentType;
                    if (!string.IsNullOrEmpty(fun.ComponentID) && Enum.TryParse<ETMS.Product.ExtendComponentType>(fun.ComponentID, true, out componentType))
                    {
                        //未启用的组件功能则跳过
                        if (!ETMS.Product.ProductComponentStrategy.IsSupport(componentType))
                        {
                            continue;
                        }
                    }

                    TreeNode functionTreeNode = new TreeNode();
                    functionTreeNode.Expanded = true;
                    findNode.ChildNodes.Add(functionTreeNode);

                    functionTreeNode.Text = fun.FunctionName;
                    functionTreeNode.Value = string.Format("fid:{0}:{1}", fun.FunctionGroupID, fun.FunctionID);
                    functionTreeNode.ImageUrl = ETMS.Utility.StaticResourceUtility.GetImageFullPath("default/function.gif");
                    RoleFunction findRoleFunction = Array.Find<RoleFunction>(rolefunctions, new Predicate<RoleFunction>(delegate(RoleFunction find) { return (find.FunctionID == fun.FunctionID); }));
                    if (findRoleFunction != null)
                    {
                        functionTreeNode.Value = string.Format("{0}:{1}", functionTreeNode.Value, findRoleFunction.RoleFunctionID);
                        SelectTreeNode(functionTreeNode);
                    }
                }
            }

            this.SortChildNode(root);


            //加入到成员列表中
            if (root.ChildNodes.Count > 0)
            {
                this.lsMember.Nodes.Add(root);
                this.btnSave.Enabled = true;
            }
            else
            {
                this.btnSave.Enabled = false;
            }
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

                    {
                        //如果一级功能组设置了组件ID，并且组件ID是枚举ETMS.Product.ExtendComponentType成员
                        ETMS.Product.ExtendComponentType menuComponentType;
                        if (!string.IsNullOrEmpty(group.ComponentID) && Enum.TryParse<ETMS.Product.ExtendComponentType>(group.ComponentID, true, out menuComponentType))
                        {
                            //未启用的组件功能则跳过
                            if (!ETMS.Product.ProductComponentStrategy.IsSupport(menuComponentType))
                            {
                                return null;
                            }
                        }
                    }

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
            treeNode.ChildNodes.Add(childTreeNode);
            doLoadTree(childTreeNode, node.ChildNodes[i]);
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
