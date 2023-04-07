using System;
using System.Data;

using System.Data.SqlClient;
using ETMS.Utility.Data;

namespace ETMS.Components.Basic.Implement.DAL.TrainingItem.Course.Teacher
{
    public partial class Tr_ItemCourseTeacherDataAccess
    {

        /// <summary>
        /// 获取某培训项目课程下的讲师数量
        /// </summary>
        /// <param name="trainingItemCourseID">培训项目课程ID</param>
        /// <returns></returns>
        public int GetTeacherTotal(Guid trainingItemCourseID)
        {
            //string commandName = string.Format("select COUNT(*) from Tr_ItemCourseTeacher where TrainingItemCourseID='{0}' and IsUse='1'", trainingItemCourseID, (Int32)Basic.API.Entity.EnumResourcesType.Courseware);
            string commandName = string.Format("select COUNT(*) from Tr_ItemCourseTeacher where TrainingItemCourseID='{0}'", trainingItemCourseID, (Int32)Basic.API.Entity.EnumResourcesType.Courseware);
            return (Int32)SqlHelper.ExecuteScalar(ConnectionString.ETMSRead, CommandType.Text, commandName, null);
        }


        /// <summary>
        /// 获取培训项目课程的讲师列表
        /// </summary>
        /// <param name="trainingItemCourseID">培训项目课程ID</param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        public DataTable GetTeacherListByItemCourseID(Guid trainingItemCourseID, out int totalRecords)
        {
            string commandName = "dbo.[Pr_Tr_ItemCourseTeacher_SelectTeacher]";
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
        /// 获取培训项目课程的未选择的讲师列表
        /// </summary>
        /// <param name="trainingItemCourseID">培训项目课程ID</param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        public DataTable GetNoSelectTeacherListByItemCourseID(Guid trainingItemCourseID, out int totalRecords)
        {
            string commandName = "dbo.[Pr_Tr_ItemCourseTeacher_NoSelectTeacher]";
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
        /// 获取培训项目课程的未选择的讲师列表（如果课程不是本机构的 本机构的讲师也会查出）
        /// </summary>
        /// <param name="trainingItemCourseID">培训项目课程ID</param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        public DataTable GetNoSelectTeacherListByItemCourseIDOrgID(Guid trainingItemCourseID,int orgID, out int totalRecords)
        {
            string commandName = "dbo.[Pr_Tr_ItemCourseTeacher_NoSelectTeacherOrg]";
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@TrainingItemCourseID", SqlDbType.UniqueIdentifier),
                    new SqlParameter("@OrgID", SqlDbType.Int),
                    new SqlParameter("@ReturnValue", SqlDbType.Int, 4, ParameterDirection.ReturnValue, false, 0, 0, String.Empty, DataRowVersion.Default, null)
                };
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }
            parms[0].Value = trainingItemCourseID;
            parms[1].Value = orgID;
            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];

            totalRecords = dt.Rows.Count;
            return dt;
        }

        /// <summary>
        /// 获取培训项目课程的已选择的讲师列表（如果课程不是本机构的 本机构的讲师也会查出）
        /// </summary>
        /// <param name="trainingItemCourseID">培训项目课程ID</param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        public DataTable GetSelectTeacherListByItemCourseIDOrgID(Guid trainingItemCourseID, int orgID, out int totalRecords)
        {
            string commandName = "dbo.[Pr_Tr_ItemCourseTeacher_SelectTeacherOrg]";
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@TrainingItemCourseID", SqlDbType.UniqueIdentifier),
                    new SqlParameter("@OrgID", SqlDbType.Int),
                    new SqlParameter("@ReturnValue", SqlDbType.Int, 4, ParameterDirection.ReturnValue, false, 0, 0, String.Empty, DataRowVersion.Default, null)
                };
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }
            parms[0].Value = trainingItemCourseID;
            parms[1].Value = orgID;
            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];

            totalRecords = dt.Rows.Count;
            return dt;
        }

        /// <summary>
        /// 跟据项目ID获得项目课程选择的讲师列表
        /// </summary>
        /// <param name="TrainingItemID">培训项目ID</param>
        /// <returns></returns>
        public DataTable GetItemCourseTeacherList(Guid TrainingItemID)
        {
            string commandName = "dbo.[Pr_Tr_ItemCourseTeacher_GetByTrainingItemID]";
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@TrainingItemID", SqlDbType.UniqueIdentifier)
                };
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }
            parms[0].Value = TrainingItemID;
            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];

            return dt;
        }

    }
}
