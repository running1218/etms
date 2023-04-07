using System;
using ETMS.Utility.Data;
using System.Data.SqlClient;
using System.Data;

using ETMS.Components.Basic.API.Entity.TrainingItem.Course.Resources;


namespace ETMS.Components.ExOnlineTest.Implement.DAL
{
    public class Res_ItemCourse_OnLineTestDataAccess
    {


        /// <summary>
        /// 验证某个培训项目课程资源是否被学习或使用
        /// </summary>
        /// <param name="courseResID">培训项目课程资源ID</param>
        /// <returns></returns>
        public bool CheckResourceIsUsed(Guid courseResID)
        {
            bool isUsed = true;
            string sqlModal = @"select COUNT(*)  num from Ex_StudentOnlineTest a where a.OnLineTestID ='{0}'";
            string sql = string.Format(sqlModal, courseResID);
            int ret = (Int32)SqlHelper.ExecuteScalar(ConnectionString.ETMSRead, CommandType.Text, sql, null);
            if (ret < 1)
                isUsed = false;
            return isUsed;

        }



        /// <summary>
        /// 根据培训项目课程ID获取其在线测试总数
        /// </summary>
        /// <param name="trainingItemCourseID">培训项目课程ID</param>
        /// <returns>返回:在线测试总数</returns>
        public Int32 GetItemCourseOnLineTestTotal(Guid trainingItemCourseID)
        {
            string commandName = string.Format("select COUNT(*) from Tr_ItemCourseRes where TrainingItemCourseID='{0}' and CourseResTypeID={1}", trainingItemCourseID, (Int32)Basic.API.Entity.EnumResourcesType.OnLineTest);
            return (Int32)SqlHelper.ExecuteScalar(ConnectionString.ETMSRead, CommandType.Text, commandName, null);
        }



        /// <summary>
        /// 获取某个培训项目课程未使用的在线测试资源列表
        /// </summary>
        /// <param name="trainingItemCourseID">培训项目课程ID</param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        public DataTable GetTrainingItemNoSelectResourcesList(Guid trainingItemCourseID,  out int totalRecords)
        {
            string commandName = "dbo.Pr_Tr_ItemCourseRes_NoSelectOnLineTest";
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
        /// 获取某个培训项目课程已使用的在线测试资源列表
        /// </summary>
        /// <param name="trainingItemCourseID">培训项目课程ID</param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        public DataTable GetTrainingItemSelectResourcesList(Guid trainingItemCourseID, out int totalRecords)
        {
            string commandName = "dbo.Pr_Tr_ItemCourseRes_SelectOnLineTest";
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
        /// 获取某个培训项目课程的某个在线测试资源信息
        /// </summary>
        /// <param name="trainingItemCourseID">培训项目课程ID</param>
        /// <param name="ItemCourseResID">培训项目课程资源ID</param>
        /// <returns></returns>
        public DataTable GetTrainingItemGetOneResources(Guid trainingItemCourseID, Guid ItemCourseResID)
        {
            string commandName = "dbo.Pr_Tr_ItemCourseRes_GetOneOnLineTest";
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
        /// 获取所有学员的在线考试异常信息
        /// </summary>
        /// <param name="pageIndex">起始页</param>
        /// <param name="pageSize">每页的记录数</param>
        /// <param name="sortExpression">排序方式</param>
        /// <param name="criteria">与 AND 开头的查询条件</param>
        /// <param name="totalRecords">返回总的满足条件的记录数</param>
        public DataTable GetExceptionOnlineTestInfo(int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {
            string commandName = "dbo.[Pr_Ex_StudentOnlineTest_GetExceptionTestInfo]";
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
        /// 删除学员的在线测试：主要是删除异常的在线测试
        /// </summary>
        /// <param name="studentOnlineTestID">要删除的学员的在线测试ID</param>
        public void RemoveStudentOnlineTest(Guid studentOnlineTestID)
        {
            string commandName = "dbo.[Pr_Ex_StudentOnlineTest_DeleteException]";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@StudentOnlineTestID", SqlDbType.UniqueIdentifier)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
            }

            parms[0].Value = studentOnlineTestID;

            #endregion
            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);

        }

        /// <summary>
        /// 根据指定条件删除学员的在线测试：主要是删除异常的在线测试
        /// </summary>
        /// <param name="deleteSQLCondition">要删除的学员的在线测试的条件</param>
        public void RemoveStudentOnlineTestBySQLCondition(string deleteSQLCondition)
        {
            string commandName = "dbo.[Pr_Ex_StudentOnlineTest_DeleteExceptionByCondition]";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@Criteria", SqlDbType.VarChar)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
            }

            parms[0].Value = deleteSQLCondition;

            #endregion
            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
        }


    }
}
