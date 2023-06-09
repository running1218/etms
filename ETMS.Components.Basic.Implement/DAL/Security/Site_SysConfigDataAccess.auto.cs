//==================================================================================================
//Version 1.0, auto-generated.
//Generated By running1218
//Date: 2012-4-10 10:24:30.
//==================================================================================================
//==================================================================================================
//This file is generated by CodeGenerator(Ver 2.1)
//==================================================================================================

using System;
using System.Collections.Generic;
using System.Data;

using System.Data.SqlClient;
using ETMS.Utility.Data;

using ETMS.Components.Basic.API.Entity.Security;

namespace ETMS.Components.Basic.Implement.DAL.Security
{
    /// <summary>
    /// 系统配置数据访问
    /// </summary>
    public partial class Site_SysConfigDataAccess
    {
        /// <summary>
        /// 增加
        /// </summary>
        public void Add(Site_SysConfig site_SysConfig)
        {
            string commandName = "dbo.Pr_Site_SysConfig_Insert";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {	new SqlParameter("@ConfigID", SqlDbType.Int, 4, ParameterDirection.Output, false, 0, 0, String.Empty, DataRowVersion.Default, null),
				
					new SqlParameter("@OrganizationID", SqlDbType.Int),
					new SqlParameter("@Name", SqlDbType.VarChar, 50),
					new SqlParameter("@ConfigGroupID", SqlDbType.Int),
					new SqlParameter("@DisplayName", SqlDbType.VarChar, 100),
					new SqlParameter("@DefaultValue", SqlDbType.VarChar, 1000),
					new SqlParameter("@OrderNo", SqlDbType.Int),
					new SqlParameter("@UserValue", SqlDbType.VarChar, 1000),
					new SqlParameter("@Modifier", SqlDbType.VarChar, 50),
					new SqlParameter("@ModifyTime", SqlDbType.DateTime),
					new SqlParameter("@Description", SqlDbType.VarChar, 500)
					};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
            }

            parms[1].Value = site_SysConfig.OrganizationID;
            if (site_SysConfig.Name != null) { parms[2].Value = site_SysConfig.Name; } else { parms[2].Value = DBNull.Value; }
            parms[3].Value = site_SysConfig.ConfigGroupID;
            if (site_SysConfig.DisplayName != null) { parms[4].Value = site_SysConfig.DisplayName; } else { parms[4].Value = DBNull.Value; }
            if (site_SysConfig.DefaultValue != null) { parms[5].Value = site_SysConfig.DefaultValue; } else { parms[5].Value = DBNull.Value; }
            parms[6].Value = site_SysConfig.OrderNo;
            if (site_SysConfig.UserValue != null) { parms[7].Value = site_SysConfig.UserValue; } else { parms[7].Value = DBNull.Value; }
            if (site_SysConfig.Modifier != null) { parms[8].Value = site_SysConfig.Modifier; } else { parms[8].Value = DBNull.Value; }
            parms[9].Value = site_SysConfig.ModifyTime;
            if (site_SysConfig.Description != null) { parms[10].Value = site_SysConfig.Description; } else { parms[10].Value = DBNull.Value; }
            #endregion
            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);

