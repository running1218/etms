using System;
using System.Data;

using System.Data.SqlClient;
using ETMS.Utility.Data;

namespace ETMS.Components.Scrom.Implement.DAL
{
    public class ObjectivesDataAccess
    {
        /// <summary>
        /// SetValue("cmi.objectives.n.id",id) 增加一个知识点
        /// </summary>
        /// <param name="ResourceID"></param>
        /// <param name="ObjectiveID"></param>
        /// <returns>返回主键</returns>
        public int AddObjective(Guid ResourceID, string ObjectiveID)
        {
            string commandName = "dbo.pr_sco_e_Objectives_Insert";

            SqlParameter[] parms ={ 
                new SqlParameter("@ResourceID",SqlDbType.UniqueIdentifier),
                new SqlParameter("@ObjectiveID",SqlDbType.NVarChar,200)
            };
            parms[0].Value = ResourceID;
            parms[1].Value = ObjectiveID;

            return (int)SqlHelper.ExecuteScalar(ConnectionString.ScormWrite, CommandType.StoredProcedure, commandName, parms);
        }

        /// <summary>
        /// 根据n获取知识点代码
        /// </summary>
        /// <param name="resourceID">资源ID</param>
        /// <param name="n">知识点的索引</param>
        /// <returns></returns>
        public string GetObjectiveByIndex(Guid resourceID, int n)
        {
            string objectiveCode = "";
            string commandName = "dbo.Pr_sco_e_Objectives_GetByResourceIDIndex";

            SqlParameter[] parms ={ 
                new SqlParameter("@ResourceID",SqlDbType.UniqueIdentifier),
                new SqlParameter("@index",SqlDbType.Int)
            };
            parms[0].Value = resourceID;
            parms[1].Value = n;

            DataTable tab = SqlHelper.ExecuteDataset(ConnectionString.ScormRead, CommandType.StoredProcedure, commandName, parms).Tables[0];
            if (tab.Rows.Count > 0) {
                if (!Convert.IsDBNull(tab.Rows[0]["objectiveCode"])) {
                    objectiveCode = Convert.ToString(tab.Rows[0]["objectiveCode"]);
                }
            }
            return objectiveCode;
        }

        /// <summary>
        /// 知识点数量 返回整数
        /// </summary>
        /// <param name="resourceID"></param>
        /// <returns></returns>
        public int GetObjectivesCount(Guid resourceID)
        {
            int objectivesCount = 0;
            string commandName = "dbo.Pr_sco_e_Objectives_GetCountByResourceID";

            SqlParameter[] parms ={ 
                new SqlParameter("@ResourceID",SqlDbType.UniqueIdentifier)
            };
            parms[0].Value = resourceID;

            DataTable tab = SqlHelper.ExecuteDataset(ConnectionString.ScormRead, CommandType.StoredProcedure, commandName, parms).Tables[0];
            
            if (tab.Rows.Count > 0) {
                if (!Convert.IsDBNull(tab.Rows[0]["objectivesCount"])) {
                    objectivesCount = Convert.ToInt32(tab.Rows[0]["objectivesCount"]);
                }
            }
            return objectivesCount;
        }

        /// <summary>
        /// 获取知识点得分0-100
        /// </summary>
        /// <param name="ResourceID"></param>
        /// <param name="UserID"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public string GetObjectivesScoreRaw(Guid ResourceID,Guid itemCourseResID, int UserID, int n)
        {
            string ScoreRaw = "";
            string commandName = "dbo.Pr_sco_UserObjectives_GetScoreRaw";
            SqlParameter[] parms ={ 
                new SqlParameter("@ResourceID",SqlDbType.UniqueIdentifier),
                new SqlParameter("@UserID",SqlDbType.Int),
                new SqlParameter("@index",SqlDbType.Int),
                new SqlParameter("@ItemCourseResID",SqlDbType.UniqueIdentifier)
            };

            parms[0].Value = ResourceID;
            parms[1].Value = UserID;
            parms[2].Value = n;
            parms[3].Value = itemCourseResID;

            DataTable tab = SqlHelper.ExecuteDataset(ConnectionString.ScormRead, CommandType.StoredProcedure, commandName, parms).Tables[0];

            if (tab.Rows.Count > 0) {
                if (!Convert.IsDBNull(tab.Rows[0]["ScoreRaw"])) {
                    ScoreRaw = Convert.ToString(tab.Rows[0]["ScoreRaw"]);
                }
            }
            return ScoreRaw;
        }


        /// <summary>
        /// 设置知识点得分0-100
        /// </summary>
        /// <param name="ResourceID"></param>
        /// <param name="UserID"></param>
        /// <param name="n"></param>
        /// <param name="ScoreRaw"></param>
        public void SetObjectivesScoreRaw(Guid ResourceID, int UserID, int n, string ScoreRaw)
        {
            string commandName = "dbo.Pr_sco_UserObjectives_SetScoreRaw";
            SqlParameter[] parms ={ 
                new SqlParameter("@ResourceID",SqlDbType.UniqueIdentifier),
                new SqlParameter("@UserID",SqlDbType.Int),
                new SqlParameter("@index",SqlDbType.Int),
                new SqlParameter("@ScoreRaw",SqlDbType.NVarChar,10)
            };

            parms[0].Value = ResourceID;
            parms[1].Value = UserID;
            parms[2].Value = n;
            parms[3].Value = ScoreRaw;

            SqlHelper.ExecuteNonQuery(ConnectionString.ScormWrite,CommandType.StoredProcedure,commandName,parms);
        }

        /// <summary>
        /// 获取知识点状态
        /// </summary>
        /// <param name="ResourceID"></param>
        /// <param name="UserID"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public string GetObjectivesStatus(Guid ResourceID, int UserID, int n)
        {
            string StatusCode = "";
            string commandName = "dbo.Pr_sco_UserObjectives_GetStatusCode";
            SqlParameter[] parms ={ 
                new SqlParameter("@ResourceID",SqlDbType.UniqueIdentifier),
                new SqlParameter("@UserID",SqlDbType.Int),
                new SqlParameter("@index",SqlDbType.Int)
            };
            parms[0].Value = ResourceID;
            parms[1].Value = UserID;
            parms[2].Value = n;

            DataTable tab = SqlHelper.ExecuteDataset(ConnectionString.ScormRead, CommandType.StoredProcedure, commandName, parms).Tables[0];
            if (tab.Rows.Count > 0) {
                if (!Convert.IsDBNull(tab.Rows[0]["StatusCode"]))
                {
                    StatusCode = Convert.ToString(tab.Rows[0]["StatusCode"]);
                }
            }
            return StatusCode;
        }

        /// <summary>
        /// 设置知识点状态
        /// </summary>
        /// <param name="ResourceID"></param>
        /// <param name="UserID"></param>
        /// <param name="n"></param>
        /// <param name="Status"></param>
        public void SetObjectivesStatus(Guid ResourceID, int UserID, int n, string Status)
        {
            string commandName = "dbo.Pr_sco_UserObjectives_SetStatusCode";
            SqlParameter[] parms ={ 
                new SqlParameter("@ResourceID",SqlDbType.UniqueIdentifier),
                new SqlParameter("@UserID",SqlDbType.Int),
                new SqlParameter("@index",SqlDbType.Int),
                new SqlParameter("@StatusCode",SqlDbType.NVarChar,50)
            };

            parms[0].Value = ResourceID;
            parms[1].Value = UserID;
            parms[2].Value = n;
            parms[3].Value = Status;

            SqlHelper.ExecuteNonQuery(ConnectionString.ScormWrite,CommandType.StoredProcedure,commandName,parms);
        }
    }
}
