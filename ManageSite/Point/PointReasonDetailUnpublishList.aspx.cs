using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ETMS.Utility;
using ETMS.Controls;
using ETMS.Components.Basic.Implement.BLL.TrainingItem;
using ETMS.Components.Point.Implement.BLL;
using ETMS.Components.Point.API.Entity;


public partial class Point_PointReasonDetailUnpublishList : ETMS.Controls.BasePage
{
    private static Point_Student_PointReasonDetailLogic pointreasonDetailLogic = new Point_Student_PointReasonDetailLogic();

    private Guid TrainingItemID
    {
        get { return Request.QueryString["TrainingItemID"].ToGuid(); }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        PageSet1.pageInit(this.CustomGridView1, PageDataSource);

        if (!Page.IsPostBack)
        {
            InitialControl();
            this.PageSet1.QueryChange();
        }        
        aBack.HRef = "PointReasonDetailPublishList.aspx";
    }
    private void InitialControl()
    {
        this.lblItemName.Text = new Tr_ItemLogic().GetById(TrainingItemID).ItemName;
    }
    private System.Collections.IList PageDataSource(int pageIndex, int pageSize, out int totalRecordCount)
    {
        List<PointStudentReasonStudentInfo> dt = pointreasonDetailLogic.GetNotPublishPointStudentList(TrainingItemID,pageIndex, pageSize, out totalRecordCount);
        PageDataSourceProvider pageDataSource = new PageDataSourceProvider(dt, pageIndex, pageSize);
        return pageDataSource.PageDataSource;
    }
}