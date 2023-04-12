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
using ETMS.Components.Poll.API.Entity;
using ETMS.Components.Poll.Implement.BLL;
public partial class Poll_ResourceQuery_UserControl : System.Web.UI.UserControl, IMuliViewControl
{
    private static Poll_QueryLogic Logic = new Poll_QueryLogic();

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
            Poll_Query entity = (Poll_Query)domainModel;

            if (entity.QueryID == 0)//注意:仅添加模式不需要
                return;

            this.txtQueryName.Text = entity.QueryName;
            this.txtBeginTime.DateTimeValue = entity.BeginTime;
            this.txtEndTime.DateTimeValue = entity.EndTime;
            this.rblIsAllShowResult.SelectedValue = entity.IsDisplayResult ? "1" : "0";
            this.rblStatus.SelectedValue = entity.Status.ToString();
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

        Poll_Query entity = (Poll_Query)domainModel;

        this.lblQueryName.Text = entity.QueryName;
        this.lblBeginTime.DateTimeValue = entity.BeginTime;
        this.lblEndTime.DateTimeValue = entity.EndTime;
        this.lblIsShowResult.FieldIDValue = entity.IsDisplayResult ? "1" : "0";
        this.lblIsPublish.FieldIDValue = entity.IsPublish ? "1" : "0";
        this.lblState.FieldIDValue = entity.Status.ToString();
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
            Poll_Query entity = (Poll_Query)domainModel;

            entity.QueryName = this.txtQueryName.Text;
            entity.BeginTime = this.txtBeginTime.DateTimeValue;
            entity.EndTime = this.txtEndTime.DateTimeValue;
            entity.IsDisplayResult = this.rblIsAllShowResult.SelectedValue == "1" ? true : false;
            entity.Status = Int16.Parse(this.rblStatus.SelectedValue);
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

