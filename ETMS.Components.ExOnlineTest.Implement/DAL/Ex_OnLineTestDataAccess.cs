using System;
using ETMS.Utility.Data;
using System.Data.SqlClient;
using System.Data;

namespace ETMS.Components.ExOnlineTest.Implement.DAL
{
    public partial class Ex_OnLineTestDataAccess
    {


        /// <summary>
        /// 验证某个学习资源是否被培训项目的课程引用
        /// </summary>
        /// <param name="resID">学习资源ID</param>
        /// <returns></returns>
        public bool CheckResourceIsUsed(Guid resID)
        {
            bool isUsed = true;
            string sqlModal = @"select COUNT(*)  num from Tr_ItemCourseRes a where a.CourseResID ='{0}' and a.CourseResTypeID='{1}'";
            string sql = string.Format(sqlModal, resID, (int)Basic.API.Entity.EnumResourcesType.OnLineTest);
            int ret = (Int32)SqlHelper.ExecuteScalar(ConnectionString.ETMSRead, CommandType.Text, sql, null);
            if (ret < 1)
                isUsed = false;
            return isUsed;

        }

        /// <summary>
        /// 根据课程编号获取在线测试的可用总数（状态为“启用”）
        /// </summary>
        /// <param name="courseID">课程编号</param>
        /// <returns></returns>
        public Int32 Get_OnlineTestTotal(Guid courseID)
        {
            string commandName = string.Format("select COUNT(*) from Res_CourseRes where IsUse='1' and CourseID='{0}' and CourseResTypeID={1}", courseID, (Int32)Basic.API.Entity.EnumResourcesType.OnLineTest);
            return (Int32)SqlHelper.ExecuteScalar(ConnectionString.ETMSRead, CommandType.Text, commandName, null);
        }

        /// <summary>
        /// 根据课程编号获取在线测试的总数
        /// </summary>
        /// <param name="courseID">课程编号</param>
        /// <returns></returns>
        public Int32 GetALLOnlineTestTotal(Guid courseID)
        {
            string commandName = string.Format("select COUNT(*) from Res_CourseRes where CourseID='{0}' and CourseResTypeID={1}", courseID, (Int32)Basic.API.Entity.EnumResourcesType.OnLineTest);
            return (Int32)SqlHelper.ExecuteScalar(ConnectionString.ETMSRead, CommandType.Text, commandName, null);
        }



        /// <summary>
        /// 根据培训项目课程编号获取在线测试总数
        /// </summary>
        /// <param name="trainingItemCourseID"培训项目课程编号></param>
        /// <returns></returns>
        public Int32 GetItemCourseOnlineTestTotal(Guid trainingItemCourseID)
        {
            string commandName = string.Format("select COUNT(*) from Tr_ItemCourseRes where TrainingItemCourseID='{0}' and CourseResTypeID={1}", trainingItemCourseID, (Int32)Basic.API.Entity.EnumResourcesType.OnLineTest);
            return (Int32)SqlHelper.ExecuteScalar(ConnectionString.ETMSRead, CommandType.Text, commandName, null);
        }

        /// <summary>
        /// 获取某个学员的某个培训项目课程的某个在线测试已经考试的次数
        /// </summary>
        /// <param name="userID">学员ID</param>
        /// <param name="trainingItemCourseID"培训项目课程编号></param>
        /// <param name="onLineTestID">在线测试ID</param>
        /// <returns></returns>
        public int GetStudentOnLineTestExamNum(int userID, Guid trainingItemCourseID, Guid onLineTestID)
        {
            string sqlModal = "SELECT COUNT(*) FROM Ex_StudentOnlineTest WHERE StudentID='{0}' AND TrainingItemCourseID='{1}' AND OnLineTestID='{2}' AND EndTime IS NOT NULL";
            string commandName = string.Format(sqlModal, userID, trainingItemCourseID, onLineTestID);
            return (Int32)SqlHelper.ExecuteScalar(ConnectionString.ETMSRead, CommandType.Text, commandName, null);
        }


        /// <summary>
        /// 获取某个学员的某个培训项目课程的某个在线测试已经完成考试的次数
        /// </summary>
        /// <param name="userID">学员ID</param>
        /// <param name="trainingItemCourseID"培训项目课程编号></param>
        /// <param name="onLineTestID">在线测试ID</param>
        /// <returns></returns>
        public int GetStudentOnLineTestCompleteExamNum(int userID, Guid trainingItemCourseID, Guid onLineTestID)
        {
            string sqlModal = "SELECT COUNT(*) FROM Ex_StudentOnlineTest WHERE StudentID='{0}' AND TrainingItemCourseID='{1}' AND OnLineTestID='{2}' AND EndTime IS NOT NULL";
            string commandName = string.Format(sqlModal, userID, trainingItemCourseID, onLineTestID);
            return (Int32)SqlHelper.ExecuteScalar(ConnectionString.ETMSRead, CommandType.Text, commandName, null);

        }




        /// <summary>
        /// 获取某个培训项目课程的在线测试可以考试的次数,如果没有设置,默认返回1
        /// </summary>
        /// <param name="trainingItemCourseID"培训项目课程编号></param>
        /// <param name="onLineTestID">在线测试ID</param>
        /// <returns></returns>
        public int GetOnLineTestCanExamNum(Guid trainingItemCourseID, Guid onLineTestID)
        {
            string sqlModal = @"
                    SELECT oe.OrgExamNum
                    FROM Ex_OnLineTest ot
						INNER JOIN dbo.Tr_ItemCourseRes icr ON icr.CourseResID= ot.OnLineTestID AND	icr.CourseResTypeID=5
						INNER JOIN dbo.Tr_ItemCourse ic ON ic.TrainingItemCourseID=icr.TrainingItemCourseID
						INNER JOIN dbo.Tr_Item i ON i.TrainingItemID=ic.TrainingItemID
						INNER JOIN dbo.KS_OrgExamNum oe ON oe.OrgID = i.OrgID
                    WHERE icr.TrainingItemCourseID='{0}'
						AND icr.CourseResID='{1}'
                ";
            string commandName = string.Format(sqlModal, trainingItemCourseID, onLineTestID);
            var p = SqlHelper.ExecuteScalar(ConnectionString.ETMSRead, CommandType.Text, commandName, null);
            if (p != null)
                return (Int32)p;
            
            return 1;

        }

        /// <summary>
        /// 查询在线测试的试卷信息
        /// </summary>
        /// <param name="trainingItemCourseID">培训项目课程ID</param>
        /// <param name="TestPaperID">试卷ID</param>
        /// <param name="UserID">用户ID</param>
        /// <returns></returns>
        public DataTable GetOnlineTestExamTestPaper(Guid TrainingItemCourseID, Guid TestPaperID,int UserID)
        {
            string commandName = "[dbo].[Pr_KS_TestPaper_GetOnLineTestExamTestPaper]";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@TestPaperID", SqlDbType.UniqueIdentifier),
                    new SqlParameter("@TrainingItemCourseID", SqlDbType.UniqueIdentifier),
                    new SqlParameter("@UserID", SqlDbType.Int),
                };
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = TestPaperID;
            parms[1].Value = TrainingItemCourseID;
            parms[2].Value = UserID;
            #endregion
            return SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];
        }




    }
}
