using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ETMS.Controls;
using ETMS.Utility;

using ETMS.Components.ExOfflineHomework.API.Entity;
using ETMS.Components.Basic.Implement;
using ETMS.Components.Basic.Implement.BLL.TrainingItem;
using ETMS.Components.Basic.API.Entity.TrainingItem;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Course;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.StudentCourse;


public partial class Grade_GradeManage_GradeViewList : ETMS.Controls.BasePage
{
    private static Sty_StudentOffLineJob studentJobRecord = new Sty_StudentOffLineJob();
    public static PublicFacade publicFaced = new PublicFacade();
    private static Tr_ItemLogic itemLogic = new Tr_ItemLogic();
    private static Tr_Item item = new Tr_Item();
    private static Tr_ItemCourseLogic itemCourseLogic = new Tr_ItemCourseLogic();

    /// <summary>
    /// 培训项目编号
    /// </summary>
    protected Guid TrainingItemID
    {
        get { return Request.QueryString["TrainingItemID"].ToGuid(); }
    }

    /// <summary>
    /// 课程编号
    /// </summary>
    protected Guid CourseID
    {
        get { return Request.QueryString["CourseID"].ToGuid(); }
    }

    /// <summary>
    /// 培训项目课程编号
    /// </summary>
    protected Guid TrainingItemCourseID
    {
        get { return Request.QueryString["TrainingItemCourseID"].ToGuid(); }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        PageSet1.pageInit(this.CustomGridView1, PageDataSource);

        if (!Page.IsPostBack)
        {
            this.PageSet1.QueryChange();
            Initial();
        }
        aBack.HRef = this.ActionHref(string.Format("GradeList.aspx?CourseID={0}&TrainingItemCourseID={1}&TrainingItemID={2}", CourseID, TrainingItemCourseID, TrainingItemID));
        //单机构版本隐藏“组织机构”列
        if (ETMS.Product.ProductDefine.IsSingleOrganizationVersion)
        {
            this.CustomGridView1.Columns[3].Visible = false;
        }
    }

    protected void Initial()
    {

        this.lblCourseName.Text = publicFaced.GetCourseNameByID(CourseID);
        item = itemLogic.GetById(TrainingItemID);
        this.lblItemName.Text = item.ItemName;
        this.lblItemCode.Text = item.ItemCode;
        int total = 0;
        DataTable dt = itemCourseLogic.GetGradeIssueList(1, 1, null, string.Format(" and Tr_ItemCourse.TrainingItemCourseID='{0}'", TrainingItemCourseID.ToString()), out total);
        this.lblCourseCode.Text = dt.Rows[0]["CourseCode"].ToString();
        this.lblCourseName.Text = dt.Rows[0]["CourseName"].ToString();
        //this.lblTrainingModel.FieldIDValue = dt.Rows[0]["TrainingModelID"].ToString();
        this.lblTeachModel.FieldIDValue = dt.Rows[0]["TeachModelID"].ToString();
        //this.ddlDepartment.FieldIDValue = dt.Rows[0]["DutyDeptID"].ToString();
        //this.ddlDepartment.Text = publicFaced.GetDeptNameByID((Int32)dt.Rows[0]["DutyDeptID"]);
        Sty_StudentCourseLogic studentcourseLogic = new Sty_StudentCourseLogic();
        this.lblStudentNum.Text = studentcourseLogic.GetItemCourseStudentNum(TrainingItemCourseID).ToString();


    }


    private System.Collections.IList PageDataSource(int pageIndex, int pageSize, out int totalRecordCount)
    {
        DataTable dt = itemCourseLogic.GetItemCourseStudentScoreList(TrainingItemCourseID, pageIndex, pageSize, string.Empty, string.Empty, out totalRecordCount);

        PageDataSourceProvider pageDataSource = new PageDataSourceProvider(dt, pageIndex, pageSize);

        return pageDataSource.PageDataSource;
    }
    protected void CustomGridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow && !this.CustomGridView1.IsEmpty)
        {
            DataRowView drv = e.Row.DataItem as DataRowView;
            LinkButton lblView = e.Row.FindControl("lblView") as LinkButton;
            string pathStr = this.ActionHref(string.Format("{0}/Grade/GradeManage/GradeView.aspx?TrainingItemCourseID={1}&CourseID={2}&TrainingItemID={3}&UserID={4}&StudentCourse={5}", WebUtility.AppPath, TrainingItemCourseID, CourseID, TrainingItemID, drv["UserID"].ToInt(), drv["StudentCourse"].ToGuid()));
            lblView.Attributes.Add("href", string.Format("javascript:showWindow('成绩查询','{0}','600','300')", pathStr));
        }
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        ////List<string> checkValues = CustomGridView1.AllCheckValues;
        int totalRecords = 0;
        ////this.GridViewList.Columns.RemoveAt(0);//移除全选
        this.CustomGridView2.DataSource = this.PageDataSource(1, 99999999, out totalRecords);
        this.CustomGridView2.DataBind();
        //ExportExcel(CustomGridView2);
        ETMS.Utility.FileDownLoadUtility.ExportToExcel("培训项目考试学员信息.xls", this.CustomGridView2);
    }

    /// <summary>
    /// GridView导出Excel特殊设置
    /// </summary>
    /// <param name="control"></param>
    public override void VerifyRenderingInServerForm(Control control)
    {
        if (!control.GetType().Equals(CustomGridView2.GetType()))
        {
            base.VerifyRenderingInServerForm(control);
        }
    }
}