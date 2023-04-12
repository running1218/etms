using System;
using System.Configuration;
using ETMS.Utility;
using System.Net;
using System.IO;
using ETMS.Utility;
using ETMS.Components.Courseware.Implement.BLL;

public partial class Resource_CoursewareManage_UpFile3 : System.Web.UI.Page
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
        if (UpFile.FileUrl.Count > 0)
        {
            new Res_CoursewareLogic().UpdateResourceStatus(Request.QueryString["CoursewareID"].ToGuid(), 1, UpFile.FileUrl[0].FileName);
            Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "sess", "<script>UpFileSuccess('" + UpFile.FileUrl[0].FileName + "')</script>");
        }
        else
            Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "sess", "<script>UpFileError()</script>");
    }
}