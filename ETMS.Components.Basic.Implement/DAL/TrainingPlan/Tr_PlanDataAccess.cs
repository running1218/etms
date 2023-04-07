using System;
using System.Data;

using System.Data.SqlClient;
using ETMS.Utility.Data;


namespace ETMS.Components.Basic.Implement.DAL.TrainingPlan
{
    /// <summary>
    /// 培训计划表数据访问扩展类
    /// 黄中福2012－04－17
    /// </summary>
    public partial class Tr_PlanDataAccess
    {

        #region 业务操作方法，如：添加、修改、删除、审核等

        /// <summary>
        /// 审核某个培训计划
        /// </summary>
        /// <param name="planID">培训计划ID</param>
        /// <param name="planStatus">审核结果（20：审核通过，40：审核不通过）</param>
        /// <param name="auditUser">审核人</param>
        /// <param name="auditOpinion">审核意见</param>
        public void Tr_Plan_Audit(Guid planID, int planStatus, string auditUser, string auditOpinion)
        {
            string commandName = "dbo.Pr_Tr_Plan_Audit";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@PlanID", SqlDbType.UniqueIdentifier),
                    new SqlParameter("@PlanStatus", SqlDbType.Int),
                    new SqlParameter("@AuditUser", SqlDbType.NVarChar),
                    new SqlParameter("@AuditOpinion", SqlDbType.NVarChar)
                    };
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
            }

            parms[0].Value = planID;
            parms[1].Value = planStatus;
            parms[2].Value = auditUser;
            parms[3].Value = auditOpinion;
            #endregion
            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);

        }




        /// <summary>
        /// 取消审核某个培训计划
        /// </summary>
        /// <param name="planID">培训计划ID</param>
        /// <param name="auditUser">审核人</param>
        /// <param name="auditOpinion">审核意见</param>
        public void Tr_Plan_CancelAudit(Guid planID,  string auditUser, string auditOpinion)
        {
            string commandName = "dbo.Pr_Tr_Plan_CancelAudit";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@PlanID", SqlDbType.UniqueIdentifier),
                    new SqlParameter("@AuditUser", SqlDbType.NVarChar),
                    new SqlParameter("@AuditOpinion", SqlDbType.NVarChar)
                    };
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
            }

            parms[0].Value = planID;
            parms[1].Value = auditUser;
            parms[2].Value = auditOpinion;
            #endregion
            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);

        }



        /// <summary>
        /// 归档某个培训计划
        /// </summary>
        /// <param name="planID">培训计划ID</param>
        /// <param name="planEndModeID">归档方式（1:正常结束,2:异常结束,3:审核通过结束,4:审核不通过结束）</param>
        /// <param name="planEndReMark">归档备注</param>
        /// <param name="modifyUser">归档人</param>
        public void Tr_Plan_Achive(Guid planID, int planEndModeID, string planEndReMark, string modifyUser)
        {
            string commandName = "dbo.[Pr_Tr_Plan_Achive]";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@PlanID", SqlDbType.UniqueIdentifier),
                    new SqlParameter("@PlanEndModeID", SqlDbType.Int),
                    new SqlParameter("@PlanEndReMark", SqlDbType.NVarChar),
                    new SqlParameter("@modifyUser", SqlDbType.NVarChar)
                    };
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
            }

            parms[0].Value = planID;
            parms[1].Value = planEndModeID;
            parms[2].Value = planEndReMark;
            parms[3].Value = modifyUser;
            #endregion
            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);

        }

        #endregion

        #region 数据查询

        /// <summary>
        /// 获取某培训计划下的所有课程数量
        /// </summary>
        /// <param name="planID">培训计划ID</param>
        /// <returns></returns>
        public int GetPlanCourseTotal(Guid planID)
        {
            string commandName = string.Format("select COUNT(*)  num from Tr_PlanCourse where PlanID='{0}' ", planID);
            return (Int32)SqlHelper.ExecuteScalar(ConnectionString.ETMSRead, CommandType.Text, commandName, null);
        }


        /// <summary>
        /// 获取某个组织机构的所有培训计划列表
        /// </summary>
        /// <param name="orgID">组织机构ID</param>
        /// <param name="pageIndex">起始页</param>
        /// <param name="pageSize">每页的记录数</param>
        /// <param name="sortExpression">排序方式</param>
        /// <param name="criteria">与 AND 开头的查询条件</param>
        /// <param name="totalRecords">返回总的满足条件的记录数</param>
        /// <returns>DataTable</returns>
        public DataTable GetPlanListByOrgID(int orgID, int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {
            criteria += string.Format(" AND OrgID='{0}'", orgID.ToString());
            return GetPagedList(pageIndex, pageSize, sortExpression, criteria, out  totalRecords);
        }


        /// <summary>
        /// 获取某个组织机构的所有培训计划列表
        /// </summary>
        /// <param name="planID">培训计划ID</param>
        /// <param name="pageIndex">起始页</param>
        /// <param name="pageSize">每页的记录数</param>
        /// <param name="sortExpression">排序方式</param>
        /// <param name="criteria">与 AND 开头的查询条件</param>
        /// <param name="totalRecords">返回总的满足条件的记录数</param>
        /// <returns>DataTable</returns>
        public DataTable GetPlanCourseListByPlanID(int planID, int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {
            criteria += string.Format(" AND OrgID='{0}'", planID.ToString());
            return GetPagedList(pageIndex, pageSize, sortExpression, criteria, out  totalRecords);
        }



        #endregion






    }
}
