using System;
using ETMS.Utility.Data;
using System.Data.SqlClient;
using System.Data;

namespace ETMS.Components.ExOnlineTest.Implement.DAL
{
    public class Res_Student_OnlineTestDataAccess
    {
        /// <summary>
        /// 获取学员在线作业列表
        /// </summary>
        /// <param name="userID">学员编号</param>
        /// <param name="ItemID">项目ID</param>
        /// <param name="ItemCourseID">项目课程ID</param>
        /// <returns></returns>
        public DataTable GetOnlineTestListByUserID(int userID,Guid ItemID,Guid ItemCourseID)
        {
            string commandName = "[dbo].[Pr_Ex_OnlineTest_ListByUserID]";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@UserID", SqlDbType.Int),
                    new SqlParameter("@ItemID", SqlDbType.UniqueIdentifier),
                    new SqlParameter("@ItemCourseID", SqlDbType.UniqueIdentifier)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = userID;
            parms[1].Value = ItemID;
            parms[2].Value = ItemCourseID;
            #endregion
            return SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];

        }
    }
}
