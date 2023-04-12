using University.Mooc.AppContext;
using ETMS.Components.Basic.API.Entity.TrainingItem;
using ETMS.Components.Basic.Implement.BLL.StudentStudy;
using ETMS.Components.Basic.Implement.BLL.Course.Teacher;
using ETMS.Components.Basic.Implement.BLL.TrainingItem;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Course;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.StudentCourse;
using ETMS.Utility;
using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Text;
using ETMS.Components.Basic.Implement.BLL.Course;

namespace ETMS.Studying.Public
{
    public partial class MyTrain : System.Web.UI.Page
    {
        public int OptionalCount;
        public int SelectedCount;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitalControl();
                //BindData();
            }

        }

        /// <summary>
        /// 初始化页面控件
        /// </summary>
        private void InitalControl()
        {
            string itemID = Request.QueryString["TrainingItemID"] != null ? Request.QueryString["TrainingItemID"].ToString() : "";

            DataTable dt = new Sty_StudentCourseLogic().GetTrainingItemListByUserID(UserContext.Current.UserID);
            if (dt.Rows.Count > 0)
            {
                ddlItem.DataSource = dt;
                ddlItem.DataValueField = "TrainingItemID";
                ddlItem.DataTextField = "ItemName";
                ddlItem.DataBind();
                if (itemID != "") {
                    ddlItem.Items.FindByValue(itemID).Selected = true;
                }
                ddlItem_SelectedIndexChanged(null, null);
                NoContentPanel.Visible = false;
                ContentPanel.Visible = true;
            }
            else {
                //无数据
                NoContentPanel.Visible = true;
                ContentPanel.Visible = false;
            }
           
        }
        //private void BindData()
        //{
        //    var dt = new Tr_ItemCourseLogic().GetOffLineJobListByTrainingItemID(ddlItem.SelectedValue.ToGuid(),UserContext.Current.UserID);
        //    this.RPracticeList.DataSource = dt;
        //    this.RPracticeList.DataBind();

        //}

        public string GetStudyProgress(Guid TrainingItemCourseID)
        {
            StringBuilder html = new StringBuilder();
            try
            {
                var progressLogic = new Sty_UserStudyProgressLogic();
                int contentComplete = 0, contentNotstarted = 0, contentUnfinished = 0, testjobComplete = 0;
                var contentList = progressLogic.GetCourseProgressByTrainingItemCourseID(TrainingItemCourseID, UserContext.Current.UserID, ref contentComplete, ref contentNotstarted, ref contentUnfinished);
                var ds = progressLogic.GetTestAndPaperProgress(UserContext.Current.UserID, TrainingItemCourseID);
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    int count = r["UserTestCount"] != null ? (string.IsNullOrWhiteSpace(r["UserTestCount"].ToString()) ? 0 : Convert.ToInt32(r["UserTestCount"])) : 0;
                    if (count > 0)
                        testjobComplete += 1;
                }
                foreach (DataRow r in ds.Tables[1].Rows)
                {
                    string status = r["TestStatus"] != null ? (string.IsNullOrWhiteSpace(r["TestStatus"].ToString()) ? "未提交" : r["TestStatus"].ToString()) : "未提交";
                    if (status == "已提交")
                        testjobComplete += 1;
                }

                var total = contentList.Count + ds.Tables[0].Rows.Count + ds.Tables[1].Rows.Count;
                decimal percentage = 0;
                if (total > 0 && (contentComplete + testjobComplete) > 0)
                {
                    percentage = Math.Round((decimal)((contentComplete + testjobComplete) * 100 / total), 2, MidpointRounding.AwayFromZero);
                }
                //return percentage;

                html.Append("<span class=\"scaleBox\">");
                html.Append("<i class=\"scale\" style=\"width:" + percentage + "%\"></i>");
                html.Append("</span>");
                html.Append("<span class=\"scaleNum\">" + percentage + "%</span>");
            }
            catch (Exception ex) { }
            return html.ToString();
        }

        public string GetStudentCount(Guid TrainingItemCourseID)
        {
            if (CacheHelper.Get(TrainingItemCourseID.ToString()) != null)
            {
                return CacheHelper.Get(TrainingItemCourseID.ToString()).ToString();
            }
            else
            {
                var result = new Sty_StudentCourse().GetStudentCountByItemCourseID(TrainingItemCourseID).ToString();
                CacheHelper.Add(TrainingItemCourseID.ToString(), result, DateTime.Now.AddHours(4), System.Web.Caching.CacheItemPriority.Normal);
                return result;
            }
        }

        protected void ddlItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            Guid trainingItemID = ddlItem.SelectedValue.ToGuid();
            if (trainingItemID != Guid.Empty)
            {
                try
                {
                    Tr_ItemCourseLogic Logic = new Tr_ItemCourseLogic();
                    DataTable dt = Logic.GetItemCourseListByTrainingItemID(UserContext.Current.UserID, trainingItemID);
                    //分离必修、选修数据
                    DataTable dtBiXiu = dt.Clone();
                    DataTable dtZiXuan = dt.Clone();
                    DataTable dtXuanXiu = dt.Clone();

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["CourseAttrID"].ToString() == "1")
                        {//必修
                            if (dt.Rows[i]["StudentCourse"] != null && dt.Rows[i]["StudentCourse"].ToString() != "")
                            {
                                DataRow row = dtBiXiu.NewRow();
                                row.ItemArray = dt.Rows[i].ItemArray;
                                dtBiXiu.Rows.Add(row);
                            }
                        }
                        else if (dt.Rows[i]["CourseAttrID"].ToString() == "2")
                        {
                            //自选,已选
                            if (dt.Rows[i]["StudentCourse"] != null && dt.Rows[i]["StudentCourse"].ToString() != "")
                            {
                                DataRow row = dtZiXuan.NewRow();
                                row.ItemArray = dt.Rows[i].ItemArray;
                                dtZiXuan.Rows.Add(row);
                            }

                            DataRow row1 = dtXuanXiu.NewRow();
                            row1.ItemArray = dt.Rows[i].ItemArray;
                            dtXuanXiu.Rows.Add(row1);
                        }
                    }

                    if (dt.Rows.Count > 0)
                    {
                        OptionalCount = Convert.ToInt32(dt.Rows[0]["ElectiveNumber"]);
                    }
                    SelectedCount = dtZiXuan.Rows.Count;

                    CourseList.DataSource = dtBiXiu;
                    CourseList.DataBind();
                    CourseListZiXuan.DataSource = dtZiXuan;
                    CourseListZiXuan.DataBind();
                    CourseListXuanXiu.DataSource = dtXuanXiu;
                    CourseListXuanXiu.DataBind();

                    Tr_Item tr_Item = new Tr_ItemLogic().GetById(trainingItemID);
                    lit_Start.Text = tr_Item.ItemBeginTime.ToString("yyyy-MM-dd");
                    lit_End.Text = tr_Item.ItemEndTime.ToString("yyyy-MM-dd");
                }
                catch (Exception ex)
                {

                }
            }
        }

        public string GetElectiveStatus(string studentCourse) {
            if (OptionalCount == -1)
            {
                if (studentCourse != "")
                {
                    return "prohibit";
                }
                else
                {
                    return string.Empty;
                }
            }
            else if (OptionalCount <= SelectedCount || studentCourse!="")
            {
                return "prohibit";
            }
            else {
                return "";
            }
        }
        protected void CourseList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Repeater rep = e.Item.FindControl("TeacherList") as Repeater;
                DataRowView row1 = (DataRowView)e.Item.DataItem;
                Guid CourseID = row1["CourseID"].ToGuid();
                rep.DataSource = new Res_TeacherCourseLogic().GetCourseTeacher(CourseID);
                rep.DataBind();
            }
        }
    }
}