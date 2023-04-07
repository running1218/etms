using System;
using System.Data.SqlClient;
using ETMS.Utility.Data;
using System.Data;

namespace ETMS.Components.Basic.Implement.DAL.Course.Teacher
{
    public partial class Res_TeacherCourseDataAccess
    {
        /// <summary>
        /// 删除
        /// </summary>
        public void Remove(int teacherID, Guid courseID)
        {
            string commandName = "dbo.Pr_Res_TeacherCourse_Delete";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@TeacherID", SqlDbType.Int),
                    new SqlParameter("@CourseID", SqlDbType.UniqueIdentifier)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
            }

            parms[0].Value = teacherID;
            parms[1].Value = courseID;

            #endregion
            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
        }

        /// <summary>
        /// 获取讲师教授课程列表
        /// </summary>
        /// <param name="teacherID"></param>
        /// <returns></returns>
        public DataTable GetTeacherTeachCourse(int teacherID)
        {
            string commandName = "dbo.Pr_Res_TeacherCourse_GetTeacherTeachCourseList";
			#region Parameters
			SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
			if (parms == null)
			{
				parms = new SqlParameter[] {
					new SqlParameter("@TeacherID", SqlDbType.Int)					
				};
				SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
			}

            parms[0].Value = teacherID;

			#endregion
			DataTable dt=SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];
			return dt;
		}

        /// <summary>
        /// 获取有效状态的课程讲师
        /// </summary>
        /// <param name="organizationID"></param>
        /// <returns></returns>
        public DataTable GetTeachersByCourseID(Guid courseID)
        {
            string commandName = "[Pr_Site_GetTeacherByCourseID]";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@CourseID", SqlDbType.UniqueIdentifier),
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = courseID;
            #endregion
            return SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];
        }
    }
}
