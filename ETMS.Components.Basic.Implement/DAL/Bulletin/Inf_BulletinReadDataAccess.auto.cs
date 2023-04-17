//==================================================================================================
//Version 1.0, auto-generated.
//Generated By running1218
//Date: 2012/3/29 10:29:27.
//==================================================================================================
//==================================================================================================
//This file is generated by CodeGenerator(Ver 2.1)
//==================================================================================================

using System;
using System.Data;

using System.Data.SqlClient;
using ETMS.Utility.Data;

using ETMS.Components.Basic.API.Entity.Bulletin;

namespace ETMS.Components.Basic.Implement.DAL.Bulletin
{
    /// <summary>
    /// 公告已读记录表数据访问
    /// </summary>
    public partial class Inf_BulletinReadDataAccess
	{
		/// <summary>
		/// 增加
		/// </summary>
		public void Add(Inf_BulletinRead inf_BulletinRead)
		{
			string commandName = "dbo.Pr_Inf_BulletinRead_Insert";
			#region Parameters
			SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
			if (parms == null)
			{
				parms = new SqlParameter[] {	new SqlParameter("@ArticleClickID", SqlDbType.Int, 4, ParameterDirection.Output, false, 0, 0, String.Empty, DataRowVersion.Default, null),
				
					new SqlParameter("@ArticleID", SqlDbType.Int),
					new SqlParameter("@UserID", SqlDbType.Int),
					new SqlParameter("@CreateTime", SqlDbType.DateTime)
					};
				SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
			}
			
			parms[1].Value = inf_BulletinRead.ArticleID;
			parms[2].Value = inf_BulletinRead.UserID;
			parms[3].Value = inf_BulletinRead.CreateTime;
			#endregion
			SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
			
			inf_BulletinRead.ArticleClickID = (Int32)parms[0].Value;
			
		}
		
		/// <summary>
		/// 删除
		/// </summary>
		public void Remove(Int32 articleClickID)
		{
			string commandName = "dbo.Pr_Inf_BulletinRead_Delete";
			#region Parameters
			SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
			if (parms == null)
			{
				parms = new SqlParameter[] {
					new SqlParameter("@ArticleClickID", SqlDbType.Int)
				};
				SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
			}
			
			parms[0].Value = articleClickID;
			
			#endregion
			SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
		}
		
		/// <summary>
		/// 保存
		/// </summary>
		public void Save(Inf_BulletinRead inf_BulletinRead)
		{
			string commandName = "dbo.Pr_Inf_BulletinRead_Update";
			#region Parameters
			SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
			if (parms == null)
			{
				parms = new SqlParameter[] {
					new SqlParameter("@ArticleClickID", SqlDbType.Int),
					new SqlParameter("@ArticleID", SqlDbType.Int),
					new SqlParameter("@UserID", SqlDbType.Int),
					new SqlParameter("@CreateTime", SqlDbType.DateTime)
				};
				SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
			}
			
			parms[0].Value = inf_BulletinRead.ArticleClickID;
			parms[1].Value = inf_BulletinRead.ArticleID;
			parms[2].Value = inf_BulletinRead.UserID;
			parms[3].Value = inf_BulletinRead.CreateTime;
			#endregion
			SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
		}
		
		/// <summary>
		/// 根据标识获取对象
		/// </summary>
		public Inf_BulletinRead GetById(Int32 articleClickID)
		{
			Inf_BulletinRead inf_BulletinRead = null;
			
			string commandName = "dbo.Pr_Inf_BulletinRead_GetByPK";
			#region Parameters
			SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
			if (parms == null)
			{
				parms = new SqlParameter[] {
					new SqlParameter("@ArticleClickID", SqlDbType.Int)
				};
				SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
			}
			
			parms[0].Value = articleClickID;
			
			#endregion
			using (SqlDataReader dataReader = SqlHelper.ExecuteReader(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms))
			{
				if (dataReader.Read())
				{
					inf_BulletinRead = PopulateInf_BulletinReadFromDataReader(dataReader);
				}
			}
			
			return inf_BulletinRead;
		}				
		
        public int GetReadNum(int articleID)
		{
			int readNum = 0;

            string commandName = "Pr_Inf_BulletinRead_GetReadNum";
			#region Parameters
			SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
			if (parms == null)
			{
				parms = new SqlParameter[] {
					new SqlParameter("@ArticleID", SqlDbType.Int)
				};
				SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
			}

            parms[0].Value = articleID;
			
			#endregion
			using (SqlDataReader dataReader = SqlHelper.ExecuteReader(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms))
			{
				if (dataReader.Read())
				{
                    int index = dataReader.GetOrdinal("Num");
                    if (!dataReader.IsDBNull(index))
                    {
                        readNum = dataReader.GetInt32(index);
                    }
				}
			}

            return readNum;
		}
		/// <summary>
		/// 根据参数获取对象列表（分页，可排序）
		/// </summary>
		public DataTable GetPagedList(int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
		{			
			string commandName = "dbo.Pr_Inf_BulletinRead_GetPagedList";
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
			DataTable dt=SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];
			totalRecords = (int)parms[4].Value;
			return dt;
		}
		
		/// <summary>
		/// 从DataReader中读取数据到业务对象
		/// </summary>
		private Inf_BulletinRead PopulateInf_BulletinReadFromDataReader(SqlDataReader reader)
		{
			Inf_BulletinRead inf_BulletinRead = new Inf_BulletinRead();
			
			int articleClickIDIndex = reader.GetOrdinal("ArticleClickID"); 
			if(!reader.IsDBNull(articleClickIDIndex))
			{
				inf_BulletinRead.ArticleClickID= reader.GetInt32(articleClickIDIndex);
			}
			
			int articleIDIndex = reader.GetOrdinal("ArticleID"); 
			if(!reader.IsDBNull(articleIDIndex))
			{
				inf_BulletinRead.ArticleID= reader.GetInt32(articleIDIndex);
			}
			
			int userIDIndex = reader.GetOrdinal("UserID"); 
			if(!reader.IsDBNull(userIDIndex))
			{
				inf_BulletinRead.UserID= reader.GetInt32(userIDIndex);
			}
			
			int createTimeIndex = reader.GetOrdinal("CreateTime"); 
			if(!reader.IsDBNull(createTimeIndex))
			{
				inf_BulletinRead.CreateTime= reader.GetDateTime(createTimeIndex);
			}
			
			return inf_BulletinRead; 
		}
	}
}
