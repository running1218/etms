using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using ETMS.Controls;
using System.Data;
using ETMS.Utility;
using ETMS.Utility.Service.FileUpload;

using ETMS.Components.Basic.API.Entity.Security;
using ETMS.Components.Basic.Implement.BLL.Security;
using ETMS.Components.Basic.API.Entity.Teacher;
using ETMS.Components.Basic.Implement.BLL.Teacher;
using ETMS.Components.Basic.Implement.BLL.TraningOrgnization;
using ETMS.Components.Basic.API.Entity.TraningOrgnization;

namespace ETMS.WebApp.Manage.Resource.ProfessorManage.Controls
{
    public partial class ProfessorInfoOutside : System.Web.UI.UserControl
    {
        public static Site_TeacherLogic site_TeacherLogic = new Site_TeacherLogic();
        public static UserLogic userLogic = new UserLogic();

        #region 页面条件参数存放
        private int TeacherID
        {
            get
            {
                int teacherID = 0;
                if (Request.QueryString["TeacherID"] != null)
                {
                    teacherID = int.Parse(Request.QueryString["TeacherID"]);
                }
                return teacherID;
            }
        }

        private Site_Teacher site_Teacher
        {
            get
            {
                return (Site_Teacher)ViewState["site_Teacher"];
            }
            set
            {
                ViewState["site_Teacher"] = value;
            }
        }

        #endregion
        private void InitPageValue()
        {
            //初始化控件

            //讲师信息编辑
            if (TeacherID > 0)
            {
                //处理页面数据初始化
                site_Teacher = site_TeacherLogic.GetById(TeacherID);
                site_Teacher.UserInfo = userLogic.GetUserByID(TeacherID);

                txtTeacherName.Text = site_Teacher.UserInfo.RealName;
                txtTeacherCode.Text = site_Teacher.TeacherCode;
                Dic_Organization.SelectedValue = site_Teacher.OuterOrgID.ToString();
                Dic_Status.SelectedValue = site_Teacher.IsUse.ToString();
                Dic_ProfessorGrade.SelectedValue = site_Teacher.TeacherLevelID.ToString();
                Dic_Sex.SelectedValue = site_Teacher.UserInfo.SexTypeID.ToString();
                txtClassReward.Text = site_Teacher.ClassReward.ToString();
                txtTitleName.Text = site_Teacher.UserInfo.TitleName;
                if (site_Teacher.UserInfo.Birthday.ToString("yyyy-MM-dd") != DateTime.MinValue.ToString("yyyy-MM-dd") && site_Teacher.UserInfo.Birthday.ToString("yyyy-MM-dd") != DateTime.MaxValue.ToString("yyyy-MM-dd"))
                {
                    txtBirthDay.Text = site_Teacher.UserInfo.Birthday.ToString("yyyy-MM-dd");
                }
                txtMail.Text = site_Teacher.UserInfo.Email;
                txtTel.Text = site_Teacher.UserInfo.MobilePhone;
                ddlSourceType.SelectedValue = site_Teacher.TeacherSourceID.ToString();

                Image1.ImageUrl = ETMS.Utility.StaticResourceUtility.GetFullPathByFileType("UserIcon", string.IsNullOrEmpty(site_Teacher.UserInfo.PhotoUrl) ? "teacher.jpg" : site_Teacher.UserInfo.PhotoUrl);

                txtServiceEnterprise.Text = site_Teacher.ServiceEnterprise;
                txtWorkExperience.Text = site_Teacher.WorkExperience;
                txtExpertise.Text = site_Teacher.Expertise;
                txtRepresentativeWorks.Text = site_Teacher.RepresentativeWorks;
                txtDescription.Text = site_Teacher.TeacherBrief;


                txtUserName.Text = site_Teacher.UserInfo.LoginName;
                txtUserName.Enabled = false;
                thPassword.Visible = false;
                tdPassword.Visible = false;
                ddlCorpationRealtion.SelectedValue = site_Teacher.IsCollaborate.ToString();
                //tdUserName.ColSpan = 3;
            }
            else //讲师信息新增
            {
                site_Teacher = new Site_Teacher();
                Image1.ImageUrl = ETMS.Utility.StaticResourceUtility.GetFullPathByFileType("UserIcon", "teacher.jpg");
                txtBirthDay.Text = DateTime.Now.ToDate();
            }
        }

        private void GetTeacherInfo()
        {
            if (TeacherID == 0)
            {
                site_Teacher.UserInfo = new User();
                site_Teacher.UserInfo.LoginName = txtUserName.Text.Trim();
                site_Teacher.UserInfo.PassWord = txtPassword.Text.Trim();
            }
            //讲师信息
            site_Teacher.TeacherCode = txtTeacherCode.Text.Trim();
            site_Teacher.OuterOrgID = string.IsNullOrEmpty(Dic_Organization.SelectedValue) == true ? new Guid() : Dic_Organization.SelectedValue.ToGuid();
            site_Teacher.IsUse = Dic_Status.SelectedValue.ToInt();
            site_Teacher.TeacherLevelID = Dic_ProfessorGrade.SelectedValue.ToInt();
            site_Teacher.UserInfo.SexTypeID = Dic_Sex.SelectedValue.ToInt();
            site_Teacher.ClassReward = string.IsNullOrEmpty(txtClassReward.Text.Trim()) ? 0 : txtClassReward.Text.Trim().ToDecimal();
            site_Teacher.IsCollaborate = ddlCorpationRealtion.SelectedValue.ToInt();

            site_Teacher.WorkExperience = txtWorkExperience.Text;
            site_Teacher.ServiceEnterprise = txtServiceEnterprise.Text;
            site_Teacher.Expertise = txtExpertise.Text;
            site_Teacher.RepresentativeWorks = txtRepresentativeWorks.Text;
            site_Teacher.TeacherBrief = txtDescription.Text;
            site_Teacher.TeacherSourceID = ddlSourceType.SelectedValue.ToInt();
            site_Teacher.UserInfo.Birthday = txtBirthDay.Text.ToDateTime();

            List<FileUploadInfo> uploaders = this.uploader.FileUrl;
            FileUploadInfo fileDefine = uploaders.Count > 0 ? this.uploader.FileUrl[0] : null;

            site_Teacher.UserInfo.PhotoUrl = fileDefine == null ? site_Teacher.UserInfo.PhotoUrl : fileDefine.BizUrl;

            //帐号信息
            site_Teacher.UserInfo.RealName = txtTeacherName.Text.Trim();
            site_Teacher.UserInfo.TitleName = txtTitleName.Text.Trim();
            site_Teacher.UserInfo.Email = txtMail.Text.Trim();
            site_Teacher.UserInfo.MobilePhone = txtTel.Text.Trim();

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitPageValue();
            }
        }


        protected void lbnSave_Click(object sender, EventArgs e)
        {

            try
            {
                GetTeacherInfo();
                site_TeacherLogic.Save(site_Teacher);
                ETMS.Utility.JsUtility.SuccessMessageBoxAndCloseWindowAndTriggerRefreshEvent("保存成功");
            }
            catch (ETMS.AppContext.BusinessException bizEx)
            {
                ETMS.Utility.JsUtility.FailedMessageBox(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
            }
            catch (Exception ex)
            {
                ETMS.Utility.JsUtility.FailedMessageBox(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(ex));
            }
        }
    }
}