using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ETMS.Controls;
using ETMS.Utility;
using ETMS.Components.Point.Implement.BLL;

public partial class Point_StudentPointSearchDetail : ETMS.Controls.BasePage
{
    public static Point_Student_IssueDetailLogic isssueDetailLogic = new Point_Student_IssueDetailLogic();
    public static StudentCoursePointLogic studentCoursePointLogic = new StudentCoursePointLogic();
    public int StudentID
    {
        get { return Request.QueryString["StudentID"].ToInt(); }
    }
    public string BeginDateTime
    {
        get { return Request.QueryString["BeginDateTime"].ToString(); }
    }
    public string EndDateTime
    {
        get { return Request.QueryString["EndDateTime"].ToString(); }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        PageSet1.pageInit(this.GridViewList, PageDataSource);

        if (!Page.IsPostBack)
        {
            this.PageSet1.QueryChange();
            InititalControls();
        }
        aBack.HRef = "StudentPointSearch.aspx";
    }
    private void InititalControls()
    {
        try
        {
            int totalRecordCount = 0;
            string crieria = string.Format(" and a.StudentID={0}", StudentID);
            DataTable dt = isssueDetailLogic.StatStudentPointAllInfoListByOrgID(ETMS.AppContext.UserContext.Current.OrganizationID, 1, 1, "", crieria, out totalRecordCount);
            this.lblDepartment.FieldIDValue = dt.Rows[0]["DepartmentID"].ToString();
            this.lblRank.FieldIDValue = dt.Rows[0]["RankID"].ToString();
            this.lblPost.FieldIDValue = dt.Rows[0]["PostID"].ToString();
            this.lblRealName.Text = dt.Rows[0]["RealName"].ToString();
            this.lblDate.Text = BeginDateTime + "至" + EndDateTime;
            this.lblStartPoint.Text = isssueDetailLogic.StatStudentPointByBeforeDateTime(StudentID, BeginDateTime.ToDateTime()).ToString();
            int num = dt.Rows[0]["AccessPoints"].ToInt() - isssueDetailLogic.StatStudentExpensePointBetweenTwoDate(StudentID, BeginDateTime.ToDateTime(), EndDateTime.ToDateTime());
            this.lblEndPoint.Text = num.ToString();
        }
        catch  { }
    }
    private System.Collections.IList PageDataSource(int pageIndex, int pageSize, out int totalRecordCount)
    {
        string strQuery = string.Format(" and a.IssueTime>='{0}' and a.IssueTime<'{1}'", BeginDateTime.ToDate(), EndDateTime.ToDateTime().AddDays(1).ToDate());
        DataTable dt = isssueDetailLogic.GetStudentPointAllInfoListByStudentID(StudentID, pageIndex, pageSize, string.Empty, strQuery, out totalRecordCount);
        PageDataSourceProvider pageDataSource = new PageDataSourceProvider(dt, pageIndex, pageSize);
        return pageDataSource.PageDataSource;
    }
}