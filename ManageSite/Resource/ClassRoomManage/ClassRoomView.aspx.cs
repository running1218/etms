using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Utility;
using ETMS.Controls;
using ETMS.Components.Basic.Implement.BLL.ClassRoom;
using ETMS.Components.Basic.API.Entity.ClassRoom;


namespace ETMS.WebApp.Manage.Resource.ClassRoomManage
{
    public partial class ClassRoomView : System.Web.UI.Page
    {

        private static Res_ClassRoomLogic classRoomLogic = new Res_ClassRoomLogic();

        public Guid ClassRoomID
        {
            get { return Request.QueryString["ClassRoomID"].ToGuid(); }
        }

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
                Initital();
                this.ClassRoomInfo1.BindFromData(ClassRoom, ViewMode.Browse);
            }
        }
        private void Initital()
        {
            ClassRoom = classRoomLogic.GetById(ClassRoomID);
        }       
       
    }
}