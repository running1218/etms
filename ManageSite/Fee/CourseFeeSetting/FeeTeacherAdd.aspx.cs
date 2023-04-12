﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using ETMS.Components.Basic.Implement.BLL;
using ETMS.Controls;
using ETMS.WebApp;

public partial class FeeTeacherAdd : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        PageSet1.pageInit(this.CustomGridView1, PageDataSource);
        PageSet1.PageSize = 5;

        if (!Page.IsPostBack)
        {
            this.PageSet1.QueryChange();
        }
    }

    private BizDataSourceEnum DataSourceEnum
    {
        get
        {
            return BizDataSourceEnum.tb_CourseFeeSetInsideTeacher;
        }
    }
    private System.Collections.IList PageDataSource(int pageIndex, int pageSize, out int totalRecordCount)
    {
        totalRecordCount = 0;
        DataTable dt = ReadExcel.GetData(DataSourceEnum);

        PageDataSourceProvider pageDataSource = new PageDataSourceProvider(dt, pageIndex, pageSize);
        totalRecordCount = dt.Rows.Count;
        return pageDataSource.PageDataSource;
    }

    protected void lbnSave_Click(object sender, EventArgs e)
    {
        ETMS.Utility.JsUtility.SuccessMessageBoxAndCloseWindowAndTriggerRefreshEvent("课酬标准增加，点击“确定”后，重新刷新当前页数据！");
    }
}