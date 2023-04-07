using System;
using System.Data;

using System.Data.SqlClient;
using ETMS.Utility.Data;

using ETMS.Components.ExOnlineJob.API.Entity.ExOnlineJob;

namespace ETMS.Components.ExOnlineJob.Implement.DAL.ExOnlineJob
{
    /// <summary>
    /// 数据访问
    /// </summary>
    public partial class Ex_StudentOnlineJobDataAccess
    {
       
        /// <summary>
        /// 根据标识获取对象
        /// </summary>
        public Ex_StudentOnlineJob GetByStudentOnlineJob(Guid onlineJobID, int userID, Guid trainingItemCourseID)
        {
            Ex_StudentOnlineJob ex_StudentOnlineJob = null;

            string commandName = "dbo.Pr_Ex_StudentOnlineJob_GetByStudentOnlineJob";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@OnlineJobID", SqlDbType.UniqueIdentifier),
                    new SqlParameter("@UserID", SqlDbType.Int),
                    new SqlParameter("@TrainingItemCourseID", SqlDbType.UniqueIdentifier)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = onlineJobID;
            parms[1].Value = userID;
            parms[2].Value = trainingItemCourseID;

            #endregion
            using (SqlDataReader dataReader = SqlHelper.ExecuteReader(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms))
            {
                if (dataReader.Read())
                {
                    ex_StudentOnlineJob = PopulateEx_StudentOnlineJobFromDataReader(dataReader);
                }
            }

            return ex_StudentOnlineJob;
        }


    }
}
