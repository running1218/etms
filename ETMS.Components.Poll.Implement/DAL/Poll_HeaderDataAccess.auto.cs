//==================================================================================================
//Version 1.0, auto-generated.
//Generated By running1218
//Date: 2012-4-17 15:53:39.
//==================================================================================================
//==================================================================================================
//This file is generated by CodeGenerator(Ver 2.1)
//==================================================================================================

using System;
using System.Collections.Generic;
using System.Data;

using System.Data.SqlClient;
using ETMS.Utility.Data;

using ETMS.Components.Poll.API.Entity;

namespace ETMS.Components.Poll.Implement.DAL
{
    /// <summary>
    /// 选项列标题数据访问
    /// </summary>
    public partial class Poll_HeaderDataAccess
	{
		/// <summary>
		/// 增加
		/// </summary>
		public void Add(Poll_Header poll_Header)
		{
			string commandName = "dbo.Pr_Poll_Header_Insert";
			#region Parameters
			SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
			if (parms == null)
			{
				parms = new SqlParameter[] {	new SqlParameter("@HeaderID", SqlDbType.Int, 4, ParameterDirection.Output, false, 0, 0, String.Empty, DataRowVersion.Default, null),
				
					new SqlParameter("@TitleID", SqlDbType.Int),
					new SqlParameter("@HeaderName", SqlDbType.NVarChar),
					new SqlParameter("@HeaderNo", SqlDbType.Int)
					};
				SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
			}
			
			parms[1].Value = poll_Header.TitleID;
			if (poll_Header.HeaderName!= null){ parms[2].Value = poll_Header.HeaderName; } else { parms[2].Value = DBNull.Value; }
			parms[3].Value = poll_Header.HeaderNo;
			#endregion
			SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
			
			poll_Header.HeaderID = (Int32)parms[0].Value;
			
		}
		
		/// <summary>
		/// 删除
		/// </summary>
		public void Remove(Int32 headerID)
		{
			string commandName = "dbo.Pr_Poll_Header_Delete";
			#region Parameters
			SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
			if (parms == null)
			{
				parms = new SqlParameter[] {
					new SqlParameter("@HeaderID", SqlDbType.Int)
				};
				SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
			}
			
			parms[0].Value = headerID;
			
			#endregion
			SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
		}
		
		/// <summary>
		/// 保存
		/// </summary>
		public void Save(Poll_Header poll_Header)
		{
			string commandName = "dbo.Pr_Poll_Header_Update";
			#region Parameters
			SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
			if (parms == null)
			{
				parms = new SqlParameter[] {
					new SqlParameter("@HeaderID", SqlDbType.Int),
					new SqlParameter("@TitleID", SqlDbType.Int),
					new SqlParameter("@HeaderName", SqlDbType.NVarChar),
					new SqlParameter("@HeaderNo", SqlDbType.Int)
				};
				SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
			}
			
			parms[0].Value = poll_Header.HeaderID;
			parms[1].Value = poll_Header.TitleID;
			if (poll_Header.HeaderName!= null){ parms[2].Value = poll_Header.HeaderName; } else { parms[2].Value = DBNull.Value; }
			parms[3].Value = poll_Header.HeaderNo;
			#endregion
			SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
		}
		
		/// <summary>
		/// 根据标识获取对象
		/// </summary>
		public Poll_Header GetById(Int32 headerID)
		{
			Poll_Header poll_Header = null;
			
			string commandName = "dbo.Pr_Poll_Header_GetByPK";
			#region Parameters
			SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
			if (parms == null)
			{
				parms = new SqlParameter[] {
					new SqlParameter("@HeaderID", SqlDbType.Int)
				};
				SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
			}
			
			parms[0].Value = headerID;
			
			#endregion
			using (SqlDataReader dataReader = SqlHelper.ExecuteReader(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms))
			{
				if (dataReader.Read())
				{
					poll_Header = PopulatePoll_HeaderFromDataReader(dataReader);
				}
			}
			
			return poll_Header;
		}				
		
		/// <summary>
		/// 根据参数获取对象列表（分页，可排序）
		/// </summary>
		public DataTable GetPagedList(int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
		{			
			string commandName = "dbo.Pr_Poll_Header_GetPagedList";
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
		/// 根据参数获取对象列表（分页，可排序）
		/// </summary>
		public IList<Poll_Header> GetEntityList(int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
		{			
            IList<Poll_Header> list=new List<Poll_Header>();
			string commandName = "dbo.Pr_Poll_Header_GetPagedList";
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
					list.Add(PopulatePoll_HeaderFromDataReader(dataReader));
				}
			}			
			totalRecords = (int)parms[4].Value;
			return list;
		}
		
		/// <summary>
		/// 从DataReader中读取数据到业务对象
		/// </summary>
		private Poll_Header PopulatePoll_HeaderFromDataReader(SqlDataReader reader)
		{
			Poll_Header poll_Header = new Poll_Header();
			
			int headerIDIndex = reader.GetOrdinal("HeaderID"); 
			if(!reader.IsDBNull(headerIDIndex))
			{
				poll_Header.HeaderID= reader.GetInt32(headerIDIndex);
			}
			
			int titleIDIndex = reader.GetOrdinal("TitleID"); 
			if(!reader.IsDBNull(titleIDIndex))
			{
				poll_Header.TitleID= reader.GetInt32(titleIDIndex);
			}
			
			int headerNameIndex = reader.GetOrdinal("HeaderName"); 
			if(!reader.IsDBNull(headerNameIndex))
			{
				poll_Header.HeaderName= reader.GetString(headerNameIndex);
			}
			
			int headerNoIndex = reader.GetOrdinal("HeaderNo"); 
			if(!reader.IsDBNull(headerNoIndex))
			{
				poll_Header.HeaderNo= reader.GetInt32(headerNoIndex);
			}
			
			return poll_Header; 
		}
	}
}
