using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Components.Basic.Implement.BLL;
using ETMS.Controls;
using ETMS.WebApp;
using System.Data;
using ETMS.Components.Basic.Implement.BLL.TrainingPlan;
using ETMS.Utility;

public partial class TraningPlan_TraningPlanResult_TraningPlanResultList : System.Web.UI.Page
{
    #region 页面参数

    /// <summary>
    /// 查询条件 
    /// </summary>
    protected string Crieria
    {
        get
        {
            if (ViewState["Crieria"] == null)
                ViewState["Crieria"] = "";
            return (string)ViewState["Crieria"];
        }
        set
        {
            ViewState["Crieria"] = value;
        }
    }

    /// <summary>
    /// 排序条件
    /// </summary>
    protected string SortExpression
    {
        get
        {
            if (ViewState["SortExpression"] == null)
            {
                ViewState["SortExpression"] = " CreateTime DESC";
            }
            return (string)ViewState["SortExpression"];
        }
        set
        {
            ViewState["SortExpression"] = value;
        }
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        PageSet1.pageInit(this.CustomGridView1, PageDataSource);

        if (!Page.IsPostBack)
        {
            this.PageSet1.QueryChange();
        }
    }

    /// <summary>
    /// 查询
    /// </summary>
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        this.PageSet1.QueryChange();
    }

    private System.Collections.IList PageDataSource(int pageIndex, int pageSize, out int totalRecordCount)
    {
        Crieria = string.Format(" {0} AND PlanStatus IN (20,40,90) ", BasePage.getQueryConditionFromQueryControlList(tableQueryControlList));

        Tr_PlanLogic planLogic = new Tr_PlanLogic();

        DataTable dt = planLogic.GetPlanListByOrgID(ETMS.AppContext.UserContext.Current.OrganizationID, pageIndex, pageSize, SortExpression, Crieria, out totalRecordCount);

        PageDataSourceProvider pageDataSource = new PageDataSourceProvider(dt, pageIndex, pageSize);
        return pageDataSource.PageDataSource;
    }

    /// <summary>
    /// 行邦定
    /// </summary>
    protected void CustomGridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string PlanID = CustomGridView1.DataKeys[e.Row.RowIndex].Value.ToString();
            #region 获取控件
            HiddenField Hf_PlanStatus = (HiddenField)e.Row.FindControl("Hf_PlanStatus");
            Hf_PlanStatus = Hf_PlanStatus == null ? new HiddenField() : Hf_PlanStatus;

            LinkButton lbtn_File = (LinkButton)e.Row.FindControl("lbtn_File");
            lbtn_File = lbtn_File == null ? new LinkButton() : lbtn_File;

            LinkButton lbtn_View = (LinkButton)e.Row.FindControl("lbtn_View");
            lbtn_View = lbtn_View == null ? new LinkButton() : lbtn_View;
            #endregion

            switch (Hf_PlanStatus.Value)
            {
                case "90":
                    lbtn_View.Visible = true;
                    lbtn_View.PostBackUrl =this.ActionHref(string.Format("TraningPlanResultView.aspx?PlanID={0}", PlanID));
                    lbtn_File.Visible = false;
                    break;
                default:
                    lbtn_View.Visible = false;
                    lbtn_File.Attributes["onclick"] = string.Format("javascript:showWindow('计划归档','{0}',650,500);javascript:return false;", this.ActionHref(string.Format("TraningPlanResultEdit.aspx?PlanID={0}", PlanID)));
                    lbtn_File.Visible = true;
                    break;
            }
        }
    }
}