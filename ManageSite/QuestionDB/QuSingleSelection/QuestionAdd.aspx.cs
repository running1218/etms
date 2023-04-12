using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class QuestionDB_QuSingleSelection_QuestionAdd : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        QuestionInfo1.Action = ETMS.AppContext.OperationAction.Add;
        QuestionInfo1.QuestionBankID = new Guid(Request.QueryString["QuestionBankID"].ToString());
        
    }
}