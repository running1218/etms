using ETMS.Components.Basic.Implement.BLL.Course;
using ETMS.Components.StudyClass.API.Entity.StudyClass;
using ETMS.Utility;
using ETMS.Utility.Service;
using ETMS.Utility.Service.FileUpload;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ETMS.WebApp.Manage.Resource.CourseManage
{

    public partial class VideoPreview : ETMS.Controls.BasePage
    {
        private static readonly Res_ContentLogic resContentLogic = new Res_ContentLogic();

        public string Source = string.Empty;
        public Guid ContentID = Guid.Empty;
        public int ContentType = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            ContentID = getSafeRequest(this, "ContentID").ToGuid();
            ContentType = getSafeRequest(this, "Type").ToInt();
            InitControl();
            //ETMS.Utility.JsUtility.CloseWindow("delVideo");
        }
        private void InitControl()
        {
            string code = ConfigurationManager.AppSettings["CodingStream"] != null ? ConfigurationManager.AppSettings["CodingStream"].ToString() : "";
            DataTable dt = resContentLogic.GetContentDataInfo(ContentID, ContentType, code);
            if (dt.Rows.Count == 1) {
                //Path = ETMS.Utility.WebUtility.AppPath + "/" + dt.Rows[0]["DataInfo"].ToString();
                FileUploadConfig fileUploadConfig = ServiceRepository.FileUploadStrategyService.GetStrategy("MediaResourceVideo");
                //obj.UrlRoot = fileUploadConfig.UrlRoot;

                CourseContentStudyProgress content = new CourseContentStudyProgress();
                content.ContentID = ContentID;
                content.Name = dt.Rows[0]["Name"].ToString();
                content.Type = ContentType;
                content.DataInfo = dt.Rows[0]["DataInfo"].ToString();
                content.PlayTime= dt.Rows[0]["PlayTime"].ToInt();
                content.UrlRoot = fileUploadConfig.UrlRoot;
                Source = JsonHelper.GetInvokeSuccessJson(content);
            }
        }
    }
}