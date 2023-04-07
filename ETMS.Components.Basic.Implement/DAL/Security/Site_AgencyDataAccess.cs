using ETMS.Utility.Data;
using System.Data;
using System.Data.SqlClient;
using ETMS.Components.Basic.API.Entity.Security;

namespace ETMS.Components.Basic.Implement.DAL.Security
{
    public class Site_AgencyDataAccess
    {
        public int Insert(Site_Agency entity)
        {
            string commandName = "[Pr_Site_Agency_Insert]";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@AgencyCode", SqlDbType.NVarChar, 20),
                    new SqlParameter("@AgencyName", SqlDbType.NVarChar, 50),
                    new SqlParameter("@Status", SqlDbType.Int),
                    new SqlParameter("@OrgID", SqlDbType.Int),
                    new SqlParameter("@CreateTime", SqlDbType.DateTime),
                    new SqlParameter("@CreateUserID", SqlDbType.Int),
                    new SqlParameter("@CreateUser", SqlDbType.NVarChar, 50),
                    new SqlParameter("@ModifyTime", SqlDbType.DateTime),
                    new SqlParameter("@ModifyUser", SqlDbType.NVarChar, 50),
                    };
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
            }
            parms[0].Value = entity.AgencyCode;
            parms[1].Value = entity.AgencyName;
            parms[2].Value = entity.Status;
            parms[3].Value = entity.OrgID;
            parms[4].Value = entity.CreateTime;
            parms[5].Value = entity.CreateUserID;
            parms[6].Value = entity.CreateUser;
            parms[7].Value = entity.ModifyTime;
            parms[8].Value = entity.ModifyUser;

            #endregion
            return SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
        }

        public int Update(Site_Agency entity)
        {
            string commandName = "[Pr_Site_Agency_Update]";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@AgencyCode", SqlDbType.NVarChar, 20),
                    new SqlParameter("@AgencyName", SqlDbType.NVarChar, 50),
                    new SqlParameter("@Status", SqlDbType.Int),                  
                    new SqlParameter("@ModifyTime", SqlDbType.DateTime),
                    new SqlParameter("@ModifyUser", SqlDbType.NVarChar, 50),
                    new SqlParameter("@AgencyID", SqlDbType.Int)
                    };
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
            }
            parms[0].Value = entity.AgencyCode;
            parms[1].Value = entity.AgencyName;
            parms[2].Value = entity.Status;
            parms[3].Value = entity.ModifyTime;
            parms[4].Value = entity.ModifyUser;
            parms[5].Value = entity.AgencyID;

            #endregion
            return SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
        }

        public int Delete(int agencyID)
        {
            string command = "DELETE FROM dbo.Site_Agency WHERE AgencyID = @AgencyID";
            #region Parameters
            SqlParameter[] parms  = new SqlParameter[] {
                    new SqlParameter("@AgencyID", SqlDbType.Int)
                    };

            parms[0].Value = agencyID;

            #endregion
            return SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.Text, command, parms);
        }

        public DataTable GetPageList(int orgID)
        {
            string command = @"SELECT [AgencyID]
                              ,[AgencyCode]
                              ,[AgencyName]
                              ,[Status]
                              ,[OrgID]
                              ,[CreateTime]
                              ,[CreateUserID]
                              ,[CreateUser]
                              ,[ModifyTime]
                              ,[ModifyUser]
                          FROM[Site_Agency]
                          WHERE OrgID = @OrgID
                          ORDER BY CreateTime DESC";
            SqlParameter[] parms = new SqlParameter[] {new SqlParameter("@OrgID", SqlDbType.Int)};

            parms[0].Value = orgID;
            return SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.Text, command, parms).Tables[0];
        }
    }
}
