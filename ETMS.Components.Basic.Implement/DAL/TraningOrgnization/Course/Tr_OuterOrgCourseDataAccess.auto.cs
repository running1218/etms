//==================================================================================================
//Version 1.0, auto-generated.
//Generated By huangzhf.
//Date: 2012-4-24 21:06:16.
//==================================================================================================
//==================================================================================================
//This file is generated by CodeGenerator(Ver 2.1)
//==================================================================================================

using System;
using System.Collections.Generic;
using System.Data;

using System.Data.SqlClient;
using ETMS.Utility.Data;

using ETMS.Components.Basic.API.Entity.TraningOrgnization.Course;

namespace ETMS.Components.Basic.Implement.DAL.TraningOrgnization.Course
{
    /// <summary>
    /// 外部培训机构课程表数据访问
    /// </summary>
    public partial class Tr_OuterOrgCourseDataAccess
	{
		/// <summary>
		/// 增加
		/// </summary>
		public void Add(Tr_OuterOrgCourse tr_OuterOrgCourse)
		{
			string commandName = "dbo.Pr_Tr_OuterOrgCourse_Insert";
			#region Parameters
			SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
			if (parms == null)
			{
				parms = new SqlParameter[] {
					new SqlParameter("@OuterOrgCourseID", SqlDbType.UniqueIdentifier),
					new SqlParameter("@OuterOrgID", SqlDbType.UniqueIdentifier),
					new SqlParameter("@CourseCode", SqlDbType.NVarChar, 100),
					new SqlParameter("@CourseName", SqlDbType.NVarChar, 200),
					new SqlParameter("@CourseTypeID", SqlDbType.Int),
					new SqlParameter("@ThumbnailURL", SqlDbType.NVarChar, 256),
					new SqlParameter("@AddrURL", SqlDbType.NVarChar, 256),
					new SqlParameter("@ForObject", SqlDbType.NVarChar, -1),
					new SqlParameter("@CourseIntroduction", SqlDbType.NVarChar, -1),
					new SqlParameter("@CourseOutline", SqlDbType.NVarChar, -1),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@CreateUserID", SqlDbType.Int),
					new SqlParameter("@CreateUser", SqlDbType.NVarChar, 128),
					new SqlParameter("@ModifyTime", SqlDbType.DateTime),
					new SqlParameter("@ModifyUser", SqlDbType.NVarChar, 128),
					new SqlParameter("@Remark", SqlDbType.NVarChar, -1)
					};
				SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
			}
			
			parms[0].Value = tr_OuterOrgCourse.OuterOrgCourseID;
			parms[1].Value = tr_OuterOrgCourse.OuterOrgID;
			if (tr_OuterOrgCourse.CourseCode!= null){ parms[2].Value = tr_OuterOrgCourse.CourseCode; } else { parms[2].Value = DBNull.Value; }
			if (tr_OuterOrgCourse.CourseName!= null){ parms[3].Value = tr_OuterOrgCourse.CourseName; } else { parms[3].Value = DBNull.Value; }
			parms[4].Value = tr_OuterOrgCourse.CourseTypeID;
			if (tr_OuterOrgCourse.ThumbnailURL!= null){ parms[5].Value = tr_OuterOrgCourse.ThumbnailURL; } else { parms[5].Value = DBNull.Value; }
			if (tr_OuterOrgCourse.AddrURL!= null){ parms[6].Value = tr_OuterOrgCourse.AddrURL; } else { parms[6].Value = DBNull.Value; }
			if (tr_OuterOrgCourse.ForObject!= null){ parms[7].Value = tr_OuterOrgCourse.ForObject; } else { parms[7].Value = DBNull.Value; }
			if (tr_OuterOrgCourse.CourseIntroduction!= null){ parms[8].Value = tr_OuterOrgCourse.CourseIntroduction; } else { parms[8].Value = DBNull.Value; }
			if (tr_OuterOrgCourse.CourseOutline!= null){ parms[9].Value = tr_OuterOrgCourse.CourseOutline; } else { parms[9].Value = DBNull.Value; }
			parms[10].Value = tr_OuterOrgCourse.CreateTime;
			parms[11].Value = tr_OuterOrgCourse.CreateUserID;
			if (tr_OuterOrgCourse.CreateUser!= null){ parms[12].Value = tr_OuterOrgCourse.CreateUser; } else { parms[12].Value = DBNull.Value; }
			parms[13].Value = tr_OuterOrgCourse.ModifyTime;
			if (tr_OuterOrgCourse.ModifyUser!= null){ parms[14].Value = tr_OuterOrgCourse.ModifyUser; } else { parms[14].Value = DBNull.Value; }
			if (tr_OuterOrgCourse.Remark!= null){ parms[15].Value = tr_OuterOrgCourse.Remark; } else { parms[15].Value = DBNull.Value; }
			#endregion
			SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
			
		}
		
