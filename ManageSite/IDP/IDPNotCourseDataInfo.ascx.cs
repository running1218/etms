using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Controls;
using ETMS.Utility;
using ETMS.AppContext;
using ETMS.AppContext.Component;
using ETMS.Components.IDP.API.Entity.NotCourseData;

public partial class IDP_IDPNotCourseDataInfo : System.Web.UI.UserControl, IMuliViewControl
{
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
    public void BindFromData_Manage(object domainModel)
    {       
    }
    public void BindFromData_Edit(object domainModel)
    {

        if (!this.Page.IsPostBack)
        {
            IDP_NotCourseData entity = (IDP_NotCourseData)domainModel;

            if (entity.IDP_NotCourseDataID == Guid.Empty)//注意:仅添加模式不需要
            {
                this.rbnDataStatus.SelectedValue="1";
                this.rbnTeachModelID.SelectedIndex=0;
                entity.IDPSourceID = 2;//默认为2：学习地图的IDP非课程
                entity.OrgID = ETMS.AppContext.UserContext.Current.OrganizationID;
                entity.CreateTime = DateTime.Now;
                entity.CreateUser = ETMS.AppContext.UserContext.Current.RealName;
                entity.CreateUserID = ETMS.AppContext.UserContext.Current.UserID;
                return;
            }

            this.txtDataCode.Text = entity.DataCode.ToString();
            this.txtDataName.Text = entity.DataName.ToString();
            if (!string.IsNullOrEmpty(entity.DataCotent))
                this.txtDataCotent.Text = entity.DataCotent.ToString();
            if (!string.IsNullOrEmpty(entity.DataOutline))
                this.txtDataOutline.Text = entity.DataOutline.ToString();
            this.txtTimeLength.Text = entity.TimeLength.ToString();
            this.rbnDataStatus.SelectedValue = entity.DataStatus.ToString();
            this.rbnTeachModelID.SelectedValue = entity.StudyModelID.ToString();
            this.txtStudyTimes.Text = entity.StudyTimes.ToString();
            if (!string.IsNullOrEmpty(entity.Implementor))
               this.txtImplementor.Text = entity.Implementor.ToString();
            if (!string.IsNullOrEmpty(entity.DataURL))
               this.txtDataURL.Text = entity.DataURL.ToString();
            if (!string.IsNullOrEmpty(entity.DutyMan))
               this.txtDutyMan.Text = entity.DutyMan.ToString();
            if (!string.IsNullOrEmpty(entity.EvaluationMode))
               this.txtEvaluationMode.Text = entity.EvaluationMode.ToString();
        }
            
    }
    public void BindFromData_Browse(object domainModel)
    {
        IDP_NotCourseData entity = (IDP_NotCourseData)domainModel;

        
        this.lblDataCode.Text = entity.DataCode.ToString();      
        this.lblDataName.Text = entity.DataName.ToString();
        if (!string.IsNullOrEmpty(entity.DataCotent))
            this.lblDataCotent.Text = entity.DataCotent.ToString();
        if (!string.IsNullOrEmpty(entity.DataOutline))
            this.lblDataOutline.Text = entity.DataOutline.ToString();
        this.lblTimeLength.Text = entity.TimeLength.ToString();
        this.lblDataStatus.FieldIDValue = entity.DataStatus.ToString();
        this.lblTeachModelID.FieldIDValue = entity.StudyModelID.ToString();
        this.lblStudyTimes.Text = entity.StudyTimes.ToString();
        if (!string.IsNullOrEmpty(entity.Implementor))
           this.lblImplementor.Text = entity.Implementor.ToString();
        if (!string.IsNullOrEmpty(entity.DataURL))
            this.lblDataURL.Text = entity.DataURL.ToString();
        if (!string.IsNullOrEmpty(entity.DutyMan))
            this.lblDutyMan.Text = entity.DutyMan.ToString();
        if (!string.IsNullOrEmpty(entity.EvaluationMode))
            this.lblEvaluationMode.Text = entity.EvaluationMode.ToString();

        if (entity.CreateTime.ToString("yyyy-MM-dd") != DateTime.MinValue.ToString("yyyy-MM-dd") && entity.CreateTime.ToString("yyyy-MM-dd") != DateTime.MaxValue.ToString("yyyy-MM-dd"))
        {
            this.lblCreateTime.Text = entity.CreateTime.ToString("yyyy-MM-dd");
        }
        
        this.lblCreateUser.Text = entity.CreateUser.ToString();

        if (entity.ModifyTime.ToString("yyyy-MM-dd") != DateTime.MinValue.ToString("yyyy-MM-dd") && entity.ModifyTime.ToString("yyyy-MM-dd") != DateTime.MaxValue.ToString("yyyy-MM-dd"))
        {
            this.lblModifyTime.Text = entity.ModifyTime.ToString("yyyy-MM-dd");
        }        
        this.lblModifyUser.Text = entity.ModifyUser.ToString();                
    }		 


    #region List_View
	public void BindFromData_List(object domainModel)
     {
       
    } 
	#endregion 
		 
    public object UnBindFromData(object domainModel)
    {
        if (this.ControlMode == ViewMode.Edit)
        {
            IDP_NotCourseData entity = (IDP_NotCourseData)domainModel;
                entity.DataCode =this.txtDataCode.Text;
                entity.DataName = this.txtDataName.Text;
                entity.DataCotent =this.txtDataCotent.Text;
                entity.DataOutline =this.txtDataOutline.Text;

            if (!String.IsNullOrEmpty(this.txtTimeLength.Text))
                entity.TimeLength = this.txtTimeLength.Text.ToDecimal();

            entity.DataStatus = this.rbnDataStatus.SelectedValue.ToInt();

            entity.StudyModelID = this.rbnTeachModelID.SelectedValue.ToInt();

            if (!String.IsNullOrEmpty(this.txtStudyTimes.Text))
                entity.StudyTimes =this.txtStudyTimes.Text.ToInt();

                entity.Implementor =this.txtImplementor.Text;

                entity.DataURL = this.txtDataURL.Text;
                entity.DutyMan = this.txtDutyMan.Text;
                entity.EvaluationMode = this.txtEvaluationMode.Text;           
           
            entity.ModifyTime = DateTime.Now;
            entity.ModifyUser = ETMS.AppContext.UserContext.Current.RealName;
        }
        return domainModel;
    }

    public void BindFromData(object domainModel, ViewMode controlMode)
    {
        this.Views.BindFromData(domainModel, controlMode);
    }
}

