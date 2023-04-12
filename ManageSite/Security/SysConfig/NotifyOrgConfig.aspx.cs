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

using System.Collections.Generic;
using System.Collections.Specialized;
using ETMS.Controls;
using ETMS.Utility;
using ETMS.AppContext;
using ETMS.AppContext.Component;
using ETMS.Components.Basic.API.Entity.Notify;
using ETMS.Components.Basic.Implement.BLL.Notify;
using ETMS.Editor.UEditor;
public partial class Admin_Site_SysConfig_NotifyOrgConfig : BasePage
{
    private Notify_MessageClassLogic ConfigGroupLogic = new Notify_MessageClassLogic();
    private Notify_MessageConfigLogic ConfigLogic = new Notify_MessageConfigLogic();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            int totalRecords = 0;
            IList<Notify_MessageClass> configGroups = ConfigGroupLogic.GetEntityList(1, 1000, "", "", out totalRecords);

            //��Tabs��ͷ��
            this.rptConfigGroup.DataSource = configGroups;
            this.rptConfigGroup.DataBind();

            //��Tabs�����ݣ�
            this.rptConfigs.DataSource = configGroups;
            this.rptConfigs.DataBind();

        }

    }
    protected void btn_SaveHandle(object sender, CommandEventArgs e)
    {
        Control container = (sender as Control).Parent;

        //������ID
        Int16 configGroupID = e.CommandArgument.ToInt16();
        //����ID
        int configID = (container.FindControl("hdConfigId") as HiddenField).Value.ToInt();

        //ģ�����˵��
        string templateVariableDefine = (container.FindControl("txtTemplateVariableDefine") as Label).Text;

        //�ʼ��������
        bool isEnableEmail = (container.FindControl("ckboxIsEnableEmail") as CheckBox).Checked;
        string emailSubjectTemplate = (container.FindControl("txtEmailSubjectTemplate") as TextBox).Text;
        string emailBodyTemplate = (container.FindControl("txtEmailBodyTemplate") as UEditor).Text;

        //�����������
        bool isEnableSMS = (container.FindControl("ckboxIsEnableSMS") as CheckBox).Checked;
        string smsSubjectTemplate = (container.FindControl("txtSMSSubjectTemplate") as TextBox).Text;
        string smsBodyTemplate = (container.FindControl("txtSMSBodyTemplate") as TextBox).Text;

        //�ʼ��������
        bool isEnableSiteInfo = (container.FindControl("ckboxIsEnableSiteInfo") as CheckBox).Checked;
        string siteInfoSubjectTemplate = (container.FindControl("txtSiteInfoSubjectTemplate") as TextBox).Text;
        string siteInfoBodyTemplate = (container.FindControl("txtSiteInfoBodyTemplate") as UEditor).Text;

        Notify_MessageConfig defaultConfigItem = ConfigLogic.GetById(configID);
        if (defaultConfigItem.OrganizationID == 0)//����������
        {
            //��������������
            Notify_MessageConfig configItem = new Notify_MessageConfig()
            {
                OrganizationID = UserContext.Current.OrganizationID,//��������
                MessageClassID = configGroupID,
                TemplateVariableDefine = templateVariableDefine,

                IsEnableEmail = isEnableEmail,
                EmailSubjectTemplate = emailSubjectTemplate,
                EmailBodyTemplate = emailBodyTemplate,

                IsEnableSMS = isEnableSMS,
                SMSSubjectTemplate = smsSubjectTemplate,
                SMSBodyTemplate = smsBodyTemplate,

                IsEnableSiteInfo = isEnableSiteInfo,
                SiteInfoSubjectTemplate = siteInfoSubjectTemplate,
                SiteInfoBodyTemplate = siteInfoBodyTemplate,

                Status = 1,

                CreatorID = ETMS.AppContext.UserContext.Current.UserID,
                CreateTime = DateTime.Now,
                UpdaterID = ETMS.AppContext.UserContext.Current.UserID,
                UpdateTime = DateTime.Now
            };
            ConfigLogic.Add(configItem);
        }
        else//�޸Ļ���������
        {
            defaultConfigItem.TemplateVariableDefine = templateVariableDefine;

            defaultConfigItem.IsEnableEmail = isEnableEmail;
            defaultConfigItem.EmailSubjectTemplate = emailSubjectTemplate;
            defaultConfigItem.EmailBodyTemplate = emailBodyTemplate;

            defaultConfigItem.IsEnableSMS = isEnableSMS;
            defaultConfigItem.SMSSubjectTemplate = smsSubjectTemplate;
            defaultConfigItem.SMSBodyTemplate = smsBodyTemplate;

            defaultConfigItem.IsEnableSiteInfo = isEnableSiteInfo;
            defaultConfigItem.SiteInfoSubjectTemplate = siteInfoSubjectTemplate;
            defaultConfigItem.SiteInfoBodyTemplate = siteInfoBodyTemplate;

            ConfigLogic.Update(defaultConfigItem);
        }

        ETMS.Utility.JsUtility.SuccessMessageBox("������ʾ", "�������ɹ���", "function(){ window.location.href=window.location.href; }");
    }

    protected void rptConfigs_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Notify_MessageClass configGroup = e.Item.DataItem as Notify_MessageClass;
            //��������µĻ����Զ���������
            string filter = string.Format(" and organizationId='{1}' and MessageClassID='{0}'"
                , configGroup.MessageClassID
                , UserContext.Current.OrganizationID);
            int totalRecords = 0;
            IList<Notify_MessageConfig> list = ConfigLogic.GetEntityList(1, 1000, "", filter, out totalRecords);
            if (list.Count == 0)
            {
                //��������µ�ϵͳĬ��������
                filter = string.Format(" and organizationId='0' and MessageClassID='{0}'", configGroup.MessageClassID);
                list = ConfigLogic.GetEntityList(1, 1000, "", filter, out totalRecords);
            }
            if (list.Count == 0)
            {
                //׷��1���հ�λ�ã��ṩ�û�����¼��
                list.Add(new Notify_MessageConfig());
            }
            Control container = e.Item;
            //����ID
            (container.FindControl("hdConfigId") as HiddenField).Value = list[0].ConfigID.ToString();

            //ģ�����˵��
            (container.FindControl("txtTemplateVariableDefine") as Label).Text = list[0].TemplateVariableDefine;

            //�ʼ��������
            (container.FindControl("ckboxIsEnableEmail") as CheckBox).Checked = list[0].IsEnableEmail;
            (container.FindControl("txtEmailSubjectTemplate") as TextBox).Text = list[0].EmailSubjectTemplate;
            (container.FindControl("txtEmailBodyTemplate") as UEditor).Text = list[0].EmailBodyTemplate;

            //�����������
            (container.FindControl("ckboxIsEnableSMS") as CheckBox).Checked = list[0].IsEnableSMS;
            (container.FindControl("txtSMSSubjectTemplate") as TextBox).Text = list[0].SMSSubjectTemplate;
            (container.FindControl("txtSMSBodyTemplate") as TextBox).Text = list[0].SMSBodyTemplate;

            //�ʼ��������
            (container.FindControl("ckboxIsEnableSiteInfo") as CheckBox).Checked = list[0].IsEnableSiteInfo;
            (container.FindControl("txtSiteInfoSubjectTemplate") as TextBox).Text = list[0].SiteInfoSubjectTemplate;
            (container.FindControl("txtSiteInfoBodyTemplate") as UEditor).Text = list[0].SiteInfoBodyTemplate;

        }
    }
}

