using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Utility;
public partial class QuestionDB_ExContest_ExerciseAdd : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ExerciseInfo1.Action = ETMS.AppContext.OperationAction.Add;
            ExerciseInfo1.CourseID = Request.ToparamValue<Guid>("CourseID");
        }
    }
}