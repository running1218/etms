using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Components.Basic.API.Entity.Course;
using ETMS.AppContext;
using ETMS.Components.Basic.Implement.BLL.Course;
using ETMS.Utility;
using ETMS.Components.Basic.Implement.BLL.Course.Resources;
using ETMS.Components.Basic.API.Entity.Course.Resources;
using ETMS.Utility.Service.FileUpload;

namespace ETMS.WebApp.Manage.Resource.CourseManage.Content
{
    public partial class VideoManage : ETMS.Controls.BasePage
    {
        private static readonly Res_ContentLogic resContentLogic = new Res_ContentLogic();

        #region 页面条件参数存放
        public ResContentMore Source
        {
            get
            {
                return (ResContentMore)ViewState["Source"];
            }
            set
            {
                ViewState["Source"] = value;
            }
        }

        /// <summary>
        /// 操作动作
        /// </summary>
        public OperationAction Action
        {
            get
            {
                return (OperationAction)ViewState["Action"];
            }
            set
            {
                ViewState["Action"] = value;
            }
        }

        /// <summary>
        /// 资源ID
        /// </summary>
        public Guid ContentID
        {
            get { return (Guid)ViewState["ContentID"]; }
            set { ViewState["ContentID"] = value; }
        }
        /// <summary>
        /// 课件ID
        /// </summary>
        public Guid CoursewareID
        {
            get
            {
                return (Guid)ViewState["CoursewareID"];
            }
            set
            {
                ViewState["CoursewareID"] = value;
            }
        }

        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            CoursewareID = getSafeRequest(this, "CoursewareID").ToGuid();
            ContentID = getSafeRequest(this, "ContentID").ToGuid();
            Action = getSafeRequest(this, "Action").ToLower() == "edit" ? OperationAction.Edit : OperationAction.Add;

            if (!IsPostBack)
            {

                if (Action == OperationAction.Edit)
                {
                    InitControl();
                }
                else if (Action == OperationAction.Add)
                {
                    hiddIsEdit.Value = "1";
                }
            }
        }

        private void InitControl()
        {
            Source = resContentLogic.GetByID(ContentID);
            txtVideoName.Text = Source.Name;
            txtTeacherName.Text = Source.TeacherName;
            hiddSort.Value = Source.Sort.ToString();
            radStatus.SelectedValue = Source.Status.ToString();
            radIsOpen.SelectedValue = Source.IsOpen ? "1" : "0";
            hiddIsEdit.Value = "0";
        }

        /// <summary>
        /// 新增、修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                List<FileUploadInfo> uploaders = this.uploader.FileUrl;
                if (uploaders.Count < 1 && Action == OperationAction.Add)
                {
                    JsUtility.AlertMessageBox("请上传文件, 或等待上传完成100%！");
                    return;
                }
                //保存数据
                InitialEntity();
                resContentLogic.Save(Source, Action, Convert.ToInt32(hiddIsEdit.Value));

                ETMS.Utility.JsUtility.SuccessMessageBoxAndCloseWindow("保存成功！", "function(){var url=window.parent.location.href;if(url.indexOf('add')<0){url=url+'&oper=add';}window.parent.location.href=url;}");
            }
            catch (ETMS.AppContext.BusinessException bizEx)
            {
                ETMS.Utility.Logging.ErrorLogHelper.Error(bizEx);
                ETMS.Utility.JsUtility.AlertMessageBox(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
            }
        }

        private void InitialEntity()
        {
            int count = resContentLogic.GetContentCount(CoursewareID);
            if (Action == OperationAction.Add)
            {

                Source = new ResContentMore()
                {
                    ContentID = Guid.NewGuid(),
                    CoursewareID = CoursewareID,
                    Type = 1,
                    Sort = (count + 1),
                    ModifyTime = DateTime.Now,
                    CreateTime = DateTime.Now
                };
            }
            else if (Action == OperationAction.Edit)
            {
                Source.ModifyTime = DateTime.Now;
            }
            Source.Name = txtVideoName.Text.Trim();
            List<FileUploadInfo> uploaders = this.uploader.FileUrl;
            FileUploadInfo fileDefine = uploaders.Count > 0 ? this.uploader.FileUrl[0] : null;
            Source.DataInfo = fileDefine == null ? Source.DataInfo : fileDefine.BizUrl; 
            Source.Status = radStatus.SelectedValue.ToInt();
            Source.TeacherName = txtTeacherName.Text.Trim();
            Source.IsOpen = radIsOpen.SelectedValue.ToInt() == 1 ? true : false;

        }
    }
}