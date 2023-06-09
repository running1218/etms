//==================================================================================================
//Version 1.0, auto-generated.
//Generated By huangzhf.
//Date: 2012-5-6 14:53:50.
//==================================================================================================
//==================================================================================================
//This file is generated by CodeGenerator(Ver 2.1)
//==================================================================================================

using System;
using System.Collections.Generic;
using System.Data;

using System.Data.SqlClient;
using ETMS.Utility.Data;

using ETMS.Components.Point.API.Entity;

namespace ETMS.Components.Point.Implement.DAL
{
    /// <summary>
    /// 学员培训项目课程积分规则表数据访问
    /// </summary>
    public partial class Point_Student_CourseRoleDataAccess
	{
		/// <summary>
		/// 增加
		/// </summary>
		public void Add(Point_Student_CourseRole point_Student_CourseRole)
		{
			string commandName = "dbo.Pr_Point_Student_CourseRole_Insert";
			#region Parameters
			SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
			if (parms == null)
			{
				parms = new SqlParameter[] {
					new SqlParameter("@StudentCoursePointRoleID", SqlDbType.UniqueIdentifier),
					new SqlParameter("@OrgID", SqlDbType.Int),
					new SqlParameter("@CourseAttrID", SqlDbType.Int),
					new SqlParameter("@StudentCoursePointTypeID", SqlDbType.Int),
					new SqlParameter("@MinNum", SqlDbType.Int),
					new SqlParameter("@MaxNum", SqlDbType.Int),
					new SqlParameter("@GivePoints", SqlDbType.Int),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@CreateUserID", SqlDbType.Int),
					new SqlParameter("@CreateUser", SqlDbType.NVarChar, 128),
					new SqlParameter("@ModifyTime", SqlDbType.DateTime),
					new SqlParameter("@ModifyUser", SqlDbType.NVarChar, 128),
					new SqlParameter("@Remark", SqlDbType.NVarChar, -1)
					};
				SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
			}
			
			parms[0].Value = point_Student_CourseRole.StudentCoursePointRoleID;
			parms[1].Value = point_Student_CourseRole.OrgID;
			parms[2].Value = point_Student_CourseRole.CourseAttrID;
			parms[3].Value = point_Student_CourseRole.StudentCoursePointTypeID;
			parms[4].Value = point_Student_CourseRole.MinNum;
			parms[5].Value = point_Student_CourseRole.MaxNum;
			parms[6].Value = point_Student_CourseRole.GivePoints;
			parms[7].Value = point_Student_CourseRole.CreateTime;
			parms[8].Value = point_Student_CourseRole.CreateUserID;
			if (point_Student_CourseRole.CreateUser!= null){ parms[9].Value = point_Student_CourseRole.CreateUser; } else { parms[9].Value = DBNull.Value; }
			parms[10].Value = point_Student_CourseRole.ModifyTime;
			if (point_Student_CourseRole.ModifyUser!= null){ parms[11].Value = point_Student_CourseRole.ModifyUser; } else { parms[11].Value = DBNull.Value; }
			if (point_Student_CourseRole.Remark!= null){ parms[12].Value = point_Student_CourseRole.Remark; } else { parms[12].Value = DBNull.Value; }
			#endregion
			SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
			
		}
		
		/// <summary>
		/// 删除
		/// </summary>
		public void Remove(Guid studentCoursePointRoleID)
		{
			string commandName = "dbo.Pr_Point_Student_CourseRole_Delete";
			#region Parameters
			SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
			if (parms == null)
			{
				parms = new SqlParameter[] {
					new SqlParameter("@StudentCoursePointRoleID", SqlDbType.UniqueIdentifier)
				};
				SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
			}
			
			parms[0].Value = studentCoursePointRoleID;
			
			#endregion
			SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
		}
		
