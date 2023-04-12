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
public partial class Dictionary_UserControl : UserControl
{
    private DictionaryLogic Logic = new DictionaryLogic();

    /// <summary>
    /// 字典类型（外部出入）
    /// </summary>
    public string DicType { get; set; }

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
                    Logic.DictionaryDelete(new DictionaryParm() { TableEnglishName = DicType, ColumnCodeValue = dataItem[0].ToString() });
                    this.GridViewList.CustomDataBind();//刷新数据
                    ETMS.Utility.JsUtility.SuccessMessageBox("数据字典项删除成功！");
                }
                catch (Exception ex)
                {
                    ETMS.Utility.JsUtility.FailedMessageBox("数据字典项删除失败！原因:" + ex.Message);
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
                OrganizationID = ETMS.AppContext.UserContext.Current.OrganizationID,
                TableEnglishName = this.DicType
            });
            ETMS.Utility.JsUtility.SuccessMessageBox("操作提示：", "数据字典保存成功！", "function(){window.location.href=window.location.href;}");
        }
        catch (Exception ex)
        {
            ETMS.Utility.JsUtility.FailedMessageBox("数据字典保存失败！原因:" + ex.Message);
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        this.GridViewList.Initialization(new ETMS.Controls.IPageDataSource(delegate(int pageIndex, int pageSize, out int totalRecordCount)
        {
            totalRecordCount = 0;
            System.Data.DataTable dt = Logic.GetDictionaryList(DicType);//原型阶段
            ETMS.Controls.PageDataSourceProvider pageDataSourceProvider = new PageDataSourceProvider(dt, pageSize, pageSize);
            return pageDataSourceProvider.PageDataSource;
        }
       ), null);
        this.GridViewList.EmptyData = new DictionaryParm[] { new DictionaryParm() };

        if (!Page.IsPostBack)
        {
            this.GridViewList.CustomDataBind();
        }
    }
}