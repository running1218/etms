//==================================================================================================
//Version 1.0, auto-generated.
//Generated By xueyb.
//Date: 2012-3-28 14:31:33.
//==================================================================================================
//==================================================================================================
//This file is generated by CodeGenerator(Ver 2.1), a product of ZhouYonghua(Zhou_Yonghua@163.com).
//==================================================================================================

using System;
using System.Data;

using System.Data.SqlClient;
using ETMS.Utility.Data;

using ETMS.Components.Courseware.API.Entity;

namespace ETMS.Components.Courseware.Implement.DAL
{
    /// <summary>
    /// 课件表数据访问
    /// </summary>
    public partial class Res_CoursewareDataAccess
	{
		/// <summary>
		/// 增加
		/// </summary>
		public void Add(Res_Courseware res_Courseware)
		{
			string commandName = "dbo.Pr_Res_Courseware_Insert";
			#region Parameters
			SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
			if (parms == null)
			{
				parms = new SqlParameter[] {
					new SqlParameter("@CoursewareID", SqlDbType.UniqueIdentifier),
					new SqlParameter("@CoursewareTypeID", SqlDbType.Int),
					new SqlParameter("@OrgID", SqlDbType.Int),
					new SqlParameter("@CoursewareName", SqlDbType.NVarChar, 100),
					new SqlParameter("@CoursewarePath", SqlDbType.NVarChar, 200),
					new SqlParameter("@CoursewareSource", SqlDbType.NVarChar, 100),
					new SqlParameter("@CoursewareStatus", SqlDbType.Int),
					new SqlParameter("@ShowHoures", SqlDbType.Int),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@CreateUserID", SqlDbType.Int),
					new SqlParameter("@CreateUser", SqlDbType.NVarChar, 64),
					new SqlParameter("@ModifyTime", SqlDbType.DateTime),
					new SqlParameter("@ModifyUser", SqlDbType.NVarChar, 64),
					new SqlParameter("@Remark", SqlDbType.NVarChar, 1024),
					new SqlParameter("@DelFlag", SqlDbType.Bit),
                    new SqlParameter("@IsUrl", SqlDbType.Bit),
                    new SqlParameter("@CoverImg", SqlDbType.NVarChar)
					};
				SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
			}
			
			parms[0].Value = res_Courseware.CoursewareID;
			parms[1].Value = res_Courseware.CoursewareTypeID;
			parms[2].Value = res_Courseware.OrgID;
			if (res_Courseware.CoursewareName!= null){ parms[3].Value = res_Courseware.CoursewareName; } else { parms[3].Value = DBNull.Value; }
			if (res_Courseware.CoursewarePath!= null){ parms[4].Value = res_Courseware.CoursewarePath; } else { parms[4].Value = DBNull.Value; }
			if (res_Courseware.CoursewareSource!= null){ parms[5].Value = res_Courseware.CoursewareSource; } else { parms[5].Value = DBNull.Value; }
			parms[6].Value = res_Courseware.CoursewareStatus;
			parms[7].Value = res_Courseware.ShowHoures;
			parms[8].Value = res_Courseware.CreateTime;
			parms[9].Value = res_Courseware.CreateUserID;
			if (res_Courseware.CreateUser!= null){ parms[10].Value = res_Courseware.CreateUser; } else { parms[10].Value = DBNull.Value; }
			parms[11].Value = res_Courseware.ModifyTime;
			if (res_Courseware.ModifyUser!= null){ parms[12].Value = res_Courseware.ModifyUser; } else { parms[12].Value = DBNull.Value; }
			if (res_Courseware.Remark!= null){ parms[13].Value = res_Courseware.Remark; } else { parms[13].Value = DBNull.Value; }
			parms[14].Value = res_Courseware.DelFlag;
            parms[15].Value = res_Courseware.IsURL;
            parms[16].Value = res_Courseware.CoverImg;
			#endregion
			SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
			
		}
		
		/// <summary>
		/// 删除
		/// </summary>
		public void Remove(Guid coursewareID)
		{
			string commandName = "dbo.Pr_Res_Courseware_Delete";
			#region Parameters
			SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
			if (parms == null)
			{
				parms = new SqlParameter[] {
					new SqlParameter("@CoursewareID", SqlDbType.UniqueIdentifier)
				};
				SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
			}
			
			parms[0].Value = coursewareID;
			
			#endregion
			SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
		}
		
