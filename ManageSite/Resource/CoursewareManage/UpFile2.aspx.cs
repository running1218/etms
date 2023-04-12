using System;
using System.Configuration;
using ETMS.Utility;
using System.Net;
using System.IO;

public partial class Resource_CoursewareManage_UpFile2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //上传文件格式
            if (Request.QueryString["AllowType"] != null)
                hfFtpScromAllowType.Value = ConfigurationManager.AppSettings[Request.QueryString["AllowType"]].Trim(new char[] { ',' });

            UpFile.FunctionType = Request.QueryString["FunctionType"];
        }
    }

    protected void btn_Save_Click(object sender, EventArgs e)
    {
        //for (int i = 0; i < UpFile.FileUrl.Count; i++)
        //{
        //    Response.Write(UpFile.FileUrl[i].BizUrl + ":" + UpFile.FileUrl[i].FileSizeStr + "<br/>");
        //}
        if (UpFile.FileUrl.Count > 0)
            Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "sess", "<script>UpFileSuccess('" + UpFile.FileUrl[0].FileName + "')</script>");
        else
            Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "sess", "<script>UpFileError()</script>");
    }
}