﻿//==================================================================================================
//Version 1.0, auto-generated.
//Generated By xueyb.
//Date: 2012-3-29 22:16:00.
//==================================================================================================
//==================================================================================================
//This file is generated by CodeGenerator(Ver 2.1), a product of ZhouYonghua(Zhou_Yonghua@163.com).
//==================================================================================================

using System;
using System.Data;

using System.Data.SqlClient;
using ETMS.Utility.Data;

namespace ETMS.Components.Basic.Implement.DAL.ELearningMap
{
    /// <summary>
    /// 学习地图与课程关系表数据访问
    /// </summary>
    public partial class Res_StudyMapReferCourseDataAccess
    {
        /// <summary>
        /// 根据培训项目课程获取导学资料
        /// </summary>
        /// <param name="trainingItemCourseID">培训项目课程编号</param>
        /// <returns>返回培训项目课程编号下导学资料列表</returns>
        public DataTable GetCourseListByStudyMapID(Guid StudyMapID, int pageIndex, int pageSize, string sortExpression, out int totalRecords)
        {
            string commandName = "[dbo].[Pr_Res_StudyMapReferCourse_GetCourseListByStudyMapID]";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@StudyMapID", SqlDbType.UniqueIdentifier),
                    new SqlParameter("@PageIndex", SqlDbType.Int),
					new SqlParameter("@PageSize", SqlDbType.Int),
					new SqlParameter("@SortExpression", SqlDbType.VarChar),
					new SqlParameter("@ReturnValue", SqlDbType.Int, 4, 
				ParameterDirection.ReturnValue, false, 0, 0, String.Empty, DataRowVersion.Default, null)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = StudyMapID;
            parms[1].Value = pageIndex;
            parms[2].Value = pageSize;
            parms[3].Value = sortExpression;
            
            #endregion

            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];
            totalRecords = (int)parms[4].Value;

            return dt;
        }

        /// <summary>
        /// 批量建立学习地图和课程的关系
        /// </summary>
        /// <param name="CourseIDBatch"></param>
        /// <param name="StudyMapID"></param>
        /// <param name="UserID"></param>
        /// <param name="UserName"></param>
        public void StudyMapReferCourseInsertBatch( string CourseIDBatch,Guid StudyMapID, int UserID, string UserName)
        {
            string commandName = "dbo.Pr_Res_StudyMapReferCourse_Insert_Batch";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@CourseIDBatch", SqlDbType.VarChar),
					new SqlParameter("@StudyMapID", SqlDbType.UniqueIdentifier),
					new SqlParameter("@UserID", SqlDbType.Int),
					new SqlParameter("@UserName", SqlDbType.VarChar)
					};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
            }

            parms[0].Value = CourseIDBatch;
            parms[1].Value = StudyMapID;
            parms[2].Value = UserID;
            parms[3].Value = UserName;
            
          
            #endregion
            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
        }

        /// <summary>
        /// 学习地图课程学习情况
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public DataTable GetStudyMapCourseStatus(int userID)
        {
            string commandName = "[dbo].[Pr_Res_StudyMapReferCourse_GetStudyMapCourseStatus]";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@UserID", SqlDbType.Int)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = userID;

            #endregion

            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];
            return dt;
        }
    }
}
