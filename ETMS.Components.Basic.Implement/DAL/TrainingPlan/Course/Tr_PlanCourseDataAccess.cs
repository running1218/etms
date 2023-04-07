using System;
using System.Data;

using System.Data.SqlClient;
using ETMS.Utility.Data;

namespace ETMS.Components.Basic.Implement.DAL.TrainingPlan.Course
{


    /// <summary>
    /// 培训计划课程扩展类
    /// 黄中福：2012－04－18
    /// </summary>
    public partial class Tr_PlanCourseDataAccess
    {


        /// <summary>
        /// 获取培训计划的所有课程信息列表
        ///FROM Tr_PlanCourse a 
        ///inner join Tr_Plan c on c.PlanID = a.PlanID
        ///inner join Res_Course e on e.CourseID = a.CourseID
        ///left join Tr_OuterOrg b on b.OuterOrgID = a.OuterOrgID
        /// </summary>
        /// <param name="pageIndex">起始页</param>
        /// <param name="pageSize">每页的记录数</param>
        /// <param name="sortExpression">排序方式</param>
        /// <param name="criteria">以 AND 打头的查询条件</param>
        /// <param name="totalRecords">所有满足条件的记录数</param>
        public DataTable GetPlanCourseALLInfoList(int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {
            string commandName = "[dbo].[Pr_Tr_PlanCourse_GetALLInfoList]";
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


    }
}
