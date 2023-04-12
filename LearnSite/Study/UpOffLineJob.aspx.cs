using University.Mooc.AppContext;
using ETMS.Components.ExOfflineHomework.API.Entity;
using ETMS.Components.ExOfflineHomework.Implement.BLL;
using ETMS.Controls;
using ETMS.Utility;
using ETMS.Utility.Service.FileUpload;
using System;
using System.Collections.Generic;

namespace ETMS.Studying.Study
{
    public partial class UpOffLineJob : System.Web.UI.Page
    {

        /// <summary>
        /// 实践id
        /// </summary>
        protected Guid JobID
        {
            get
            {
                if (ViewState["JobID"] == null)
                {
                    ViewState["JobID"] = BasePage.UrlParamDecode(Request.QueryString["JobID"]).ToGuid();
                }
                return ViewState["JobID"].ToGuid();
            }
            set
            {
                ViewState["JobID"] = value;
            }
        }
        /// <summary>
        /// 学生实践ID
        /// </summary>
        protected string StudentJobID
        {
            get
            {
                if (ViewState["StudentJobID"] == null)
                {
                    ViewState["StudentJobID"] = BasePage.UrlParamDecode(Request.QueryString["StudentJobID"]).ToString();
                }
                return ViewState["StudentJobID"].ToString();
            }
            set
            {
                ViewState["StudentJobID"] = value;
            }
        }
        /// <summary>
        /// 离线作业课程ID
        /// </summary>
        protected Guid ItemCourseOffLineJobID
        {
            get
            {
                if (ViewState["ItemCourseOffLineJobID"] == null)
                {
                    ViewState["ItemCourseOffLineJobID"] = BasePage.UrlParamDecode(Request.QueryString["ItemCourseOffLineJobID"]).ToGuid();
                }
                return ViewState["ItemCourseOffLineJobID"].ToGuid();
            }
            set
            {
                ViewState["ItemCourseOffLineJobID"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Bind();
            }

        }
        private void Bind()
        {
            Res_e_OffLineJob job = new Res_e_OffLineJobLogic().GetById(JobID);

        }

        protected void Btn_Save_Click(object sender, EventArgs e)
        {
            try
            {
               
                List<FileUploadInfo> uploaders = this.MiniUpFile.FileUrl;
                FileUploadInfo fileDefine = uploaders.Count > 0 ? this.MiniUpFile.FileUrl[0] : null;
                if (fileDefine != null)
                {
                    Sty_StudentOffLineJob sJob = new Sty_StudentOffLineJob();                 
                    if (!string.IsNullOrEmpty(StudentJobID))
                    {
                        sJob = new Sty_StudentOffLineJobLogic().GetById(StudentJobID.ToGuid());
                        sJob.UploadFileName = fileDefine.FileOldName;//离线作业附件路径
                        sJob.UploadFilePath = fileDefine.BizUrl;
                        sJob.UploadTime = DateTime.Now;
                        new Sty_StudentOffLineJobLogic().Update(sJob);
                        MessageHelper.SuccessMessageBoxAndCloseWindowAndParentLocation("提示", "作品修改成功");
                    }
                    else
                    {
                        sJob.StudentJobID = Guid.NewGuid();
                        sJob.UserID = UserContext.Current.UserID;
                        sJob.ItemCourseOffLineJobID = ItemCourseOffLineJobID;                      
                        sJob.CreateTime = DateTime.Now;
                        sJob.UploadFileName = fileDefine.FileOldName;//离线作业附件路径
                        sJob.UploadFilePath = fileDefine.BizUrl;
                        sJob.UploadTime = DateTime.Now;
                        new Sty_StudentOffLineJobLogic().Add(sJob);
                        //MessageHelper.SuccessMessageBoxAndCloseWindow("提示", "添加成功", "setData");
                        MessageHelper.SuccessMessageBoxAndCloseWindowAndParentLocation("提示", "作品提交成功");
                    }                                     
                    
                   
                }
                else
                {
                    MessageHelper.FailedMessageBox("请上传作品后提交");
                }
             
            }
            catch (ETMS.AppContext.BusinessException bizEx)
            {
                MessageHelper.FailedMessageBox(ExceptionUtility.GetTranslateExceptionMessage(bizEx));
            }

        }
    }

}