using University.Mooc.AppContext;
using ETMS.Components.Basic.Implement.BLL.Course.Resources;
using ETMS.Utility;
using System;
using System.Linq;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Course;
using ETMS.Components.OnlinePlaying.Implement.BLL;

namespace ETMS.Studying.Study
{
    public partial class CourseStudy : System.Web.UI.Page
    {
        public Guid TrainingItemCourseID { get; set; }
        //public string CourseNameString { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                TrainingItemCourseID = Request.QueryString["TrainingItemCourseID"].ToGuid();
                var data = new Tr_ItemCourseLogic().GetItemCourseListByTrainingItemCourseID(TrainingItemCourseID);
                //录播课程
                if (data.Rows[0]["CourseModel"].ToInt() == 1)
                {
                    InitalRecordsCourseControl();
                }//直播课程
                else {
                    ResourceList.Visible = false;
                    rptLiving.Visible = true;
                    InitalLivingCourseControl(data.Rows[0]["CourseID"].ToGuid());
                }
            }
        }

        private void InitalRecordsCourseControl()
        {
            if (TrainingItemCourseID != Guid.Empty)
            {
                ResourceList.DataSource = new Res_ContentVideoLogic().GetResourceByCourse(TrainingItemCourseID, UserContext.Current.UserID);
                ResourceList.DataBind();
            }
        }

        private void InitalLivingCourseControl(Guid courseID)
        {
            if (courseID != Guid.Empty)
            {
                var result = new OnlinePlayingLogic().GetLivingByCourseID(courseID);
                var styResult = new StudentOnlinePlaying().GetStyLivingsByUserCourse(UserContext.Current.UserID, TrainingItemCourseID);

                foreach (var item in result)
                {
                    if (styResult != null)
                    {
                        if (styResult.Count(f => f.LivingID == item.LivingID) > 0)
                        {
                            item.StyStatus = 1;
                        }
                        else
                        {
                            item.StyStatus = -1;
                        }
                    }
                }
                rptLiving.DataSource = result;
                rptLiving.DataBind();
            }
        }

        public string GetResourceStatus(string status)
        {
            string result = "";
            switch (status)
            {
                case "1":
                    result = "study_end";
                    break;
                case "0":
                    result = "studying";
                    break;
                default:
                    result = "status_icon";
                    break;
            }
            return result;
        }

        protected string GetTime(int type, int vtime, int ptime)
        {
            string timestring = string.Empty;
            switch (type)
            {
                case 1:
                    timestring = formatSeconds(vtime);
                    break;
                case 2:
                    timestring = ptime.ToString();//timeFormat(ptime);
                    break;
                default:
                    break;
            }
            return timestring;
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