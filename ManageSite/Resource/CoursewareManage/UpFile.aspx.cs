using System;
using System.Configuration;

public partial class Resource_CoursewareManage_UpFile : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            hfFtpServer.Value = ConfigurationManager.AppSettings["FtpServer"];
            hfFtpUser.Value = ConfigurationManager.AppSettings["FtpUser"];
            hfFtpPassword.Value = ConfigurationManager.AppSettings["FtpPassword"];
            hfFtpPort.Value = ConfigurationManager.AppSettings["FtpPort"];
            hfFtpMaxSize.Value = string.IsNullOrEmpty(ConfigurationManager.AppSettings["FtpMaxSize"]) ? "1024" : ConfigurationManager.AppSettings["FtpMaxSize"];

            //上传文件格式
            if (Request.QueryString["AllowType"] != null)
                hfFtpScromAllowType.Value = ConfigurationManager.AppSettings[Request.QueryString["AllowType"]].Trim(new char[]{','});
        }
    }
}