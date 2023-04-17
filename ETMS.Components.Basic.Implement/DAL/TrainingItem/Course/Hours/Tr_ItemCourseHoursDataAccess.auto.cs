//==================================================================================================
//Version 1.0, auto-generated.
//Generated By huangzhf.
//Date: 2012-5-23 16:14:32.
//==================================================================================================
//==================================================================================================
//This file is generated by CodeGenerator(Ver 2.1)
//==================================================================================================

using System;
using System.Collections.Generic;
using System.Data;

using System.Data.SqlClient;
using ETMS.Utility.Data;

using ETMS.Components.Basic.API.Entity.TrainingItem.Course.Hours;

namespace ETMS.Components.Basic.Implement.DAL.TrainingItem.Course.Hours
{
    /// <summary>
    /// 培训项目课程课时安排表数据访问
    /// </summary>
    public partial class Tr_ItemCourseHoursDataAccess
	{
		/// <summary>
		/// 增加
		/// </summary>
		public void Add(Tr_ItemCourseHours tr_ItemCourseHours)
		{
			string commandName = "dbo.Pr_Tr_ItemCourseHours_Insert";
			#region Parameters
			SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
			if (parms == null)
			{
				parms = new SqlParameter[] {
					new SqlParameter("@ItemCourseHoursID", SqlDbType.UniqueIdentifier),
					new SqlParameter("@TrainingItemCourseID", SqlDbType.UniqueIdentifier),
					new SqlParameter("@ClassRoomID", SqlDbType.UniqueIdentifier),
					new SqlParameter("@CourseHoursStatusID", SqlDbType.Int),
					new SqlParameter("@TrainingTimeDescID", SqlDbType.Int),
					new SqlParameter("@TrainingDate", SqlDbType.DateTime),
					new SqlParameter("@TrainingBeginTime", SqlDbType.DateTime),
					new SqlParameter("@TrainingEndTime", SqlDbType.DateTime),
					new SqlParameter("@CourseFee", SqlDbType.Decimal, 9, ParameterDirection.Input, true, 15, 2, String.Empty, DataRowVersion.Default, null),
					new SqlParameter("@CourseHours", SqlDbType.Decimal, 5, ParameterDirection.Input, true, 8, 2, String.Empty, DataRowVersion.Default, null),
					new SqlParameter("@CourseHoursDesc", SqlDbType.NVarChar, -1),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@CreateUserID", SqlDbType.Int),
					new SqlParameter("@CreateUser", SqlDbType.NVarChar, 128),
					new SqlParameter("@ModifyTime", SqlDbType.DateTime),
					new SqlParameter("@ModifyUser", SqlDbType.NVarChar, 128),
					new SqlParameter("@Remark", SqlDbType.NVarChar, -1),
					new SqlParameter("@DelFlag", SqlDbType.Bit),
					new SqlParameter("@RealCourseHours", SqlDbType.Decimal, 5, ParameterDirection.Input, true, 8, 2, String.Empty, DataRowVersion.Default, null),
					new SqlParameter("@RealCourseFee", SqlDbType.Decimal, 9, ParameterDirection.Input, true, 15, 2, String.Empty, DataRowVersion.Default, null),
					new SqlParameter("@TeacherID", SqlDbType.Int),
					new SqlParameter("@PayStatus", SqlDbType.Int),
					new SqlParameter("@CourseHoursStatusDesc", SqlDbType.NVarChar, -1)
					};
				SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
			}
			
			parms[0].Value = tr_ItemCourseHours.ItemCourseHoursID;
			parms[1].Value = tr_ItemCourseHours.TrainingItemCourseID;
			parms[2].Value = tr_ItemCourseHours.ClassRoomID;
			parms[3].Value = tr_ItemCourseHours.CourseHoursStatusID;
			parms[4].Value = tr_ItemCourseHours.TrainingTimeDescID;
			parms[5].Value = tr_ItemCourseHours.TrainingDate;
			parms[6].Value = tr_ItemCourseHours.TrainingBeginTime;
			parms[7].Value = tr_ItemCourseHours.TrainingEndTime;
			parms[8].Value = tr_ItemCourseHours.CourseFee;
			parms[9].Value = tr_ItemCourseHours.CourseHours;
			if (tr_ItemCourseHours.CourseHoursDesc!= null){ parms[10].Value = tr_ItemCourseHours.CourseHoursDesc; } else { parms[10].Value = DBNull.Value; }
			parms[11].Value = tr_ItemCourseHours.CreateTime;
			parms[12].Value = tr_ItemCourseHours.CreateUserID;
			if (tr_ItemCourseHours.CreateUser!= null){ parms[13].Value = tr_ItemCourseHours.CreateUser; } else { parms[13].Value = DBNull.Value; }
			parms[14].Value = tr_ItemCourseHours.ModifyTime;
			if (tr_ItemCourseHours.ModifyUser!= null){ parms[15].Value = tr_ItemCourseHours.ModifyUser; } else { parms[15].Value = DBNull.Value; }
			if (tr_ItemCourseHours.Remark!= null){ parms[16].Value = tr_ItemCourseHours.Remark; } else { parms[16].Value = DBNull.Value; }
			parms[17].Value = tr_ItemCourseHours.DelFlag;
			parms[18].Value = tr_ItemCourseHours.RealCourseHours;
			parms[19].Value = tr_ItemCourseHours.RealCourseFee;
			parms[20].Value = tr_ItemCourseHours.TeacherID;
			parms[21].Value = tr_ItemCourseHours.PayStatus;
			if (tr_ItemCourseHours.CourseHoursStatusDesc!= null){ parms[22].Value = tr_ItemCourseHours.CourseHoursStatusDesc; } else { parms[22].Value = DBNull.Value; }
			#endregion
			SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
			
		}
		
