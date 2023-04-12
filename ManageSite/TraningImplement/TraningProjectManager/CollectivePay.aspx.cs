using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Controls;
using ETMS.Components.Basic.Implement.BLL.TrainingItem;
using ETMS.Components.Basic.API.Entity.TrainingItem;
using ETMS.Utility;

public partial class TraningImplement_TraningProjectManager_CollectivePay : System.Web.UI.Page
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

    public static int payMode = 1;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack && !string.IsNullOrEmpty(BasePage.getSafeRequest(this.Page, "TrainingItemID")))
        {
            TrainingItemID = new Guid(BasePage.getSafeRequest(this.Page, "TrainingItemID"));
            payMode = Request.QueryString["Mode"].ToInt();
            if (payMode == 1)
            {

                Dic_PayMode.Items[0].Enabled = false;
                Dic_PayMode.Items[1].Selected = true;
            }
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
            this.Dic_PayMode.SelectedValue = item.PayFrom.ToString();
            //Dic_PayMode.SelectedIndex = item.PayFrom - 1;
            ctbPayMoney.Text = item.BudgetFee.ToString();
            if (item.IsIssue && payMode == 3)
            {
                Dic_PayMode.Enabled = false;
                Dic_PayMode.CssClass = "link_colorGray";
            }
        }
    }

    /// <summary>
    /// 保存
    /// </summary>
    protected void Btn_Save_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(ctbPayMoney.Text))
        {
            ETMS.Utility.JsUtility.AlertMessageBox("请设置项目的收费标准！");
            return;
        }
        else
        {
            payMode = Dic_PayMode.SelectedValue.ToInt();
            Tr_ItemLogic item = new Tr_ItemLogic();
            item.UpdateItemMoney(TrainingItemID.ToString(), ctbPayMoney.Text, payMode);
            ETMS.Utility.JsUtility.SuccessMessageBoxAndCloseWindowAndTriggerRefreshEvent("提示", "项目收费标准设置成功。");
        }
    }
}