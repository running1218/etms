using System;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace ETMS.Controls
{
    /// <summary>
    /// �ı�����
    /// </summary>
    public enum TextBoxType
    {
        /// <summary>
        /// Ĭ�ϣ�֧�ָ��������ַ�����
        /// </summary>
        Default,
        /// <summary>
        /// ��֧����������
        /// </summary>
        Number,
        /// <summary>
        /// ��֧�ָ�����
        /// </summary>
        Decimal,
    }
    public class CustomTextBox : TextBox
    {
        #region ��������
        /// <summary>
        /// �������������
        /// </summary>
        public TextBoxType ContentType
        {
            get
            {
                if (ViewState["ContentType"] == null)
                {
                    ViewState["ContentType"] = TextBoxType.Default;
                }
                return (TextBoxType)ViewState["ContentType"];
            }
            set
            {
                ViewState["ContentType"] = value;
            }
        }
        public string ButtonControlId
        {
            get
            {
                return (string)ViewState["ButtonControlId"];
            }
            set
            {
                ViewState["ButtonControlId"] = value;
            }
        }
        #endregion
        #region ���ظ��෽��
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            if (!string.IsNullOrEmpty(this.ButtonControlId))
            {
                Control btnControl = this.FindControl(this.ButtonControlId);
                if (btnControl == null)
                {
                    throw new Exception(string.Format("��ǰ������û���ҵ���Ϊ{0}��Button�ؼ���", this.ButtonControlId));
                }
                this.Attributes.Add("onkeypress", string.Format("if (window.event.keyCode==13)document.getElementById('{0}').click();", btnControl.ClientID));
            }
            switch (this.ContentType)
            {
                case TextBoxType.Number:
                    this.Attributes.Add("onkeyup", "value=value.replace(^-?\\d+$,'')");
                    this.Attributes.Add("onbeforepaste", "clipboardData.setData('text',clipboardData.getData('text').replace(^-?\\d+$,''))");
                    break;
                case TextBoxType.Decimal:
                    this.Attributes.Add("t_value", this.Text);
                    this.Attributes.Add("onkeyup", "if(null==this.value.match(/^[0-9]+(.[0-9]{0,2})?$/) && this.value!='')this.value=this.t_value;else this.t_value=this.value; ");
                    this.Attributes.Add("onchange", "if(null==this.value.match(/^[0-9]+(.[0-9]{0,2})?$/) && this.value!='')this.value=this.t_value;else this.t_value=this.value; ");
                    this.Attributes.Add("onbeforepaste", "if(null==this.value.match(/^[0-9]+(.[0-9]{0,2})?$/) && this.value!='')this.value=this.t_value;else this.t_value=this.value; ");
                    this.Attributes.Add("onmouseout", "if(null==this.value.match(/^[0-9]+(.[0-9]{0,2})?$/) && this.value!='')this.value=this.t_value;else this.t_value=this.value; ");
                    break;
                default://default
                    break;
            }
        }
        #endregion
    }
}
