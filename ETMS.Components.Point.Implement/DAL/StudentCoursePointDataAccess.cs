using System;
using System.Data;

using System.Data.SqlClient;
using ETMS.Utility.Data;


namespace ETMS.Components.Point.Implement.DAL
{
    /// <summary>
    /// 学生课程积分数据访问类
    /// 黄中福：2012－05－08
    /// </summary>
    public partial class StudentCoursePointDataAccess
    {

        /// <summary>
        /// 获取某个培训项目课程的课时对应设置的课程积分数，如果返回-100,说明没有设置对应的积分
        /// </summary>
        /// <param name="trainingItemCourseID">培训项目课程ID</param>
        /// <returns></returns>
        public int GetTrainingItemCourseHourseGivePoint(Guid trainingItemCourseID)
        {
            string sqlModal = @"
                SELECT 
	                cr.GivePoints
                FROM Point_Student_CourseRole cr
	                INNER JOIN (
		                select
			                t.OrgID,tc.CourseHours,tc.CourseAttrID
		                from  Tr_ItemCourse tc
			                INNER JOIN dbo.Tr_Item t ON t.TrainingItemID = tc.TrainingItemID
		                where tc.TrainingItemCourseID = '{0}'
		                ) tc ON tc.OrgID = cr.OrgID AND tc.CourseAttrID = cr.CourseAttrID
                WHERE tc.CourseHours >cr.MinNum and tc.CourseHours <= cr.MaxNum
                ";
            string commandName = string.Format(sqlModal, trainingItemCourseID);
            object ret = SqlHelper.ExecuteScalar(ConnectionString.ETMSRead, CommandType.Text, commandName, null);
            if (ret == null)
                return -100;
            if (ret.ToString() == "")
                return -100;
            return int.Parse(ret.ToString());
        }


        /// <summary>
        /// 判断某个培训项目课程的课时是否设置有对应的积分
        /// </summary>
        /// <param name="trainingItemCourseID"></param>
        /// <returns></returns>
        public bool CheckTrainingItemCourseIsHourseGivePoint(Guid trainingItemCourseID)
        {
            int givePoint = GetTrainingItemCourseHourseGivePoint(trainingItemCourseID);
            if (givePoint != -100)
                return false;
            return true;
        }



        /// <summary>
        /// 获取某个培训项目课程已经发布积分的学员数
        /// </summary>
        /// <param name="trainingItemCourseID">培训项目课程ID</param>
        /// <returns></returns>
        public int GetIssuePointsStudentNumByTrainingItemCourseID(Guid trainingItemCourseID)
        {
            string sqlModal = @"
                SELECT COUNT(*)
                FROM dbo.Sty_StudentCourse sc
                WHERE sc.TrainingItemCourseID = '{0}'
                    AND sc.IsIssueScore = 1
                ";
            string commandName = string.Format(sqlModal, trainingItemCourseID);
            return (int)SqlHelper.ExecuteScalar(ConnectionString.ETMSRead, CommandType.Text, commandName, null);
        }



        /// <summary>
        /// 获取某个培训项目课程可以发布积分的学员数
        /// </summary>
        /// <param name="trainingItemCourseID">培训项目课程ID</param>
        /// <returns></returns>
        public int GetCanIssuePointsStudentNumByTrainingItemCourseID(Guid trainingItemCourseID)
        {
            string sqlModal = @"
                SELECT COUNT(*)
                FROM dbo.Sty_StudentCourse sc
                WHERE sc.TrainingItemCourseID = '{0}'
                    AND sc.IsIssueScore = 0 AND AccessPoints > 0
                ";
            string commandName = string.Format(sqlModal, trainingItemCourseID);
            return (int)SqlHelper.ExecuteScalar(ConnectionString.ETMSRead, CommandType.Text, commandName, null);
        }



