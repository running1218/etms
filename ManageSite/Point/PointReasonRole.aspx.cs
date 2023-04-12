using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ETMS.Utility;
using ETMS.Controls;
using ETMS.Components.Point.Implement.BLL;
using ETMS.Components.Point.API.Entity;


public partial class Point_PointReasonRole : ETMS.Controls.BasePage
{
    private static Point_Student_PointReasonRoleLogic pointReasonRoleLogic = new Point_Student_PointReasonRoleLogic();
    private static Dic_PointReasonTypeLogic pointReasonTypeLogic = new Dic_PointReasonTypeLogic();
    protected void Page_Load(object sender, EventArgs e)
    {
        PageSet1.pageInit(this.CustomGridView1, PageDataSource);

        if (!Page.IsPostBack)
        {
            Initial();
            this.PageSet1.QueryChange();
        }
    }
    /// <summary>
    /// 控件绑定
    /// </summary>
    private void Initial()
    {
        int total=0;        
        DataTable dt = pointReasonTypeLogic.GetPagedList(1, int.MaxValue - 1, string.Empty, string.Format(" and OrgID={0}",ETMS.AppContext.UserContext.Current.OrganizationID), out total);
        this.ddlPointReasonTypeID.DataSource = dt;
        this.ddlPointReasonTypeID.DataTextField = "PointReasonTypeName";
        this.ddlPointReasonTypeID.DataValueField = "PointReasonTypeID";
        this.ddlPointReasonTypeID.DataBind();
        this.ddlPointReasonTypeID.Items.Insert(0,new ListItem("全部",""));
    }

    private System.Collections.IList PageDataSource(int pageIndex, int pageSize, out int totalRecordCount)
    {
        string crieria = string.Empty;//BasePage.getQueryConditionFromQueryControlList(tableQueryControlList);
        if (!string.IsNullOrEmpty(txt_PointReason.Text.Trim()))
            crieria += string.Format(" and PointReason like '%{0}%'", getSafeSQLValue(txt_PointReason.Text.Trim()));
        if (!string.IsNullOrEmpty(this.txtMinNum.Text.Trim())&&!string.IsNullOrEmpty(this.txtMaxNum.Text.Trim()))
        {
            crieria += string.Format(" and GivePoints>={0} and GivePoints<={1}", txtMinNum.Text.Trim().ToInt(), txtMaxNum.Text.Trim().ToInt());
        }
        else if (!string.IsNullOrEmpty(this.txtMinNum.Text.Trim()))
        {
            crieria += string.Format(" and GivePoints>={0}", txtMinNum.Text.Trim().ToInt());
        }
        else if (!string.IsNullOrEmpty(this.txtMaxNum.Text.Trim()))
        {
            crieria += string.Format(" and GivePoints<={0}", txtMaxNum.Text.Trim().ToInt());
        }
        if (!string.IsNullOrEmpty(this.ddl_IsUse.SelectedValue) && this.ddl_IsUse.SelectedValue.ToInt() != -1)
        {
            crieria += string.Format(" and IsUse={0}",this.ddl_IsUse.SelectedValue.ToInt());
        }
        if (!string.IsNullOrEmpty(this.ddlPointReasonTypeID.SelectedValue) && this.ddlPointReasonTypeID.SelectedValue != "-1")
        {
            crieria += string.Format(" and PointReasonTypeID={0}", ddlPointReasonTypeID.SelectedValue.ToInt());
        }
        crieria += string.Format(" and OrgID={0}", ETMS.AppContext.UserContext.Current.OrganizationID);
        DataTable dt = pointReasonRoleLogic.GetPagedList(pageIndex, pageSize, " PointReason", crieria, out totalRecordCount);
        PageDataSourceProvider pageDataSource = new PageDataSourceProvider(dt, pageIndex, pageSize);
        return pageDataSource.PageDataSource;
    }
    protected void CustomGridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow && !CustomGridView1.IsEmpty)
        {
            DataRowView drv=e.Row.DataItem as DataRowView;
            Label lblPointReasonTypeID = e.Row.FindControl("lblPointReasonTypeID") as Label;
            lblPointReasonTypeID.Text = pointReasonTypeLogic.GetById(drv["PointReasonTypeID"].ToInt()).PointReasonTypeName;
        }       
    }

    /// <summary>
    /// 行操作
    /// </summary>
    protected void CustomGridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            //编辑
            if (e.CommandName == "del")
            {

                pointReasonRoleLogic.doRemove(e.CommandArgument.ToGuid());
                ETMS.Utility.JsUtility.SuccessMessageBox("删除成功！");
                this.PageSet1.DataBind();

            }
            else if (e.CommandName == "isuse")
            {
                Point_Student_PointReasonRole pointReasonRole = pointReasonRoleLogic.GetById(e.CommandArgument.ToGuid());
                pointReasonRole.IsUse = pointReasonRole.IsUse == 1 ? 0 : 1;
                pointReasonRoleLogic.Save(pointReasonRole, ETMS.AppContext.OperationAction.Edit);
                ETMS.Utility.JsUtility.SuccessMessageBox("设置成功！");
                this.PageSet1.DataBind();
            }
        }
        catch (ETMS.AppContext.BusinessException bizEx)
        {
            ETMS.WebApp.Manage.Extention.ShowScriptManagerMessage(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
            return;
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(this.txtMinNum.Text.Trim()) && !string.IsNullOrEmpty(this.txtMaxNum.Text.Trim()))
        {
            string strRegex = @"^[+-]?[0-9]+$";//,^-?[1-9]\d*$
            System.Text.RegularExpressions.Regex rg = new System.Text.RegularExpressions.Regex(strRegex);
            string checkText = string.Empty;
            if (!string.IsNullOrEmpty(txtMinNum.Text.Trim()))
            {
                if (!rg.IsMatch(txtMinNum.Text.Trim()))
                {
                    checkText += "积分值起始数格式错误!" + "  ";
                }
            }
            if (!string.IsNullOrEmpty(txtMaxNum.Text.Trim()))
            {
                if (!rg.IsMatch(txtMaxNum.Text.Trim()))
                {
                    checkText += "积分值截止数格式错误!" + "  ";
                }
            }
            if (!string.IsNullOrEmpty(checkText))
            {
                checkText += "必须为输入整数";
                ETMS.WebApp.Manage.Extention.AlertMessageBox(checkText);
                return;
            }
          
            if (!string.IsNullOrEmpty(txtMaxNum.Text.Trim()) && !string.IsNullOrEmpty(txtMinNum.Text.Trim()))
            {
                if (txtMinNum.Text.Trim().ToInt() > txtMaxNum.Text.Trim().ToInt())
                {
                    ETMS.WebApp.Manage.Extention.AlertMessageBox("积分值起始数超过截止数！");
                    return;
                }
            }
        }
        this.PageSet1.QueryChange();
    }
}