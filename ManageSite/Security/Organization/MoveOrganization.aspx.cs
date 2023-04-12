﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Controls;

using ETMS.Components.Basic.API.Entity;
using ETMS.Components.Basic.API.Entity.Security;
using ETMS.Components.Basic.Implement.BLL.Security;
public partial class Security_Organization_MoveOrganization : ETMS.Controls.BasePage
{
    private static OrganizationLogic organizationLogic = new OrganizationLogic();
    /// <summary>
    /// 页面验证参数定义
    /// </summary>
    protected override RequestParameter[] PageRequestArgs
    {
        get
        {
            /*
             * 需要验证的页面参数包含：
             *    参数名  参数范围
             * 1、parentid    {0,1,2..}
             */
            return new RequestParameter[] 
            {
                RequestParameter.CreateRangeRequestParameter("id", Request.QueryString["id"],RequestParameter.NaturalInt32RangeVerify)
            };
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Organization org = (Organization)organizationLogic.GetNodeByID(int.Parse(Request.QueryString["id"]));
            this.Label1.Text = org.DisplayPath;

        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        organizationLogic.MoveNode(this.OrganizationDropDownList1.OrganizationID, int.Parse(Request.QueryString["id"]));
        ETMS.Utility.JsUtility.SuccessMessageBoxAndCloseWindow("机构调整成功！", "function(){window.parent.location.href=window.parent.location.href;}");//刷新页面
    }
}