using System;
using ETMS.Utility.Data;
using System.Data.SqlClient;
using System.Data;

namespace ETMS.Components.Courseware.Implement.DAL
{
    public partial class Res_CoursewareDataAccess
    {
        /// <summary>
        /// 验证某个学习资源是否被培训项目的课程引用
        /// </summary>
        /// <param name="resID">学习资源ID</param>
        /// <returns></returns>
        public bool CheckResourceIsUsed(Guid resID)
        {
            bool isUsed = true;
            string sqlModal = @"select COUNT(*)  num from Tr_ItemCourseRes a where a.CourseResID ='{0}' and a.CourseResTypeID='{1}'";
            string sql = string.Format(sqlModal, resID, (int)Basic.API.Entity.EnumResourcesType.Courseware);
            int ret = (Int32)SqlHelper.ExecuteScalar(ConnectionString.ETMSRead, CommandType.Text, sql, null);
            if (ret < 1)
                isUsed = false;
            return isUsed;

        }


        /// <summary>
        /// 根据参数获取对象列表（分页，可排序）
        /// </summary>
        public DataTable Res_CoursewareGetPagedList(int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {
            string commandName = "[dbo].[Pr_Tr_VW_Res_Courseware_GetPagedList]";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@PageIndex", SqlDbType.Int),
					new SqlParameter("@PageSize", SqlDbType.Int),
					new SqlParameter("@SortExpression", SqlDbType.NVarChar),
					new SqlParameter("@Criteria", SqlDbType.NVarChar),
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
        /// 根据课程编号获取课件的可用总数（就是状态为“启用”）
        /// </summary>
        /// <param name="courseID">课程编号</param>
        /// <returns></returns>
        public Int32 GetCourseWareTotal(Guid courseID)
        {
            string commandName = string.Format("select COUNT(*) from Res_CourseRes where IsUse='1' and  CourseID='{0}' and CourseResTypeID='{1}'", courseID, (Int32)Basic.API.Entity.EnumResourcesType.Courseware);
            return (Int32)SqlHelper.ExecuteScalar(ConnectionString.ETMSRead, CommandType.Text, commandName, null);
        }


        /// <summary>
        /// 根据课程编号获取课件的总数
        /// </summary>
        /// <param name="courseID">课程编号</param>
        /// <returns></returns>
        public Int32 GetALLCourseWareTotal(Guid courseID)
        {
            string commandName = string.Format("select COUNT(*) from Res_CourseRes where CourseID='{0}' and CourseResTypeID='{1}'", courseID, (Int32)Basic.API.Entity.EnumResourcesType.Courseware);
            return (Int32)SqlHelper.ExecuteScalar(ConnectionString.ETMSRead, CommandType.Text, commandName, null);
        }

        /// <summary>
        /// 更新资源状态
        /// </summary>
        public void UpdateResourceStatus(Guid coursewareID, int resourceStatus, string resourcePath)
        {
            string commandName = @"Update Res_Courseware 
                                    Set ResourceStatus = @ResourceStatus
                                        , ResourcePath = @ResourcePath 
                                    Where CoursewareID = @CoursewareID";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@CoursewareID", SqlDbType.UniqueIdentifier), 
                    new SqlParameter("@ResourceStatus", SqlDbType.Int),
                    new SqlParameter("@ResourcePath", SqlDbType.NVarChar, 200)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
            }

            parms[0].Value = coursewareID;
            parms[1].Value = resourceStatus;
            parms[2].Value = resourcePath;

            #endregion
            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.Text, commandName, parms);
        }

        /// <summary>
        /// 更新资源状态
        /// </summary>
        public void UpdateResourceStatus(Guid coursewareID, int resourceStatus)
        {
            string commandName = @"Update Res_Courseware 
                                    Set ResourceStatus = @ResourceStatus
                                    Where CoursewareID = @CoursewareID";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@CoursewareID", SqlDbType.UniqueIdentifier), 
                    new SqlParameter("@ResourceStatus", SqlDbType.Int)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
            }

            parms[0].Value = coursewareID;
            parms[1].Value = resourceStatus;

            #endregion
            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.Text, commandName, parms);
        }
    }
}
