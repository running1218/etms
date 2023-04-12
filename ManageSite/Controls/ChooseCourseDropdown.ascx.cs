using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ETMS.Components.Basic.Implement.BLL;
using ETMS.Controls;
using ETMS.WebApp;
using ETMS.Components.Basic.Implement.BLL.Course;
using ETMS.Components.Basic.API.Entity.Course;
using ETMS.AppContext;
using System.Text;

public partial class Controls_ChooseCourseDropdown : System.Web.UI.UserControl
{
    #region 页面条件参数存放

    //课件编号
    public String CourseName
    {
        get
        {
            if (ViewState["CourseName"] == null)
                ViewState["CourseName"] = "";
            return (String)ViewState["CourseName"];
        }
        set
        {
            ViewState["CourseName"] = value;
        }
    }
    public Guid CourseID
    {
        get
        {
            if (ViewState["CourseID"] == null)
                ViewState["CourseID"] = new Guid();
            return (Guid)ViewState["CourseID"];
        }
        set
        {
            ViewState["CourseID"] = value;
        }
    }

    /// <summary>
    /// 是否可用
    /// </summary>
    public bool isEnabled
    {
        get
        {
            if (ViewState["isEnabled"] == null)
                ViewState["isEnabled"] = true;
            return (bool)ViewState["isEnabled"];
        }
        set
        {
            ViewState["isEnabled"] = value;
        }
    }

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {        
        if (!IsPostBack)
        {
            InitialList();
            txtSearch.Text = CourseName;
            txtCourseID.Value = CourseID.ToString();
            txtSearch.Enabled = isEnabled;
        }

        txtSearch.Attributes.Add("onkeyup", "searchList()");
    }

    private void InitialList()
    {
        List<Res_Course> courses = new Res_CourseLogic().GetCourseByOrgID();
        StringBuilder sb = new StringBuilder();

        foreach (Res_Course course in courses)
        {
            sb.Append(string.Format("{0}$#${1}#$#", course.CourseID, string.Format("{0}{1}", course.CourseCode, course.CourseName)));
        }

        hfCourseList.Value = sb.Length > 0 ? sb.ToString().Substring(0, sb.ToString().Length - 3) : string.Empty;
    }
    public Guid getCourseID()
    {
        if (string.IsNullOrEmpty(txtCourseID.Value))
        {
            return Guid.Empty;
        }

        return new Guid(txtCourseID.Value);
    }
}