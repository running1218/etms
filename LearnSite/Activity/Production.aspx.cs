using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ETMS.Activity.Implement.BLL;
using ETMS.Utility;
using ETMS.Components.Basic.Implement.BLL.Dictionary;
using ETMS.Utility.Service.FileUpload;

namespace ETMS.Studying.Activity
{
    public partial class Production : System.Web.UI.Page
    {

        public Guid SiginupID { get { return Request.QueryString["ID"].ToGuid(); } }
        public Guid ProductID { get { return Request.QueryString["ProductID"].ToGuid(); } }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetControlValue();

                if (ProductID != Guid.Empty)
                {
                    InitData();
                }
            }
        }

        private void SetControlValue()
        {
            var list = new ActivityDirectoryLogic().GetProductTypeList();
            ddlProductType.DataSource = list;
            ddlProductType.DataValueField = "ColumnCodeValue";
            ddlProductType.DataTextField = "ColumnNameValue";
            ddlProductType.DataBind();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                ProductionLogic logic = new ProductionLogic();

                List<FileUploadInfo> uploaders = this.MiniUpFile.FileUrl;
                FileUploadInfo fileDefine = uploaders.Count > 0 ? this.MiniUpFile.FileUrl[0] : null;

                if (ProductID == default(Guid))
                {
                    ETMS.Activity.Entity.Production entity = new ETMS.Activity.Entity.Production();
                    entity.ProductID = Guid.NewGuid();
                    entity.SiginupID = SiginupID;
                    entity.ProductCode = string.Empty;
                    entity.ProductName = txtTitle.Text.Trim();
                    entity.ProductType = ddlProductType.SelectedValue.ToInt();
                    if (fileDefine != null)
                    {
                        entity.Extention = fileDefine.FileType;
                        entity.Address = fileDefine.BizUrl;
                    }
                    entity.UploadTime = DateTime.Now;
                    entity.AppraiseStatus = 0;
                    logic.Insert(entity);
                    MessageHelper.SuccessMessageBoxAndCloseWindowAndParentLocation("提示", "活动作品提交成功！");
                }
                else
                {
                    var data = new ETMS.Activity.Implement.BLL.ProductionLogic().GetProductions(SiginupID);

                    if (data.Count > 0)
                    {
                        ETMS.Activity.Entity.Production entity = data.Where(f => f.ProductID == ProductID).SingleOrDefault();
                        entity.ProductName = txtTitle.Text.Trim();
                        entity.ProductType = ddlProductType.SelectedValue.ToInt();
                        if (fileDefine != null)
                        {
                            entity.Extention = fileDefine.FileType;
                            entity.Address = fileDefine.BizUrl;
                        }
                        entity.UploadTime = DateTime.Now;
                        logic.Update(entity);
                        MessageHelper.SuccessMessageBoxAndCloseWindowAndParentLocation("提示", "活动作品提交成功");
                    }
                }
            }
            catch (ETMS.AppContext.BusinessException bizEx)
            {
                MessageHelper.FailedMessageBox(ExceptionUtility.GetTranslateExceptionMessage(bizEx));
            }
        }

        private void InitData()
        {
            var data = new ETMS.Activity.Implement.BLL.ProductionLogic().GetProductions(SiginupID);

            if (data.Count > 0)
            {
                ETMS.Activity.Entity.Production entity = data.Where(f => f.ProductID == ProductID).SingleOrDefault();
                ddlProductType.SelectedValue = entity.ProductType.ToString();
                txtTitle.Text = entity.ProductName;
            }
        }


        public string UpUrl
        {
            get
            {
                string url = (ViewState["UpUrl"] == null) ? WebUtility.AppPath + "/Controls/UploadHandler.ashx" : (string)ViewState["UpUrl"];

                return this.ActionHref(string.Format("{0}?type={1}&pr={2}"
                    , url
                    , HttpUtility.UrlEncode(ETMS.Utility.CrypProvider.Encryptor(FunctionType))
                    , HttpUtility.UrlEncode(ETMS.Utility.CrypProvider.Encryptor(DateTime.Now.ToString("yyyyMMddHHmmssfff")))));
            }
            set
            {
                ViewState["UpUrl"] = value;
            }
        }

        /// <summary>
        /// 此次上传对应的业务功能类型
        /// </summary>
        public string FunctionType
        {
            get
            {
                return (string)ViewState["FunctionType"];
            }
            set
            {
                ViewState["FunctionType"] = value;
            }
        }
        /// <summary>
        /// 上传后回调js
        /// </summary>
        public string CallBack
        {
            get
            {
                return (ViewState["CallBack"] == null) ? "null" : (string)ViewState["CallBack"];
            }
            set
            {
                ViewState["CallBack"] = value;
            }
        }
    }
}