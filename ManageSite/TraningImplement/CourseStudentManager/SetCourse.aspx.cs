using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Controls;
using ETMS.Components.Basic.Implement.BLL.TrainingItem;
using ETMS.Components.Basic.API.Entity.TrainingItem;
using ETMS.Utility;
using ETMS.Components.Basic.Implement.BLL.Security;

public partial class TraningImplement_CourseStudentManager_SetCourse : System.Web.UI.Page
{
    #region 页面参数

    /// <summary>
    /// 项目ID
    /// </summary>
    public Guid TrainingItemID
    {
        get
        {
            if (ViewState["TrainingItemID"] == null)
                ViewState["TrainingItemID"] = Guid.Empty;

            return ViewState["TrainingItemID"].ToGuid();
        }
        set
        {
            ViewState["TrainingItemID"] = value;
        }
    }

    /// <summary>
    /// 用户ID
    /// </summary>
    public int UserID
    {
        get
        {
            if (ViewState["UserID"] == null)
                ViewState["UserID"] =0;

            return ViewState["UserID"].ToInt();
        }
        set
        {
            ViewState["UserID"] = value;
        }
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (!string.IsNullOrEmpty(BasePage.getSafeRequest(this.Page, "TrainingItemID")))
                TrainingItemID = BasePage.getSafeRequest(this.Page, "TrainingItemID").ToGuid();
            if (!string.IsNullOrEmpty(BasePage.getSafeRequest(this.Page, "UserID")))
                UserID = BasePage.getSafeRequest(this.Page, "UserID").ToInt();
            
            //单机构版本隐藏机构查询条件
            if (ETMS.Product.ProductDefine.IsSingleOrganizationVersion)
            {
                this.trOrg.Visible = false;
            }
            bind();

            CourseSelect1.TrainingItemID = TrainingItemID;
            CourseSelect1.UserID = UserID;
            CourseNoSelect2.TrainingItemID = TrainingItemID;
            CourseNoSelect2.UserID = UserID;
        }
        lbtnReturn.PostBackUrl = "StudentList.aspx";
    }

    /// <summary>
    /// 邦定基本信息
    /// </summary>
    private void bind()
    {
        Tr_Item item = new Tr_ItemLogic().GetById(TrainingItemID);
        if (item != null)
        {
            lblItemCode.Text = item.ItemCode;
            lblItemName.Text = item.ItemName;

            ETMS.Components.Basic.API.Entity.Security.User user = new UserLogic().GetUserByID(UserID);

            lblDepartmentID.FieldIDValue = user.DepartmentID.ToString();
            lblOrganization.FieldIDValue = user.OrganizationID.ToString();
            lblRealName.Text = user.RealName;
            lblPost.FieldIDValue = new Site_StudentLogic().GetById(UserID).PostID.ToString();
        }
    }
}