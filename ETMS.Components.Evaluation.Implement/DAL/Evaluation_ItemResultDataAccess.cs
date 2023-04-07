using System;
using System.Data;

using System.Data.SqlClient;
using ETMS.Utility.Data;

namespace ETMS.Components.Evaluation.Implement.DAL
{
    /// <summary>
    /// 评价项记录表数据访问
    /// </summary>
    public partial class Evaluation_ItemResultDataAccess
	{

        public void AddList(int userID, string objectID, Guid plateID, string itemList, string scoreList, string remark)
        {
            string commandName = "dbo.Pr_Evaluation_ItemResult_InsertList";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@UserID", SqlDbType.Int),
                    new SqlParameter("@ObjectID", SqlDbType.NVarChar),
                    new SqlParameter("@PlateID", SqlDbType.UniqueIdentifier),
                    new SqlParameter("@ItemIDList", SqlDbType.VarChar),
                    new SqlParameter("@ScoreList", SqlDbType.VarChar),
                    new SqlParameter("@Remark", SqlDbType.VarChar)
					};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }
            parms[0].Value = userID;
            parms[1].Value = objectID;
            parms[2].Value = plateID;
            parms[3].Value = itemList;
            parms[4].Value = scoreList;
            parms[5].Value = remark;
            #endregion
            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);

        }


        /// <summary>
        /// 显示结果
        /// </summary>
        public DataTable GetResultShow(string objectID, Guid plateID)
        {
            string commandName = "dbo.Pr_Evaluation_ItemResult_Show";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@ObjectID", SqlDbType.NVarChar),
                    new SqlParameter("@PlateID", SqlDbType.UniqueIdentifier)
					
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = objectID;
            parms[1].Value = plateID;

            #endregion

            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];

            return dt;
        }

        /// <summary>
        /// 好评度
        /// </summary>
        public DataTable GetResultShowGood(string objectID, Guid plateID)
        {
            string commandName = "dbo.Pr_Evaluation_ItemResult_ShowGood";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@ObjectID", SqlDbType.NVarChar),
                    new SqlParameter("@PlateID", SqlDbType.UniqueIdentifier)
					
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = objectID;
            parms[1].Value = plateID;

            #endregion

            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];

            return dt;
        }

        public DataTable GetCourseEvaluationDetails(string objectID, Guid plateID)
        {
            string commandName = "Pr_Evaluation_ItemResult_StaticRecords";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@ObjectID", SqlDbType.NVarChar),
                    new SqlParameter("@PlateID", SqlDbType.UniqueIdentifier)
					
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = objectID;
            parms[1].Value = plateID;

            #endregion

            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];

            return dt;
        }


        /// <summary>
        /// 审核后好评度
        /// </summary>
        public DataTable GetResultShowGoodApprove(string objectID, Guid plateID)
        {
            string commandName = "dbo.[Pr_Evaluation_ItemResult_ShowGoodApprove]";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@ObjectID", SqlDbType.NVarChar),
                    new SqlParameter("@PlateID", SqlDbType.UniqueIdentifier)
					
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = objectID;
            parms[1].Value = plateID;

            #endregion

            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];

            return dt;
        }


        /// <summary>
        /// 用户客观评价平均值
        /// </summary>
        /// <param name="objectID"></param>
        /// <param name="plateID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public DataTable ItemResultByUser(string objectID, Guid plateID, int userID)
        {
            string commandName = "dbo.Pr_Evaluation_ItemResultByUser";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@ObjectID", SqlDbType.NVarChar),
                    new SqlParameter("@PlateID", SqlDbType.UniqueIdentifier),
                    new SqlParameter("@UserID", SqlDbType.Int)
					
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = objectID;
            parms[1].Value = plateID;
            parms[2].Value = userID;

            #endregion

            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];

            return dt;
        }


        /// <summary>
        /// 用户客观评价平均值
        /// </summary>
        /// <param name="objectID"></param>
        /// <param name="plateID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public DataTable ItemResultByUserApprove(string objectID, Guid plateID, int userID)
        {
            string commandName = "dbo.[Pr_Evaluation_ItemResultByUserApprove]";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@ObjectID", SqlDbType.NVarChar),
                    new SqlParameter("@PlateID", SqlDbType.UniqueIdentifier),
                    new SqlParameter("@UserID", SqlDbType.Int)
					
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = objectID;
            parms[1].Value = plateID;
            parms[2].Value = userID;

            #endregion

            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];

            return dt;
        }



        /// <summary>
        /// 获取某个项目的评价数
        /// </summary>
        /// <param name="objectID"></param>
        /// <param name="plateID"></param>
        /// <param name="userID"></param>
        /// <returns>返回:评价数</returns>
        public int GetItemResultTotalByUser(string objectID, Guid plateID, int userID)
        {
            string sqlModal = @"select  count(*) as num
                from [Evaluation_ItemResult]
                where ObjectID='{0}'
                    and (UserID={1} or {1} =-1)
                    and ItemID in (
                        select ItemID from Evaluation_Item
                        where (PlateID='{2}' or '{2}' is null or '{2}' = '00000000-0000-0000-0000-000000000000') 
                            and EvaluationLevel=5
                        )  ";
            string sql = string.Format(sqlModal, objectID, userID, plateID);
            return (int)SqlHelper.ExecuteScalar(ConnectionString.ETMSRead, CommandType.Text, sql, null);
        }



        /// <summary>
        /// 获取某个用户点评分数
        /// </summary>
        public DataTable GetResultUserScore(string objectID, Guid plateID, int userID)
        {
            string commandName = "dbo.Pr_Evaluation_ItemResult_UserScore";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@ObjectID", SqlDbType.NVarChar),
                    new SqlParameter("@PlateID", SqlDbType.UniqueIdentifier),
                    new SqlParameter("@UserID", SqlDbType.Int)					
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = objectID;
            parms[1].Value = plateID;
            parms[2].Value=userID;

            #endregion

            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];

            return dt;
        }

    }
}
