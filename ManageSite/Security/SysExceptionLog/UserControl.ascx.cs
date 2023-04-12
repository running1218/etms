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
using ETMS.Utility;
using ETMS.AppContext;
using ETMS.AppContext.Component;
using ETMS.Components.Basic.API.Entity.Security;
using ETMS.Components.Basic.Implement.BLL.Security;
public partial class Log_SystemException_UserControl : System.Web.UI.UserControl, IMuliViewControl
{
    private static Log_SystemExceptionLogic Logic = new Log_SystemExceptionLogic();

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
        this.PageSet1.pageInit(this.GridViewList, new IPageDataSource(this.getDataSource1));
        if (!Page.IsPostBack)
        {
            this.PageSet1.QueryChange();
        }
    }
    private IList getDataSource1(int pageIndex, int pageSize, out int totalRecords)
    {
        DataTable dataList = Logic.GetPagedList(pageIndex, pageSize, " SysExLogID desc ", string.Format(" AND Message like '%{0}%' AND LoginName like '%{1}%'", this.txtquery.Text.ToSafeSQLValue(), this.txtOpeator.Text.ToSafeSQLValue()), out totalRecords);
        PageDataSourceProvider psp = new PageDataSourceProvider(dataList, pageIndex, pageSize);
        return psp.PageDataSource;
    }

    protected void btn_Search_Click(object sender, EventArgs e)
    {
        this.PageSet1.QueryChange();
    }

    protected void btnDeletes_Click(object sender, EventArgs e)
    {
        Int64[] selectedValues = CustomGridView.GetSelectedValues<Int64>(this.GridViewList);
        if (selectedValues.Length == 0)
        {
            JsUtility.AlertMessageBox("至少勾选一个！");
            return;
        }
        //批量重置用户密码
        foreach (object obj in selectedValues)
        {
            Logic.Remove((Int64)obj);
        }
        //刷新数据
        this.PageSet1.DataBind();
        JsUtility.SuccessMessageBox("批量删除操作成功！");
    }
    #endregion

    #region Edit_View
    public void BindFromData_Edit(object domainModel)
    {

    }
    #endregion

    #region Browse_View
    public void BindFromData_Browse(object domainModel)
    {
        Log_SystemException entity = (Log_SystemException)domainModel;


        if (entity.SysExLogID != null)
        {
            if (entity.SysExLogID.GetTypeCode() == System.TypeCode.DateTime)
                this.lblSysExLogID.Text = entity.SysExLogID.Equals(DateTime.Parse("1900-1-1")) ? "" : string.Format("{0:yyyy-MM-dd}", entity.SysExLogID);
            else
                this.lblSysExLogID.Text = entity.SysExLogID.ToString();
        }

        if (entity.ApplicationName != null)
        {
            if (entity.ApplicationName.GetTypeCode() == System.TypeCode.DateTime)
                this.lblApplicationName.Text = entity.ApplicationName.Equals(DateTime.Parse("1900-1-1")) ? "" : string.Format("{0:yyyy-MM-dd}", entity.ApplicationName);
            else
                this.lblApplicationName.Text = entity.ApplicationName.ToString();
        }

        if (entity.Message != null)
        {
            if (entity.Message.GetTypeCode() == System.TypeCode.DateTime)
                this.lblMessage.Text = entity.Message.Equals(DateTime.Parse("1900-1-1")) ? "" : string.Format("{0:yyyy-MM-dd}", entity.Message);
            else
                this.lblMessage.Text = entity.Message.ToString();
        }

        if (entity.BaseMessage != null)
        {
            if (entity.BaseMessage.GetTypeCode() == System.TypeCode.DateTime)
                this.lblBaseMessage.Text = entity.BaseMessage.Equals(DateTime.Parse("1900-1-1")) ? "" : string.Format("{0:yyyy-MM-dd}", entity.BaseMessage);
            else
                this.lblBaseMessage.Text = entity.BaseMessage.ToString();
        }

        if (entity.StackTrace != null)
        {
            if (entity.StackTrace.GetTypeCode() == System.TypeCode.DateTime)
                this.lblStackTrace.Text = entity.StackTrace.Equals(DateTime.Parse("1900-1-1")) ? "" : string.Format("{0:yyyy-MM-dd}", entity.StackTrace);
            else
                this.lblStackTrace.Text = entity.StackTrace.ToString();
        }

        if (entity.LoginName != null)
        {
            if (entity.LoginName.GetTypeCode() == System.TypeCode.DateTime)
                this.lblLoginName.Text = entity.LoginName.Equals(DateTime.Parse("1900-1-1")) ? "" : string.Format("{0:yyyy-MM-dd}", entity.LoginName);
            else
                this.lblLoginName.Text = entity.LoginName.ToString();
        }

        if (entity.CreateTime != null)
        {
            if (entity.CreateTime.GetTypeCode() == System.TypeCode.DateTime)
                this.lblCreateTime.Text = entity.CreateTime.Equals(DateTime.Parse("1900-1-1")) ? "" : string.Format("{0:yyyy-MM-dd}", entity.CreateTime);
            else
                this.lblCreateTime.Text = entity.CreateTime.ToString();
        }

        if (entity.ServerName != null)
        {
            if (entity.ServerName.GetTypeCode() == System.TypeCode.DateTime)
                this.lblServerName.Text = entity.ServerName.Equals(DateTime.Parse("1900-1-1")) ? "" : string.Format("{0:yyyy-MM-dd}", entity.ServerName);
            else
                this.lblServerName.Text = entity.ServerName.ToString();
        }

        if (entity.ClientIP != null)
        {
            if (entity.ClientIP.GetTypeCode() == System.TypeCode.DateTime)
                this.lblClientIP.Text = entity.ClientIP.Equals(DateTime.Parse("1900-1-1")) ? "" : string.Format("{0:yyyy-MM-dd}", entity.ClientIP);
            else
                this.lblClientIP.Text = entity.ClientIP.ToString();
        }

        if (entity.PageUrl != null)
        {
            if (entity.PageUrl.GetTypeCode() == System.TypeCode.DateTime)
                this.lblPageUrl.Text = entity.PageUrl.Equals(DateTime.Parse("1900-1-1")) ? "" : string.Format("{0:yyyy-MM-dd}", entity.PageUrl);
            else
                this.lblPageUrl.Text = entity.PageUrl.ToString();
        }

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
        return domainModel;
    }

    public void BindFromData(object domainModel, ViewMode controlMode)
    {
        this.Views.BindFromData(domainModel, controlMode);
    }

    #endregion
    #endregion
}

