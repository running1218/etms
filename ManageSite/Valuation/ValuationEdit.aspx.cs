using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using ETMS.Utility;
using ETMS.Components.Evaluation.API.Entity;
using ETMS.Components.Evaluation.Implement.BLL;

public partial class Valuation_ValuationEdit : System.Web.UI.Page
{
    #region 页面参数

    public Guid PlateID
    {
        get
        {
            if (ViewState["PlateID"] == null)
            {
                ViewState["PlateID"] = new Guid();
            }
            return (Guid)ViewState["PlateID"];
        }
        set
        {
            ViewState["PlateID"] = value;
        }
    }

    #endregion

    private static readonly Evaluation_PlateLogic evaluationLogic = new Evaluation_PlateLogic();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            PlateID = Request.QueryString["PlateID"].ToGuid();
            InitControl();
        }
    }

    /// <summary>
    /// 初始化控件值
    /// </summary>
    private void InitControl()
    {
        Evaluation_Plate evaluation = new Evaluation_Plate();
        evaluation = evaluationLogic.GetById(PlateID);

        ltlPlateName.Text = evaluation.PlateName;
        lblObjectTypeName.FieldIDValue = evaluation.ObjectTypeID.ToString();
        rblIsUse.SelectedValue = evaluation.IsUse.ToString();
        rblIsViewResult.SelectedValue = evaluation.IsViewResult.ToString().ToLower() ;
        txtItems.Text = getEvaluationItems(PlateID);
        
        cbxIsRepeat.Checked = evaluation.MaxRepeat == 0 ? false : true;
        txtMaxRepeat.Visible = cbxIsRepeat.Checked;
        txtMaxRepeat.Text = evaluation.MaxRepeat == 0 ? "" : evaluation.MaxRepeat.ToString();
        
        cbxIsOther.Checked = evaluation.IsOther;
        txtOtherTitle.Visible = cbxIsOther.Checked;
        txtOtherTitle.Text = evaluation.IsOther == true ? evaluation.OtherTitle : "";
    }

    protected string getEvaluationItems(Guid plateID)
    {
        Evaluation_ItemLogic itemLogic = new Evaluation_ItemLogic();
        List<Evaluation_Item> items = itemLogic.GetByPlate(PlateID);
        string allItems = string.Empty;
        for (int i = 0; i < items.Count; i++)
        {
            allItems += (allItems == string.Empty ? "" : ";") + items[i].ItemName;
        }
        return allItems;

    }

    protected void lbtnSave_Click(object sender, EventArgs e)
    {

        ETMS.Utility.JsUtility.SuccessMessageBoxAndCloseWindow("评价量信息保存成功，点击“确定”后，重新刷新当前页数据！");
    }

    protected void cbxIsRepeat_CheckedChanged(object sender, EventArgs e)
    {
        txtMaxRepeat.Visible = cbxIsRepeat.Checked;
    }
    protected void cbxIsOther_CheckedChanged(object sender, EventArgs e)
    {
        txtOtherTitle.Visible = cbxIsOther.Checked;
    }
}