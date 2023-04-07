//==================================================================================================
//Version 1.0, auto-generated.
//Generated By xueyb.
//Date: 2012-3-9 9:58:11.
//==================================================================================================
//==================================================================================================
//This file is generated by CodeGenerator(Ver 2.1), a product of ZhouYonghua(Zhou_Yonghua@163.com).
//==================================================================================================

using System;
using System.Data;

using System.Data.SqlClient;
using ETMS.Utility.Data;

using ETMS.Components.Basic.API.Entity.Dictionary;

namespace ETMS.Components.Basic.Implement.DAL.Dictionary
{
    /// <summary>
    /// 岗位数据访问
    /// </summary>
    public partial class Dic_PostDataAccess
	{ 
        /// <summary>
        /// 增加
        /// </summary>
        public void Add(Dic_Post dic_Post)
        {
            string commandName = "dbo.Pr_Dic_Post_Insert";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {	new SqlParameter("@PostID", SqlDbType.Int, 4, ParameterDirection.Output, false, 0, 0, String.Empty, DataRowVersion.Default, null),
				
					new SqlParameter("@PostCode", SqlDbType.VarChar, 10),
					new SqlParameter("@PostName", SqlDbType.VarChar, 50),
					new SqlParameter("@Description", SqlDbType.VarChar, 500),
					new SqlParameter("@Liability", SqlDbType.VarChar, 500),
					new SqlParameter("@PostTypeID", SqlDbType.Int),
					new SqlParameter("@OrganizationID", SqlDbType.Int),
					new SqlParameter("@Status", SqlDbType.SmallInt)
					};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
            }

            if (dic_Post.PostCode != null) { parms[1].Value = dic_Post.PostCode; } else { parms[1].Value = DBNull.Value; }
            if (dic_Post.PostName != null) { parms[2].Value = dic_Post.PostName; } else { parms[2].Value = DBNull.Value; }
            if (dic_Post.Description != null) { parms[3].Value = dic_Post.Description; } else { parms[3].Value = DBNull.Value; }
            if (dic_Post.Liability != null) { parms[4].Value = dic_Post.Liability; } else { parms[4].Value = DBNull.Value; }
            parms[5].Value = dic_Post.PostTypeID;
            parms[6].Value = dic_Post.OrganizationID;
            parms[7].Value = dic_Post.Status;
            #endregion
            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);

            dic_Post.PostID = (Int32)parms[0].Value;

        }

        /// <summary>
        /// 删除
        /// </summary>
        public void Remove(Int32 postID)
        {
            string commandName = "dbo.Pr_Dic_Post_Delete";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@PostID", SqlDbType.Int)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
            }

            parms[0].Value = postID;

            #endregion
            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
        }

        /// <summary>
        /// 保存
        /// </summary>
        public void Save(Dic_Post dic_Post)
        {
            string commandName = "dbo.Pr_Dic_Post_Update";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@PostID", SqlDbType.Int),
					new SqlParameter("@PostCode", SqlDbType.VarChar, 10),
					new SqlParameter("@PostName", SqlDbType.VarChar, 50),
					new SqlParameter("@Description", SqlDbType.VarChar, 500),
					new SqlParameter("@Liability", SqlDbType.VarChar, 500),
					new SqlParameter("@PostTypeID", SqlDbType.Int),
					new SqlParameter("@OrganizationID", SqlDbType.Int),
					new SqlParameter("@Status", SqlDbType.SmallInt)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
            }

            parms[0].Value = dic_Post.PostID;
            if (dic_Post.PostCode != null) { parms[1].Value = dic_Post.PostCode; } else { parms[1].Value = DBNull.Value; }
            if (dic_Post.PostName != null) { parms[2].Value = dic_Post.PostName; } else { parms[2].Value = DBNull.Value; }
            if (dic_Post.Description != null) { parms[3].Value = dic_Post.Description; } else { parms[3].Value = DBNull.Value; }
            if (dic_Post.Liability != null) { parms[4].Value = dic_Post.Liability; } else { parms[4].Value = DBNull.Value; }
            parms[5].Value = dic_Post.PostTypeID;
            parms[6].Value = dic_Post.OrganizationID;
            parms[7].Value = dic_Post.Status;
            #endregion
            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
        }

        /// <summary>
        /// 根据标识获取对象
        /// </summary>
        public Dic_Post GetById(Int32? postID)
        {
            Dic_Post dic_Post = null;

            string commandName = "dbo.Pr_Dic_Post_GetByPK";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@PostID", SqlDbType.Int)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = postID;

            #endregion
            using (SqlDataReader dataReader = SqlHelper.ExecuteReader(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms))
            {
                if (dataReader.Read())
                {
                    dic_Post = PopulateDic_PostFromDataReader(dataReader);
                }
            }

            return dic_Post;
        }

        /// <summary>
        /// 根据参数获取对象列表（分页，可排序）
        /// </summary>
        public DataTable GetPagedList(int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {
            string commandName = "dbo.Pr_Dic_Post_GetPagedList";
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
        /// 从DataReader中读取数据到业务对象
        /// </summary>
        private Dic_Post PopulateDic_PostFromDataReader(SqlDataReader reader)
        {
            Dic_Post dic_Post = new Dic_Post();

            int postIDIndex = reader.GetOrdinal("PostID");
            if (!reader.IsDBNull(postIDIndex))
            {
                dic_Post.PostID = reader.GetInt32(postIDIndex);
            }

            int postCodeIndex = reader.GetOrdinal("PostCode");
            if (!reader.IsDBNull(postCodeIndex))
            {
                dic_Post.PostCode = reader.GetString(postCodeIndex);
            }

            int postNameIndex = reader.GetOrdinal("PostName");
            if (!reader.IsDBNull(postNameIndex))
            {
                dic_Post.PostName = reader.GetString(postNameIndex);
            }

            int descriptionIndex = reader.GetOrdinal("Description");
            if (!reader.IsDBNull(descriptionIndex))
            {
                dic_Post.Description = reader.GetString(descriptionIndex);
            }

            int liabilityIndex = reader.GetOrdinal("Liability");
            if (!reader.IsDBNull(liabilityIndex))
            {
                dic_Post.Liability = reader.GetString(liabilityIndex);
            }

            int postTypeIDIndex = reader.GetOrdinal("PostTypeID");
            if (!reader.IsDBNull(postTypeIDIndex))
            {
                dic_Post.PostTypeID = reader.GetInt32(postTypeIDIndex);
            }

            int organizationIDIndex = reader.GetOrdinal("OrganizationID");
            if (!reader.IsDBNull(organizationIDIndex))
            {
                dic_Post.OrganizationID = reader.GetInt32(organizationIDIndex);
            }

            int statusIndex = reader.GetOrdinal("Status");
            if (!reader.IsDBNull(statusIndex))
            {
                dic_Post.Status = reader.GetInt16(statusIndex);
            }

            return dic_Post;
        }
         
	}
}
