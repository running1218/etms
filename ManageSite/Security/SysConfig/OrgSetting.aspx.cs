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
using ETMS.Components.Basic.API.Entity.Security;
using ETMS.Components.Basic.Implement.BLL.Security;
public partial class Admin_Site_SysConfig_OrgSetting : BasePage
{
    private Site_SysConfigGroupLogic ConfigGroupLogic = new Site_SysConfigGroupLogic();
    private Site_SysConfigLogic ConfigLogic = new Site_SysConfigLogic();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            int totalRecords = 0;
            IList<Site_SysConfigGroup> configGroups = ConfigGroupLogic.GetEntityList(1, 1000, "", "", out totalRecords);

            //绑定Tabs（头）
            this.rptConfigGroup.DataSource = configGroups;
            this.rptConfigGroup.DataBind();

            //绑定Tabs（内容）
            this.rptConfigs.DataSource = configGroups;
            this.rptConfigs.DataBind();

        }

    }

    protected void btn_SaveHandle(object sender, EventArgs e)
    {
        GridView gv = (sender as Control).Parent.FindControl("gvConfigs") as GridView;
        foreach (GridViewRow row in gv.Rows)
        {
            string userValue = (row.FindControl("txtUserValue") as TextBox).Text;
            IOrderedDictionary keyDatas = gv.DataKeys[row.RowIndex].Values;
            //key:ConfigID
            int configID = keyDatas["ConfigID"].ToInt();
            Site_SysConfig defaultConfigItem = ConfigLogic.GetById(configID);
            if (defaultConfigItem.OrganizationID == 0 && ETMS.AppContext.UserContext.Current.UserID !=1)//当前机构没有配置过，取自系统配置
            {
                //新增机构配置项
                defaultConfigItem.ConfigID = 0;
                defaultConfigItem.OrganizationID = ETMS.AppContext.UserContext.Current.OrganizationID;
                defaultConfigItem.UserValue = userValue;
                defaultConfigItem.Modifier = ETMS.AppContext.UserContext.Current.RealName;
                defaultConfigItem.ModifyTime = DateTime.Now;
                ConfigLogic.Add(defaultConfigItem);
            }
            else
            {
                //修改机构配置项
                defaultConfigItem.UserValue = userValue;
                defaultConfigItem.Modifier = ETMS.AppContext.UserContext.Current.RealName;
                defaultConfigItem.ModifyTime = DateTime.Now;
                ConfigLogic.Update(defaultConfigItem);
            }
        }
        ETMS.Utility.JsUtility.SuccessMessageBox("操作提示", "配置项保存成功！", "function(){ window.location.href=window.location.href; }");
    }

    protected void rptConfigs_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Site_SysConfigGroup configGroup = e.Item.DataItem as Site_SysConfigGroup;
            CustomGridView gv = e.Item.FindControl("gvConfigs") as CustomGridView;
            //载入分组下的配置项

            gv.DataSource = ConfigLogic.GetOrganizationConfig(ETMS.AppContext.UserContext.Current.OrganizationID, configGroup.ConfigGroupID);
            gv.DataBind();

        }
    }

    protected void gvConfigs_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        var hfName = (HiddenField)e.Row.FindControl("hfConfigName");
        var txtUserValue = (TextBox)e.Row.FindControl("txtUserValue");
       

        if (hfName != null)
        { 
            var value = txtUserValue.Text;
            if (hfName.Value.ToLower() == "smtp_password")
            {
                txtUserValue.TextMode = TextBoxMode.Password;
                txtUserValue.Attributes.Add("value", value);
            }
        }
    }
}

