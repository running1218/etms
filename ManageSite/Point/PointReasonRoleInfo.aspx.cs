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

public partial class Point_PointReasonRoleInfo : ETMS.Controls.BasePage
{
    private static Point_Student_PointReasonRoleLogic pointReasonRoleLogic = new Point_Student_PointReasonRoleLogic();
    private static Dic_PointReasonTypeLogic pointReasonTypeLogic = new Dic_PointReasonTypeLogic();

    /// <summary>
    /// 学员积分原因规则ID
    /// </summary>
    public Guid StudentPointReasonRoleID
    {
        get { return Request.QueryString["StudentPointReasonRoleID"].ToGuid(); }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Initial();
        }
    }
    /// <summary>
    /// 修改时控件绑定
    /// </summary>
    private void Initial()
    {
        this.rbnStatus.SelectedIndex = 0;

        int total = 0;
        DataTable dt = pointReasonTypeLogic.GetPagedList(1, int.MaxValue - 1, string.Empty, string.Format(" and OrgID={0}", ETMS.AppContext.UserContext.Current.OrganizationID), out total);
        this.ddlPointReasonTypeID.DataSource = dt.DefaultView;
        this.ddlPointReasonTypeID.DataTextField = "PointReasonTypeName";
        this.ddlPointReasonTypeID.DataValueField = "PointReasonTypeID";
        this.ddlPointReasonTypeID.DataBind();
        this.ddlPointReasonTypeID.Items.Insert(0, new ListItem("请选择", ""));

        if (StudentPointReasonRoleID != Guid.Empty)//修改
        {
            Point_Student_PointReasonRole pointReasonRole = pointReasonRoleLogic.GetById(StudentPointReasonRoleID);
            this.txtGivePoints.Text = pointReasonRole.GivePoints.ToString();
            this.txtPointReason.Text = pointReasonRole.PointReason.ToString();
            this.ddlPointReasonTypeID.SelectedValue = pointReasonRole.PointReasonTypeID.ToString();
            this.rbnStatus.SelectedValue = pointReasonRole.IsUse.ToString();

        }      
    }
    /// <summary>
    /// 保存
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnUpdate_Click(object sender, EventArgs e)
    {        
        try
        {
            string result = string.Empty;
            if (this.txtGivePoints.Text.Trim().ToInt() == 0)
            {
                result += "积分超出了范围！";
            }
            if (!string.IsNullOrEmpty(result))
            {
                ETMS.Utility.JsUtility.AlertMessageBox(result);
                return;
            }
            if (StudentPointReasonRoleID == Guid.Empty)//新增
            {
                Point_Student_PointReasonRole pointReasonRole = new Point_Student_PointReasonRole();
                pointReasonRole.GivePoints = this.txtGivePoints.Text.Trim().ToInt();
                pointReasonRole.PointReason = this.txtPointReason.Text;
                pointReasonRole.PointReasonTypeID = this.ddlPointReasonTypeID.SelectedValue.ToInt();
                pointReasonRole.IsUse = this.rbnStatus.SelectedValue.ToInt();
                pointReasonRole.OrgID = ETMS.AppContext.UserContext.Current.OrganizationID;
                pointReasonRole.CreateTime = DateTime.Now;
                pointReasonRole.CreateUser = ETMS.AppContext.UserContext.Current.RealName;
                pointReasonRole.CreateUserID = ETMS.AppContext.UserContext.Current.UserID;
                pointReasonRoleLogic.Save(pointReasonRole , ETMS.AppContext.OperationAction.Add);
            }
            else//修改
            {
                Point_Student_PointReasonRole pointReasonRole = pointReasonRoleLogic.GetById(StudentPointReasonRoleID);
                pointReasonRole.GivePoints = this.txtGivePoints.Text.Trim().ToInt();
                pointReasonRole.PointReason = this.txtPointReason.Text;
                pointReasonRole.PointReasonTypeID = this.ddlPointReasonTypeID.SelectedValue.ToInt();
                pointReasonRole.IsUse = this.rbnStatus.SelectedValue.ToInt();
                pointReasonRole.ModifyTime = DateTime.Now;
                pointReasonRole.ModifyUser = ETMS.AppContext.UserContext.Current.RealName;
                pointReasonRoleLogic.Save(pointReasonRole, ETMS.AppContext.OperationAction.Edit);
            }
            ETMS.Utility.JsUtility.SuccessMessageBoxAndCloseWindowAndTriggerRefreshEvent("保存成功");
        }
        catch (ETMS.AppContext.BusinessException bizEx)
        {
            ETMS.WebApp.Manage.Extention.ShowScriptManagerMessage(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
            return;
        }
    }
}