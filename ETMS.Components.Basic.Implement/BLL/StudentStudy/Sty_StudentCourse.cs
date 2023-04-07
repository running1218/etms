using System;
using ETMS.Components.Basic.Implement.DAL.StudentStudy;
using System.Data;

namespace ETMS.Components.Basic.Implement.BLL.StudentStudy
{
    public partial class Sty_StudentCourse
    {
        private static readonly Sty_StudentCourseAccess DAL = new Sty_StudentCourseAccess();

        #region 课程
        /// <summary>
        /// 首页 我的课程 显示课程截止日期最近的前5条数据
        /// </summary>
        /// <returns>课程名称  项目名称  截止日期  进入课堂</returns>
        public DataTable MyCourseHome(string UserID)
        {
            int pageIndex = 1;
            int pageSize = 5;
            string sortExpression = " Tr_ItemCourse.CourseEndTime ASC";
            string criteria = string.Format(" And Sty_StudentSignup.UserID ='{0}'", UserID);
            int totalRecords = 5;

            return GetCourseList(pageIndex, pageSize, sortExpression, criteria, out totalRecords);
        }

        /// <summary>
        /// 我的课程，某下项目
        /// </summary>
        /// <returns></returns>
        public DataTable MyCourseByProject()
        {
            DataTable dt = null;
            return dt;
        }

        /// <summary>
        /// 课程列表
        /// </summary>
        public DataTable GetCourseList(int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {
            return DAL.GetCourseList(pageIndex, pageSize, sortExpression, criteria, out totalRecords);
        }

        #endregion

        #region 项目
        /// <summary>
        /// 首页 我的项目 显示项目截止日期最近的前5条数据
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public DataTable MyProjectHome(string UserID)
        {
            int pageIndex = 1;
            int pageSize = 5;
            string sortExpression = " Tr_Item.ItemEndTime ASC ";
            string criteria = string.Format(" And Sty_StudentSignup.UserID ='{0}'", UserID);
            int totalRecords = 5;

            return GetProjectList(pageIndex, pageSize, sortExpression, criteria, out totalRecords);
        }

        /// <summary>
        /// 项目列表
        /// </summary>
        public DataTable GetProjectList(int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {
            return DAL.GetProjectList(pageIndex, pageSize, sortExpression, criteria, out totalRecords);
        }

        #endregion

        #region 公告

        /// <summary>
        /// 首页 公告 包括公司级和部门级 前10条
        /// </summary>
        /// <returns>名称 类型 是否已读</returns>
        public DataTable MyAnnouncementHome(string LoginName)
        {
            int pageIndex = 1;
            int pageSize = 5;
            string sortExpression = "";
            string criteria = LoginName;
            int totalRecords = 5;

            return GetBulletinList(pageIndex, pageSize, sortExpression, criteria, out totalRecords);
        }

        /// <summary>
        /// 公告列表
        /// </summary>
        public DataTable GetBulletinList(int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {
            return DAL.GetBulletinList(pageIndex, pageSize, sortExpression, criteria, out totalRecords);
        }

        #endregion


        /// <summary>
        /// 获取某个培训项目课程未录入成绩的学生总数
        /// </summary>
        /// <param name="trainingItemCourseID">培训项目课程ID</param>
        /// <returns></returns>
        public Int32 GetNoInputGradeTotalByItemCourseID(Guid trainingItemCourseID)
        {
            return DAL.GetNoInputGradeTotalByItemCourseID(trainingItemCourseID);
        }

        public int GetStudentCountByItemCourseID(Guid trainingItemCourseID) {
            return DAL.GetStudentCountByItemCourseID(trainingItemCourseID);
        }


        //public int GetStudyProgress(Guid courseID, int userID) {
        //    DataTable dt = DAL.GetStudyProgress(courseID, userID);
        //    if (dt.Rows.Count == 1) {
        //        if (dt.Rows[0]["studyCount"].ToString() != "0" && dt.Rows[0]["resourceCount"].ToString() != "0")
        //        {
        //            double ratio = Convert.ToInt32(dt.Rows[0]["studyCount"]) / Convert.ToInt32(dt.Rows[0]["resourceCount"]) * 100;
        //            return Convert.ToInt32(Math.Round(ratio, 0));
        //        }
        //        else {
        //            return 0;
        //        }
                
        //    }
        //    else { return 0; }
        //}

    }
}
