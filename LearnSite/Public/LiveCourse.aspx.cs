using System;

namespace ETMS.Studying.Public
{
    public partial class LiveCourse : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (University.Mooc.AppContext.UserContext.Current == null || University.Mooc.AppContext.UserContext.Current.UserID == 0) {
                Response.Redirect("/Index.aspx");
            }
        }
    }
}