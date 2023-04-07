using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using ETMS.Utility.Data;


using ETMS.Components.Basic.API.Entity.Security;
namespace ETMS.Components.Basic.Implement.DAL.Security
{
    public class FunctionDataAccess : ETMS.Components.Basic.Implement.DAL.Common.IDataAccess
    {
        private static string SelectSql = @"
SELECT [FunctionID]
      ,[FunctionName]
      ,[FunctionType]
      ,[OrderNo]
      ,[Description]
      ,[HelpID]
      ,[Status]
      ,[Creator]
      ,[CreateTime]
      ,[Modifier]
      ,[ModifyTime]
      ,[FunctionGroupID]
      ,[ComponentID]
  FROM [dbo].[Site_Function] WHERE 1=1 ";

        #region IDataAccess ≥…‘±

        public void Add(object obj)
        {
            Function function = (Function)obj;

            string commandName = "dbo.Pr_Site_Function_Insert";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {	new SqlParameter("@FunctionID", SqlDbType.Int, 4, ParameterDirection.Output, false, 0, 0, String.Empty, DataRowVersion.Default, null),
				
					new SqlParameter("@FunctionName", SqlDbType.NVarChar, 50),
					new SqlParameter("@FunctionType", SqlDbType.Int),
					new SqlParameter("@OrderNo", SqlDbType.Int),
					new SqlParameter("@Description", SqlDbType.NVarChar, 200),
					new SqlParameter("@HelpID", SqlDbType.Int),
					new SqlParameter("@Status", SqlDbType.Int),
					new SqlParameter("@Creator",  SqlDbType.NVarChar, 50),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@Modifier",  SqlDbType.NVarChar, 50),
					new SqlParameter("@ModifyTime", SqlDbType.DateTime),
					new SqlParameter("@FunctionGroupID", SqlDbType.Int),
					new SqlParameter("@ComponentID", SqlDbType.VarChar,50)
					};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
            }

            if (function.FunctionName != null) { parms[1].Value = function.FunctionName; } else { parms[1].Value = DBNull.Value; }
            parms[2].Value = function.FunctionType;
            parms[3].Value = function.OrderNo;
            if (function.Description != null) { parms[4].Value = function.Description; } else { parms[4].Value = DBNull.Value; }
            parms[5].Value = function.HelpID;
            parms[6].Value = function.Status;
            parms[7].Value = function.Creator;
            parms[8].Value = function.CreateTime;
            parms[9].Value = function.Modifier;
            parms[10].Value = function.ModifyTime;
            parms[11].Value = function.FunctionGroupID; 
            parms[12].Value = function.ComponentID;
            #endregion
            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);

            function.FunctionID = (Int32)parms[0].Value;
        }

        public void Update(object obj)
        {
            Function function = (Function)obj;

            string commandName = "dbo.Pr_Site_Function_Update";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@FunctionID", SqlDbType.Int),
					new SqlParameter("@FunctionName", SqlDbType.NVarChar, 50),
					new SqlParameter("@FunctionType", SqlDbType.Int),
					new SqlParameter("@OrderNo", SqlDbType.Int),
					new SqlParameter("@Description", SqlDbType.NVarChar, 200),
					new SqlParameter("@HelpID", SqlDbType.Int),
					new SqlParameter("@Status", SqlDbType.Int),
					new SqlParameter("@Creator",  SqlDbType.NVarChar, 50),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@Modifier",  SqlDbType.NVarChar, 50),
					new SqlParameter("@ModifyTime", SqlDbType.DateTime),
					new SqlParameter("@FunctionGroupID", SqlDbType.Int),
					new SqlParameter("@ComponentID", SqlDbType.VarChar,50)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
            }

            parms[0].Value = function.FunctionID;
            if (function.FunctionName != null) { parms[1].Value = function.FunctionName; } else { parms[1].Value = DBNull.Value; }
            parms[2].Value = function.FunctionType;
            parms[3].Value = function.OrderNo;
            if (function.Description != null) { parms[4].Value = function.Description; } else { parms[4].Value = DBNull.Value; }
            parms[5].Value = function.HelpID;
            parms[6].Value = function.Status;
            parms[7].Value = function.Creator;
            parms[8].Value = function.CreateTime;
            parms[9].Value = function.Modifier;
            parms[10].Value = function.ModifyTime;
            parms[11].Value = function.FunctionGroupID;
            parms[12].Value = function.ComponentID;
            #endregion
            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
        }

        public void Delete(object obj)
        {
            Function function = (Function)obj;

            string commandName = "dbo.Pr_Site_Function_Delete";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@FunctionID", SqlDbType.Int)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
            }

            parms[0].Value = function.FunctionID;

            #endregion
            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
        }

        public object Query(object id)
        {
            Int32 nodeID = (int)id;

            string sqlScriptFormat = SelectSql + " AND FunctionID={0}";

            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.Text, string.Format(sqlScriptFormat, nodeID)).Tables[0];
            if (dt.Rows.Count == 0)
                return null;
            return this.ConvertDataRowToFunction(dt.Rows[0]);
        }

        public Object[] Query(string filter)
        {
            string sqlScriptFormat = SelectSql + " {0} " + " ORDER BY OrderNo";

            List<Function> list = new List<Function>();
            foreach (DataRow row in SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.Text, string.Format(sqlScriptFormat, filter)).Tables[0].Rows)
            {
                list.Add(this.ConvertDataRowToFunction(row));
            }
            return (Object[])list.ToArray();
        }

        public Object[] Query(int pageIndex, int pageSize, string filter, string orderBy, out int recordCount)
        {
            List<Function> functions = new List<Function>();

            string commandName = "dbo.Pr_Site_Function_GetPagedList";
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
                functions.Add(this.ConvertDataRowToFunction(row));
            }

            recordCount = (int)parms[4].Value;
            return (Object[])functions.ToArray();
        }

        #endregion

        #region ORM
        public Function ConvertDataRowToFunction(DataRow row)
        {
            Function entity = new Function();

            entity.FunctionID = (Int32)row["FunctionID"];

            entity.FunctionName = (String)row["FunctionName"];

            entity.FunctionType = (Int32)row["FunctionType"];

            entity.OrderNo = (Int32)row["OrderNo"];

            entity.Description = (row["Description"] == System.DBNull.Value ? "" : (String)row["Description"]);

            entity.HelpID = (Int32)row["HelpID"];

            entity.Status = (Int32)row["Status"];

            entity.Creator = (string)row["Creator"];

            entity.CreateTime = (DateTime)row["CreateTime"];

            entity.Modifier = (string)row["Modifier"];

            entity.ModifyTime = (DateTime)row["ModifyTime"];

            entity.FunctionGroupID = (Int32)row["FunctionGroupID"];

            entity.ComponentID = Convert.ToString(row["ComponentID"]);

            return entity;
        }
        #endregion
    }
}
