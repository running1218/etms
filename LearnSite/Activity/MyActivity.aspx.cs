using System;
using ETMS.Activity.Implement.BLL;
using University.Mooc.AppContext;
using ETMS.Utility;
using ETMS.Utility.Service.FileUpload;

namespace ETMS.Studying.Activity
{
    public partial class MyActivity1 : System.Web.UI.Page
    {
        private static readonly SiginupLogic logic = new SiginupLogic();

        public Guid SiginUpID { get { return Request.QueryString["id"].ToGuid(); } }

        public int CommitCount { get { return GetCommitCount(); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
                LoadMyProduct();
            }
        }

        private void LoadData()
        {
            var entity = logic.GetMyActivity(SiginUpID);
            if (null != entity)
            {
                imgPic.ImageUrl = StaticResourceUtility.GetFullPathByFileType(FileUploadFunctionType.MediaLogo, string.IsNullOrEmpty(entity.ImageUrl) ? "default.jpg" : entity.ImageUrl);
                ltlTitle.Text = entity.AppraisalTitle;
                ltlBeginTime.Text = entity.BeginTime.ToString();
                ltlEndTime.Text = entity.EndTime.ToString();
                ltlSiginNo.Text = entity.SiginupNo;                               
            }
        }

        private void LoadMyProduct()
        {
            var data = new ETMS.Activity.Implement.BLL.ProductionLogic().GetProductions(SiginUpID);
            if (data.Count > 0)
            {
                var entity = data[0];
                ltlType.Text = entity.TypeName;
                ltlExtention.Text = entity.Extention;
                ltlName.Text = entity.ProductName;
                ltlTime.Text = entity.UploadTime.ToString();
                hfID.Value = entity.ProductID.ToString();
                ltlName.NavigateUrl = string.Format("{0}/ExOfflineHomework/{1}", WebUtility.FileUrlRoot, entity.Address);

                if (data.Count > 1)
                {
                    entity = data[1];
                    ltlType2.Text = entity.TypeName;
                    ltlExtention2.Text = entity.Extention;
                    ltlName2.Text = entity.ProductName;
                    ltlTime2.Text = entity.UploadTime.ToString();
                    hfID2.Value = entity.ProductID.ToString();
                    ltlName2.NavigateUrl = string.Format("{0}/ExOfflineHomework/{1}", WebUtility.FileUrlRoot, entity.Address);
                }
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                new ETMS.Activity.Implement.BLL.ProductionLogic().Delete(hfID.Value.ToGuid());
                MessageHelper.CloseWindowAndTriggerRefreshEvent();
            }
            catch (BusinessException bizEx)
            {
                MessageHelper.FailedMessageBox(ExceptionUtility.GetTranslateExceptionMessage(bizEx));
            }
        }

        protected void btnDelete2_Click(object sender, EventArgs e)
        {
            try
            {
                new ETMS.Activity.Implement.BLL.ProductionLogic().Delete(hfID2.Value.ToGuid());
                MessageHelper.CloseWindowAndTriggerRefreshEvent();
            }
            catch (BusinessException bizEx)
            {
                MessageHelper.FailedMessageBox(ExceptionUtility.GetTranslateExceptionMessage(bizEx));
            }
        }

        public int GetCommitCount()
        {
            var data = new ETMS.Activity.Implement.BLL.ProductionLogic().GetProductions(SiginUpID);
            return data.Count;
        }
    }
}