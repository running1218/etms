using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Controls;
using ETMS.Components.Reporting.Implement.BLL;
using ETMS.Utility;
using ETMS.Product;
using System.Data;
using Open.Excel.Provider;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Course.Hours;
public partial class TrainingFeeDetails : BasePage
{
    #region 页面参数
    /// <summary>
    /// 排序条件
    /// </summary>
    protected string SortExpression
    {
        get
        {
            if (ViewState["SortExpression"] == null)
            {
                ViewState["SortExpression"] = " ";
            }
            return (string)ViewState["SortExpression"];
        }
        set
        {
            ViewState["SortExpression"] = value;
        }
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        PageSet1.pageInit(CustomGridView1, PageDataSource);
        PageSet1.PageSize = 50;
        if (!Page.IsPostBack)
        {
            this.PageSet1.QueryChange();
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        this.PageSet1.QueryChange();
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
        ////讲师课酬支付管理：按培训项目、讲师姓名、课程名称排序。
        SortExpression = " f.TrainingItemID asc ,d.RealName asc,g.CourseName asc ";
        string Crieria = BasePage.getQueryConditionFromQueryControlList(tableQueryControlList);

        //PayStatus 支付状态 课时安排状态必须是“已执行” TeacherSourceID = 1 必须是内部讲师
        Crieria += string.Format("{0} And a.PayStatus={1} And a.CourseHoursStatusID=1 And c.TeacherSourceID = 1  and d.OrganizationID={2} ", Crieria, 1, ETMS.AppContext.UserContext.Current.OrganizationID);

        Tr_ItemCourseHoursLogic itemCourseHoursLogic = new Tr_ItemCourseHoursLogic();

        DataTable dt = itemCourseHoursLogic.GetItemCourseHoursALLInfoList(pageIndex, pageSize, SortExpression, Crieria, out totalRecordCount);

        PageDataSourceProvider pageDataSource = new PageDataSourceProvider(dt, pageIndex, pageSize);

        return pageDataSource.PageDataSource;
    }

    protected void btnExportNew_Click(object sender, EventArgs e)
    {
        int totalRecordCount = 0;
        SortExpression = " f.TrainingItemID asc ,d.RealName asc,g.CourseName asc ";
        string Crieria = BasePage.getQueryConditionFromQueryControlList(tableQueryControlList);

        //PayStatus 支付状态 课时安排状态必须是“已执行” TeacherSourceID = 1 必须是内部讲师
        Crieria = string.Format("{0} And a.PayStatus={1} And a.CourseHoursStatusID=1 And c.TeacherSourceID = 1  and d.OrganizationID={2} ", Crieria, 1, ETMS.AppContext.UserContext.Current.OrganizationID);

        Tr_ItemCourseHoursLogic itemCourseHoursLogic = new Tr_ItemCourseHoursLogic();

        DataTable dt = itemCourseHoursLogic.GetItemCourseHoursALLInfoList(1, (int.MaxValue -1), SortExpression, Crieria, out totalRecordCount);
        Export(dt);
    }
    private void Export(DataTable source)
    {
        source.Columns.Add("TrainingCourseTime", typeof(string));
        source.Columns.Add("TrainingDateFormat", typeof(string));

        foreach (DataRow row in source.Rows)
        {
            row["TrainingCourseTime"] = string.Format("{0}-{1}", string.IsNullOrEmpty(row["TrainingBeginTime"].ToString()) ? "" : row["TrainingBeginTime"].ToDateTime().ToString("HH:MM"), string.IsNullOrEmpty(row["TrainingEndTime"].ToString()) ? "" : row["TrainingEndTime"].ToDateTime().ToString("HH:MM"));
            row["TrainingDateFormat"] = string.IsNullOrEmpty(row["TrainingDate"].ToString()) ? string.Empty : row["TrainingDate"].ToDate();
        }
        //导出
        string xmlTemplatePath = Server.MapPath(@"ExportXML/TrainingFeeDetails.xml");

        string fileName = "培训项目费用明细";
        string strDateTime = DateTime.Now.ToString("yyyyMMddHHmmss");
        string pathFileName = Server.MapPath("~/Temp/" + fileName + strDateTime + ".xls");

        ExcelProvider provider = new ExcelProvider(xmlTemplatePath, fileName, source);
        provider.DataFormate = "yyyy-MM-dd HH:MM:ss";
        provider.SheetSize = 60000;
        provider.XlsSavePath = pathFileName;
        provider.XlsWriteModel = XlsWriteModel.FileOutput;
        provider.ExportExcel();

        //压缩
        string sourcePath = Server.MapPath("../Temp/");
        string downFileName = string.Format(fileName + "{0}.xls", strDateTime);
        string downPath = Server.MapPath("../Temp/");
        string downFileNameCompress = string.Format(fileName + "{0}.zip", strDateTime);

        FileUtility.Compress(sourcePath, downFileName, downPath, downFileNameCompress, CompressType.Zip, CompressModel.File);
        System.IO.File.Delete(pathFileName);
        //下载
        FileDownLoadUtility.ExportFile(System.IO.Path.Combine(downPath, downFileNameCompress));
    }
}