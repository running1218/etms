using University.Mooc.AppContext;
using ETMS.Components.Basic.Implement.BLL.Course.Resources;
using ETMS.Components.Basic.Implement.BLL.Course.Teacher;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Course;
using ETMS.Utility;
using ETMS.Utility.Service.FileUpload;
using System;
using System.Data;
using ETMS.Components.Basic.Implement.BLL.Course;
using ETMS.Components.OnlinePlaying.Implement.BLL;

namespace ETMS.Studying.Master
{
    public partial class StudyAlone : System.Web.UI.MasterPage
    {
        public Guid TrainingItemCourseID { get; set; }
        public int CurrentUserID { get; set; }
        public string CurrentNameString { get; set; }

        public decimal StudyProgress { get; set; }

        public int CourseModel
        {
            get {
                return (int)ViewState["CourseModel"];
            }
            set {
                ViewState["CourseModel"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            CurrentUserID = UserContext.Current.UserID;
            TrainingItemCourseID = Request.QueryString["TrainingItemCourseID"].ToGuid();
            InitalControl();

        }

        private void InitalControl()
        {
            Tr_ItemCourseLogic Logic = new Tr_ItemCourseLogic();
            DataTable dt = Logic.GetItemCourseListByTrainingItemCourseID(TrainingItemCourseID);
            if (dt.Rows.Count == 1)
            {
                CourseImg.ImageUrl = StaticResourceUtility.GetFullPathByFileType(FileUploadFunctionType.CourseLogo, string.IsNullOrEmpty(dt.Rows[0]["ThumbnailURL"].ToString()) ? "default.jpg" : dt.Rows[0]["ThumbnailURL"].ToString());
                CourseName.Text = dt.Rows[0]["CourseName"].ToString();
                //CurrentNameString= dt.Rows[0]["CourseName"].ToString();

                lit_Start.Text = Convert.ToDateTime(dt.Rows[0]["CourseBeginTime"]).ToString("yyyy-MM-dd");
                lit_End.Text = Convert.ToDateTime(dt.Rows[0]["CourseEndTime"]).ToString("yyyy-MM-dd");

                TeacherList.DataSource = new Res_TeacherCourseLogic().GetCourseTeacher(dt.Rows[0]["CourseID"].ToGuid());
                TeacherList.DataBind();

                lit_CourseTime.Text = GetResourceTime();
                lit_StudyTime.Text = GetStudyResourceTime();
                decimal progress = GetCourseProgressPercentage(TrainingItemCourseID);
                lit_StudyProgressBox.Text = "<i class=\"scale\" style=\"width:" + progress + "%\"></i>";
                lit_StudyProgress.Text = progress.ToString();
                StudyProgress = progress;
                //if (progress >= 100)
                //{
                //    StudyCertificate.Style.Add("display", "");
                //    StudyCertificate.Attributes.Add("href", "../Study/StudyCertificate.aspx?TrainingItemCourseID=" + TrainingItemCourseID);
                //}
                //else
                //{
                //    StudyCertificate.Style.Add("display", "none");
                //}
            }
        }

        public string GetResourceTime()
        {
            int time = new Res_ContentVideoLogic().GetContentVideoTotalByCourse(TrainingItemCourseID);
            return formatSeconds(time);
        }

        public string GetStudyResourceTime()
        {
            int time = new Res_ContentVideoLogic().GetStudentStudyTimeByCourse(TrainingItemCourseID, UserContext.Current.UserID);
            return formatSeconds(time);
        }

        //public int GetStudyProgress()
        //{
        //    return new Sty_StudentCourse().GetStudyProgress(TrainingItemCourseID, UserContext.Current.UserID);
        //}

        public decimal GetCourseProgressPercentage(Guid trainingItemCourseID)
        {
            var data = new Tr_ItemCourseLogic().GetItemCourseListByTrainingItemCourseID(trainingItemCourseID);
            CourseModel = data.Rows[0]["CourseModel"].ToInt();

            if (data.Rows[0]["CourseModel"].ToInt() == 1)
            {
                var progressLogic = new Sty_UserStudyProgressLogic();
                int contentComplete = 0, contentNotstarted = 0, contentUnfinished = 0, testjobComplete = 0;
                var contentList = progressLogic.GetCourseProgressByTrainingItemCourseID(trainingItemCourseID, UserContext.Current.UserID, ref contentComplete, ref contentNotstarted, ref contentUnfinished);
                var ds = progressLogic.GetTestAndPaperProgress(UserContext.Current.UserID, trainingItemCourseID);
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
                var percentage = total == 0 ? 0 : Math.Round((decimal)((contentComplete + testjobComplete) * 100 / total), 2, MidpointRounding.AwayFromZero);
                return percentage;
            }
            else if (data.Rows[0]["CourseModel"].ToInt() == 2)
            {
                var result = new OnlinePlayingLogic().GetLivingByCourseID(data.Rows[0]["CourseID"].ToGuid());
                var styResult = new StudentOnlinePlaying().GetStyLivingsByUserCourse(UserContext.Current.UserID, TrainingItemCourseID);

                int total = 0;
                int studyied = 0;
                if (result != null && result.Count > 0)
                    total = result.Count;
                if (styResult != null)
                    studyied = styResult.Count;
                var percentage = total == 0 ? 0 : Math.Round((decimal)(studyied * 100 / total), 2, MidpointRounding.AwayFromZero);
                return percentage;
            }
            else {
                return 0;
            }
        }


        string formatSeconds(int PlayingFlag)
        {
            int msec = (PlayingFlag % 3600000) % 60000 % 1000;//毫秒

            int theTime = (PlayingFlag % 3600000) % 60000 / 1000;// 秒

            int theTime1 = (PlayingFlag % 3600000) / 60000;// 分

            int theTime2 = PlayingFlag / 3600000;// 小时

            string result = "";

            result += theTime2 + ":";


            if (theTime1 > 9)
            {
                result += theTime1 + ":";
            }
            else
            {
                result += "0" + theTime1 + ":";
            }


            if (theTime > 9)
            {
                result += theTime;
            }
            else
            {
                result += "0" + theTime;
            }

            //毫秒
            //if (msec >= 0 && msec <= 9)
            //{
            //    result += ":00" + msec;
            //}
            //else if (msec >= 10 && msec <= 99)
            //{
            //    result += ":0" + msec;
            //}
            //else if (msec >= 100 && msec <= 999)
            //{
            //    result += ":" + msec;
            //}

            return result;

        }
    }
}