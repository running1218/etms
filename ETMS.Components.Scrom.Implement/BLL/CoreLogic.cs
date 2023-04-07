using System;
using System.Data;
using ETMS.Components.Scrom.Implement.DAL;
namespace ETMS.Components.Scrom.Implement.BLL
{
    public partial class CoreLogic
    {
        private static readonly CoreDataAccess CoreDal = new CoreDataAccess();

        /// <summary>
        /// 获取用户学习最后记录
        /// </summary>
        /// <param name="ResourceID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public DataTable GetCmiCore(Guid ResourceID,Guid ItemCourseResID, int UserID)
        {
            return CoreDal.GetCmiCoreByCRU(ResourceID, ItemCourseResID,UserID);
        }

        /// <summary>
        /// 获取用户最后学习状态
        /// </summary>
        /// <param name="ResourceID">资源ID</param>
        /// <param name="UserID">用户ID</param>
        /// <returns></returns>
        public string GetCoreLessonStatus(Guid ResourceID,Guid ItemCourseResID, int UserID)
        {
            string StatusCode = "";
            DataTable dt = CoreDal.GetCmiCoreByCRU(ResourceID,ItemCourseResID, UserID);
            if (dt.Rows.Count > 0) {
                StatusCode = Convert.ToString(dt.Rows[0]["StatusCode"]);
            }
            return StatusCode;
        }

        //插入学习状态（初始值）
        public void InsertCoreLessonStatus(Guid ResourceID,Guid ItemCourseResID, int UserID, string Status)
        {
            CmiCoreInsert(ResourceID, ItemCourseResID,UserID, Status, "", "", "", 0);
        }
        
        /// <summary>
        /// 插入 断点数据
        /// </summary>
        /// <param name="ResourceID">资源编号</param>
        /// <param name="UserID">用户编号</param>
        /// <param name="StatusCode">状态代码</param>
        /// <param name="ExitCode">原因代码</param>
        /// <param name="ScoreRaw">得分</param>
        /// <param name="lessonlocation">最后浏览位置</param>
        /// <param name="SessionTime">学习时长</param>
        public void CmiCoreInsert(Guid ResourceID,Guid ItemCourseResID, int UserID, string StatusCode, string ExitCode, string ScoreRaw, string lessonlocation, int SessionTime)
        {
            CoreDal.CmiCoreInsert(ResourceID, ItemCourseResID, UserID, StatusCode, ExitCode, ScoreRaw, lessonlocation, SessionTime);
        }


        /// <summary>
        /// 设置用户最后学习状态
        /// </summary>
        /// <param name="ResourceID"></param>
        /// <param name="UserID"></param>
        /// <param name="Status"></param>
        public void SetCoreLessonStatus(Guid ResourceID,Guid ItemCourseResID, int UserID, string Status)
        {
            CoreDal.SetCoreLessonStatus(ResourceID,ItemCourseResID, UserID, Status);
        }

        /// <summary>
        /// 学习模式：普通模式
        /// </summary>
        /// <returns></returns>
        public string GetCoreLessonMode()
        {   
            return "normal";
        }


        /// <summary>
        /// 设置退出标记
        /// </summary>
        /// <param name="ResourceID"></param>
        /// <param name="UserID"></param>
        /// <param name="Exit"></param>
        public void SetCoreExit(Guid ResourceID,Guid ItemCourseResID, int UserID, string ExitCode)
        {
            CoreDal.CmiCoreUpdateExitCodeByCRU(ResourceID,ItemCourseResID, UserID, ExitCode);
        }

        /// <summary>
        /// 获取学生SCO成绩 0-100 或空值
        /// </summary>
        /// <param name="ResourceID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public string GetCoreScoreRaw(Guid ResourceID,Guid ItemCourseResID, int UserID)
        {
            string ScoreRaw = "";
            DataTable dt = CoreDal.GetCmiCoreByCRU(ResourceID,ItemCourseResID, UserID);
            if (dt.Rows.Count > 0)
            {
                ScoreRaw = Convert.ToString(dt.Rows[0]["ScoreRaw"]);
            }
            return ScoreRaw;
        }

        /// <summary>
        /// 设置学生SCO成绩 0-100 或空值
        /// </summary>
        /// <param name="ResourceID"></param>
        /// <param name="UserID"></param>
        /// <param name="ScoreRaw"></param>
        public void SetCoreScoreRaw(Guid ResourceID,Guid ItemCourseResID, int UserID, string ScoreRaw)
        {
            CoreDal.CmiCoreUpdateScoreRawByRUS(ResourceID,ItemCourseResID, UserID, ScoreRaw);
        }

        /// <summary>
        /// 获取断点续学地址
        /// </summary>
        /// <param name="ResourceID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public string GetCoreLessonLocation(Guid ResourceID,Guid ItemCourseResID, int UserID)
        {
            string lessonlocation = "";
            DataTable dt = CoreDal.GetCmiCoreByCRU(ResourceID,ItemCourseResID, UserID);
            if (dt.Rows.Count > 0)
            {
                lessonlocation = Convert.ToString(dt.Rows[0]["lessonlocation"]);
            }
            return lessonlocation;
        }

        /// <summary>
        /// 设置断点续学地址
        /// </summary>
        /// <param name="ResourceID"></param>
        /// <param name="UserID"></param>
        /// <param name="LessonLocation"></param>
        public void SetCoreLessonLocation(Guid ResourceID, Guid ItemCourseResID, int UserID, string LessonLocation)
        {
            CoreDal.CmiCoreUpdateLessonlocationByRUL(ResourceID, ItemCourseResID, UserID, LessonLocation);
        }

        /// <summary>
        /// 设置用户学习时长
        /// </summary>
        /// <param name="ResourceID"></param>
        /// <param name="UserID"></param>
        /// <param name="SessionTime"></param>
        public void SetCoreSessionTime(Guid ResourceID,Guid ItemCourseResID,int UserID, int SessionTime)
        {
            CoreDal.SetCoreSessionTime(ResourceID,ItemCourseResID, UserID, SessionTime);
        }
    }
}
