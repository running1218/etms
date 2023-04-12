using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Components.Basic.Implement.BLL;
using System.Data;
using ETMS.WebApp;
using ETMS.Controls;
using ETMS.Utility;
using ETMS.Components.StudyClass.Implement.BLL.StudyClass;
using ETMS.Components.StudyClass.API.Entity.StudyClass;

public partial class LearningManagement_GroupManager_GroupList : ETMS.Controls.BasePage
{
    private static Sty_ClassSubgroupStudentLogic classSubgroupStudentLogic = new Sty_ClassSubgroupStudentLogic();
    private static Sty_ClassSubgroupLogic classSubgroupLogic = new Sty_ClassSubgroupLogic();
    private static Sty_ClassLogic classLogic = new Sty_ClassLogic();
    // <summary>
    /// 班级ID
    /// </summary>
    public Guid ClassID
    {
        get { return Request.QueryString["ClassID"].ToGuid(); }
    }
    /// <summary>
    /// 项目ID
    /// </summary>
    public Guid TrainingItemID
    {
        get { return Request.QueryString["TrainingItemID"].ToGuid(); }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        PageSet1.pageInit(this.CustomGridView1, PageDataSource);

        if (!Page.IsPostBack)
        {
            this.PageSet1.QueryChange();
            InitialControl();
        }
        aBack.HRef = this.ActionHref(string.Format("{0}/LearningManagement/ClassManager/ClassList.aspx?TrainingItemID={1}",WebUtility.AppPath,TrainingItemID));
    }
    private void InitialControl()
    {
        try
        {
            int total = 0;
            DataTable dt = classLogic.GetClassItemList(1, 1, string.Empty, string.Format(" and TrainingItemID='{0}'", TrainingItemID), out total);
            this.lblItemName.Text = dt.Rows[0]["ItemName"].ToString();
            this.lblTime.Text = dt.Rows[0]["ItemBeginTime"].ToDate() + "至" + dt.Rows[0]["ItemEndTime"].ToDate();
            //this.lblSingnNum.Text = dt.Rows[0]["SingnNum"].ToString(); 
            //班级
            List<Sty_Class> classList = classLogic.GetClassListByTrainingItemID(TrainingItemID);
            var classListResult = classList.SingleOrDefault(f=>f.ClassID.Equals(ClassID));
            this.lblGrade.Text = classListResult.ClassName;
            this.lblSingnNum.Text = classListResult.AssignCount.ToString(); 
            //未分班的
            total = 0;
            List<Sty_ClassStudent> list = classSubgroupStudentLogic.ChoseGroupStudentByClassID(ClassID, string.Empty, string.Empty, 1, int.MaxValue-1, out total);
            this.lblUnAssignNum.Text = total.ToString();
            //已分班的
            this.lblAssignNum.Text = (lblSingnNum.Text.ToInt() - total).ToString();
        }
        catch { }
    }
   
    private System.Collections.IList PageDataSource(int pageIndex, int pageSize, out int totalRecordCount)
    {
        List<Sty_ClassSubgroup> groupList= classSubgroupLogic.GetGroupListByClassID(ClassID,pageIndex,pageSize,out totalRecordCount);

        PageDataSourceProvider pageDataSource = new PageDataSourceProvider(groupList, pageIndex, pageSize);
        return pageDataSource.PageDataSource;
    }
    protected void CustomGridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow && !this.CustomGridView1.IsEmpty)
        {
            Sty_ClassSubgroup drv = (Sty_ClassSubgroup)e.Row.DataItem;
            CustomLinkButton lbnDel = (CustomLinkButton)e.Row.FindControl("lbtn_Del");
            if (drv.GroupStudentNum > 0)
            {
                lbnDel.Enabled = false;
                lbnDel.EnableConfirm = false;
                lbnDel.ForeColor = System.Drawing.Color.Gray;
            }
        }
    }

    //单个删除班级信息
    protected void CustomGridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            CustomLinkButton lbnDel = e.CommandSource as CustomLinkButton;

            if (e.CommandName == "Del" && lbnDel.Enabled)
            {
                classSubgroupLogic.Remove(e.CommandArgument.ToGuid());
                this.PageSet1.DataBind();
            }
        }
        catch (ETMS.AppContext.BusinessException bizEx)
        {
            JsUtility.FailedMessageBox(ExceptionUtility.GetTranslateExceptionMessage(bizEx));
            return;
        }
    }
}