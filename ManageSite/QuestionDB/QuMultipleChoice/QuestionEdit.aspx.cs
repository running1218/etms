using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class QuestionDB_QuMultipleChoice_QuestionEdit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        QuestionInfo1.Action = ETMS.AppContext.OperationAction.Edit;
        QuestionInfo1.QuestionBankID = new Guid(Request.QueryString["QuestionBankID"].ToString());
        QuestionInfo1.QuestionID = new Guid(Request.QueryString["QuestionID"].ToString());
    }
}