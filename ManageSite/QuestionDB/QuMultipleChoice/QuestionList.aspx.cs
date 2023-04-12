using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Components.Exam.API.Entity.ItemBank;

public partial class QuestionDB_QuMultipleChoice_QuestionList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        QuestionList1.QuestionType = QuestionType.MultipleChoice;
        QuestionList1.CourseID = new Guid(Request.QueryString["CourseID"].ToString());
    }
}