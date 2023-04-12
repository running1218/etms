using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.AppContext;
using ETMS.Components.Basic.API.Entity.TrainingItem;
using ETMS.Components.Basic.Implement.BLL.TrainingItem;
using ETMS.Components.Basic.Implement.BLL.TrainingPlan;
using ETMS.Controls;
using ETMS.Utility;
using ETMS.Utility.Service.FileUpload;
using ETMS.WebApp.Manage;
using System.Collections.Generic;

public partial class TraningImplement_TraningProjectManager_Controls_TraningProjectInfo : System.Web.UI.UserControl
{
    #region 页面参数

    /// <summary>
    /// 操作动作
    /// </summary>
    public OperationAction Action
    {
        get;
        set;
    }
    /// <summary>
    /// 项目ID
    /// </summary>
    public Guid TrainingItemID
    {
        get
        {
            return ViewState["TrainingItemID"].ToGuid();
        }
        set
        {
            ViewState["TrainingItemID"] = value;
        }
    }

    /// <summary>
    /// 项目对象
    /// </summary>
    public Tr_Item Item
    {
        get
        {
            if (ViewState["Item"] == null)
                return null;
            else
                return (Tr_Item)ViewState["Item"];
        }
        set { ViewState["Item"] = value; }
    }

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bindPlan();
            if (Action == OperationAction.Add)
            {
                rblIsUse.SelectedValue = "1";
                rblTrainingLevelID.SelectedValue = "1";
                Rbl_IsPlanItem.SelectedValue = "0";
                Dic_SignupMode.SelectedValue = "3";
                Dtt_ItemBeginTime.Text =DateTime.Now.ToDate();
                Dtt_ItemEndTime.Text = DateTime.Now.AddYears(1).AddDays(-1).ToDate();
                imgCourseLogo.ImageUrl = StaticResourceUtility.GetFullPathByFileType(FileUploadFunctionType.ItemLogo, "default.jpg");     
            }
            else if (Action == OperationAction.Edit && !string.IsNullOrEmpty(BasePage.getSafeRequest(this.Page, "TrainingItemID")))
            {
                TrainingItemID = new Guid(BasePage.getSafeRequest(this.Page, "TrainingItemID"));
                bind();
            }
        }
    }

    /// <summary>
    /// 邦定计划信息
    /// </summary>
    private void bindPlan() { 
        Tr_PlanLogic planLogic = new Tr_PlanLogic();
        int total = 0;
        //对本组织机构下，已审核通过、启用、且培训周期未结束的培训计划
        string criteria =string.Format(" AND IsUse=1 AND PlanStatus=20 AND PlanBeginTime<='{0}' AND PlanEndTime>='{0}'",DateTime.Now);
        Ddl_PlanID.DataSource = planLogic.GetPlanListByOrgID(ETMS.AppContext.UserContext.Current.OrganizationID, 1, int.MaxValue-1,"",criteria, out total);
        Ddl_PlanID.DataTextField = "PlanName";
        Ddl_PlanID.DataValueField = "PlanID";
        Ddl_PlanID.DataBind();
        Ddl_PlanID.Items.Insert(0, new ListItem("请选择", ""));
    }

    /// <summary>
    /// 邦定信息
    /// </summary>
    private void bind()
    {
        Tr_ItemLogic itemLogic = new Tr_ItemLogic();
        Tr_Item item = itemLogic.GetById(TrainingItemID);
        Item = item;
        if (item != null)
        {
            txt_ItemCode.Text = item.ItemCode;
            Txt_ItemName.Text = item.ItemName;
            ddlSpecialtyTypeCode.SelectedValue = item.SpecialtyTypeCode;
            Rbl_IsPlanItem.SelectedValue = item.IsPlanItem ? "1" : "0";
            Ddl_PlanID.SelectedValue = item.PlanID.ToString();
            rblIsUse.SelectedValue = item.IsUse.ToString();
            rblTrainingLevelID.SelectedValue = item.TrainingLevelID.ToString();
            ddlDutyDeptID.SelectedValue = item.DutyDeptID.ToString();
            Dtt_ItemBeginTime.Text = item.ItemBeginTime.ToDate();
            Dtt_ItemEndTime.Text = item.ItemEndTime.ToDate();
            Dic_SignupMode.SelectedValue = item.SignupModeID.ToString();
            dttSignupBeginTime.Text = item.SignupBeginTime.ToDate();
            dttSignupEndTime.Text = item.SignupEndTime.ToDate();
            Txt_DutyUser.Text = item.DutyUser;
            Txt_Mobile.Text = item.Mobile;
            Txt_Mobile.Attributes.Add("t_value", Txt_Mobile.Text);
            Txt_EMAIL.Text = item.EMAIL;
            Txt_ItemTarget.Text = item.ItemTarget;
            Txt_ItemObjectStudent.Text = item.ItemObjectStudent;
            Txt_Remark.Text = item.Remark;
            txtSelfChooseCourseNum.Text = decimal.Round(item.BudgetFee, 0).ToString();
            if (item.PayFrom != 0) {
                Dic_SignupMode.Enabled = false;
                Dic_SignupMode.ToolTip = "已设置收费标准不可修改";
            }
            imgCourseLogo.ImageUrl = StaticResourceUtility.GetFullPathByFileType(FileUploadFunctionType.ItemLogo, string.IsNullOrEmpty(item.ThumbnailURL) ? "default.jpg" : item.ThumbnailURL);     
        }
    }

    /// <summary>
    /// 保存
    /// </summary>
    protected void Btn_Save_Click(object sender, EventArgs e)
    {
        if (CheckMsg()) return;

        Tr_Item newItem = new Tr_Item();
        Tr_ItemLogic itemLogic = new Tr_ItemLogic();
        if (Action == OperationAction.Add)
        {
            SetItem(newItem);
            newItem.TrainingItemID = Guid.NewGuid();
            newItem.OrgID = UserContext.Current.OrganizationID;
            newItem.ItemStatus = 10;
            newItem.CreateTime = DateTime.Now;
            newItem.CreateUserID = ETMS.AppContext.UserContext.Current.UserID;
            newItem.CreateUser = ETMS.AppContext.UserContext.Current.RealName;
            newItem.ModifyTime = DateTime.Now;
            newItem.ModifyUser = ETMS.AppContext.UserContext.Current.RealName;
        }
        else if (Action == OperationAction.Edit)
        {
            newItem = Item;
            SetItem(newItem);
            newItem.TrainingItemID = TrainingItemID;
            newItem.ModifyTime = DateTime.Now;
            newItem.ModifyUser = ETMS.AppContext.UserContext.Current.RealName;

            //验证项目状态是否被修改
            Tr_Item item = itemLogic.GetById(TrainingItemID);
            if (item.ItemStatus != 10) {
                ETMS.Utility.JsUtility.FailedMessageBox(" 提示", "当前项目状态发生变化，不能修改！");
                return;
            }
        }
        try
        {
            List<FileUploadInfo> uploaders = this.uploader.FileUrl;
            FileUploadInfo fileDefine = uploaders.Count > 0? this.uploader.FileUrl[0] : null;

            newItem.ThumbnailURL = fileDefine == null ? newItem.ThumbnailURL : fileDefine.BizUrl;
            itemLogic.Save(newItem, Action);
            ETMS.Utility.JsUtility.SuccessMessageBoxAndCloseWindowAndTriggerRefreshEvent("项目信息保存成功！");
        }
        catch (ETMS.AppContext.BusinessException bizEx)
        {
            ETMS.Utility.JsUtility.FailedMessageBox(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
            return;
        }
    }

    /// <summary>
    /// 验证
    /// </summary>
    /// <returns></returns>
    private bool CheckMsg() {
        bool res = false;
        //验证培训计划
        if (Rbl_IsPlanItem.SelectedValue.ToBoolean()) {
            if (Ddl_PlanID.SelectedValue == "") {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "msg", "<script>alert('请选择培训计划！')</script>");
                res = true;
            }
        }
        //培训级别 如果是部门级
        if (rblTrainingLevelID.SelectedValue.ToInt() == 2) {
            if (ddlDutyDeptID.SelectedValue == "") {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "msg", "<script>alert('请选择组织部门！')</script>");
                res = true;
            }
        }
        //验证报名开始时间与结束时间
        if (Dic_SignupMode.SelectedValue == "1" || Dic_SignupMode.SelectedValue == "2")
        {
            if (dttSignupBeginTime.Text.Trim() == "") {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "msg", "<script>alert('报名开始时间不能为空！')</script>");
                res = true;
            }
            else if (dttSignupEndTime.Text.Trim() == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "msg", "<script>alert('报名结束时间不能为空！')</script>");
                res = true;
            }
        }
        return res;
    }

    /// <summary>
    /// 为对象赋值
    /// </summary>
    private void SetItem(Tr_Item newItem)
    {
        newItem.ItemCode = txt_ItemCode.Text.Trim();
        newItem.ItemName = Txt_ItemName.Text.Trim();
        newItem.SpecialtyTypeCode = ddlSpecialtyTypeCode.SelectedValue;
        newItem.IsPlanItem = Rbl_IsPlanItem.SelectedValue.ToBoolean();
        //是否来自计划
        newItem.PlanID = newItem.IsPlanItem ? Ddl_PlanID.SelectedValue.ToGuid() : Guid.Empty;
        newItem.TrainingLevelID = rblTrainingLevelID.SelectedValue.ToInt();//1 公司级  2部门级
        newItem.IsUse = rblIsUse.SelectedValue.ToInt();
        //培训级别
        if (newItem.TrainingLevelID == 2 && ddlDutyDeptID.SelectedValue != "-1" && ddlDutyDeptID.SelectedValue != "")
        {
            newItem.DutyDeptID = ddlDutyDeptID.SelectedValue.ToInt();
        }
        else
        {
            newItem.DutyDeptID = 0;
        }
        newItem.ItemBeginTime = Dtt_ItemBeginTime.Text.ToDateTime();
        newItem.ItemEndTime = Dtt_ItemEndTime.Text.ToDateTime();
        newItem.SignupModeID = Dic_SignupMode.SelectedValue.ToInt();
        //是否允许学员报名
        if (newItem.SignupModeID == 1 || newItem.SignupModeID == 2)
        {
            newItem.SignupBeginTime = dttSignupBeginTime.Text.ToDateTime();
            newItem.SignupEndTime = dttSignupEndTime.Text.ToDateTime();
        }
        else
        {
            newItem.SignupBeginTime =DateTime.MinValue;
            newItem.SignupEndTime = DateTime.MinValue;
        }
        newItem.DutyUser = Txt_DutyUser.Text;
        newItem.Mobile = Txt_Mobile.Text;
        newItem.EMAIL = Txt_EMAIL.Text;
        newItem.ItemTarget = Txt_ItemTarget.Text;
        newItem.ItemObjectStudent = Txt_ItemObjectStudent.Text;
        newItem.Remark = Txt_Remark.Text;
        newItem.BudgetFee = txtSelfChooseCourseNum.Text.Trim().ToDecimal();
    }
}
