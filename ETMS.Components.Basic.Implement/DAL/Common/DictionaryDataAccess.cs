using System.Data;

using System.Data.SqlClient;
using ETMS.Utility.Data;
using ETMS.Components.Basic.API.Entity.Common;

namespace ETMS.Components.Basic.Implement.DAL.Common
{
    public class DictionaryDataAccess
    {
        public void DictionaryEdit(DictionaryParm parm)
        {
            string commandName = "[dbo].[Pr_Common_DictionaryEdit]";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@code", SqlDbType.NVarChar, 50),
					new SqlParameter("@value", SqlDbType.NVarChar, 50),
                    new SqlParameter("@Remark", SqlDbType.NVarChar, 100),
					new SqlParameter("@tableName", SqlDbType.NVarChar,30)
					};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
            }
            parms[0].Value = parm.ColumnCodeValue;
            parms[1].Value = parm.ColumnNameValue;
            parms[2].Value = parm.Remark;
            parms[3].Value = parm.TableEnglishName;

            #endregion
            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
        }

        public void DictionaryDelete(string tableName, string columnCode)
        {
            string commandName = "[dbo].[Pr_Common_DictionaryDelete]";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					 new SqlParameter("@code", SqlDbType.NVarChar,50),
                    new SqlParameter("@tableName", SqlDbType.NVarChar,30)
					};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
            }
            parms[0].Value = columnCode;
            parms[1].Value = tableName;

            #endregion
            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
        }
        public DataTable GetDictionaryBycode(string tableName,string columnCode)
        {
            string commandName = "[dbo].[Pr_Common_GetDictionaryByCode]";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
	                new SqlParameter("@tableName", SqlDbType.NVarChar,30),
                    new SqlParameter("@ColumnCode", SqlDbType.NVarChar,50)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }
            parms[0].Value = tableName;
            parms[1].Value = columnCode;

            #endregion

            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];
            return dt;
        }
        public DataTable GetDictionaryList(string tableName)
        {
            string commandName = "[dbo].[Pr_Common_GetDictionaryList]";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {	
                    new SqlParameter("@tableName", SqlDbType.NVarChar,30),
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }
            parms[0].Value = tableName;
            
            #endregion

            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];
            return dt;
        }

        public void SavaCatalog(int id, string code, string name)
        {
            string commandName = "[dbo].[Pr_Dic_Sys_CourseType_Save]";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@ID", SqlDbType.Int),
                    new SqlParameter("@Code", SqlDbType.NVarChar, 80),
                    new SqlParameter("@Name", SqlDbType.NVarChar, 100)                   
                    };
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
            }
            parms[0].Value = id;
            parms[1].Value = code;
            parms[2].Value = name;            

            #endregion
            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
        }

        public void SavaSpecialty(int id, string code, string name)
        {
            string commandName = "[dbo].[Pr_Dic_Sys_Specialty_Save]";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@ID", SqlDbType.Int),
                    new SqlParameter("@Code", SqlDbType.NVarChar, 80),
                    new SqlParameter("@Name", SqlDbType.NVarChar, 100)
                    };
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
            }
            parms[0].Value = id;
            parms[1].Value = code;
            parms[2].Value = name;

            #endregion
            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
        }
    }
}
