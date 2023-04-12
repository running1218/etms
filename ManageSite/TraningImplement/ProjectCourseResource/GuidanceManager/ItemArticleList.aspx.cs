using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Course;
using ETMS.Controls;
using System.Data;
using System.Collections;
using ETMS.Components.Basic.Implement.BLL.Bulletin;
using ETMS.Utility;
using ETMS.Components.Basic.Implement.BLL.Teacher;

public partial class QuestionDB_GuidanceManager_ItemArticleList : ETMS.Controls.BasePage
{
    public static Tr_ItemCourseLogic itemCourseLogic = new Tr_ItemCourseLogic();
    public static Inf_BulletinLogic bulletinLogic = new Inf_BulletinLogic();
    protected void Page_Load(object sender, EventArgs e)
    {
        this.PageSet1.pageInit(this.GridViewList, new IPageDataSource(this.getDataSource1));
        if (!Page.IsPostBack)
        {
            this.PageSet1.QueryChange();
        }
    }
    
    private IList getDataSource1(int pageIndex, int pageSize, out int totalRecords)
    {
        DataTable dataList = itemCourseLogic.GetItemCourseListByUserID(ETMS.AppContext.UserContext.Current.UserID, pageIndex, pageSize, out totalRecords); //Logic.GetPagedList(pageIndex, pageSize, SortExpression, Crieria, out totalRecords);

        PageDataSourceProvider psp = new PageDataSourceProvider(dataList, pageIndex, pageSize);
        return psp.PageDataSource;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        this.PageSet1.QueryChange();
    }
}