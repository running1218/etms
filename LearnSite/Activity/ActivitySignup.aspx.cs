using System;
using ETMS.Utility;
using ETMS.Activity.Implement.BLL;
using System.Data;

namespace ETMS.Studying.Activity
{
    public partial class ActivitySignup : System.Web.UI.Page
    {
        public Guid ActivityID;
        protected void Page_Load(object sender, EventArgs e)
        {
            ActivityID = Request.QueryString["activityID"] != null ? Request.QueryString["activityID"].ToGuid() : Guid.Empty;
            if (!IsPostBack) {
                Bind();
            }
        }

        private void Bind() {
            //AppraisalLogic logic = new AppraisalLogic();
            //DataTable dt = logic.GetDicGroupList();
            AppraisalLogic logic = new AppraisalLogic();
            DataTable dt = logic.GetAppraisal(ActivityID);
            if (dt.Rows.Count == 1)
            {
                string regionIds = dt.Rows[0]["Region"].ToString().Substring(1, dt.Rows[0]["Region"].ToString().Length - 2).Replace("\"", "'");
                DataTable dtRegion = logic.GetDicRegion(regionIds);
                DDL_Area.DataSource = dtRegion;
                DDL_Area.DataTextField = "RegionName";
                DDL_Area.DataValueField = "RegionID";
                DDL_Area.DataBind();

                string groupIds = dt.Rows[0]["Group"].ToString().Substring(1, dt.Rows[0]["Group"].ToString().Length - 2).Replace("\"", "'");
                DataTable dtGroup = logic.GetDicGroup(regionIds);
                DDL_Team.DataSource = dtGroup;
                DDL_Team.DataTextField = "GroupName";
                DDL_Team.DataValueField = "GroupID";
                DDL_Team.DataBind();
            }
        }
    }
}