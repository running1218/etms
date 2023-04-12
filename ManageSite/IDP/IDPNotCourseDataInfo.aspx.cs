using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Components.IDP.API.Entity.NotCourseData;
using ETMS.Controls;
using ETMS.Utility;
using ETMS.Components.IDP.Implement.BLL.NotCourseData;

public partial class IDP_IDPNotCourseDataAdd : ETMS.Controls.BasePage
{
    private static IDP_NotCourseDataLogic idpNotCourseDataLogic = new IDP_NotCourseDataLogic();
    private IDP_NotCourseData idpNotCourseData = new IDP_NotCourseData();
    /// <summary>
    /// 1:add,2:edit,3:view
    /// </summary>
    public int Operation
    {
        get { return Request.QueryString["Operation"].ToInt(); }
    }
    /// <summary>
    /// IDP非课程资料ID
    /// </summary>
    private Guid IDP_NotCourseDataID
    {
        get { return Request.QueryString["IDP_NotCourseDataID"].ToGuid(); }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Initial();
        }       
    }
    private void Initial()
    {
        this.btnUpdate.Visible = false;
        switch (Operation)
        {
            case 1://add
                this.btnUpdate.Visible = true;
                this.IDPNotCourseDataInfo.BindFromData(new IDP_NotCourseData(), ViewMode.Edit);
                break;
            case 2://edit
                this.btnUpdate.Visible = true;
                idpNotCourseData = idpNotCourseDataLogic.GetById(IDP_NotCourseDataID);
                this.IDPNotCourseDataInfo.BindFromData(idpNotCourseData, ViewMode.Edit);
                break;
            case 3://view
                idpNotCourseData = idpNotCourseDataLogic.GetById(IDP_NotCourseDataID);
                this.IDPNotCourseDataInfo.BindFromData(idpNotCourseData, ViewMode.Browse);
                break;
        }
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            IDP_NotCourseData entity = (IDP_NotCourseData)this.IDPNotCourseDataInfo.DomainModel;
            idpNotCourseDataLogic.Save(entity);
            ETMS.Utility.JsUtility.SuccessMessageBoxAndCloseWindowAndTriggerRefreshEvent("保存成功！");

        }
        catch (ETMS.AppContext.BusinessException bizEx)
        {
            if (Operation == 1)
            {
                IDP_NotCourseData entity = (IDP_NotCourseData)this.IDPNotCourseDataInfo.DomainModel;
                entity.IDP_NotCourseDataID = Guid.Empty;
            }
            ETMS.Utility.JsUtility.FailedMessageBox(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
        }
        catch (Exception ex)
        {
            if (Operation == 1)
            {
                IDP_NotCourseData entity = (IDP_NotCourseData)this.IDPNotCourseDataInfo.DomainModel;
                entity.IDP_NotCourseDataID = Guid.Empty;
            }
            ETMS.Utility.JsUtility.FailedMessageBox(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(ex));
        }
    }

}