		/// <summary>
		/// 保存
		/// </summary>
		public void Save(Point_Student_CourseRole point_Student_CourseRole)
		{
			string commandName = "dbo.Pr_Point_Student_CourseRole_Update";
			#region Parameters
			SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
			if (parms == null)
			{
				parms = new SqlParameter[] {
					new SqlParameter("@StudentCoursePointRoleID", SqlDbType.UniqueIdentifier),
					new SqlParameter("@OrgID", SqlDbType.Int),
					new SqlParameter("@CourseAttrID", SqlDbType.Int),
					new SqlParameter("@StudentCoursePointTypeID", SqlDbType.Int),
					new SqlParameter("@MinNum", SqlDbType.Int),
					new SqlParameter("@MaxNum", SqlDbType.Int),
					new SqlParameter("@GivePoints", SqlDbType.Int),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@CreateUserID", SqlDbType.Int),
					new SqlParameter("@CreateUser", SqlDbType.NVarChar, 128),
					new SqlParameter("@ModifyTime", SqlDbType.DateTime),
					new SqlParameter("@ModifyUser", SqlDbType.NVarChar, 128),
					new SqlParameter("@Remark", SqlDbType.NVarChar, -1)
				};
				SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
			}
			
			parms[0].Value = point_Student_CourseRole.StudentCoursePointRoleID;
			parms[1].Value = point_Student_CourseRole.OrgID;
			parms[2].Value = point_Student_CourseRole.CourseAttrID;
			parms[3].Value = point_Student_CourseRole.StudentCoursePointTypeID;
			parms[4].Value = point_Student_CourseRole.MinNum;
			parms[5].Value = point_Student_CourseRole.MaxNum;
			parms[6].Value = point_Student_CourseRole.GivePoints;
			parms[7].Value = point_Student_CourseRole.CreateTime;
			parms[8].Value = point_Student_CourseRole.CreateUserID;
			if (point_Student_CourseRole.CreateUser!= null){ parms[9].Value = point_Student_CourseRole.CreateUser; } else { parms[9].Value = DBNull.Value; }
			parms[10].Value = point_Student_CourseRole.ModifyTime;
			if (point_Student_CourseRole.ModifyUser!= null){ parms[11].Value = point_Student_CourseRole.ModifyUser; } else { parms[11].Value = DBNull.Value; }
			if (point_Student_CourseRole.Remark!= null){ parms[12].Value = point_Student_CourseRole.Remark; } else { parms[12].Value = DBNull.Value; }
			#endregion
			SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
		}
		
		/// <summary>
		/// 根据标识获取对象
		/// </summary>
		public Point_Student_CourseRole GetById(Guid studentCoursePointRoleID)
		{
			Point_Student_CourseRole point_Student_CourseRole = null;
			
			string commandName = "dbo.Pr_Point_Student_CourseRole_GetByPK";
			#region Parameters
			SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
			if (parms == null)
			{
				parms = new SqlParameter[] {
					new SqlParameter("@StudentCoursePointRoleID", SqlDbType.UniqueIdentifier)
				};
				SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
			}
			
			parms[0].Value = studentCoursePointRoleID;
			
			#endregion
			using (SqlDataReader dataReader = SqlHelper.ExecuteReader(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms))
			{
				if (dataReader.Read())
				{
					point_Student_CourseRole = PopulatePoint_Student_CourseRoleFromDataReader(dataReader);
				}
			}
			
			return point_Student_CourseRole;
		}				
		
