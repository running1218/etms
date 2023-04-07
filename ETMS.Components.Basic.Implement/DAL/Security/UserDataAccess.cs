using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using ETMS.Utility.Data;

namespace ETMS.Components.Basic.Implement.DAL.Security
{
    public class UserDataAccess : ETMS.Components.Basic.Implement.DAL.Common.IDataAccess
    {
        private static string SelectSql = @"
SELECT [UserID]
      ,[LoginName]
      ,[RealName]
      ,[PassWord]
      ,[Email]
      ,[Telphone]
      ,[IsSysAccount]
      ,[OfficeTelphone]
      ,[MobilePhone]
      ,[Description]
      ,[Status]
      ,[OrganizationID]
      ,[DepartmentID]
      ,[Creator]
      ,[CreateTime]
      ,[Modifier]
      ,[ModifyTime]
      ,[PhotoUrl]      
      ,[SexTypeID]
      ,[Birthday]
      ,[Identity]
      ,[PoliticsTypeID]
      ,[TitleName]
  FROM [dbo].[Site_User] WHERE 1=1 ";

        #region IDataAccess 成员

        /// <summary>
        /// 增加
        /// </summary>
        public void Add(object obj)
        {
            ETMS.Components.Basic.API.Entity.Security.User site_User = (ETMS.Components.Basic.API.Entity.Security.User)obj;
            string commandName = "dbo.Pr_Site_User_Insert";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {	new SqlParameter("@UserID", SqlDbType.Int, 4, ParameterDirection.Output, false, 0, 0, String.Empty, DataRowVersion.Default, null),
				
					new SqlParameter("@LoginName", SqlDbType.NVarChar, 100),
					new SqlParameter("@RealName", SqlDbType.NVarChar, 100),
					new SqlParameter("@PassWord", SqlDbType.NVarChar, 100),
					new SqlParameter("@Email", SqlDbType.NVarChar, 100),
					new SqlParameter("@Telphone", SqlDbType.NVarChar, 100),
					new SqlParameter("@IsSysAccount", SqlDbType.Bit),
					new SqlParameter("@OfficeTelphone", SqlDbType.NVarChar, 40),
					new SqlParameter("@MobilePhone", SqlDbType.NVarChar, 40),
					new SqlParameter("@Description", SqlDbType.NVarChar),
					new SqlParameter("@Status", SqlDbType.Int),
					new SqlParameter("@OrganizationID", SqlDbType.Int),
					new SqlParameter("@DepartmentID", SqlDbType.Int),
					new SqlParameter("@Creator", SqlDbType.NVarChar, 100),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@Modifier", SqlDbType.NVarChar, 100),
					new SqlParameter("@ModifyTime", SqlDbType.DateTime),
					new SqlParameter("@PhotoUrl", SqlDbType.NVarChar, 1000),
					new SqlParameter("@SexTypeID", SqlDbType.Int),
					new SqlParameter("@Birthday", SqlDbType.DateTime),
					new SqlParameter("@Identity", SqlDbType.NVarChar, 60),
					new SqlParameter("@PoliticsTypeID", SqlDbType.Int),
					new SqlParameter("@TitleName", SqlDbType.NVarChar, 200)
					};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
            }

