using System;

using System.Web.UI;
using System.Web.UI.WebControls;
namespace ETMS.Controls
{
    /// <summary>
    /// 一定范围内的复选框控制器
    /// 负责控制当前容器下所有复选框的状态
    /// 提供：全选/取消全选功能
    /// </summary>
    public class CheckBoxsController : CheckBox
    {      
        /// <summary>
        /// 是否反选
        /// </summary>
        public bool IsReverse
        {
            get
            {
                if (this.ViewState["IsReverse"] == null)
                {
                    this.ViewState["IsReverse"] = false;
                }
                return (bool)this.ViewState["IsReverse"];
            }
            set
            {
                this.ViewState["IsReverse"] = value;
            }
        }
        /// <summary>
        /// 待勾选的复选框容器ID
        /// </summary>
        public string ContainerID
        {
            get
            {
                object ret = this.ViewState["ContainerID"];
                if (ret != null)
                {
                    return (string)ret;
                }
                return string.Empty;
            }
            set
            {
                this.ViewState["ContainerID"] = value;
            }
        }

        private string ScriptKey
        {
            get
            {
                return "CheckBoxsControllerScript";
            }
        }
        private string Script
        {
            get
            {
                return @"
         <script language='javascript' type='text/javascript'>
    // JScript 文件
    function OnClickImpl(obj,container)
    {  
        if(obj.checked)
            SelectAll(container);
        else
           CancelSelectAll(container);         
    }
    //反选点击
    function OnClickImpl_Reverse(obj,container)
    {
        ReverseSelect(container);     
    }
    //反选
    function ReverseSelect(container)
    {
         var items=container.getElementsByTagName('input');
         for(var i=0;i<items.length;i++)
         {
            if(items[i].type=='checkbox' )
                items[i].checked=!items[i].checked;
         }
      
    }
    //全选
    function SelectAll(container)
    {    
         var items=container.getElementsByTagName('input');
         for(var i=0;i<items.length;i++)
         {
            if(items[i].type=='checkbox' && items[i].disabled!='disabled')
                items[i].checked=true;
         }
    }
    //取消
    function CancelSelectAll(container)
    {
         var items=container.getElementsByTagName('input');
         for(var i=0;i<items.length;i++)
         {
            if(items[i].type=='checkbox')
               items[i].checked=false;
         }
    }
</script>";
            }
        }
        protected override void OnPreRender(EventArgs e)
        {
            if (this.Checked)
                this.Checked = false;

            if (IsReverse)
            {
                this.Text = "反选";
            }
            else
            {
                this.Text = "全选";
            }
            if (!this.Page.ClientScript.IsClientScriptBlockRegistered(this.ScriptKey))
            {
                this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), this.ScriptKey, this.Script);
            }
            base.OnPreRender(e);
        }
        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            base.AddAttributesToRender(writer);
            if (!string.IsNullOrEmpty(this.ContainerID))
            {
                string script = null;
                Control con = FindControl(this.ContainerID);
                if (con != null)//同一控件树层次下找到ID为this.ContainerID的控件
                {
                    script = this.IsReverse ? string.Format("OnClickImpl_Reverse(this,document.getElementById('{0}'))", con.ClientID) : string.Format("OnClickImpl(this,document.getElementById('{0}'))", con.ClientID);
                }
                else//未找到，则把this.ContainerID作为HTML控件对待
                {
                    script = this.IsReverse ? string.Format("OnClickImpl_Reverse(this,document.getElementById('{0}'))", this.ContainerID) : string.Format("OnClickImpl(this,document.getElementById('{0}'))", this.ContainerID);
                }
                writer.AddAttribute("onclick", script);
                writer.AddAttribute("class","selectCheckbox");
            }

        }
    }
}
