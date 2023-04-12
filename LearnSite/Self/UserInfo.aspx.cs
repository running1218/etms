using University.Mooc.AppContext;
using ETMS.Components.Basic.API.Entity.Security;
using ETMS.Components.Basic.Implement.BLL.Security;
using System;

namespace ETMS.Studying.Self
{
    public partial class UserInfo : System.Web.UI.Page
    {   
        public string ImgUrl { get; set;}
           

        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (!IsPostBack)
            {             
                BindData();
            }
        }

        protected void BindData()
        {
            UserLogic userLogic = new UserLogic();         
            User user = userLogic.GetUserBaseData(UserContext.Current.UserID);
            spRealName.InnerText = user.RealName==null?"": user.RealName;
            emRealName.InnerText = user.RealName == null ? "" : user.RealName;
            emUserName.InnerText = user.LoginName == null ? "" : user.LoginName;
            emUserOrg.InnerText = user.OrganizationName == null ? "" : user.OrganizationName;
            emUserSex.InnerText =  user.SexTypeName == null ? "" : user.SexTypeName;
            emUserType.InnerText = user.ResettlementWayName == null ? "" : user.ResettlementWayName;
            txtEmail.Text = user.Email == null ? "" : user.Email;
            txtMobile.Text = user.MobilePhone == null ? "" : user.MobilePhone;
            ImgUrl = ETMS.Utility.StaticResourceUtility.GetFullPathByFileType("UserIcon", user.PhotoUrl==null?"": user.PhotoUrl);            
        }
    }
}