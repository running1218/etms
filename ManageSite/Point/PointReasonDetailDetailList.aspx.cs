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
using ETMS.Components.Basic.Implement.BLL.TrainingItem;


public partial class Point_PointReasonDetailDetailList : ETMS.Controls.BasePage
{
    private static StudentCoursePointLogic studentCoursePointLogic = new StudentCoursePointLogic();
    private static Point_Student_PointReasonDetailLogic pointReasonDetailLogic = new Point_Student_PointReasonDetailLogic();
    private static Dic_PointReasonTypeLogic pointReasonTypeLogic = new Dic_PointReasonTypeLogic();
    /// <summary>
    /// 学员选课编码
    /// </summary>
    protected Guid StudentSignupID
    {
        get { return Request.QueryString["StudentSignupID"].ToGuid(); }
    }
    /// <summary>
    /// 项目编码
    /// </summary>
    protected Guid TrainingItemID
    {
        get { return Request.QueryString["TrainingItemID"].ToGuid(); }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        // 邦定控件
        bind();
        if (!IsPostBack)
        {          
            // 绑定列表
            GridViewBinding();
        }
        aBack.HRef = "PointReasonDetailList.aspx";
    }
    /// <summary>
    /// 邦定控件
    /// </summary>
    private void bind()
    {
        try
        {
            int total = 0;
            string crieria = string.Format(" and b.StudentSignupID='{0}'", StudentSignupID);
            DataTable dt = pointReasonDetailLogic.GetCanInputPointStudentListByTrainingItemID(TrainingItemID, 1, 1, string.Empty, crieria, out total);
            this.lblItemName.Text = dt.Rows[0]["ItemName"].ToString();
            this.lblClassName.Text = dt.Rows[0]["ClassName"].ToString();
            this.lblGroupName.Text = dt.Rows[0]["ClassSubgroupName"].ToString();
            this.lblRealName.Text = dt.Rows[0]["RealName"].ToString();
            this.lblOrgID.FieldIDValue = dt.Rows[0]["OrganizationID"].ToString();
            this.lblDepartment.FieldIDValue = dt.Rows[0]["DepartmentID"].ToString();
            this.lblSumPoint.Text = pointReasonDetailLogic.StatStudentInputPointByStudentSignupID(StudentSignupID).ToString();
        }
        catch { }
    }
    /// <summary>
    /// 绑定列表
    /// </summary>
    private void GridViewBinding()
    {
        int total = 0;
        DataTable dtlist = pointReasonDetailLogic.GetStudentInputPointAllInfoListByStudentSignupID(StudentSignupID, 1, int.MaxValue - 1, string.Empty, " and a.IsIssuePoint=0", out total);
        this.CustomGridView1.DataSource = dtlist.DefaultView;
        this.CustomGridView1.DataBind();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        // 绑定列表
        GridViewBinding();
    }
    protected void CustomGridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow && !CustomGridView1.IsEmpty)
        {
            DataRowView drv = e.Row.DataItem as DataRowView;
            LinkButton lblEdit = e.Row.FindControl("lblEdit") as LinkButton;
            LinkButton lblDel = e.Row.FindControl("lblDel") as LinkButton;
            if (drv["IsIssuePoint"].ToString() == "true")
            {
                lblEdit.Visible = false;
                lblDel.Visible = false;
            }
            else 
            {
                lblEdit.Attributes.Add("onclick", string.Format("javascript:showWindow('修改积分','{0}');javascript: return false;", this.ActionHref(string.Format("PointReasonDetailDetailListInfo.aspx?TrainingItemID={0}&StudentSignupID={1}&StudentPointReasonDetailID={2}", TrainingItemID, StudentSignupID, drv["StudentPointReasonDetailID"]))));
            }
        }
    }
    /// <summary>
    /// 行操作
    /// </summary>
    protected void CustomGridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            //编辑
            if (e.CommandName == "del")
            {

                pointReasonDetailLogic.doRemove(e.CommandArgument.ToGuid());                
                GridViewBinding();
                ETMS.Utility.JsUtility.SuccessMessageBox("删除成功！");
            }            
        }
        catch (ETMS.AppContext.BusinessException bizEx)
        {
            ETMS.WebApp.Manage.Extention.ShowScriptManagerMessage(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
            return;
        }
    }
}