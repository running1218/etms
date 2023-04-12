using ETMS.Activity.Implement.BLL;
using ETMS.Utility;
using ETMS.Utility.Service.FileUpload;
using System;
using System.Data;
using System.Web.UI.WebControls;
using University.Mooc.AppContext;

namespace ETMS.Studying.Activity
{
    public partial class Activity : System.Web.UI.Page
    {
        public Guid ActivityID;
        public string SiginupBtn = "";
        public string SiginupBtnText = "立即报名";
        protected void Page_Load(object sender, EventArgs e)
        {
            ActivityID = Request.QueryString["ID"] != null ? Request.QueryString["ID"].ToGuid() : Guid.Empty;
            if (!IsPostBack)
            {
                InitialControlers();
            }
        }
        private void InitialControlers()
        {
            if (ActivityID != Guid.Empty)
            {
                AppraisalLogic logic = new AppraisalLogic();
                DataTable dt = logic.GetAppraisal(ActivityID);
                if (dt.Rows.Count == 1)
                {
                    lit_Name.Text = dt.Rows[0]["AppraisalTitle"].ToString();

                    Picture.ImageUrl = StaticResourceUtility.GetFullPathByFileType(FileUploadFunctionType.MediaLogo, string.IsNullOrEmpty(dt.Rows[0]["ImageUrl"].ToString()) ? "default.jpg" : dt.Rows[0]["ImageUrl"].ToString());
                    lit_Abstract.Text = dt.Rows[0]["Abstract"].ToString();
                    lit_Start.Text = Convert.ToDateTime(dt.Rows[0]["BeginTime"].ToString()).ToString("yyyy年MM月dd日");
                    lit_End.Text = Convert.ToDateTime(dt.Rows[0]["EndTime"].ToString()).ToString("yyyy年MM月dd日");
                    if (DateTime.Now >= Convert.ToDateTime(dt.Rows[0]["EndTime"].ToString()))
                    {
                        SiginupBtn = "con-bm_n";
                        SiginupBtnText = "活动已结束";
                    }

                    if (Convert.ToInt32(dt.Rows[0]["LimitNum"].ToString()) > 0)
                    {
                        //查询报名人数
                        int count = new SiginupLogic().GetSiginupCount(ActivityID);
                        lit_LimitNum.Text = count + "/" + dt.Rows[0]["LimitNum"].ToString();
                        if (count >= Convert.ToInt32(dt.Rows[0]["LimitNum"].ToString()))
                        {
                            SiginupBtn = "con-bm_n";
                            SiginupBtnText = "名额已满";
                        }
                    }
                    else
                    {
                        int count = new SiginupLogic().GetSiginupCount(ActivityID);
                        lit_LimitNum.Text = string.Format("{0}/+∞", count);
                    }
                    lit_Type.Text = dt.Rows[0]["TypeName"].ToString();
                    lit_Shape.Text = dt.Rows[0]["ShapeName"].ToString();
                    if (dt.Rows[0]["Address"].ToString() != "")
                    {
                        lit_Address.Text = "<div class=\"act_l1\">活动地点：" + dt.Rows[0]["Province"].ToString() + dt.Rows[0]["City"].ToString() + dt.Rows[0]["Address"].ToString() + "</div>";
                    }
                    //活动区域
                    string regionIds = dt.Rows[0]["Region"].ToString().Substring(1, dt.Rows[0]["Region"].ToString().Length - 2).Replace("\"", "'");
                    DataTable dtRegion = logic.GetDicRegion(regionIds);
                    Area_List.DataSource = dtRegion;
                    Area_List.DataBind();
                    //活动组别
                    string groupIds = dt.Rows[0]["Group"].ToString().Substring(1, dt.Rows[0]["Group"].ToString().Length - 2).Replace("\"", "'");
                    DataTable dtGroup = logic.GetDicGroup(regionIds);
                    Group_List.DataSource = dtGroup;
                    Group_List.DataBind();
                    lit_Details.Text = dt.Rows[0]["Details"].ToString();
                    lit_ReviewRule.Text = dt.Rows[0]["ReviewRule"].ToString();

                    if (UserContext.Current != null)
                    {
                        SiginupLogic siginupLogic = new SiginupLogic();
                        DataTable dt1 = siginupLogic.GetSiginup(UserContext.Current.UserID, ActivityID);
                        if (dt1.Rows.Count == 1)
                        {
                            SiginupBtn = "con-bm_n";
                            SiginupBtnText = "已报名";
                        }
                    }

                    //结果公示
                    PrizeResultLogic prizeLogic = new PrizeResultLogic();
                    DataTable regionDt = prizeLogic.GetPrizeRegion(ActivityID);
                    PrizeRegionRepeater.DataSource = regionDt;
                    PrizeRegionRepeater.DataBind();
                }
            }

        }

        protected void PrizeRegionRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType==ListItemType.AlternatingItem)
            {
                Repeater rep = e.Item.FindControl("PrizeRepeater") as Repeater;
                DataRowView rowv = (DataRowView)e.Item.DataItem;
                int regionID = Convert.ToInt32(rowv["RegionID"]); //获取填充子类的id 
                PrizeResultLogic prizeLogic = new PrizeResultLogic();
                DataTable regionDt = prizeLogic.GetPrizeResult(ActivityID, regionID, 10);
                rep.DataSource = regionDt;
                rep.DataBind();
            }
        }
    }
}