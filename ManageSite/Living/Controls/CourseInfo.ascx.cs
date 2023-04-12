using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

using ETMS.Components.Basic.Implement.BLL.Course;
using ETMS.Components.Basic.API.Entity.Course;
using ETMS.Components.Courseware.API.Entity;
using ETMS.Components.Courseware.Implement.BLL;
using ETMS.Utility.Service.FileUpload;
using ETMS.Controls;
using ETMS.Utility;
using ETMS.AppContext;

namespace ETMS.WebApp.Manage.Living
{
    public partial class CourseInfo : System.Web.UI.UserControl
    {
        private static readonly Res_CourseLogic courseLogic = new Res_CourseLogic();        
        
        #region 页面条件参数存放
        public Res_Course Source
        {
            get 
            {
                return (Res_Course)ViewState["Source"];
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
            get;
            set;
        }

        /// <summary>
        /// 课程ID
        /// </summary>
        public Guid CourseID
        {
            get;
            set;
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Action == OperationAction.Edit)
                    InitControl();
                else if (Action == OperationAction.Add)
                    imgCourseLogo.ImageUrl = StaticResourceUtility.GetFullPathByFileType(FileUploadFunctionType.CourseLogo, "default.jpg");
            }
        }

        private void InitControl()
        {
            Source = courseLogic.GetById(CourseID);
            lblCourseCode.Text = Source.CourseCode;
            txtCourseName.Text = Source.CourseName;
            txtCourseHours.Text = Source.CourseHours.ToString();
            txtCourseHours.Attributes.Add("t_value", txtCourseHours.Text);
            FCKeditorCourseIntroduction.Text = Source.CourseIntroduction;
            FCKeditorCourseOutline.Text = Source.CourseOutline;
            //FCKeditorForObject.Text = Source.ForObject;
            txtForObject.Text = Source.ForObject;
            radCourseStatus.SelectedValue = Source.CourseStatus.ToString();
            radIsPublic.SelectedValue = Source.IsPublic ? "1" : "0";
            dropCourseTypeCode.SelectedValue = Source.CourseTypeID.ToString();
            pnlCourseType.Visible = false;
            pnlCourseType2.Visible = true;
            lblCourseType.FieldIDValue = Source.CourseTypeID.ToString();
            dropCourseLevelID.SelectedValue = Source.CourseLevelID.ToString();
            imgCourseLogo.ImageUrl = StaticResourceUtility.GetFullPathByFileType(FileUploadFunctionType.CourseLogo, string.IsNullOrEmpty(Source.ThumbnailURL) ? "default.jpg" : Source.ThumbnailURL);
            rblIsPay.SelectedValue = Source.IsPay ? "1" : "0";
            txtPrice.Text = Source.Price.ToString();
            txtDiscountPrice.Text = Source.DiscountPrice.ToString();
            ddlLivingType.SelectedValue = Source.LivingType.ToString();
        }

        /// <summary>
        /// 新增、修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbtnSave_Click(object sender, EventArgs e)
        {
            if (!Validate())
            {
                return;
            }

            try
            {
                InitialEntity();
                courseLogic.Save(Source, Action);

                ETMS.Utility.JsUtility.SuccessMessageBoxAndCloseWindowAndTriggerRefreshEvent("课程信息保存成功！");
            }
            catch (ETMS.AppContext.BusinessException bizEx)
            {
                ETMS.Utility.JsUtility.AlertMessageBox(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
            }
        }

        private bool Validate()
        {
            if (rblIsPay.SelectedValue == "0")
            {
                return true;
            }
            if (string.IsNullOrEmpty(txtPrice.Text.Trim()))
            {
                JsUtility.AlertMessageBox("请输入原价格！");
                return false;
            }
            if (txtPrice.Text.Trim().ToDecimal() < 0)
            {
                JsUtility.AlertMessageBox("原价格不能小于0");
                return false;
            }

            if (string.IsNullOrEmpty(txtDiscountPrice.Text.Trim()))
            {
                JsUtility.AlertMessageBox("请输入原价格！");
                return false;
            }
            if (txtDiscountPrice.Text.Trim().ToDecimal() < 0)
            {
                JsUtility.AlertMessageBox("优惠价格不能小于0");
                return false;
            }
            if (txtPrice.Text.ToDecimal() < txtDiscountPrice.Text.ToDecimal())
            {
                JsUtility.AlertMessageBox("优惠价格不能大于原价格！");
                return false;
            }

            return true;

        }

        private void InitialEntity()
        {
            if (Action == OperationAction.Add)
            {
                Source = new Res_Course()
                {
                    CourseID = Guid.NewGuid(),
                    CreateUser = UserContext.Current.RealName,
                    CreateUserID = UserContext.Current.UserID,
                    ModifyUser = UserContext.Current.RealName,
                    ModifyTime = DateTime.Now,
                    CreateTime = DateTime.Now
                };
            }
            else if (Action == OperationAction.Edit)
            {
                Source.ModifyUser = UserContext.Current.RealName;
                Source.ModifyTime = DateTime.Now;
            }

            Source.CourseName = txtCourseName.Text.Trim();
            Source.CourseHours = txtCourseHours.Text.ToDecimal();
            Source.CourseIntroduction = FCKeditorCourseIntroduction.Text;
            Source.CourseOutline = FCKeditorCourseOutline.Text;
            List<FileUploadInfo> uploaders = this.uploader.FileUrl;
            FileUploadInfo fileDefine = uploaders.Count > 0 ? this.uploader.FileUrl[0] : null;

            Source.ThumbnailURL = fileDefine == null ? Source.ThumbnailURL : fileDefine.BizUrl; 
            Source.ForObject = txtForObject.Text;
            Source.CourseStatus = radCourseStatus.SelectedValue.ToInt();
            Source.IsPublic = radIsPublic.SelectedValue.ToBoolean();
            Source.CourseTypeID = dropCourseTypeCode.SelectedValue.ToInt();
            Source.CourseLevelID = dropCourseLevelID.SelectedValue.ToInt();
            Source.OrgID = UserContext.Current.OrganizationID;
            Source.CourseModel = 2;
            Source.IsPay = rblIsPay.SelectedValue.ToBoolean();
            Source.Price = txtPrice.Text.ToDecimal();
            Source.DiscountPrice = txtDiscountPrice.Text.ToDecimal();
            Source.LivingType = ddlLivingType.SelectedValue.ToInt();
        }
    }
}