using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Default2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ETMS.Utility.Logging.ErrorLogHelper.Error(new Exception("dddd"));
        return;
        new ETMS.Components.Basic.Implement.BLL.BizLogTest().Add(new ETMS.Components.Basic.Implement.BLL.BizTestItem() { ID = Guid.Empty, Name = "testsetsetset" });

        if (!IsPostBack)
        {
            dynamic a = ETMS.WebApp.ReadExcel.GetData("tb_ClassRoom");
        }
    }
    protected void btn1_Click(object sender, EventArgs e)
    {
        ETMS.Utility.JsUtility.SuccessMessageBox("sdfsafdsadfsaf", "sdsssssssss", "function(){location.href=location.href;}");
        Response.Write("asadsfasf");
    }
}