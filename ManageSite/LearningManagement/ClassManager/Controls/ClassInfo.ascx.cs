using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Components.StudyClass.API.Entity.StudyClass;
using ETMS.Components.StudyClass.Implement.BLL.StudyClass;

public partial class LearningManagement_ClassManager_Controls_ClassInfo : System.Web.UI.UserControl
{
    private Sty_ClassLogic classLogic = new Sty_ClassLogic();
    private Sty_Class StyClass
    {
        get
        {
            if (ViewState["StyClass"] == null)
                ViewState["StyClass"] = new Sty_Class();
            return (Sty_Class)ViewState["StyClass"];
        }
        set { ViewState["StyClass"] = value; }
    
    }
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
    /// 项目编码
    /// </summary>
    private Guid trainingItemID;
    public Guid TrainingItemID
    {
        set { trainingItemID = value; }
        get { return trainingItemID; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitialControls();
        }
    }
    private void InitialControls()
    {
        if (classID != new Guid())
        {
            StyClass = classLogic.GetById(classID);
            this.txtClassName.Text = StyClass.ClassName;
            this.txtClassDesc.Text = StyClass.ClassDesc;
            this.txtDutyUser.Text = StyClass.DutyUser;
            this.txtTel.Text = StyClass.TelPhone;
            this.txtEmail.Text = StyClass.Email;
            StyClass.ModifyTime = DateTime.Now;
            StyClass.ModifyUser = ETMS.AppContext.UserContext.Current.RealName;
        }
        else
        {
            //StyClass.ClassID = Guid.NewGuid();
            StyClass.TrainingItemID = trainingItemID;
            StyClass.CreateUser = ETMS.AppContext.UserContext.Current.RealName;
            StyClass.CreateUserID = ETMS.AppContext.UserContext.Current.UserID;
            StyClass.CreateTime = DateTime.Now;
        }
    }

    private void InitialEntity()
    {
        StyClass.ClassName = this.txtClassName.Text;
        StyClass.ClassDesc = this.txtClassDesc.Text;
        StyClass.DutyUser = this.txtDutyUser.Text;
        StyClass.TelPhone = this.txtTel.Text;
        StyClass.Email = this.txtEmail.Text;
        
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            InitialEntity();
            classLogic.Save(StyClass);
            ETMS.Utility.JsUtility.SuccessMessageBoxAndCloseWindowAndTriggerRefreshEvent("保存成功");
        }
        catch (ETMS.AppContext.BusinessException bizEx)
        {
            ETMS.Utility.JsUtility.FailedMessageBox(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
        }
        catch (Exception ex)
        {
            if (ex.Message.IndexOf("ClassBBS.dbo.dnt_e_course", StringComparison.InvariantCultureIgnoreCase) != -1)
            {
                throw new ETMS.AppContext.BusinessException("将班级信息数据同步到班级论坛（ClassBBS）出错，请与系统管理员联系！");
            }
            ETMS.Utility.JsUtility.FailedMessageBox(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(ex));
        }
    }
}