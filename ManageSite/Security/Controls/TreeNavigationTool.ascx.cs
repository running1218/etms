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
    using ETMS.Components.Basic.Implement.BLL.Security;
    using ETMS.Components.Basic.API.Entity.Security;
    using ETMS.Utility;

    public partial class Tree_NavigationTool : System.Web.UI.UserControl
    {
        protected string NavigationPath = "";

        /// <summary>
        /// NodeID在URL参数定义名称
        /// </summary>
        public string QueryParmName
        {
            get
            {
                return ViewState["QueryParmName"].ToString();
            }
            set
            {
                ViewState["QueryParmName"] = value;
            }
        }
        /// <summary>
        /// 根业务描述名称
        /// </summary>
        public string BussinessTitle
        {
            get
            {
                return ViewState["BussinessTitle"].ToString();
            }
            set
            {
                ViewState["BussinessTitle"] = value;
            }
        }
        /// <summary>
        /// Tree层次调用URL格式
        /// </summary>
        public string BussinessUrlFormat
        {
            get
            {
                return ViewState["BussinessUrlFormat"].ToString();
            }
            set
            {
                ViewState["BussinessUrlFormat"] = value;
            }
        }

        private string m_ParentUrl;
        /// <summary>
        /// 上级Url
        /// </summary>
        public string ParentUrl
        {
            get
            {
                return m_ParentUrl;
            }
        }
        private string m_CurrrentUrl;
        /// <summary>
        /// 当前级别Url
        /// </summary>
        public string CurrrentUrl
        {
            get
            {
                return m_CurrrentUrl;
            }
        }
        private NodeLogic m_NodeLogic;
        /// <summary>
        /// 节点逻辑操作实现类
        /// </summary>
        public NodeLogic NodeLogicImpl
        {
            get
            {
                return m_NodeLogic;
            }
            set
            {
                m_NodeLogic = value;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (this.m_NodeLogic == null)
            {
                throw new ArgumentNullException("NodeLogicImpl");
            }

            int nodeID = 0;
            try
            {
                nodeID = Request.ToparamValue<int>(QueryParmName);
            }
            catch { }

            if (nodeID == 0)
            {
                NavigationPath = BussinessTitle;
                m_ParentUrl = string.Format(this.BussinessUrlFormat, 0);
                m_CurrrentUrl = m_ParentUrl;
                return;
            }
            System.Collections.Stack stack = new Stack();
            Node parent = this.NodeLogicImpl.GetNodeByID(nodeID);
            m_CurrrentUrl = string.Format(this.BussinessUrlFormat, parent.NodeID);
            stack.Push(string.Format("<a href='{0}'>{1}</a>", string.Format(this.BussinessUrlFormat, parent.NodeID), parent.NodeName));
            //stack.Push(string.Format("{0}", parent.NodeName));
            while (parent.ParentNodeID != 0)
            {
                parent = this.NodeLogicImpl.GetNodeByID(parent.ParentNodeID);
                stack.Push(string.Format("<a href='{0}'>{1}</a>&nbsp;&gt;&gt;&nbsp;", string.Format(this.BussinessUrlFormat, parent.NodeID), parent.NodeName));

                if (m_ParentUrl == null)
                {
                    m_ParentUrl = string.Format(this.BussinessUrlFormat, parent.NodeID);
                }

            }
            stack.Push(string.Format("<a href='{0}'>{1}</a>&nbsp;&gt;&gt;&nbsp;", string.Format(this.BussinessUrlFormat, 0), BussinessTitle));
            if (m_ParentUrl == null)
            {
                m_ParentUrl = string.Format(this.BussinessUrlFormat, 0);
            }
            System.Text.StringBuilder writer = new System.Text.StringBuilder();
            while (stack.Count > 0)
            {
                writer.Append(stack.Pop().ToString());
            }
            this.NavigationPath = writer.ToString();

        }
    }
}