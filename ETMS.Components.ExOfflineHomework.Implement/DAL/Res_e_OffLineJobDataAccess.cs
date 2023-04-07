using System;
using System.Data;

using System.Data.SqlClient;
using ETMS.Utility.Data;

namespace ETMS.Components.ExOfflineHomework.Implement.DAL
{
    /// <summary>
    /// ������ҵ�����ݷ���
    /// </summary>
    public partial class Res_e_OffLineJobDataAccess
    {
        /// <summary>
        /// ���ݲ�����ȡ�����б���ҳ��������
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
        /// ����������ҵ��ź�����״̬����ȡλ�ύ��ҵ��ѧԱ����������ҵ�����¼
        /// </summary>
        /// <param name="itemCourseOffLineJobID">�γ�������ҵID</param>
        /// <param name="studentJobStatus">��ҵ����״̬0��δ���� 1��������</param>
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
        /// ��������״̬����ȡ�ύ��ҵ��ѧԱ����������ҵ�����¼
        /// </summary>      
        /// <param name="studentJobStatus">��ҵ����״̬0��δ���� 1��������</param>
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
        /// ��ȡδ�ύ��ҵ��ѧԱ
        /// </summary>      
        /// <returns></returns>
        public DataTable GetNoSumbitStuList()
        {
            string commandName = "[dbo].[Pr_Res_e_OffLineJob_GetNoSubmitStuList]";       
            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName).Tables[0];
            return dt;
        }
        /// <summary>
        /// ����������ҵ
        /// </summary>
        /// <param name="studentJobID">ѧԱ������ҵ�����¼���</param>
        /// <param name="markFilePath">������ҵ���·��</param>
        /// <param name="markFileName">������ҵ����</param>
        /// <param name="evaluationUser">������Ա</param>
        /// <param name="evaluation">����</param>
        /// <param name="score">����</param>
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
        /// ��ȡĳ����ѵ��Ŀ�γ̵Ŀ���������ҵ������״̬Ϊ�����á���
        /// </summary>
        /// <param name="trainingItemCourseID">��ѵ��Ŀ�γ�ID</param>
        /// <returns></returns>
        public Int32 GetOfflineJobTotalByTrainingItemCourseID(Guid trainingItemCourseID)
        {
            string commandName = string.Format("select COUNT(*) from Res_ItemCourseOffLineJob where IsUse='1' and TrainingItemCourseID='{0}'", trainingItemCourseID, (Int32)Basic.API.Entity.EnumResourcesType.OnLineJob);
            return (Int32)SqlHelper.ExecuteScalar(ConnectionString.ETMSRead, CommandType.Text, commandName, null);
        }




    }
}
