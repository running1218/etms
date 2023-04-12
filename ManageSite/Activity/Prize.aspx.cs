using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Utility;
using ETMS.Activity.Implement.BLL;
using ETMS.AppContext;

public partial class Activity_Prize : System.Web.UI.Page
{
    private Guid ProductionID { get { return Request.QueryString["ProductionID"].ToGuid(); } }
    private static readonly ProductionLogic logic = new ProductionLogic();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindData();
        }
    }

    private void BindData()
    {
        var entity = logic.GetPrizeByProductionID(ProductionID);
        if (entity != null)
        {
            lblSiginNo.Text = entity.SiginupNo;
            lblName.Text = entity.Name;
            lblProductionName.Text = entity.ProductName;
            lblScore.Text = entity.Score.ToString();
        }
    }

    protected void Btn_Save_Click(object sender, EventArgs e)
    {
        try
        {
            logic.SetPrize(ProductionID, ddlPrize.SelectedValue.ToInt(), UserContext.Current.UserID);
            JsUtility.SuccessMessageBoxAndCloseWindowAndTriggerRefreshEvent("保存成功！");
        }
        catch (BusinessException bizEx)
        {
            JsUtility.AlertMessageBox(ExceptionUtility.GetTranslateExceptionMessage(bizEx));
        }
    }
}