		/// <summary>
		/// 删除
		/// </summary>
		public void Remove(Guid itemCourseHoursID)
		{
			string commandName = "dbo.Pr_Tr_ItemCourseHours_Delete";
			#region Parameters
			SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
			if (parms == null)
			{
				parms = new SqlParameter[] {
					new SqlParameter("@ItemCourseHoursID", SqlDbType.UniqueIdentifier)
				};
				SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
			}
			
			parms[0].Value = itemCourseHoursID;
			
			#endregion
			SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
		}
		
		/// <summary>
		/// 保存
		/// </summary>
		public void Save(Tr_ItemCourseHours tr_ItemCourseHours)
		{
			string commandName = "dbo.Pr_Tr_ItemCourseHours_Update";
			#region Parameters
			SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
			if (parms == null)
			{
				parms = new SqlParameter[] {
					new SqlParameter("@ItemCourseHoursID", SqlDbType.UniqueIdentifier),
					new SqlParameter("@TrainingItemCourseID", SqlDbType.UniqueIdentifier),
					new SqlParameter("@ClassRoomID", SqlDbType.UniqueIdentifier),
					new SqlParameter("@CourseHoursStatusID", SqlDbType.Int),
					new SqlParameter("@TrainingTimeDescID", SqlDbType.Int),
					new SqlParameter("@TrainingDate", SqlDbType.DateTime),
					new SqlParameter("@TrainingBeginTime", SqlDbType.DateTime),
					new SqlParameter("@TrainingEndTime", SqlDbType.DateTime),
					new SqlParameter("@CourseFee", SqlDbType.Decimal, 9, ParameterDirection.Input, true, 15, 2, String.Empty, DataRowVersion.Default, null),
					new SqlParameter("@CourseHours", SqlDbType.Decimal, 5, ParameterDirection.Input, true, 8, 2, String.Empty, DataRowVersion.Default, null),
					new SqlParameter("@CourseHoursDesc", SqlDbType.NVarChar, -1),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@CreateUserID", SqlDbType.Int),
					new SqlParameter("@CreateUser", SqlDbType.NVarChar, 128),
					new SqlParameter("@ModifyTime", SqlDbType.DateTime),
					new SqlParameter("@ModifyUser", SqlDbType.NVarChar, 128),
					new SqlParameter("@Remark", SqlDbType.NVarChar, -1),
					new SqlParameter("@DelFlag", SqlDbType.Bit),
					new SqlParameter("@RealCourseHours", SqlDbType.Decimal, 5, ParameterDirection.Input, true, 8, 2, String.Empty, DataRowVersion.Default, null),
					new SqlParameter("@RealCourseFee", SqlDbType.Decimal, 9, ParameterDirection.Input, true, 15, 2, String.Empty, DataRowVersion.Default, null),
					new SqlParameter("@TeacherID", SqlDbType.Int),
					new SqlParameter("@PayStatus", SqlDbType.Int),
					new SqlParameter("@CourseHoursStatusDesc", SqlDbType.NVarChar, -1)
				};
				SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
			}
			
			parms[0].Value = tr_ItemCourseHours.ItemCourseHoursID;
			parms[1].Value = tr_ItemCourseHours.TrainingItemCourseID;
			parms[2].Value = tr_ItemCourseHours.ClassRoomID;
			parms[3].Value = tr_ItemCourseHours.CourseHoursStatusID;
			parms[4].Value = tr_ItemCourseHours.TrainingTimeDescID;
			parms[5].Value = tr_ItemCourseHours.TrainingDate;
			parms[6].Value = tr_ItemCourseHours.TrainingBeginTime;
			parms[7].Value = tr_ItemCourseHours.TrainingEndTime;
			parms[8].Value = tr_ItemCourseHours.CourseFee;
			parms[9].Value = tr_ItemCourseHours.CourseHours;
			if (tr_ItemCourseHours.CourseHoursDesc!= null){ parms[10].Value = tr_ItemCourseHours.CourseHoursDesc; } else { parms[10].Value = DBNull.Value; }
			parms[11].Value = tr_ItemCourseHours.CreateTime;
			parms[12].Value = tr_ItemCourseHours.CreateUserID;
			if (tr_ItemCourseHours.CreateUser!= null){ parms[13].Value = tr_ItemCourseHours.CreateUser; } else { parms[13].Value = DBNull.Value; }
			parms[14].Value = tr_ItemCourseHours.ModifyTime;
			if (tr_ItemCourseHours.ModifyUser!= null){ parms[15].Value = tr_ItemCourseHours.ModifyUser; } else { parms[15].Value = DBNull.Value; }
			if (tr_ItemCourseHours.Remark!= null){ parms[16].Value = tr_ItemCourseHours.Remark; } else { parms[16].Value = DBNull.Value; }
			parms[17].Value = tr_ItemCourseHours.DelFlag;
			parms[18].Value = tr_ItemCourseHours.RealCourseHours;
			parms[19].Value = tr_ItemCourseHours.RealCourseFee;
			parms[20].Value = tr_ItemCourseHours.TeacherID;
			parms[21].Value = tr_ItemCourseHours.PayStatus;
			if (tr_ItemCourseHours.CourseHoursStatusDesc!= null){ parms[22].Value = tr_ItemCourseHours.CourseHoursStatusDesc; } else { parms[22].Value = DBNull.Value; }
			#endregion
			SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
		}
		