		/// <summary>
		/// 删除
		/// </summary>
		public void Remove(Guid outerOrgCourseID)
		{
			string commandName = "dbo.Pr_Tr_OuterOrgCourse_Delete";
			#region Parameters
			SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
			if (parms == null)
			{
				parms = new SqlParameter[] {
					new SqlParameter("@OuterOrgCourseID", SqlDbType.UniqueIdentifier)
				};
				SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
			}
			
			parms[0].Value = outerOrgCourseID;
			
			#endregion
			SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
		}
		
		/// <summary>
		/// 保存
		/// </summary>
		public void Save(Tr_OuterOrgCourse tr_OuterOrgCourse)
		{
			string commandName = "dbo.Pr_Tr_OuterOrgCourse_Update";
			#region Parameters
			SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
			if (parms == null)
			{
				parms = new SqlParameter[] {
					new SqlParameter("@OuterOrgCourseID", SqlDbType.UniqueIdentifier),
					new SqlParameter("@OuterOrgID", SqlDbType.UniqueIdentifier),
					new SqlParameter("@CourseCode", SqlDbType.NVarChar, 100),
					new SqlParameter("@CourseName", SqlDbType.NVarChar, 200),
					new SqlParameter("@CourseTypeID", SqlDbType.Int),
					new SqlParameter("@ThumbnailURL", SqlDbType.NVarChar, 256),
					new SqlParameter("@AddrURL", SqlDbType.NVarChar, 256),
					new SqlParameter("@ForObject", SqlDbType.NVarChar, -1),
					new SqlParameter("@CourseIntroduction", SqlDbType.NVarChar, -1),
					new SqlParameter("@CourseOutline", SqlDbType.NVarChar, -1),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@CreateUserID", SqlDbType.Int),
					new SqlParameter("@CreateUser", SqlDbType.NVarChar, 128),
					new SqlParameter("@ModifyTime", SqlDbType.DateTime),
					new SqlParameter("@ModifyUser", SqlDbType.NVarChar, 128),
					new SqlParameter("@Remark", SqlDbType.NVarChar, -1)
				};
				SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
			}
			
			parms[0].Value = tr_OuterOrgCourse.OuterOrgCourseID;
			parms[1].Value = tr_OuterOrgCourse.OuterOrgID;
			if (tr_OuterOrgCourse.CourseCode!= null){ parms[2].Value = tr_OuterOrgCourse.CourseCode; } else { parms[2].Value = DBNull.Value; }
			if (tr_OuterOrgCourse.CourseName!= null){ parms[3].Value = tr_OuterOrgCourse.CourseName; } else { parms[3].Value = DBNull.Value; }
			parms[4].Value = tr_OuterOrgCourse.CourseTypeID;
			if (tr_OuterOrgCourse.ThumbnailURL!= null){ parms[5].Value = tr_OuterOrgCourse.ThumbnailURL; } else { parms[5].Value = DBNull.Value; }
			if (tr_OuterOrgCourse.AddrURL!= null){ parms[6].Value = tr_OuterOrgCourse.AddrURL; } else { parms[6].Value = DBNull.Value; }
			if (tr_OuterOrgCourse.ForObject!= null){ parms[7].Value = tr_OuterOrgCourse.ForObject; } else { parms[7].Value = DBNull.Value; }
			if (tr_OuterOrgCourse.CourseIntroduction!= null){ parms[8].Value = tr_OuterOrgCourse.CourseIntroduction; } else { parms[8].Value = DBNull.Value; }
			if (tr_OuterOrgCourse.CourseOutline!= null){ parms[9].Value = tr_OuterOrgCourse.CourseOutline; } else { parms[9].Value = DBNull.Value; }
			parms[10].Value = tr_OuterOrgCourse.CreateTime;
			parms[11].Value = tr_OuterOrgCourse.CreateUserID;
			if (tr_OuterOrgCourse.CreateUser!= null){ parms[12].Value = tr_OuterOrgCourse.CreateUser; } else { parms[12].Value = DBNull.Value; }
			parms[13].Value = tr_OuterOrgCourse.ModifyTime;
			if (tr_OuterOrgCourse.ModifyUser!= null){ parms[14].Value = tr_OuterOrgCourse.ModifyUser; } else { parms[14].Value = DBNull.Value; }
			if (tr_OuterOrgCourse.Remark!= null){ parms[15].Value = tr_OuterOrgCourse.Remark; } else { parms[15].Value = DBNull.Value; }
			#endregion
			SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
		}
		
