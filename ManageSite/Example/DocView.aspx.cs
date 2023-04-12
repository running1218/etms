using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Example_DocView : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(Request.QueryString["file"]))
        {
            //this.DocViewer1.DocURL = "1.txt.swf";

            //this.DocViewer1.DocURL = "2.docx.swf";

            //this.DocViewer1.DocURL = "3.pptx.swf";

            //this.DocViewer1.DocURL = "4.xls.swf";

            this.DocViewer1.DocURL = "http://10.96.33.232:8010/files/DisScorm/5.pdf.swf";
        }
        else
        {
            this.DocViewer1.DocURL = ETMS.Utility.StaticResourceUtility.GetFullPathByFileType("DocResource", Request.QueryString["file"]) + ".swf";
        }
    }
}