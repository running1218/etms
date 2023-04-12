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

using ETMS.Utility;
using ETMS.Controls;
using ETMS.Components.Basic.API.Entity.Common;
using ETMS.Components.Basic.Implement.BLL.Common;
using ETMS.Components.Basic.API.Entity.Security;
using ETMS.Components.Basic.Implement.BLL.Security;
using System.Collections.Generic;
using ETMS.Utility.Service.FileUpload;

public partial class Admin_Site_Organization_UserControl : System.Web.UI.UserControl, IMuliViewControl
{
    private static OrganizationLogic Logic = new OrganizationLogic();

    #region IMuliViewControl ��Ա
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

    /// <summary>
    /// �Ƿ��Լ��޸Ļ�����Ϣ
    /// </summary>
    public bool IsSelfChangeOperate
    {
        get
        {
            return (bool)ViewState["IsSelfChangeOperate"];
        }
        set
        {
            ViewState["IsSelfChangeOperate"] = value;
        }
    }
    #region Edit_View
    public void BindFromData_Edit(object domainModel)
    {
        if (!this.Page.IsPostBack)
        {
            //����˳�������б�
            for (int i = 1; i <= 100; i++)
            {
                ddlOrder.Items.Add(i.ToString());
            }
            Organization entity = (Organization)domainModel;

            //�Ƿ��Լ��޸Ļ�����Ϣ
            IsSelfChangeOperate = (ETMS.AppContext.UserContext.Current.OrganizationID != 0
                && (entity.OrganizationID == ETMS.AppContext.UserContext.Current.OrganizationID));
            //��ֹ�Լ������޸����ƣ�����
            this.txtOrgName.Enabled = !this.IsSelfChangeOperate;
            this.txtStudentNum.Enabled = !this.IsSelfChangeOperate;

            if (null != Request.ToparamValue<string>("op"))
            {
                this.txtOrgCode.Enabled = Request.ToparamValue<string>("op").Equals("add", StringComparison.OrdinalIgnoreCase);
            }
            else
            {
                this.txtOrgCode.Enabled = false;
            }
            int parentOrgID = 0;
            if (entity.NodeID == 0)//ע��:�����ģʽ����Ҫ
            {
                parentOrgID = int.Parse(Request.QueryString["parentid"]);
            }
            else
            {
                parentOrgID = entity.ParentNodeID;
            }
            //���븸������������
            Organization parentOrg = (Organization)Logic.GetNodeByID(parentOrgID);
            if (parentOrg.NodeID == 0)
            {
                parentOrg = (Organization)Logic.GetNodeTreeForManager(parentOrg, false);
                parentOrg.DisplayPath = "/������";
            }
            this.lblParentCode.Text = parentOrg.OrganizationCode;
            this.lblParentName.Text = parentOrg.DisplayPath;            

            if (entity.NodeID == 0)//ע��:�����ģʽ����Ҫ
            {
                //�Զ��������
                if (parentOrg.ChildNodes.Count == 0)
                {
                    ddlOrder.SelectedIndex = 0;
                }
                else
                {
                    System.Collections.Generic.List<Node> list = (System.Collections.Generic.List<Node>)parentOrg.ChildNodes;
                    list.Sort(new Comparison<Node>(delegate(Node A, Node B) { return A.OrderNo.CompareTo(B.OrderNo); }));
                    ddlOrder.SelectedValue = (parentOrg.ChildNodes[parentOrg.ChildNodes.Count - 1].OrderNo + 1).ToString();
                }
                Image1.ImageUrl = ETMS.Utility.StaticResourceUtility.GetOrgLogoFullPath("default.jpg");//����Ĭ��ͼ
                return;
            }
            Image1.ImageUrl = ETMS.Utility.StaticResourceUtility.GetOrgLogoFullPath(string.IsNullOrEmpty(entity.Logo) ? "default.jpg" : entity.Logo);
            //��������
            this.txtAddress.Text = entity.Address;
            this.txtEmail.Text = entity.Email;
            this.txtEstablishTime.Text = entity.EstablishTime == null ? "" : entity.EstablishTime.Value.ToString("yyyy-MM-dd");
            this.txtFax.Text = entity.Fax;
            this.txtManager.Text = entity.Manager;
            this.txtMobilePhone.Text = entity.MobilePhone;
            this.txtOrgCode.Text = entity.OrganizationCode;
            this.txtOrgName.Text = entity.OrganizationName;
            this.txtPostCode.Text = entity.PostCode;
            this.txtTelphone.Text = entity.Telphone;
            this.txtTrainer.Text = entity.Trainer;
            this.txtTrainerEmail.Text = entity.TrainerEmail;
            this.txtTrainerTelphonePhone.Text = entity.TrainerTelphonePhone; 
            this.ddlOrder.SelectedValue = entity.OrderNo.ToString();
            this.txtStudentNum.Text = entity.StudentNum.ToString();
            this.txtDomain.Text = entity.Domain;
        }
    }
    #endregion

