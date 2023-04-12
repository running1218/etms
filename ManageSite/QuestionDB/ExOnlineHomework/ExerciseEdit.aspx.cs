using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class QuestionDB_ExOnlineHomework_ExerciseEdit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ExerciseInfo1.Action = ETMS.AppContext.OperationAction.Edit;
            ExerciseInfo1.OnlineJobID = new Guid(Request.QueryString["OnlineJobID"]);
        }
    }
}