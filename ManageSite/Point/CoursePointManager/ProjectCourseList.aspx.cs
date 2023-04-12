using System;
using System.Collections;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.StudentCourse;
using ETMS.Components.Point.Implement.BLL;
using ETMS.Controls;
using ETMS.Utility;
using System.Collections.Generic;
using ETMS.Components.ExOnlineTest.Implement.BLL;
public partial class Point_CoursePointManager_ProjectCourseList : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        PageSet1.pageInit(this.CustomGridView1, PageDataSource);
        if (!Page.IsPostBack)
        {
            bind();
            this.PageSet1.QueryChange();
        }
    }

    /// <summary>
    /// 邦定项目信息
    /// </summary>
    private void bind()
    {
        StudentCoursePointLogic pointLogic = new StudentCoursePointLogic();
        ddl_ItemName.DataSource = pointLogic.GetCanComputeCoursePointTrainingItemListByOrgID(ETMS.AppContext.UserContext.Current.OrganizationID);
        ddl_ItemName.DataTextField = "ItemName";
        ddl_ItemName.DataValueField = "TrainingItemID";
        ddl_ItemName.DataBind();
        ddl_ItemName.Items.Insert(0, new ListItem("全部", ""));
    }

    private System.Collections.IList PageDataSource(int pageIndex, int pageSize, out int totalRecordCount)
    {
        //当前组织机构下的数据
        string criteria = string.Format(" AND t.OrgID={0}", ETMS.AppContext.UserContext.Current.OrganizationID);
        //按项目名称排序
        string sortExpression = "t.TrainingItemID";
        StudentCoursePointLogic pointLogic = new StudentCoursePointLogic();
        DataTable dt = null;
        if (ddl_ItemName.SelectedValue != "")
        {
            criteria += string.Format(" And t.TrainingItemID='{0}'", ddl_ItemName.SelectedValue);
        }
        if (txt_CourseName.Text != "")
        {
            criteria += string.Format(" And c.CourseName like '%{0}%'", txt_CourseName.Text);
        }
        dt = pointLogic.GetCanComputePointCourseList(pageIndex, pageSize, sortExpression, criteria, out totalRecordCount);

        PageDataSourceProvider pageDataSource = new PageDataSourceProvider(dt, pageIndex, pageSize);
        return pageDataSource.PageDataSource;
    }

    /// <summary>
    /// 查询
    /// </summary>
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        this.PageSet1.QueryChange();
    }

    /// <summary>
    /// 行绑定
    /// </summary>
    protected void CustomGridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Guid TrainingItemCourseID = CustomGridView1.DataKeys[e.Row.RowIndex].Value.ToGuid();
            //已报名学员数
            Label lbl_SignUpStudentTotal = (Label)e.Row.FindControl("lbl_SignUpStudentTotal");
            if (lbl_SignUpStudentTotal != null)
            {
                lbl_SignUpStudentTotal.Text = new Sty_StudentCourseLogic().GetItemCourseStudentNum(TrainingItemCourseID).ToString();
            }

            //在线测试数
            Label lblOnlineTest = (Label)e.Row.FindControl("lblOnlineTest");
            if(lblOnlineTest!=null)
            {
                lblOnlineTest.Text = new Ex_OnLineTestLogic().GetItemCourseOnlineTestTotal(TrainingItemCourseID).ToString();
            }

            //已发布
            Label lblPublished = (Label)e.Row.FindControl("lblPublished");
            if (lblPublished != null)
            {
                lblPublished.Text = new StudentCoursePointLogic().GetIssuePointsStudentNumByTrainingItemCourseID(TrainingItemCourseID).ToString();
            }

            //可发布
            HyperLink lblUnpublished = (HyperLink)e.Row.FindControl("lblUnpublished");
            if (lblUnpublished != null)
            {
                lblUnpublished.Text = new StudentCoursePointLogic().GetCanIssuePointsStudentNumByTrainingItemCourseID(TrainingItemCourseID).ToString();
            }

           
            //未计算
            Label lblNotCalculated = (Label)e.Row.FindControl("lblNotCalculated");
            if (lblNotCalculated != null)
            {
                lblNotCalculated.Text = new StudentCoursePointLogic().GetNoPointsStudentNumByTrainingItemCourseID(TrainingItemCourseID).ToString();
            }
            //系统计算积分
            LinkButton lblSystemCalculatePoint = (LinkButton)e.Row.FindControl("lblSystemCalculatePoint");
            lblSystemCalculatePoint = lblSystemCalculatePoint == null ? new LinkButton() : lblSystemCalculatePoint;

            lblSystemCalculatePoint.Enabled = true;
            lblSystemCalculatePoint.PostBackUrl = this.ActionHref(string.Format("PointCalculate.aspx?TrainingItemCourseID={0}", TrainingItemCourseID));

            //手动设置积分    
            LinkButton lblManualSetupPoint = (LinkButton)e.Row.FindControl("lblManualSetupPoint");
            lblManualSetupPoint = lblManualSetupPoint == null ? new LinkButton() : lblManualSetupPoint;

            lblManualSetupPoint.Enabled = true;
            lblManualSetupPoint.PostBackUrl = this.ActionHref(string.Format("ManualSetupPoint.aspx?TrainingItemCourseID={0}", TrainingItemCourseID));

        }
    }
}