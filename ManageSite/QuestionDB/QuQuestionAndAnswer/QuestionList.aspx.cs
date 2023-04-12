using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Components.Exam.API.Entity.ItemBank;
using ETMS.Utility;

public partial class QuestionDB_QuQuestionAndAnswer_QuestionList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        QuestionList1.QuestionType = QuestionType.ExtendedText;
        QuestionList1.CourseID = Request.QueryString["CourseID"].ToGuid();
    }
}