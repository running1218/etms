using System;

using System.Web.UI;
using System.Web.UI.WebControls;
namespace ETMS.Controls
{
    /// <summary>
    /// һ����Χ�ڵĸ�ѡ�������
    /// ������Ƶ�ǰ���������и�ѡ���״̬
    /// �ṩ��ȫѡ/ȡ��ȫѡ����
    /// </summary>
    public class CheckBoxsController : CheckBox
    {      
        /// <summary>
        /// �Ƿ�ѡ
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
        /// ����ѡ�ĸ�ѡ������ID
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
    // JScript �ļ�
    function OnClickImpl(obj,container)
    {  
        if(obj.checked)
            SelectAll(container);
        else
           CancelSelectAll(container);         
    }
    //��ѡ���
    function OnClickImpl_Reverse(obj,container)
    {
        ReverseSelect(container);     
    }
    //��ѡ
    function ReverseSelect(container)
    {
         var items=container.getElementsByTagName('input');
         for(var i=0;i<items.length;i++)
         {
            if(items[i].type=='checkbox' )
                items[i].checked=!items[i].checked;
         }
      
    }
    //ȫѡ
    function SelectAll(container)
    {    
         var items=container.getElementsByTagName('input');
         for(var i=0;i<items.length;i++)
         {
            if(items[i].type=='checkbox' && items[i].disabled!='disabled')
                items[i].checked=true;
         }
    }
    //ȡ��
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
                this.Text = "��ѡ";
            }
            else
            {
                this.Text = "ȫѡ";
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
                if (con != null)//ͬһ�ؼ���������ҵ�IDΪthis.ContainerID�Ŀؼ�
                {
                    script = this.IsReverse ? string.Format("OnClickImpl_Reverse(this,document.getElementById('{0}'))", con.ClientID) : string.Format("OnClickImpl(this,document.getElementById('{0}'))", con.ClientID);
                }
                else//δ�ҵ������this.ContainerID��ΪHTML�ؼ��Դ�
                {
                    script = this.IsReverse ? string.Format("OnClickImpl_Reverse(this,document.getElementById('{0}'))", this.ContainerID) : string.Format("OnClickImpl(this,document.getElementById('{0}'))", this.ContainerID);
                }
                writer.AddAttribute("onclick", script);
                writer.AddAttribute("class","selectCheckbox");
            }

        }
    }
}
