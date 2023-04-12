using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Components.ExOfflineHomework.API.Entity;
using ETMS.Utility.Service.FileUpload;
using ETMS.Components.ExOfflineHomework.Implement.BLL;
using ETMS.Components.ExOfflineHomework.Implement;
using ETMS.Controls;
using ETMS.Utility.Service.FileUpload;
using ETMS.Components.Basic.Implement.BLL.TrainingItem;
using ETMS.Components.Basic.Implement.BLL.Course;
using ETMS.Utility;
using ETMS.Components.Basic.API.Entity.TrainingItem;
using ETMS.Components.Basic.API.Entity.Course;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Course;
using System.Data;

public partial class TraningImplement_TraningProjectManager_Controls_TrainingProjectPracticeInfo : System.Web.UI.UserControl
{
    public Res_e_OffLineJobLogic Logic = new Res_e_OffLineJobLogic();
    public OffLineJob OfflineJob = new OffLineJob();  
    #region 页面条件参数存放

    /// <summary>
    /// 获取项目id
    /// </summary>
    protected Guid TrainingItemID
    {
        get
        {
            if (ViewState["TrainingItemID"] == null)
            {
                //ViewState["TrainingItemCourseID"] = new Guid("a7f97727-b0c2-4611-8060-ca05c3437227");
                ViewState["TrainingItemID"] = BasePage.UrlParamDecode(Request.QueryString["TrainingItemID"]).ToGuid();
            }
            return ViewState["TrainingItemID"].ToGuid();
        }
        set
        {
            ViewState["TrainingItemID"] = value;
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
                string temp = Request.QueryString["op"].ToLower();
                ViewState["Op"] = BasePage.UrlParamDecode(Request.QueryString["op"].ToLower());
            }
            return (string)ViewState["Op"];
        }
        set
        {
            ViewState["Op"] = value;
        }
    }
  

    /// <summary>
    /// 作业id
    /// </summary>
    protected Guid JobID
    {
        get
        {
            if (ViewState["JobID"] == null)
            {
                string obj = Request.QueryString["id"].ToLower();
                ViewState["JobID"] = new Guid(BasePage.UrlParamDecode(Request.QueryString["id"].ToLower()));
            }
            return (Guid)ViewState["JobID"];
        }
        set
        {
            ViewState["JobID"] = value;
        }
    }

    #endregion
    /// <summary>
    /// 保存实例
    /// </summary>
    protected Res_e_OffLineJob entity
    {
        get
        {
            if (ViewState["Entity"] == null)
            {
                ViewState["Entity"] = new Res_e_OffLineJob();
            }
            return (Res_e_OffLineJob)ViewState["Entity"];
        }
        set
        {
            ViewState["Entity"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bind();
            Intital();
        }
    }
    private void Intital()
    {
        GetDate();
        switch (Op)
        {
            case "edit":
                entity = Logic.GetById(JobID);

                this.txtJobName.Text = this.entity.JobName;
                this.FCKeditorJobDescription.Text = this.entity.JobDescription;            
                this.rblStatus.SelectedValue = this.entity.IsUse.ToString();
                this.dttbBeginDate.Text = entity.BeginTime.ToDate();
                this.dttbEndDate.Text = entity.EndTime.ToDate();
                this.lbItemName.Text = new Res_ItemCourseOffLineJobLogic().GetByJobId(JobID).TrainingItemName;
                this.lbItemName.Visible = true;
                this.ddl_Item.Visible = false;
                break;
            default:
                break;
        }

    }

    protected void GetDate()
    {
        Tr_Item item = new Tr_ItemLogic().GetById(ddl_Item.SelectedValue.ToGuid());
        this.dttbBeginDate.Text = item.ItemBeginTime.ToDate();
        this.dttbEndDate.Text = item.ItemEndTime.ToDate();
    }

    /// <summary>
    /// 邦定项目信息
    /// </summary>
    private void bind()
    {
        Tr_ItemLogic itemLogic = new Tr_ItemLogic();
        //对本组织机构创建的 待审核、审核通过 并且启用的培训项目
        string Crieria = string.Format(" AND OrgID={0} AND ItemStatus in (10,20) AND IsUse=1", ETMS.AppContext.UserContext.Current.OrganizationID);
        int total = 0;
        ddl_Item.DataSource = itemLogic.GetPagedList(1, int.MaxValue - 1, " CreateTime DESC", Crieria, out total);
        ddl_Item.DataTextField = "ItemName";
        ddl_Item.DataValueField = "TrainingItemID";
        ddl_Item.DataBind();
       
    }
    protected void lbnSave_Click(object sender, EventArgs e)
    {
        switch (Op)
        {
            case "add":
                Add();
                break;
            case "edit":
                Edit();
                break;

        }
    }

    protected void Add()
    {
        entity.JobID = Guid.NewGuid();
        if (!String.IsNullOrEmpty(this.txtJobName.Text))
            entity.JobName = this.txtJobName.Text;
        entity.JobDescription = this.FCKeditorJobDescription.Text;
        entity.IsUse = this.rblStatus.SelectedValue.ToInt();
        if (!String.IsNullOrEmpty(this.dttbBeginDate.Text))
            entity.BeginTime = this.dttbBeginDate.Text.ToDateTime();
        if (!String.IsNullOrEmpty(this.dttbEndDate.Text))
            entity.EndTime = this.dttbEndDate.Text.ToDateTime();
        entity.TeacherID = ETMS.AppContext.UserContext.Current.UserName;    
        entity.CreateTime = System.DateTime.Now;
        entity.CreateUserID = ETMS.AppContext.UserContext.Current.UserID;
        entity.OrgID = ETMS.AppContext.UserContext.Current.OrganizationID;
        try
        {
           
            Logic.AddItemOffLineJob(entity, ddl_Item.SelectedValue.ToGuid());
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
    protected void Edit()
    {
        
        if (!String.IsNullOrEmpty(this.txtJobName.Text))
            entity.JobName = this.txtJobName.Text;

        entity.JobDescription = this.FCKeditorJobDescription.Text;

        entity.IsUse = Convert.ToInt16(this.rblStatus.SelectedValue);
        if (!String.IsNullOrEmpty(this.dttbBeginDate.Text))
            entity.BeginTime = this.dttbBeginDate.Text.ToDateTime();
        if (!String.IsNullOrEmpty(this.dttbEndDate.Text))
            entity.EndTime = this.dttbEndDate.Text.ToDateTime();
        entity.ModifyTime = DateTime.Now;
        entity.ModifyUser = ETMS.AppContext.UserContext.Current.RealName;
        try
        {
            Logic.Save(entity);
            ETMS.Utility.JsUtility.SuccessMessageBoxAndCloseWindowAndTriggerRefreshEvent("保存成功");
        }
        catch (ETMS.AppContext.BusinessException bizEx)
        {
            ETMS.Utility.JsUtility.FailedMessageBox(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
        }
    }

    protected void ddl_Item_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetDate();
        update1.Update();
    }
}