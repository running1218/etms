using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using ETMS.Utility.Data;


using ETMS.Components.Basic.API.Entity.Security;
namespace ETMS.Components.Basic.Implement.DAL.Security
{
    public class RoleDataAccess : ETMS.Components.Basic.Implement.DAL.Common.IDataAccess
    {
        private static string SelectSql = @"
    select  
           [RoleID]
          ,[RoleName]
          ,[ParentID]
          ,[RoleCode]
          ,[RoleMapCode]
          ,[State]
          ,[Description]
          ,[Creator]
          ,[CreateTime]
          ,[Modifier]
          ,[ModifyTime]
          ,[OrganizationID]  
    from Site_Role WHERE 1=1 ";

        #region IDataAccess 成员

        public void Add(object obj)
        {
            Role role = (Role)obj;

            string commandName = "Pr_Site_Role_Add";
            SqlParameter[]
                parameters = {	
                                 new SqlParameter( "@RoleName" ,SqlDbType.NVarChar,50)
                                ,new SqlParameter( "@ParentID" ,SqlDbType.Int)
                                ,new SqlParameter( "@RoleCode" ,SqlDbType.NVarChar,50)
                                ,new SqlParameter( "@RoleMapCode" ,SqlDbType.NVarChar,50)
                                ,new SqlParameter( "@State" ,SqlDbType.SmallInt)
                                ,new SqlParameter( "@Description" ,SqlDbType.NVarChar,100)
                                ,new SqlParameter( "@Creator" , SqlDbType.NVarChar, 50)
                                ,new SqlParameter( "@CreateTime" ,SqlDbType.DateTime)
                                ,new SqlParameter( "@Modifier" , SqlDbType.NVarChar, 50)
                                ,new SqlParameter( "@ModifyTime" ,SqlDbType.DateTime)     
                                ,new SqlParameter( "@OrganizationID" ,SqlDbType.Int)
							 };

            parameters[0].Value = role.RoleName;
            parameters[1].Value = role.ParentNodeID;
            parameters[2].Value = role.RoleCode;
            parameters[3].Value = role.RoleMapCode;
            parameters[4].Value = role.State;
            parameters[5].Value = role.Description;
            parameters[6].Value = role.Creator;
            parameters[7].Value = role.CreateTime;
            parameters[8].Value = role.Modifier;
            parameters[9].Value = role.ModifyTime;
            parameters[10].Value = role.OrganizationID;
            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parameters);
        }

        public void Update(object obj)
        {
            Role role = (Role)obj;

            string commandName = "Pr_Site_Role_Update";
            SqlParameter[]
                parameters = {	
                                 new SqlParameter( "@RoleName" ,SqlDbType.NVarChar,50)
                                ,new SqlParameter( "@ParentID" ,SqlDbType.Int)
                                ,new SqlParameter( "@RoleCode" ,SqlDbType.NVarChar,50)
                                ,new SqlParameter( "@RoleMapCode" ,SqlDbType.NVarChar,50)
                                ,new SqlParameter( "@State" ,SqlDbType.SmallInt)
                                ,new SqlParameter( "@Description" ,SqlDbType.NVarChar,100)
                                ,new SqlParameter( "@Creator" , SqlDbType.NVarChar, 50)
                                ,new SqlParameter( "@CreateTime" ,SqlDbType.DateTime)
                                ,new SqlParameter( "@Modifier" , SqlDbType.NVarChar, 50)
                                ,new SqlParameter( "@ModifyTime" ,SqlDbType.DateTime)
                                ,new SqlParameter( "@RoleID" ,SqlDbType.Int)
							 };

            parameters[0].Value = role.RoleName;
            parameters[1].Value = role.ParentNodeID;
            parameters[2].Value = role.RoleCode;
            parameters[3].Value = role.RoleMapCode;
            parameters[4].Value = role.State;
            parameters[5].Value = role.Description;
            parameters[6].Value = role.Creator;
            parameters[7].Value = role.CreateTime;
            parameters[8].Value = role.Modifier;
            parameters[9].Value = role.ModifyTime;
            parameters[10].Value = role.RoleID;

            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parameters);
        }

        public void Delete(object obj)
        {
            Role role = (Role)obj;

            string commandName = "Pr_Site_Role_Delete";
            SqlParameter[] parameters = { new SqlParameter("@RoleID", SqlDbType.Int) };
            parameters[0].Value = role.RoleID;

            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parameters);
        }

        public object Query(object id)
        {
            Int32 nodeID = (int)id;

            string sqlScriptFormat = SelectSql + " AND RoleID={0}";

            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.Text, string.Format(sqlScriptFormat, nodeID)).Tables[0];
            if (dt.Rows.Count == 0)
                return null;
            return Role.ConvertDataRowToRole(dt.Rows[0]);
        }

        public Object[] Query(string filter)
        {
            string sqlScriptFormat = SelectSql + " {0} order by RoleCode ";

            List<Role> list = new List<Role>();
            foreach (DataRow row in SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.Text, string.Format(sqlScriptFormat, filter)).Tables[0].Rows)
            {
                list.Add(Role.ConvertDataRowToRole(row));
            }

            return (Object[])list.ToArray();
        }

        public Object[] Query(int pageIndex, int pageSize, string filter, string orderBy, out int recordCount)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion

        /// <summary>
        /// 根据教师角色查询教师功能列表（管理教师）
        /// 功能类型: 
        ///     1:业务功能区;2:课程情况查询;3:作业情况查询;4:上网行为查询
        /// </summary>
        /// <param name="roleCode"></param>
        /// <returns></returns>
        public DataTable GetFunctionListByRoleCode(string roleCode, int functionType)
        {
            string commandName = "Pr_Site_GetFunctionListByRoleCode";

            SqlParameter[]
                parameters = {			
                                new SqlParameter( "@UserRoleCode" ,	SqlDbType.NVarChar,50),
                                new SqlParameter( "@FunctionType" ,	SqlDbType.Int)
							 };

            parameters[0].Value = roleCode;
            parameters[1].Value = functionType;

            return SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parameters).Tables[0];
        }

        /// <summary>
        /// 根据教师角色查询教师权限列表（管理教师）
        /// </summary>
        /// <param name="roleCode"></param>
        /// <returns></returns>
        public DataTable GetPageUrlByRoleCode(string roleCode)
        {
            string commandName = "Pr_Site_GetPageUrlByRoleCode";

            SqlParameter[]
                parameters = {			
                                new SqlParameter( "@UserRoleCode" ,	SqlDbType.NVarChar,50)
							 };

            parameters[0].Value = roleCode;

            return SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parameters).Tables[0];
        }

        /// <summary>
        /// 根据教师角色查询教师权限列表（管理教师）
        /// </summary>
        /// <param name="roleCode"></param>
        /// <returns></returns>
        public DataTable GetPageUrlByUserID(int userID)
        {
            string commandName = "Pr_Site_GetPageUrlByUserID";

            SqlParameter[]
                parameters = {			
                                new SqlParameter( "@UserID" ,	SqlDbType.Int)
							 };

            parameters[0].Value = userID;

            return SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parameters).Tables[0];
        }
    }
}
