using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Utility;
using ETMS.Components.Basic.API.Entity.Security;
using ETMS.Components.Basic.Implement.BLL.Security;
using ETMS.Components.Mentor.Implement.BLL.Mentor;
using ETMS.Components.Mentor.API.Entity.Mentor;

public partial class Security_MentorManage_MentorView : System.Web.UI.Page
{
    private static Site_StudentLogic studentLogic = new Site_StudentLogic();
    private static Site_MentorLogic siteMentorLogic = new Site_MentorLogic();

    private int MentorID
    {
        get { return Request.QueryString["MentorID"].ToInt(); }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Initial();
        }
    }

    private void Initial()
    {
        Site_Student entity = studentLogic.GetById(MentorID);
        Site_Mentor mentor = siteMentorLogic.GetById(MentorID);

        this.lblRealName.Text = entity.RealName;
        this.lblEmail.Text = entity.Email;
        this.lblMobilePhone.Text = entity.MobilePhone;

        this.lblBirthDay.DateTimeValue = entity.Birthday;
        this.lblJobTime.DateTimeValue = entity.JoinTime;
        this.lblSex.FieldIDValue = entity.SexTypeID.ToString();

        this.lblHightestEducation.Text = entity.LastEducation;
        this.lblIdentity.Text = entity.Identity;
        this.lblParent.Text = entity.Superior;
        this.lblSpecialty.Text = entity.Specialty;
        this.lblWorkNo.Text = entity.WorkerNo;
        this.lblTelephone.Text = entity.OfficeTelphone;
        this.lblDepartment.FieldIDValue = entity.DepartmentID.ToString();
        this.lblPolitics.FieldIDValue = entity.PoliticsTypeID.ToString();
        this.lblJobTitle.Text = entity.TitleName;
        this.lblPost.FieldIDValue = entity.PostID.ToString();
        this.lblRank.FieldIDValue = entity.RankID.ToString();

        if (null != mentor)
        {
            this.lblCreateTime.Text = mentor.CreateTime.ToString("yyyy-MM-dd");
            this.lblCreator.Text = mentor.CreateUser;
            this.lblModifyTime.Text = mentor.ModifyTime.ToString("yyyy-MM-dd");
            this.lblModifyUser.Text = mentor.ModifyUser;
        }
    }
}