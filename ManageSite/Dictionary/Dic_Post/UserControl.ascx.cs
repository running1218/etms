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
using ETMS.Components.Basic.Implement.BLL;
using ETMS.Components.Basic.API.Entity.Dictionary;
using ETMS.Components.Basic.Implement.BLL.Dictionary;
public partial class Dic_Post_UserControl : System.Web.UI.UserControl, IMuliViewControl
{
    private static Dic_PostLogic Logic = new Dic_PostLogic();

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
        string filter = string.Format(" AND OrganizationID={0}", ETMS.AppContext.UserContext.Current.OrganizationID);
        if (!string.IsNullOrEmpty(this.txtPostCode_Query.Text))
        {
            filter += string.Format(" AND PostCode like '%{0}%'", this.txtPostCode_Query.Text.Trim().ToSafeSQLValue());
        }
        if (!string.IsNullOrEmpty(this.txtPostName_Query.Text))
        {
            filter += string.Format(" AND PostName like '%{0}%'", this.txtPostName_Query.Text.Trim().ToSafeSQLValue());
        }
        if (this.ddlPostType_Query.SelectedValue != "-1")
        {
            filter += string.Format(" AND PostTypeID ={0}", this.ddlPostType_Query.SelectedValue);
        }
        if (this.ddlStatus_Query.SelectedValue != "-1")
        {
            filter += string.Format(" AND Status ={0}", this.ddlStatus_Query.SelectedValue);
        }

        DataTable dataList = Logic.GetPagedList(pageIndex, pageSize, "", filter, out totalRecords);
        PageDataSourceProvider psp = new PageDataSourceProvider(dataList, pageIndex, pageSize);
        return psp.PageDataSource;
    }
    protected void btn_Search_Click(object sender, EventArgs e)
    {
        this.PageSet1.QueryChange();
    }

    protected void btnDeletes_Click(object sender, EventArgs e)
    {
        int[] selectedValues = CustomGridView.GetSelectedValues<int>(this.GridViewList);
        if (selectedValues.Length == 0)
        {
            JsUtility.AlertMessageBox("至少勾选一个！");
            return;
        }
        //批量重置用户密码
        foreach (int obj in selectedValues)
        {
            Logic.Remove((Int32)obj);
        }
        //刷新数据
        this.PageSet1.DataBind();
        JsUtility.SuccessMessageBox("批量删除操作成功！");
    }
    #endregion

    #region Edit_View
    public void BindFromData_Edit(object domainModel)
    {
        if (!this.Page.IsPostBack)
        {
            Dic_Post entity = (Dic_Post)domainModel;

            if (entity.PostID == 0)//注意:仅添加模式不需要
                return;

            this.txtPostCode.Text = entity.PostCode.ToString();
            this.txtPostName.Text = entity.PostName.ToString();
            this.ddlPostTypeCode.SelectedValue = entity.PostTypeID.ToString();
            this.txtDescription.Text = entity.Description;
            this.txtLiability.Text = entity.Liability;
            this.rbStatus.SelectedValue = entity.Status.ToString();

        }
    }
    #endregion

    #region Browse_View
    public void BindFromData_Browse(object domainModel)
    {
        Dic_Post entity = (Dic_Post)domainModel;


        this.lblPostCode.Text = entity.PostCode;
        this.lblPostName.Text = entity.PostName;

        this.lblPostTypeCode.FieldIDValue = entity.PostTypeID.ToString();
        this.lblDescription.Text = entity.Description;
        this.lblLiability.Text = entity.Liability;

        this.lblStatus.FieldIDValue = entity.Status.ToString();

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
            Dic_Post entity = (Dic_Post)domainModel;


            if (!String.IsNullOrEmpty(this.txtPostCode.Text))
                entity.PostCode = this.txtPostCode.Text;

            if (!String.IsNullOrEmpty(this.txtPostName.Text))
                entity.PostName = this.txtPostName.Text;

            entity.PostTypeID = int.Parse(ddlPostTypeCode.SelectedValue);

            if (!String.IsNullOrEmpty(this.txtDescription.Text))
                entity.Description = this.txtDescription.Text;

            if (!String.IsNullOrEmpty(this.txtLiability.Text))
                entity.Liability = this.txtLiability.Text;

            entity.Status = this.rbStatus.SelectedValue.ToInt16();

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

