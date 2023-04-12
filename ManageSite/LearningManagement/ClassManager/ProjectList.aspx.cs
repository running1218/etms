using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Components.Basic.Implement.BLL;
using ETMS.Controls;
using ETMS.WebApp;
using System.Data;
using ETMS.Utility;
using ETMS.Components.StudyClass.Implement.BLL.StudyClass;

public partial class LearningManagement_ClassManager_ProjectList : ETMS.Controls.BasePage
{
    private static Sty_ClassLogic classLogic = new Sty_ClassLogic();

    protected void Page_Load(object sender, EventArgs e)
    {
        PageSet1.pageInit(this.CustomGridView1, PageDataSource);
        
        if (!Page.IsPostBack)
        {
            this.PageSet1.QueryChange();
        }
    }    

    private System.Collections.IList PageDataSource(int pageIndex, int pageSize, out int totalRecordCount)
    {
        string whereStr = string.Empty;
        whereStr = BasePage.getQueryConditionFromQueryControlList(tableQueryControlList);
        whereStr += string.Format(" and OrgId={0}", ETMS.AppContext.UserContext.Current.OrganizationID);
        DataTable dt = classLogic.GetClassItemList(pageIndex, pageSize, string.Empty, whereStr, out totalRecordCount);

        PageDataSourceProvider pageDataSource = new PageDataSourceProvider(dt, pageIndex, pageSize);
        return pageDataSource.PageDataSource;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        this.PageSet1.QueryChange();
        upList.Update();
    }
}