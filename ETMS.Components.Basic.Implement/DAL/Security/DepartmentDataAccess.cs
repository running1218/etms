using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using ETMS.Utility.Data;


using ETMS.Components.Basic.API.Entity.Security;
namespace ETMS.Components.Basic.Implement.DAL.Security
{
    public class DepartmentDataAccess : ETMS.Components.Basic.Implement.DAL.Common.IDataAccess
    {
        private static string SelectSql = @"
SELECT [DepartmentID]
      ,[DepartmentName]
      ,[ParentID]
      ,[DepartmentCode]
      ,[Path]
      ,[DisplayPath]
      ,[OrganizationID]
      ,[State]
      ,[Description]
      ,[Manager]
      ,[OrderNo]
      ,[Creator]
      ,[CreateTime]
      ,[Modifier]
      ,[ModifyTime]
  FROM [dbo].[Site_Department] WHERE 1=1 ";

        private static string SelectDicSql = @"
SELECT [DepartmentID] as [ColumnCodeValue]
      ,[DisplayPath]  as [ColumnNameValue]
  FROM [dbo].[Site_Department] WHERE 1=1 ";

        #region IDataAccess 成员

        public void Add(object obj)
        {
            Department site_Department = (Department)obj;

            string commandName = "dbo.Pr_Site_Department_Insert";
            #region
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {	new SqlParameter("@DepartmentID", SqlDbType.Int, 4, ParameterDirection.Output, false, 0, 0, String.Empty, DataRowVersion.Default, null),
				
					new SqlParameter("@DepartmentName", SqlDbType.NVarChar, 100),
					new SqlParameter("@ParentID", SqlDbType.Int),
					new SqlParameter("@DepartmentCode", SqlDbType.NVarChar, 15),
					new SqlParameter("@Path", SqlDbType.NVarChar, 100),
					new SqlParameter("@DisplayPath", SqlDbType.NVarChar, 200),
					new SqlParameter("@OrganizationID", SqlDbType.Int),
					new SqlParameter("@State", SqlDbType.SmallInt),
					new SqlParameter("@Description", SqlDbType.NVarChar, 200),
					new SqlParameter("@Manager", SqlDbType.NVarChar, 50),
					new SqlParameter("@OrderNo", SqlDbType.Int),
					new SqlParameter("@Creator", SqlDbType.NVarChar, 50),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@Modifier", SqlDbType.NVarChar, 50),
					new SqlParameter("@ModifyTime", SqlDbType.DateTime)
					};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
            }

            if (site_Department.DepartmentName != null) { parms[1].Value = site_Department.DepartmentName; } else { parms[1].Value = DBNull.Value; }
            parms[2].Value = site_Department.ParentNodeID;
            if (site_Department.DepartmentCode != null) { parms[3].Value = site_Department.DepartmentCode; } else { parms[3].Value = DBNull.Value; }
            if (site_Department.Path != null) { parms[4].Value = site_Department.Path; } else { parms[4].Value = DBNull.Value; }
            if (site_Department.DisplayPath != null) { parms[5].Value = site_Department.DisplayPath; } else { parms[5].Value = DBNull.Value; }
            parms[6].Value = site_Department.OrganizationID;
            parms[7].Value = site_Department.State;
            if (site_Department.Description != null) { parms[8].Value = site_Department.Description; } else { parms[8].Value = DBNull.Value; }
            if (site_Department.Manager != null) { parms[9].Value = site_Department.Manager; } else { parms[9].Value = DBNull.Value; }
            parms[10].Value = site_Department.OrderNo;
            if (site_Department.Creator != null) { parms[11].Value = site_Department.Creator; } else { parms[11].Value = DBNull.Value; }
            parms[12].Value = site_Department.CreateTime;
            if (site_Department.Modifier != null) { parms[13].Value = site_Department.Modifier; } else { parms[13].Value = DBNull.Value; }
            parms[14].Value = site_Department.ModifyTime;
            #endregion
            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);

