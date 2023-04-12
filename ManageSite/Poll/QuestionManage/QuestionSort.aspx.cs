using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ETMS.Components.Poll.Implement.BLL;

public partial class Poll_QuestionManage_QuestionSort : System.Web.UI.Page
{
    private static Poll_TitleLogic queryTitleBiz = new Poll_TitleLogic();
    private static Poll_ColumnLogic ColumnLogic = new Poll_ColumnLogic();
    public string QueryID
    {
        get { return Request.QueryString["queryid"]; }
    }
    public string ColumnID = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        int totalRecords = 0;
        ColumnID = ColumnLogic.GetEntityList(1, 1, "", string.Format(" AND QueryID={0}", QueryID), out totalRecords)[0].ColumnID.ToString();

        DataTable dt = queryTitleBiz.GetPagedList(1, 10000, " TitleNo asc", string.Format(" AND ColumnID={0}", ColumnID), out totalRecords);
        if (!Page.IsPostBack)
        {
            lbCourseSort.Multiple = true;

            foreach (DataRow dr in dt.Rows)
            {
                lbCourseSort.Items.Add(new ListItem() { Value = dr["TitleID"].ToString(), Text = dr["TitleName"].ToString() });
            }
        }


    }
}