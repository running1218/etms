using System;
using System.Data;
using System.Text;

using System.Data.SqlClient;
using ETMS.Utility.Data;

namespace ETMS.Components.QS.Implement.DAL
{
    /// <summary>
    /// 问卷调查表数据访问
    /// </summary>
    public partial class QS_QueryDataAccess
    {

        /// <summary>
        /// 复制整个问卷调查
        /// </summary>
        /// <param name="queryID">要复制的问卷调查ID</param>
        /// <param name="createUser">操作人</param>
        /// <param name="createUserID">操作人ID</param>
        /// <returns>新创建的调查问卷ID</returns>
        public Guid CopyTemplate(Guid queryID, string createUser, int createUserID)
        {
            string commandName = "dbo.[Pr_QS_Query_CopyTemplate]";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@QueryID", SqlDbType.UniqueIdentifier),
                    new SqlParameter("@CreateUser", SqlDbType.NVarChar),
                    new SqlParameter("@CreateUserID", SqlDbType.Int),
                    new SqlParameter("@ReturnNewQueryID", SqlDbType.UniqueIdentifier)
                    };
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
            }

            parms[0].Value = queryID;
            parms[1].Value = createUser;
            parms[2].Value = createUserID;
            
            parms[3].Value = Guid.NewGuid();
            parms[3].Direction = ParameterDirection.Output;

            #endregion
            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);


            Guid returnQueryID = new Guid();
            if (parms[3].Value != null)
                returnQueryID = (Guid)parms[3].Value;
            return returnQueryID;


        }


        /// <summary>
        /// 发布某个“问卷调查”
        /// </summary>
        /// <param name="queryID">“问卷调查”ID</param>
        /// <param name="isIssue">是否发布（0：不发布，1：发布）</param>
        /// <param name="issueUser">发布人</param>
        public void QS_Query_Issue(Guid queryID, int isIssue, string issueUser)
        {
            string commandName = "dbo.Pr_QS_Query_Issue";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@QueryID", SqlDbType.UniqueIdentifier),
                    new SqlParameter("@IsIssue", SqlDbType.Int),
                    new SqlParameter("@IssueUser", SqlDbType.NVarChar)
                    };
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
            }

            parms[0].Value = queryID;
            parms[1].Value = isIssue;
            parms[2].Value = issueUser;
            #endregion
            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);

        }


        /// <summary>
        /// 根据标识问卷的XML文档
        /// </summary>
        public String CreateXMLByQueryID(Guid queryID)
        {
            return CreateXMLByQueryID(queryID, 1, null, null);
        }

        /// <summary>
        /// 根据标识问卷的AnswerXML文档
        /// </summary>
        public String CreateAnswerXMLByQueryID(Guid queryID)
        {
            return CreateXMLByQueryID(queryID, 2, null, null);
        }

        private String CreateXMLByQueryID(Guid queryID, int requstType, string resourceType, string resourceCode)
        {
            StringBuilder XmlContent = new StringBuilder();
            XmlContent.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            SqlParameter[] parms = null;
            string commandName = "";
            if (requstType == 1)
            {
                commandName = "dbo.[Pr_QS_Query_CreateXMLByQueryID]";
                parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
                if (parms == null)
                {
                    parms = new SqlParameter[] {
					new SqlParameter("@QueryID", SqlDbType.UniqueIdentifier)
				};
                    SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
                }

                parms[0].Value = queryID;
            }
            else if (requstType == 2)
            {
                commandName = "dbo.Pr_QS_Query_CreateAnswerXMLByQueryID";
                parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
                if (parms == null)
                {
                    parms = new SqlParameter[] {
					new SqlParameter("@QueryID", SqlDbType.UniqueIdentifier)
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


    }
}
