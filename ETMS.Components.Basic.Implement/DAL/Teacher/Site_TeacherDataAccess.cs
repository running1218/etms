using System;
using System.Data;

using System.Data.SqlClient;
using ETMS.Utility.Data;
namespace ETMS.Components.Basic.Implement.DAL.Teacher
{
    public partial class Site_TeacherDataAccess
    {


        /// <summary>
        ///  批量添加内部讲师
        /// </summary>
        /// <param name="teacherIDList">内部讲师编号列表</param>
        public void SetInnerTeacher(string teacherIDList, int roleID, string creator)
        {
            string commandName = "[dbo].[Pr_Site_Teacher_SetInnerTeacher]";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@TeacherIDList", SqlDbType.NVarChar),
                    new SqlParameter("@RoleID", SqlDbType.Int),
                    new SqlParameter("@Creator", SqlDbType.NVarChar)
					};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
            }

            parms[0].Value = teacherIDList;
            parms[1].Value = roleID;
            parms[2].Value = creator;
            #endregion
            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
        }

        /// <summary>
        /// 获取内部员工设为讲师列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortExpression"></param>
        /// <param name="criteria"></param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        public DataTable GetInnerChoseTeacherList(int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
		{
            string commandName = "dbo.Pr_Site_User_ChoseTeacher";
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
			DataTable dt=SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];
			totalRecords = (int)parms[4].Value;
			return dt;
		}

        /// <summary>
        /// 讲师综合查询
        /// </summary>
        /// <param name="teacherStatus"></param>
        /// <param name="teacherName"></param>
        /// <param name="teachCourse"></param>
        /// <param name="itemBeginDate"></param>
        /// <param name="itemEndDate"></param>
        /// <param name="organizationID"></param>
        /// <returns></returns>
        public DataTable GetTeacherMuiltyInfoList(int teacherStatus, string teacherName, string teachCourse, DateTime itemCourseBeginDate, DateTime itemCourseEndDate, int organizationID)
        {
            string commandName = "dbo.Pr_Site_Teacher_GetTeacherTeachCourseQuery";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@TeacherStatus", SqlDbType.Int),
					new SqlParameter("@TeacherName", SqlDbType.NVarChar),
					new SqlParameter("@TeachCourse", SqlDbType.NVarChar),
					new SqlParameter("@ItemCourseBeginDate", SqlDbType.DateTime),
					new SqlParameter("@ItemCourseEndDate", SqlDbType.DateTime),
                    new SqlParameter("@OrganizationID", SqlDbType.Int)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = teacherStatus;
            parms[1].Value = teacherName;
            parms[2].Value = teachCourse;

            if (itemCourseBeginDate == default(DateTime))
                parms[3].Value = DBNull.Value;
            else
                parms[3].Value = itemCourseBeginDate;

            if (itemCourseEndDate == default(DateTime))
                parms[4].Value = DBNull.Value;
            else
                parms[4].Value = itemCourseEndDate;
            parms[5].Value = organizationID;
            #endregion
            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];
            return dt;
        }

        /// <summary>
        /// 讲师综合查询-培训项目
        /// </summary>
        /// <param name="teacherID"></param>
        /// <param name="itemBeginDate"></param>
        /// <param name="itemEndDate"></param>
        /// <returns></returns>
        public DataTable GetTeacherTraningItemList(int teacherID, DateTime itemCourseBeginDate, DateTime itemCourseEndDate)
        {
            string commandName = "dbo.Pr_Site_Teacher_GetTeachItemList";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@TeacherID", SqlDbType.Int),
					new SqlParameter("@ItemCourseBeginDate", SqlDbType.DateTime),
					new SqlParameter("@ItemCourseEndDate", SqlDbType.DateTime),
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = teacherID;

            if (itemCourseBeginDate == default(DateTime))
                parms[1].Value = DBNull.Value;
            else
                parms[1].Value = itemCourseBeginDate;

            if (itemCourseEndDate == default(DateTime))
                parms[2].Value = DBNull.Value;
            else
                parms[2].Value = itemCourseEndDate;

            #endregion
            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];
            return dt;
        }

        /// <summary>
        /// 讲师综合查询-培训项目课程列表
        /// </summary>
        /// <param name="teacherID"></param>
        /// <param name="itemBeginDate"></param>
        /// <param name="itemEndDate"></param>
        /// <returns></returns>
        public DataTable GetTraniningItemCourseList(int teacherID, DateTime itemCourseBeginDate, DateTime itemCourseEndDate)
        {
            string commandName = "dbo.Pr_Site_Teacher_GetTraniningItemCourseList";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@TeacherID", SqlDbType.Int),
					new SqlParameter("@ItemCourseBeginDate", SqlDbType.DateTime),
					new SqlParameter("@ItemCourseEndDate", SqlDbType.DateTime),
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = teacherID;

            if (itemCourseBeginDate == default(DateTime))
                parms[1].Value = DBNull.Value;
            else
                parms[1].Value = itemCourseBeginDate;

            if (itemCourseEndDate == default(DateTime))
                parms[2].Value = DBNull.Value;
            else
                parms[2].Value = itemCourseEndDate;

            #endregion
            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];
            return dt;
        }

        /// <summary>
        /// 讲师综合查询-培训项目课程课时列表
        /// </summary>
        /// <param name="teacherID"></param>
        /// <param name="itemBeginDate"></param>
        /// <param name="itemEndDate"></param>
        /// <returns></returns>
        public DataTable GetTraniningItemCourseHoursList(int teacherID, DateTime itemCourseBeginDate, DateTime itemCourseEndDate)
        {
            string commandName = "dbo.Pr_Site_Teacher_GetTraniningItemCourseHoursList";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@TeacherID", SqlDbType.Int),
					new SqlParameter("@ItemCourseBeginDate", SqlDbType.DateTime),
					new SqlParameter("@ItemCourseEndDate", SqlDbType.DateTime),
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = teacherID;

            if (itemCourseBeginDate == default(DateTime))
                parms[1].Value = DBNull.Value;
            else
                parms[1].Value = itemCourseBeginDate;

            if (itemCourseEndDate == default(DateTime))
                parms[2].Value = DBNull.Value;
            else
                parms[2].Value = itemCourseEndDate;

            #endregion
            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];
            return dt;
        }

        /// <summary>
        /// 获取内部讲师列表
        /// </summary>
        /// <param name="organizationID">组织机构编号</param>
        /// <param name="workerNo">工号</param>
        /// <param name="teacherName">教师姓名</param>
        /// <param name="teacherTypeID">教师类型</param>
        /// <param name="teacherLevelID">教师级别</param>
        /// <returns></returns>
        public DataTable GetInnerTeacherList(int organizationID, string workerNo, string teacherName, int teacherTypeID, int teacherLevelID, int isUse, int isCourseTeacher)
        {
            string commandName = "[dbo].[Pr_Site_Teacher_GetInnerTeacherList]";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@OrganizationID", SqlDbType.Int),
					new SqlParameter("@WorkerNo", SqlDbType.VarChar,20),
                    new SqlParameter("@TeacherName", SqlDbType.VarChar,50),
                    new SqlParameter("@TeacherTypeID", SqlDbType.Int),
                    new SqlParameter("@TeacherLevelID", SqlDbType.Int),
                    new SqlParameter("@IsUse", SqlDbType.Int),
                    new SqlParameter("@IsCourseDesigner", SqlDbType.Int),
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = organizationID;
            parms[1].Value = workerNo;
            parms[2].Value = teacherName;
            parms[3].Value = teacherTypeID;
            parms[4].Value = teacherLevelID;
            parms[5].Value = isUse;
            parms[6].Value = isCourseTeacher;
            #endregion
            return SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];
        }

        
        /// <summary>
        /// 获取讲师列表
        /// </summary>
        /// <param name="organizationID">组织机构编号</param>
        /// <param name="outerOrgID">外部组织机构编号</param>
        /// <param name="isUse">教师状态：1启用 0 停用</param>
        /// <param name="teacherLevelID">教师级别</param>
        /// <param name="RealName">教师姓名</param>
        /// <param name="teacherSourceID">教师来源1：内部2：外聘</param>
        /// <returns></returns>
        public DataTable GetOuterTeacherList(int organizationID, Guid outerOrgID, int isUse, int teacherLevelID, string RealName, int IsCollaborate)
        {
            string commandName = "[dbo].[Pr_Site_Teacher_GetOuterTeacherList]";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@OrganizationID", SqlDbType.Int),
					new SqlParameter("@OuterOrgID", SqlDbType.UniqueIdentifier),
                    new SqlParameter("@IsUse", SqlDbType.Int),
                    new SqlParameter("@TeacherLevelID", SqlDbType.Int),
                    new SqlParameter("@RealName", SqlDbType.VarChar,50),
                    new SqlParameter("@IsCollaborate", SqlDbType.Int),
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = organizationID;
            parms[1].Value = outerOrgID;
            parms[2].Value = isUse;
            parms[3].Value = teacherLevelID;
            parms[4].Value = RealName;
            parms[5].Value = IsCollaborate;
            #endregion
            return SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];
        }

        /// <summary>
        /// 获取有效状态的讲师
        /// </summary>
        /// <param name="organizationID"></param>
        /// <returns></returns>
        public DataTable GetTeachersByOrganization(int organizationID)
        {
            string commandName = "[Pr_Site_GetTeacherByOrganization]";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@OrganizationID", SqlDbType.Int),
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = organizationID;
            #endregion
            return SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];
        }       

        /// <summary>
        /// 获取所有的讲师（包括讲师停用，用户停用）
        /// </summary>
        /// <param name="organizationID"></param>
        /// <returns></returns>
        public DataTable GetAllTeachersByOrganization(int organizationID)
        {
            string commandName = "[Pr_Site_GetAllTeacherByOrganization]";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@OrganizationID", SqlDbType.Int),
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = organizationID;
            #endregion
            return SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];
        }


        /// <summary>
        /// 获取所有讲师列表
        /// from Site_Teacher a
        /// inner join Site_User b on b.UserID=a.TeacherID
        /// </summary>
        public DataTable GetTeacherList(int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {
            string commandName = "dbo.[Pr_Site_Teacher_GetTeacherList]";
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
