using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Components.Basic.Implement.BLL;
using System.Data;
using ETMS.WebApp;
using ETMS.Controls;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Course.Hours;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Course.Hours.Student;

public partial class TraningImplement_SignInManager_TraningProjectList : System.Web.UI.Page
{
    private static Tr_ItemCourseHoursLogic itemCourseHoursLogic = new Tr_ItemCourseHoursLogic();
    public static Tr_ItemCourseHoursStudentLogic itemCourseHoursStudentLogic = new Tr_ItemCourseHoursStudentLogic();
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
        whereStr += string.Format("  and f.OrgID={0}", ETMS.AppContext.UserContext.Current.OrganizationID);
        DataTable dt = itemCourseHoursLogic.GetItemCourseHoursALLInfoList(pageIndex, pageSize, string.Empty, whereStr, out totalRecordCount);

        PageDataSourceProvider pageDataSource = new PageDataSourceProvider(dt, pageIndex, pageSize);
        return pageDataSource.PageDataSource;
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        this.PageSet1.QueryChange();
        upList.Update();
    }
   
}