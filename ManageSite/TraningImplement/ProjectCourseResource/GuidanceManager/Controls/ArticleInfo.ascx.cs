using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Components.Basic.Implement.BLL.Bulletin;
using ETMS.Components.Basic.API.Entity.Bulletin;
using ETMS.Controls;
using System.Data;
using ETMS.Utility;

public partial class QuestionDB_GuidanceManager_Controls_ArticleInfo : System.Web.UI.UserControl
{
    Inf_BulletinLogic Logic = new Inf_BulletinLogic();
    /// <summary>
    /// 培训项目课程编号
    /// </summary>
    protected Guid TrainingItemCourseID
    {
        get
        {
            if (ViewState["TrainingItemCourseID"] == null)
            {
                ViewState["TrainingItemCourseID"] = BasePage.UrlParamDecode(Request.QueryString["TrainingItemCourseID"]).ToGuid();
            }
            return ViewState["TrainingItemCourseID"].ToGuid();
        }
        set
        {
            ViewState["TrainingItemCourseID"] = value;
        }
    }
    /// <summary>
    /// 保存是添加还是修改
    /// </summary>
    protected string Op
    {
        get
        {
            if (ViewState["Op"] == null)
            {
                ViewState["Op"] = BasePage.UrlParamDecode(Request.QueryString["op"].ToLower());
            }
            return (string)ViewState["Op"];
        }
        set
        {
            ViewState["Op"] = value;
        }
    }
    public Inf_Bulletin Entity
    {
        get
        {
            if (ViewState["Entity"] == null)
            {
                ViewState["Entity"] = new Inf_Bulletin();
            }
            return (Inf_Bulletin)ViewState["Entity"];
        }
        set { ViewState["Entity"] = value; }
    }
    /// <summary>
    /// 获取id
    /// </summary>
    protected int ArticleID
    {
        get
        {
            if (ViewState["ArticleID"] == null)
            {
                ViewState["ArticleID"] = int.Parse(BasePage.UrlParamDecode(Request.QueryString["id"]));
            }
            return (int)ViewState["ArticleID"];
        }
        set
        {
            ViewState["ArticleID"] = value;
        }
    }

    protected int ArticleType
    {
        get
        {
           return  Request.QueryString["ArticleType"].ToInt();
        }
    }
    private void Intital()
    {        
        switch (Op)
        {
            case "edit":
                Entity = Logic.GetById(ArticleID);
                this.txtMainHead.Text = Entity.MainHead;
                this.txtBrief.Text = Entity.Brief;               
                this.fckEditor.Text = Entity.ArticleContent;
                this.rblStatus.SelectedValue = Entity.IsUse.ToString();
                this.txtKeyword.Text = Entity.Keyword;
                break;

        }

    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Intital();
        }
    }
    protected void InitialControlers()
    {
        Entity.MainHead = this.txtMainHead.Text.Trim();
        Entity.Brief = this.txtBrief.Text.Trim();
        
        Entity.InfoLevelID = 3;
        Entity.ArticleContent = this.fckEditor.Text;
        Entity.IsUse = this.rblStatus.SelectedValue.ToInt();
        Entity.BeginDate =DateTime.Now;
        Entity.EndDate =DateTime.Now;
        Entity.TrainingItemCourseID = TrainingItemCourseID;
        Entity.Keyword = this.txtKeyword.Text;
    }
    protected void lbnSave_Click(object sender, EventArgs e)
    {
        switch (Op)
        {
            case "add":
                Entity = new Inf_Bulletin();
                InitialControlers();
                Entity.CreateTime = DateTime.Now;
                Entity.CreateMan = ETMS.AppContext.UserContext.Current.RealName;
                Entity.CreateUserID = ETMS.AppContext.UserContext.Current.UserID;
                Entity.ArticleTypeID = ArticleType;               
                try
                {
                    Logic.AddItemCourseMentorData(Entity);
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
                break;
            case "edit":
                InitialControlers();
                Entity.UpdateMan = ETMS.AppContext.UserContext.Current.RealName;
                Entity.UpdateTime = DateTime.Now;               
                try
                {
                    Logic.Save(Entity);
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
                break;

        }
    }
}