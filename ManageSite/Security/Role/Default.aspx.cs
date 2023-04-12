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


using ETMS.Components.Basic.API.Entity;
using ETMS.Components.Basic.API.Entity.Security;
using ETMS.Components.Basic.API.Entity.Security;
using ETMS.Components.Basic.Implement.BLL.Security;
using ETMS.Controls;
public partial class Admin_Role_Default : ETMS.Controls.BasePage
{
    private static RoleLogic roleLogic = new RoleLogic();

    protected void Page_Load(object sender, EventArgs e)
    {
        ///*
        // * 检查当前角色是否越权
        // */
        //Node currentRequestNode = (parentID == 0 ? null : roleLogic.GetNodeByID(parentID));
        //IRole userRoleNode = WebSession.Administrator.MapRole;
        ////如果当前用户角色编码不是以用户登陆的角色编码开始，则提示,并返回前一个操作页面
        //if ((!userRoleNode.IsSysAdminRole && parentID == 0) || (currentRequestNode != null && !currentRequestNode.NodeCode.StartsWith(this.UserRole)))
        //{
        //    ETMS.WebBase.JsUtility.MessageBoxAndRedirect("访问被拒绝，原因：权限不足！", string.Format("{0}/Admin/Default.aspx", ETMS.Utility.WebUtility.AppPath), this.Page);
        //    return;
        //}

        /*
         * 角色列表绑定初始化工作
         */
        //提取数据接口
        this.PageSet1.pageInit(this.gvRoles, PageDataSource);
        //数据为空时使用的空数据集合
        this.gvRoles.EmptyData = new Role[] { new Role() };

        //页面首次载入是绑定数据
        if (!Page.IsPostBack)
        {
            this.PageSet1.QueryChange();
        }
    }
    private IList PageDataSource(int pageIndex, int pageSize, out int totalRecordCount)
    {
        //2、载入角色列表
        ArrayList list = new ArrayList();
        ////2.1 系统内置角色
        //Role[] sysRoles = roleLogic.GetOrganizationRoles(0);
        //list.AddRange(sysRoles);
        //2.2 机构自建角色
        Role[] orgRoles = roleLogic.GetOrganizationRoles(ETMS.AppContext.UserContext.Current.OrganizationID);
        list.AddRange(orgRoles);
        PageDataSourceProvider psp = new PageDataSourceProvider(list, pageIndex, pageSize);
        totalRecordCount = list.Count; 
        return psp.PageDataSource;
    }
}
