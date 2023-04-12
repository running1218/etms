using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
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
using ETMS.Controls;
using ETMS.Components.Basic.Implement.BLL.Security;
using ETMS.Controls;
public partial class Admin_User_FunctionGroup_Default : ETMS.Controls.BasePage
{
    private static FunctionGroupLogic functionGroupLogic = new FunctionGroupLogic();
    private static RoleLogic roleLogic = new RoleLogic();

    private FunctionGroupFunctionRelationLogic functionGroupRelationManager = new FunctionGroupFunctionRelationLogic(null);
    private RoleFunctionRelationLogic roleFunctionRelationManager = new RoleFunctionRelationLogic(null);
    private UserFunctionRelationLogic userFunctionRelationManager = new UserFunctionRelationLogic(null);
    private UserRoleRelationLogic roleUserRelationLogic = new UserRoleRelationLogic();
    protected override RequestParameter[] PageRequestArgs
    {
        get
        {
            /*
            * 需要验证的页面参数包含：
            *    参数名  参数范围
            * 1、  id    {0,1,2..}
            * 2、  op    {add,edit,delete,view}
            */
            return new RequestParameter[]
            {
                 RequestParameter.CreateRangeRequestParameter("id",RequestParameter.NaturalInt32RangeVerify) 
            };
        }
    }

    public int UserID
    {
        get
        {
            return int.Parse(Request.QueryString["id"]);
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //Tree控件全选功能的客户端脚本支持 
        this.lsMember.Attributes.Add("onclick", "CheckEvent()");
        if (!Page.IsPostBack)
        {
            LoadMemberTree();
        }
    }

    #region 载入管理者Tree列表,成员Tree列表

    /// <summary>
    /// 载入被管理者（成员）层次列表
    /// </summary>
    protected void LoadMemberTree()
    {
        this.lsMember.Nodes.Clear();

        //根据角色与功能分配关系载入角色对应的功能组列表
        {
            //角色对应的功能列表
            RoleFunction[] rolefunctions = null;
            ArrayList sumRoleFunctions = new ArrayList();
            /* 
             * 角色功能分配规则
             *     功能继承
             */
            //当前父角色功能列表
            foreach (Role item in roleUserRelationLogic.Query(UserID))
            {
                Role parentRole = new Role();
                //当前子角色对应的功能列表
                parentRole.RoleID = item.RoleID;
                roleFunctionRelationManager.Manager = parentRole;
                sumRoleFunctions.AddRange((RoleFunction[])roleFunctionRelationManager.GetAllMembers(""));
            }
            rolefunctions = (RoleFunction[])sumRoleFunctions.ToArray(typeof(RoleFunction));

            //当前用户对应的功能列表
            User settingUser = new User();
            settingUser.UserID = UserID;
            userFunctionRelationManager.Manager = settingUser;
            UserFunction[] childRolefunctions = (UserFunction[])userFunctionRelationManager.GetAllMembers("");

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


                    //如果基于角色权限（即用户权限为空）
                    if (childRolefunctions.Length == 0)
                    {
                        /*
                         * 如果存在角色功能权限，则选择
                         */
                        if (Array.Exists<RoleFunction>(rolefunctions, new Predicate<RoleFunction>(delegate(RoleFunction find)
                        {
                            return (find.FunctionID == item.FunctionID);
                        })))
                        {
                            SelectTreeNode(functionTreeNode);
                        }
                    }
                    else//如果基于用户权限（即用户权限非空）
                    {
                        /*
                         * 如果存在角色功能权限，则选择
                         */
                        UserFunction userFunction = Array.Find<UserFunction>(childRolefunctions, new Predicate<UserFunction>(delegate(UserFunction find)
                         {
                             return (find.FunctionID == item.FunctionID);
                         }));
                        if (userFunction != null)
                        {
                            functionTreeNode.Value = string.Format("{0}:{1}", functionTreeNode.Value, userFunction.UserFunctionID);
                            SelectTreeNode(functionTreeNode);
                        }
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

            ////如果当前的用户角色
            //if (this.RoleTree1.CurrentRoleID == WebSession.Administrator.MapRole.RoleID && !WebSession.Administrator.MapRole.IsSysAdminRole)
            //{
            //    this.btnSave.Enabled = false;
            //    this.btnSave.ToolTip = "当前角色自身不能修改，须由父角色修改！";
            //}

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

    #endregion
    private class UserFunctionCommand
    {
        public UserFunction m_RoleFunction;
        public string m_Command;
    }
    List<UserFunctionCommand> selectedRoles = new List<UserFunctionCommand>();
    private void LoadSelectedRole(TreeNode node)
    {
        foreach (TreeNode childNode in node.ChildNodes)
        {
            LoadSelectedRole(childNode);
        }
        if (node.Value.StartsWith("fid"))
        {
            string[] parms = node.Value.Split(':');
            UserFunction userFunction = new UserFunction();
            userFunction.FunctionGroupID = parms[1].ToInt();
            userFunction.FunctionID = parms[2].ToInt();
            userFunction.CreateTime = DateTime.Now;
            userFunction.Creator = ETMS.AppContext.UserContext.Current.RealName;
            userFunction.UserFunctionID = (parms.Length == 4) ? parms[3].ToInt() : 0;
            userFunction.UserID = ETMS.AppContext.UserContext.Current.UserID;
            UserFunctionCommand entity = new UserFunctionCommand();
            entity.m_RoleFunction = userFunction;
            if (node.Checked && userFunction.UserFunctionID == 0)
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
        User settingUser = new User();
        settingUser.UserID = UserID;

        userFunctionRelationManager.Manager = settingUser;
        foreach (UserFunctionCommand item in selectedRoles)
        {
            //1、case：UserFunctionCommand.m_Command=="add"
            if (item.m_Command == "add")
                userFunctionRelationManager.Associate(item.m_RoleFunction);
            //1、case：UserFunctionCommand.m_Command=="delete"
            if (item.m_Command == "delete")
                userFunctionRelationManager.ReleaseAssociate(item.m_RoleFunction);
        }

        //重新载入成员列表
        LoadMemberTree();

        //清除相关缓存 
        string key = "UserPageUrls_" + UserID.ToString();
        ETMS.Utility.BizCache.BizCacheHelper.RemoveCacheItem(key);

        ETMS.Utility.JsUtility.SuccessMessageBox("用户权限设置完成！");
    }
}
