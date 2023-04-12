using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Questionnaire_QuestionManage_QuSingleSelectionEdit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        QuSelectionEdit1.Action = ETMS.AppContext.OperationAction.Edit;
    }
}