		/// <summary>
		/// 根据标识获取对象
		/// </summary>
		public Tr_ItemCourseHours GetById(Guid itemCourseHoursID)
		{
			Tr_ItemCourseHours tr_ItemCourseHours = null;
			
			string commandName = "dbo.Pr_Tr_ItemCourseHours_GetByPK";
			#region Parameters
			SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
			if (parms == null)
			{
				parms = new SqlParameter[] {
					new SqlParameter("@ItemCourseHoursID", SqlDbType.UniqueIdentifier)
				};
				SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
			}
			
			parms[0].Value = itemCourseHoursID;
			
			#endregion
			using (SqlDataReader dataReader = SqlHelper.ExecuteReader(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms))
			{
				if (dataReader.Read())
				{
					tr_ItemCourseHours = PopulateTr_ItemCourseHoursFromDataReader(dataReader);
				}
			}
			
			return tr_ItemCourseHours;
		}				
		
		/// <summary>
		/// 根据参数获取对象列表（分页，可排序）
		/// </summary>
		public DataTable GetPagedList(int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
		{			
			string commandName = "dbo.Pr_Tr_ItemCourseHours_GetPagedList";
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
		public IList<Tr_ItemCourseHours> GetEntityList(int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
		{			
            IList<Tr_ItemCourseHours> list=new List<Tr_ItemCourseHours>();
			string commandName = "dbo.Pr_Tr_ItemCourseHours_GetPagedList";
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
					list.Add(PopulateTr_ItemCourseHoursFromDataReader(dataReader));
				}
			}			
			totalRecords = (int)parms[4].Value;
			return list;
		}
		
		/// <summary>
		/// 从DataReader中读取数据到业务对象
		/// </summary>
		private Tr_ItemCourseHours PopulateTr_ItemCourseHoursFromDataReader(SqlDataReader reader)
		{
			Tr_ItemCourseHours tr_ItemCourseHours = new Tr_ItemCourseHours();
			
			int itemCourseHoursIDIndex = reader.GetOrdinal("ItemCourseHoursID"); 
			if(!reader.IsDBNull(itemCourseHoursIDIndex))
			{
				tr_ItemCourseHours.ItemCourseHoursID= reader.GetGuid(itemCourseHoursIDIndex);
			}
			
			int trainingItemCourseIDIndex = reader.GetOrdinal("TrainingItemCourseID"); 
			if(!reader.IsDBNull(trainingItemCourseIDIndex))
			{
				tr_ItemCourseHours.TrainingItemCourseID= reader.GetGuid(trainingItemCourseIDIndex);
			}
			
			int classRoomIDIndex = reader.GetOrdinal("ClassRoomID"); 
			if(!reader.IsDBNull(classRoomIDIndex))
			{
				tr_ItemCourseHours.ClassRoomID= reader.GetGuid(classRoomIDIndex);
			}
			
			int courseHoursStatusIDIndex = reader.GetOrdinal("CourseHoursStatusID"); 
			if(!reader.IsDBNull(courseHoursStatusIDIndex))
			{
				tr_ItemCourseHours.CourseHoursStatusID= reader.GetInt32(courseHoursStatusIDIndex);
			}
			
			int trainingTimeDescIDIndex = reader.GetOrdinal("TrainingTimeDescID"); 
			if(!reader.IsDBNull(trainingTimeDescIDIndex))
			{
				tr_ItemCourseHours.TrainingTimeDescID= reader.GetInt32(trainingTimeDescIDIndex);
			}
			
			int trainingDateIndex = reader.GetOrdinal("TrainingDate"); 
			if(!reader.IsDBNull(trainingDateIndex))
			{
				tr_ItemCourseHours.TrainingDate= reader.GetDateTime(trainingDateIndex);
			}
			
			int trainingBeginTimeIndex = reader.GetOrdinal("TrainingBeginTime"); 
			if(!reader.IsDBNull(trainingBeginTimeIndex))
			{
				tr_ItemCourseHours.TrainingBeginTime= reader.GetDateTime(trainingBeginTimeIndex);
			}
			
			int trainingEndTimeIndex = reader.GetOrdinal("TrainingEndTime"); 
			if(!reader.IsDBNull(trainingEndTimeIndex))
			{
				tr_ItemCourseHours.TrainingEndTime= reader.GetDateTime(trainingEndTimeIndex);
			}
			
			int courseFeeIndex = reader.GetOrdinal("CourseFee"); 
			if(!reader.IsDBNull(courseFeeIndex))
			{
				tr_ItemCourseHours.CourseFee= reader.GetDecimal(courseFeeIndex);
			}
			
			int courseHoursIndex = reader.GetOrdinal("CourseHours"); 
			if(!reader.IsDBNull(courseHoursIndex))
			{
				tr_ItemCourseHours.CourseHours= reader.GetDecimal(courseHoursIndex);
			}
			
			int courseHoursDescIndex = reader.GetOrdinal("CourseHoursDesc"); 
			if(!reader.IsDBNull(courseHoursDescIndex))
			{
				tr_ItemCourseHours.CourseHoursDesc= reader.GetString(courseHoursDescIndex);
			}
			
			int createTimeIndex = reader.GetOrdinal("CreateTime"); 
			if(!reader.IsDBNull(createTimeIndex))
			{
				tr_ItemCourseHours.CreateTime= reader.GetDateTime(createTimeIndex);
			}
			
			int createUserIDIndex = reader.GetOrdinal("CreateUserID"); 
			if(!reader.IsDBNull(createUserIDIndex))
			{
				tr_ItemCourseHours.CreateUserID= reader.GetInt32(createUserIDIndex);
			}
			
			int createUserIndex = reader.GetOrdinal("CreateUser"); 
			if(!reader.IsDBNull(createUserIndex))
			{
				tr_ItemCourseHours.CreateUser= reader.GetString(createUserIndex);
			}
			
			int modifyTimeIndex = reader.GetOrdinal("ModifyTime"); 
			if(!reader.IsDBNull(modifyTimeIndex))
			{
				tr_ItemCourseHours.ModifyTime= reader.GetDateTime(modifyTimeIndex);
			}
			
			int modifyUserIndex = reader.GetOrdinal("ModifyUser"); 
			if(!reader.IsDBNull(modifyUserIndex))
			{
				tr_ItemCourseHours.ModifyUser= reader.GetString(modifyUserIndex);
			}
			
			int remarkIndex = reader.GetOrdinal("Remark"); 
			if(!reader.IsDBNull(remarkIndex))
			{
				tr_ItemCourseHours.Remark= reader.GetString(remarkIndex);
			}
			
			int delFlagIndex = reader.GetOrdinal("DelFlag"); 
			if(!reader.IsDBNull(delFlagIndex))
			{
				tr_ItemCourseHours.DelFlag= reader.GetBoolean(delFlagIndex);
			}
			
			int realCourseHoursIndex = reader.GetOrdinal("RealCourseHours"); 
			if(!reader.IsDBNull(realCourseHoursIndex))
			{
				tr_ItemCourseHours.RealCourseHours= reader.GetDecimal(realCourseHoursIndex);
			}
			
			int realCourseFeeIndex = reader.GetOrdinal("RealCourseFee"); 
			if(!reader.IsDBNull(realCourseFeeIndex))
			{
				tr_ItemCourseHours.RealCourseFee= reader.GetDecimal(realCourseFeeIndex);
			}
			
			int teacherIDIndex = reader.GetOrdinal("TeacherID"); 
			if(!reader.IsDBNull(teacherIDIndex))
			{
				tr_ItemCourseHours.TeacherID= reader.GetInt32(teacherIDIndex);
			}
			
			int payStatusIndex = reader.GetOrdinal("PayStatus"); 
			if(!reader.IsDBNull(payStatusIndex))
			{
				tr_ItemCourseHours.PayStatus= reader.GetInt32(payStatusIndex);
			}
			
			int courseHoursStatusDescIndex = reader.GetOrdinal("CourseHoursStatusDesc"); 
			if(!reader.IsDBNull(courseHoursStatusDescIndex))
			{
				tr_ItemCourseHours.CourseHoursStatusDesc= reader.GetString(courseHoursStatusDescIndex);
			}
			
			return tr_ItemCourseHours; 
		}
	}
}
