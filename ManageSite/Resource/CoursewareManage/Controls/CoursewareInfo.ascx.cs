using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using ETMS.Components.Basic.Implement.BLL;
using ETMS.Controls;
using ETMS.Components.Courseware.API.Entity;
using ETMS.Components.Courseware.Implement.BLL;
using ETMS.Utility;
using ETMS.Utility.Service.FileUpload;
using ETMS.Utility.Service;
using ETMS.AppContext;

namespace ETMS.WebApp.Manage.Resource.CoursewareManage.Controls
{
    public partial class CoursewareInfo : System.Web.UI.UserControl
    {
        private static readonly Res_CoursewareLogic res_CoursewareLogic = new Res_CoursewareLogic();
        private static Guid defaultGuidValue = new Guid();
        //private static Res_Courseware courseware = new Res_Courseware();

        #region 页面条件参数存放

        /// <summary>
        /// 操作类型 add  edit
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

        //课件ID
        public Guid CoursewareID
        {
            get
            {
                if (ViewState["CoursewareID"] == null)
                {
                    ViewState["CoursewareID"] = BasePage.UrlParamDecode(Request.QueryString["CoursewareID"]).ToGuid();
                }
                return ViewState["CoursewareID"].ToGuid();
            }
            set
            {
                ViewState["CoursewareID"] = value;
            }
        }

        /// <summary>
        /// 课程ID
        /// </summary>
        public Guid CourseID
        {
            get
            {
                if (ViewState["CourseID"] == null)
                {
                    ViewState["CourseID"] = BasePage.UrlParamDecode(Request.QueryString["CourseID"]).ToGuid();
                }
                return (Guid)ViewState["CourseID"];
            }
            set
            {
                ViewState["CourseID"] = value;
            }
        }

        private Res_Courseware courseware
        {
            get
            {
                if (ViewState["courseware"] == null)
                {
                    ViewState["courseware"] = new Res_Courseware();
                }
                return (Res_Courseware)ViewState["courseware"];
            }
            set
            {
                ViewState["courseware"] = value;
            }
        }

