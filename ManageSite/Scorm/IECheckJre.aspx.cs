﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Controls;
using ETMS.Utility;

public partial class Scorm_IECheckJre : System.Web.UI.Page
{
    #region 页面参数
    public Guid CourseID
    {
        get
        {
            return (Guid)(ViewState["CourseID"]);
        }
        set
        {
            ViewState["CourseID"] = value;
        }
    }
    public Guid CourseWareID
    {
        get
        {
            return (Guid)(ViewState["CourseWareID"]);
        }
        set
        {
            ViewState["CourseWareID"] = value;
        }
    }
    public Guid ItemCourseResID
    {
        get
        {
            return Guid.Empty;
        }
        set
        {
            ViewState["ItemCourseResID"] = value;
        }
    }
    #endregion

    public string url = "";
    protected void Page_Load(object sender, EventArgs e)
    {


        if (!IsPostBack)
        {
            if (Request.QueryString["CourseWareID"] != null)
            {
                CourseWareID = new Guid(BasePage.UrlParamDecode(BasePage.getSafeRequest(this, "CourseWareID")));
            }
            if (Request.QueryString["CourseID"] != null)
            {
                CourseID = new Guid(BasePage.UrlParamDecode(BasePage.getSafeRequest(this, "CourseID")));
            }
            if (Request.QueryString["ItemCourseResID"] != null)
            {
                ItemCourseResID = new Guid(BasePage.UrlParamDecode(BasePage.getSafeRequest(this, "ItemCourseResID")));
            }
        }
        url = this.ActionHref(string.Format("~/Scorm/Default.aspx?CourseID={0}&CourseWareID={1}&ItemCourseResID={2}", CourseID, CourseWareID, ItemCourseResID));
    }
}