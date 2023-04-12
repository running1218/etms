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
    public partial class FunctionGroupDropDownList : System.Web.UI.UserControl
    {
        private static FunctionGroupLogic functionGroupLogic = new FunctionGroupLogic();
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
            Node rootFunctionGroup = functionGroupLogic.GetNodeTreeForManager(new FunctionGroup(), true);
            rootFunctionGroup.NodeName = "根功能组";
            doLoadTree(rootFunctionGroup);
        }

        /// <summary>
        /// TreeNode 与业务 Node之间的挂接转化
        /// </summary>
        /// <param name="treeNode"></param>
        /// <param name="node"></param>
        private void doLoadTree(Node node)
        {
            string name = node.NodeName;
            string id = node.NodeID.ToString();
            name = "|--" + "".PadLeft(2 * (node.NodeCode.Length > 0 ? (node.NodeCode.Length / 3 + 1) : 0), '-') + name;
            ListItem item = new ListItem(name, id);
            this.DropDownList1.Items.Add(item);

            if (m_FunctonGroupID == node.NodeID)
            {
                item.Selected = true;
            }
            for (int i = 0; i < node.ChildNodes.Count; i++)
            {
                doLoadTree(node.ChildNodes[i]);
            }
        }

        private int m_FunctonGroupID;
        public int FunctonGroupID
        {
            get
            {
                return int.Parse(this.DropDownList1.SelectedValue);
            }
            set
            {
                m_FunctonGroupID = value;
            }
        }
    }

}