            if (site_User.LoginName != null) { parms[1].Value = site_User.LoginName; } else { parms[1].Value = DBNull.Value; }
            if (site_User.RealName != null) { parms[2].Value = site_User.RealName; } else { parms[2].Value = DBNull.Value; }
            if (site_User.PassWord != null) { parms[3].Value = site_User.PassWord; } else { parms[3].Value = DBNull.Value; }
            if (site_User.Email != null) { parms[4].Value = site_User.Email; } else { parms[4].Value = DBNull.Value; }
            if (site_User.Telphone != null) { parms[5].Value = site_User.Telphone; } else { parms[5].Value = DBNull.Value; }
            parms[6].Value = site_User.IsSysAccount;
            if (site_User.OfficeTelphone != null) { parms[7].Value = site_User.OfficeTelphone; } else { parms[7].Value = DBNull.Value; }
            if (site_User.MobilePhone != null) { parms[8].Value = site_User.MobilePhone; } else { parms[8].Value = DBNull.Value; }
            if (site_User.Description != null) { parms[9].Value = site_User.Description; } else { parms[9].Value = DBNull.Value; }
            parms[10].Value = site_User.Status;
            parms[11].Value = site_User.OrganizationID;
            if (-1 != site_User.DepartmentID && site_User.DepartmentID != default(int)) { parms[12].Value = site_User.DepartmentID; } else { parms[12].Value = DBNull.Value; }
            if (site_User.Creator != null) { parms[13].Value = site_User.Creator; } else { parms[13].Value = DBNull.Value; }
            parms[14].Value = site_User.CreateTime;
            if (site_User.Modifier != null) { parms[15].Value = site_User.Modifier; } else { parms[15].Value = DBNull.Value; }
            parms[16].Value = site_User.ModifyTime;
            if (site_User.PhotoUrl != null) { parms[17].Value = site_User.PhotoUrl; } else { parms[17].Value = DBNull.Value; }
            parms[18].Value = site_User.SexTypeID;
            parms[19].Value = site_User.Birthday;
            if (site_User.Identity != null) { parms[20].Value = site_User.Identity; } else { parms[20].Value = DBNull.Value; }
            parms[21].Value = site_User.PoliticsTypeID;
            if (site_User.TitleName != null) { parms[22].Value = site_User.TitleName; } else { parms[22].Value = DBNull.Value; }
            #endregion
            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);