        /// <summary>
        /// 获取某个培训项目课程未发布积分且没有积分（积分为零）的学员数
        /// </summary>
        /// <param name="trainingItemCourseID">培训项目课程ID</param>
        /// <returns></returns>
        public int GetNoPointsStudentNumByTrainingItemCourseID(Guid trainingItemCourseID)
        {
            string sqlModal = @"
                SELECT COUNT(*)
                FROM dbo.Sty_StudentCourse sc
                WHERE sc.TrainingItemCourseID = '{0}'
                    AND sc.IsIssueScore = 0 AND (AccessPoints = 0 OR AccessPoints IS NULL)
                ";
            string commandName = string.Format(sqlModal, trainingItemCourseID);
            return (int)SqlHelper.ExecuteScalar(ConnectionString.ETMSRead, CommandType.Text, commandName, null);
        }




        /// <summary>
        /// 获取选课的所有学生的获得积分的列表
        /// FROM Sty_StudentCourse a
        /// INNER JOIN Sty_StudentSignup b on b.StudentSignupID =a.StudentSignupID
        /// INNER JOIN Tr_Item c on c.TrainingItemID =b.TrainingItemID
        /// INNER JOIN Tr_ItemCourse d on d.TrainingItemCourseID =a.TrainingItemCourseID
        /// INNER JOIN Res_Course e on e.CourseID = d.CourseID
        /// INNER JOIN Site_User u on u.UserID=b.UserID
        /// INNER JOIN Site_Student s on s.UserID=u.UserID
        /// LEFT JOIN Sty_ClassStudent g on g.StudentSignupID = b.StudentSignupID
        /// LEFT JOIN Sty_Class h on h.ClassID = g.ClassID
        /// LEFT JOIN Sty_ClassSubgroup j on j.ClassID = h.ClassID
        /// j.ClassSubgroupID,j.ClassSubgroupName
        /// </summary>
        /// <param name="pageIndex">起始页</param>
        /// <param name="pageSize">每页的记录数</param>
        /// <param name="sortExpression">排序方式</param>
        /// <param name="criteria">以 AND 打头的查询条件</param>
        /// <param name="totalRecords">所有满足条件的记录数</param>
        public DataTable GetStudentCoursePointAllInfoList(int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {
            string commandName = "[dbo].[Pr_Point_Student_Course_GetStudentCoursePointAllInfoList]";
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
        /// 获取所有可以计算积分的培训项目课程列表
        /// from Tr_ItemCourse tc
        ///     inner join Tr_Item t on t.TrainingItemID = tc.TrainingItemID
        ///     inner join Res_Course c on c.CourseID = tc.CourseID
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortExpression"></param>
        /// <param name="criteria"></param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        public DataTable GetCanComputePointCourseList(int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {
            string commandName = "[dbo].[Pr_Point_Student_Course_GetCanComputePointCourseList]";
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
        /// 获取所有可以计算积分的培训项目列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetCanComputeCoursePointTrainingItemList()
        {
            string commandName = "[dbo].[Pr_Point_Student_Course_GetCanComputePointTrainingItemList]";
            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, null).Tables[0];
            return dt;
        }



        /// <summary>
        /// 获取某个组织机构下的所有可以计算积分的培训项目列表
        /// </summary>
        /// <param name="orgID">组织机构ID</param>
        /// <returns></returns>
        public DataTable GetCanComputeCoursePointTrainingItemListByOrgID(int orgID)
        {
            string commandName = "[dbo].[Pr_Point_Student_Course_GetCanComputePointTrainingItemListByOrgID]";
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
        /// 计算某个培训项目课程的课程积分
        /// </summary>
        /// <param name="trainingItemCourseID">培训项目课程ID</param>
        /// <param name="accessPointsMode">获得积分方式:1计算,2手动</param>
        /// <param name="passLine">分数线：能获取积分的分数线</param>
        /// <param name="accessPointsUser">操作员</param>
        /// <returns></returns>
        public int ComputeCoursePointByTrainingItemCourseID(Guid trainingItemCourseID, int accessPointsMode,int passLine, string accessPointsUser)
        {
            string commandName = "[dbo].[Pr_Point_Student_Course_ComputePoint]";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@TrainingItemCourseID", SqlDbType.UniqueIdentifier),
					new SqlParameter("@AccessPointsMode", SqlDbType.Int),
					new SqlParameter("@PassLine", SqlDbType.Int),
					new SqlParameter("@AccessPointsUser", SqlDbType.VarChar),
					new SqlParameter("@ReturnValue", SqlDbType.Int, 4, ParameterDirection.ReturnValue, false, 0, 0, String.Empty, DataRowVersion.Default, null)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
            }

            parms[0].Value = trainingItemCourseID;
            parms[1].Value = accessPointsMode;
            parms[2].Value = passLine;
            parms[3].Value = accessPointsUser;
            #endregion

            int totalRecords = SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
            totalRecords = (int)parms[4].Value;

            return totalRecords;
        }


        /// <summary>
        /// 计算某个培训项目课程的课程积分
        /// </summary>
        /// <param name="trainingItemCourseID">培训项目课程ID</param>
        /// <param name="accessPointsMode">获得积分方式:1计算,2手动</param>
        /// <param name="passLine">分数线：能获取积分的分数线</param>
        /// <param name="accessPointsUser">操作员</param>
        /// <returns></returns>
        public int ComputeCoursePointByStudentCourseID(Guid trainingItemCourseID, Guid studentCourseID, int accessPointsMode, int passLine, string accessPointsUser)
        {
            string commandName = "[dbo].[Pr_Point_Student_Course_ComputePointByStudentCourseID]";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@TrainingItemCourseID", SqlDbType.UniqueIdentifier),
					new SqlParameter("@StudentCourseID", SqlDbType.UniqueIdentifier),
					new SqlParameter("@AccessPointsMode", SqlDbType.Int),
					new SqlParameter("@PassLine", SqlDbType.Int),
					new SqlParameter("@AccessPointsUser", SqlDbType.VarChar),
					new SqlParameter("@ReturnValue", SqlDbType.Int, 4, ParameterDirection.ReturnValue, false, 0, 0, String.Empty, DataRowVersion.Default, null)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
            }

            parms[0].Value = trainingItemCourseID;
            parms[1].Value = studentCourseID;
            parms[2].Value = accessPointsMode;
            parms[3].Value = passLine;
            parms[4].Value = accessPointsUser;
            #endregion

            int totalRecords = SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
            totalRecords = (int)parms[5].Value;

            return totalRecords;
        }



