using System;
using System.Data;

using System.Data.SqlClient;
using ETMS.Utility.Data;

namespace ETMS.Components.Point.Implement.DAL
{
    /// <summary>
    /// 学习过程积分扩展类
    /// 黄中福：2012－05－10
    /// </summary>
    public partial class Point_Student_PointReasonDetailDataAccess
    {
        /// <summary>
        /// 获取所有可以录入“学习过程积分”的培训项目列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetCanInputPointTrainingItemList()
        {
            string commandName = "[dbo].[Pr_Point_Student_PointReasonDetail_GetCanInputPointTrainingItemList]";
            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, null).Tables[0];
            return dt;
        }

        /// <summary>
        /// 获取某个组织机构的所有可以录入“学习过程积分”的培训项目列表
        /// </summary>
        /// <param name="orgID">组织机构ID</param>
        /// <returns></returns>
        public DataTable GetCanInputPointTrainingItemListByOrgID(int orgID)
        {
            string commandName = "[dbo].[Pr_Point_Student_PointReasonDetail_GetCanInputPointTrainingItemListByOrgID]";
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@OrgID", SqlDbType.Int)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = orgID;
            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];
            return dt;
        }

        /// <summary>
        /// 统计某个培训项目的学员已经录入但未发布的“学习过程”的积分情况
        /// FROM Point_Student_PointReasonDetail a
        /// INNER JOIN Sty_StudentSignup b on b.StudentSignupID =a.StudentSignupID
        /// INNER JOIN Tr_Item c on c.TrainingItemID =b.TrainingItemID
        /// INNER JOIN Site_User u on u.UserID=b.UserID
        /// INNER JOIN Site_Student s on s.UserID=u.UserID
        /// LEFT JOIN Sty_ClassStudent g on g.StudentSignupID = b.StudentSignupID
        /// LEFT JOIN Sty_Class h on h.ClassID = g.ClassID
        /// LEFT JOIN (
        ///     select
        ///         c.ClassStudentID,c.StudentSignupID,c.UserID
        ///         ,b.ClassSubgroupID,b.ClassSubgroupName
        ///     from Sty_ClassSubgroupStudent a
        ///         inner join Sty_ClassSubgroup b on b.ClassSubgroupID = a.ClassSubgroupID
        ///         inner join Sty_ClassStudent c on c.ClassStudentID = a.ClassStudentID
        ///         inner join Sty_Class d on d.ClassID = c.ClassID
        ///         inner join Sty_StudentSignup e on e.StudentSignupID =c.StudentSignupID
        ///         ) j on j.UserID= s.UserID and j.StudentSignupID=b.StudentSignupID
        /// </summary>
        /// <param name="trainingItemID">培训项目ID </param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortExpression"></param>
        /// <param name="criteria"></param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        public DataTable StatStudentInputPointListByTrainingItemID(Guid trainingItemID, int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {
            string commandName = "dbo.[Pr_Point_Student_PointReasonDetail_StatStudentInputPointListByTrainingItemID]";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@TrainingItemID", SqlDbType.Int),
					new SqlParameter("@PageIndex", SqlDbType.Int),
					new SqlParameter("@PageSize", SqlDbType.Int),
					new SqlParameter("@SortExpression", SqlDbType.VarChar),
					new SqlParameter("@Criteria", SqlDbType.VarChar),
					new SqlParameter("@ReturnValue", SqlDbType.Int, 4, ParameterDirection.ReturnValue, false, 0, 0, String.Empty, DataRowVersion.Default, null)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = trainingItemID;
            parms[1].Value = pageIndex;
            parms[2].Value = pageSize;
            parms[3].Value = sortExpression;
            parms[4].Value = criteria;
            #endregion
            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];
            totalRecords = (int)parms[5].Value;
            return dt;
        }

         /// <summary>
        /// 统计某个学员报名获得的未发布“学习过程”的积分总和
        /// </summary>
        /// <param name="studentSignupID">学员报名ID</param>
        /// <returns></returns>
        public Int64 StatStudentInputPointByStudentSignupID(Guid studentSignupID)
        {
            string sqlModal = "select ISNULL(SUM(Cast(AccessPoints As bigint)),0) AS AccessPoints from Point_Student_PointReasonDetail where StudentSignupID='{0}' and (IsIssuePoint=0 or IsIssuePoint is null)";
            string commandName = string.Format(sqlModal, studentSignupID);
            return (Int64)SqlHelper.ExecuteScalar(ConnectionString.ETMSRead, CommandType.Text, commandName, null);
        }

        /// <summary>
        /// 统计某个培训项目的学员已经录入但未发布的“学习过程”的积分情况
        /// FROM Point_Student_PointReasonDetail a
        /// INNER JOIN Sty_StudentSignup b on b.StudentSignupID =a.StudentSignupID
        /// INNER JOIN Tr_Item c on c.TrainingItemID =b.TrainingItemID
        /// INNER JOIN Site_User u on u.UserID=b.UserID
        /// INNER JOIN Site_Student s on s.UserID=u.UserID
        /// LEFT JOIN Sty_ClassStudent g on g.StudentSignupID = b.StudentSignupID
        /// LEFT JOIN Sty_Class h on h.ClassID = g.ClassID
        /// LEFT JOIN (
        ///     select
        ///         c.ClassStudentID,c.StudentSignupID,c.UserID
        ///         ,b.ClassSubgroupID,b.ClassSubgroupName
        ///     from Sty_ClassSubgroupStudent a
        ///         inner join Sty_ClassSubgroup b on b.ClassSubgroupID = a.ClassSubgroupID
        ///         inner join Sty_ClassStudent c on c.ClassStudentID = a.ClassStudentID
        ///         inner join Sty_Class d on d.ClassID = c.ClassID
        ///         inner join Sty_StudentSignup e on e.StudentSignupID =c.StudentSignupID
        ///         ) j on j.UserID= s.UserID and j.StudentSignupID=b.StudentSignupID
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortExpression"></param>
        /// <param name="criteria"></param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        public DataTable GetStudentInputPointAllInfoList( int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {
            string commandName = "dbo.[Pr_Point_Student_PointReasonDetail_GetStudentDetailPointAllInfoList]";
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
        /// 获取学习过程积分发布列表
        /// </summary>
        /// <param name="organizationID"></param>
        /// <returns></returns>
        public DataTable GetWaitPublishPointItemList(int organizationID)
        {
            string commandName = "dbo.[Pr_Point_Student_PointReasonDetail_GetWaitPublishItemList]";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@OrganizationID", SqlDbType.Int)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = organizationID;
            #endregion
            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];
            return dt;
        }

        /// <summary>
        /// 获取学习过程学员积分待发布列表
        /// </summary>
        /// <param name="organizationID"></param>
        /// <returns></returns>
        public DataTable GetNotPublishPointStudentList(Guid trainingItemID)
        {
            string commandName = "dbo.[Pr_Point_Student_PointReasonDetail_NotPublishStudentList]";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@TrainingItemID", SqlDbType.UniqueIdentifier)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = trainingItemID;
            #endregion
            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];
            return dt;
        }

        /// <summary>
        /// 学员学习过程积分发布
        /// </summary>
        public void StudentStudyProcessPublish(Guid trainingItemID, int userID, string userName)
        {
            string commandName = "dbo.Pr_Point_Student_PointReasonDetail_IssuePoint";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@TrainingItemID", SqlDbType.UniqueIdentifier),					
					new SqlParameter("@UserID", SqlDbType.Int),					
					new SqlParameter("@IssueUser", SqlDbType.NVarChar, 50)
					};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
            }

            parms[0].Value = trainingItemID;
            parms[1].Value = userID;
            parms[2].Value = userName;           
            #endregion
            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
        }
    }
}
