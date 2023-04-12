using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Components.StudyClass.API.Entity.StudyClass;
using ETMS.Components.StudyClass.Implement.BLL.StudyClass;

public partial class LearningManagement_GroupManager_Controls_GroupInfo : System.Web.UI.UserControl
{
    private static Sty_ClassSubgroupLogic classSubgroupLogic = new Sty_ClassSubgroupLogic();
    /// <summary>
    /// 班级iD
    /// </summary>
    private Guid classID;
    public Guid ClassID
    {
        set { classID = value; }
        get { return classID; }
    }
    /// <summary>
    /// 学习群组编码
    /// </summary>
    private Guid classSubgroupID;
    public Guid ClassSubgroupID
    {
        set { classSubgroupID = value; }
        get { return classSubgroupID; }
    }

    public Sty_ClassSubgroup ClassSubgroup
    {
        set { ViewState["ClassSubgroup"] = value; }
        get 
        {
            if(ViewState["ClassSubgroup"]==null)
                ViewState["ClassSubgroup"] = new Sty_ClassSubgroup();
            return (Sty_ClassSubgroup)ViewState["ClassSubgroup"];
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitialEntity();
        }
    }

    private void InitialEntity()
    {
        if (classSubgroupID != new Guid())
        {
            ClassSubgroup = classSubgroupLogic.GetById(classSubgroupID);
            this.txtGroupName.Text = ClassSubgroup.ClassSubgroupName;
            this.txtGroupDescription.Text = ClassSubgroup.ClassSubgroupDesc;
            ClassSubgroup.ModifyTime = DateTime.Now;
            ClassSubgroup.ModifyUser = ETMS.AppContext.UserContext.Current.RealName;
            
        }
        else
        {
            ClassSubgroup.ClassID = classID;
            ClassSubgroup.CreateTime = DateTime.Now;
            ClassSubgroup.CreateUser = ETMS.AppContext.UserContext.Current.RealName;
            ClassSubgroup.CreateUserID = ETMS.AppContext.UserContext.Current.UserID;
        }
        
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            ClassSubgroup.ClassSubgroupName = this.txtGroupName.Text;
            ClassSubgroup.ClassSubgroupDesc = this.txtGroupDescription.Text;
            classSubgroupLogic.Save(ClassSubgroup);
            ETMS.Utility.JsUtility.SuccessMessageBoxAndCloseWindowAndTriggerRefreshEvent("保存成功");
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