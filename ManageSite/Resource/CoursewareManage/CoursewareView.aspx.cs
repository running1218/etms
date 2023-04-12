using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using ETMS.Components.Courseware.Implement.BLL;
using ETMS.Components.Courseware.API.Entity;
using ETMS.Controls;
using ETMS.Utility;


namespace ETMS.WebApp.Manage.Resource.CoursewareManage
{
    
    public partial class CoursewareView : ETMS.Controls.BasePage
    {
        private static readonly Res_CoursewareLogic res_CoursewareLogic = new Res_CoursewareLogic();

        #region 页面条件参数存放

        //课件ID
        public Guid CoursewareID
        {
            get
            {
                if (ViewState["CoursewareID"] == null)
                {
                    ViewState["CoursewareID"] = BasePage.UrlParamDecode(Request.QueryString["CoursewareID"]).ToGuid();
                }
                return ViewState["CoursewareID"].ToGuid();
            }
            set
            {
                ViewState["CoursewareID"] = value;
            }
        }
       
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //CoursewareID = new Guid(BasePage.UrlParamDecode(BasePage.getSafeRequest(this, "CoursewareID")));           
                InitControl();
            }
            
        }

        //初始化控件值
        private void InitControl()
        {
            int total=0;
            string temp=string.Format(" and CoursewareID='{0}'",CoursewareID);
            DataTable dt= res_CoursewareLogic.Res_CoursewareGetPagedList(1,1,string.Empty,temp,out total);
            if (dt != null)
            {
                this.ltlCourseCode.Text = dt.Rows[0]["CourseCode"].ToString();
                this.ltlCourseName.Text=dt.Rows[0]["CourseName"].ToString();
                ltlCoursewareName.Text = dt.Rows[0]["CoursewareName"].ToString();
                //ltlCoursewarePath.Text = dt.Rows[0]["CoursewarePath"].ToString();
                ltlCoursewareStatus.Text = dt.Rows[0]["CoursewareStatus"].ToString()=="1"?"启用":"停用";
                ltlShowHoures.Text = dt.Rows[0]["ShowHoures"].ToString();
                ltlCoursewareSource.Text = dt.Rows[0]["CoursewareSource"].ToString();
                ltlRemark.Text = dt.Rows[0]["Remark"].ToString();
                ltlCreateTime.Text = dt.Rows[0]["CreateTime"].ToDate();
                ltlCreateUser.Text = dt.Rows[0]["CreateUser"].ToString();
                ltlModifyTime.Text = dt.Rows[0]["ModifyTime"].ToDate();
                ltlModifyUser.Text = dt.Rows[0]["ModifyUser"].ToString();
            }
        }
    }
}