    #region Browse_View
    public void BindFromData_Browse(object domainModel)
    {

        Organization entity = (Organization)domainModel;
        this.imgLogo.ImageUrl = ETMS.Utility.StaticResourceUtility.GetOrgLogoFullPath(string.IsNullOrEmpty(entity.Logo) ? "default.jpg" : entity.Logo);
        this.lblAddress.Text = entity.Address;
        this.lblEmail.Text = entity.Email;
        this.lblEstablishTime.Text = entity.EstablishTime == null ? "" : entity.EstablishTime.Value.ToString("yyyy-MM-dd");
        this.lblFax.Text = entity.Fax;
        this.lblManager.Text = entity.Manager;
        this.lblMobilePhone.Text = entity.MobilePhone;
        this.lblOrgCode.Text = entity.OrganizationCode;
        this.lblOrgName.Text = entity.OrganizationName;
        this.lblPostCode.Text = entity.PostCode;
        this.lblTelphone.Text = entity.Telphone;
        this.lblTrainer.Text = entity.Trainer;
        this.lblTrainerEmail.Text = entity.TrainerEmail;
        this.lblTrainerTelphonePhone.Text = entity.TrainerTelphonePhone;
        this.lblStatus.Text = entity.State == 1 ? "����" : "ͣ��";
        lblStudentNum.Text = entity.StudentNum.ToString();
        this.lblDomain.Text = entity.Domain;

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
            Organization entity = (Organization)domainModel;

            //��������
            entity.Address = this.txtAddress.Text;
            entity.Email = this.txtEmail.Text;
            if (!string.IsNullOrEmpty(this.txtEstablishTime.Text))
            {
                entity.EstablishTime = DateTime.Parse(this.txtEstablishTime.Text);
            }
            entity.Fax = this.txtFax.Text;
            entity.Manager = this.txtManager.Text;
            entity.MobilePhone = this.txtMobilePhone.Text;
            entity.OrganizationCode = this.txtOrgCode.Text;
            entity.OrganizationName = this.txtOrgName.Text;
            entity.PostCode = this.txtPostCode.Text;
            entity.Telphone = this.txtTelphone.Text;
            entity.Trainer = this.txtTrainer.Text;
            entity.TrainerEmail = this.txtTrainerEmail.Text;
            entity.TrainerTelphonePhone = this.txtTrainerTelphonePhone.Text; 
            entity.OrderNo = int.Parse(this.ddlOrder.SelectedValue);
            entity.StudentNum = this.txtStudentNum.Text.ToInt();
            entity.Domain = this.txtDomain.Text;

            //�����ϴ��ļ�
            List<FileUploadInfo> uploaders = this.uploader.FileUrl;
            FileUploadInfo fileDefine = uploaders.Count > 0 ? this.uploader.FileUrl[0] : null;

            entity.Logo = fileDefine == null ? entity.Logo : fileDefine.BizUrl;
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

