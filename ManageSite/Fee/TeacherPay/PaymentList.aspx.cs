using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Fee_TeacherPay_PaymentList : System.Web.UI.Page
{
    #region 页面参数

    /// <summary>
    /// 可见
    /// </summary>
    public bool isVisible1
    {
        get
        {
            if (ViewState["isVisible1"] == null)
            {
                ViewState["isVisible1"] = true;
            }
            return (bool)ViewState["isVisible1"];
        }
        set
        {
            ViewState["isVisible1"] = value;
        }
    }

    /// <summary>
    /// 可见
    /// </summary>
    public bool isVisible2
    {
        get
        {
            if (ViewState["isVisible2"] == null)
            {
                ViewState["isVisible2"] = false;
            }
            return (bool)ViewState["isVisible2"];
        }
        set
        {
            ViewState["isVisible2"] = value;
        }

    }

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {       
        
        
    }



}