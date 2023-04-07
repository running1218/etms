using System;
using System.Web.UI.WebControls;
namespace ETMS.Controls
{
    /// <summary>
    /// 自定义按钮，支持自定的Confirm功能
    /// </summary>
    public class CustomButton : Button
    {
        /// <summary>
        /// 启用按钮Confirm功能
        /// </summary>
        public bool EnableConfirm
        {
            get
            {
                if (ViewState["EnableConfirm"] == null)
                {
                    ViewState["EnableConfirm"] = false;
                }
                return (bool)ViewState["EnableConfirm"];
            }
            set
            {
                ViewState["EnableConfirm"] = value;
            }
        }
        /// <summary>
        /// 提示标题
        /// </summary>
        public string ConfirmTitle
        {
            get
            {
                return (string)ViewState["ConfirmTitle"];
            }
            set
            {
                ViewState["ConfirmTitle"] = value;
            }
        }
        /// <summary>
        /// 提示消息
        /// </summary>
        public string ConfirmMessage
        {
            get
            {
                return (string)ViewState["ConfirmMessage"];
            }
            set
            {
                ViewState["ConfirmMessage"] = value;
            }
        }


        protected override void OnPreRender(EventArgs e)
        {
            if (EnableConfirm)
            {
                if (string.IsNullOrEmpty(this.ConfirmTitle))
                {
                    this.OnClientClick = ETMS.Utility.JsUtility.GetControlClientConfirmScriptCode(this.ConfirmMessage);
                }
                else
                {
                    this.OnClientClick = ETMS.Utility.JsUtility.GetControlClientConfirmScriptCode(this.ConfirmTitle, this.ConfirmMessage);
                }
            }

            base.OnPreRender(e);
        }
    }
}
