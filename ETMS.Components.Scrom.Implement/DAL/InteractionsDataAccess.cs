using System;
using System.Data;

using System.Data.SqlClient;
using ETMS.Utility.Data;

namespace ETMS.Components.Scrom.Implement.DAL
{
    public class InteractionsDataAccess
    {
        /// <summary>
        /// 增加新的交互（基础信息）
        /// </summary>
        /// <param name="resourceID">资源ID</param>
        /// <param name="interactionID">交互ID——Scorm课件指定的ID</param>
        /// <returns></returns>
        public int AddInteraction(Guid resourceID, string interactionID)
        {
            string commandName = "dbo.Pr_sco_e_Interactions_Insert";
            SqlParameter[] parms ={ 
                new SqlParameter("@ResourceID",SqlDbType.UniqueIdentifier),
                new SqlParameter("@InteractionID",SqlDbType.NVarChar,200)
            };

            parms[0].Value = resourceID;
            parms[1].Value = interactionID;

            return (int)SqlHelper.ExecuteScalar(ConnectionString.ScormWrite, CommandType.StoredProcedure, commandName, parms);
        }

        /// <summary>
        /// 获取数据库中已有的资源的交互数
        /// </summary>
        /// <param name="resourceID">资源ID</param>
        /// <returns></returns>
        public int GetInteractionCount(Guid resourceID)
        {
            int InteractionCount = 0;
            string commandName = "dbo.Pr_sco_e_Interactions_GetCountByResourceID";
            SqlParameter[] parms ={ 
               new SqlParameter("@ResourceID",SqlDbType.UniqueIdentifier)
            };
            parms[0].Value = resourceID;

            DataTable tab = SqlHelper.ExecuteDataset(ConnectionString.ScormRead, CommandType.StoredProcedure, commandName, parms).Tables[0];
            if (tab.Rows.Count > 0) {
                if (!Convert.IsDBNull(tab.Rows[0]["InteractionCount"])) {
                    InteractionCount = Convert.ToInt32(tab.Rows[0]["InteractionCount"]);
                }
            }
            return InteractionCount;
        }

        /// <summary>
        /// 根据交互的索引获取交互的ＩＤ
        /// </summary>
        /// <param name="resourceID">资源ID</param>
        /// <param name="n">交互的索引</param>
        /// <returns></returns>
        public string GetInteractionByIndex(Guid resourceID, int n)
        {
            string InteractionCode = "";
            string commandName = "dbo.Pr_sco_e_Interactions_GetInteractionCodeByRN";
            SqlParameter[] parms ={
                new SqlParameter("@ResourceID",SqlDbType.UniqueIdentifier),
                new SqlParameter("@index",SqlDbType.Int)
            };
            parms[0].Value = resourceID;
            parms[1].Value = n;

            DataTable tab = SqlHelper.ExecuteDataset(ConnectionString.ScormRead, CommandType.StoredProcedure, commandName, parms).Tables[0];
            if (tab.Rows.Count > 0)
            {
                if (!Convert.IsDBNull(tab.Rows[0]["InteractionID"]))
                {
                    InteractionCode = Convert.ToString(tab.Rows[0]["InteractionID"]);
                }
            }
            return InteractionCode;
        }

        /// <summary>
        /// 记录用户交互的答案(如果数据库中没有对应的记录，则增加数据，否则修改数据) 
        /// </summary>
        /// <param name="resourceID">资源ID</param>
        /// <param name="n">交互的索引</param>
        /// <param name="userID">用户ID</param>
        /// <param name="response">用户答案</param>
        public void SetInteractionResponse(Guid resourceID,Guid itemCourseResID, int n, int userID, string response)
        {
            string commandName = "dbo.Pr_sco_UserInteractions_SetResponse";

            SqlParameter[] parms ={ 
                new SqlParameter("@ResourceID",SqlDbType.UniqueIdentifier),
                new SqlParameter("@index",SqlDbType.Int),
                new SqlParameter("@UserID",SqlDbType.Int),
                new SqlParameter("@studentresponse",SqlDbType.NVarChar,5000),
                new SqlParameter("@ItemCourseResID",SqlDbType.UniqueIdentifier)
            };

            parms[0].Value = resourceID;
            parms[1].Value = n;
            parms[2].Value = userID;
            parms[3].Value = response;
            parms[4].Value = itemCourseResID;
            SqlHelper.ExecuteNonQuery(ConnectionString.ScormWrite,CommandType.StoredProcedure,commandName,parms);
        }

        /// <summary>
        /// 记录用户交互的结果(如果数据库中没有对应的记录，则增加数据，否则修改数据) 
        /// </summary>
        /// <param name="resourceID">资源ID</param>
        /// <param name="n">交互的索引</param>
        /// <param name="userID">用户ID</param>
        /// <param name="result">交互的结果</param>
        /// <returns></returns>
        public void SetInteractionResult(Guid resourceID, Guid itemCourseResID, int n, int userID, string result)
        {
            string commandName = "dbo.Pr_sco_UserInteractions_SetResult";
            
            SqlParameter[] parms ={ 
                new SqlParameter("@ResourceID",SqlDbType.UniqueIdentifier),
                new SqlParameter("@index",SqlDbType.Int),
                new SqlParameter("@UserID",SqlDbType.Int),
                new SqlParameter("@Result",SqlDbType.NVarChar,30),
                new SqlParameter("@ItemCourseResID",SqlDbType.UniqueIdentifier)
            };

            parms[0].Value = resourceID;
            parms[1].Value = n;
            parms[2].Value = userID;
            parms[3].Value = result;
            parms[4].Value = itemCourseResID;

            SqlHelper.ExecuteNonQuery(ConnectionString.ScormWrite, CommandType.StoredProcedure, commandName, parms);
        }

        /// <summary>
        /// 获取用户交互的答案
        /// </summary>
        /// <param name="resourceID">资源ID</param>
        /// <param name="n">交互的索引</param>
        /// <param name="userID">用户ID</param>
        /// <returns></returns>
        public string GetInteractionResponse(Guid resourceID, Guid itemCourseResID, int n, int userID)
        {
            string studentResponse = "";
            string commandName = "dbo.Pr_sco_UserInteractions_GetResponse";

            SqlParameter[] parms ={ 
                new SqlParameter("@ResourceID",SqlDbType.UniqueIdentifier),
                new SqlParameter("@index",SqlDbType.Int),
                new SqlParameter("@UserID",SqlDbType.Int),
                new SqlParameter("@ItemCourseResID",SqlDbType.UniqueIdentifier)
            };

            parms[0].Value = resourceID;
            parms[1].Value = n;
            parms[2].Value = userID;
            parms[3].Value = itemCourseResID;
            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ScormRead, CommandType.StoredProcedure, commandName, parms).Tables[0];
            if (dt.Rows.Count > 0)
            {
                if (!Convert.IsDBNull(dt.Rows[0]["studentresponse"]))
                {
                    studentResponse = dt.Rows[0]["studentresponse"].ToString();
                }
            }
            return studentResponse;
        }
    }
}
