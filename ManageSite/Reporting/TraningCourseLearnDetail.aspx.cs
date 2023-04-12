using System;
using System.Data;
using System.Web.UI.WebControls;
using ETMS.Components.Basic.Implement.BLL.Dictionary;
using ETMS.Components.Basic.Implement.BLL.Security;
using ETMS.Components.Reporting.API.Entity;
using ETMS.Components.Reporting.Implement.BLL;
using ETMS.Controls;
using ETMS.Utility;
using Open.Excel.Provider;
using System.IO;
using System.Configuration;
using System.Web;
public partial class Reporting_TraningCourseLearnDetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        PageSet1.pageInit(this.CustomGridView1, PageDataSource);
        PageSet1.PageSize = 50;
        if (!IsPostBack)
        {
            //单机构版本隐藏机构查询条件
            if (ETMS.Product.ProductDefine.IsSingleOrganizationVersion)
            {
                this.trOrg.Visible = false;
                ddl_OrganizationID.SelectedValue = ETMS.AppContext.UserContext.Current.OrganizationID.ToString();
            }
            ddl_OrganizationID_SelectedIndexChanged(sender, e);//触发Selected事件
            this.PageSet1.QueryChange();
        }
    }

    #region 机构 部门 岗位

    /// <summary>
    /// 机构选中事件
    /// </summary>
    protected void ddl_OrganizationID_SelectedIndexChanged(object sender, EventArgs e)
    {
        DepartmentLogic DepamentLogic = new DepartmentLogic();
        Dic_PostLogic PostLogic = new Dic_PostLogic();
        //载入选中机构下部门，岗位数据
        int orgID = int.Parse(this.ddl_OrganizationID.SelectedValue);
        DataTable dt = DepamentLogic.GetAllEnableDepartmentsByOrganizationID(orgID);
        this.ddl_DepartmentID.DataSource = dt;
        this.ddl_DepartmentID.DataTextField = "ColumnNameValue";
        this.ddl_DepartmentID.DataValueField = "ColumnCodeValue";
        this.ddl_DepartmentID.DataBind();
        this.ddl_DepartmentID.Items.Insert(0, new ListItem("全部", "-1"));
        this.ddl_DepartmentID.SelectedIndex = 0;

        dt = PostLogic.GetAllEnablePostByOrgID(orgID);
        this.ddl_PostID.DataSource = dt;
        this.ddl_PostID.DataTextField = "ColumnNameValue";
        this.ddl_PostID.DataValueField = "ColumnCodeValue";
        this.ddl_PostID.DataBind();
        this.ddl_PostID.Items.Insert(0, new ListItem("全部", "-1"));
        this.ddl_PostID.SelectedIndex = 0;
    }
    #endregion

    //查询
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        this.PageSet1.QueryChange();
    }

    private System.Collections.IList PageDataSource(int pageIndex, int pageSize, out int totalRecordCount)
    {
        OnlineStudyLogic onLineLogic = new OnlineStudyLogic();
        DataTable dt = onLineLogic.GetTraningCourseLearnPageList(GetOnlineItemCourseWhere(), ETMS.AppContext.UserContext.Current.OrganizationID, pageIndex, pageSize, out totalRecordCount);
        PageDataSourceProvider pageDataSource = new PageDataSourceProvider(dt, pageIndex, pageSize);
        return pageDataSource.PageDataSource;
    }

    /// <summary>
    /// 获得查询条件
    /// </summary>
    /// <returns></returns>
    private OnlineItemCourse GetOnlineItemCourseWhere()
    {
        OnlineItemCourse itemCourse = new OnlineItemCourse();
        itemCourse.ItemCode = txtItemNameCode.Text.Trim();
        itemCourse.ItemName = txtItemNameCode.Text.Trim();
        itemCourse.ItemCourseCode = txtCourseNameCode.Text.Trim();
        itemCourse.ItemCourseName = txtCourseNameCode.Text.Trim();
        itemCourse.ItemCourseAttrID = ddlCourseAttrID.SelectedValue.ToInt();
        itemCourse.ItemCourseTypeID = ddl_CourseTypeID.SelectedValue.ToInt();
        itemCourse.ItemCourseBeginTime = ttbCourseBeginTime.Text.Trim() == "" ? "1900-01-01".ToDateTime() : (ttbCourseBeginTime.Text+" 00:00:00").ToDateTime();
        itemCourse.ItemCourseEndTime = ttbCourseEndTime.Text.Trim() == "" ? DateTime.MaxValue : (ttbCourseEndTime.Text+" 23:59:59").ToDateTime();
        itemCourse.ItemCourseScoreStatus = ddlItemCourseScoreStatus.SelectedValue.ToInt();
        itemCourse.OrganizationID = ddl_OrganizationID.SelectedValue.ToInt();
        itemCourse.DepartmentID = ddl_DepartmentID.SelectedValue.ToInt();
        itemCourse.PostID = ddl_PostID.SelectedValue.ToInt();
        itemCourse.PostTypeID = ddlPostType.SelectedValue.ToInt();
        itemCourse.RankID = ddlRank.SelectedValue.ToInt();
        itemCourse.RealName = txtRealName.Text.Trim();
        itemCourse.ItemCourseTeachModelID = ddl_TeachModelID.SelectedValue.ToInt();

        return itemCourse;
    }

    protected void btn_Export_Click(object sender, EventArgs e)
    {
        btn_Export.Enabled = false;
        Export();
        btn_Export.Enabled = true;
    }

    int sheetSize=50000;
    private void Export()
    {
        int totalRecordCount = 0;

        DataTable dt = null;

        //导出
        string xmlTemplatePath = Server.MapPath(@"ExportXML/TraningCourseLearnDetail.xml");
        int fileNameCount = 1;//防止文件同名
        string fileName = "培训课程学习情况汇总" + fileNameCount;
        string strDateTime = DateTime.Now.ToString("yyyyMMddHHmmss");
        
        
        //存放目录
        string savePathFilesName = fileName + "Files" + strDateTime;
        if (!Directory.Exists(savePathFilesName))
        {
            Directory.CreateDirectory(Server.MapPath("~/Temp/" + savePathFilesName));
        }
        //导出excel路径

        string pathFileName = Server.MapPath("~/Temp/" + savePathFilesName + "/" + fileNameCount+fileName + strDateTime + ".xls");

        dt = new OnlineStudyLogic().GetTraningCourseLearnPageList(GetOnlineItemCourseWhere(), ETMS.AppContext.UserContext.Current.OrganizationID, 1, sheetSize, out totalRecordCount);
        ExportExcel(xmlTemplatePath, fileName, dt, pathFileName);
        if (totalRecordCount > sheetSize)
        {
            for (int i = 1; i <= totalRecordCount / sheetSize; i++)
            {
                fileNameCount++;
                strDateTime = DateTime.Now.ToString("yyyyMMddHHmmss");
                pathFileName = Server.MapPath("~/Temp/" + savePathFilesName + "/" + fileNameCount+fileName + strDateTime + ".xls");
                int pageIndex = i + 1;
                dt = new OnlineStudyLogic().GetTraningCourseLearnPageList(GetOnlineItemCourseWhere(), ETMS.AppContext.UserContext.Current.OrganizationID, pageIndex, sheetSize, out totalRecordCount);
                ExportExcel(xmlTemplatePath, fileName, dt, pathFileName);
            }

        }
        //foreach (DataRow row in dt.Rows)
        //{
        //    row["ItemCourseTeacher"] = row["ItemCourseTeacher"].ToString().Replace("#", "\r\n");
        //    row["ItemCourseTestName"] = row["ItemCourseTestName"].ToString().Replace("#", "\r\n");
        //    row["ItemCourseTestScore"] = row["ItemCourseTestScore"].ToString().Replace("#", "\r\n");
        //}


        //压缩目录
        string sorurceDictory = Server.MapPath("~/Temp/" + savePathFilesName + "/");
        string downDictory = Server.MapPath("~/Temp/" + savePathFilesName + ".zip");
        FileUtility.ZipFileDictory(sorurceDictory, downDictory, "");
        Directory.Delete(sorurceDictory, true);
        
        //下载
        FileDownLoadUtility.DownloadFile(HttpContext.Current, downDictory, 202400);
    }


    private void ExportExcel(string xmlTemplatePath,string fileName, DataTable  dt,string pathFileName)
    {
        ExcelProvider provider = new ExcelProvider(xmlTemplatePath, fileName, dt);
        provider.DataFormate = "yyyy-MM-dd HH:MM:ss";
        provider.XlsSavePath = pathFileName;
        provider.XlsWriteModel = XlsWriteModel.FileOutput;
        provider.SheetSize = sheetSize;
        provider.ExportExcel();
    }
}