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
using ETMS.Components.Basic.Implement.BLL.TrainingItem;
using ETMS.Components.Basic.API.Entity.TrainingItem;
using ETMS.Utility;

public partial class TraningImplement_TraningProjectAudit_TraningProjectAudit : BasePage
{
    #region 页面参数
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
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack && !string.IsNullOrEmpty(BasePage.getSafeRequest(this.Page, "TrainingItemID")))
        {
            TrainingItemID = new Guid(BasePage.getSafeRequest(this.Page, "TrainingItemID"));
        }
        TraningProjectView1.TrainingItemID = TrainingItemID;
        TraningProjectView1.IsAuditVisible = false;
        TraningProjectView1.IsItemEndModeVisible = false;
        TraningCourseListView3.TrainingItemID = TrainingItemID;
    }

    /// <summary>
    /// 审核通过
    /// </summary>
    protected void btnAgree_Click(object sender, EventArgs e)
    {
        try
        {
            if (checkStatus())
            {
                return;
            }
            Tr_ItemLogic ItemLogic = new Tr_ItemLogic();
            ItemLogic.Tr_Item_Audit(TrainingItemID, 20, ETMS.AppContext.UserContext.Current.RealName, labOpinion.Text.Trim());
            ETMS.Utility.JsUtility.SuccessMessageBox("提示", "项目审核成功！", "function(){window.location = '" + this.ActionHref("TraningProjectList.aspx") + "'}");
        }
        catch (ETMS.AppContext.BusinessException bizEx)
        {
            ETMS.Utility.JsUtility.AlertMessageBox(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
            return;
        }
    }
    
    /// <summary>
    /// 审核不通过
    /// </summary>
    protected void btnDeny_Click(object sender, EventArgs e)
    {
        try
        {
            if (checkStatus()) {
                return;
            }
            Tr_ItemLogic ItemLogic = new Tr_ItemLogic();
            ItemLogic.Tr_Item_Audit(TrainingItemID, 40, ETMS.AppContext.UserContext.Current.RealName, labOpinion.Text.Trim());
            ETMS.Utility.JsUtility.SuccessMessageBox("提示", "操作成功！", "function(){window.location = '" + this.ActionHref("TraningProjectList.aspx") + "'}");
        }
        catch (ETMS.AppContext.BusinessException bizEx)
        {
            ETMS.WebApp.Manage.Extention.ShowScriptManagerMessage(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
            return;
        }
    }

    /// <summary>
    /// 验证项目状态是否被修改
    /// </summary>
    /// <returns></returns>
    private bool checkStatus()
    {
        bool res = false;
        Tr_ItemLogic itemLogic = new Tr_ItemLogic();
        Tr_Item item = itemLogic.GetById(TrainingItemID);
        if (item.ItemStatus != 10)
        {
            res = true;
            ETMS.WebApp.Manage.Extention.ShowScriptManagerMessage( "当前项目状态发生变化，不能审核！");
        }
        return res;
    }
}