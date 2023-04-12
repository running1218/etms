using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using ETMS.Utility;
using ETMS.Utility.Data;
public partial class Example_ExportExcel_2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ExcelDataAccess EDA = new ExcelDataAccess();
        //copy template.xls到temp文件夹
        string xlsFileName = DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
        string fullXlsFilePath = StaticResourceUtility.GetFullRootPathByFileType("Temp", xlsFileName);
        string dirPath = System.IO.Path.GetDirectoryName(fullXlsFilePath);
        if (!System.IO.Directory.Exists(dirPath))
        {
            System.IO.Directory.CreateDirectory(dirPath);
        }
        //复制excel
        System.IO.File.Copy(Server.MapPath("~/Security/StudentManager/导出模板.xls"), fullXlsFilePath, true);
        //保存物理文件
        EDA.SaveExcel(GetDataTable(), "", fullXlsFilePath, "sheet1");
        //Excel导出
        FileDownLoadUtility.ExportFile(fullXlsFilePath);
    }
    private DataTable GetDataTable()
    {
        DataTable exportDT = new DataTable();
        exportDT.Columns.Add("学员账号", typeof(string));
        exportDT.Columns.Add("学员姓名", typeof(string));
        exportDT.Columns.Add("工号", typeof(string));
        exportDT.Columns.Add("部门", typeof(string));
        exportDT.Columns.Add("职级", typeof(string));
        exportDT.Columns.Add("岗位", typeof(string));
        exportDT.Columns.Add("邮箱", typeof(string));
        exportDT.Columns.Add("手机", typeof(string));
        exportDT.Columns.Add("状态", typeof(string));
        exportDT.Columns.Add("描述", typeof(string));
        for (int i = 1; i <= 10; i++)
        {
            DataRow newRow = exportDT.NewRow();
            exportDT.Rows.Add(newRow);

            newRow["学员账号"] = "学员" + i.ToString();
            newRow["学员姓名"] = "学员姓名" + i.ToString();
            newRow["工号"] = "工号" + i.ToString();
            newRow["部门"] = "/公司/IT中心/研发部";
            newRow["职级"] = "职级1" + i.ToString();
            newRow["岗位"] = "岗位1" + i.ToString();
            newRow["邮箱"] = "zhangsan@mail.com.cn";
            newRow["手机"] = "12342839212";
            newRow["状态"] = "失败";
            newRow["描述"] = "学员账户已经存在！";
        }
        return exportDT;
    }
}