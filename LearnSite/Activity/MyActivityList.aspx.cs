using System;
using ETMS.Activity.Implement.BLL;
using University.Mooc.AppContext;
using ETMS.Utility.Service.FileUpload;
using ETMS.Utility;

namespace ETMS.Studying.Activity
{
    public partial class MyActivityList : System.Web.UI.Page
    {
        private static readonly SiginupLogic logic = new SiginupLogic();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
            }
        }
        private void LoadData()
        {
            var list = logic.GetMyActivitiesGoing(UserContext.Current.UserID);
            foreach (var item in list)
            {
                item.ImageUrl = StaticResourceUtility.GetFullPathByFileType(FileUploadFunctionType.MediaLogo, string.IsNullOrEmpty(item.ImageUrl) ? "default.jpg" : item.ImageUrl);
            }
            rptGoing.DataSource = list;
            rptGoing.DataBind();

            var history = logic.GetMyActivitiesCompleted(UserContext.Current.UserID);
            foreach (var item in history)
            {
                item.ImageUrl = StaticResourceUtility.GetFullPathByFileType(FileUploadFunctionType.MediaLogo, string.IsNullOrEmpty(item.ImageUrl) ? "default.jpg" : item.ImageUrl);
            }
            rptHistory.DataSource = history;
            rptHistory.DataBind();
        }
    }
}