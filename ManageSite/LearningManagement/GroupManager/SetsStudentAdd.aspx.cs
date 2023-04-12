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
using ETMS.Components.Basic.Implement;

public partial class LearningManagement_GroupManager_SetsStudentAdd : ETMS.Controls.BasePage
{
    private static Sty_ClassSubgroupStudentLogic classSubgroupStudentLogic = new Sty_ClassSubgroupStudentLogic();
    private static Sty_ClassSubgroupStudent classSubgroupStudent = new Sty_ClassSubgroupStudent();
    private static PublicFacade publicFacade = new PublicFacade();
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

    /// <summary>
    /// 学习群组Id
    /// </summary>
    public Guid ClassSubgroupID
    {
        get { return Request.QueryString["ClassSubgroupID"].ToGuid(); }
    }
    /// <summary>
    /// 学习群组名称
    /// </summary>
    public string ClassSubgroupName
    {
        get { return Request.QueryString["ClassSubgroupName"].ToString(); }
    }  

    protected void Page_Load(object sender, EventArgs e)
    {
        PageSet1.pageInit(this.CustomGridView1, PageDataSource);

        if (!Page.IsPostBack)
        {
            this.PageSet1.QueryChange();
        }
        aBack.HRef = this.ActionHref(string.Format("SetsStudentList.aspx?ClassID={0}&TrainingItemID={1}&ClassSubgroupID={2}&ClassSubgroupName={3}", ClassID, TrainingItemID, ClassSubgroupID, ClassSubgroupName));
        //单机构版本隐藏“组织机构”列
        if (ETMS.Product.ProductDefine.IsSingleOrganizationVersion)
        {
            this.CustomGridView1.Columns[3].Visible = false;
        }
    }

    private System.Collections.IList PageDataSource(int pageIndex, int pageSize, out int totalRecordCount)
    {
        string realName=this.txtRealName.Text.Trim();
        string workerNo=this.txtWorkerNo.Text.Trim();
        List<Sty_ClassStudent> list = classSubgroupStudentLogic.ChoseGroupStudentByClassID(ClassID,realName,workerNo,pageIndex,pageSize,out totalRecordCount);

        PageDataSourceProvider pageDataSource = new PageDataSourceProvider(list, pageIndex, pageSize);
        return pageDataSource.PageDataSource;
    }

    protected void CustomGridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow && !this.CustomGridView1.IsEmpty)
        {
            Sty_ClassStudent entity = (Sty_ClassStudent)e.Row.DataItem;            

            Label lblPostion = (Label)e.Row.FindControl("lblPostion");
            if (!string.IsNullOrEmpty(entity.ClassPostion))
            {
                lblPostion.Text = entity.ClassPostion;
            }
            else
            {
                lblPostion.Text = "普通学员";
            }            
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        this.PageSet1.QueryChange();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
            try
            {
                Guid[] selectValue = CustomGridView.GetSelectedValues<Guid>(this.CustomGridView1);
                if (selectValue.Length > 0)
                {
                    for (int i = 0; i < selectValue.Length; i++)
                    {
                        classSubgroupStudent = new Sty_ClassSubgroupStudent();
                        classSubgroupStudent.ClassSubgroupID = ClassSubgroupID;
                        classSubgroupStudent.IsLeader = false;
                        classSubgroupStudent.CreateTime = DateTime.Now;
                        classSubgroupStudent.CreateUser = ETMS.AppContext.UserContext.Current.RealName;
                        classSubgroupStudent.CreateUserID = ETMS.AppContext.UserContext.Current.UserID;
                        classSubgroupStudent.ClassStudentID = selectValue[i];
                        classSubgroupStudentLogic.Save(classSubgroupStudent);
                        ETMS.WebApp.Manage.Extention.SuccessMessageBox("提示", "新增群组学员成功！", "function(){window.location = '" + this.ActionHref(string.Format("SetsStudentList.aspx?ClassID={0}&TrainingItemID={1}&ClassSubgroupID={2}&ClassSubgroupName={3}", ClassID, TrainingItemID, ClassSubgroupID, ClassSubgroupName)) + "'}");
                    }
                }
                else
                {
                    ETMS.WebApp.Manage.Extention.AlertMessageBox("请选择学员");
                }
            }
            catch (ETMS.AppContext.BusinessException bizEx)
            {
                ETMS.Utility.JsUtility.FailedMessageBox(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
            }
            catch (Exception ex)
            {
                ETMS.Utility.JsUtility.FailedMessageBox(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(ex));
            }        
    }   
}