using System;
using ETMS.Utility.Data;
using System.Data.SqlClient;
using System.Data;

using ETMS.Components.Basic.API.Entity.TrainingItem.Course.Resources;


namespace ETMS.Components.ExOnlineJob.Implement.DAL
{
    public class Res_ItemCourse_OnLineJobDataAccess
    {
        /// <summary>
        /// 验证某个培训项目课程资源是否被学习或使用
        /// </summary>
        /// <param name="courseResID">培训项目课程资源ID</param>
        /// <returns></returns>
        public bool CheckResourceIsUsed(Guid courseResID)
        {
            bool isUsed = true;
            string sqlModal = @"select COUNT(*)  num from Ex_StudentOnlineJob a where a.OnlineJobID ='{0}'";
            string sql = string.Format(sqlModal, courseResID);
            int ret = (Int32)SqlHelper.ExecuteScalar(ConnectionString.ETMSRead, CommandType.Text, sql, null);
            if (ret < 1)
                isUsed = false;
            return isUsed;

        }




        /// <summary>
        /// 根据培训项目课程ID获取其在线作业总数
        /// </summary>
        /// <param name="trainingItemCourseID">培训项目课程ID</param>
        /// <returns></returns>
        public Int32 GetItemCourseOnLineJobTotal(Guid trainingItemCourseID)
        {
            string commandName = string.Format("select COUNT(*) from Tr_ItemCourseRes where TrainingItemCourseID='{0}' and CourseResTypeID={1}", trainingItemCourseID, (Int32)Basic.API.Entity.EnumResourcesType.OnLineJob);
            return (Int32)SqlHelper.ExecuteScalar(ConnectionString.ETMSRead, CommandType.Text, commandName, null);
        }