        /// <summary>
        /// 统计某个培训项目课程的积分总和
        /// </summary>
        /// <param name="trainingItemCourseID">培训项目课程ID</param>
        /// <returns></returns>
        public int StatCoursePointByTrainingItemCourseID(Guid trainingItemCourseID)
        {
            string sqlModal = "select ISNULL(SUM(AccessPoints),0) AS AccessPoints from Sty_StudentCourse where TrainingItemCourseID='{0}' ";
            string commandName = string.Format(sqlModal, trainingItemCourseID);
            return (int)SqlHelper.ExecuteScalar(ConnectionString.ETMSRead, CommandType.Text, commandName, null);
        }






        /// <summary>
        /// 获取所有可以发布课程积分的培训项目列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetCanIssueCoursePointTrainingItemList()
        {
            string commandName = "[dbo].[Pr_Point_Student_Course_GetCanIssuePointTrainingItemList]";
            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, null).Tables[0];
            return dt;
        }

        /// <summary>
        /// 获取所有可以发布积分的培训项目课程列表
        /// from Tr_ItemCourse tc
        ///     inner join Tr_Item t on t.TrainingItemID = tc.TrainingItemID
        ///     inner join Res_Course c on c.CourseID = tc.CourseID
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortExpression"></param>
        /// <param name="criteria"></param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        public DataTable GetCanIssuePointCourseList(int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {
            string commandName = "[dbo].[Pr_Point_Student_Course_GetCanIssuePointCourseList]";
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
        /// 获取某个组织机构下的所有可以发布积分的培训项目列表
        /// </summary>
        /// <param name="orgID">组织机构ID</param>
        /// <returns></returns>
        public DataTable GetCanIssueCoursePointTrainingItemListByOrgID(int orgID)
        {
            string commandName = "[dbo].[Pr_Point_Student_Course_GetCanIssuePointTrainingItemListByOrgID]";
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
        /// 发布满足指定查询条件的学员选课的课程积分
        /// </summary>
        /// <param name="criteria">制定的查询条件（参考GetStudentCoursePointAllInfoList方法）</param>
        /// <param name="scoreIssueUser">操作员</param>
        /// <param name="scoreIssueUserID">操作员ID</param>
        /// <returns></returns>
        public int IssueCoursePointByConditionSQL(string criteria, string scoreIssueUser, int scoreIssueUserID)
        {
            string commandName = "[dbo].[Pr_Point_Student_Course_IssuePointByConditionSQL]";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@Criteria", SqlDbType.VarChar),
					new SqlParameter("@ScoreIssueUserID", SqlDbType.Int),
					new SqlParameter("@ScoreIssueUser", SqlDbType.VarChar),
					new SqlParameter("@ReturnValue", SqlDbType.Int, 4, ParameterDirection.ReturnValue, false, 0, 0, String.Empty, DataRowVersion.Default, null)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
            }

            parms[0].Value = criteria;
            parms[1].Value = scoreIssueUserID;
            parms[2].Value = scoreIssueUser;
            #endregion
            int totalRecords = SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
            totalRecords = (int)parms[3].Value;
            return totalRecords;
        }
    

        

