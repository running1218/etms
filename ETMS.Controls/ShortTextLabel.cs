using System;

namespace ETMS.Controls
{
    public class ShortTextLabel : System.Web.UI.WebControls.Label
    {
        public int ShowTextNum
        {
            get
            {
                if (null == ViewState["ShowTextNum"])
                    return 10;
                else
                    return (int)ViewState["ShowTextNum"];
            }
            set
            {
                ViewState["ShowTextNum"] = value;
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            this.ToolTip = this.Text;
             this.Text = this.Text;//.ShortText(ShowTextNum);
        }
    }
}
