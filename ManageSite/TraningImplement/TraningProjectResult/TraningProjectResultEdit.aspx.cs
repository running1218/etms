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

public partial class TraningImplement_TraningProjectResult_TraningProjectResultEdit : System.Web.UI.Page
{
    #region 页面参数
    /// <summary>
    /// 项目ID 
    /// </summary>
    protected Guid TrainingItemID
    {
        get
        {
            if (ViewState["TrainingItemID"] == null)
                ViewState["TrainingItemID"] =Guid.Empty;
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
            Lbl_ItemCode.Text = item.ItemCode;
            Lbl_ItemName.Text = item.ItemName;
            dlblSpecialtyType.FieldIDValue = item.SpecialtyTypeCode;
            dlblProjectType.FieldIDValue = item.ItemStatus.ToString();
            lblItemDate.Text = item.ItemBeginTime.ToDate() + " 至 " + item.ItemEndTime.ToDate();
                       
            switch(item.ItemStatus){
                case 20://状态为审核通过时
                    if (item.IsIssue)//发布时
                    {
                        ddl_ItemEndMode.Items.Add(new ListItem("正常结束", "1"));
                        ddl_ItemEndMode.Items.Add(new ListItem("异常结束", "2"));
                    }
                    else
                    {
                        //未发布时  只显示审核通过结束
                        ddl_ItemEndMode.Items.Add(new ListItem("审核通过结束", "3"));
                    }
                    break;
                case 40://状态为审核不通过时  只显示审核不通过结束
                    ddl_ItemEndMode.Items.Add(new ListItem("审核不通过结束", "4"));
                    break;
            }
        }
    }

    /// <summary>
    /// 归档
    /// </summary>
    protected void btnFile_Click(object sender, EventArgs e)
    {
        try
        {
            Tr_ItemLogic logic = new Tr_ItemLogic();
            logic.Tr_Item_Achive(TrainingItemID, ddl_ItemEndMode.Text.ToInt(), txtItemEndReMark.Text, ETMS.AppContext.UserContext.Current.RealName);
            ETMS.Utility.JsUtility.SuccessMessageBoxAndCloseWindowAndTriggerRefreshEvent("项目归档成功！");
        }
        catch (ETMS.AppContext.BusinessException bizEx)
        {
            ETMS.Utility.JsUtility.AlertMessageBox(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
            return;
        }
    }
}