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
using ETMS.Components.Basic.Implement;
using ETMS.Components.StudyClass.Implement.BLL.StudyClass;
using ETMS.Components.StudyClass.API.Entity.StudyClass;

public partial class LearningManagement_GroupManager_SetsStudentList :ETMS.Controls.BasePage
{
    public static PublicFacade publicFacade = new PublicFacade();
    public static Sty_ClassLogic classLogic = new Sty_ClassLogic();
    public static Sty_ClassStudent classStudent = new Sty_ClassStudent();
    public static Sty_ClassStudentLogic classStudentLogic = new Sty_ClassStudentLogic();

    private static Sty_ClassSubgroupStudentLogic classsubgroupStudentLogic = new Sty_ClassSubgroupStudentLogic();
    //private static Guid[] subgroupStudentIDs = new Guid[20];
    private static Sty_ClassSubgroupStudent classSubgroupStudent = new Sty_ClassSubgroupStudent();

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
            InitialControl();
        }
        aBack.HRef = this.ActionHref(string.Format("{0}/LearningManagement/GroupManager/GroupList.aspx?ClassID={1}&TrainingItemID={2}",WebUtility.AppPath,ClassID,TrainingItemID));
        //单机构版本隐藏“组织机构”列
        if (ETMS.Product.ProductDefine.IsSingleOrganizationVersion)
        {
            this.CustomGridView1.Columns[4].Visible = false;
        }
    }

    private void InitialControl()
    {
        try
        {
            int total = 0;            
            DataTable dt = classLogic.GetClassItemList(1, 1, string.Empty, string.Format(" and TrainingItemID='{0}'", TrainingItemID), out total);
            this.lblItemName.Text = dt.Rows[0]["ItemName"].ToString();
            Sty_Class styClass = classLogic.GetById(ClassID);
            this.lblClassName.Text = styClass.ClassName;
            this.lblGroupName.Text = ClassSubgroupName;
        }
        catch { }
    }

    protected void btn1_Click(object sender, EventArgs e)
    {
        ETMS.Utility.JsUtility.SuccessMessageBox("提示", "设置成功！", "function(){location.href=location.href;}");
    }
   
    private System.Collections.IList PageDataSource(int pageIndex, int pageSize, out int totalRecordCount)
    {
        List<Sty_ClassStudent> classStudentList = classsubgroupStudentLogic.GetGroupStudentByGroupID(ClassSubgroupID);

        PageDataSourceProvider pageDataSource = new PageDataSourceProvider(classStudentList, pageIndex, pageSize);
        totalRecordCount = classStudentList.Count;
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
                lblPostion.Text = "学员";
            }
            LinkButton btnLeader = e.Row.FindControl("btnLeader") as LinkButton;
            btnLeader.Attributes.Add("onclick",string.Format("javascript:showWindow('设置队长','{0}',500,300);javascript: return false;",this.ActionHref(string.Format("SetLeader.aspx?SubgroupStudentID={0}", entity.SubgroupStudentID))));
        }
    }

    protected void btnLeader_Click(object sender, EventArgs e)
    {
        
        try
        {
            Guid[] selectValue = CustomGridView.GetSelectedValues<Guid>(this.CustomGridView1);
            if (selectValue.Length > 0)
            {
                for (int i = 0; i < selectValue.Length; i++)
                {
                    classSubgroupStudent = classsubgroupStudentLogic.GetById(selectValue[i]);
                    classSubgroupStudent.IsLeader = true;
                    classSubgroupStudent.ModifyTime = DateTime.Now;
                    classSubgroupStudent.ModifyUser = ETMS.AppContext.UserContext.Current.RealName;
                    classsubgroupStudentLogic.Save(classSubgroupStudent);                       
                }
                ETMS.Utility.JsUtility.SuccessMessageBoxAndCloseWindowAndTriggerRefreshEvent("队长设置成功！");
                this.PageSet1.DataBind();
            }
            else
            {
                ETMS.Utility.JsUtility.AlertMessageBox("请选择学员");
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
    protected void btnDel_Click(object sender, EventArgs e)
    {
       
        try
        {
                Guid[] selectValue = CustomGridView.GetSelectedValues<Guid>(this.CustomGridView1);
                if (selectValue.Length > 0)
                {
                    classsubgroupStudentLogic.Delete(selectValue);
                    this.PageSet1.DataBind();
                }
                else
                {
                    ETMS.Utility.JsUtility.AlertMessageBox("请选择学员");
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