		/// <summary>
		/// 保存
		/// </summary>
		public void Save(Res_Courseware res_Courseware)
		{
			string commandName = "dbo.Pr_Res_Courseware_Update";
			#region Parameters
			SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
			if (parms == null)
			{
				parms = new SqlParameter[] {
					new SqlParameter("@CoursewareID", SqlDbType.UniqueIdentifier),
					new SqlParameter("@CoursewareTypeID", SqlDbType.Int),
					new SqlParameter("@OrgID", SqlDbType.Int),
					new SqlParameter("@CoursewareName", SqlDbType.NVarChar, 100),
					new SqlParameter("@CoursewarePath", SqlDbType.NVarChar, 200),
					new SqlParameter("@CoursewareSource", SqlDbType.NVarChar, 100),
					new SqlParameter("@CoursewareStatus", SqlDbType.Int),
					new SqlParameter("@ShowHoures", SqlDbType.Int),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@CreateUserID", SqlDbType.Int),
					new SqlParameter("@CreateUser", SqlDbType.NVarChar, 64),
					new SqlParameter("@ModifyTime", SqlDbType.DateTime),
					new SqlParameter("@ModifyUser", SqlDbType.NVarChar, 64),
					new SqlParameter("@Remark", SqlDbType.NVarChar, 1024),
					new SqlParameter("@DelFlag", SqlDbType.Bit),
                    new SqlParameter("@IsUrl", SqlDbType.Bit),
                    new SqlParameter("@ResourceStatus", SqlDbType.Int),
                    new SqlParameter("@CoverImg", SqlDbType.NVarChar)
				};
				SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
			}
			
			parms[0].Value = res_Courseware.CoursewareID;
			parms[1].Value = res_Courseware.CoursewareTypeID;
			parms[2].Value = res_Courseware.OrgID;
			if (res_Courseware.CoursewareName!= null){ parms[3].Value = res_Courseware.CoursewareName; } else { parms[3].Value = DBNull.Value; }
			if (res_Courseware.CoursewarePath!= null){ parms[4].Value = res_Courseware.CoursewarePath; } else { parms[4].Value = DBNull.Value; }
			if (res_Courseware.CoursewareSource!= null){ parms[5].Value = res_Courseware.CoursewareSource; } else { parms[5].Value = DBNull.Value; }
			parms[6].Value = res_Courseware.CoursewareStatus;
			parms[7].Value = res_Courseware.ShowHoures;
			parms[8].Value = res_Courseware.CreateTime;
			parms[9].Value = res_Courseware.CreateUserID;
			if (res_Courseware.CreateUser!= null){ parms[10].Value = res_Courseware.CreateUser; } else { parms[10].Value = DBNull.Value; }
			parms[11].Value = res_Courseware.ModifyTime;
			if (res_Courseware.ModifyUser!= null){ parms[12].Value = res_Courseware.ModifyUser; } else { parms[12].Value = DBNull.Value; }
			if (res_Courseware.Remark!= null){ parms[13].Value = res_Courseware.Remark; } else { parms[13].Value = DBNull.Value; }
			parms[14].Value = res_Courseware.DelFlag;
            parms[15].Value = res_Courseware.IsURL;
            parms[16].Value = res_Courseware.ResourceStatus;
            parms[17].Value = res_Courseware.CoverImg;
			#endregion
			SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
		}
		
		/// <summary>
		/// 根据标识获取对象
		/// </summary>
		public Res_Courseware GetById(Guid coursewareID)
		{
			Res_Courseware res_Courseware = null;
			
			string commandName = "dbo.Pr_Res_Courseware_GetByPK";
			#region Parameters
			SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
			if (parms == null)
			{
				parms = new SqlParameter[] {
					new SqlParameter("@CoursewareID", SqlDbType.UniqueIdentifier)
				};
				SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
			}
			
			parms[0].Value = coursewareID;
			
			#endregion
			using (SqlDataReader dataReader = SqlHelper.ExecuteReader(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms))
			{
				if (dataReader.Read())
				{
					res_Courseware = PopulateRes_CoursewareFromDataReader(dataReader);
				}
			}
			
			return res_Courseware;
		}				
		
