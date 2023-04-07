using System;
using System.Collections.Generic;
using System.Linq;
using ETMS.Components.OnlinePlaying.API.Entity;
using ETMS.Components.OnlinePlaying.Implement.DAL;
using ETMS.Utility;
using System.Data;
using ETMS.Components.Basic.API.Entity.Course;

namespace ETMS.Components.OnlinePlaying.Implement.BLL
{

    public partial class StudentOnlinePlaying
    {
        private static readonly OnlinePlayingDataAccess DAL = new OnlinePlayingDataAccess();
        public void CreateStudentOnlinePlaying(Sty_StudentOnlinePlaying entity)
        {
            DAL.CreateStudentOnlinePlaying(entity);
        }

        public List<Res_Living> GetNowValidLivings(int pageIndex, int pageSize, int orgID, out int totalRecords)
        {
            return DAL.GetNowValidLivings(orgID).PageList<Res_Living>(pageIndex, pageSize, out totalRecords);
        }

        public DataTable GetIndexLivings(int orgID)
        {
            return DAL.GetIndexLivings(orgID);
        }

        public List<Res_Living> GetValidLivings(int pageIndex, int pageSize, int orgID, out int totalRecords)
        {
            return DAL.GetValidLivings(orgID).PageList<Res_Living>(pageIndex, pageSize, out totalRecords);
        }
        public List<DemandCourse> GetAllLivings(int pageIndex, int pageSize, int orgID, out int totalRecords)
        {
            return DAL.GetAllLivings(orgID).PageList<DemandCourse>(pageIndex, pageSize, out totalRecords);
        }
        public List<Res_Living> GetHistoryLivings(int pageIndex, int pageSize, int orgID, out int totalRecords)
        {
            return DAL.GetHistoryLivings(pageIndex, pageSize, orgID, out totalRecords).ToList<Res_Living>();
        }

        /// <summary>
        /// 获取精品课程列表（数据量少，暂不分页）
        /// </summary>
        /// <param name="orgID"></param>
        /// <returns></returns>
        public List<CompetitiveCourse> GetCompetitiveCourses(int orgID)
        {
            List<CompetitiveCourse> list = DAL.GetCompetitiveCourses(orgID).ToList<CompetitiveCourse>();

            foreach (var item in list)
            {
                var livings = GetCourseLivings(item.CourseID);
                if (null != livings && livings.Count > 0)
                {
                    Res_Living currentLiving = GetCurrentOrFutureLiving(livings);
                    item.TeacherName = currentLiving.TeacherName;
                    item.livingNum = livings.Count;
                    item.LivingTime = string.Format("{0} {1}-{2}", currentLiving.StartTime.ToDate(), currentLiving.SHHMM, currentLiving.EHHMM);                    
                }

                item.SignupNum = GetSignupNum(item.CourseID);
            }

            return list;
        }

        public List<CompetitiveCourse> GetMyCompetitiveCourses(int userID)
        {
            List<CompetitiveCourse> list = DAL.GetMyCompetitiveCourse(userID).ToList<CompetitiveCourse>();

            foreach (var item in list)
            {
                var livings = GetCourseLivings(item.CourseID);
                if (null != livings && livings.Count > 0)
                {
                    Res_Living currentLiving = GetCurrentOrFutureLiving(livings);
                    item.TeacherName = currentLiving.TeacherName;
                    item.livingNum = livings.Count;
                    item.LivingTime = string.Format("{0} {1}-{2}", currentLiving.StartTime.ToDate(), currentLiving.SHHMM, currentLiving.EHHMM);
                }

                item.SignupNum = GetSignupNum(item.CourseID);
                item.Livings = livings;
            }

            return list;
        }

        /// <summary>
        /// 获取直播课程信息
        /// </summary>
        /// <param name="courseID"></param>
        /// <returns></returns>
        public CompetitiveCourse GetCompetitiveCourse(Guid courseID)
        {
            CompetitiveCourse item = DAL.GetCompetitiveCourse(courseID).Rows[0].ToEntity<CompetitiveCourse>();
            var livings = GetCourseLivings(item.CourseID);
            if (null != livings && livings.Count > 0)
            {
                Res_Living currentLiving = GetCurrentOrFutureLiving(livings);

                item.livingNum = livings.Count;
                item.TeacherName = currentLiving.TeacherName;
                item.LivingTime = string.Format("{0} {1}-{2}", currentLiving.StartTime.ToDate(), currentLiving.SHHMM, currentLiving.EHHMM);
            }

            item.SignupNum = GetSignupNum(item.CourseID);

            return item;
        }

        public List<Res_Living> GetCourseLivings(Guid courseID)
        {
            return DAL.GetLivingPageList(courseID).ToList<Res_Living>();
        }

        /// <summary>
        /// 计算正在开始或即将开始或已结束最后一次直播的信息
        /// </summary>
        /// <param name="livings"></param>
        /// <returns></returns>
        private Res_Living GetCurrentOrFutureLiving(List<Res_Living> livings)
        {
            var sortlivings = livings.OrderBy(f => f.EndTime).ToList();
            Res_Living row = null;
            foreach (var item in sortlivings)
            {
                row = item;
                if (item.EndTime >= DateTime.Now)
                    break;
            }

            return row;
        }

        private int GetSignupNum(Guid courseID)
        {
            return DAL.GetSignupNum(courseID).Rows[0]["Num"].ToInt();
        }

        public int CreateUserLiving(string userID, string livingID)
        {
            return DAL.CreateUserLiving(userID, livingID);
        }
        public int GetLivingUserCount(string livingID)
        {
            DataTable result = DAL.GetLivingUserCount(livingID);
            return result.Rows[0][0].ToInt();
        }

        public List<Sty_UserLiving> GetStyLivingsByUserCourse(int userID, Guid trainingItemCourseID)
        {
            return DAL.GetStyLivingsByUserCourse(userID, trainingItemCourseID).ToList<Sty_UserLiving>();
        }
    }
}
