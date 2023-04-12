<%@ WebHandler Language="C#" Class="ExportFile" %>

using System;
using System.Web;
using ETMS.Components.Reporting.Implement.BLL;
using System.Data;
using ETMS.Components.Reporting.API.Entity;
using ETMS.Components.Reporting.Implement;
using System.Linq;
using System.Collections.Generic;
using ETMS.Utility.ExportFile;
using ETMS.Utility;
using System.IO;
using ETMS.Utility.Data;
using System.Text;

public class ExportFile : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        ExcelDataAccess EDA = new ExcelDataAccess();
        OnlineItemCourse itemCourse = new OnlineItemCourse();
        string exportType = context.Request.Form["ExportType"];

        //跟据类型查询数据
        string sourcePath = string.Empty;
        string fileName = string.Empty;
        string downPath = string.Empty;
        string downFileName = string.Empty;
        string strDateTime = DateTime.Now.ToString("yyyyMMddHHmmss");
        string downPathZip = string.Empty;

        switch (exportType.ToLower())
        {
            case "online"://在线学习情况监控
                GetRequestVal(context, itemCourse);
                downPath = context.Server.MapPath("~/Temp/在线学习监控/");
                downPathZip = "../Temp/在线学习监控/";
                fileName = "在线学习监控" + strDateTime + ".csv";
                downFileName = "在线学习监控" + strDateTime + ".zip";
         
                DataTable dt;
                dt = new OnlineStudyLogic().GetOnlineStudyList(itemCourse, ETMS.AppContext.UserContext.Current.OrganizationID);

                ExportToSvc(context, dt, downPath, fileName);
                break;
        }

        //压缩文件
        FileUtility.Compress(downPath, fileName, downPath, downFileName, CompressType.Zip, CompressModel.File);
        //FileUtility.ZipFileDictory(sourcePath, downPath + ".zip", "");
        //Directory.Delete(sourcePath, true);
        if (File.Exists(downPath + fileName))
        {
            File.Delete(downPath + fileName);
        }
        
        //返回调用端下载
        context.Response.Write(downPathZip + downFileName);
    }

    /// <summary>
    /// 把数据转为导出格式数据
    /// </summary>
    /// <param name="list"></param>
    /// <param name="tab"></param>
    /// <returns></returns>
    private DataTable GetTab(List<OnLineStudyTitleItem> list, DataTable tab)
    {
        DataTable tabNew = new DataTable();
        foreach (OnLineStudyTitleItem item in list)
        {
            tabNew.Columns.Add(item.TitleValue);
        }
        foreach (DataRow row in tab.Rows)
        {
            DataRow rowNew = tabNew.NewRow();
            foreach (OnLineStudyTitleItem item in list)
            {
                rowNew[item.TitleValue] = row[item.TitleKey];
            }
            tabNew.Rows.Add(rowNew);
        }
        return tabNew;
    }

    //获得传递的参数(在线学习情况监控,培训课程学习情况汇总)
    private void GetRequestVal(HttpContext context, OnlineItemCourse itemCourse)
    {
        itemCourse.ItemCode = context.Request.Form["ItemCode"];
        itemCourse.ItemName = context.Request.Form["ItemName"];
        itemCourse.ItemCourseCode = context.Request.Form["ItemCourseCode"];
        itemCourse.ItemCourseName = context.Request.Form["ItemCourseName"];
        itemCourse.ItemCourseAttrID = context.Request.Form["ItemCourseAttrID"].ToInt();
        itemCourse.ItemCourseTypeID = context.Request.Form["ItemCourseTypeID"].ToInt();
        itemCourse.ItemCourseBeginTime = string.IsNullOrEmpty(context.Request.Form["ItemCourseBeginTime"]) ? "1900-01-01".ToDateTime() : (context.Request.Form["ItemCourseBeginTime"] + " 00:00:00").ToDateTime();
        itemCourse.ItemCourseEndTime = string.IsNullOrEmpty(context.Request.Form["ItemCourseEndTime"]) ? DateTime.MaxValue : (context.Request.Form["ItemCourseEndTime"] + " 23:59:59").ToDateTime();
        itemCourse.ItemCourseScoreStatus = context.Request.Form["ItemCourseScoreStatus"].ToInt();
        itemCourse.OrganizationID = context.Request.Form["OrganizationID"].ToInt();
        itemCourse.DepartmentID = context.Request.Form["DepartmentID"].ToInt();
        itemCourse.PostID = context.Request.Form["PostID"].ToInt();
        itemCourse.PostTypeID = context.Request.Form["PostTypeID"].ToInt();
        itemCourse.RankID = context.Request.Form["RankID"].ToInt();
        itemCourse.RealName = context.Request.Form["RealName"];
        itemCourse.Status = context.Request.Form["Status"].ToInt();        
        itemCourse.ItemCourseTeachModelID = context.Request.Form["ItemCourseTeachModelID"].ToInt();
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

    /// <summary>
    /// DatatableToCSVFile
    /// </summary>
    /// <param name="dt">DataTable dt 导出CSV的数据 </param>
    /// <param name="xbkPath">CSV模板,主要存储一些表头的格式</param>
    /// <param name="SavePath">导出的路径</param>
    /// <param name="err">出错提示</param>
    public void DatatableToCSVFile(System.Data.DataTable dt, string xbkPath, string SavePath, ref string err)
    {
        string row;
        try
        {
            string header;
            string tmp;
            StreamReader sr = new StreamReader(xbkPath);
            header = sr.ReadLine();

            sr.Close();
            FileStream fs = File.Create(SavePath);
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine(header);

            foreach (DataRow dr in dt.Rows)
            {
                row = "";
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (i != dt.Columns.Count - 1)
                    {
                        tmp = dr[i].ToString().Trim().Replace(",", " ");
                        row = row + tmp + ",";
                    }
                    else
                    {
                        tmp = dr[i].ToString().Trim().Replace(",", ".");
                        row = row + tmp;
                    }
                }
                sw.WriteLine(row);
            }
            sw.Flush();
            sw.Close();
        }
        catch (Exception ex)
        {
            err = ex.ToString();

        }
    }

    //导出为svc文件
    public void ExportToSvc(HttpContext context, System.Data.DataTable dt,string downPath, string fileName)
    {
        //创建文件夹
        if (!Directory.Exists(downPath))
        {
            Directory.CreateDirectory(downPath);
        }
        //删除重名的原文件
        if (File.Exists(downPath + fileName))
        {
            File.Delete(downPath + fileName);
        }
        //先打印标头
        StringBuilder strColu = new StringBuilder();
        StringBuilder strValue = new StringBuilder();
        int i = 0;
        StreamWriter sw = null ;
        try
        {
            sw = new StreamWriter(new FileStream(downPath + fileName, FileMode.CreateNew), System.Text.Encoding.GetEncoding("GB2312"));

            for (i = 0; i <= dt.Columns.Count - 1; i++)
            {
                strColu.Append(dt.Columns[i].ColumnName);
                strColu.Append(",");
            }
            strColu.Remove(strColu.Length - 1, 1);//移出掉最后一个,字符

            sw.WriteLine(strColu);

            foreach (DataRow dr in dt.Rows)
            {
                strValue.Remove(0, strValue.Length);//移出

                for (i = 0; i <= dt.Columns.Count - 1; i++)
                {
                    strValue.Append(dr[i].ToString());
                    strValue.Append(",");
                }
                strValue.Remove(strValue.Length - 1, 1);//移出掉最后一个,字符
                sw.WriteLine(strValue);
            }
        }
        catch (Exception ex)
        {
            //MessageBox.Show(ex.Message);
        }
        finally {
            if (sw != null)
                sw.Close();
        }
        //System.Diagnostics.Process.Start(strPath);
    }
}