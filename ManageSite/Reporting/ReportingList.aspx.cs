using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

public partial class Reporting_ReportingList : System.Web.UI.Page
{
    #region 页面参数
    //报表系统 域名端口
    private string ReportingUrl {
        get {
            return ConfigurationManager.AppSettings["ReportingUrl"];
        }
    }

    //在线学习情况监控
    public string OnlineStudyCourseUrl {
        get {
            return System.IO.Path.Combine(ReportingUrl, "OnlineStudy/OnlineStudyCourseDetail.aspx");
        }
    }

    //培训课程学习情况汇总
    public string TraningCourseLearnUrl {
        get {
            return System.IO.Path.Combine(ReportingUrl, "TraningCourseLearn/TraningCourseLearnDetail.aspx");
        }
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
    }
}