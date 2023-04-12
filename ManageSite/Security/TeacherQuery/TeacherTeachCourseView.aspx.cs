using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Controls;
using ETMS.Utility;

public partial class Security_TeacherQuery_TeacherTeachCourseView :BasePage
{
    /// <summary>
    /// 讲师ID
    /// </summary>
    public int TeacherID
    {
        get
        {
            if (ViewState["TeacherID"] == null)
                ViewState["TeacherID"] = 0;

            return (int)ViewState["TeacherID"];
        }
        set
        {
            ViewState["TeacherID"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack && !string.IsNullOrEmpty(BasePage.getSafeRequest(this.Page, "TeacherID")))
        {
            TeacherID = BasePage.getSafeRequest(this.Page, "TeacherID").ToInt();
        }
        lbtnReturn.PostBackUrl = this.ActionHref(string.Format("TeacherTeachCourseList.aspx?TeacherID={0}", TeacherID));
    }
}