﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CourseWare_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //if (!string.IsNullOrEmpty(Request.QueryString["url"]))
            //{
            //    string rootUrl = (ServiceRepository.FileUploadStrategyService as ETMS.Utility.Service.FileUpload.DefaultFileUploadStrategyService).UrlRoot;
            //    this.DocViewer1.DocURL = this.ActionHref(string.Format("{0}/{1}", rootUrl, Request.QueryString["url"]));
            //}
            //Response.Write("<script type='text/javascript'>window.location.href ='" + this.ActionHref(string.Format("{0}?url={1}", serverUrl, eCourseware.CoursewarePath)) + "'</script>");
        }
    }
}