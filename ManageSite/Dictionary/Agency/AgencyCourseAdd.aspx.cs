using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Components.Basic.Implement.BLL.Security;
using ETMS.Components.Basic.API.Entity.Security;
using ETMS.Utility;
using ETMS.AppContext;

namespace ETMS.WebApp.Manage
{
    public partial class AgencyCourseAdd : ETMS.Controls.BasePage
    {
        private static readonly Site_AgencyProductLogic agencyProductLogic = new Site_AgencyProductLogic();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlCourse.DataSource = agencyProductLogic.GetUnAgencyCourses(Request.ToparamValue<int>("AgencyID"), UserContext.Current.OrganizationID);
                ddlCourse.DataTextField = "CourseName";
                ddlCourse.DataValueField = "CourseID";
                ddlCourse.DataBind();

                lblDiscountCode.Text = GenerateStringID();
            }
        }

        protected void lbtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                agencyProductLogic.Insert(new Site_AgencyProduct()
                {
                    AgencyProductID = Guid.NewGuid(),
                    AgencyID = Request.ToparamValue<int>("AgencyID"),
                    AgencyCode = lblDiscountCode.Text,
                    CourseID = ddlCourse.SelectedValue.ToGuid(),
                    DiscountPrice = txtDiscountPrice.Text.ToDecimal(),
                    DiscountType = 1,
                    CreateTime = DateTime.Now,
                    CreateUser = UserContext.Current.UserName,
                    CreateUserID = UserContext.Current.UserID,
                    ModifyTime = DateTime.Now,
                    ModifyUser = UserContext.Current.UserName
                });
                ETMS.Utility.JsUtility.SuccessMessageBoxAndCloseWindowAndTriggerRefreshEvent("保存成功！");
            }
            catch (ETMS.AppContext.BusinessException bizEx)
            {
                ETMS.Utility.JsUtility.AlertMessageBox(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
                return;
            }
        }

        private string GenerateStringID()
        {
            long i = 1;

            foreach (byte b in Guid.NewGuid().ToByteArray())
            {
                i *= ((int)b + 1);
            }

            return string.Format("{0:x}", i - DateTime.Now.Ticks);
        }
    }
}