using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Utility;

using ETMS.Components.Basic.API.Entity.Security;
using ETMS.Components.Basic.Implement.BLL.Security;
using ETMS.Components.Basic.API.Entity.Teacher;
using ETMS.Components.Basic.Implement.BLL.Teacher;
using ETMS.Components.Basic.Implement.BLL.TraningOrgnization;
using ETMS.Components.Basic.API.Entity.TraningOrgnization;

namespace ETMS.WebApp.Manage.Resource.ProfessorManage
{
    public partial class ProfessorViewOutside : ETMS.Controls.BasePage
    {
        private static UserLogic userLogic = new UserLogic();
        private static Site_TeacherLogic site_TeacherLogic = new Site_TeacherLogic();
        /// <summary>
        /// 教师编号
        /// </summary>
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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitialControlers();
            }
        }

        /// <summary>
        /// 初始化控件
        /// </summary>
        private void InitialControlers()
        {
            Site_Teacher site_Teacher = site_TeacherLogic.GetById(TeacherID);
            User user = userLogic.GetUserByID(TeacherID);

            lblTeacherCode.Text = site_Teacher.TeacherCode;
            lblTeacherName.Text = user.RealName;

            lblOuterOrgID.FieldIDValue = site_Teacher.OuterOrgID.ToString();
            lblTeacherStatus.FieldIDValue = site_Teacher.IsUse.ToString();
            lblTeacherLevelID.FieldIDValue = site_Teacher.TeacherLevelID.ToString();
            //lblSex.FieldIDValue = user.SexTypeID.ToString()=="1"?"男":"女";
            lblSex.Text = user.SexTypeID.ToString() == "1" ? "男" : "女";
            lblClassReward.Text = site_Teacher.ClassReward.ToString();

            lblPosition.Text = user.TitleName;
            lblBirthDay.DateTimeValue = user.Birthday;
            lblServiceEnterprise.Text = site_Teacher.ServiceEnterprise;
            lblRepresentativeWorks.Text = site_Teacher.RepresentativeWorks;
            lblExpertise.Text = site_Teacher.Expertise;
            lblWorkExperience.Text = site_Teacher.WorkExperience;
            lblBrife.Text = site_Teacher.TeacherBrief;

            lblUserName.Text = user.LoginName;
            lblMail.Text = user.Email;
            lblTel.Text = user.MobilePhone;
            LblCortpration.Text = site_Teacher.IsCollaborate == 1 ? "已合作" : "未合作";
            imgPhoto.ImageUrl = ETMS.Utility.StaticResourceUtility.GetFullPathByFileType("UserIcon", string.IsNullOrEmpty(user.PhotoUrl) ? "teacher.jpg":user.PhotoUrl);
        }
    }
}