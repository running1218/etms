using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using ETMS.Utility.Data;


using ETMS.Components.Basic.API.Entity.Security;
namespace ETMS.Components.Basic.Implement.DAL.Security
{
    public class PageUrlDataAccess : ETMS.Components.Basic.Implement.DAL.Common.IDataAccess
    {
        private static string SelectSql = @"
SELECT [PageID]
      ,[PageURL]
      ,[Status]
      ,[IsMainPage]
      ,[Description]
      ,[HelpID]
      ,[FunctionID]
  FROM  [dbo].[Site_PageUrl] WHERE 1=1 ";

        #region IDataAccess ≥…‘±

        public void Add(object obj)
        {
            PageUrl pageUrl = (PageUrl)obj;

            string commandName = "dbo.Pr_Site_PageUrl_Insert";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {	new SqlParameter("@PageID", SqlDbType.Int, 4, ParameterDirection.Output, false, 0, 0, String.Empty, DataRowVersion.Default, null),
				
					new SqlParameter("@PageURL", SqlDbType.NVarChar, 500),
					new SqlParameter("@Status", SqlDbType.Int),
					new SqlParameter("@IsMainPage", SqlDbType.Int),
					new SqlParameter("@Description", SqlDbType.NVarChar, 200),
					new SqlParameter("@HelpID", SqlDbType.Int),
					new SqlParameter("@FunctionID", SqlDbType.Int)
					};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
            }

            if (pageUrl.PageURL != null) { parms[1].Value = pageUrl.PageURL; } else { parms[1].Value = DBNull.Value; }
            parms[2].Value = pageUrl.Status;
            parms[3].Value = pageUrl.IsMainPage;
            if (pageUrl.Description != null) { parms[4].Value = pageUrl.Description; } else { parms[4].Value = DBNull.Value; }
            parms[5].Value = pageUrl.HelpID;
            parms[6].Value = pageUrl.FunctionID;
            #endregion
            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);

            pageUrl.PageID = (Int32)parms[0].Value;
        }

        public void Update(object obj)
        {
            PageUrl pageUrl = (PageUrl)obj;

            string commandName = "dbo.Pr_Site_PageUrl_Update";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@PageID", SqlDbType.Int),
					new SqlParameter("@PageURL", SqlDbType.NVarChar, 500),
					new SqlParameter("@Status", SqlDbType.Int),
					new SqlParameter("@IsMainPage", SqlDbType.Int),
					new SqlParameter("@Description", SqlDbType.NVarChar, 200),
					new SqlParameter("@HelpID", SqlDbType.Int),
					new SqlParameter("@FunctionID", SqlDbType.Int)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
            }

            parms[0].Value = pageUrl.PageID;
            if (pageUrl.PageURL != null) { parms[1].Value = pageUrl.PageURL; } else { parms[1].Value = DBNull.Value; }
            parms[2].Value = pageUrl.Status;
            parms[3].Value = pageUrl.IsMainPage;
            if (pageUrl.Description != null) { parms[4].Value = pageUrl.Description; } else { parms[4].Value = DBNull.Value; }
            parms[5].Value = pageUrl.HelpID;
            parms[6].Value = pageUrl.FunctionID;
            #endregion
            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
        }

        public void Delete(object obj)
        {
            PageUrl pageUrl = (PageUrl)obj;

            string commandName = "dbo.Pr_Site_PageUrl_Delete";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@PageID", SqlDbType.Int)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
            }

            parms[0].Value = pageUrl.PageID;

            #endregion
            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
        }

        public object Query(object id)
        {
            Int32 nodeID = (int)id;

            string sqlScriptFormat = SelectSql + " AND PageID={0}";

            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.Text, string.Format(sqlScriptFormat, nodeID)).Tables[0];
            if (dt.Rows.Count == 0)
                return null;
            return PageUrl.ConvertDataRowToPageUrl(dt.Rows[0]);
        }

        public Object[] Query(string filter)
        {
            string sqlScriptFormat = SelectSql + " {0} ";

            List<PageUrl> list = new List<PageUrl>();
            foreach (DataRow row in SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.Text, string.Format(sqlScriptFormat, filter)).Tables[0].Rows)
            {
                list.Add(PageUrl.ConvertDataRowToPageUrl(row));
            }
            return list.ToArray();
        }

        public Object[] Query(int pageIndex, int pageSize, string filter, string orderBy, out int recordCount)
        {
            recordCount = 0;
            List<PageUrl> pageUrls = new List<PageUrl>();

            string commandName = "dbo.Pr_Site_PageUrl_GetPagedList";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@StartRowIndex", SqlDbType.Int),
					new SqlParameter("@MaximumRows", SqlDbType.Int),
					new SqlParameter("@SortExpression", SqlDbType.NVarChar),
					new SqlParameter("@Criteria", SqlDbType.NVarChar),
					new SqlParameter("@ReturnValue", SqlDbType.Int, 4, ParameterDirection.ReturnValue, false, 0, 0, String.Empty, DataRowVersion.Default, null)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = pageIndex;
            parms[1].Value = pageSize;
            parms[2].Value = orderBy;
            parms[3].Value = filter;
            #endregion
            foreach (DataRow row in SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0].Rows)
            {
                pageUrls.Add(PageUrl.ConvertDataRowToPageUrl(row));
            }

            recordCount = (int)parms[4].Value;
            return pageUrls.ToArray();
        }

        #endregion
    }
}
