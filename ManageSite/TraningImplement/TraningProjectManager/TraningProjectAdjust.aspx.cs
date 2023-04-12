using System;
using ETMS.Utility;
using ETMS.Components.Basic.Implement.BLL.TrainingItem;
using ETMS.Components.Basic.API.Entity.TrainingItem;
using ETMS.Controls;
using ETMS.Components.Basic.Implement.BLL.TrainingPlan;
using ETMS.AppContext;

public partial class TraningImplement_TraningProjectManager_TraningProjectAdjust : BasePage
{
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

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack && !string.IsNullOrEmpty(BasePage.getSafeRequest(this.Page, "TrainingItemID")))
        {
            TrainingItemID = new Guid(BasePage.getSafeRequest(this.Page, "TrainingItemID"));
            bind();
        }
    }

    /// <summary>
    /// 邦定信息
    /// </summary>
    private void bind()
    {
        Tr_ItemLogic itemLogic = new Tr_ItemLogic();
        Tr_Item item = itemLogic.GetById(TrainingItemID);
        if (item != null)
        {
            lbl_ItemCode.Text = item.ItemCode;
            lbl_ItemName.Text = item.ItemName;

            Dtt_ItemBeginTime.Text = item.ItemBeginTime.ToDate();
            Dtt_ItemEndTime.Text = item.ItemEndTime.ToDate();

            lblSignupMode.FieldIDValue = item.SignupModeID.ToString();
            //项目自主报名与课程自主报名时可以修改报名时间
            if (item.SignupModeID == 1 || item.SignupModeID == 2)
            {
                trSignup.Visible = true;
            }
            dttSignupBeginTime.Text = item.SignupBeginTime.ToDate();
            dttSignupEndTime.Text = item.SignupEndTime.ToDate();
            txtSelfChooseCourseNum.Text = ((int)item.BudgetFee).ToString();
        }
    }

     /// <summary>
    /// 保存
    /// </summary>
    protected void Btn_Save_Click(object sender, EventArgs e)
    {
        Tr_ItemLogic itemLogic = new Tr_ItemLogic();

        Tr_Item item = itemLogic.GetById(TrainingItemID);
        //验证项目状态是否被修改
        if (item.ItemStatus != 90 && item.IsIssue)
        {
            item.ItemBeginTime = Dtt_ItemBeginTime.Text.ToDateTime();
            item.ItemEndTime = Dtt_ItemEndTime.Text.ToDateTime();

            item.SignupBeginTime = dttSignupBeginTime.Text.ToDateTime();
            item.SignupEndTime = dttSignupEndTime.Text.ToDateTime();
            item.ModifyTime = DateTime.Now;
            item.ModifyUser = ETMS.AppContext.UserContext.Current.RealName;
            item.BudgetFee = txtSelfChooseCourseNum.Text.ToDecimal();
            itemLogic.Save(item, OperationAction.Edit);
            ETMS.Utility.JsUtility.SuccessMessageBoxAndCloseWindowAndTriggerRefreshEvent("提示", "项目信息调整成功，请调整此项目下的课程周期。");
        }
        else { 
            ETMS.Utility.JsUtility.FailedMessageBox(" 提示", "当前项目状态发生变化，不能修改！");
            return;           
        }
    }

}