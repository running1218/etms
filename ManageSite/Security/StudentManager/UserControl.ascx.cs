using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using ETMS.Controls;
using ETMS.Components.Basic.API.Entity.Security;
using ETMS.Components.Basic.Implement.BLL.Security;
public partial class Security_StudentManager_UserControl : System.Web.UI.UserControl, IMuliViewControl
{
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
        if (!this.Page.IsPostBack)
        {
            Site_Student entity = (Site_Student)domainModel;

            if (entity.UserID == 0)//注意:仅添加模式不需要
                return;

            this.txtLoginName.Text = entity.LoginName;
            this.txtLoginName.Enabled = false;
            this.txtRealName.Text = entity.RealName;
            this.txtEmail.Text = entity.Email;
            this.txtTelphone.Text = entity.OfficeTelphone;
            this.txtMobilePhone.Text = entity.MobilePhone;
            this.txtDescription.Text = entity.Description;
            this.rbStatus.SelectedValue = entity.Status.ToString();
            this.rdlSex.SelectedValue = entity.SexTypeID.ToString();
            this.txtHighestEducation.Text = entity.LastEducation;
            this.txtIdentity.Text = entity.Identity;
            this.txtParent.Text = entity.Superior;
            this.txtSpecialty.Text = entity.Specialty;
            this.txtWorkNo.Text = entity.WorkerNo;
            this.ddlDepartment.SelectedValue = entity.DepartmentID.ToString();
            this.txtTitleName.Text = entity.TitleName;
            this.ddlPolitics.SelectedValue = entity.PoliticsTypeID.ToString();
            this.ddlPost.SelectedValue = entity.PostID.ToString();
            this.ddlRank.SelectedValue = entity.RankID.ToString();
            this.txtBirthDay.DateTimeValue = entity.Birthday;
            this.txtJobTime.DateTimeValue = entity.JoinTime;
            this.ddlResettlementWay.SelectedValue = entity.ResettlementWayID.ToString();
        }
    }
    #endregion

    #region Browse_View
    public void BindFromData_Browse(object domainModel)
    {

        Site_Student entity = (Site_Student)domainModel;

        this.lblLoginName.Text = entity.LoginName;
        this.lblRealName.Text = entity.RealName;
        this.lblEmail.Text = entity.Email;
        this.lblMobilePhone.Text = entity.MobilePhone;
        this.lblDescription.Text = entity.Description;
        this.lblStatus.Text = entity.Status.ToString();
        this.lblCreateTime.Text = entity.CreateTime.ToString();
        this.lblCreator.Text = entity.Creator;



        this.lblBirthDay.DateTimeValue = entity.Birthday;
        this.lblJobTime.DateTimeValue = entity.JoinTime;
        this.lblSex.FieldIDValue = entity.SexTypeID.ToString();
        this.lblStatus.FieldIDValue = entity.Status.ToString();

        this.lblHightestEducation.Text = entity.LastEducation;
        this.lblIdentity.Text = entity.Identity;
        this.lblParent.Text = entity.Superior;
        this.lblSpecialty.Text = entity.Specialty;
        this.lblWorkNo.Text = entity.WorkerNo;
        this.lblTelephone.Text = entity.OfficeTelphone;
        this.lblDepartment.FieldIDValue = entity.DepartmentID.ToString();
        this.lblPolitics.FieldIDValue = entity.PoliticsTypeID.ToString();
        this.lblJobTitle.Text = entity.TitleName;
        this.lblPost.FieldIDValue = entity.PostID.ToString();
        this.lblRank.FieldIDValue = entity.RankID.ToString();
        this.dlabResettlementWay.FieldIDValue = entity.ResettlementWayID.ToString();
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
            Site_Student entity = (Site_Student)domainModel;

            entity.LoginName = this.txtLoginName.Text.Trim();
            entity.RealName = this.txtRealName.Text.Trim();
            //添加模式获取用户输入用户密码
            if (entity.UserID == 0)
            {
                entity.PassWord = this.txtPassWord.Text.Trim();
            }
            entity.MobilePhone = this.txtMobilePhone.Text.Trim();
            entity.Email = this.txtEmail.Text.Trim();
            entity.OfficeTelphone = this.txtTelphone.Text.Trim();
            entity.Description = this.txtDescription.Text;
            entity.Status = int.Parse(this.rbStatus.SelectedValue);
            if (!string.IsNullOrEmpty(this.txtBirthDay.Text.Trim()))
            {
                entity.Birthday = DateTime.Parse(this.txtBirthDay.Text.Trim());
            }
            entity.LastEducation = this.txtHighestEducation.Text.Trim();
            entity.Identity = this.txtIdentity.Text.Trim();
            if (!string.IsNullOrEmpty(this.txtJobTime.Text))
            {
                entity.JoinTime = DateTime.Parse(this.txtJobTime.Text);
            }
            entity.Superior = this.txtParent.Text.Trim();
            entity.Specialty = this.txtSpecialty.Text.Trim();
            entity.WorkerNo = this.txtWorkNo.Text.Trim();

            if (!string.IsNullOrEmpty(this.ddlDepartment.SelectedValue))
                entity.DepartmentID = int.Parse(this.ddlDepartment.SelectedValue);
            if (!string.IsNullOrEmpty(this.ddlPost.SelectedValue))
                entity.PostID = int.Parse(this.ddlPost.SelectedValue);
            if (!string.IsNullOrEmpty(this.ddlRank.SelectedValue))
                entity.RankID = int.Parse(this.ddlRank.SelectedValue);
            entity.TitleName = this.txtTitleName.Text.Trim();
            entity.PoliticsTypeID = int.Parse(this.ddlPolitics.SelectedValue);
            entity.SexTypeID = int.Parse(this.rdlSex.SelectedValue);
            entity.ResettlementWayID = int.Parse(this.ddlResettlementWay.SelectedValue);
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