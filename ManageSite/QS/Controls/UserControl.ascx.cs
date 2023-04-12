using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Controls;
using ETMS.Components.QS.Implement.BLL;
using ETMS.Components.QS.API.Entity;

public partial class QS_Controls_UserControl : System.Web.UI.UserControl, IMuliViewControl
{
    private static QS_QueryLogic LogicBiz = new QS_QueryLogic();

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
            QS_Query entity = (QS_Query)domainModel;

            if (entity.QueryID.ToString() == "00000000-0000-0000-0000-000000000000")//注意:仅添加模式不需要
                return;

            this.txtQueryName.Text = entity.QueryName;
            this.txtBeginTime.DateTimeValue = entity.BeginTime;
            this.txtEndTime.DateTimeValue = entity.EndTime;
            this.rblIsAllShowResult.SelectedValue = entity.IsDisplayResult ? "1" : "0";
            this.rblStatus.SelectedValue = entity.QueryStatus.ToString();
            this.txtDutyUser.Text = entity.DutyUser;
            this.txtHeader.Text = entity.Header;
            this.txtFooter.Text = entity.Footer;
            this.txtRemark.Text = entity.Remark;
        }
    }
    #endregion

    #region Browse_View
    public void BindFromData_Browse(object domainModel)
    {

        QS_Query entity = (QS_Query)domainModel;

        this.lblQueryName.Text = entity.QueryName;
        this.lblBeginTime.DateTimeValue = entity.BeginTime;
        this.lblEndTime.DateTimeValue = entity.EndTime;
        this.lblIsShowResult.FieldIDValue = entity.IsDisplayResult ? "1" : "0";
        this.lblIsPublish.FieldIDValue = entity.IsPublish ? "1" : "0";
        this.lblState.FieldIDValue = entity.QueryStatus.ToString();
        this.lblDutyUser.Text = entity.DutyUser;
        this.lblHeader.Text = entity.Header;
        this.lblFooter.Text = entity.Footer;
        this.lblRemark.Text = entity.Remark;
        this.lblCreator.Text = entity.CreateUser;
        this.lblCreateTime.DateTimeValue = entity.CreateTime;
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
            QS_Query entity = (QS_Query)domainModel;

            entity.QueryName = this.txtQueryName.Text;
            entity.BeginTime = this.txtBeginTime.DateTimeValue;
            entity.EndTime = this.txtEndTime.DateTimeValue;
            entity.IsDisplayResult = this.rblIsAllShowResult.SelectedValue == "1" ? true : false;
            entity.QueryStatus = Int16.Parse(this.rblStatus.SelectedValue);
            entity.DutyUser = this.txtDutyUser.Text;
            entity.Header = this.txtHeader.Text;
            entity.Footer = this.txtFooter.Text;
            entity.Remark = this.txtRemark.Text;
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