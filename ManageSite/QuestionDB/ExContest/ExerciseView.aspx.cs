using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class QuestionDB_ExContest_ExerciseView : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ExerciseView1.ExerciseType = ETMS.Components.Basic.Implement.BLL.BizExerciseType.ExContest;
    }
}