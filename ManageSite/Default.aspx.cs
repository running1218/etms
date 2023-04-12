using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Components.Basic.Implement.BLL.Security;
using ETMS.Components.Basic.API.Entity.Security;
namespace ETMS.WebApp
{
    public partial class Default : System.Web.UI.Page
    {
        private static OrganizationLogic Logic = new OrganizationLogic();
        public string WeakName
        {
            get
            {
                switch (DateTime.Now.DayOfWeek)
                {
                    case DayOfWeek.Sunday:
                        return "星期日";

                    case DayOfWeek.Monday:
                        return "星期一";

                    case DayOfWeek.Tuesday:
                        return "星期二";

                    case DayOfWeek.Wednesday:
                        return "星期三";

                    case DayOfWeek.Thursday:
                        return "星期四";

                    case DayOfWeek.Friday:
                        return "星期五";

                    default:
                        return "星期六";
                }
            }
        }
        protected override void OnInit(EventArgs e)
        {
            //ETMS.AppContext.UserContext.Current.UserID = 1;//超级管理员
            //ETMS.AppContext.UserContext.Current.UserID = 2;//机构管理员
            //ETMS.AppContext.UserContext.Current.UserID = 3;//机构自建用户1
            base.OnInit(e);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Organization entity = (Organization)Logic.GetNodeByID(ETMS.AppContext.UserContext.Current.OrganizationID);
                imgBG.Src = ETMS.Utility.StaticResourceUtility.GetOrgLogoFullPath(string.IsNullOrEmpty(entity.Logo)? "default.png":entity.Logo);
            }            
        }
    }
}