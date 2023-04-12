using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Components.QS.Implement.BLL;
using System.Data;
using System.Web.Script.Serialization;

public partial class QS_QuestionSort : System.Web.UI.Page
{
    protected QS_QueryTitleLogic queryTitleBiz = new QS_QueryTitleLogic();

    public string QueryID
    {
        get { return Request.QueryString["queryid"]; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            selQuestionSort.Multiple = true;
            int totalRecordCount = 0;

            DataTable dt = queryTitleBiz.GetPagedList(1, 10000, " TitleNo asc", " and QueryID='" + QueryID + "'", out totalRecordCount);

            foreach (DataRow dr in dt.Rows)
            {
                selQuestionSort.Items.Add(new ListItem() { Value = dr["TitleID"].ToString(), Text = dr["TitleName"].ToString() });
            }
        }
      

    }
   
}