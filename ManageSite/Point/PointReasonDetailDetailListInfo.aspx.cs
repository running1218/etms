using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ETMS.Utility;
using ETMS.Controls;
using ETMS.Components.Point.Implement.BLL;
using ETMS.Components.Point.API.Entity;


public partial class Point_PointReasonDetailDetailListInfo : System.Web.UI.Page
{
    private static StudentCoursePointLogic studentCoursePointLogic = new StudentCoursePointLogic();
    private static Point_Student_PointReasonDetailLogic pointReasonDetailLogic = new Point_Student_PointReasonDetailLogic();
    private static Dic_PointReasonTypeLogic pointReasonTypeLogic = new Dic_PointReasonTypeLogic();
    private static Point_Student_PointReasonRoleLogic pointReasonLogic = new Point_Student_PointReasonRoleLogic();
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
    /// <summary>
    /// 学生编码
    /// </summary>
    private int studentId=0;
    protected int StudentID
    {
        set { studentId = value; }
        get {return studentId;}
    }
    /// <summary>
    /// 学员学习过程获得积分编码
    /// </summary>
    protected Guid StudentPointReasonDetailID
    {
        get { return Request.QueryString["StudentPointReasonDetailID"].ToGuid(); }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bind();
        }
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

            //DataTable dt = studentCoursePointLogic.GetStudentCoursePointAllInfoListByTrainingItemID(TrainingItemID, 1, 1, string.Empty, crieria, out total);
            this.lblItemName.Text = dt.Rows[0]["ItemName"].ToString();
            this.lblClassName.Text = dt.Rows[0]["ClassName"].ToString();
            this.lblGroupName.Text = dt.Rows[0]["ClassSubgroupName"].ToString();
            this.lblRealName.Text = dt.Rows[0]["RealName"].ToString();
            this.lblOrgID.FieldIDValue = dt.Rows[0]["OrganizationID"].ToString();
            this.lblDepartment.FieldIDValue = dt.Rows[0]["DepartmentID"].ToString();
            this.lblWorkerNo.Text = dt.Rows[0]["WorkerNo"].ToString();
            studentId = dt.Rows[0]["UserID"].ToInt();

            //积分原因类型
            string whereStr = string.Format(" and OrgID={0} and IsUse=1", ETMS.AppContext.UserContext.Current.OrganizationID);
            total = 0;
            dt = pointReasonTypeLogic.GetPagedList(1, int.MaxValue - 1, " PointReasonTypeName", whereStr, out total);
            this.ddlPointReasonTypeID.DataSource = dt;
            this.ddlPointReasonTypeID.DataTextField = "PointReasonTypeName";
            this.ddlPointReasonTypeID.DataValueField = "PointReasonTypeID";
            this.ddlPointReasonTypeID.DataBind();
            this.ddlPointReasonTypeID.Items.Insert(0, new ListItem("请选择", ""));
            //积分原因
            this.ddlPointReason.Items.Insert(0, new ListItem("请选择", ""));            
           