		/// <summary>
		/// 根据参数获取对象列表（分页，可排序）
		/// </summary>
		public DataTable GetPagedList(int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
		{			
			string commandName = "dbo.Pr_Res_Courseware_GetPagedList";
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
		private Res_Courseware PopulateRes_CoursewareFromDataReader(SqlDataReader reader)
		{
			Res_Courseware res_Courseware = new Res_Courseware();
			
			int coursewareIDIndex = reader.GetOrdinal("CoursewareID"); 
			if(!reader.IsDBNull(coursewareIDIndex))
			{
				res_Courseware.CoursewareID= reader.GetGuid(coursewareIDIndex);
			}
			
			int coursewareTypeIDIndex = reader.GetOrdinal("CoursewareTypeID"); 
			if(!reader.IsDBNull(coursewareTypeIDIndex))
			{
				res_Courseware.CoursewareTypeID= reader.GetInt32(coursewareTypeIDIndex);
			}
			
			int orgIDIndex = reader.GetOrdinal("OrgID"); 
			if(!reader.IsDBNull(orgIDIndex))
			{
				res_Courseware.OrgID= reader.GetInt32(orgIDIndex);
			}
			
			int coursewareNameIndex = reader.GetOrdinal("CoursewareName"); 
			if(!reader.IsDBNull(coursewareNameIndex))
			{
				res_Courseware.CoursewareName= reader.GetString(coursewareNameIndex);
			}
			
			int coursewarePathIndex = reader.GetOrdinal("CoursewarePath"); 
			if(!reader.IsDBNull(coursewarePathIndex))
			{
				res_Courseware.CoursewarePath= reader.GetString(coursewarePathIndex);
			}
			
			int coursewareSourceIndex = reader.GetOrdinal("CoursewareSource"); 
			if(!reader.IsDBNull(coursewareSourceIndex))
			{
				res_Courseware.CoursewareSource= reader.GetString(coursewareSourceIndex);
			}
			
			int coursewareStatusIndex = reader.GetOrdinal("CoursewareStatus"); 
			if(!reader.IsDBNull(coursewareStatusIndex))
			{
				res_Courseware.CoursewareStatus= reader.GetInt32(coursewareStatusIndex);
			}
			
			int showHouresIndex = reader.GetOrdinal("ShowHoures"); 
			if(!reader.IsDBNull(showHouresIndex))
			{
				res_Courseware.ShowHoures= reader.GetInt32(showHouresIndex);
			}
			
			int createTimeIndex = reader.GetOrdinal("CreateTime"); 
			if(!reader.IsDBNull(createTimeIndex))
			{
				res_Courseware.CreateTime= reader.GetDateTime(createTimeIndex);
			}
			
			int createUserIDIndex = reader.GetOrdinal("CreateUserID"); 
			if(!reader.IsDBNull(createUserIDIndex))
			{
				res_Courseware.CreateUserID= reader.GetInt32(createUserIDIndex);
			}
			
			int createUserIndex = reader.GetOrdinal("CreateUser"); 
			if(!reader.IsDBNull(createUserIndex))
			{
				res_Courseware.CreateUser= reader.GetString(createUserIndex);
			}
			
			int modifyTimeIndex = reader.GetOrdinal("ModifyTime"); 
			if(!reader.IsDBNull(modifyTimeIndex))
			{
				res_Courseware.ModifyTime= reader.GetDateTime(modifyTimeIndex);
			}
			
			int modifyUserIndex = reader.GetOrdinal("ModifyUser"); 
			if(!reader.IsDBNull(modifyUserIndex))
			{
				res_Courseware.ModifyUser= reader.GetString(modifyUserIndex);
			}
			
			int remarkIndex = reader.GetOrdinal("Remark"); 
			if(!reader.IsDBNull(remarkIndex))
			{
				res_Courseware.Remark= reader.GetString(remarkIndex);
			}
			
			int delFlagIndex = reader.GetOrdinal("DelFlag"); 
			if(!reader.IsDBNull(delFlagIndex))
			{
				res_Courseware.DelFlag= reader.GetBoolean(delFlagIndex);
			}

            int isUrlIndex = reader.GetOrdinal("IsUrl");
            if (!reader.IsDBNull(isUrlIndex))
            {
                res_Courseware.IsURL = reader.GetBoolean(isUrlIndex);
            }

            int resourceStatuslIndex = reader.GetOrdinal("ResourceStatus");
            if (!reader.IsDBNull(resourceStatuslIndex))
            {
                res_Courseware.ResourceStatus = reader.GetInt32(resourceStatuslIndex);
            }

            int resourcePathlIndex = reader.GetOrdinal("ResourcePath");
            if (!reader.IsDBNull(resourcePathlIndex))
            {
                res_Courseware.ResourcePath = reader.GetString(resourcePathlIndex);
            }
            int CoverImgIndex = reader.GetOrdinal("CoverImg");
            if (!reader.IsDBNull(CoverImgIndex))
            {
                res_Courseware.CoverImg = reader.GetString(CoverImgIndex);
            }
			
			return res_Courseware; 
		}
	}
}
