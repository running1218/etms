using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Controls;
using ETMS.Utility;
using ETMS.Components.Basic.API.Entity.ClassRoom;

namespace ETMS.WebApp.Manage.Resource.ClassRoomManage.Controls
{
    public partial class ClassRoomInfo : System.Web.UI.UserControl, IMuliViewControl
    {

        #region IMuliViewControl 成员
        private object m_DomainModel;
        public Object DomainModel
        {
            get
            {
                return this.Views.DomainModel;
            }
            set
            {
                m_DomainModel = value;
            }
        }

        public ViewMode ControlMode
        {
            get
            {
                return this.Views.ControlMode;
            }
        }
        #endregion

        #region Manage View
        public void BindFromData_Manage(object domainModel)
        {
        }

        #region Edit_View
        public void BindFromData_Edit(object domainModel)
        {
            
        }
        #endregion

        #region Browse_View
        public void BindFromData_Browse(object domainModel)
        {
            Res_ClassRoom entity = (Res_ClassRoom)domainModel;

            
            this.lblClassRoomCode.Text = entity.ClassRoomCode;            
            this.lblClassRoomName.Text = entity.ClassRoomName;            
            this.lblClassRoomStatus.Text = entity.ClassRoomStatus?"启用":"停用";            
            this.lblCapacity.Text = entity.Capacity.ToString();
            this.lblDutyUser.Text = entity.DutyUser;            
            this.lblPhone.Text = entity.Phone;
            this.lblAddress.Text = entity.Address;           
            this.lblDescription.Text = entity.Description;            
            this.lblRegulations.Text = entity.Regulations;

            if (entity.CreateTime != null && entity.CreateTime != DateTime.MaxValue && entity.CreateTime != DateTime.MinValue)
            {
                this.lblCreateTime.Text = entity.CreateTime.ToDate();
            }

            if (entity.ModifyTime != null && entity.ModifyTime != DateTime.MaxValue && entity.ModifyTime != DateTime.MinValue)
            {
                this.lblModifyTime.Text = entity.ModifyTime.ToDate();
            }
            this.lblCreateUser.Text = entity.CreateUser;
            this.lblModifyUser.Text = entity.ModifyUser; 
            this.lblClassRoomPurpose.Text = entity.ClassRoomPurpose;
            this.lblPrice.Text = entity.Price.ToString();
            
        }
        #endregion

        #region List_View
        public void BindFromData_List(object domainModel)
        {

        }
        #endregion

        #region Handler

        public object UnBindFromData(object domainModel)
        {
            return domainModel;
        }

        public void BindFromData(object domainModel, ViewMode controlMode)
        {
            this.Views.BindFromData(domainModel, controlMode);
        }

        #endregion
        #endregion

    }
}
