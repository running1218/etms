using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Components.Exam.API.Entity.ItemBank;

public partial class QuestionDB_QuSingleSelection_QuestionList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        QuestionList1.QuestionType = QuestionType.SingleChoice;
        QuestionList1.CourseID = new Guid(Request.QueryString["CourseID"].ToString());
    }
}