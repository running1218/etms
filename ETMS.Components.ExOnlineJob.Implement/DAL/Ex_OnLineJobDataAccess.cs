using System;
using ETMS.Utility.Data;
using System.Data.SqlClient;
using System.Data;
namespace ETMS.Components.ExOnlineJob.Implement.DAL.ExOnlineJob
{
    public partial class Ex_OnLineJobDataAccess
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
            string sql = string.Format(sqlModal, resID, (int)Basic.API.Entity.EnumResourcesType.OnLineJob);
            int ret = (Int32)SqlHelper.ExecuteScalar(ConnectionString.ETMSRead, CommandType.Text, sql, null);
            if (ret < 1)
                isUsed = false;
            return isUsed;

        }


        /// <summary>
        /// 根据课程编号获取在线作业的可用总数（状态为“启用”）
        /// </summary>
        /// <param name="courseID">课程编号</param>
        /// <returns></returns>
        public Int32 Get_OnlineJobTotal(Guid courseID)
        {
            string commandName = string.Format("select COUNT(*) from Res_CourseRes where IsUse='1' and CourseID='{0}' and CourseResTypeID={1}", courseID, (Int32)Basic.API.Entity.EnumResourcesType.OnLineJob);
            return (Int32)SqlHelper.ExecuteScalar(ConnectionString.ETMSRead, CommandType.Text, commandName, null);
        }

        /// <summary>
        /// 根据课程编号获取在作业的总数
        /// </summary>
        /// <param name="courseID">课程编号</param>
        /// <returns></returns>
        public Int32 GetALLOnlineJobTotal(Guid courseID)
        {
            string commandName = string.Format("select COUNT(*) from Res_CourseRes where CourseID='{0}' and CourseResTypeID={1}", courseID, (Int32)Basic.API.Entity.EnumResourcesType.OnLineJob);
            return (Int32)SqlHelper.ExecuteScalar(ConnectionString.ETMSRead, CommandType.Text, commandName, null);
        }



        /// <summary>
        ///  根据培训项目课程编号获取在线作业总数
        /// </summary>
        /// <param name="trainingItemCourseID">培训项目课程编号</param>
        /// <returns></returns>
        public Int32 GetItemCourseOnlineJobTotal(Guid trainingItemCourseID)
        {
            string commandName = string.Format("select COUNT(*) from Tr_ItemCourseRes where TrainingItemCourseID='{0}' and CourseResTypeID={1}", trainingItemCourseID, (Int32)Basic.API.Entity.EnumResourcesType.OnLineJob);
            return (Int32)SqlHelper.ExecuteScalar(ConnectionString.ETMSRead, CommandType.Text, commandName, null);
        }
        /// <summary>
        /// 查询在线作业的试卷信息
        /// </summary>
        /// <param name="trainingItemCourseID">培训项目课程ID</param>
        /// <param name="TestPaperID">试卷ID</param>
        /// <returns></returns>
        public DataTable GetOnlineJobExamTestPaper(Guid TrainingItemCourseID, Guid TestPaperID)
        {
            string commandName = "[dbo].[Pr_KS_TestPaper_GetOnlineJobExamTestPaper]";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@TestPaperID", SqlDbType.UniqueIdentifier),
                    new SqlParameter("@TrainingItemCourseID", SqlDbType.UniqueIdentifier)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = TestPaperID;
            parms[1].Value = TrainingItemCourseID;
            #endregion
            return SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];
        }

    }
}
