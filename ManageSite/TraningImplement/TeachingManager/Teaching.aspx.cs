﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Components.Basic.Implement.BLL;
using ETMS.Controls;
using ETMS.WebApp;
using System.Data;
using System.Web.UI.HtmlControls;

public partial class TraningImplement_TeachingManager_Teaching : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //PageSet1.pageInit(this.CustomGridView1, PageDataSource);

        if (!Page.IsPostBack)
        {
            //this.PageSet1.QueryChange();
        }
    }
   
    private System.Collections.IList PageDataSource(int pageIndex, int pageSize, out int totalRecordCount)
    {
        DataTable dt = new DataTable();

        PageDataSourceProvider pageDataSource = new PageDataSourceProvider(dt, pageIndex, pageSize);
        totalRecordCount = dt.Rows.Count;
        return pageDataSource.PageDataSource;
    }
}