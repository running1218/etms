using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Utility;
using ETMS.Components.Evaluation.Implement.BLL;
using ETMS.Components.Evaluation.API.Entity;
using ETMS.Components.Basic.Implement.BLL.Security;
using System.Data;

public partial class Valuation_EvaluationApprove : System.Web.UI.Page
{
    private Guid ResultSubID
    {
        get { return ViewState["ResultSubID"].ToGuid(); }
        set { ViewState["ResultSubID"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        ResultSubID = Request.QueryString["ResultSubID"].ToGuid();
        if (!IsPostBack) {
            Bind();
        }
    }

    private void Bind()
    {
        Evaluation_PlateResult plateResult = new Evaluation_PlateResultLogic().GetById(ResultSubID);
        lab_RealName.Text = new UserLogic().GetUserByID(plateResult.UserID).RealName;
        lab_CreateTime.Text = plateResult.CreateTime.ToDate();
        lab_EvaluationContent.Text = plateResult.EvaluationContent;
        rblApproveStatus.SelectedValue ="1";
        lab_Result.Text = getScore(plateResult.UserID, "cf3d623b-f9b7-417a-bf76-377bb70a3935".ToGuid(), plateResult.ObjectID);
        foreach (ListItem item in rblApproveStatus.Items) {
            if (item.Value == "0") {
                rblApproveStatus.Items.Remove(item);
                return;
            }
        }

    }
    protected string getScore(int userID,Guid plateID,string objectID)
    {
        string str = "";
        DataTable dt = new Evaluation_ItemResultLogic().GetResultUserScore(objectID, plateID, userID);
            if (null != dt && dt.Rows.Count > 0)
            {
                switch (dt.Rows[0]["Score"].ToString())
                {
                    case "1":
                        str = "差评";
                        break;
                    case "2":
                        str = "中评";
                        break;
                    case "3":
                        str = "好评";
                        break;
                }
            }
        return str;
    }

    /// <summary>
    /// 保存
    /// </summary>
    protected void Btn_Save_Click(object sender, EventArgs e)
    {
        try
        {
            Evaluation_PlateResultLogic plateResultLogic=new Evaluation_PlateResultLogic();
            Evaluation_PlateResult plateResult = plateResultLogic.GetById(ResultSubID);
            plateResult.ApproveStatus = rblApproveStatus.SelectedValue.ToInt();
            plateResult.ApproveTime = DateTime.Now;
            plateResult.ApproveUserID = ETMS.AppContext.UserContext.Current.UserID;
            plateResultLogic.Update(plateResult);

            ETMS.Utility.JsUtility.SuccessMessageBox("提示", "点评审批成功！", "window.parent.triggerRefreshEvent");
        }
        catch (ETMS.AppContext.BusinessException bizEx)
        {
            ETMS.Utility.JsUtility.AlertMessageBox(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
            return;
        }
    }
}