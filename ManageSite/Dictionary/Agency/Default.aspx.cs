using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using ETMS.Components.Basic.API.Entity.Security;
using ETMS.Components.Basic.Implement.BLL.Common;
using ETMS.Components.Basic.Implement.BLL.Security;
using ETMS.Controls;
using ETMS.Utility;
using ETMS.AppContext;

public partial class Agency_Default : BasePage
{
    private Site_AgencyLogic Logic = new Site_AgencyLogic();

    protected void lkbtnEdit_Command(object sender, CommandEventArgs e)
    {
        int itemIndex = e.CommandArgument.ToInt();
        IOrderedDictionary dataItem = this.GridViewList.DataKeys[itemIndex].Values;
        switch (e.CommandName)
        {
            case "Edit1":
                this.hfID.Value = dataItem[0].ToString();
                this.ColumnCode.Text = dataItem[1].ToString();
                this.ColumnName.Text = (string)dataItem[2];
                break;
            case "Delete1":
                try
                {  //删除
                    Logic.Delete(dataItem[0].ToInt());
                    this.GridViewList.CustomDataBind();//刷新数据
                    ETMS.Utility.JsUtility.SuccessMessageBox("删除成功！");
                }
                catch (Exception ex)
                {
                    ETMS.Utility.JsUtility.FailedMessageBox("删除失败！原因:" + ex.Message);
                }
                break;
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            Logic.Save(new Site_Agency()
            {
                AgencyID = hfID.Value.ToInt(),
                AgencyCode = this.ColumnCode.Text.Trim(),
                AgencyName = this.ColumnName.Text.Trim(),
                OrgID = UserContext.Current.OrganizationID,
                CreateTime = DateTime.Now,
                CreateUser = UserContext.Current.UserName,
                CreateUserID = UserContext.Current.UserID,
                ModifyTime = DateTime.Now,
                ModifyUser = UserContext.Current.UserName
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
            var list = Logic.GetPageList(UserContext.Current.OrganizationID);//原型阶段
            ETMS.Controls.PageDataSourceProvider pageDataSourceProvider = new PageDataSourceProvider(list, pageSize, pageSize);
            return pageDataSourceProvider.PageDataSource;
        }
       ), null);
        this.GridViewList.EmptyData = new List<Site_Agency>();

        if (!Page.IsPostBack)
        {
            this.GridViewList.CustomDataBind();
        }
    }
}