using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Utility;
using ETMS.Controls;
using ETMS.Components.Basic.Implement.BLL.TrainingItem;
using ETMS.Components.Basic.API.Entity.TrainingItem;

public partial class TraningImplement_TraningProjectManager_TraningProjectCopy : System.Web.UI.Page
{
    /// <summary>
    /// 原项目ID
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
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(BasePage.getSafeRequest(this.Page, "TrainingItemID")))
            {
                TrainingItemID = BasePage.getSafeRequest(this.Page, "TrainingItemID").ToGuid();
                bind();
            }
        }
    }

    //邦定原项目信息
    private void bind() { 
        Tr_ItemLogic itemLogic = new Tr_ItemLogic();
        Tr_Item item = itemLogic.GetById(TrainingItemID);
        if (item != null)
        {
            labItemCode.Text = item.ItemCode;
            labItemName.Text = item.ItemName;
            labItemCycle.Text = string.Format("{0} 至 {1}", item.ItemBeginTime.ToDate(), item.ItemEndTime.ToDate());

            Txt_ItemName.Text = string.Format("{0}_复制", item.ItemName);
        }
        Dtt_ItemBeginTime.Text = DateTime.Now.ToDate();
        Dtt_ItemEndTime.Text = DateTime.Now.AddYears(1).AddDays(-1).ToDate();
    }

     /// <summary>
    /// 保存
    /// </summary>
    protected void Btn_Save_Click(object sender, EventArgs e)
    {
        Tr_Item item = new Tr_Item();
        item.TrainingItemID = TrainingItemID;
        item.ItemName = Txt_ItemName.Text;
        item.ItemBeginTime = Dtt_ItemBeginTime.Text.ToDateTime();
        item.ItemEndTime = Dtt_ItemEndTime.Text.ToDateTime();
        item.CreateUserID = ETMS.AppContext.UserContext.Current.UserID;
        item.CreateUser = ETMS.AppContext.UserContext.Current.RealName;

        try
        {
            Tr_ItemLogic itemLogic = new Tr_ItemLogic();
            itemLogic.ItemCopy(item);
            ETMS.Utility.JsUtility.SuccessMessageBoxAndCloseWindowAndTriggerRefreshEvent("项目信息复制成功！");
        }
        catch (ETMS.AppContext.BusinessException bizEx)
        {
            ETMS.Utility.JsUtility.FailedMessageBox(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
            return;
        }
    }

}