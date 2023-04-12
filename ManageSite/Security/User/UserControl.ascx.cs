using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using ETMS.Controls;
using ETMS.Components.Basic.API.Entity.Security;
using ETMS.Components.Basic.Implement.BLL.Security;
public partial class Admin_Site_User_UserControl : System.Web.UI.UserControl, IMuliViewControl
{
    private static bool IsIncludeDataTimeField = false;
    private static UserLogic Logic = new UserLogic();
    private void DateTimeControlSetting()
    {
        TextBox[] txtBeginTimes = new TextBox[0];//{};
        foreach (TextBox txtBeginTime in txtBeginTimes)
        {
            txtBeginTime.Attributes.Add("readonly", "");
            txtBeginTime.Attributes.Add("onfocus", "new WdatePicker(this,'%Y-%M-%D',true)");
            txtBeginTime.Attributes.Add("class", "Wdate");
        }
        string ScriptFormat = "  <script language=\"javascript\" type=\"text/javascript\" src=\"{0}/DateControl/WdatePicker.js\"></script>";
        Page.RegisterClientScriptBlock("datetimejs", string.Format(ScriptFormat, ETMS.Utility.WebUtility.AppPath));
    }
    #region IMuliViewControl 成员
    private object m_DomainModel;
    public Object DomainModel
    {
        get
        {
            return this.Views.DomainModel;
        }
        set
        {
            m_DomainModel = value;
        }
    }

    public ViewMode ControlMode
    {
        get
        {
            return this.Views.ControlMode;
        }
    }

    #region Manage View
    public void BindFromData_Manage(object domainModel)
    {

    }
    #endregion

    #region Edit_View
    public void BindFromData_Edit(object domainModel)
    {
        if (IsIncludeDataTimeField)
            DateTimeControlSetting();

        if (!this.Page.IsPostBack)
        {
            User entity = (User)domainModel;

            if (entity.UserID == 0)//注意:仅添加模式不需要
                return;

            this.txtLoginName.Text = entity.LoginName;
            this.txtLoginName.Enabled = false;
            this.txtRealName.Text = entity.RealName;
            this.txtEmail.Text = entity.Email;
            this.txtTelphone.Text = entity.Telphone;
            this.txtOfficeTelphone.Text = entity.OfficeTelphone;
            this.txtMobilePhone.Text = entity.MobilePhone;
            this.txtDescription.Text = entity.Description;
            this.rbStatus.SelectedValue = entity.Status.ToString();
        }
    }
    #endregion

    #region Browse_View
    public void BindFromData_Browse(object domainModel)
    {

        User entity = (User)domainModel;

        this.lblUserID.Text = entity.UserID.ToString();
        this.lblLoginName.Text = entity.LoginName;

        this.lblRealName.Text = entity.RealName;
        this.lblEmail.Text = entity.Email;
        this.lblHomeTelphone.Text = entity.Telphone;
        this.lblTelphone.Text = entity.OfficeTelphone;
        this.lblMobilePhone.Text = entity.MobilePhone;
        this.lblDescription.Text = entity.Description;
        this.lblStatus.Text = entity.Status == 1 ? "启用" : "停用";
        this.lblCreateTime.Text = entity.CreateTime.ToString();
        this.lblCreator.Text = entity.Creator;

    }
    #endregion

    #region List_View
    public void BindFromData_List(object domainModel)
    {

    }
    #endregion

    #region Handler

    public object UnBindFromData(object domainModel)
    {
        if (this.ControlMode == ViewMode.Edit)
        {
            User entity = (User)domainModel;

            entity.LoginName = this.txtLoginName.Text.Trim();
            entity.RealName = this.txtRealName.Text.Trim();
            //添加模式获取用户输入用户密码
            if (entity.UserID == 0)
            {
                entity.PassWord = this.txtPassWord.Text.Trim();
            }
            entity.MobilePhone = this.txtMobilePhone.Text.Trim();
            entity.Email = this.txtEmail.Text.Trim();
            entity.Telphone = this.txtTelphone.Text.Trim();
            entity.OfficeTelphone = this.txtOfficeTelphone.Text.Trim();

            entity.Description = this.txtDescription.Text;
            entity.Status = int.Parse(this.rbStatus.SelectedValue);

        }
        return domainModel;
    }

    public void BindFromData(object domainModel, ViewMode controlMode)
    {
        this.Views.BindFromData(domainModel, controlMode);
    }

    #endregion
    #endregion
}

