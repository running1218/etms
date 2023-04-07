using ETMS.Components.Basic.API.Entity.Operation;
using ETMS.Utility;
using ETMS.Utility.Data;
using System;
using System.Data;
using System.Data.SqlClient;

namespace ETMS.Components.Basic.Implement.DAL.Operation
{
    public  class BannerSpreadDataAccess
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="bannerSpread">业务实体</param>
		public void Insert(BannerSpread bannerSpread)
        {
            string commandName = "[dbo].[Pr_Banner_Spread_Insert]";
            SqlParameter[] parameters = new SqlParameter[]
            {
                SqlHelper.CreateInputSqlParameter("@BannerSpreadID", SqlDbType.UniqueIdentifier, bannerSpread.BannerSpreadID),
                SqlHelper.CreateInputSqlParameter("@SpreadName", SqlDbType.NVarChar, bannerSpread.SpreadName),
                SqlHelper.CreateInputSqlParameter("@SpreadPCLink", SqlDbType.NVarChar, bannerSpread.SpreadPCLink, true),
                SqlHelper.CreateInputSqlParameter("@SpreadMobileLink", SqlDbType.NVarChar, bannerSpread.SpreadMobileLink, true),
                SqlHelper.CreateInputSqlParameter("@KeyWords", SqlDbType.NVarChar, bannerSpread.KeyWords, true),
                SqlHelper.CreateInputSqlParameter("@PCImageName", SqlDbType.NVarChar, bannerSpread.PCImageName, true),
                SqlHelper.CreateInputSqlParameter("@PCImagePath", SqlDbType.NVarChar, bannerSpread.PCImagePath, true),
                SqlHelper.CreateInputSqlParameter("@MobileImageName", SqlDbType.NVarChar, bannerSpread.MobileImageName, true),
                SqlHelper.CreateInputSqlParameter("@MobileImagePath", SqlDbType.NVarChar, bannerSpread.MobileImagePath, true),
                SqlHelper.CreateInputSqlParameter("@ReleaseStatus", SqlDbType.SmallInt, bannerSpread.ReleaseStatus, true),
                SqlHelper.CreateInputSqlParameter("@Order", SqlDbType.Int, bannerSpread.Order, true),
                SqlHelper.CreateInputSqlParameter("@Creator", SqlDbType.Int, bannerSpread.Creator, true),
                SqlHelper.CreateInputSqlParameter("@CreateTime", SqlDbType.DateTime, bannerSpread.CreateTime, true),
                SqlHelper.CreateInputSqlParameter("@Modifier", SqlDbType.Int, bannerSpread.Modifier, true),
                SqlHelper.CreateInputSqlParameter("@ModifyTime", SqlDbType.DateTime, bannerSpread.ModifyTime, true),
                SqlHelper.CreateInputSqlParameter("@OrgID", SqlDbType.Int, bannerSpread.OrgID, true)
            };
            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parameters);
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="bannerSpread">业务实体</param>
		public void Update(BannerSpread bannerSpread)
        {
            string commandName = "[dbo].[Pr_Banner_Spread_Update]";
            SqlParameter[] parameters = new SqlParameter[]
            {
                SqlHelper.CreateInputSqlParameter("@BannerSpreadID", SqlDbType.UniqueIdentifier, bannerSpread.BannerSpreadID),
                SqlHelper.CreateInputSqlParameter("@SpreadName", SqlDbType.NVarChar, bannerSpread.SpreadName),
                SqlHelper.CreateInputSqlParameter("@SpreadPCLink", SqlDbType.NVarChar, bannerSpread.SpreadPCLink, true),
                SqlHelper.CreateInputSqlParameter("@SpreadMobileLink", SqlDbType.NVarChar, bannerSpread.SpreadMobileLink, true),
                SqlHelper.CreateInputSqlParameter("@KeyWords", SqlDbType.NVarChar, bannerSpread.KeyWords, true),
                SqlHelper.CreateInputSqlParameter("@PCImageName", SqlDbType.NVarChar, bannerSpread.PCImageName, true),
                SqlHelper.CreateInputSqlParameter("@PCImagePath", SqlDbType.NVarChar, bannerSpread.PCImagePath, true),
                SqlHelper.CreateInputSqlParameter("@MobileImageName", SqlDbType.NVarChar, bannerSpread.MobileImageName, true),
                SqlHelper.CreateInputSqlParameter("@MobileImagePath", SqlDbType.NVarChar, bannerSpread.MobileImagePath, true),              
                SqlHelper.CreateInputSqlParameter("@ReleaseStatus", SqlDbType.SmallInt, bannerSpread.ReleaseStatus, true),
                SqlHelper.CreateInputSqlParameter("@Order", SqlDbType.Int, bannerSpread.Order, true),
                SqlHelper.CreateInputSqlParameter("@Creator", SqlDbType.Int, bannerSpread.Creator, true),
                SqlHelper.CreateInputSqlParameter("@CreateTime", SqlDbType.DateTime, bannerSpread.CreateTime, true),
                SqlHelper.CreateInputSqlParameter("@Modifier", SqlDbType.Int, bannerSpread.Modifier, true),
                SqlHelper.CreateInputSqlParameter("@ModifyTime", SqlDbType.DateTime, bannerSpread.ModifyTime, true)
            };
            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parameters);
        }

        /// <summary>
        /// 获取顺序的最大值
        /// </summary>
        /// <returns></returns>
        public int GetMaxOrderValue()
        {
            int orderValue = 0;
            string commandName = "[dbo].[Pr_Banner_Spread_GetMaxOrder]";
            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName).Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                orderValue = dt.Rows[0][0].ToInt();
            }
            return orderValue;
        }
        /// <summary>
        /// 根据主键删除业务实体
        /// </summary>
        /// <param name="bannerSpreadID">Banner推广ID</param>
		public void Remove(Guid bannerSpreadID)
        {
            string commandName = "[dbo].[Pr_Banner_Spread_Delete]";
            SqlParameter[] parameters = new SqlParameter[]
            {
                SqlHelper.CreateInputSqlParameter("@BannerSpreadID", SqlDbType.UniqueIdentifier, bannerSpreadID)
            };

            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parameters);
        }

        /// <summary>
        /// 根据参数获取对象列表（分页，可排序）
        /// </summary>
        public DataTable GetPageList(string SpreadName, string PublishStatus, int orgID)
        {
            string commandName = "dbo.Pr_BannerSpread_GetPagedList";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@SpreadName", SqlDbType.NVarChar),
                    new SqlParameter("@PublishStatus", SqlDbType.NVarChar),
                    new SqlParameter("@OrgID", SqlDbType.Int)         
                };
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = SpreadName;
            parms[1].Value = PublishStatus;
            parms[2].Value = orgID;
            #endregion
            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];
            return dt;
        }

        /// <summary>
        /// 根据Banner推广ID修改Banner排序号
        /// </summary>
        /// <param name="bannerSpreadID">Banner推广ID</param>
        /// <param name="orderNum">排序号</param>
        public void UpdateOrderNumByBannerSpreadID(Guid bannerSpreadID, int orderNum)
        {

            string commandName = "dbo.Pr_BannerSpread_UpdateOrderNum";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@bannerSpreadID", SqlDbType.UniqueIdentifier),
                    new SqlParameter("@OrderNum",SqlDbType.Int)
                };
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
            }

            parms[0].Value = bannerSpreadID;
            parms[1].Value = orderNum;

            #endregion
            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
        }

        /// <summary>
        /// 根据主键ID查询数据
        /// </summary>
        /// <param name="BannerSpreadID">推广ID</param>
        public DataTable GetByID(Guid BannerSpreadID)
        {
            string commandName = "dbo.Pr_BannerSpread_GetByID";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@BannerSpreadID", SqlDbType.UniqueIdentifier)
                  
                };
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }
            parms[0].Value = BannerSpreadID;          
            #endregion
            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];
            return dt;
        }

        /// <summary>
        ///查询首页Banner信息
        /// </summary>
        public DataTable GetBannerList(int orgID)
        {
            string commandName = "dbo.Pr_BannerSpread_GetBannerList";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@OrgID", SqlDbType.Int)

                };
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }
            parms[0].Value = orgID;

            #endregion
            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];
            return dt;
        }

    }
}
