using System;
using System.Data;

using System.Data.SqlClient;
using ETMS.Utility.Data;

using ETMS.Components.ExContest.API.Entity.StudentContest;

namespace ETMS.Components.ExContest.Implement.DAL.StudentContest
{
    public partial class Ex_StudentContestDataAccess
    {
        /// <summary>
        /// 根据标识获取对象
        /// </summary>
        public Ex_StudentContest GetByStudentContest(Guid contestID, int userID)
        {
            Ex_StudentContest ex_StudentContest = null;

            string commandName = "dbo.Pr_Ex_StudentContest_GetByStudentContest";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@ContestID", SqlDbType.UniqueIdentifier),
                    new SqlParameter("@UserID", SqlDbType.Int)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = contestID;
            parms[1].Value = userID;


            #endregion
            using (SqlDataReader dataReader = SqlHelper.ExecuteReader(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms))
            {
                if (dataReader.Read())
                {
                    ex_StudentContest = PopulateEx_StudentContestFromDataReader(dataReader);
                }
            }

            return ex_StudentContest;
        }


    }
}