            site_User.UserID = (Int32)parms[0].Value;

        }

        public void Update(object obj)
        {
            ETMS.Components.Basic.API.Entity.Security.User site_User = (ETMS.Components.Basic.API.Entity.Security.User)obj;


            string commandName = "dbo.Pr_Site_User_Update";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@UserID", SqlDbType.Int),
					new SqlParameter("@LoginName", SqlDbType.NVarChar, 100),
					new SqlParameter("@RealName", SqlDbType.NVarChar, 100),
					new SqlParameter("@PassWord", SqlDbType.NVarChar, 100),
					new SqlParameter("@Email", SqlDbType.NVarChar, 100),
					new SqlParameter("@Telphone", SqlDbType.NVarChar, 100),
					new SqlParameter("@IsSysAccount", SqlDbType.Bit),
					new SqlParameter("@OfficeTelphone", SqlDbType.NVarChar, 40),
					new SqlParameter("@MobilePhone", SqlDbType.NVarChar, 40),
					new SqlParameter("@Description", SqlDbType.NVarChar),
					new SqlParameter("@Status", SqlDbType.Int),
					new SqlParameter("@OrganizationID", SqlDbType.Int),
					new SqlParameter("@DepartmentID", SqlDbType.Int),
					new SqlParameter("@Creator", SqlDbType.NVarChar, 100),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@Modifier", SqlDbType.NVarChar, 100),
					new SqlParameter("@ModifyTime", SqlDbType.DateTime),
					new SqlParameter("@PhotoUrl", SqlDbType.NVarChar, 1000),
					new SqlParameter("@SexTypeID", SqlDbType.Int),
					new SqlParameter("@Birthday", SqlDbType.DateTime),
					new SqlParameter("@Identity", SqlDbType.NVarChar, 60),
					new SqlParameter("@PoliticsTypeID", SqlDbType.Int),
					new SqlParameter("@TitleName", SqlDbType.NVarChar, 200)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
            }

            parms[0].Value = site_User.UserID;
            if (site_User.LoginName != null) { parms[1].Value = site_User.LoginName; } else { parms[1].Value = DBNull.Value; }
            if (site_User.RealName != null) { parms[2].Value = site_User.RealName; } else { parms[2].Value = DBNull.Value; }
            if (site_User.PassWord != null) { parms[3].Value = site_User.PassWord; } else { parms[3].Value = DBNull.Value; }
            if (site_User.Email != null) { parms[4].Value = site_User.Email; } else { parms[4].Value = DBNull.Value; }
            if (site_User.Telphone != null) { parms[5].Value = site_User.Telphone; } else { parms[5].Value = DBNull.Value; }
            parms[6].Value = site_User.IsSysAccount;
            if (site_User.OfficeTelphone != null) { parms[7].Value = site_User.OfficeTelphone; } else { parms[7].Value = DBNull.Value; }
            if (site_User.MobilePhone != null) { parms[8].Value = site_User.MobilePhone; } else { parms[8].Value = DBNull.Value; }
            if (site_User.Description != null) { parms[9].Value = site_User.Description; } else { parms[9].Value = DBNull.Value; }
            parms[10].Value = site_User.Status;
            parms[11].Value = site_User.OrganizationID;
            if (-1 != site_User.DepartmentID && site_User.DepartmentID != default(int)) { parms[12].Value = site_User.DepartmentID; } else { parms[12].Value = DBNull.Value; }
            if (site_User.Creator != null) { parms[13].Value = site_User.Creator; } else { parms[13].Value = DBNull.Value; }
            parms[14].Value = site_User.CreateTime;
            if (site_User.Modifier != null) { parms[15].Value = site_User.Modifier; } else { parms[15].Value = DBNull.Value; }
            parms[16].Value = site_User.ModifyTime;
            if (site_User.PhotoUrl != null) { parms[17].Value = site_User.PhotoUrl; } else { parms[17].Value = DBNull.Value; }
            parms[18].Value = site_User.SexTypeID;
            parms[19].Value = site_User.Birthday;
            if (site_User.Identity != null) { parms[20].Value = site_User.Identity; } else { parms[20].Value = DBNull.Value; }
            parms[21].Value = site_User.PoliticsTypeID;
            if (site_User.TitleName != null) { parms[22].Value = site_User.TitleName; } else { parms[22].Value = DBNull.Value; }
            #endregion
            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
        }

        public void Delete(object obj)
        {
            ETMS.Components.Basic.API.Entity.Security.User user = (ETMS.Components.Basic.API.Entity.Security.User)obj;

            string commandName = "dbo.Pr_Site_User_Delete";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@UserID", SqlDbType.Int)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
            }

            parms[0].Value = user.UserID;

            #endregion
            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
        }

        public object Query(object id)
        {
            Int32 nodeID = (int)id;

            string sqlScriptFormat = SelectSql + " AND UserID={0}";

            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.Text, string.Format(sqlScriptFormat, nodeID)).Tables[0];
            if (dt.Rows.Count == 0)
                return null;
            return ETMS.Components.Basic.API.Entity.Security.User.ConvertDataRowToUser(dt.Rows[0]);
        }

        public Object[] Query(string filter)
        {
            string sqlScriptFormat = SelectSql + " {0} ";

            List<ETMS.Components.Basic.API.Entity.Security.User> list = new List<ETMS.Components.Basic.API.Entity.Security.User>();
            foreach (DataRow row in SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.Text, string.Format(sqlScriptFormat, filter)).Tables[0].Rows)
            {
                list.Add(ETMS.Components.Basic.API.Entity.Security.User.ConvertDataRowToUser(row));
            }
            return (Object[])list.ToArray();
        }

        public Object[] Query(int pageIndex, int pageSize, string filter, string orderBy, out int recordCount)
        {
            List<ETMS.Components.Basic.API.Entity.Security.User> users = new List<ETMS.Components.Basic.API.Entity.Security.User>();

            string commandName = "dbo.Pr_Site_User_GetPagedList";
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
                users.Add(ETMS.Components.Basic.API.Entity.Security.User.ConvertDataRowToUser(row));
            }

            recordCount = (int)parms[4].Value;
            return (Object[])users.ToArray();
        }
        /// <summary>
        /// 获取学员个人中心基本信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataTable GetUserBaseData(int id)
        {           
            string commandName = "[Pr_Site_User_GetUserBaseData]";
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@UserID", SqlDbType.Int)                 
                };
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }
            parms[0].Value = id;
           return SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];         
         
        }


        #endregion
    }
}
