using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void lbnLogin_Click(object sender, EventArgs e)
    {
        ////判断验证码是否正确
        //if (!this.txtValidate.Text.Equals(ETMS.Utility.ValidCodeUtility.ValidateCode, StringComparison.InvariantCultureIgnoreCase))
        //{
        //    ETMS.Utility.JsUtility.AlertMessageBox("验证码错误！");
        //    return;
        //} 
    }
}