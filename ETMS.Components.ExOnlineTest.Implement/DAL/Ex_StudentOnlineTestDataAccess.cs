using System;
using System.Data;

using System.Data.SqlClient;
using ETMS.Utility.Data;

using ETMS.Components.ExOnlineTest.API.Entity.ExOnlineTest;

namespace ETMS.Components.ExOnlineTest.Implement.DAL.ExOnlineTest
{
    public partial class Ex_StudentOnlineTestDataAccess
    {
        /// <summary>
        /// 根据标识获取对象
        /// </summary>
        public Ex_StudentOnlineTest GetByStudentOnlineTest(Guid onlineTestID, int userID, Guid trainingItemCourseID)
        {
            Ex_StudentOnlineTest ex_StudentOnlineTest = null;

            string commandName = "dbo.Pr_Ex_StudentOnlineTest_GetByStudentOnlineTest";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@OnlineTestID", SqlDbType.UniqueIdentifier),
                    new SqlParameter("@UserID", SqlDbType.Int),
                    new SqlParameter("@TrainingItemCourseID", SqlDbType.UniqueIdentifier)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = onlineTestID;
            parms[1].Value = userID;
            parms[2].Value = trainingItemCourseID;

            #endregion
            using (SqlDataReader dataReader = SqlHelper.ExecuteReader(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms))
            {
                if (dataReader.Read())
                {
                    ex_StudentOnlineTest = PopulateEx_StudentOnlineTestFromDataReader(dataReader);
                }
            }

            return ex_StudentOnlineTest;
        }

        /// <summary>
        /// 根据标识获取对象（是否提交答卷）
        /// </summary>
        public Ex_StudentOnlineTest GetByStudentOnlineTestSubmit(Guid onlineTestID, int userID, Guid trainingItemCourseID)
        {
            Ex_StudentOnlineTest ex_StudentOnlineTest = null;

            string commandName = "dbo.Pr_Ex_StudentOnlineTest_GetByStudentOnlineTestUnSubmit";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@OnlineTestID", SqlDbType.UniqueIdentifier),
                    new SqlParameter("@UserID", SqlDbType.Int),
                    new SqlParameter("@TrainingItemCourseID", SqlDbType.UniqueIdentifier)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = onlineTestID;
            parms[1].Value = userID;
            parms[2].Value = trainingItemCourseID;

            #endregion
            //using (SqlDataReader dataReader = SqlHelper.ExecuteReader(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms))
            //{
            //    if (dataReader.Read())
            //    {
            //        ex_StudentOnlineTest = PopulateEx_StudentOnlineTestFromDataReader(dataReader);
            //    }
            //}

            DataTable tab = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];
            if (tab != null && tab.Rows.Count > 0) {
                ex_StudentOnlineTest = PopulateEx_StudentOnlineTestFromDataRow(tab.Rows[0]);
            }

            return ex_StudentOnlineTest;
        }
        /// <summary>
        /// 查询最大成绩
        /// </summary>
        /// <param name="onlineTestID"></param>
        /// <param name="userID"></param>
        /// <param name="trainingItemCourseID"></param>
        /// <returns></returns>
        public string maxScore(Guid onlineTestID, int userID, Guid trainingItemCourseID)
        {
            string commandName = string.Format("SELECT MAX(Score) FROM [dbo].[Ex_StudentOnlineTest] WHERE [OnlineTestID] ='{0}' and  [StudentID]= {1} and [TrainingItemCourseID]='{2}' and EndTime IS NOT NULL", onlineTestID, userID, trainingItemCourseID);
            return decimal.Round((decimal)SqlHelper.ExecuteScalar(ConnectionString.ETMSRead, CommandType.Text, commandName, null), 2).ToString();

        }
    }
}