            site_SysConfig.ConfigID = (Int32)parms[0].Value;

        }

        /// <summary>
        /// 删除
        /// </summary>
        public void Remove(Int32 configID)
        {
            string commandName = "dbo.Pr_Site_SysConfig_Delete";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@ConfigID", SqlDbType.Int)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
            }

            parms[0].Value = configID;

            #endregion
            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
        }

        /// <summary>
        /// 保存
        /// </summary>
        public void Save(Site_SysConfig site_SysConfig)
        {
            string commandName = "dbo.Pr_Site_SysConfig_Update";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@ConfigID", SqlDbType.Int),
					new SqlParameter("@OrganizationID", SqlDbType.Int),
					new SqlParameter("@Name", SqlDbType.VarChar, 50),
					new SqlParameter("@ConfigGroupID", SqlDbType.Int),
					new SqlParameter("@DisplayName", SqlDbType.VarChar, 100),
					new SqlParameter("@DefaultValue", SqlDbType.VarChar, 1000),
					new SqlParameter("@OrderNo", SqlDbType.Int),
					new SqlParameter("@UserValue", SqlDbType.VarChar, 1000),
					new SqlParameter("@Modifier", SqlDbType.VarChar, 50),
					new SqlParameter("@ModifyTime", SqlDbType.DateTime),
					new SqlParameter("@Description", SqlDbType.VarChar, 500)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
            }

            parms[0].Value = site_SysConfig.ConfigID;
            parms[1].Value = site_SysConfig.OrganizationID;
            if (site_SysConfig.Name != null) { parms[2].Value = site_SysConfig.Name; } else { parms[2].Value = DBNull.Value; }
            parms[3].Value = site_SysConfig.ConfigGroupID;
            if (site_SysConfig.DisplayName != null) { parms[4].Value = site_SysConfig.DisplayName; } else { parms[4].Value = DBNull.Value; }
            if (site_SysConfig.DefaultValue != null) { parms[5].Value = site_SysConfig.DefaultValue; } else { parms[5].Value = DBNull.Value; }
            parms[6].Value = site_SysConfig.OrderNo;
            if (site_SysConfig.UserValue != null) { parms[7].Value = site_SysConfig.UserValue; } else { parms[7].Value = DBNull.Value; }
            if (site_SysConfig.Modifier != null) { parms[8].Value = site_SysConfig.Modifier; } else { parms[8].Value = DBNull.Value; }
            parms[9].Value = site_SysConfig.ModifyTime;
            if (site_SysConfig.Description != null) { parms[10].Value = site_SysConfig.Description; } else { parms[10].Value = DBNull.Value; }
            #endregion
            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
        }

        /// <summary>
        /// 根据标识获取对象
        /// </summary>
        public Site_SysConfig GetById(Int32 configID)
        {
            Site_SysConfig site_SysConfig = null;

            string commandName = "dbo.Pr_Site_SysConfig_GetByPK";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@ConfigID", SqlDbType.Int)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = configID;

            #endregion
            using (SqlDataReader dataReader = SqlHelper.ExecuteReader(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms))
            {
                if (dataReader.Read())
                {
                    site_SysConfig = PopulateSite_SysConfigFromDataReader(dataReader);
                }
            }

            return site_SysConfig;
        }

        /// <summary>
        /// 根据参数获取对象列表（分页，可排序）
        /// </summary>
        public DataTable GetPagedList(int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {
            string commandName = "dbo.Pr_Site_SysConfig_GetPagedList";
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
        /// 根据参数获取对象列表（分页，可排序）
        /// </summary>
        public IList<Site_SysConfig> GetEntityList(int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {
            IList<Site_SysConfig> list = new List<Site_SysConfig>();
            string commandName = "dbo.Pr_Site_SysConfig_GetPagedList";
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
            using (SqlDataReader dataReader = SqlHelper.ExecuteReader(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms))
            {
                while (dataReader.Read())
                {
                    list.Add(PopulateSite_SysConfigFromDataReader(dataReader));
                }
            }
            totalRecords = (int)parms[4].Value;
            return list;
        }

        /// <summary>
        /// 从DataReader中读取数据到业务对象
        /// </summary>
        private Site_SysConfig PopulateSite_SysConfigFromDataReader(SqlDataReader reader)
        {
            Site_SysConfig site_SysConfig = new Site_SysConfig();

            int configIDIndex = reader.GetOrdinal("ConfigID");
            if (!reader.IsDBNull(configIDIndex))
            {
                site_SysConfig.ConfigID = reader.GetInt32(configIDIndex);
            }

            int organizationIDIndex = reader.GetOrdinal("OrganizationID");
            if (!reader.IsDBNull(organizationIDIndex))
            {
                site_SysConfig.OrganizationID = reader.GetInt32(organizationIDIndex);
            }

            int nameIndex = reader.GetOrdinal("Name");
            if (!reader.IsDBNull(nameIndex))
            {
                site_SysConfig.Name = reader.GetString(nameIndex);
            }

            int configGroupIDIndex = reader.GetOrdinal("ConfigGroupID");
            if (!reader.IsDBNull(configGroupIDIndex))
            {
                site_SysConfig.ConfigGroupID = reader.GetInt32(configGroupIDIndex);
            }

            int displayNameIndex = reader.GetOrdinal("DisplayName");
            if (!reader.IsDBNull(displayNameIndex))
            {
                site_SysConfig.DisplayName = reader.GetString(displayNameIndex);
            }

            int defaultValueIndex = reader.GetOrdinal("DefaultValue");
            if (!reader.IsDBNull(defaultValueIndex))
            {
                site_SysConfig.DefaultValue = reader.GetString(defaultValueIndex);
            }

            int orderNoIndex = reader.GetOrdinal("OrderNo");
            if (!reader.IsDBNull(orderNoIndex))
            {
                site_SysConfig.OrderNo = reader.GetInt32(orderNoIndex);
            }

            int userValueIndex = reader.GetOrdinal("UserValue");
            if (!reader.IsDBNull(userValueIndex))
            {
                site_SysConfig.UserValue = reader.GetString(userValueIndex);
            }

            int modifierIndex = reader.GetOrdinal("Modifier");
            if (!reader.IsDBNull(modifierIndex))
            {
                site_SysConfig.Modifier = reader.GetString(modifierIndex);
            }

            int modifyTimeIndex = reader.GetOrdinal("ModifyTime");
            if (!reader.IsDBNull(modifyTimeIndex))
            {
                site_SysConfig.ModifyTime = reader.GetDateTime(modifyTimeIndex);
            }

            int descriptionIndex = reader.GetOrdinal("Description");
            if (!reader.IsDBNull(descriptionIndex))
            {
                site_SysConfig.Description = reader.GetString(descriptionIndex);
            }

            return site_SysConfig;
        }
    }
}
