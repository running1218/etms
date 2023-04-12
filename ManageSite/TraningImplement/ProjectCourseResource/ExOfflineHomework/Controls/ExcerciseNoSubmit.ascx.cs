using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Components.ExOfflineHomework.Implement.BLL;
using ETMS.Controls;
using System.Collections;
using System.Data;
using ETMS.Components.Basic.Implement.BLL.Security;
using ETMS.Components.Basic.API.Entity.Security;
using ETMS.Utility;

public partial class QuestionDB_ExOfflineHomework_Controls_ExcerciseNoSubmit : System.Web.UI.UserControl
{
    private static Res_e_OffLineJobLogic Logic = new Res_e_OffLineJobLogic();   
    /// <summary>
    /// 离线作业编码JobID
    /// </summary>
    public Guid JobID
    {
        get
        {
            return (Guid)ViewState["JobID"];
        }
        set
        {
            ViewState["JobID"] = value;
        }
    }

    public Guid ItemCourseOffLineJobID
    {
        get
        {
            return (Guid)ViewState["ItemCourseOffLineJobID"];
        }
        set
        {
            ViewState["ItemCourseOffLineJobID"] = value;
        }
    }
  
   
    protected void Page_Load(object sender, EventArgs e)
    {
        this.PageSet3.pageInit(this.GridViewList, new IPageDataSource(this.getDataSource1));
        if (!Page.IsPostBack)
        {
            this.PageSet3.QueryChange();
        }
        //单机构版本隐藏“组织机构”列
        if (ETMS.Product.ProductDefine.IsSingleOrganizationVersion)
        {
            this.GridViewList.Columns[2].Visible = false;
        }
    }
    
    private IList getDataSource1(int pageIndex, int pageSize, out int totalRecords)
    {
        DataTable dataList = Logic.GetUnSubmitStudentListbyItemCourseOffLineJobID(ItemCourseOffLineJobID);
        totalRecords = dataList.Rows.Count;
        PageDataSourceProvider psp = new PageDataSourceProvider(dataList, pageIndex, pageSize);       
        return psp.PageDataSource;
    }
   
}