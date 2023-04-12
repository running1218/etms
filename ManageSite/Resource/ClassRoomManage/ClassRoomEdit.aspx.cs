using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Utility;
using ETMS.Components.Basic.Implement.BLL.ClassRoom;
using ETMS.Components.Basic.API.Entity.ClassRoom;
using ETMS.Controls;

namespace ETMS.WebApp.Manage.Resource.ClassRoomManage
{
    public partial class ClassRoomEdit : ETMS.Controls.BasePage
    {
        private static Res_ClassRoomLogic classRoomLogic = new Res_ClassRoomLogic();        

        public Guid ClassRoomID
        {
            get { return Request.QueryString["ClassRoomID"].ToGuid();}
        }

        public Res_ClassRoom ClassRoom
        {
            set
            {
                ViewState["ClassRoom"]=value;
            }
            get
            {
                if(ViewState["ClassRoom"]==null)
                {
                    ViewState["ClassRoom"]=new Res_ClassRoom();
                }
                return (Res_ClassRoom)ViewState["ClassRoom"];
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Initial();
            }
        }
        protected void Initial()
        {
            this.Dic_Statuses.SelectedValue = "0";
            ClassRoom = classRoomLogic.GetById(ClassRoomID);
            this.txtClassRoomCode.Text = ClassRoom.ClassRoomCode;
            this.txtClassRoomName.Text = ClassRoom.ClassRoomName;
            this.Dic_Statuses.SelectedValue = ClassRoom.ClassRoomStatus.ToString()=="True"?"1":"0";
            this.txtCapacity.Text = ClassRoom.Capacity.ToString();
            this.txtDutyUser.Text = ClassRoom.DutyUser;
            this.txtPhone.Text = ClassRoom.Phone.ToString();
            this.txtAddress.Text = ClassRoom.Address;
            this.txtDescription.Text = ClassRoom.Description;
            this.txtRegulations.Text = ClassRoom.Regulations;
            this.txtClassRoomPurpose.Text = ClassRoom.ClassRoomPurpose;
            this.txtPrice.Text = ClassRoom.Price.ToString();
            
        }
        protected void InitialEntity()
        {           
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
                ClassRoom.Phone =this.txtPhone.Text;

            if (!String.IsNullOrEmpty(this.txtAddress.Text))
                ClassRoom.Address = this.txtAddress.Text;

            if (!String.IsNullOrEmpty(this.txtDescription.Text))
                ClassRoom.Description = this.txtDescription.Text;

            if (!String.IsNullOrEmpty(this.txtRegulations.Text))
                ClassRoom.Regulations = this.txtRegulations.Text;
            ClassRoom.ClassRoomPurpose = this.txtClassRoomPurpose.Text;
            ClassRoom.ModifyTime = DateTime.Now;
            ClassRoom.ModifyUser = ETMS.AppContext.UserContext.Current.RealName;
            if (!String.IsNullOrEmpty(this.txtPrice.Text))
                ClassRoom.Price = this.txtPrice.Text.ToDecimal();
        }

        protected void lbnSave_Click(object sender, EventArgs e)
        {
            try
            {
                ClassRoom = classRoomLogic.GetById(ClassRoomID);
                InitialEntity();
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