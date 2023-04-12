using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class QuestionDB_ExOnlineTest_ExerciseEdit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ExerciseInfo1.Action = ETMS.AppContext.OperationAction.Edit;
        ExerciseInfo1.OnlineTestID = new Guid(Request.QueryString["OnlineTestID"]);

    }
}