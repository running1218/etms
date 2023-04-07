using System;
using ETMS.Utility.Data;
using System.Data.SqlClient;
using System.Data;

namespace ETMS.Components.Courseware.Implement.DAL
{
    public class Res_Student_CoursewareDataAccess
    {
        /// <summary>
        /// 获取学员在线作业列表
        /// </summary>
        /// <param name="userID">学员编号</param>
        /// <param name="ItemCourseID">项目课程ID</param>
        /// <returns></returns>
        public DataTable GetCoursewareListByUserID(int userID, Guid ItemCourseID)
        {
            string commandName = "[dbo].[Pr_Ex_Courseware_ListByUserID]";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@UserID", SqlDbType.Int),
                    new SqlParameter("@ItemCourseID", SqlDbType.UniqueIdentifier)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = userID;
            parms[1].Value = ItemCourseID;
            #endregion
            return SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];

        }

        /// <summary>
        /// 跟据课程ID，UserID 获得学习时长、已学资源、全部资源
        /// </summary>
        /// <param name="userID">UserID</param>
        /// <param name="coursewareID">课程ID</param>
        /// <returns></returns>
        public DataTable GetCoursewareSessionTimeByCoursewareID(int userID,Guid itemCourseResID, Guid coursewareID)
        {
            string commandName = "dbo.Pr_rs_e_CoursewareSessionTimeByCoursewareID";

            SqlParameter[] param = new SqlParameter[]{
                new SqlParameter("@UserID",SqlDbType.Int),
                new SqlParameter("@CoursewareID",SqlDbType.UniqueIdentifier),
                new SqlParameter("@ItemCourseResID",SqlDbType.UniqueIdentifier)
            };

            param[0].Value = userID;
            param[1].Value = coursewareID;
            param[2].Value = itemCourseResID;

            return SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, param).Tables[0];
        }
    }
}
