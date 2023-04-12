using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.AppContext;
using ETMS.Utility;
using ETMS.Components.Fee.Implement.BLL;
using System.Data;
using ETMS.Components.Fee.API.Entity;
using ETMS.Components.Basic.Implement;
using ETMS.Components.Basic.Implement.BLL.TrainingItem;

public partial class Fee_FeeDetails_Controls_FeeDetails : System.Web.UI.UserControl
{
    #region 页面条件参数存放

    /// <summary>
    /// 操作
    /// </summary>
    public OperationAction Action
    {
        get
        {
            return (OperationAction)ViewState["Action"];
        }
        set
        {
            ViewState["Action"] = value;
        }
    }
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

    private static readonly Fee_FeeCostDetailsLogic feeCostDetailsLogic = new Fee_FeeCostDetailsLogic();
    private Fee_FeeCostDetails feeCostDetails = new Fee_FeeCostDetails();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitItemList();
            rbtnIsGetInvoice.SelectedValue = "false";
            txtAmount.Text = "0.00";
            if (Action == OperationAction.Add)
            {
                trFeeCostDetailNo.Visible = false;
            }
            else if (Action == OperationAction.Edit)
            {
                InitControl();
            }
        }
    }

    private void InitItemList()
    {
        Tr_ItemLogic itemLogic = new Tr_ItemLogic();
        ddlItemID.DataSource = itemLogic.getTrainingItemNoAchiveListByOrgID(ETMS.AppContext.UserContext.Current.OrganizationID);
        ddlItemID.DataTextField = "ItemName";
        ddlItemID.DataValueField = "TrainingItemID";
        ddlItemID.DataBind();

    }

    private void InitControl()
    {
        feeCostDetails = feeCostDetailsLogic.GetById(FeeCostDetailID);

        ddlItemID.SelectedValue = feeCostDetails.TrainingItemID.ToString();
        ltlFeeCostDetailNo.Text = feeCostDetails.FeeCostDetailNo;
        txtFeeName.Text = feeCostDetails.FeeCostDetailName;
        txtCostDate.Text = feeCostDetails.CostDate.ToDate();
        txtAmount.Text = feeCostDetails.Amount.ToString();
        txtAmount.Attributes.Add("t_value", txtAmount.Text);
        txtPurpose.Text = feeCostDetails.Purpose;
        txtPRNo.Text = feeCostDetails.PRNo;
        rbtnIsGetInvoice.SelectedValue = feeCostDetails.IsGetInvoice.ToString().ToLower();
        txtReimbursementDate.Text = feeCostDetails.ReimbursementDate.ToDate();
        txtHandler.Text = feeCostDetails.Handler;
        txtRemark.Text = feeCostDetails.Remark;
    }

    /// <summary>
    /// 给实体赋值
    /// </summary>
    private void InitialEntity()
    {
        if (Action == OperationAction.Add)
        {
            //流 水 号：	系统自动生成，规则：FY+机构编码+YYMM+四位流水号
            string Crieria = string.Format(" AND a.TrainingItemID in ( select TrainingItemID from Tr_Item where OrgID={0})", UserContext.Current.OrganizationID);

            int totalRecordCount = 0;
            DataTable dt = feeCostDetailsLogic.GetPagedList(1, 1, "", Crieria, out totalRecordCount);

            PublicFacade publicFacde = new PublicFacade();
            string strNum = "FY" + publicFacde.GetOrgCodeByID(ETMS.AppContext.UserContext.Current.OrganizationID) + DateTime.Now.ToString("yyMM") + string.Format("{0:D4}", (totalRecordCount + 1));

            //新增实体
            feeCostDetails = new Fee_FeeCostDetails()
            {
                FeeCostDetailID = Guid.Empty,
                FeeCostDetailNo = strNum,
                CreateUser = UserContext.Current.RealName,
                CreateUserID = UserContext.Current.UserID,
                CreateTime = DateTime.Now
            };
        }
        else if (Action == OperationAction.Edit)
        {
            feeCostDetails = feeCostDetailsLogic.GetById(FeeCostDetailID);
            feeCostDetails.ModifyUser = ETMS.AppContext.UserContext.Current.RealName.ToString();
            feeCostDetails.ModifyTime = DateTime.Now;
        }

        feeCostDetails.TrainingItemID = ddlItemID.SelectedValue.ToGuid();
        feeCostDetails.FeeCostDetailName = txtFeeName.Text.Trim();
        feeCostDetails.CostDate = txtCostDate.Text.Trim().ToDateTime();
        feeCostDetails.Amount = txtAmount.Text.Trim().ToDecimal();
        feeCostDetails.Purpose = txtPurpose.Text.Trim();
        feeCostDetails.PRNo = txtPRNo.Text.Trim();
        feeCostDetails.IsGetInvoice = Boolean.Parse(rbtnIsGetInvoice.SelectedValue);
        feeCostDetails.ReimbursementDate = txtReimbursementDate.Text.Trim().ToDateTime();
        feeCostDetails.Handler = txtHandler.Text.Trim();
        feeCostDetails.Remark = txtRemark.Text.Trim();
        feeCostDetails.DelFlag = false;
    }

    protected void lbnSave_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtFeeName.Text.Trim()))
        {
            ETMS.Utility.JsUtility.FailedMessageBox("请填写费用流水名称！");
            return;
        }
        try
        {
            InitialEntity();
            feeCostDetailsLogic.Save(feeCostDetails);
            
            ETMS.Utility.JsUtility.SuccessMessageBoxAndCloseWindowAndTriggerSearchEvent("费用流水信息保存成功！");
        }
        catch (ETMS.AppContext.BusinessException bizEx)
        {
            ETMS.Utility.JsUtility.AlertMessageBox(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
        }
    }
}