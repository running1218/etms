using System;
using ETMS.Utility.Data;
using System.Data.SqlClient;
using System.Data;

using ETMS.Components.Basic.Implement.DAL.Common;

using ETMS.Components.Basic.API.Entity.TrainingItem.Course.Resources;


namespace ETMS.Components.Courseware.Implement.DAL
{
    public class Res_ItemCourse_CoursewareDataAccess
    {
        //字段等于查询条件模板
        string fieldEqualModal = "AND ([{0}].[{1}] = '{2}')";
        string sqlModal = @"select
                    Tr_ItemCourse.CourseID, Tr_ItemCourse.CourseStatus, Tr_ItemCourse.CourseBeginTime, Tr_ItemCourse.CourseEndTime
                    ,Tr_ItemCourse.TeacherID,Tr_ItemCourse.TeachModelID,Tr_ItemCourse.TrainingItemID 
                    ,Tr_ItemCourseRes.* from dbo.Tr_ItemCourseRes
                    inner join dbo.Tr_ItemCourse on Tr_ItemCourse.TrainingItemCourseID = Tr_ItemCourseRes.TrainingItemCourseID
                    where Tr_ItemCourseRes.CourseResTypeID = '1' {0}";




        /// <summary>
        /// 验证某个培训项目课程资源是否被学习或使用
        /// </summary>
        /// <param name="courseResID">培训项目课程资源ID</param>
        /// <returns></returns>
        public bool CheckResourceIsUsed(Guid courseResID)
        {
            bool isUsed = true;
            string sqlModal = @"select COUNT(*)  num from Sco_Cmi_Core a inner join Sco_e_Resource b on b.ResourceID = a.ResourceID where b.CoursewareID ='{0}'";
            string sql = string.Format(sqlModal, courseResID);
            int ret = (Int32)SqlHelper.ExecuteScalar(ConnectionString.ETMSRead, CommandType.Text, sql, null);
            if (ret < 1)
                isUsed = false;
            return isUsed;

        }




        /// <summary>
        /// 根据培训项目课程编号获取课件总数
        /// </summary>
        /// <param name="courseID">课程编号</param>
        /// <returns></returns>
        public Int32 GetItemCourseCoursewareTotal(Guid trainingItemCourseID)
        {
            string commandName = string.Format("select COUNT(*) from Tr_ItemCourseRes where TrainingItemCourseID='{0}' and CourseResTypeID={1}", trainingItemCourseID, (Int32)Basic.API.Entity.EnumResourcesType.Courseware);
            return (Int32)SqlHelper.ExecuteScalar(ConnectionString.ETMSRead, CommandType.Text, commandName, null);
        }


        public DataTable GetTrainingItemResourcesList(Guid trainingItemCourseID, int pageIndex, int pageSize, string criteria, out int totalRecords)
        {
            string orderBy = "order by Tr_ItemCourseRes.CreateTime DESC";
            criteria += string.Format(fieldEqualModal, "Tr_ItemCourseRes", "trainingItemCourseID", trainingItemCourseID);
            string sql = string.Format(sqlModal, criteria);
            return GetData.GetPagedListFromSQL(sql, pageIndex, pageSize, orderBy, out totalRecords);
        }


        /// <summary>
        /// 获取某个培训项目课程未使用的课程资源列表
        /// </summary>
        /// <param name="trainingItemCourseID">培训项目课程ID</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="criteria"></param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        public DataTable GetTrainingItemNoSelectResourcesList(Guid trainingItemCourseID, int pageIndex, int pageSize, string criteria, out int totalRecords)
        {
            //Pr_Tr_ItemCourseRes_NoSelectCourseware
            string sqlModal = @"
                    select Dic_Sys_CoursewareType.CoursewareTypeName 
                        ,Res_CourseRes.CourseResID,Res_CourseRes.CourseID, Res_CourseRes.CourseResTypeID
                        ,Res_CourseRes.ResID,Res_CourseRes.ResName
                        ,Res_CourseRes.IsUse,Res_CourseRes.ResBeginTime,Res_CourseRes.ResEndTime
                        ,Res_Courseware.CoursewareID,Res_Courseware.CoursewareTypeID,Res_Courseware.CoursewarePath,Res_Courseware.CoursewareStatus
                    from Res_Courseware
                    inner join Res_CourseRes on Res_CourseRes.ResID = Res_Courseware.CoursewareID
                    left join Dic_Sys_CoursewareType on Dic_Sys_CoursewareType.CoursewareTypeID = Res_Courseware.CoursewareTypeID
                    where Res_CourseRes.CourseResTypeID='1' {0}
                        and Res_CourseRes.ResID not in (
                        select CourseResID 
                        from Tr_ItemCourseRes 
                        where Tr_ItemCourseRes.CourseResTypeID='1'  
                            and Tr_ItemCourseRes.TrainingItemCourseID='{1}'
                        )";
            string orderBy = "order by Res_Courseware.CreateTime DESC";
            string sql = string.Format(sqlModal, criteria, trainingItemCourseID);
            return GetData.GetPagedListFromSQL(sql, pageIndex, pageSize, orderBy, out totalRecords);
        }




        /// <summary>
        /// 获取某个培训项目课程未使用的在线课程资源列表
        /// </summary>
        /// <param name="trainingItemCourseID">培训项目课程ID</param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        public DataTable GetTrainingItemNoSelectResourcesList(Guid trainingItemCourseID,  out int totalRecords)
        {
            string commandName = "dbo.Pr_Tr_ItemCourseRes_NoSelectCourseware";
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



        /// 获取某个培训项目课程已使用的在线课程资源列表
        /// </summary>
        /// <param name="trainingItemCourseID">培训项目课程ID</param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        public DataTable GetTrainingItemSelectResourcesList(Guid trainingItemCourseID, out int totalRecords)
        {
            string commandName = "dbo.Pr_Tr_ItemCourseRes_SelectCourseware";
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
        /// 修改项目课程资源排序号
        /// </summary>
        public void ItemCourseResUpdateOrderNum(Guid ItemCourseResID, int OrderNum)
        {
            string commandName = "dbo.Pr_Tr_ItemCourseRes_UpdateOrderNum";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@ItemCourseResID", SqlDbType.UniqueIdentifier),
					new SqlParameter("@OrderNum", SqlDbType.Int)
					};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
            }

            parms[0].Value = ItemCourseResID;
            parms[1].Value = OrderNum;
            #endregion
            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
        }


        /// <summary>
        /// 获取某个培训项目课程的某个在线课程资源信息
        /// </summary>
        /// <param name="trainingItemCourseID">培训项目课程ID</param>
        /// <param name="ItemCourseResID">培训项目课程资源ID</param>
        /// <returns></returns>
        public DataTable GetTrainingItemGetOneResources(Guid trainingItemCourseID, Guid ItemCourseResID)
        {
            string commandName = "dbo.Pr_Tr_ItemCourseRes_GetOneCourseware";
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
        public void AddCoursewareToItemCourse(Tr_ItemCourseRes tr_ItemCourseRes)
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
        public void RemoveCoursewareFromItemCourse(Guid itemCourseResID)
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
        public void SaveCoursewareToItemCourse(Tr_ItemCourseRes tr_ItemCourseRes)
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



    }
}
