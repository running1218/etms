using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Utility;
using ETMS.Controls;
using System.Collections;
using ETMS.Components.Basic.Implement.BLL.TrainingItem;
using ETMS.Components.StudyClass.Implement.BLL.StudyClass;
using ETMS.Components.StudyClass.API.Entity.StudyClass;
using System.Data;
using ETMS.Components.Point.Implement.BLL;
using ETMS.Components.Point.API.Entity;

public partial class Point_PointReasonDetailListAdd : ETMS.Controls.BasePage
{
    private static Dic_PointReasonTypeLogic pointReasonTypeLogic = new Dic_PointReasonTypeLogic();
    private static Point_Student_PointReasonRoleLogic pointReasonLogic = new Point_Student_PointReasonRoleLogic();
    public static Point_Student_PointReasonDetailLogic pointReasonDetailLogic = new Point_Student_PointReasonDetailLogic();
    private ArrayList arrayList=new ArrayList();
    /// <summary>
    /// 所选择的学员选课表编码
    /// </summary>
    public string StudentSignupIDs
    {
        get { return Request.QueryString["StudentSignupID"].ToString(); }
    }
    /// <summary>
    /// 班级ID
    /// </summary>
    public Guid ClassID
    {
        get { return Request.QueryString["ClassID"].ToGuid(); }
    }
    /// <summary>
    /// 群组ID
    /// </summary>
    public Guid ClassSubgroupID
    {
        get { return Request.QueryString["ClassSubgroupID"].ToGuid(); }
    }
    /// <summary>
    /// 项目ID
    /// </summary>
    public Guid TrainingItemID
    {
        get { return Request.QueryString["TrainingItemID"].ToGuid(); }
    }
    /// <summary>
    /// 解析传出来的值
    /// </summary>
    private void AnalyseStudentcourse()
    {
        string[] StudentCourseList = StudentSignupIDs.Split(',');
        for (int i = 0; i < StudentCourseList.Length; i++)
        {
            arrayList.Add(StudentCourseList[i]);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bingding();
        }
    }
    /// <summary>
    /// 控件binding
    /// </summary>
    private void bingding()
    {
        try
        {
            // 解析传出来的值
            AnalyseStudentcourse();
            //项目
            Tr_ItemLogic itemLogic = new Tr_ItemLogic();
            this.lblItemName.Text = itemLogic.GetById(TrainingItemID).ItemName;
            //班级
            if (ClassID == Guid.Empty)
            {
                this.lblClassName.Text = "全部";
            }
            else
            {
                Sty_ClassLogic classLogic = new Sty_ClassLogic();
                this.lblClassName.Text = classLogic.GetById(ClassID).ClassName;
            }
            //群组
            if (ClassSubgroupID == Guid.Empty)
            {
                this.lblGroupName.Text = "全部";
            }
            else
            {
                Sty_ClassSubgroupLogic classSubgroupLogic = new Sty_ClassSubgroupLogic();
                this.lblGroupName.Text = classSubgroupLogic.GetById(ClassSubgroupID).ClassSubgroupName;
            }
            //学生数
            this.lblStudentNum.Text = this.arrayList.Count.ToString();
            //积分原因类型
            string whereStr = string.Format(" and OrgID={0} and IsUse=1", ETMS.AppContext.UserContext.Current.OrganizationID);
            int  total = 0;
            DataTable dt = pointReasonTypeLogic.GetPagedList(1, int.MaxValue - 1, " PointReasonTypeName", whereStr, out total);
            this.ddlPointReasonTypeID.DataSource = dt;
            this.ddlPointReasonTypeID.DataTextField = "PointReasonTypeName";
            this.ddlPointReasonTypeID.DataValueField = "PointReasonTypeID";
            this.ddlPointReasonTypeID.DataBind();
            this.ddlPointReasonTypeID.Items.Insert(0, new ListItem("请选择", ""));
            //积分原因
            this.ddlPointReason.Items.Insert(0, new ListItem("请选择", ""));  
        }
        catch{}
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
            AnalyseStudentcourse();
            Guid[] studentSignupID=new Guid[arrayList.Count];
            for(int i=0;i<this.arrayList.Count;i++)
            {
                studentSignupID[i]=arrayList[i].ToGuid();
            }
           
                pointReasonDetailLogic.BatchAddStudentPointReasonDetail(studentSignupID,
                    this.ddlPointReason.SelectedValue.ToGuid(),
                    this.ddlPointReason.SelectedItem.Text,
                    this.txtGivePoints.Text.Trim().ToInt(),
                    this.txtRemark.Text.Trim(),
                    ETMS.AppContext.UserContext.Current.RealName,
                    ETMS.AppContext.UserContext.Current.UserID);
           
            JsUtility.SuccessMessageBoxAndCloseWindowAndTriggerRefreshEvent("保存成功！");
        }
        catch (ETMS.AppContext.BusinessException bizEx)
        {
            ETMS.WebApp.Manage.Extention.ShowScriptManagerMessage(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
        }
    }
}