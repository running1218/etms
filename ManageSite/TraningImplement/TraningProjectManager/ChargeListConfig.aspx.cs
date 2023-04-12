using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Components.Basic.Implement.BLL.TrainingItem;
using ETMS.Controls;
using System.Data;
using ETMS.Utility;

public partial class TraningImplement_TraningProjectManager_ChargeListConfig : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        PageSet1.pageInit(this.CustomGridView1, PageDataSource);

        if (!Page.IsPostBack)
        {
            this.PageSet1.QueryChange();
        }
    }

    private System.Collections.IList PageDataSource(int pageIndex, int pageSize, out int totalRecordCount)
    {
        string Crieria = string.Format(" {0} AND OrgID={1} AND ItemStatus in (10,20) ", BasePage.getQueryConditionFromQueryControlList(tableQueryControlList), ETMS.AppContext.UserContext.Current.OrganizationID);

        Tr_ItemLogic item = new Tr_ItemLogic();
        DataTable dt = item.GetPagedList(pageIndex, pageSize, " CreateTime desc", Crieria, out totalRecordCount);
        PageDataSourceProvider pageDataSource = new PageDataSourceProvider(dt, pageIndex, pageSize);
        return pageDataSource.PageDataSource;
    }

    /// <summary>
    /// 查询
    /// </summary>
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        this.PageSet1.QueryChange();
    }

    /// <summary>
    /// 行绑定
    /// </summary>
    protected void CustomGridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string itemID = CustomGridView1.DataKeys[e.Row.RowIndex].Value.ToString();
            #region 获取控件


            LinkButton Lbtn_Edit = (LinkButton)e.Row.FindControl("Lbtn_Edit");
            Lbtn_Edit = Lbtn_Edit == null ? new LinkButton() : Lbtn_Edit;

            DictionaryLabel lalIsPublish = (DictionaryLabel)e.Row.FindControl("DictionaryReleaseState");
            DictionaryLabel lblMethod = (DictionaryLabel)e.Row.FindControl("dicEnrollmentMethod");
            string methodVal = lblMethod.FieldIDValue;
            if (methodVal == "1" || methodVal == "3")
            {
                Lbtn_Edit.Enabled = true;
                Lbtn_Edit.Attributes["onclick"] = string.Format("javascript:showWindow('培训收费标准设置','{0}','500','350');javascript:return false;", this.ActionHref(string.Format("CollectivePay.aspx?TrainingItemID={0}&Mode={1}", itemID, methodVal)));
            }
            else
            {
                Lbtn_Edit.Enabled = true;
                Lbtn_Edit.Attributes["onclick"] = string.Format("javascript:showWindow('项目课程收费标准设置','{0}');javascript:return false;", this.ActionHref(string.Format("ChargeCourseConfig.aspx?TrainingItemID={0}&Mode={1}", itemID, methodVal)));
            }
            #endregion
        }
    }

    ///// <summary>
    ///// 行操作
    ///// </summary>
    //protected void CustomGridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    //{
    //    int index = Convert.ToInt32(e.CommandArgument);
    //    string itemID = CustomGridView1.DataKeys[index].Value.ToString();
    //    GridViewRow row = CustomGridView1.Rows[index];
    //    if (e.CommandName == "A")
    //    {
    //        DictionaryLabel lblMethod = (DictionaryLabel)row.FindControl("dicEnrollmentMethod");
    //        string methodVal = lblMethod.FieldIDValue;
    //        if (methodVal == "1" || methodVal == "3")
    //        {
    //            CustomTextBox ctbPayMoney = (CustomTextBox)row.FindControl("ctbPayMoney");
    //            ctbPayMoney.Visible = true;
    //            Literal litPayMoney = (Literal)row.FindControl("LitPayMoney");
    //            litPayMoney.Visible = false;
    //            CustomLinkButton btn = row.FindControl("lbtnSave") as CustomLinkButton;
    //            btn.Visible = true;
    //            btn = row.FindControl("lbtnCannel") as CustomLinkButton;
    //            btn.Visible = true;
    //            btn = row.FindControl("lbtnConfig") as CustomLinkButton;
    //            btn.Visible = false;
    //        }
    //        else
    //        {
    //            CustomLinkButton btn = row.FindControl("lbtnConfig") as CustomLinkButton;

    //            btn.PostBackUrl = this.ActionHref("ChargeCourseConfig.aspx?TrainingItemID=" + itemID);
    //        }
    //    }
    //    else
    //    {
    //        CustomTextBox ctbPayMoney = (CustomTextBox)row.FindControl("ctbPayMoney");
    //        ctbPayMoney.Visible = false;
    //        Literal litPayMoney = (Literal)row.FindControl("LitPayMoney");
    //        litPayMoney.Visible = true;
    //        if (e.CommandName == "B")
    //        {
    //            litPayMoney.Text = ctbPayMoney.Text;
    //            Tr_ItemLogic item = new Tr_ItemLogic();
    //            item.UpdateItemMoney(itemID, litPayMoney.Text);

    //        }
    //        CustomLinkButton btn = row.FindControl("lbtnSave") as CustomLinkButton;
    //        btn.Visible = false;
    //        btn = row.FindControl("lbtnCannel") as CustomLinkButton;
    //        btn.Visible = false;
    //        btn = row.FindControl("lbtnConfig") as CustomLinkButton;
    //        btn.Visible = true;
    //    }
    //}
}