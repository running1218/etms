using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using ETMS.Utility;
using ETMS.AppContext;
using ETMS.Controls;
using ETMS.Components.Evaluation.Implement.BLL;
using ETMS.Components.Evaluation.API.Entity;

public partial class Valuation_ValuationList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            PageSet1.pageInit(this.CustomGridView1, PageDataSource);

            if (!Page.IsPostBack)
            {
                this.PageSet1.QueryChange();

            }

        }
    }

    /// <summary>
    /// 查询结果
    /// </summary>
    /// <param name="pageIndex"></param>
    /// <param name="pageSize"></param>
    /// <param name="totalRecordCount"></param>
    /// <returns></returns>
    private System.Collections.IList PageDataSource(int pageIndex, int pageSize, out int totalRecordCount)
    {
        Evaluation_PlateLogic plateLogic = new Evaluation_PlateLogic();

        DataTable dt = plateLogic.GetPagedList(pageIndex, pageSize, "", "", out totalRecordCount);

        PageDataSourceProvider pageDataSource = new PageDataSourceProvider(dt, pageIndex, pageSize);
        return pageDataSource.PageDataSource;
    }

    protected string getEvaluationItems(string PlateID)
    {
        Evaluation_ItemLogic itemLogic = new Evaluation_ItemLogic();
        List<Evaluation_Item> items = itemLogic.GetByPlate(PlateID.ToGuid());
        string allItems = string.Empty;
        for (int i = 0; i < items.Count; i++)
        {
            allItems += (allItems == string.Empty ? "" : "<br/>&nbsp;") + items[i].ItemName;
        }
        return allItems;
        
    }

}