        /// <summary>
        /// 积分排名
        /// </summary>
        /// <returns></returns>
        public DataTable GetStudentRanking(int OrganizationID)
        {
            string commandName = "[dbo].[Pr_Point_Student_TotalRecord_GetStudentRanking]";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] { 
					new SqlParameter("@OrganizationID", SqlDbType.Int)
				}; 
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
            }

            parms[0].Value = OrganizationID;
            #endregion
            return SqlHelper.ExecuteDataset(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms).Tables[0];
        }





        /// <summary>
        /// 批量删除（手动录入/计算）学员选课的积分
        /// </summary>
        /// <param name="studentCourseIDArray">要批量删除积分的学员选课ID数组</param>
        /// <param name="accessPointsUser">操作员姓名</param>
        public int BatchDeleteStudentCoursePoints(Guid[] studentCourseIDArray, string accessPointsUser)
        {
            string sqlModal = @"
                    UPDATE [dbo].Sty_StudentCourse SET      
                        AccessPoints = 0,      
                        AccessPointsUser = null,      
                        AccessPointsTime = null,
                        ModifyTime = GETDATE(),
                        ModifyUser = '{0}'
                   WHERE IsIssueScore=0 AND AccessPoints > 0 AND StudentCourse IN ({1})
                ";
            string guid = "";
            for (int i = 0; i < studentCourseIDArray.Length; i++)
            {
                guid += "'" + studentCourseIDArray[i].ToString() + "'";
                if (i < studentCourseIDArray.Length - 1)
                    guid += ",";
            }
            if (guid.Trim().Length > 0)
            {
                string sql = string.Format(sqlModal, accessPointsUser, guid);
                return SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.Text, sql);
            }
            return 0;
        }



        /// <summary>
        /// 删除某个培训项目课程的（手动录入/计算）的未发布积分
        /// </summary>
        /// <param name="trainingItemCourseID">要除积分的培训项目课程OD</param>
        /// <param name="accessPointsMode">获得积分方式:1计算,2手动</param>
        /// <param name="accessPointsUser">操作员姓名</param>
        public int DeleteStudentCoursePointsByTrainingItemCourseID(Guid trainingItemCourseID, int accessPointsMode, string accessPointsUser)
        {
            string sqlModal = @"
                    UPDATE [dbo].Sty_StudentCourse SET      
                        AccessPoints = 0,      
                        AccessPointsUser = null,      
                        AccessPointsTime = null,
                        ModifyTime = GETDATE(),
                        ModifyUser = '{0}'
                    WHERE IsIssueScore=0 AND AccessPoints > 0 AND TrainingItemCourseID ='{1}' AND AccessPointsMode = '{2}'
                ";
            string sql = string.Format(sqlModal, accessPointsUser, trainingItemCourseID, accessPointsMode);
            return SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.Text, sql);
        }





    }
}
