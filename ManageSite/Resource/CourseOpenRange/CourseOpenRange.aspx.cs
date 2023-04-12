using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Components.Basic.API.Entity.Security;
using System.Collections;
using ETMS.Components.Basic.API.Entity.Common;
using ETMS.Components.Basic.Implement.BLL.Security;
using ETMS.Utility;
using ETMS.Components.Basic.Implement.BLL.Course;
using ETMS.Components.Basic.API.Entity.Course;
using System.Data;

public partial class Resource_CourseOpenRange_CourseOpenRange : System.Web.UI.Page
{
    private static OrganizationLogic organizationLogic = new OrganizationLogic();
    #region 页面参数
    /// <summary>
    /// 课程ID
    /// </summary>
    private Guid CourseID
    {
        get
        {
            if (ViewState["CourseID"] == null)
                ViewState["CourseID"] = Guid.Empty;
            return ViewState["CourseID"].ToGuid();
        }
        set
        {
            ViewState["CourseID"] = value;
        }
    }

    /// <summary>
    /// 课程所在的组织机构ID
    /// </summary>
    private int OrgID
    {
        get
        {
            if (ViewState["OrgID"] == null)
                ViewState["OrgID"] = 0;
            return ViewState["OrgID"].ToInt();
        }
        set
        {
            ViewState["OrgID"] = value;
        }
    }

