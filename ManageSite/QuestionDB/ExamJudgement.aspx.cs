using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class QuestionDB_ExamJudgement : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ExamQuestionBank1.QuestionType = ETMS.Components.Exam.API.Entity.ItemBank.QuestionType.Judgement;
    }
}