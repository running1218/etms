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
public partial class Admin_Site_SysConfig_Default : BasePage
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
    protected void Opeator_Command(object sender, CommandEventArgs e)
    {
        int configID = e.CommandArgument.ToInt();
        ConfigLogic.Remove(configID);
        ETMS.Utility.JsUtility.SuccessMessageBox("操作提示", "配置项删除成功！", "function(){ window.location.href=window.location.href; }");
    }
    protected void btn_SaveHandle(object sender, CommandEventArgs e)
    {
        GridView gv = (sender as Control).Parent.FindControl("gvConfigs") as GridView;
        int configGroupID = e.CommandArgument.ToInt();
        foreach (GridViewRow row in gv.Rows)
        {
            //配置键名
            string name = (row.FindControl("txtName") as TextBox).Text;
            if (string.IsNullOrEmpty(name))
            {
                //键名为空跳过！
                continue;
            }
            //配置名称
            string displayName = (row.FindControl("txtDisplayName") as TextBox).Text;
            if (string.IsNullOrEmpty(displayName))
            {
                displayName = name;//如果没有设置配置名称，则按照配置键名保存
            }
            //配置值
            string defaultValue = (row.FindControl("txtDefaultValue") as TextBox).Text;
            //描述
            string description = (row.FindControl("txtDescription") as TextBox).Text;
            int orderNo = int.Parse((row.FindControl("txtOrderNo") as TextBox).Text ?? "1");
            IOrderedDictionary keyDatas = gv.DataKeys[row.RowIndex].Values;
            //key:ConfigID
            int configID = keyDatas["ConfigID"].ToInt();
            if (configID == 0 && !string.IsNullOrEmpty(name))//新增配置项
            {
                //新增机构配置项
                Site_SysConfig configItem = new Site_SysConfig()
                {
                    OrganizationID = 0,//属于系统
                    ConfigGroupID = configGroupID,
                    Name = name,
                    DisplayName=displayName,
                    DefaultValue = defaultValue,
                    Description = description,
                    OrderNo = orderNo,
                    Modifier = ETMS.AppContext.UserContext.Current.RealName,
                    ModifyTime = DateTime.Now
                };
                ConfigLogic.Add(configItem);
            }
            else//修改机构配置项
            {
                Site_SysConfig defaultConfigItem = ConfigLogic.GetById(configID);
                defaultConfigItem.Name = name;
                defaultConfigItem.DisplayName = displayName;
                defaultConfigItem.DefaultValue = defaultValue;
                defaultConfigItem.Description = description;
                defaultConfigItem.Modifier = ETMS.AppContext.UserContext.Current.RealName;
                defaultConfigItem.ModifyTime = DateTime.Now;
                defaultConfigItem.OrderNo = orderNo;
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
            string filter = string.Format(" and organizationId='0' and ConfigGroupID='{0}'", configGroup.ConfigGroupID);
            int totalRecords = 0;
            IList<Site_SysConfig> list = ConfigLogic.GetEntityList(1, 1000, " OrderNo,configID", filter, out totalRecords);
            //追加1个空白位置，提供用户新增录入
            list.Add(new Site_SysConfig() { OrderNo = totalRecords + 1 });
            gv.DataSource = list;
            gv.DataBind();
        }
    }
}

