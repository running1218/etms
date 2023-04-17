//==================================================================================================
//Version 1.0, auto-generated.
//Generated By running1218
//Date: 2012/3/29 17:59:09.
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
    /// 公告表数据访问
    /// </summary>
    public partial class Inf_BulletinDataAccess
	{
		/// <summary>
		/// 增加
		/// </summary>
		public void Add(Inf_Bulletin inf_Bulletin)
		{
			string commandName = "dbo.Pr_Inf_Bulletin_Insert";
			#region Parameters
			SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
			if (parms == null)
			{
				parms = new SqlParameter[] {	new SqlParameter("@ArticleID", SqlDbType.Int, 4, ParameterDirection.Output, false, 0, 0, String.Empty, DataRowVersion.Default, null),
				
					new SqlParameter("@InfoLevelID", SqlDbType.Int),
					new SqlParameter("@ArticleTypeID", SqlDbType.Int),
					new SqlParameter("@MainHead", SqlDbType.NVarChar, 200),
					new SqlParameter("@Brief", SqlDbType.NVarChar, 400),
					new SqlParameter("@Keyword", SqlDbType.NVarChar, 200),
					new SqlParameter("@ArticleContent", SqlDbType.NVarChar),
                    new SqlParameter("@OrgID", SqlDbType.Int),
					new SqlParameter("@BeginDate", SqlDbType.DateTime),
					new SqlParameter("@EndDate", SqlDbType.DateTime),
					new SqlParameter("@IsTop", SqlDbType.Bit),
					new SqlParameter("@IsUse", SqlDbType.Int),
					new SqlParameter("@CreateMan", SqlDbType.NVarChar, 128),
					new SqlParameter("@CreateUserID", SqlDbType.Int),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@UpdateMan", SqlDbType.NVarChar, 128),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
                    new SqlParameter("@ImageUrl", SqlDbType.NVarChar)
                    };
				SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
			}
			
			parms[1].Value = inf_Bulletin.InfoLevelID;
			parms[2].Value = inf_Bulletin.ArticleTypeID;
			if (inf_Bulletin.MainHead!= null){ parms[3].Value = inf_Bulletin.MainHead; } else { parms[3].Value = DBNull.Value; }
			if (inf_Bulletin.Brief!= null){ parms[4].Value = inf_Bulletin.Brief; } else { parms[4].Value = DBNull.Value; }
			if (inf_Bulletin.Keyword!= null){ parms[5].Value = inf_Bulletin.Keyword; } else { parms[5].Value = DBNull.Value; }
			if (inf_Bulletin.ArticleContent!= null){ parms[6].Value = inf_Bulletin.ArticleContent; } else { parms[6].Value = DBNull.Value; }
			parms[7].Value = inf_Bulletin.OrgID;
			parms[8].Value = inf_Bulletin.BeginDate;
			parms[9].Value = inf_Bulletin.EndDate;
			parms[10].Value = inf_Bulletin.IsTop;
			parms[11].Value = inf_Bulletin.IsUse;
			if (inf_Bulletin.CreateMan!= null){ parms[12].Value = inf_Bulletin.CreateMan; } else { parms[12].Value = DBNull.Value; }
			parms[13].Value = inf_Bulletin.CreateUserID;
			parms[14].Value = inf_Bulletin.CreateTime;
			if (inf_Bulletin.UpdateMan!= null){ parms[15].Value = inf_Bulletin.UpdateMan; } else { parms[15].Value = DBNull.Value; }
			parms[16].Value = inf_Bulletin.UpdateTime;
            parms[17].Value = inf_Bulletin.ImageUrl;
            #endregion
            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
			
			inf_Bulletin.ArticleID = (Int32)parms[0].Value;
			
		}
		
		/// <summary>
		/// 删除
		/// </summary>
		public void Remove(Int32 articleID)
		{
			string commandName = "dbo.Pr_Inf_Bulletin_Delete";
			#region Parameters
			SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
			if (parms == null)
			{
				parms = new SqlParameter[] {
					new SqlParameter("@ArticleID", SqlDbType.Int)
				};
				SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
			}
			
			parms[0].Value = articleID;
			
			#endregion
			SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
		}
		
		/// <summary>
		/// 保存
		/// </summary>
		public void Save(Inf_Bulletin inf_Bulletin)
		{
			string commandName = "dbo.Pr_Inf_Bulletin_Update";
			#region Parameters
			SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
			if (parms == null)
			{
				parms = new SqlParameter[] {
					new SqlParameter("@ArticleID", SqlDbType.Int),
					new SqlParameter("@InfoLevelID", SqlDbType.Int),
					new SqlParameter("@ArticleTypeID", SqlDbType.Int),
					new SqlParameter("@MainHead", SqlDbType.NVarChar, 200),
					new SqlParameter("@Brief", SqlDbType.NVarChar, 400),
					new SqlParameter("@Keyword", SqlDbType.NVarChar, 200),
					new SqlParameter("@ArticleContent", SqlDbType.NVarChar),
					new SqlParameter("@OrgID", SqlDbType.Int),
					new SqlParameter("@BeginDate", SqlDbType.DateTime),
					new SqlParameter("@EndDate", SqlDbType.DateTime),
					new SqlParameter("@IsTop", SqlDbType.Bit),
					new SqlParameter("@IsUse", SqlDbType.Int),
					new SqlParameter("@CreateMan", SqlDbType.NVarChar, 128),
					new SqlParameter("@CreateUserID", SqlDbType.Int),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@UpdateMan", SqlDbType.NVarChar, 128),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
                    new SqlParameter("@ImageUrl", SqlDbType.NVarChar)
                };
				SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
			}
			
			parms[0].Value = inf_Bulletin.ArticleID;
			parms[1].Value = inf_Bulletin.InfoLevelID;
			parms[2].Value = inf_Bulletin.ArticleTypeID;
			if (inf_Bulletin.MainHead!= null){ parms[3].Value = inf_Bulletin.MainHead; } else { parms[3].Value = DBNull.Value; }
			if (inf_Bulletin.Brief!= null){ parms[4].Value = inf_Bulletin.Brief; } else { parms[4].Value = DBNull.Value; }
			if (inf_Bulletin.Keyword!= null){ parms[5].Value = inf_Bulletin.Keyword; } else { parms[5].Value = DBNull.Value; }
			if (inf_Bulletin.ArticleContent!= null){ parms[6].Value = inf_Bulletin.ArticleContent; } else { parms[6].Value = DBNull.Value; }
			parms[7].Value = inf_Bulletin.OrgID;
			parms[8].Value = inf_Bulletin.BeginDate;
			parms[9].Value = inf_Bulletin.EndDate;
			parms[10].Value = inf_Bulletin.IsTop;
			parms[11].Value = inf_Bulletin.IsUse;
			if (inf_Bulletin.CreateMan!= null){ parms[12].Value = inf_Bulletin.CreateMan; } else { parms[12].Value = DBNull.Value; }
			parms[13].Value = inf_Bulletin.CreateUserID;
			parms[14].Value = inf_Bulletin.CreateTime;
			if (inf_Bulletin.UpdateMan!= null){ parms[15].Value = inf_Bulletin.UpdateMan; } else { parms[15].Value = DBNull.Value; }
			parms[16].Value = inf_Bulletin.UpdateTime;
            parms[17].Value = inf_Bulletin.ImageUrl;
            #endregion
            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
		}
		
		/// <summary>
		/// 根据标识获取对象
		/// </summary>
		public Inf_Bulletin GetById(Int32 articleID)
		{
			Inf_Bulletin inf_Bulletin = null;
			
			string commandName = "dbo.Pr_Inf_Bulletin_GetByPK";
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
					inf_Bulletin = PopulateInf_BulletinFromDataReader(dataReader);
				}
			}
			
			return inf_Bulletin;
		}				
		
		/// <summary>
		/// 根据参数获取对象列表（分页，可排序）
		/// </summary>
		public DataTable GetPagedList(int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
		{			
			string commandName = "dbo.Pr_Inf_Bulletin_GetPagedList";
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
		/// 从DataReader中读取数据到业务对象
		/// </summary>
		private Inf_Bulletin PopulateInf_BulletinFromDataReader(SqlDataReader reader)
		{
			Inf_Bulletin inf_Bulletin = new Inf_Bulletin();
			
			int articleIDIndex = reader.GetOrdinal("ArticleID"); 
			if(!reader.IsDBNull(articleIDIndex))
			{
				inf_Bulletin.ArticleID= reader.GetInt32(articleIDIndex);
			}
			
			int infoLevelIDIndex = reader.GetOrdinal("InfoLevelID"); 
			if(!reader.IsDBNull(infoLevelIDIndex))
			{
				inf_Bulletin.InfoLevelID= reader.GetInt32(infoLevelIDIndex);
			}
			
			int articleTypeIDIndex = reader.GetOrdinal("ArticleTypeID"); 
			if(!reader.IsDBNull(articleTypeIDIndex))
			{
				inf_Bulletin.ArticleTypeID= reader.GetInt32(articleTypeIDIndex);
			}
			
			int mainHeadIndex = reader.GetOrdinal("MainHead"); 
			if(!reader.IsDBNull(mainHeadIndex))
			{
				inf_Bulletin.MainHead= reader.GetString(mainHeadIndex);
			}
			
			int briefIndex = reader.GetOrdinal("Brief"); 
			if(!reader.IsDBNull(briefIndex))
			{
				inf_Bulletin.Brief= reader.GetString(briefIndex);
			}
			
			int keywordIndex = reader.GetOrdinal("Keyword"); 
			if(!reader.IsDBNull(keywordIndex))
			{
				inf_Bulletin.Keyword= reader.GetString(keywordIndex);
			}
			
			int articleContentIndex = reader.GetOrdinal("ArticleContent"); 
			if(!reader.IsDBNull(articleContentIndex))
			{
				inf_Bulletin.ArticleContent= reader.GetString(articleContentIndex);
			}
            int imageUrlIndex = reader.GetOrdinal("ImageUrl");
            if (!reader.IsDBNull(imageUrlIndex))
            {
                inf_Bulletin.ImageUrl = reader.GetString(imageUrlIndex);
            }

            int orgIDIndex = reader.GetOrdinal("OrgID"); 
			if(!reader.IsDBNull(orgIDIndex))
			{
				inf_Bulletin.OrgID= reader.GetInt32(orgIDIndex);
			}
			
			int beginDateIndex = reader.GetOrdinal("BeginDate"); 
			if(!reader.IsDBNull(beginDateIndex))
			{
				inf_Bulletin.BeginDate= reader.GetDateTime(beginDateIndex);
			}
			
			int endDateIndex = reader.GetOrdinal("EndDate"); 
			if(!reader.IsDBNull(endDateIndex))
			{
				inf_Bulletin.EndDate= reader.GetDateTime(endDateIndex);
			}
			
			int isTopIndex = reader.GetOrdinal("IsTop"); 
			if(!reader.IsDBNull(isTopIndex))
			{
				inf_Bulletin.IsTop= reader.GetBoolean(isTopIndex);
			}
			
			int isUseIndex = reader.GetOrdinal("IsUse"); 
			if(!reader.IsDBNull(isUseIndex))
			{
				inf_Bulletin.IsUse= reader.GetInt32(isUseIndex);
			}
			
			int createManIndex = reader.GetOrdinal("CreateMan"); 
			if(!reader.IsDBNull(createManIndex))
			{
				inf_Bulletin.CreateMan= reader.GetString(createManIndex);
			}
			
			int createUserIDIndex = reader.GetOrdinal("CreateUserID"); 
			if(!reader.IsDBNull(createUserIDIndex))
			{
				inf_Bulletin.CreateUserID= reader.GetInt32(createUserIDIndex);
			}
			
			int createTimeIndex = reader.GetOrdinal("CreateTime"); 
			if(!reader.IsDBNull(createTimeIndex))
			{
				inf_Bulletin.CreateTime= reader.GetDateTime(createTimeIndex);
			}
			
			int updateManIndex = reader.GetOrdinal("UpdateMan"); 
			if(!reader.IsDBNull(updateManIndex))
			{
				inf_Bulletin.UpdateMan= reader.GetString(updateManIndex);
			}
			
			int updateTimeIndex = reader.GetOrdinal("UpdateTime"); 
			if(!reader.IsDBNull(updateTimeIndex))
			{
				inf_Bulletin.UpdateTime= reader.GetDateTime(updateTimeIndex);
			}
			
			return inf_Bulletin; 
		}
	}
}