            site_Department.DepartmentID = (Int32)parms[0].Value;
        }

        public void Update(object obj)
        {
            Department site_Department = (Department)obj;

            string commandName = "dbo.Pr_Site_Department_Update";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@DepartmentID", SqlDbType.Int),
					new SqlParameter("@DepartmentName", SqlDbType.NVarChar, 100),
					new SqlParameter("@ParentID", SqlDbType.Int),
					new SqlParameter("@DepartmentCode", SqlDbType.NVarChar, 15),
					new SqlParameter("@Path", SqlDbType.NVarChar, 100),
					new SqlParameter("@DisplayPath", SqlDbType.NVarChar, 200),
					new SqlParameter("@OrganizationID", SqlDbType.Int),
					new SqlParameter("@State", SqlDbType.SmallInt),
					new SqlParameter("@Description", SqlDbType.NVarChar, 200),
					new SqlParameter("@Manager", SqlDbType.NVarChar, 50),
					new SqlParameter("@OrderNo", SqlDbType.Int),
					new SqlParameter("@Creator", SqlDbType.NVarChar, 50),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@Modifier", SqlDbType.NVarChar, 50),
					new SqlParameter("@ModifyTime", SqlDbType.DateTime)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
            }

            parms[0].Value = site_Department.DepartmentID;
            if (site_Department.DepartmentName != null) { parms[1].Value = site_Department.DepartmentName; } else { parms[1].Value = DBNull.Value; }
            parms[2].Value = site_Department.ParentNodeID;
            if (site_Department.DepartmentCode != null) { parms[3].Value = site_Department.DepartmentCode; } else { parms[3].Value = DBNull.Value; }
            if (site_Department.Path != null) { parms[4].Value = site_Department.Path; } else { parms[4].Value = DBNull.Value; }
            if (site_Department.DisplayPath != null) { parms[5].Value = site_Department.DisplayPath; } else { parms[5].Value = DBNull.Value; }
            parms[6].Value = site_Department.OrganizationID;
            parms[7].Value = site_Department.State;
            if (site_Department.Description != null) { parms[8].Value = site_Department.Description; } else { parms[8].Value = DBNull.Value; }
            if (site_Department.Manager != null) { parms[9].Value = site_Department.Manager; } else { parms[9].Value = DBNull.Value; }
            parms[10].Value = site_Department.OrderNo;
            if (site_Department.Creator != null) { parms[11].Value = site_Department.Creator; } else { parms[11].Value = DBNull.Value; }
            parms[12].Value = site_Department.CreateTime;
            if (site_Department.Modifier != null) { parms[13].Value = site_Department.Modifier; } else { parms[13].Value = DBNull.Value; }
            parms[14].Value = site_Department.ModifyTime;
            #endregion
            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
        }

        public void Delete(object obj)
        {
            Department site_Department = (Department)obj;
            string commandName = "dbo.Pr_Site_Department_Delete";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@DepartmentID", SqlDbType.Int)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
            }

            parms[0].Value = site_Department.DepartmentID;

            #endregion
            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
        }

        public object Query(object id)
        {
            int groupID = (Int32)id;
            string sqlScriptFormat = SelectSql + " AND DepartmentID={0}";

            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.Text, string.Format(sqlScriptFormat, groupID)).Tables[0];
            if (dt.Rows.Count == 0)
                return null;
            return Department.ConvertDataRowToRole(dt.Rows[0]);
        }

        public Object[] Query(string filter)
        {
            string sqlScriptFormat = SelectSql + " {0} order by OrderNO asc,[Path] ASC";

            List<Department> list = new List<Department>();
            foreach (DataRow row in SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.Text, string.Format(sqlScriptFormat, filter)).Tables[0].Rows)
            {
                list.Add(Department.ConvertDataRowToRole(row));
            }
            return (Object[])list.ToArray();
        }


        public Object[] QueryMove(string filter)
        {
            string sqlScriptFormat = SelectSql + " {0} order by LEN([Path]) ASC";

            List<Department> list = new List<Department>();
            foreach (DataRow row in SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.Text, string.Format(sqlScriptFormat, filter)).Tables[0].Rows)
            {
                list.Add(Department.ConvertDataRowToRole(row));
            }
            return (Object[])list.ToArray();
        }


        public DataTable QueryDataList(string filter)
        {
            string sqlScriptFormat = SelectDicSql + " {0} order by dbo.fn_GetDepartmentOrderPath(OrganizationID, [DisplayPath]) asc";

            return SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.Text, string.Format(sqlScriptFormat, filter)).Tables[0];
        }

        public Object[] Query(int pageIndex, int pageSize, string filter, string orderBy, out int recordCount)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion

        /// <summary>
        /// 生成新的部门编码与Path编码
        /// </summary>
        /// <param name="organizationID">机构ID</param>
        /// <param name="parentID">上级ID</param>
        /// <returns></returns>
        public DataTable GetNewCodePath(int organizationID, int parentID)
        {
            string commandName = "Pr_Site_Department_GetNewCodePath";
            SqlParameter[] parms = new SqlParameter[] {
					new SqlParameter("@OrganizationID", SqlDbType.Int),
					new SqlParameter("@ParentID", SqlDbType.Int)
            };
            parms[0].Value = organizationID;
            parms[1].Value = parentID;

            return SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName,parms).Tables[0];
        }
        
        /// <summary>
        /// 移动部门时生成新的部门编码与Path编码
        /// </summary>
        /// <param name="organizationID">机构ID</param>
        /// <param name="parentID">上级ID</param>
        /// <param name="departmentID">本部门ID</param>
        /// <returns></returns>
        public DataTable GetNewCodePathMove(int organizationID, int parentID, string oldNodePath)
        {
            string commandName = "Pr_Site_Department_GetNewCodePathMove";
            SqlParameter[] parms = new SqlParameter[] {
					new SqlParameter("@OrganizationID", SqlDbType.Int),
					new SqlParameter("@ParentID", SqlDbType.Int),
                    new SqlParameter("@oldNodePath",SqlDbType.VarChar)
            };
            parms[0].Value = organizationID;
            parms[1].Value = parentID;
            parms[2].Value = oldNodePath;

            return SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];
        }
    }
}
