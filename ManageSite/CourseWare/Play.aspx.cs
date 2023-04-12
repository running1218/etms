using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Utility.Service;

public partial class CourseWare_Play : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string mmediatype = "";
        string murl = Request.QueryString["url"].ToString();
        string rootUrl = (ServiceRepository.FileUploadStrategyService as ETMS.Utility.Service.FileUpload.DefaultFileUploadStrategyService).UrlRoot;
        string sextend = murl.Substring(murl.LastIndexOf(".") + 1).ToLower();

        switch (sextend)
        {
            case "wmv":
            case "asf":
            case "wma": 
            case "mp3":           
                mmediatype = "0"; //0表示是mediaplay播放器
                break;
            case "rmvb":
            case "rm":            
                mmediatype = "1"; //1表示是realplay播放器
                break;
            case "csf":
            case "csx":
                mmediatype = "2"; //2表示是ScenicPlayer播放器
                murl = murl.Substring(murl.LastIndexOf("?omsp") + 1).ToLower();
                break;
            default:
                break;
        }

        murl = string.Format("{0}/{1}", rootUrl, murl);        
        Literal1.Text = string.Format("<script language=javascript>callplay('{0}', '{1}');</script>", murl, mmediatype);
    }
}