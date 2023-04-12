using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Controls;
using ETMS.Utility;
using ETMS.Components.Basic.API.Entity.ClassRoom;
using ETMS.Components.Basic.Implement.BLL.ClassRoom;

namespace ETMS.WebApp.Manage.Resource.ClassRoomManage
{
    public partial class ClassRoomAdd : ETMS.Controls.BasePage
    {
        private static Res_ClassRoomLogic classRoomLogic = new Res_ClassRoomLogic();
        public Res_ClassRoom ClassRoom
        {
            set
            {
                ViewState["ClassRoom"] = value;
            }
            get
            {
                if (ViewState["ClassRoom"] == null)
                {
                    ViewState["ClassRoom"] = new Res_ClassRoom();
                }
                return (Res_ClassRoom)ViewState["ClassRoom"];
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitialControlers();
            }
        }
        protected void InitialControlers()
        {
            this.Dic_Statuses.SelectedIndex = 0;
        }

        protected void Initial()
        {
            //ClassRoom.ClassRoomID = Guid.NewGuid();
            ClassRoom.CreateTime = DateTime.Now;
            ClassRoom.CreateUserID = ETMS.AppContext.UserContext.Current.UserID;
            ClassRoom.CreateUser = ETMS.AppContext.UserContext.Current.RealName;
            ClassRoom.DelFlag = false;
            if (!String.IsNullOrEmpty(this.txtClassRoomCode.Text))
                ClassRoom.ClassRoomCode = this.txtClassRoomCode.Text;

            if (!String.IsNullOrEmpty(this.txtClassRoomName.Text))
                ClassRoom.ClassRoomName = this.txtClassRoomName.Text;

            ClassRoom.ClassRoomStatus = Dic_Statuses.SelectedValue.ToInt() == 1 ? true : false;

            if (!String.IsNullOrEmpty(this.txtCapacity.Text))
                ClassRoom.Capacity = this.txtCapacity.Text.ToInt();

            if (!String.IsNullOrEmpty(this.txtDutyUser.Text))
                ClassRoom.DutyUser = this.txtDutyUser.Text;

            if (!String.IsNullOrEmpty(this.txtPhone.Text))
                ClassRoom.Phone = this.txtPhone.Text;

            if (!String.IsNullOrEmpty(this.txtAddress.Text))
                ClassRoom.Address = this.txtAddress.Text;

            if (!String.IsNullOrEmpty(this.txtDescription.Text))
                ClassRoom.Description = this.txtDescription.Text;

            if (!String.IsNullOrEmpty(this.txtRegulations.Text))
                ClassRoom.Regulations = this.txtRegulations.Text;
            ClassRoom.ClassRoomPurpose = this.txtClassRoomPurpose.Text;

            if (!string.IsNullOrEmpty(this.txtPrice.Text.Trim()))
                ClassRoom.Price = this.txtPrice.Text.ToDecimal();
            ClassRoom.OrgID = ETMS.AppContext.UserContext.Current.OrganizationID;
        }       

        protected void lbnSave_Click(object sender, EventArgs e)
        {
            try
            {
                ClassRoom = new Res_ClassRoom();
                Initial();
                classRoomLogic.Save(ClassRoom);
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