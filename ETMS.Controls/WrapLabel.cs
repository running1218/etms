using System;

namespace ETMS.Controls
{
    public class WrapLabel : System.Web.UI.WebControls.Label
    {
        protected override void OnPreRender(EventArgs e)
        {
            this.CssClass = "wrap_lable";
            this.Text = this.Text.Replace("\r\n", " <br/> ");
        }
    }
}
