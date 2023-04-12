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


    using ETMS.Components.Basic.API.Entity;
    using ETMS.Components.Basic.API.Entity.Common;
    using ETMS.Components.Basic.Implement.BLL.Common;
    using ETMS.Components.Basic.API.Entity.Security;
    using ETMS.Components.Basic.API.Entity.Security;
    using ETMS.Components.Basic.Implement.BLL.Security;
    public partial class DepartmentDropDownList : System.Web.UI.UserControl
    {
        private static DepartmentLogic departmentLogic = new DepartmentLogic();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.LoadManagerTree();
            }
        }
        /// <summary>
        /// 载入管理者层次列表
        /// </summary>
        private void LoadManagerTree()
        {
            Node[] rootDepartments = departmentLogic.GetRootDepartmentsByOrganizationID(ETMS.AppContext.UserContext.Current.OrganizationID);

            OrganizationLogic orgLogic = new OrganizationLogic();
            Node org = orgLogic.GetNodeByID(ETMS.AppContext.UserContext.Current.OrganizationID);
            Node root = new Department()
            {
                DepartmentName = org.NodeName,//公司名作为根
                State = 1,
            };
            string name = "|--" + root.NodeName;
            ListItem item = new ListItem(name, root.NodeID.ToString());
            this.DropDownList1.Items.Add(item);
            foreach (Node firstNode in rootDepartments)
            {
                doLoadTree(firstNode);
            }

        }

        /// <summary>
        /// TreeNode 与业务 Node之间的挂接转化
        /// </summary>
        /// <param name="treeNode"></param>
        /// <param name="node"></param>
        private void doLoadTree(Node node)
        {
            if (node.State == 0)
            {
                return;
            }
            string name = node.NodeName;
            string id = node.NodeID.ToString();
            name = "|--" + "".PadLeft(node.NodeCode.Length, '-') + name;
            ListItem item = new ListItem(name, id);
            this.DropDownList1.Items.Add(item);

            if (m_DepartmentID == node.NodeID)
            {
                item.Selected = true;
            }
            node = departmentLogic.GetNodeTreeForManager(node, false);
            for (int i = 0; i < node.ChildNodes.Count; i++)
            {
                doLoadTree(node.ChildNodes[i]);
            }
        }

        private int m_DepartmentID;
        public int DepartmentID
        {
            get
            {
                return int.Parse(this.DropDownList1.SelectedValue);
            }
            set
            {
                m_DepartmentID = value;
            }
        }
    }

}