            //修改
            if (StudentPointReasonDetailID != Guid.Empty)
            {
                Guid StudentPointReasonRoleID = pointReasonDetailLogic.GetById(StudentPointReasonDetailID).StudentPointReasonRoleID;
                Point_Student_PointReasonRoleLogic pointReasonRoleLogic = new Point_Student_PointReasonRoleLogic();
                this.ddlPointReasonTypeID.SelectedValue = pointReasonRoleLogic.GetById(StudentPointReasonRoleID).PointReasonTypeID.ToString();
                getReasonList();
                this.ddlPointReason.SelectedValue = StudentPointReasonRoleID.ToString();
                Point_Student_PointReasonDetail pointReasonDetail = pointReasonDetailLogic.GetById(StudentPointReasonDetailID);
                this.txtGivePoints.Text = pointReasonDetail.AccessPoints.ToString();
                this.txtRemark.Text = pointReasonDetail.Remark;
            }
        }
        catch { }
    }

    private void getReasonList()
    {
        if (!string.IsNullOrEmpty(ddlPointReasonTypeID.SelectedValue))
        {
            this.ddlPointReason.Items.Clear();
            string whereStr = string.Format(" and OrgID={0} and PointReasonTypeID={1} and IsUse=1", ETMS.AppContext.UserContext.Current.OrganizationID, this.ddlPointReasonTypeID.SelectedValue.ToInt());
            int total = 0;
            DataTable dt = pointReasonLogic.GetPagedList(1, int.MaxValue - 1, " PointReason", whereStr, out total);
            this.ddlPointReason.DataSource = dt.DefaultView;
            this.ddlPointReason.DataTextField = "PointReason";
            this.ddlPointReason.DataValueField = "StudentPointReasonRoleID";
            this.ddlPointReason.DataBind();
            this.ddlPointReason.Items.Insert(0, new ListItem("请选择", ""));
        }
        else
        {
            this.ddlPointReason.Items.Clear();
            this.txtGivePoints.Text = string.Empty;
            this.txtRemark.Text = string.Empty;
        }
    }

    protected void ddlPointReasonTypeID_SelectedIndexChanged(object sender, EventArgs e)
    {
         getReasonList();
    }

    protected void ddlPointReason_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.ddlPointReason.SelectedValue != string.Empty)
        {
            Point_Student_PointReasonRole list = pointReasonLogic.GetById(this.ddlPointReason.SelectedValue.ToGuid());
            this.txtGivePoints.Text = list.GivePoints.ToString();
            this.txtRemark.Text = list.Remark;
        }
        else
        {
            this.txtGivePoints.Text = string.Empty;
            this.txtRemark.Text = string.Empty;
        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            if (studentId == 0)
            {
                int total = 0;
                string crieria = string.Format(" and b.StudentSignupID='{0}'", StudentSignupID);
                DataTable dt = pointReasonDetailLogic.GetCanInputPointStudentListByTrainingItemID(TrainingItemID, 1, 1, string.Empty, crieria, out total);
                studentId = dt.Rows[0]["UserID"].ToInt();
            }  
             Point_Student_PointReasonDetail pointReasonDetail = new Point_Student_PointReasonDetail();
            if (StudentPointReasonDetailID == Guid.Empty)
            {
                //Point_Student_PointReasonDetail pointReasonDetail = new Point_Student_PointReasonDetail();
                pointReasonDetail.StudentPointReasonRoleID = this.ddlPointReason.SelectedValue.ToGuid();
                pointReasonDetail.StudentSignupID = StudentSignupID;
                pointReasonDetail.StudentID = studentId;
                pointReasonDetail.PointReason = this.ddlPointReason.SelectedItem.Text;
                pointReasonDetail.AccessPoints = this.txtGivePoints.Text.Trim().ToInt();
                pointReasonDetail.IsIssuePoint = false;
                pointReasonDetail.Remark = this.txtRemark.Text;
                pointReasonDetail.CreateTime = DateTime.Now;
                pointReasonDetail.CreateUser = ETMS.AppContext.UserContext.Current.RealName;
                pointReasonDetail.CreateUserID = ETMS.AppContext.UserContext.Current.UserID;
                pointReasonDetailLogic.Save(pointReasonDetail, ETMS.AppContext.OperationAction.Add);
            }
            else
            {
                pointReasonDetail = pointReasonDetailLogic.GetById(StudentPointReasonDetailID);
                pointReasonDetail.StudentPointReasonRoleID = this.ddlPointReason.SelectedValue.ToGuid();
                pointReasonDetail.StudentSignupID = StudentSignupID;
                pointReasonDetail.StudentID = studentId;
                pointReasonDetail.PointReason = this.ddlPointReason.SelectedItem.Text;
                pointReasonDetail.AccessPoints = this.txtGivePoints.Text.Trim().ToInt();
                pointReasonDetail.IsIssuePoint = false;
                pointReasonDetail.Remark = this.txtRemark.Text;
                pointReasonDetail.ModifyTime = DateTime.Now;
                pointReasonDetail.ModifyUser = ETMS.AppContext.UserContext.Current.RealName;
                pointReasonDetailLogic.Save(pointReasonDetail, ETMS.AppContext.OperationAction.Edit);
            }

            JsUtility.SuccessMessageBoxAndCloseWindowAndTriggerSearchEvent("保存成功");
            //ETMS.Utility.JsUtility.SuccessMessageBox("提示", "保存成功！", "function(){window.location = '" + this.ActionHref(string.Format("PointReasonDetailDetailList.aspx?TrainingItemID={0}&StudentSignupID={1}", TrainingItemID, StudentSignupID)) + "'}");
            //ETMS.WebApp.Manage.Extention.SuccessMessageBox("提示", "保存成功！", "function(){window.location = window.location;triggerParentSearchEvent();}");
        }
        catch (ETMS.AppContext.BusinessException bizEx)
        {
            ETMS.WebApp.Manage.Extention.ShowScriptManagerMessage(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
        }
    }
  
}