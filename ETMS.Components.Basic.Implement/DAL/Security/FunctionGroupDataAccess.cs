using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using ETMS.Utility.Data;


using ETMS.Components.Basic.API.Entity.Security;
namespace ETMS.Components.Basic.Implement.DAL.Security
{
    public class FunctionGroupDataAccess : ETMS.Components.Basic.Implement.DAL.Common.IDataAccess
    {
        private static string SelectSql = @"
SELECT [GroupID]
      ,[GroupName]
      ,[ParentID]
      ,[GroupCode]
      ,[State]
      ,[Description]
      ,[Creator]
      ,[CreateTime]
      ,[Modifier]
      ,[ModifyTime]
      ,[OrderNo]
      ,[ComponentID]
  FROM  [dbo].[Site_FunctionGroup] WHERE 1=1 ";

        #region IDataAccess 成员

        public void Add(object obj)
        {
            FunctionGroup functionGroup = (FunctionGroup)obj;

            string commandName = "Pr_Site_FunctionGroup_Add";
            SqlParameter[]
                parameters = {	
                                 new SqlParameter( "@GroupName" ,SqlDbType.NVarChar,50)
                                ,new SqlParameter( "@ParentID" ,SqlDbType.Int)
                                ,new SqlParameter( "@GroupCode" ,SqlDbType.NVarChar,50)
                                ,new SqlParameter( "@State" ,SqlDbType.SmallInt)
                                ,new SqlParameter( "@Description" ,SqlDbType.NVarChar,100)
                                ,new SqlParameter( "@Creator" , SqlDbType.NVarChar, 50)
                                ,new SqlParameter( "@CreateTime" ,SqlDbType.DateTime)
                                ,new SqlParameter( "@Modifier" , SqlDbType.NVarChar, 50)
                                ,new SqlParameter( "@ModifyTime" ,SqlDbType.DateTime)
                                ,new SqlParameter( "@OrderNo" ,SqlDbType.Int)
					            ,new SqlParameter( "@ComponentID", SqlDbType.VarChar,50)
							 };

            parameters[0].Value = functionGroup.GroupName;
            parameters[1].Value = functionGroup.ParentNodeID;
            parameters[2].Value = functionGroup.GroupCode;
            parameters[3].Value = functionGroup.State;
            parameters[4].Value = functionGroup.Description;
            parameters[5].Value = (string)functionGroup.Creator;
            parameters[6].Value = functionGroup.CreateTime;
            parameters[7].Value = (string)functionGroup.Modifier;
            parameters[8].Value = functionGroup.ModifyTime;
            parameters[9].Value = functionGroup.OrderNo;
            parameters[10].Value = functionGroup.ComponentID;

            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parameters);
        }

        public void Update(object obj)
        {
            FunctionGroup functionGroup = (FunctionGroup)obj;

            string commandName = "Pr_Site_FunctionGroup_Update";
            SqlParameter[]
                parameters = {	
                                 new SqlParameter( "@GroupName" ,SqlDbType.NVarChar,50)
                                ,new SqlParameter( "@ParentID" ,SqlDbType.Int)
                                ,new SqlParameter( "@GroupCode" ,SqlDbType.NVarChar,50)
                                ,new SqlParameter( "@State" ,SqlDbType.SmallInt)
                                ,new SqlParameter( "@Description" ,SqlDbType.NVarChar,100)
                                ,new SqlParameter( "@Creator" , SqlDbType.NVarChar, 50)
                                ,new SqlParameter( "@CreateTime" ,SqlDbType.DateTime)
                                ,new SqlParameter( "@Modifier" , SqlDbType.NVarChar, 50)
                                ,new SqlParameter( "@ModifyTime" ,SqlDbType.DateTime)
                                ,new SqlParameter( "@GroupID" ,SqlDbType.Int)
                                ,new SqlParameter( "@OrderNo" ,SqlDbType.Int)
					            ,new SqlParameter( "@ComponentID", SqlDbType.VarChar,50)
							 };

            parameters[0].Value = functionGroup.GroupName;
            parameters[1].Value = functionGroup.ParentNodeID;
            parameters[2].Value = functionGroup.GroupCode;
            parameters[3].Value = functionGroup.State;
            parameters[4].Value = functionGroup.Description;
            parameters[5].Value = (string)functionGroup.Creator;
            parameters[6].Value = functionGroup.CreateTime;
            parameters[7].Value = (string)functionGroup.Modifier;
            parameters[8].Value = functionGroup.ModifyTime;
            parameters[9].Value = functionGroup.GroupID;
            parameters[10].Value = functionGroup.OrderNo;
            parameters[11].Value = functionGroup.ComponentID;


            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parameters);
        }

        public void Delete(object obj)
        {
            FunctionGroup functionGroup = (FunctionGroup)obj;

            string commandName = "Pr_Site_FunctionGroup_Delete";
            SqlParameter[] parameters = { new SqlParameter("@GroupID", SqlDbType.Int) };
            parameters[0].Value = functionGroup.GroupID;

            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parameters);
        }

        public object Query(object id)
        {
            int groupID = (Int32)id;
            string sqlScriptFormat = SelectSql + " AND GroupID={0}";

            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.Text, string.Format(sqlScriptFormat, groupID)).Tables[0];
            if (dt.Rows.Count == 0)
                return null;
            return this.ConvertDataRowToRole(dt.Rows[0]);
        }

        public Object[] Query(string filter)
        {
            string sqlScriptFormat = SelectSql + " {0} order by OrderNO asc, GroupCode ASC";

            List<FunctionGroup> list = new List<FunctionGroup>();
            foreach (DataRow row in SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.Text, string.Format(sqlScriptFormat, filter)).Tables[0].Rows)
            {
                list.Add(this.ConvertDataRowToRole(row));
            }
            return (Object[])list.ToArray();
        }

        public Object[] Query(int pageIndex, int pageSize, string filter, string orderBy, out int recordCount)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion

        #region ORM
        /// <summary>
        /// 行数据转换为对象实体
        /// </summary>
        /// <param name="row">tb_e_PlantRole行数据</param>
        /// <returns>Role对象实体</returns>
        public FunctionGroup ConvertDataRowToRole(DataRow row)
        {
            FunctionGroup entity = new FunctionGroup();
            entity.GroupID = (int)row["GroupID"];
            entity.GroupName = (string)row["GroupName"];
            entity.ParentNodeID = (int)row["ParentID"];
            entity.GroupCode = (string)row["GroupCode"];
            entity.State = Convert.ToInt32(row["State"]);
            entity.Description = (string)row["Description"];
            entity.Creator = (string)row["Creator"];
            entity.CreateTime = (DateTime)row["CreateTime"];
            entity.Modifier = (string)row["Modifier"];
            entity.ModifyTime = (DateTime)row["ModifyTime"];
            entity.OrderNo = (int)row["OrderNo"];
            entity.ComponentID = Convert.ToString(row["ComponentID"]);
            return entity;
        }
        #endregion
    }
}
