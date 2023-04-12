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
using ETMS.Components.Basic.API.Entity;
using ETMS.Controls;
using ETMS.Components.Basic.API.Entity.Security;
using ETMS.Components.Basic.Implement.BLL.Security;
public partial class Admin_Function_URL_Default : ETMS.Controls.BasePage
{
    private static FunctionLogic functionLogic = new FunctionLogic();
    private static FunctionGroupLogic functionGroupLogic = new FunctionGroupLogic();
    private FunctionPageUrlRelationLogic functionPageUrlRelationLogic = new FunctionPageUrlRelationLogic(null);
    private static PageUrlLogic pageUrlLogic = new PageUrlLogic();

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
             * 1、id    {1,2..}
             * 1、groupid    {1,2..}
             */
            return new RequestParameter[] 
            {
                 RequestParameter.CreateRangeRequestParameter("id", Request.QueryString["id"],RequestParameter.PositiveInt32RangeVerify)
                //,RequestParameter.CreateRangeRequestParameter("groupid", Request.QueryString["groupid"],RequestParameter.PositiveInt32RangeVerify)
            };
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        ///*
        //* 检查当前角色是否越权
        //*/
        //if (!WebSession.Administrator.MapRole.IsSysAdminRole)//仅系统管理员有此页访问权限
        //{
        //    ETMS.WebBase.JsUtility.MessageBoxAndRedirect("访问被拒绝，原因：权限不足！", string.Format("{0}/Admin/Default.aspx", ETMS.Utility.WebUtility.AppPath), this.Page);
        //    return;
        //}

        int functionID = this.PageRequestArgs[0].ParameterValue.ToInt();
        Function function = functionLogic.GetFunctionByID(functionID);

        functionPageUrlRelationLogic.Manager = function;

        this.gvRoles.Initialization(new ETMS.Controls.IPageDataSource(delegate(int pageIndex, int pageSize, out int totalRecordCount)
        {
            totalRecordCount = 0;
            return functionPageUrlRelationLogic.GetAllMembers("");
        }
        ), null);
        this.gvRoles.EmptyData = new PageUrl[] { new PageUrl() };

        if (!Page.IsPostBack)
        {
            this.gvRoles.CustomDataBind();
        }

    }
    protected void lkbtnEdit_Command(object sender, CommandEventArgs e)
    {
        int pageID = e.CommandArgument.ToInt();
        switch (e.CommandName)
        {
            case "Edit1":
                PageUrl pageUrl = (PageUrl)pageUrlLogic.GetPageUrlByID(pageID);
                this.txtPageID.Value = pageUrl.PageID.ToString();
                this.txtUrl.Text = pageUrl.PageURL;
                this.rbtnlistIsMainPage.SelectedValue = pageUrl.IsMainPage.ToString();
                this.txtRemark.Text = pageUrl.Description;
                break;
            case "Delete1":
                //删除
                pageUrlLogic.Remove(new PageUrl() { PageID = pageID });
                this.gvRoles.CustomDataBind();//刷新数据
                ETMS.Utility.JsUtility.SuccessMessageBox("功能URL删除成功！");
                break;
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        //保存操作
        if (string.IsNullOrEmpty(this.txtPageID.Value))//添加模式
        {
            PageUrl pageUrl = new PageUrl();
            pageUrl.IsMainPage = int.Parse(this.rbtnlistIsMainPage.SelectedValue);
            pageUrl.PageURL = this.txtUrl.Text;
            pageUrl.Description = this.txtRemark.Text;
            pageUrl.Status = 1;
            functionPageUrlRelationLogic.Associate(pageUrl);
            ETMS.Utility.JsUtility.SuccessMessageBox("操作提示：", "功能URL添加成功！", "function(){window.location.href=window.location.href;}");
        }
        else
        {
            PageUrl pageUrl = (PageUrl)functionPageUrlRelationLogic.GetMemberByPkValue(new PageUrl() { PageID = this.txtPageID.Value.ToInt() });
            pageUrl.IsMainPage = int.Parse(this.rbtnlistIsMainPage.SelectedValue);
            pageUrl.PageURL = this.txtUrl.Text;
            pageUrl.Description = this.txtRemark.Text;
            pageUrlLogic.Save(pageUrl);
            ETMS.Utility.JsUtility.SuccessMessageBox("操作提示：", "功能URL修改成功！", "function(){window.location.href=window.location.href;}");
        }
    }
}