    private DataTable TabOrg {
        get {
            if (ViewState["TabOrg"] == null)
                GetCourseOpenRange();
            return (DataTable)ViewState["TabOrg"];
        }
        set {
            ViewState["TabOrg"] = value;
        }
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (null != Request.QueryString["CourseID"])
            {
                CourseID = Request.QueryString["CourseID"].ToGuid();
                bind();
                GetCourseOpenRange();
                LoadMemberTree(sender, e);
            }
        }
        //Tree控件全选功能的客户端脚本支持
        //this.lsMember.Attributes.Add("onclick", "CheckEvent()");

    }

    /// <summary>
    /// 邦定信息
    /// </summary>
    private void bind()
    {
        Res_CourseLogic res_CourseLogic = new Res_CourseLogic();
        Res_Course course = res_CourseLogic.GetById(CourseID);
        lblCourseCode.Text = course.CourseCode;
        lblCourseName.Text = course.CourseName;
        OrgID = course.OrgID;
    }

    /// <summary>
    /// 获取已存在的机构
    /// </summary>
    private void GetCourseOpenRange() {        
        int orgCount = 0;
        CourseOpenRangeLogic courseOpenRanLogic = new CourseOpenRangeLogic();
        TabOrg = courseOpenRanLogic.GetList(CourseID, out orgCount);
    }

    #region 加载机构信息

    /// <summary>
    /// 加载机构信息
    /// </summary>
    protected void LoadMemberTree(object sender, EventArgs e)
    {
        this.lsMember.Nodes.Clear();
        //组织机构信息
        Node parentNode = new FunctionGroup();

        //修改了开放机构为当前机构的下级机构 2014-2-10 hjy
        Node node=new Organization();
        node.NodeID=ETMS.AppContext.UserContext.Current.OrganizationID;

        parentNode = organizationLogic.GetNodeTreeForManager(node, true);
        parentNode.NodeName =  organizationLogic.GetNodeByID(node.NodeID).NodeName;

        TreeNode rootTreeNode = new TreeNode();
        rootTreeNode.Expanded = true;
        rootTreeNode.ImageUrl = ETMS.Utility.StaticResourceUtility.GetImageFullPath("default/functionSet.gif");

        this.lsMember.Nodes.Add(rootTreeNode);
        doLoadTree(rootTreeNode, parentNode);
    }

    /// <summary>
    /// TreeNode 与业务 Node之间的挂接转化
    /// </summary>
    /// <param name="treeNode"></param>
    /// <param name="node"></param>
    private void doLoadTree(TreeNode treeNode, Node node)
    {
        treeNode.SelectAction = TreeNodeSelectAction.None;
        treeNode.Text = node.NodeName;
        treeNode.Value = node.NodeID.ToString();

        //选中课程所属机构 与 之前选过的机构
        if (node.NodeID == OrgID || IsCourseOrg(node.NodeID))
        {
            treeNode.Checked = true;
        }
        if (node.NodeID == OrgID)
        {
            treeNode.ShowCheckBox = false;
            treeNode.Text = "<input type='checkbox' name='checkOrgID' id='checkOrgID' checked='checked' disabled='disabled' /> "+node.NodeName;
        }

        for (int i = 0; i < node.ChildNodes.Count; i++)
        {
            TreeNode childTreeNode = new TreeNode();
            childTreeNode.Expanded = true;
            childTreeNode.ImageUrl = ETMS.Utility.StaticResourceUtility.GetImageFullPath("default/functionSet.gif");
            treeNode.ChildNodes.Add(childTreeNode);
            doLoadTree(childTreeNode, node.ChildNodes[i]);
        }
    }    
    #endregion

    /// <summary>
    /// 验证此机构是否存在
    /// </summary>
    /// <param name="orgID">机构ID</param>
    /// <returns></returns>
    private bool IsCourseOrg(int orgID) {
        bool res = false;
        if (TabOrg != null)
        {
            DataRow[] rows = TabOrg.Select("OrgID='"+orgID+"'" );
            if (rows.Length > 0)
            {
                res = true;
            }
        }
        return res;
    }

    #region 获取选中的机构
    private class CourseOpenRangeCommand
    {
        public Res_CourseOpenRange m_CourseOpenRange;
        public string m_Command;
    }
    System.Collections.Generic.List<CourseOpenRangeCommand> selectedCourseOpenRange = new System.Collections.Generic.List<CourseOpenRangeCommand>();
    private void LoadSelectedRole(TreeNode node)
    {
        foreach (TreeNode childNode in node.ChildNodes)
        {
            LoadSelectedRole(childNode);
        }

        if (!string.IsNullOrEmpty(node.Value) && node.Value != "0")
        {
            //如果节点选中 且不包含在之前保存的数据中 并且不等于当前课程所属机构 就添加
            if (node.Checked && !IsCourseOrg(node.Value.ToInt()) && node.Value.ToInt()!= OrgID)
            {
                Res_CourseOpenRange m_CourseOpenRange = new Res_CourseOpenRange();
                m_CourseOpenRange.OpenRangeID = Guid.NewGuid();
                m_CourseOpenRange.OrgID = node.Value.ToInt();
                m_CourseOpenRange.CourseID = CourseID;
                m_CourseOpenRange.CreateUser = ETMS.AppContext.UserContext.Current.RealName;
                m_CourseOpenRange.CreateUserID = ETMS.AppContext.UserContext.Current.UserID;

                CourseOpenRangeCommand openRangeCommand = new CourseOpenRangeCommand();
                openRangeCommand.m_CourseOpenRange = m_CourseOpenRange;
                openRangeCommand.m_Command = "add";
                selectedCourseOpenRange.Add(openRangeCommand); 
            }
            else if (!node.Checked && IsCourseOrg(node.Value.ToInt()))//如果没有选中 且包含在之前保存的数据中 就删除
            {
                Res_CourseOpenRange m_CourseOpenRange = new Res_CourseOpenRange();
                m_CourseOpenRange.OrgID = node.Value.ToInt();
                m_CourseOpenRange.CourseID = CourseID;

                CourseOpenRangeCommand openRangeCommand = new CourseOpenRangeCommand();
                openRangeCommand.m_CourseOpenRange = m_CourseOpenRange;
                openRangeCommand.m_Command = "del";
                selectedCourseOpenRange.Add(openRangeCommand);
            }
        }
    }
    #endregion

    /// <summary>
    /// 保存
    /// </summary>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        LoadSelectedRole(this.lsMember.Nodes[0]);
        CourseOpenRangeLogic courseOpenRangeLogic = new CourseOpenRangeLogic();
        foreach (CourseOpenRangeCommand courseOpenrangeCommand in selectedCourseOpenRange) {
            if (courseOpenrangeCommand.m_Command == "add") {
                courseOpenRangeLogic.Add(courseOpenrangeCommand.m_CourseOpenRange);
            }
            else if (courseOpenrangeCommand.m_Command == "del") {
                courseOpenRangeLogic.Remove(courseOpenrangeCommand.m_CourseOpenRange.CourseID, courseOpenrangeCommand.m_CourseOpenRange.OrgID);
            }
        }

        ETMS.Utility.JsUtility.SuccessMessageBoxAndCloseWindowAndTriggerRefreshEvent("课程开放机构保存成功！");
    }

}