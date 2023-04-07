using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace ETMS.Utility.Data
{
    public class SqlUtility
    {

        #region 新增

        /// <summary>
        /// 新增
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="model">实体数据</param>
        /// <param name="ignoreColumns">忽略字段,例如 ID,Name</param>
        /// <param name="commandTimeout">超时时长</param>
        public static int Insert<T>(string connectString, T model, string ignoreColumns = null, int? commandTimeout = null)
        {
            var tModel = typeof(T);
            string tableName = GetTableName(tModel);
            return Insert(connectString, model, tableName, ignoreColumns, commandTimeout);
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="table">表名</param>
        /// <param name="ignoreColumns">忽略字段(ID,Name)</param>
        /// <param name="commandTimeout">超时时长</param>
        public static int Insert(string connectString, dynamic data, string table, string ignoreColumns = null, int? commandTimeout = null)
        {
            var obj = data as object;

            var insertPropertyInfos = GetPropertyInfos(obj);

            if (!string.IsNullOrEmpty(ignoreColumns))
            {
                var ignores = ignoreColumns.Split(',');
                insertPropertyInfos = insertPropertyInfos.Where(p => !ignores.Contains(p.Name, StringComparer.OrdinalIgnoreCase)).ToList();

            }
            var columns = string.Join(",", insertPropertyInfos.Select(p => "["+p.Name+"]"));
            var values = string.Join(",", insertPropertyInfos.Select(p => "@" + p.Name));

            var sql = string.Concat("insert into ", table, "(", columns, ")", " values (", values, ")");



            return SqlHelper.ExecuteNonQuery(connectString, CommandType.Text, sql, GetSqlParameters(data, insertPropertyInfos));

        }

        #region 事务
        /// <summary>
        /// 新增
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="model">实体数据</param>
        /// <param name="ignoreColumns">忽略字段,例如 ID,Name</param>
        /// <param name="commandTimeout">超时时长</param>
        public static int Insert<T>(SqlTransaction sqlTransaction, T model, string ignoreColumns = null, int? commandTimeout = null)
        {
            var tModel = typeof(T);
            string tableName = GetTableName(tModel);
            return Insert(sqlTransaction, model, tableName, ignoreColumns, commandTimeout);
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="sqlTransaction"></param>
        /// <param name="data">数据</param>
        /// <param name="table">表名</param>
        /// <param name="ignoreColumns">忽略字段(ID,Name)</param>
        /// <param name="commandTimeout">超时时长</param>
        public static int Insert(SqlTransaction sqlTransaction, dynamic data, string table, string ignoreColumns = null, int? commandTimeout = null)
        {
            var obj = data as object;

            var insertPropertyInfos = GetPropertyInfos(obj);

            if (!string.IsNullOrEmpty(ignoreColumns))
            {
                var ignores = ignoreColumns.Split(',');
                insertPropertyInfos = insertPropertyInfos.Where(p => !ignores.Contains(p.Name, StringComparer.OrdinalIgnoreCase)).ToList();

            }
            var columns = string.Join(",", insertPropertyInfos.Select(p => p.Name));
            var values = string.Join(",", insertPropertyInfos.Select(p => "@" + p.Name));

            var sql = string.Concat("insert into ", table, "(", columns, ")", " values (", values, ")");



            return SqlHelper.ExecuteNonQuery(sqlTransaction, CommandType.Text, sql, GetSqlParameters(data, insertPropertyInfos));

        }
        #endregion

        #endregion

        #region 更新
        /// <summary>
        /// 更新
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="model">实体</param> 
        /// <param name="commandTimeout">超时时长</param>
        /// <returns>受影响条数</returns>
        public static int Update<T>(string connectString, T model, int? commandTimeout = null)
        {
            var tModel = typeof(T);
            string tableName = GetTableName(tModel);
            return Update(connectString, model, tableName, commandTimeout);
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="data">实体</param>
        /// <param name="table">表名</param>
        /// <param name="commandTimeout">超时时长</param>
        /// <returns>受影响条数</returns>
        public static int Update(string connectString, dynamic data, string table, int? commandTimeout = null)
        {
            var obj = data as object;



            var updatePropertyInfos = GetPropertyInfos(obj).Where(x => x.GetValue(obj) != null).ToList();

            var updateProperties = updatePropertyInfos.Select(p => p.Name);

            var updateFields = string.Join(",", updateProperties.Select(p => p + " = @" + p));
            var keyPropertys =
                updatePropertyInfos.Where(
                    x => x.CustomAttributes.Count(y => y.AttributeType.Name == "KeyAttribute") > 0).Select(x => x.Name).ToList();

            var whereFields = " where ";
            for (int i = 0; i < keyPropertys.Count; i++)
            {
                if (i > 0)
                {
                    whereFields += " and ";
                }
                whereFields += keyPropertys[i] + "=@" + keyPropertys[i];
            }

            var sql = string.Concat("update ", table, " set ", updateFields, whereFields);

            return SqlHelper.ExecuteNonQuery(connectString, CommandType.Text, sql, GetSqlParameters(data, updatePropertyInfos));

        }

        #region 事务
        /// <summary>
        /// 更新
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="model">实体</param> 
        /// <param name="commandTimeout">超时时长</param>
        /// <returns>受影响条数</returns>
        public static int Update<T>(SqlTransaction sqlTransaction, T model, int? commandTimeout = null)
        {
            var tModel = typeof(T);
            string tableName = GetTableName(tModel);
            return Update(sqlTransaction, model, tableName, commandTimeout);
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="data">实体</param>
        /// <param name="table">表名</param>
        /// <param name="commandTimeout">超时时长</param>
        /// <returns>受影响条数</returns>
        public static int Update(SqlTransaction sqlTransaction, dynamic data, string table, int? commandTimeout = null)
        {
            var obj = data as object;



            var updatePropertyInfos = GetPropertyInfos(obj).Where(x => x.GetValue(obj) != null).ToList();

            var updateProperties = updatePropertyInfos.Select(p => p.Name);

            var updateFields = string.Join(",", updateProperties.Select(p => p + " = @" + p));
            var keyPropertys =
                updatePropertyInfos.Where(
                    x => x.CustomAttributes.Count(y => y.AttributeType.Name == "KeyAttribute") > 0).Select(x => x.Name).ToList();

            var whereFields = " where ";
            for (int i = 0; i < keyPropertys.Count; i++)
            {
                if (i > 0)
                {
                    whereFields += " and ";
                }
                whereFields += keyPropertys[i] + "=@" + keyPropertys[i];
            }

            var sql = string.Concat("update ", table, " set ", updateFields, whereFields);

            return SqlHelper.ExecuteNonQuery(sqlTransaction, CommandType.Text, sql, GetSqlParameters(data, updatePropertyInfos));

        }
        #endregion

        #endregion


        #region 删除
        /// <summary>
        /// 删除
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="model">实体</param> 
        /// <param name="commandTimeout">超时时长</param>
        /// <returns>受影响条数</returns>
        public static int Delete<T>(string connectString, T model, int? commandTimeout = null)
        {
            var tModel = typeof(T);
            string tableName = GetTableName(tModel);
            return Delete(connectString, model, tableName, commandTimeout);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="data">实体</param>
        /// <param name="table">表名</param>
        /// <param name="commandTimeout">超时时长</param>
        /// <returns>受影响条数</returns>
        public static int Delete(string connectString,dynamic data, string table, int? commandTimeout = null)
        {
            var obj = data as object;


            var deletePropertyInfos = GetPropertyInfos(obj).Where(x => x.GetValue(obj) != null).ToList();

            var keyPropertys =
                deletePropertyInfos.Where(
                    x => x.CustomAttributes.Count(y => y.AttributeType.Name == "KeyAttribute") > 0).Select(x => x.Name).ToList();

            var whereFields = " where ";
            for (int i = 0; i < keyPropertys.Count; i++)
            {
                if (i > 0)
                {
                    whereFields += " and ";
                }
                whereFields += keyPropertys[i] + "=@" + keyPropertys[i];
            }


            //var keyProperty =
            //    deletePropertyInfos.FirstOrDefault(
            //        x => x.CustomAttributes.Count(y => y.AttributeType.Name == "KeyAttribute") > 0)?.Name;

            //var whereFields = " where " + keyProperty + "=@" + keyProperty;

            var sql = string.Concat("delete ", table, whereFields);
            return SqlHelper.ExecuteNonQuery(connectString, CommandType.Text, sql, GetSqlParameters(data, deletePropertyInfos));

        }

        #region 事务
        /// <summary>
        /// 删除
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="model">实体</param> 
        /// <param name="commandTimeout">超时时长</param>
        /// <returns>受影响条数</returns>
        public static int Delete<T>(SqlTransaction sqlTransaction, T model, int? commandTimeout = null)
        {
            var tModel = typeof(T);
            string tableName = GetTableName(tModel);
            return Delete(sqlTransaction, model, tableName, commandTimeout);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="data">实体</param>
        /// <param name="table">表名</param>
        /// <param name="commandTimeout">超时时长</param>
        /// <returns>受影响条数</returns>
        public static int Delete(SqlTransaction sqlTransaction, dynamic data, string table, int? commandTimeout = null)
        {
            var obj = data as object;


            var deletePropertyInfos = GetPropertyInfos(obj).Where(x => x.GetValue(obj) != null).ToList();

            var keyPropertys =
                deletePropertyInfos.Where(
                    x => x.CustomAttributes.Count(y => y.AttributeType.Name == "KeyAttribute") > 0).Select(x => x.Name).ToList();

            var whereFields = " where ";
            for (int i = 0; i < keyPropertys.Count; i++)
            {
                if (i > 0)
                {
                    whereFields += " and ";
                }
                whereFields += keyPropertys[i] + "=@" + keyPropertys[i];
            }


            //var keyProperty =
            //    deletePropertyInfos.FirstOrDefault(
            //        x => x.CustomAttributes.Count(y => y.AttributeType.Name == "KeyAttribute") > 0)?.Name;

            //var whereFields = " where " + keyProperty + "=@" + keyProperty;

            var sql = string.Concat("delete ", table, whereFields);
            return SqlHelper.ExecuteNonQuery(sqlTransaction, CommandType.Text, sql, GetSqlParameters(data, deletePropertyInfos));

        }
        #endregion
        #endregion

        #region 查询

        /// <summary>
        /// 删除
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="model">实体</param> 
        /// <param name="commandTimeout">超时时长</param>
        /// <returns>受影响条数</returns>
        public static T GetById<T>(string connectString, dynamic model, int? commandTimeout = null)
        {
            var tModel = typeof(T);
            string tableName = GetTableName(tModel);
            return GetById<T>(connectString, model, tableName, commandTimeout);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="data">实体</param>
        /// <param name="table">表名</param>
        /// <param name="commandTimeout">超时时长</param>
        /// <returns>受影响条数</returns>
        public static T GetById<T>(string connectString, dynamic data, string table, int? commandTimeout = null)
        {
            var obj = data as object;


            var propertyInfos = GetPropertyInfos(obj).Where(x => x.GetValue(obj) != null).ToList();
            var keyProperty =
                propertyInfos.FirstOrDefault(
                    x => x.CustomAttributes.Count(y => y.AttributeType.Name == "KeyAttribute") > 0)?.Name;

            var whereFields = " where " + keyProperty + "=@" + keyProperty;

            var sql = string.Concat("SELECT * From ", table, whereFields);

            DataTable dt = SqlHelper.ExecuteDataset(connectString, CommandType.Text, sql, GetSqlParameters(data, propertyInfos)).Tables[0];
            return dt.Rows.Count > 0 ? dt.Rows[0].ToEntity<T>() : default(T);

        }


        #region 分页sql语句
        /// <summary>
        /// 生成完整分页sql语句
        /// </summary>
        /// <param name="sql">查询语句</param>
        /// <param name="orderSql">排序字段</param>
        /// <returns>分页sql语句</returns>
        public static string GeneratePagedSql(string sql, string orderSql)
        {
            return string.Concat(@";WITH cte AS (
                                                    SELECT TOP (@PageIndex * @PageSize) *,
                                                    ROW_NUMBER() OVER(ORDER BY ", orderSql, @") AS [Sequency],
                                                    COUNT(*) OVER(PARTITION BY '') AS [TotalRecords] 
		                                            from ( ", sql, @" )t
                                                  )
                                     SELECT * FROM cte WHERE [Sequency] BETWEEN (@PageIndex - 1 ) * @PageSize + 1 AND @PageIndex * @PageSize
                                                      ORDER BY [Sequency]
          ");
        }

        #endregion

        #endregion

        #region 操作方法

        #region 类属性
        private readonly static ConcurrentDictionary<Type, List<PropertyInfo>> _paramCache = new ConcurrentDictionary<Type, List<PropertyInfo>>();
        /// <summary>
        /// 获取属性列表
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private static List<PropertyInfo> GetPropertyInfos(object obj)
        {
            if (obj == null)
            {
                return new List<PropertyInfo>();
            }

            List<PropertyInfo> properties;
            if (_paramCache.TryGetValue(obj.GetType(), out properties)) return properties.ToList();
            properties = obj.GetType().GetProperties(BindingFlags.GetProperty | BindingFlags.Instance | BindingFlags.Public).ToList();
            _paramCache[obj.GetType()] = properties;
            return properties;
        }
        /// <summary>
        /// 获取表面
        /// </summary>
        /// <param name="tModel"></param>
        /// <returns></returns>
        private static string GetTableName(Type tModel)
        {

            string tableName = tModel.Name;
            var tableAttribute = tModel.CustomAttributes.FirstOrDefault(x => x.AttributeType.Name == "TableAttribute");
            if (tableAttribute != null)
            {
                tableName = tableAttribute.ConstructorArguments[0].Value.ToString();
            }
            return tableName;
        }
        #endregion

        #region sql处理相关
        /// <summary>
        /// 生成SqlParameter
        /// </summary>
        /// <param name="data"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        private static SqlParameter[] GetSqlParameters(object data, List<PropertyInfo> list)
        {
            var parameters = new List<SqlParameter>();
            foreach (var insertPropertyInfo in list)
            {
                parameters.Add(SqlHelper.CreateInputSqlParameter("@" + insertPropertyInfo.Name, GetSqlDbType(insertPropertyInfo.PropertyType.ToString()), insertPropertyInfo.GetValue(data)));
            }
            return parameters.ToArray();
        }



        /// <summary>
        /// 通过属性类型，返回SqlDbType类型
        /// </summary>
        /// <param name="propertyTypeName"></param>
        /// <returns></returns>
        private static SqlDbType GetSqlDbType(string propertyTypeName)
        {

            if (Regex.IsMatch(propertyTypeName, "string", RegexOptions.IgnoreCase))
            {
                return SqlDbType.NVarChar;
            }
            if (Regex.IsMatch(propertyTypeName, "guid", RegexOptions.IgnoreCase))
            {
                return SqlDbType.UniqueIdentifier;
            }
            if (Regex.IsMatch(propertyTypeName, "datetime", RegexOptions.IgnoreCase))
            {
                return SqlDbType.DateTime;
            }
            if (Regex.IsMatch(propertyTypeName, "int32", RegexOptions.IgnoreCase))
            {
                return SqlDbType.Int;
            }
            if (Regex.IsMatch(propertyTypeName, "int64", RegexOptions.IgnoreCase))
            {
                return SqlDbType.BigInt;
            }
            if (Regex.IsMatch(propertyTypeName, "int16", RegexOptions.IgnoreCase))
            {
                return SqlDbType.SmallInt;
            }
            if (Regex.IsMatch(propertyTypeName, "int8", RegexOptions.IgnoreCase))
            {
                return SqlDbType.TinyInt;
            }
            if (Regex.IsMatch(propertyTypeName, "decimal", RegexOptions.IgnoreCase))
            {
                return SqlDbType.Decimal;
            }
            if (Regex.IsMatch(propertyTypeName, "bool", RegexOptions.IgnoreCase))
            {
                return SqlDbType.Bit;
            }

            return SqlDbType.NVarChar;
        }
        #endregion


        #endregion





    }
}
