using System;
using System.Data;
using System.Data.SqlClient;
using ETMS.Utility.Data;

namespace ETMS.Components.Basic.Implement.DAL.LearningManagement.LearnProcessControl
{
    /// <summary>
    /// 学习过程监控
    /// </summary>
    public class ItemCourseControlDataAccess
    {

        /// <summary>
        /// 根据参数获取对象列表（分页，可排序）
        /// </summary>
        public DataTable GetPagedList(int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {
            string commandName = "dbo.Pr_Tr_ItemCourseControl_GetList";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@PageIndex", SqlDbType.Int),
					new SqlParameter("@PageSize", SqlDbType.Int),
					new SqlParameter("@SortExpression", SqlDbType.VarChar),
					new SqlParameter("@Criteria", SqlDbType.VarChar),
					new SqlParameter("@ReturnValue", SqlDbType.Int, 4, ParameterDirection.ReturnValue, false, 0, 0, String.Empty, DataRowVersion.Default, null)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = pageIndex;
            parms[1].Value = pageSize;
            parms[2].Value = sortExpression;
            parms[3].Value = criteria;
            #endregion
            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];
            totalRecords = (int)parms[4].Value;
            return dt;
        }

        /// <summary>
        /// 获取培训项目课程下开始学习人数与学习时长
        /// </summary>
        /// <param name="TrainingItemCourseID">项目课程ID</param>
        /// <returns></returns>
        public DataTable GetStudentNumSessionTime(Guid TrainingItemCourseID)
        {
            string commandName = "dbo.[Pr_Tr_ItemCourse_GetStudentNumSessionTime]";
            #region Parameters
            SqlParameter[] parms = new SqlParameter[] {
					new SqlParameter("@TrainingItemCourseID", SqlDbType.UniqueIdentifier)
				};

            parms[0].Value = TrainingItemCourseID;
            #endregion
            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];
            return dt;
        }

        /// <summary>
        /// 获取项目课程学员中已经开始学习的学员信息
        /// </summary>
        /// <param name="TrainingItemCourseID">项目课程ID</param>
        /// <returns></returns>
        public DataTable GetStudentOpenLearn(Guid TrainingItemCourseID)
        {
            string commandName = "dbo.[Pr_Tr_ItemCourse_GetStudentOpenLearn]";
            #region Parameters
            SqlParameter[] parms = new SqlParameter[] {
					new SqlParameter("@TrainingItemCourseID", SqlDbType.UniqueIdentifier)
				};

            parms[0].Value = TrainingItemCourseID;
            #endregion
            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];
            return dt;
        }
    }
}