		/// <summary>
		/// 根据参数获取对象列表（分页，可排序）
		/// </summary>
		public DataTable GetPagedList(int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
		{			
			string commandName = "dbo.Pr_Point_Student_CourseRole_GetPagedList";
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
		public IList<Point_Student_CourseRole> GetEntityList(int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
		{			
            IList<Point_Student_CourseRole> list=new List<Point_Student_CourseRole>();
			string commandName = "dbo.Pr_Point_Student_CourseRole_GetPagedList";
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
					list.Add(PopulatePoint_Student_CourseRoleFromDataReader(dataReader));
				}
			}			
			totalRecords = (int)parms[4].Value;
			return list;
		}
		
		/// <summary>
		/// 从DataReader中读取数据到业务对象
		/// </summary>
		private Point_Student_CourseRole PopulatePoint_Student_CourseRoleFromDataReader(SqlDataReader reader)
		{
			Point_Student_CourseRole point_Student_CourseRole = new Point_Student_CourseRole();
			
			int studentCoursePointRoleIDIndex = reader.GetOrdinal("StudentCoursePointRoleID"); 
			if(!reader.IsDBNull(studentCoursePointRoleIDIndex))
			{
				point_Student_CourseRole.StudentCoursePointRoleID= reader.GetGuid(studentCoursePointRoleIDIndex);
			}
			
			int orgIDIndex = reader.GetOrdinal("OrgID"); 
			if(!reader.IsDBNull(orgIDIndex))
			{
				point_Student_CourseRole.OrgID= reader.GetInt32(orgIDIndex);
			}
			
			int courseAttrIDIndex = reader.GetOrdinal("CourseAttrID"); 
			if(!reader.IsDBNull(courseAttrIDIndex))
			{
				point_Student_CourseRole.CourseAttrID= reader.GetInt32(courseAttrIDIndex);
			}
			
			int studentCoursePointTypeIDIndex = reader.GetOrdinal("StudentCoursePointTypeID"); 
			if(!reader.IsDBNull(studentCoursePointTypeIDIndex))
			{
				point_Student_CourseRole.StudentCoursePointTypeID= reader.GetInt32(studentCoursePointTypeIDIndex);
			}
			
			int minNumIndex = reader.GetOrdinal("MinNum"); 
			if(!reader.IsDBNull(minNumIndex))
			{
				point_Student_CourseRole.MinNum= reader.GetInt32(minNumIndex);
			}
			
			int maxNumIndex = reader.GetOrdinal("MaxNum"); 
			if(!reader.IsDBNull(maxNumIndex))
			{
				point_Student_CourseRole.MaxNum= reader.GetInt32(maxNumIndex);
			}
			
			int givePointsIndex = reader.GetOrdinal("GivePoints"); 
			if(!reader.IsDBNull(givePointsIndex))
			{
				point_Student_CourseRole.GivePoints= reader.GetInt32(givePointsIndex);
			}
			
			int createTimeIndex = reader.GetOrdinal("CreateTime"); 
			if(!reader.IsDBNull(createTimeIndex))
			{
				point_Student_CourseRole.CreateTime= reader.GetDateTime(createTimeIndex);
			}
			
			int createUserIDIndex = reader.GetOrdinal("CreateUserID"); 
			if(!reader.IsDBNull(createUserIDIndex))
			{
				point_Student_CourseRole.CreateUserID= reader.GetInt32(createUserIDIndex);
			}
			
			int createUserIndex = reader.GetOrdinal("CreateUser"); 
			if(!reader.IsDBNull(createUserIndex))
			{
				point_Student_CourseRole.CreateUser= reader.GetString(createUserIndex);
			}
			
			int modifyTimeIndex = reader.GetOrdinal("ModifyTime"); 
			if(!reader.IsDBNull(modifyTimeIndex))
			{
				point_Student_CourseRole.ModifyTime= reader.GetDateTime(modifyTimeIndex);
			}
			
			int modifyUserIndex = reader.GetOrdinal("ModifyUser"); 
			if(!reader.IsDBNull(modifyUserIndex))
			{
				point_Student_CourseRole.ModifyUser= reader.GetString(modifyUserIndex);
			}
			
			int remarkIndex = reader.GetOrdinal("Remark"); 
			if(!reader.IsDBNull(remarkIndex))
			{
				point_Student_CourseRole.Remark= reader.GetString(remarkIndex);
			}
			
			return point_Student_CourseRole; 
		}
	}
}
