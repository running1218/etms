using System;
using System.Data;

using System.Data.SqlClient;
using ETMS.Utility.Data;

namespace ETMS.Components.ExOfflineHomework.Implement.DAL
{
    /// <summary>
    /// 离线作业表数据访问
    /// </summary>
    public partial class Res_e_OffLineJobDataAccess
    {
        /// <summary>
        /// 根据参数获取对象列表（分页，可排序）
        /// </summary>
        public DataTable GetUserOffLineJobs(int userID, Guid trainingItemCourseID)
        {
            string commandName = "dbo.Pr_Res_e_OffLineJob_GetUserJobs";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@UserID", SqlDbType.Int),
                    new SqlParameter("@TrainingItemCourseID", SqlDbType.UniqueIdentifier)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = userID;
            parms[1].Value = trainingItemCourseID;

            #endregion
            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];
            return dt;
        }

        public DataTable GetUserCourseOffLineJobs(int userID, Guid trainingItemCourseID)
        {
            string commandName = "dbo.Pr_Res_e_GetUserCourseOffLineJobs";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@UserID", SqlDbType.Int),
                    new SqlParameter("@TrainingItemCourseID", SqlDbType.UniqueIdentifier)
                };
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = userID;
            parms[1].Value = trainingItemCourseID;

            #endregion
            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];
            return dt;
        }


        /// <summary>
        /// 根据离线作业编号和批阅状态，获取位提交作业的学员或者离线作业作答记录
        /// </summary>
        /// <param name="itemCourseOffLineJobID">课程离线作业ID</param>
        /// <param name="studentJobStatus">作业批阅状态0：未批阅 1：已批阅</param>
        /// <returns></returns>
        public DataTable GetStudentListbyItemCourseOffLineJobID(Guid itemCourseOffLineJobID, int studentJobStatus)
        {
            string commandName = "[dbo].[Pr_Res_e_OffLineJob_GetStudentListbyItemCourseOffLineJobID]";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@ItemCourseOffLineJobID", SqlDbType.UniqueIdentifier),
					new SqlParameter("@StudentJobStatus", SqlDbType.Int),
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = itemCourseOffLineJobID;
            parms[1].Value = studentJobStatus;
            #endregion
            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];
            return dt;
        }
        /// <summary>
        /// 根据批阅状态，获取提交作业的学员或者离线作业作答记录
        /// </summary>      
        /// <param name="studentJobStatus">作业批阅状态0：未批阅 1：已批阅</param>
        /// <returns></returns>
        public DataTable GetStuList(string JobName,string ItemName,int studentJobStatus)
        {
            string commandName = "[dbo].[Pr_Res_e_OffLineJob_GetStuListbyItemCourseOffLineJobID]";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@JobName", SqlDbType.NVarChar),
                    new SqlParameter("@ItemName", SqlDbType.NVarChar),
                    new SqlParameter("@StudentJobStatus", SqlDbType.Int)
                };
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }
            parms[0].Value = JobName;
            parms[1].Value = ItemName;
            parms[2].Value = studentJobStatus;
            #endregion
            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];
            return dt;
        }
        /// <summary>
        /// 获取未提交作业的学员
        /// </summary>      
        /// <returns></returns>
        public DataTable GetNoSumbitStuList()
        {
            string commandName = "[dbo].[Pr_Res_e_OffLineJob_GetNoSubmitStuList]";       
            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName).Tables[0];
            return dt;
        }
        /// <summary>
        /// 批阅离线作业
        /// </summary>
        /// <param name="studentJobID">学员离线作业作答记录编号</param>
        /// <param name="markFilePath">离线作业存放路径</param>
        /// <param name="markFileName">离线作业名称</param>
        /// <param name="evaluationUser">批阅人员</param>
        /// <param name="evaluation">评语</param>
        /// <param name="score">分数</param>
        public void SetEvaluationOffLineJob(Guid studentJobID, string markFilePath, string markFileName, string evaluationUser, string evaluation, decimal? score)
        {
            string commandName = "[dbo].[Pr_Res_e_OffLineJob_SetEvaluationOffLineJob]";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@StudentJobID", SqlDbType.UniqueIdentifier),
					new SqlParameter("@MarkFileName", SqlDbType.NVarChar, 100),
					new SqlParameter("@MarkFilePath", SqlDbType.NVarChar, 128),
					new SqlParameter("@EvaluationUser", SqlDbType.NVarChar, 64),
					new SqlParameter("@Evaluation", SqlDbType.NVarChar),
					new SqlParameter("@Score", SqlDbType.Decimal),
					};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
            }
 
            parms[0].Value = studentJobID;
            parms[1].Value = markFileName;
            parms[2].Value = markFilePath;
            parms[3].Value = evaluationUser;
            parms[4].Value = evaluation;
            parms[5].Value = score;
            
            #endregion
            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
        }



        /// <summary>
        /// 获取某个培训项目课程的可用离线作业总数（状态为“启用”）
        /// </summary>
        /// <param name="trainingItemCourseID">培训项目课程ID</param>
        /// <returns></returns>
        public Int32 GetOfflineJobTotalByTrainingItemCourseID(Guid trainingItemCourseID)
        {
            string commandName = string.Format("select COUNT(*) from Res_ItemCourseOffLineJob where IsUse='1' and TrainingItemCourseID='{0}'", trainingItemCourseID, (Int32)Basic.API.Entity.EnumResourcesType.OnLineJob);
            return (Int32)SqlHelper.ExecuteScalar(ConnectionString.ETMSRead, CommandType.Text, commandName, null);
        }




    }
}
