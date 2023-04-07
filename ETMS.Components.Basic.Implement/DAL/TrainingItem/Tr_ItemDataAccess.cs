using System;
using System.Data;


using System.Data.SqlClient;
using ETMS.Utility.Data;

using ETMS.Components.Basic.API.Entity.TrainingItem;

namespace ETMS.Components.Basic.Implement.DAL.TrainingItem
{
    public partial class Tr_ItemDataAccess
    {


        /// <summary>
        /// 审核某个培训项目
        /// </summary>
        /// <param name="trainingItemID">培训项目ID</param>
        /// <param name="itemStatus">审核结果（20：审核通过，40：审核不通过）</param>
        /// <param name="auditUser">审核人</param>
        /// <param name="auditOpinion">审核意见</param>
        public void Tr_Item_Audit(Guid trainingItemID, int itemStatus, string auditUser, string auditOpinion)
        {
            string commandName = "dbo.Pr_Tr_Item_Audit";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@TrainingItemID", SqlDbType.UniqueIdentifier),
                    new SqlParameter("@ItemStatus", SqlDbType.Int),
                    new SqlParameter("@AuditUser", SqlDbType.NVarChar),
                    new SqlParameter("@AuditOpinion", SqlDbType.NVarChar)
                    };
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
            }

            parms[0].Value = trainingItemID;
            parms[1].Value = itemStatus;
            parms[2].Value = auditUser;
            parms[3].Value = auditOpinion;
            #endregion
            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);

        }


        /// <summary>
        /// 取消审核某个培训项目
        /// </summary>
        /// <param name="trainingItemID">培训项目ID</param>
        /// <param name="auditUser">审核人</param>
        /// <param name="auditOpinion">审核意见</param>
        public void Tr_Item_CancelAudit(Guid trainingItemID, string auditUser, string auditOpinion)
        {
            string commandName = "dbo.Pr_Tr_Item_CancelAudit";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@TrainingItemID", SqlDbType.UniqueIdentifier),
                    new SqlParameter("@AuditUser", SqlDbType.NVarChar),
                    new SqlParameter("@AuditOpinion", SqlDbType.NVarChar)
                    };
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
            }

            parms[0].Value = trainingItemID;
            parms[1].Value = auditUser;
            parms[2].Value = auditOpinion;
            #endregion
            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);

        }




        /// <summary>
        /// 发布某个培训项目
        /// </summary>
        /// <param name="trainingItemID">培训项目ID</param>
        /// <param name="isIssue">是否发布（0：不发布，1：发布）</param>
        /// <param name="issueUser">发布人</param>
        public void Tr_Item_Issue(Guid trainingItemID, int isIssue, string issueUser)
        {
            string commandName = "dbo.Pr_Tr_Item_Issue";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@TrainingItemID", SqlDbType.UniqueIdentifier),
                    new SqlParameter("@IsIssue", SqlDbType.Int),
                    new SqlParameter("@IssueUser", SqlDbType.NVarChar)
                    };
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
            }

            parms[0].Value = trainingItemID;
            parms[1].Value = isIssue;
            parms[2].Value = issueUser;
            #endregion
            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);

        }




        /// <summary>
        /// 归档某个培训项目
        /// </summary>
        /// <param name="trainingItemID">培训项目ID</param>
        /// <param name="itemEndModeID">项目归档方式（1:正常结束,2:异常结束,3:审核通过结束,4:审核不通过结束）</param>
        /// <param name="itemEndReMark">归档备注</param>
        /// <param name="modifyUser">归档人</param>
        public void Tr_Item_Achive(Guid trainingItemID, int itemEndModeID, string itemEndReMark, string modifyUser)
        {
            string commandName = "dbo.[Pr_Tr_Item_Achive]";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@TrainingItemID", SqlDbType.UniqueIdentifier),
                    new SqlParameter("@ItemEndModeID", SqlDbType.Int),
                    new SqlParameter("@ItemEndReMark", SqlDbType.NVarChar),
                    new SqlParameter("@modifyUser", SqlDbType.NVarChar)
                    };
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
            }

            parms[0].Value = trainingItemID;
            parms[1].Value = itemEndModeID;
            parms[2].Value = itemEndReMark;
            parms[3].Value = modifyUser;
            #endregion
            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);

        }

        /// <summary>
        /// 获取学员可以报名的项目列表(学员所在组织机构及其上级机构的项目）
        /// </summary>
        /// <param name="organizationID"></param>
        /// <returns></returns>
        public DataTable GetMySingnTrainingItemList(int organizationID)
        {
            string commandName = "[dbo].[Pr_Tr_Item_GetMySingnTrainingItemList]";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@OrganizationID", SqlDbType.Int)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = organizationID;
            #endregion
            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];
            return dt;
        }

        /// <summary>
        /// 获取项目到期提醒学员（发送邮件、站内信...)
        /// </summary>
        /// <param name="organizationID"></param>
        /// <returns></returns>
        public DataTable GetNoticeItemStudent(DateTime itemBeginTime)
        {
            string commandName = "[dbo].[Pr_Tr_Item_GetNoticeItemStudent]";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@ItemBeginTime", SqlDbType.DateTime)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = itemBeginTime;
            #endregion
            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];
            return dt;
        }

        /// <summary>
        /// 根据项目ID，获取项目到期提醒学员（发送邮件、站内信...)
        /// </summary>
        /// <param name="organizationID"></param>
        /// <returns></returns>
        public DataTable GetNoticeItemStudent(Guid trainingItemID)
        {
            string commandName = "[dbo].[Pr_Tr_Item_GetNoticeItemStudentByTrainingItemID]";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@TrainingItemID", SqlDbType.UniqueIdentifier)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = trainingItemID;
            #endregion
            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];
            return dt;
        }

        /// <summary>
        /// 培训项目复制
        /// </summary>
        public void ItemCopy(Tr_Item tr_Item)
        {
            string commandName = "dbo.[Pr_Tr_Item_Copy]";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@TrainingItemID", SqlDbType.UniqueIdentifier),
					new SqlParameter("@ItemCode", SqlDbType.NVarChar, 100),
					new SqlParameter("@ItemName", SqlDbType.NVarChar, 200),
					new SqlParameter("@ItemBeginTime", SqlDbType.DateTime),
					new SqlParameter("@ItemEndTime", SqlDbType.DateTime),
					new SqlParameter("@CreateUserID", SqlDbType.Int),
					new SqlParameter("@CreateUser", SqlDbType.NVarChar, 128)
					};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
            }

            parms[0].Value = tr_Item.TrainingItemID;
            if (tr_Item.ItemCode != null) { parms[1].Value = tr_Item.ItemCode; } else { parms[1].Value = DBNull.Value; }
            if (tr_Item.ItemName != null) { parms[2].Value = tr_Item.ItemName; } else { parms[2].Value = DBNull.Value; }
            parms[3].Value = tr_Item.ItemBeginTime;
            parms[4].Value = tr_Item.ItemEndTime;
            parms[5].Value = tr_Item.CreateUserID;
            if (tr_Item.CreateUser != null) { parms[6].Value = tr_Item.CreateUser; } else { parms[6].Value = DBNull.Value; }
            #endregion
            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);

        }

        /// <summary>
        /// 修改项目费用
        /// </summary>
        /// <param name="itemID"></param>
        /// <param name="BudgetFee"></param>
        public void UpdateItemMoney(string itemID, string budgetFee, int payFrom)
        {
            string commandName = "UPDATE Tr_Item SET BudgetFee='{0}',PayFrom={2},IsSettingPay=1 WHERE TrainingItemID='{1}'";
            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.Text, string.Format(commandName, budgetFee, itemID, payFrom), null);
        }

        /// <summary>
        /// 修改项目费用
        /// </summary>
        /// <param name="itemID"></param>
        /// <param name="BudgetFee"></param>
        public void UpdateItemMoney(string itemID, int payFrom)
        {
            string commandName = @"UPDATE Tr_Item SET BudgetFee=(SELECT SUM(BudgetFee) FROM dbo.Tr_ItemCourse WHERE TrainingItemID='{0}') ,PayFrom={1},IsSettingPay=1 WHERE TrainingItemID='{0}'";
            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.Text, string.Format(commandName, itemID, payFrom), null);
        }


        /// <summary>
        /// 根据机构ID获取项目列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="orgID"></param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        public DataTable GetNotPayItemList(int pageIndex, int pageSize, int orgID, out int totalRecords)
        {
            string commandName = "Pr_Tr_Item_NotPayGetPagedList";

            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@PageIndex", SqlDbType.Int),
					new SqlParameter("@PageSize", SqlDbType.Int),
                    new SqlParameter("@OrgID",SqlDbType.Int),
					new SqlParameter("@ReturnValue", SqlDbType.Int, 4, ParameterDirection.ReturnValue, false, 0, 0, String.Empty, DataRowVersion.Default, null)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }
            parms[0].Value = pageIndex;
            parms[1].Value = pageSize;
            parms[2].Value = orgID;
            #endregion
            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];
            totalRecords = (int)parms[3].Value;
            return dt;
        }
    }
}
