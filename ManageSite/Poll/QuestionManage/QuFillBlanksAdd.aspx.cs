using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Questionnaire_QuestionManage_QuFillBlanksAdd : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        QuFillBlanksEdit2.Action = ETMS.AppContext.OperationAction.Add;
        QuFillBlanksEdit2.ColumnID = int.Parse(Request.QueryString["ColumnID"]);
        QuFillBlanksEdit2.QuestionType = int.Parse(Request.QueryString["QuestionType"]);

    }
}