        /// <summary>
        /// 获取某个培训项目课程未使用的在线作业资源列表
        /// </summary>
        /// <param name="trainingItemCourseID">培训项目课程ID</param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        public DataTable GetTrainingItemNoSelectResourcesList(Guid trainingItemCourseID, out int totalRecords)
        {
            string commandName = "dbo.Pr_Tr_ItemCourseRes_NoSelectOnLineJob";
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@TrainingItemCourseID", SqlDbType.UniqueIdentifier),
                    new SqlParameter("@ReturnValue", SqlDbType.Int, 4, ParameterDirection.ReturnValue, false, 0, 0, String.Empty, DataRowVersion.Default, null)
                };
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }
            parms[0].Value = trainingItemCourseID;
            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];

            totalRecords = dt.Rows.Count;
            return dt;
        }



        /// <summary>
        /// 获取某个培训项目课程已使用的在线作业资源列表
        /// </summary>
        /// <param name="trainingItemCourseID">培训项目课程ID</param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        public DataTable GetTrainingItemSelectResourcesList(Guid trainingItemCourseID, out int totalRecords)
        {
            string commandName = "dbo.Pr_Tr_ItemCourseRes_SelectOnLineJob";
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@TrainingItemCourseID", SqlDbType.UniqueIdentifier),
                    new SqlParameter("@ReturnValue", SqlDbType.Int, 4, ParameterDirection.ReturnValue, false, 0, 0, String.Empty, DataRowVersion.Default, null)
                };
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }
            parms[0].Value = trainingItemCourseID;
            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];

            totalRecords = dt.Rows.Count;
            return dt;
        }




        /// <summary>
        /// 获取某个培训项目课程的某个在线作业资源信息
        /// </summary>
        /// <param name="trainingItemCourseID">培训项目课程ID</param>
        /// <param name="ItemCourseResID">培训项目课程资源ID</param>
        /// <returns></returns>
        public DataTable GetTrainingItemGetOneResources(Guid trainingItemCourseID, Guid ItemCourseResID)
        {
            string commandName = "dbo.Pr_Tr_ItemCourseRes_GetOneOnLineJob";
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@TrainingItemCourseID", SqlDbType.UniqueIdentifier),
                    new SqlParameter("@ItemCourseResID", SqlDbType.UniqueIdentifier)
                };
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }
            parms[0].Value = trainingItemCourseID;
            parms[1].Value = ItemCourseResID;

            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];
            return dt;
        }




        /// <summary>
        /// 增加
        /// </summary>
        public void AddResourceToItemCourse(Tr_ItemCourseRes tr_ItemCourseRes)
        {
            string commandName = "dbo.Pr_Tr_ItemCourseRes_Insert";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@ItemCourseResID", SqlDbType.UniqueIdentifier),
					new SqlParameter("@TrainingItemCourseID", SqlDbType.UniqueIdentifier),
					new SqlParameter("@CourseResTypeID", SqlDbType.Int),
					new SqlParameter("@CourseResID", SqlDbType.NVarChar, 100),
					new SqlParameter("@ResName", SqlDbType.NVarChar, 200),
					new SqlParameter("@IsUse", SqlDbType.Int),
					new SqlParameter("@ResBeginTime", SqlDbType.DateTime),
					new SqlParameter("@ResEndTime", SqlDbType.DateTime),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@CreateUser", SqlDbType.NVarChar, 128),
					new SqlParameter("@CreateUserID", SqlDbType.Int)
					};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
            }

            parms[0].Value = tr_ItemCourseRes.ItemCourseResID;
            parms[1].Value = tr_ItemCourseRes.TrainingItemCourseID;
            parms[2].Value = tr_ItemCourseRes.CourseResTypeID;
            if (tr_ItemCourseRes.CourseResID != null) { parms[3].Value = tr_ItemCourseRes.CourseResID; } else { parms[3].Value = DBNull.Value; }
            if (tr_ItemCourseRes.ResName != null) { parms[4].Value = tr_ItemCourseRes.ResName; } else { parms[4].Value = DBNull.Value; }
            parms[5].Value = tr_ItemCourseRes.IsUse;
            parms[6].Value = tr_ItemCourseRes.ResBeginTime;
            parms[7].Value = tr_ItemCourseRes.ResEndTime;
            parms[8].Value = tr_ItemCourseRes.CreateTime;
            if (tr_ItemCourseRes.CreateUser != null) { parms[9].Value = tr_ItemCourseRes.CreateUser; } else { parms[9].Value = DBNull.Value; }
            parms[10].Value = tr_ItemCourseRes.CreateUserID;
            #endregion
            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);

        }


        /// <summary>
        /// 删除
        /// </summary>
        public void RemoveResourceFromItemCourse(Guid itemCourseResID)
        {
            string commandName = "dbo.Pr_Tr_ItemCourseRes_Delete";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@ItemCourseResID", SqlDbType.UniqueIdentifier)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
            }

            parms[0].Value = itemCourseResID;

            #endregion
            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
        }

        /// <summary>
        /// 保存
        /// </summary>
        public void SaveResourceToItemCourse(Tr_ItemCourseRes tr_ItemCourseRes)
        {
            string commandName = "dbo.Pr_Tr_ItemCourseRes_Update";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@ItemCourseResID", SqlDbType.UniqueIdentifier),
					new SqlParameter("@TrainingItemCourseID", SqlDbType.UniqueIdentifier),
					new SqlParameter("@CourseResTypeID", SqlDbType.Int),
					new SqlParameter("@CourseResID", SqlDbType.NVarChar, 100),
					new SqlParameter("@ResName", SqlDbType.NVarChar, 200),
					new SqlParameter("@IsUse", SqlDbType.Int),
					new SqlParameter("@ResBeginTime", SqlDbType.DateTime),
					new SqlParameter("@ResEndTime", SqlDbType.DateTime),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@CreateUser", SqlDbType.NVarChar, 128),
					new SqlParameter("@CreateUserID", SqlDbType.Int)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
            }

            parms[0].Value = tr_ItemCourseRes.ItemCourseResID;
            parms[1].Value = tr_ItemCourseRes.TrainingItemCourseID;
            parms[2].Value = tr_ItemCourseRes.CourseResTypeID;
            if (tr_ItemCourseRes.CourseResID != null) { parms[3].Value = tr_ItemCourseRes.CourseResID; } else { parms[3].Value = DBNull.Value; }
            if (tr_ItemCourseRes.ResName != null) { parms[4].Value = tr_ItemCourseRes.ResName; } else { parms[4].Value = DBNull.Value; }
            parms[5].Value = tr_ItemCourseRes.IsUse;
            parms[6].Value = tr_ItemCourseRes.ResBeginTime;
            parms[7].Value = tr_ItemCourseRes.ResEndTime;
            parms[8].Value = tr_ItemCourseRes.CreateTime;
            if (tr_ItemCourseRes.CreateUser != null) { parms[9].Value = tr_ItemCourseRes.CreateUser; } else { parms[9].Value = DBNull.Value; }
            parms[10].Value = tr_ItemCourseRes.CreateUserID;
            #endregion
            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
        }

        /// <summary>
        /// 查询课程资源时间
        /// </summary>
        /// <param name="StudentOnlineTestID"></param>
        /// <returns></returns>
        public DateTime GetResEndTime(Guid StudentOnlineTestID,Guid trainingItemCourseID)
        {
            string commandName = string.Format(@"SELECT tr.ResEndTime FROM Ex_StudentOnlineTest sot
                INNER	JOIN Ex_OnLineTest ot ON ot.OnLineTestID = sot.OnLineTestID
                INNER JOIN Tr_ItemCourseRes tr ON tr.CourseResID = ot.OnLineTestID AND tr.CourseResTypeID=5
                WHERE sot.StudentOnlineTestID = '{0}' AND tr.TrainingItemCourseID='{1}'", StudentOnlineTestID, trainingItemCourseID);
            return (DateTime)SqlHelper.ExecuteScalar(ConnectionString.ETMSWrite, CommandType.Text, commandName, null);
        }

    }
}