		/// <summary>
		/// 根据标识获取对象
		/// </summary>
		public Tr_OuterOrgCourse GetById(Guid outerOrgCourseID)
		{
			Tr_OuterOrgCourse tr_OuterOrgCourse = null;
			
			string commandName = "dbo.Pr_Tr_OuterOrgCourse_GetByPK";
			#region Parameters
			SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
			if (parms == null)
			{
				parms = new SqlParameter[] {
					new SqlParameter("@OuterOrgCourseID", SqlDbType.UniqueIdentifier)
				};
				SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
			}
			
			parms[0].Value = outerOrgCourseID;
			
			#endregion
			using (SqlDataReader dataReader = SqlHelper.ExecuteReader(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms))
			{
				if (dataReader.Read())
				{
					tr_OuterOrgCourse = PopulateTr_OuterOrgCourseFromDataReader(dataReader);
				}
			}
			
			return tr_OuterOrgCourse;
		}				
		
		/// <summary>
		/// 根据参数获取对象列表（分页，可排序）
		/// </summary>
		public DataTable GetPagedList(int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
		{			
			string commandName = "dbo.Pr_Tr_OuterOrgCourse_GetPagedList";
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
		public IList<Tr_OuterOrgCourse> GetEntityList(int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
		{			
            IList<Tr_OuterOrgCourse> list=new List<Tr_OuterOrgCourse>();
			string commandName = "dbo.Pr_Tr_OuterOrgCourse_GetPagedList";
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
					list.Add(PopulateTr_OuterOrgCourseFromDataReader(dataReader));
				}
			}			
			totalRecords = (int)parms[4].Value;
			return list;
		}
		
		/// <summary>
		/// 从DataReader中读取数据到业务对象
		/// </summary>
		private Tr_OuterOrgCourse PopulateTr_OuterOrgCourseFromDataReader(SqlDataReader reader)
		{
			Tr_OuterOrgCourse tr_OuterOrgCourse = new Tr_OuterOrgCourse();
			
			int outerOrgCourseIDIndex = reader.GetOrdinal("OuterOrgCourseID"); 
			if(!reader.IsDBNull(outerOrgCourseIDIndex))
			{
				tr_OuterOrgCourse.OuterOrgCourseID= reader.GetGuid(outerOrgCourseIDIndex);
			}
			
			int outerOrgIDIndex = reader.GetOrdinal("OuterOrgID"); 
			if(!reader.IsDBNull(outerOrgIDIndex))
			{
				tr_OuterOrgCourse.OuterOrgID= reader.GetGuid(outerOrgIDIndex);
			}
			
			int courseCodeIndex = reader.GetOrdinal("CourseCode"); 
			if(!reader.IsDBNull(courseCodeIndex))
			{
				tr_OuterOrgCourse.CourseCode= reader.GetString(courseCodeIndex);
			}
			
			int courseNameIndex = reader.GetOrdinal("CourseName"); 
			if(!reader.IsDBNull(courseNameIndex))
			{
				tr_OuterOrgCourse.CourseName= reader.GetString(courseNameIndex);
			}
			
			int courseTypeIDIndex = reader.GetOrdinal("CourseTypeID"); 
			if(!reader.IsDBNull(courseTypeIDIndex))
			{
				tr_OuterOrgCourse.CourseTypeID= reader.GetInt32(courseTypeIDIndex);
			}
			
			int thumbnailURLIndex = reader.GetOrdinal("ThumbnailURL"); 
			if(!reader.IsDBNull(thumbnailURLIndex))
			{
				tr_OuterOrgCourse.ThumbnailURL= reader.GetString(thumbnailURLIndex);
			}
			
			int addrURLIndex = reader.GetOrdinal("AddrURL"); 
			if(!reader.IsDBNull(addrURLIndex))
			{
				tr_OuterOrgCourse.AddrURL= reader.GetString(addrURLIndex);
			}
			
			int forObjectIndex = reader.GetOrdinal("ForObject"); 
			if(!reader.IsDBNull(forObjectIndex))
			{
				tr_OuterOrgCourse.ForObject= reader.GetString(forObjectIndex);
			}
			
			int courseIntroductionIndex = reader.GetOrdinal("CourseIntroduction"); 
			if(!reader.IsDBNull(courseIntroductionIndex))
			{
				tr_OuterOrgCourse.CourseIntroduction= reader.GetString(courseIntroductionIndex);
			}
			
			int courseOutlineIndex = reader.GetOrdinal("CourseOutline"); 
			if(!reader.IsDBNull(courseOutlineIndex))
			{
				tr_OuterOrgCourse.CourseOutline= reader.GetString(courseOutlineIndex);
			}
			
			int createTimeIndex = reader.GetOrdinal("CreateTime"); 
			if(!reader.IsDBNull(createTimeIndex))
			{
				tr_OuterOrgCourse.CreateTime= reader.GetDateTime(createTimeIndex);
			}
			
			int createUserIDIndex = reader.GetOrdinal("CreateUserID"); 
			if(!reader.IsDBNull(createUserIDIndex))
			{
				tr_OuterOrgCourse.CreateUserID= reader.GetInt32(createUserIDIndex);
			}
			
			int createUserIndex = reader.GetOrdinal("CreateUser"); 
			if(!reader.IsDBNull(createUserIndex))
			{
				tr_OuterOrgCourse.CreateUser= reader.GetString(createUserIndex);
			}
			
			int modifyTimeIndex = reader.GetOrdinal("ModifyTime"); 
			if(!reader.IsDBNull(modifyTimeIndex))
			{
				tr_OuterOrgCourse.ModifyTime= reader.GetDateTime(modifyTimeIndex);
			}
			
			int modifyUserIndex = reader.GetOrdinal("ModifyUser"); 
			if(!reader.IsDBNull(modifyUserIndex))
			{
				tr_OuterOrgCourse.ModifyUser= reader.GetString(modifyUserIndex);
			}
			
			int remarkIndex = reader.GetOrdinal("Remark"); 
			if(!reader.IsDBNull(remarkIndex))
			{
				tr_OuterOrgCourse.Remark= reader.GetString(remarkIndex);
			}
			
			return tr_OuterOrgCourse; 
		}
	}
}
