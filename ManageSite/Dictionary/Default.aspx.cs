using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using ETMS.Components.Basic.API.Entity.Common;
using ETMS.Components.Basic.Implement.BLL.Common;
using ETMS.Components.Basic.Implement.BLL;
using ETMS.Controls;
using ETMS.Utility;

public partial class Dictionary_Default : BasePage
{
    protected override RequestParameter[] PageRequestArgs
    {
        get
        {
            /*
             * 需要验证的页面参数包含：
             *    参数名  参数范围 
             * 1、dicType  数据字典类型
             */
            return new RequestParameter[] 
            {
                 RequestParameter.CreateDefaultRequestParameter("dicType")
            };
        }
    }
    private DictionaryLogic Logic = new DictionaryLogic();

    protected void lkbtnEdit_Command(object sender, CommandEventArgs e)
    {
        int itemIndex = e.CommandArgument.ToInt();
        IOrderedDictionary dataItem = this.GridViewList.DataKeys[itemIndex].Values;
        switch (e.CommandName)
        {
            case "Edit1":
                this.ColumnCode.Text = dataItem[0].ToString();
                this.ColumnName.Text = (string)dataItem[1];
                break;
            case "Delete1":
                try
                {  //删除
                    Logic.DictionaryDelete(new DictionaryParm() { TableEnglishName = Request.QueryString["dicType"], ColumnCodeValue = (string)dataItem[0] });
                    this.GridViewList.CustomDataBind();//刷新数据
                    ETMS.Utility.JsUtility.SuccessMessageBox("项删除成功！");
                }
                catch (Exception ex)
                {
                    ETMS.Utility.JsUtility.FailedMessageBox("项删除失败！原因:" + ex.Message);
                }
                break;
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            Logic.DictionaryEdit(new DictionaryParm()
            {
                ColumnCodeValue = this.ColumnCode.Text,
                ColumnNameValue = this.ColumnName.Text,
                OrganizationID = ETMS.AppContext.UserContext.Current.OrganizationID
            });
            ETMS.Utility.JsUtility.SuccessMessageBox("操作提示：", "保存成功！", "function(){window.location.href=window.location.href;}");
        }
        catch (Exception ex)
        {
            ETMS.Utility.JsUtility.FailedMessageBox("保存失败！原因:" + ex.Message);
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        this.GridViewList.Initialization(new ETMS.Controls.IPageDataSource(delegate(int pageIndex, int pageSize, out int totalRecordCount)
        {
            totalRecordCount = 0;
            System.Data.DataTable dt = Logic.GetDictionaryList(Request.QueryString["dicType"]);//原型阶段
            ETMS.Controls.PageDataSourceProvider pageDataSourceProvider = new PageDataSourceProvider(dt, pageSize, pageSize);
            return pageDataSourceProvider.PageDataSource;
        }
       ), null);
        this.GridViewList.EmptyData = new DictionaryParm[] { new DictionaryParm() };

        if (!Page.IsPostBack)
        {
            this.GridViewList.CustomDataBind();
        }

        this.Label1.Text = Request.QueryString["dicType"];//设置表名
    }
}