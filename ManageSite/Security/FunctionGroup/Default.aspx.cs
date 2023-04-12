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
using ETMS.Controls;
using ETMS.Utility;
using ETMS.Components.Basic.API.Entity.Common;
using ETMS.Components.Basic.Implement.BLL.Common;
using ETMS.Components.Basic.API.Entity.Security;
using ETMS.Components.Basic.Implement.BLL.Security;
public partial class Admin_FunctionGroup_Default : ETMS.Controls.BasePage
{
    private static FunctionGroupLogic functionGroupLogic = new FunctionGroupLogic();

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
                RequestParameter.CreateRangeRequestParameter("groupid", Request.QueryString["groupid"],RequestParameter.NaturalInt32RangeVerify)
            };
        }
    }
    protected override void OnInit(EventArgs e)
    {
        //如果没有传递orgid参数，则默认重定向到此
        if (string.IsNullOrEmpty(Request.QueryString["groupid"]))
        {
            Response.Redirect(this.ActionHref("default.aspx?groupid=1"));
        }
        base.OnInit(e);
    }
    protected void OrganizationOpeator_Command(object sender, CommandEventArgs e)
    {
        int id = e.CommandArgument.ToInt();
        switch (e.CommandName)
        {
            case "SwitchStatus":
                functionGroupLogic.SwitchFunctionGroupStatus(id, ETMS.AppContext.UserContext.Current.RealName);
                ETMS.Utility.JsUtility.SuccessMessageBox("设置功能组状态操作成功！");
                break;
        }
        //刷新
        this.gvRoles.CustomDataBind();
    }
    protected void Page_Load(object sender, EventArgs e)
    {

        // /*
        //* 检查当前角色是否越权
        //*/         
        //if (!WebSession.Administrator.MapRole.IsSysAdminRole)//仅系统管理员有此页访问权限
        //{
        //    ETMS.WebBase.JsUtility.MessageBoxAndRedirect("访问被拒绝，原因：权限不足！", string.Format("{0}/Admin/Default.aspx", ETMS.Utility.WebUtility.AppPath), this.Page);
        //    return;
        //}

        this.gvRoles.Initialization(new ETMS.Controls.IPageDataSource(delegate(int pageIndex, int pageSize, out int totalRecordCount)
        {
            Node root = new FunctionGroup();
            root.NodeID = this.FunctionGroupTree1.GroupID;
            IList dataList = (IList)functionGroupLogic.GetNodeTreeForManager(root, true).ChildNodes;
            totalRecordCount = dataList.Count;
            PageDataSourceProvider psp = new PageDataSourceProvider(dataList, pageIndex, pageSize);
            return psp.PageDataSource;
        }
        ), null);
        this.gvRoles.CustomAllowPaging = false;
        this.gvRoles.AutoGenerateColumns = false;
        this.gvRoles.EmptyData = new FunctionGroup[] { new FunctionGroup() };


        if (!Page.IsPostBack)
            this.gvRoles.CustomDataBind();

    }
}
