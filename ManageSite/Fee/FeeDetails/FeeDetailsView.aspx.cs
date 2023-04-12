using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Components.Fee.API.Entity;
using ETMS.Components.Fee.Implement.BLL;
using ETMS.Utility;
using ETMS.Components.Basic.Implement.BLL.TrainingItem;

public partial class Fee_FeeDetails_FeeDetailsView : System.Web.UI.Page
{
    #region 页面条件参数存放

    /// <summary>
    /// 费用流水ID
    /// </summary>
    public Guid FeeCostDetailID
    {
        get
        {
            if (ViewState["FeeCostDetailID"] == null)
            {
                ViewState["FeeCostDetailID"] = Guid.Empty;
            }
            return (Guid)ViewState["FeeCostDetailID"];
        }
        set
        {
            ViewState["FeeCostDetailID"] = value;
        }
    }

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FeeCostDetailID = Request.QueryString["FeeCostDetailID"].ToGuid();
            InitControl();
        }
    }

    private void InitControl()
    { 
        Fee_FeeCostDetailsLogic feeCostDetailsLogic = new Fee_FeeCostDetailsLogic();
        Fee_FeeCostDetails feeCostDetails = new Fee_FeeCostDetails();

        Tr_ItemLogic itemLogic = new Tr_ItemLogic();

        feeCostDetails = feeCostDetailsLogic.GetById(FeeCostDetailID);

        ltlFeeCostDetailNo.Text = feeCostDetails.FeeCostDetailNo;
        ltlFeeName.Text = feeCostDetails.FeeCostDetailName;
        ltlItemName.Text = itemLogic.GetById(feeCostDetails.TrainingItemID).ItemName;
        ltlAmount.Text = feeCostDetails.Amount.ToString();
        ltlHandler.Text = feeCostDetails.Handler;
        ltlCostDate.Text = feeCostDetails.CostDate.ToDate();
        ltlPurpose.Text = feeCostDetails.Purpose;
        ltlPRNo.Text = feeCostDetails.PRNo;
        ltlIsGetInvoice.FieldIDValue = feeCostDetails.IsGetInvoice.ToString();
        ltlReimbursementDate.Text = feeCostDetails.ReimbursementDate.ToDate();
        ltlRemark.Text = feeCostDetails.Remark;
    }
}