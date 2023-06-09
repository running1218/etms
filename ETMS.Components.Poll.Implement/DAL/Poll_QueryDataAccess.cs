//==================================================================================================
//Version 1.0, auto-generated.
//Generated By running1218
//Date: 2012-4-17 15:53:39.
//==================================================================================================
//==================================================================================================
//This file is generated by CodeGenerator(Ver 2.1)
//==================================================================================================

using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

using System.Data.SqlClient;
using ETMS.Utility.Data;

using ETMS.Components.Poll.API.Entity;

namespace ETMS.Components.Poll.Implement.DAL
{
    /// <summary>
    /// 调查表数据访问
    /// </summary>
    public partial class Poll_QueryDataAccess
    {

        /// <summary>
        /// 根据标识问卷的XML文档
        /// </summary>
        public String CreateXMLByQueryID(Int32 queryID)
        {
            return CreateXMLByQueryID(queryID, 1, null, null);
        }

        /// <summary>
        /// 根据标识问卷的AnswerXML文档
        /// </summary>
        public String CreateAnswerXMLByQueryID(Int32 queryID)
        {
            return CreateXMLByQueryID(queryID, 2, null, null);
        }

        private String CreateXMLByQueryID(Int32 queryID, int requstType, string resourceType, string resourceCode)
        {
            StringBuilder XmlContent = new StringBuilder();
            XmlContent.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            SqlParameter[] parms = null;
            string commandName = "";
            if (requstType == 1)
            {
                commandName = "dbo.Pr_Poll_Query_CreateXMLByQueryID";
                parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
                if (parms == null)
                {
                    parms = new SqlParameter[] {
					new SqlParameter("@QueryID", SqlDbType.Int)
				};
                    SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
                }

                parms[0].Value = queryID;
            }
            else if (requstType == 2)
            {
                commandName = "dbo.Pr_Poll_Query_CreateAnswerXMLByQueryID";
                parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
                if (parms == null)
                {
                    parms = new SqlParameter[] {
					new SqlParameter("@QueryID", SqlDbType.Int)
				};
                    SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
                }

                parms[0].Value = queryID;
            }
            using (SqlDataReader dataReader = SqlHelper.ExecuteReader(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms))
            {
                if (dataReader.Read())
                {
                    XmlContent.Append(dataReader.GetString(0));
                }
            }

            return XmlContent.ToString();
        }

        public String CreateXMLByQueryID(Int32 queryID, int batchID)
        {
            StringBuilder XmlContent = new StringBuilder();
            XmlContent.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            SqlParameter[] parms = null;
            string commandName = "";

            commandName = "dbo.Pr_Poll_Query_CreateXMLByQueryID1";
            parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@QueryID", SqlDbType.Int),
                    new SqlParameter("@BatchID", SqlDbType.Int)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = queryID;
            parms[1].Value = batchID;

            using (SqlDataReader dataReader = SqlHelper.ExecuteReader(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms))
            {
                if (dataReader.Read())
                {
                    XmlContent.Append(dataReader.GetString(0));
                }
            }

            return XmlContent.ToString();
        }

        public int GetQueryUserResultCount(Int32 queryID)
        {
            string commandName = "dbo.Pr_QS_Query_PublishResultUserCount";
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@QueryID", SqlDbType.Int),
                    new SqlParameter("@TotalRecords", SqlDbType.Int)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = queryID;
            parms[1].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms);
            return (int)parms[1].Value;
        }
        /// <summary>
        /// 用户调查列表
        /// </summary>
        /// <param name="queryID"></param>
        /// <returns></returns>
        public String CreateResltListXMLByQueryID(Int32 queryID,int batchID)
        {
            StringBuilder XmlContent = new StringBuilder();
            XmlContent.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            SqlParameter[] parms = null;
            string commandName = "";

            commandName = "dbo.Pr_Poll_Query_CreateResultListXMLByQueryIDBatchID";
            parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@QueryID", SqlDbType.Int),
                    new SqlParameter("@BatchID", SqlDbType.Int)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = queryID;
            parms[1].Value = batchID;

            using (SqlDataReader dataReader = SqlHelper.ExecuteReader(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms))
            {
                if (dataReader.Read())
                {
                    XmlContent.Append(dataReader.GetString(0));
                }
            }

            return XmlContent.ToString();
        }

        /// <summary>
        /// 根据参数获取对象列表（分页，可排序）
        /// </summary>
        public IList<Poll_Query> GetEntityList(int pageIndex, int pageSize, string sType, string sortExpression, string criteria, out int totalRecords)
        {
            IList<Poll_Query> list = new List<Poll_Query>();
            string commandName = "dbo.Pr_Poll_Query_GetPagedList";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@PageIndex", SqlDbType.Int),
					new SqlParameter("@PageSize", SqlDbType.Int),
					new SqlParameter("@SortExpression", SqlDbType.VarChar),
					new SqlParameter("@Criteria", SqlDbType.VarChar),
                    new SqlParameter("@sType", SqlDbType.VarChar),
					new SqlParameter("@ReturnValue", SqlDbType.Int, 4, ParameterDirection.ReturnValue, false, 0, 0, String.Empty, DataRowVersion.Default, null)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = pageIndex;
            parms[1].Value = pageSize;
            parms[2].Value = sortExpression;
            parms[3].Value = criteria;
            parms[4].Value = sType;
            #endregion
            using (SqlDataReader dataReader = SqlHelper.ExecuteReader(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms))
            {
                while (dataReader.Read())
                {
                    list.Add(PopulatePoll_QueryFromDataReader(dataReader));
                }
            }
            totalRecords = (int)parms[5].Value;
            return list;
        }
    }
}
