using System;
using System.Data;

using System.Data.SqlClient;
using ETMS.Utility.Data;
using ETMS.Components.Basic.API.Entity.Course;

namespace ETMS.Components.Basic.Implement.DAL.Course
{
    public partial class Res_ContentDataAccess
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="resContent">业务实体</param>
		public void Add(ResContentMore resContent)
        {
            string commandName = "dbo.Pr_Res_Content_Insert";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@ContentID", SqlDbType.UniqueIdentifier),
                    new SqlParameter("@CoursewareID", SqlDbType.UniqueIdentifier),
                    new SqlParameter("@Name", SqlDbType.NVarChar),
                    new SqlParameter("@Type", SqlDbType.Int),
                    new SqlParameter("@DataInfo", SqlDbType.NVarChar),
                    new SqlParameter("@Sort", SqlDbType.Int),
                    new SqlParameter("@Status", SqlDbType.Int),
                    new SqlParameter("@TeacherName", SqlDbType.NVarChar),
                    new SqlParameter("@PlayTime", SqlDbType.Int),
                    new SqlParameter("@Remark", SqlDbType.NVarChar),
                    new SqlParameter("@IsOpen", SqlDbType.Bit),
                    new SqlParameter("@CreateTime", SqlDbType.DateTime),
                    new SqlParameter("@ModifyTime", SqlDbType.DateTime)
                    //new SqlParameter("@PlayTime", SqlDbType.Int)
                };
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
            }
            parms[0].Value = resContent.ContentID;
            parms[1].Value = resContent.CoursewareID;
            parms[2].Value = resContent.Name;
            parms[3].Value = resContent.Type;
            parms[4].Value = resContent.DataInfo;
            parms[5].Value = resContent.Sort;
            parms[6].Value = resContent.Status;
            parms[7].Value = resContent.TeacherName;
            parms[8].Value = resContent.PlayTime;
            parms[9].Value = resContent.Remark;
            parms[10].Value = resContent.IsOpen;
            parms[11].Value = resContent.CreateTime;
            parms[12].Value = resContent.ModifyTime;
            //parms[13].Value = resContent.PlayTime;
            #endregion
            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);

        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="resContent">业务实体</param>
        public void Update(ResContentMore resContent)
        {
            string commandName = "[dbo].[Pr_Res_Content_Update]";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@ContentID", SqlDbType.UniqueIdentifier),
                    new SqlParameter("@CoursewareID", SqlDbType.UniqueIdentifier),
                    new SqlParameter("@Name", SqlDbType.NVarChar),
                    new SqlParameter("@Type", SqlDbType.Int),
                    new SqlParameter("@DataInfo", SqlDbType.NVarChar),
                    new SqlParameter("@Status", SqlDbType.Int),
                    new SqlParameter("@TeacherName", SqlDbType.NVarChar),
                    new SqlParameter("@ModifyTime", SqlDbType.DateTime),
                    new SqlParameter("@IsOpen", SqlDbType.Bit),
                    new SqlParameter("@CatalogID", SqlDbType.UniqueIdentifier),
                    new SqlParameter("@PlayTime", SqlDbType.Int)
                };
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
            }
            parms[0].Value = resContent.ContentID;
            parms[1].Value = resContent.CoursewareID;
            parms[2].Value = resContent.Name;
            parms[3].Value = resContent.Type;
            parms[4].Value = resContent.DataInfo;
            parms[5].Value = resContent.Status;
            parms[6].Value = resContent.TeacherName;
            parms[7].Value = resContent.ModifyTime;
            parms[8].Value = resContent.IsOpen;
            parms[9].Value = resContent.CatalogID;
            parms[10].Value = resContent.PlayTime;
            #endregion
            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
        }

        public void RemoveCourseOpenResource(int orgID, Guid courseID)
        {
            string commandSql = @"DELETE FROM Recommend_CourseOpenResource WHERE OrgID = @OrgID AND CourseID = @CourseID";
            #region Parameters
            SqlParameter[] parms = new SqlParameter[] {
                    new SqlParameter("@OrgID", SqlDbType.Int),
                    new SqlParameter("@CourseID", SqlDbType.UniqueIdentifier)
                };
            parms[0].Value = orgID;
            parms[1].Value = courseID;
            #endregion
            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.Text, commandSql, parms);
        }
        /// <summary>
        /// 机构开放课程资源
        /// </summary>
        /// <param name="orgID"></param>
        /// <param name="courseID"></param>
        /// <param name="resourceID"></param>
        /// <param name="userID"></param>
        public void SetCourseOpenResource(int orgID, Guid courseID, Guid resourceID, int userID)
        {
            string commandSql = @"INSERT INTO Recommend_CourseOpenResource(CourseID,OrgID,ResourceID,CreateTime,CreatorID)
                                    VALUES  (@CourseID,@OrgID,@ResourceID,GETDATE(),@CreatorID)";
            #region Parameters
            SqlParameter[] parms = new SqlParameter[] {
                    new SqlParameter("@OrgID", SqlDbType.Int),
                    new SqlParameter("@CourseID", SqlDbType.UniqueIdentifier),
                    new SqlParameter("@ResourceID", SqlDbType.UniqueIdentifier),
                    new SqlParameter("@CreatorID", SqlDbType.Int)
                };
            parms[0].Value = orgID;
            parms[1].Value = courseID;
            parms[2].Value = resourceID;
            parms[3].Value = userID;
            #endregion
            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.Text, commandSql, parms);
        }
        public void SetCourseOpenLiving(int orgID, Guid courseID, string livingID, int userID)
        {
            string commandSql = @"INSERT INTO Recommend_CourseOpenResource(CourseID,OrgID,LivingID,CreateTime,CreatorID)
                                    VALUES  (@CourseID,@OrgID,@LivingID,GETDATE(),@CreatorID)";
            #region Parameters
            SqlParameter[] parms = new SqlParameter[] {
                    new SqlParameter("@OrgID", SqlDbType.Int),
                    new SqlParameter("@CourseID", SqlDbType.UniqueIdentifier),
                    new SqlParameter("@LivingID", SqlDbType.NVarChar),
                    new SqlParameter("@CreatorID", SqlDbType.Int)
                };
            parms[0].Value = orgID;
            parms[1].Value = courseID;
            parms[2].Value = livingID;
            parms[3].Value = userID;
            #endregion
            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.Text, commandSql, parms);
        }
        /// <summary>
        /// 获取机构开放资源列表
        /// </summary>
        /// <param name="orgID"></param>
        /// <param name="courseID"></param>
        /// <returns></returns>
        public DataTable GetCourseOpenResource(int orgID, Guid courseID)
        {
            string commandSql = @"SELECT ID, CourseID, OrgID, ResourceID, LivingID, CreateTime, CreatorID
                                    FROM dbo.Recommend_CourseOpenResource
                                    WHERE OrgID = @OrgID AND CourseID = @CourseID";
            #region Parameters
            SqlParameter[] parms = new SqlParameter[] {
                    new SqlParameter("@CourseID", SqlDbType.UniqueIdentifier),
                    new SqlParameter("@OrgID", SqlDbType.Int),
                };
                
            parms[0].Value = courseID;
            parms[1].Value = orgID;
            #endregion
            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.Text, commandSql, parms).Tables[0];
            return dt;
        }
        /// <summary>
        /// 根据主键删除业务实体
        /// </summary>
        /// <param name="contentID">资源编号</param>
        public void Remove(Guid contentID)
        {
            string commandName = "dbo.Pr_Res_Content_Delete";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@ContentID", SqlDbType.UniqueIdentifier)
                };
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
            }

            parms[0].Value = contentID;

            #endregion
            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
        }

        /// <summary>
        /// 根据主键获取业务表
        /// </summary>
        /// <param name="contentID">资源编号</param>
		public DataTable GetByID(Guid contentID)
        {
            string commandName = "[dbo].[Pr_Res_Content_GetByPk]";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@ContentID", SqlDbType.UniqueIdentifier)
                };
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }
            parms[0].Value = contentID;
            #endregion
            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];
            return dt;
        }
        /// <summary>
        /// 资源基本信息
        /// </summary>
        /// <param name="contentID">资源ID</param>
        /// <param name="type">资源类型</param>
        /// <param name="streamcode">视频码流</param>
        /// <returns></returns>
        public DataTable GetContentDataInfo(Guid contentID, int type, string streamcode)
        {
            string commandName = "[dbo].[Pr_GetResContentDataInfo]";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@ContentID", SqlDbType.UniqueIdentifier),
                    new SqlParameter("@Type", SqlDbType.Int),
                    new SqlParameter("@StreamCode", SqlDbType.NVarChar)
                };
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }
            parms[0].Value = contentID;
            parms[1].Value = type;
            parms[2].Value = streamcode;
            #endregion
            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];
            return dt;
        }
        
        ///// <summary>
        ///// 分页查询业务表
        ///// </summary>
        /// <param name="pageIndex">页号</param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="CourseWareID">课程ID</param>
        /// <param name="totalRecords">out 记录总数</param>
        /// <returns></returns>
        public DataTable GetPagedList(int pageIndex, int pageSize, string sortExpression, string criteria, Guid CourseID, out int totalRecords)
        {
            totalRecords = 0;
            string commandName = "dbo.Pr_Res_Content_GetPagedList";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@PageIndex", SqlDbType.Int),
                    new SqlParameter("@PageSize", SqlDbType.Int),
                    new SqlParameter("@SortExpression", SqlDbType.NVarChar),
                    new SqlParameter("@Criteria", SqlDbType.NVarChar),
                    new SqlParameter("@CourseID", SqlDbType.UniqueIdentifier),
                    new SqlParameter("@ReturnValue", SqlDbType.Int, 4, ParameterDirection.ReturnValue, false, 0, 0, String.Empty, DataRowVersion.Default, null)
                   

                };
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = pageIndex;
            parms[1].Value = pageSize;
            parms[2].Value = sortExpression;
            parms[3].Value = criteria;
            parms[4].Value = CourseID;
            #endregion
            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];
            totalRecords = (int)parms[5].Value;
            return dt;
        }
        /// <summary>
        /// 查询业务表
        /// </summary>
        /// <param name="CourseWareID"></param>
        /// <returns></returns>
        public DataTable GetCourseContentList(Guid CourseWareID)
        {
            string commandName = "dbo.Pr_Res_Content_GetContentList";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@CoursewareID", SqlDbType.UniqueIdentifier)
                };
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = CourseWareID;
            #endregion
            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];
            return dt;
        }
        /// <summary>
        /// 查询课件下资源总数
        /// </summary>
        /// <param name="CoursewareID">课件ID</param>
        /// <returns></returns>
        public int GetContentCount(Guid CoursewareID)
        {
            string commandName = "[dbo].[Pr_Res_Content_GetContentCount]";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@CoursewareID", SqlDbType.UniqueIdentifier)
                };
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }
            parms[0].Value = CoursewareID;
            #endregion
            int count = Convert.ToInt32(SqlHelper.ExecuteScalar(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms));
            return count;
        }
        /// <summary>
        /// 根据课程资源ID修改排序号
        /// </summary>
        /// <param name="CourseContentID">课程资源ID</param>
        /// <param name="orderNum">排序号</param>
        public void UpdateOrderNumByCourseContentID(Guid CourseContentID, int orderNum)
        {
            string commandName = "dbo.Pr_Res_Content_UpdateOrderNum";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@ContentID", SqlDbType.UniqueIdentifier),
                    new SqlParameter("@OrderNum",SqlDbType.Int)
                };
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
            }

            parms[0].Value = CourseContentID;
            parms[1].Value = orderNum;

            #endregion
            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
        }
        /// <summary>
        /// 查询课程下资源总数
        /// </summary>
        /// <param name="CourseID">课程ID</param>
        /// <returns></returns>
        public int GetContentCountByCourseID(Guid CourseID)
        {
            string commandName = "[dbo].[Pr_Res_Content_GetContentCountByCourseID]";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@CourseID", SqlDbType.UniqueIdentifier)
                };
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }
            parms[0].Value = CourseID;
            #endregion
            int count = Convert.ToInt32(SqlHelper.ExecuteScalar(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms));
            return count;
        }
    }
}