        public string FtpAllowType
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["FtpAllowType"].Trim(new char[] { ',' });
            }
        }

        public string FLVTransferDownloadUrl
        {
            get {
                return Server.MapPath("~/Tools/FlvTransfer.rar");
            }
        }

        #endregion


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {


                radlCoursewareStatus.SelectedValue = "1";
                fuCoverImage.FunctionType = FileUploadFunctionType.CourseLogo;
                //编辑
                if (Action == OperationAction.Edit)
                {
                    InitControl();
                }
                else
                {
                    if (null != CourseID)
                    {
                        ddlCourseID.CourseName = new ETMS.Components.Basic.Implement.PublicFacade().GetCourseNameByID(CourseID);
                    }
                }
                
            }

            rblType.Attributes.Add("onclick", "rblChange()");
            btnUpFile.Attributes["onclick"] = string.Format("javascript:showWindow('上传文件','{0}',450,320);javascript:return false;", this.ActionHref("../UpFile2.aspx?AllowType=FtpAllowType&FunctionType=DicScorm"));
        }

        //初始化控件值
        private void InitControl()
        {
            courseware = res_CoursewareLogic.GetById(CoursewareID);
            ddlCourseID.isEnabled = false;
            txtCoursewareName.Text = courseware.CoursewareName;
            radlCoursewareStatus.SelectedValue = courseware.CoursewareStatus.ToString();
            txtShowHoures.Text = courseware.ShowHoures.ToString();
            txtCoursewareSource.Text = courseware.CoursewareSource;
            txtRemark.Text = courseware.Remark;
            CoursewareID = courseware.CoursewareID;
            ddlCourseID.CourseName = new ETMS.Components.Basic.Implement.PublicFacade().GetCourseNameByID(CourseID);
            imgCoverLogo.ImageUrl = string.IsNullOrEmpty(courseware.CoverImg) ? "" : StaticResourceUtility.GetFullPathByFileType(FileUploadFunctionType.CourseLogo, courseware.CoverImg);  
            if (courseware.IsURL)
            {
                rblType.SelectedValue = "0";
                trAddress.Style.Add("display", "");
                trUpload.Style.Add("display", "none");
                txtAddress.Text = courseware.CoursewarePath;
            }
            else
            {
                rblType.SelectedValue = "1";
                trAddress.Style.Add("display", "none");
                trUpload.Style.Add("display", "");
                this.lblState.Text = string.IsNullOrEmpty(courseware.CoursewarePath) ? "<span class='colorRed'>无课件！</span>" : "<span class='colorGreen'>已有课件！</span>";
            }
            rblType.Enabled = false;
        }

        protected void lbtnSave_Click(object sender, EventArgs e)
        {
            if (ValidateCourse())
            {
                JsUtility.AlertMessageBox("课程名称不能为空!");
                return;
            }

            courseware.OrgID = ETMS.AppContext.UserContext.Current.OrganizationID;
            courseware.CoursewareTypeID = 2;
            courseware.CoursewareName = txtCoursewareName.Text.Trim();
            courseware.CoursewareSource = txtCoursewareSource.Text.Trim();
            courseware.CoursewareStatus = int.Parse(radlCoursewareStatus.SelectedValue);
            courseware.ShowHoures = string.IsNullOrEmpty(this.txtShowHoures.Text.Trim()) ? 0 : int.Parse(txtShowHoures.Text.Trim());
            courseware.Remark = txtRemark.Text.Trim();
            courseware.DelFlag = false;
            courseware.IsURL = rblType.SelectedValue == "0";
            courseware.CoverImg = fuCoverImage.UploadFileEntity().BizUrl ?? courseware.CoverImg;

            try
            {
                if (!courseware.IsURL)
                {
                    if (!string.IsNullOrEmpty(this.txtFileName.Text.Trim()))
                    {
                        courseware.CoursewarePath = UploadFile().Replace(@"\", "/");
                    }
                }
                else
                {
                    courseware.CoursewarePath = txtAddress.Text;
                }

                //增加
                if (CoursewareID.ToString() == defaultGuidValue.ToString())
                {
                    courseware.CreateUser = ETMS.AppContext.UserContext.Current.RealName;
                    courseware.CreateUserID = ETMS.AppContext.UserContext.Current.UserID;
                    courseware.CreateTime = System.DateTime.Now;
                    courseware.CoursewareID = Guid.NewGuid();
                    courseware.ModifyUser = ETMS.AppContext.UserContext.Current.RealName;
                    courseware.ModifyTime = System.DateTime.Now;
                    CourseID = ddlCourseID.getCourseID();
                    res_CoursewareLogic.AddCourseCourseware(courseware, CourseID);
                }
                else
                {
                    courseware.CoursewareID = CoursewareID;
                    courseware.ModifyUser = ETMS.AppContext.UserContext.Current.RealName;
                    courseware.CreateUserID = ETMS.AppContext.UserContext.Current.UserID;
                    courseware.ModifyTime = System.DateTime.Now;

                    res_CoursewareLogic.Save(courseware, CourseID, courseware.CoursewareID, Action);
                }

                ETMS.WebApp.Manage.Extention.SuccessMessageBoxAndCloseWindow("课件信息保存成功！");
            }
            catch (ETMS.AppContext.BusinessException bizEx)
            {
                ETMS.WebApp.Manage.Extention.FailedMessageBox(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx)); return;
            }
            catch(Exception exx)
            {
                ETMS.WebApp.Manage.Extention.FailedMessageBox("课件保存失败，请与管理员联系！" + exx.Message);
            }
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <returns></returns>
        protected string UploadFile()
        {
            string fileName = txtFileName.Text.TrimAllSpace();
            string phyPath = string.Empty;

            if (TransferPatternProvider.IsUsingPattern(fileName))
            {
                //phyPath = TransferPatternProvider.TransferPattern(fileName);
            }
            else
            {
                string root = (ServiceRepository.FileUploadStrategyService as DefaultFileUploadStrategyService).Root;
                string sourceFile = string.Format(@"{3}\UploadFiles\{0}\{1}\{2}", DateTime.Now.Year, DateTime.Now.ToString("MM"), fileName, root).Replace("\\", @"\");
                phyPath = string.Format(@"DisScorm\{0}{1}", Guid.NewGuid().ToString(), Path.GetExtension(fileName));
                string desFile = string.Format(@"{0}\{1}", root, phyPath).Replace("\\", @"\");
                System.IO.File.Move(sourceFile, desFile);
            }

            return phyPath;
        }

        bool ValidateCourse()
        {
            return ddlCourseID.getCourseID() == Guid.Empty;
        }
        protected void rblType_PreRender(object sender, EventArgs e)
        {
            //foreach (ListItem item in rblType.Items)
            //{
            //    item.Attributes.Add("onclick", "javascript:rblChange(this);");
            //}
        }
        protected void lbtnDownload_Click(object sender, EventArgs e)
        {
            ETMS.Utility.FileDownLoadUtility.ExportFile(Server.MapPath("~/Tools/FlvTransfer.rar"));

        }
        protected void linkDownload_Click(object sender, EventArgs e)
        {
            this.ModalPopupExtender1.Show();
        }
        protected void lbtnClose_Click(object sender, EventArgs e)
        {
            this.ModalPopupExtender1.Hide();
        }
    }
}
