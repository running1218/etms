using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using ETMS.Utility.Data;


using ETMS.Components.Basic.API.Entity.Security;
namespace ETMS.Components.Basic.Implement.DAL.Security
{
    public class OrganizationDataAccess : ETMS.Components.Basic.Implement.DAL.Common.IDataAccess
    {
        private static string SelectSql = @"
SELECT [OrganizationID]
      ,[OrganizationName]
      ,[ParentID]
      ,[Path]
      ,[DisplayPath]
      ,[OrganizationCode]
      ,[State]
      ,[Description]
      ,[EstablishTime]
      ,[Address]
      ,[PostCode]
      ,[Email]
      ,[Telphone]
      ,[Fax]
      ,[Manager]
      ,[MobilePhone]
      ,[Trainer]
      ,[TrainerTelphonePhone]
      ,[TrainerEmail]
      ,[Logo]
      ,[OrderNo]
      ,[Creator]
      ,[CreateTime]
      ,[Modifier]
      ,[ModifyTime]
      ,[StudentNum]
      ,[Domain]
      ,[Title]
      ,[MenuLimit]
      ,[FooterInfo]
  FROM [dbo].[Site_Organization] WHERE 1=1 ";

//      ,[DisplayPath]  as [ColumnNameValue]
//      ,OrganizationName AS [ColumnTipValue]
        private static string SelectDicSql = @"
SELECT [OrganizationID] as [ColumnCodeValue]
      ,[OrganizationName]  as [ColumnNameValue]
      ,[DisplayPath]  as [ColumnToolTipValue]
  FROM [dbo].[Site_Organization] WHERE 1=1 ";
        #region IDataAccess 成员

        public void Add(object obj)
        {
            Organization site_Organization = (Organization)obj;

            string commandName = "dbo.Pr_Site_Organization_Insert";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {	new SqlParameter("@OrganizationID", SqlDbType.Int, 4, ParameterDirection.Output, false, 0, 0, String.Empty, DataRowVersion.Default, null),
				
					new SqlParameter("@OrganizationName", SqlDbType.NVarChar, 100),
					new SqlParameter("@ParentID", SqlDbType.Int),
					new SqlParameter("@Path", SqlDbType.NVarChar, 50),
					new SqlParameter("@DisplayPath", SqlDbType.NVarChar, 200),
					new SqlParameter("@OrganizationCode", SqlDbType.NVarChar, 20),
					new SqlParameter("@State", SqlDbType.SmallInt),
					new SqlParameter("@Description", SqlDbType.NVarChar, 200),
					new SqlParameter("@EstablishTime", SqlDbType.DateTime),
					new SqlParameter("@Address", SqlDbType.NVarChar, 200),
					new SqlParameter("@PostCode", SqlDbType.NVarChar, 10),
					new SqlParameter("@Email", SqlDbType.NVarChar, 100),
					new SqlParameter("@Telphone", SqlDbType.NVarChar, 15),
					new SqlParameter("@Fax", SqlDbType.NVarChar, 15),
					new SqlParameter("@Manager", SqlDbType.NVarChar, 20),
					new SqlParameter("@MobilePhone", SqlDbType.NVarChar, 15),
					new SqlParameter("@Trainer", SqlDbType.NVarChar, 20),
					new SqlParameter("@TrainerTelphonePhone", SqlDbType.NVarChar, 15),
					new SqlParameter("@TrainerEmail", SqlDbType.NVarChar, 100),
					new SqlParameter("@OrderNo", SqlDbType.Int),
					new SqlParameter("@Creator", SqlDbType.NVarChar, 50),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@Modifier", SqlDbType.NVarChar, 50),
					new SqlParameter("@ModifyTime", SqlDbType.DateTime),
					new SqlParameter("@Logo", SqlDbType.NVarChar, 500),
                    new SqlParameter("@StudentNum",SqlDbType.Int),
                    new SqlParameter("@Domain", SqlDbType.NVarChar, 50)
					};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
            }

            if (site_Organization.OrganizationName != null) { parms[1].Value = site_Organization.OrganizationName; } else { parms[1].Value = DBNull.Value; }
            parms[2].Value = site_Organization.ParentNodeID;
            if (site_Organization.Path != null) { parms[3].Value = site_Organization.Path; } else { parms[3].Value = DBNull.Value; }
            if (site_Organization.DisplayPath != null) { parms[4].Value = site_Organization.DisplayPath; } else { parms[4].Value = DBNull.Value; }
            if (site_Organization.OrganizationCode != null) { parms[5].Value = site_Organization.OrganizationCode; } else { parms[5].Value = DBNull.Value; }
            parms[6].Value = site_Organization.State;
            if (site_Organization.Description != null) { parms[7].Value = site_Organization.Description; } else { parms[7].Value = DBNull.Value; }
            parms[8].Value = site_Organization.EstablishTime;
            if (site_Organization.Address != null) { parms[9].Value = site_Organization.Address; } else { parms[9].Value = DBNull.Value; }
            if (site_Organization.PostCode != null) { parms[10].Value = site_Organization.PostCode; } else { parms[10].Value = DBNull.Value; }
            if (site_Organization.Email != null) { parms[11].Value = site_Organization.Email; } else { parms[11].Value = DBNull.Value; }
            if (site_Organization.Telphone != null) { parms[12].Value = site_Organization.Telphone; } else { parms[12].Value = DBNull.Value; }
            if (site_Organization.Fax != null) { parms[13].Value = site_Organization.Fax; } else { parms[13].Value = DBNull.Value; }
            if (site_Organization.Manager != null) { parms[14].Value = site_Organization.Manager; } else { parms[14].Value = DBNull.Value; }
            if (site_Organization.MobilePhone != null) { parms[15].Value = site_Organization.MobilePhone; } else { parms[15].Value = DBNull.Value; }
            if (site_Organization.Trainer != null) { parms[16].Value = site_Organization.Trainer; } else { parms[16].Value = DBNull.Value; }
            if (site_Organization.TrainerTelphonePhone != null) { parms[17].Value = site_Organization.TrainerTelphonePhone; } else { parms[17].Value = DBNull.Value; }
            if (site_Organization.TrainerEmail != null) { parms[18].Value = site_Organization.TrainerEmail; } else { parms[18].Value = DBNull.Value; }
            parms[19].Value = site_Organization.OrderNo;
            if (site_Organization.Creator != null) { parms[20].Value = site_Organization.Creator; } else { parms[20].Value = DBNull.Value; }
            parms[21].Value = site_Organization.CreateTime;
            if (site_Organization.Modifier != null) { parms[22].Value = site_Organization.Modifier; } else { parms[22].Value = DBNull.Value; }
            parms[23].Value = site_Organization.ModifyTime;
            parms[24].Value = site_Organization.Logo;
            parms[25].Value = site_Organization.StudentNum;
            parms[26].Value = site_Organization.Domain;
            #endregion
            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);

            site_Organization.OrganizationID = (Int32)parms[0].Value;
        }

        public void Update(object obj)
        {
            Organization site_Organization = (Organization)obj;

            string commandName = "dbo.Pr_Site_Organization_Update";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@OrganizationID", SqlDbType.Int),
                    new SqlParameter("@OrganizationName", SqlDbType.NVarChar, 100),
                    new SqlParameter("@ParentID", SqlDbType.Int),
                    new SqlParameter("@Path", SqlDbType.NVarChar, 50),
                    new SqlParameter("@DisplayPath", SqlDbType.NVarChar, 200),
                    new SqlParameter("@OrganizationCode", SqlDbType.NVarChar, 20),
                    new SqlParameter("@State", SqlDbType.SmallInt),
                    new SqlParameter("@Description", SqlDbType.NVarChar, 200),
                    new SqlParameter("@EstablishTime", SqlDbType.DateTime),
                    new SqlParameter("@Address", SqlDbType.NVarChar, 200),
                    new SqlParameter("@PostCode", SqlDbType.NVarChar, 10),
                    new SqlParameter("@Email", SqlDbType.NVarChar, 100),
                    new SqlParameter("@Telphone", SqlDbType.NVarChar, 15),
                    new SqlParameter("@Fax", SqlDbType.NVarChar, 15),
                    new SqlParameter("@Manager", SqlDbType.NVarChar, 20),
                    new SqlParameter("@MobilePhone", SqlDbType.NVarChar, 15),
                    new SqlParameter("@Trainer", SqlDbType.NVarChar, 20),
                    new SqlParameter("@TrainerTelphonePhone", SqlDbType.NVarChar, 15),
                    new SqlParameter("@TrainerEmail", SqlDbType.NVarChar, 100),
                    new SqlParameter("@OrderNo", SqlDbType.Int),
                    new SqlParameter("@Creator", SqlDbType.NVarChar, 50),
                    new SqlParameter("@CreateTime", SqlDbType.DateTime),
                    new SqlParameter("@Modifier", SqlDbType.NVarChar, 50),
                    new SqlParameter("@ModifyTime", SqlDbType.DateTime),
                    new SqlParameter("@Logo", SqlDbType.NVarChar, 500),
                    new SqlParameter("@StudentNum",SqlDbType.Int),
                    new SqlParameter("@Domain", SqlDbType.NVarChar, 50)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
            }

            parms[0].Value = site_Organization.OrganizationID;
            if (site_Organization.OrganizationName != null) { parms[1].Value = site_Organization.OrganizationName; } else { parms[1].Value = DBNull.Value; }
            parms[2].Value = site_Organization.ParentNodeID;
            if (site_Organization.Path != null) { parms[3].Value = site_Organization.Path; } else { parms[3].Value = DBNull.Value; }
            if (site_Organization.DisplayPath != null) { parms[4].Value = site_Organization.DisplayPath; } else { parms[4].Value = DBNull.Value; }
            if (site_Organization.OrganizationCode != null) { parms[5].Value = site_Organization.OrganizationCode; } else { parms[5].Value = DBNull.Value; }
            parms[6].Value = site_Organization.State;
            if (site_Organization.Description != null) { parms[7].Value = site_Organization.Description; } else { parms[7].Value = DBNull.Value; }
            parms[8].Value = site_Organization.EstablishTime;
            if (site_Organization.Address != null) { parms[9].Value = site_Organization.Address; } else { parms[9].Value = DBNull.Value; }
            if (site_Organization.PostCode != null) { parms[10].Value = site_Organization.PostCode; } else { parms[10].Value = DBNull.Value; }
            if (site_Organization.Email != null) { parms[11].Value = site_Organization.Email; } else { parms[11].Value = DBNull.Value; }
            if (site_Organization.Telphone != null) { parms[12].Value = site_Organization.Telphone; } else { parms[12].Value = DBNull.Value; }
            if (site_Organization.Fax != null) { parms[13].Value = site_Organization.Fax; } else { parms[13].Value = DBNull.Value; }
            if (site_Organization.Manager != null) { parms[14].Value = site_Organization.Manager; } else { parms[14].Value = DBNull.Value; }
            if (site_Organization.MobilePhone != null) { parms[15].Value = site_Organization.MobilePhone; } else { parms[15].Value = DBNull.Value; }
            if (site_Organization.Trainer != null) { parms[16].Value = site_Organization.Trainer; } else { parms[16].Value = DBNull.Value; }
            if (site_Organization.TrainerTelphonePhone != null) { parms[17].Value = site_Organization.TrainerTelphonePhone; } else { parms[17].Value = DBNull.Value; }
            if (site_Organization.TrainerEmail != null) { parms[18].Value = site_Organization.TrainerEmail; } else { parms[18].Value = DBNull.Value; }
            parms[19].Value = site_Organization.OrderNo;
            if (site_Organization.Creator != null) { parms[20].Value = site_Organization.Creator; } else { parms[20].Value = DBNull.Value; }
            parms[21].Value = site_Organization.CreateTime;
            if (site_Organization.Modifier != null) { parms[22].Value = site_Organization.Modifier; } else { parms[22].Value = DBNull.Value; }
            parms[23].Value = site_Organization.ModifyTime;
            parms[24].Value = site_Organization.Logo;
            parms[25].Value = site_Organization.StudentNum;
            parms[26].Value = site_Organization.Domain;
            #endregion
            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
        }

        public void Delete(object obj)
        {
            Organization site_Organization = (Organization)obj;
            string commandName = "dbo.Pr_Site_Organization_Delete";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@OrganizationID", SqlDbType.Int)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
            }

            parms[0].Value = site_Organization.OrganizationID;

            #endregion
            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
        }

        public object Query(object id)
        {
            int groupID = (Int32)id;
            string sqlScriptFormat = SelectSql + " AND OrganizationID={0}";

            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.Text, string.Format(sqlScriptFormat, groupID)).Tables[0];
            if (dt.Rows.Count == 0)
                return null;
            return Organization.ConvertDataRowToRole(dt.Rows[0]);
        }

        public DataTable QueryByID(int orgID)
        {
            string sqlScriptFormat = SelectSql + " AND OrganizationID={0}";

            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.Text, string.Format(sqlScriptFormat, orgID)).Tables[0];
            if (dt.Rows.Count == 0)
                return null;
            return dt;
        }

        public Organization QueryByDomain(string domain)
        {
            string sqlScriptFormat = SelectSql + " AND Domain='{0}'";

            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.Text, string.Format(sqlScriptFormat, domain)).Tables[0];
            if (dt.Rows.Count == 0)
                return null;
            return Organization.ConvertDataRowToRole(dt.Rows[0]);
        }

        public Object[] Query(string filter)
        {
            string sqlScriptFormat = SelectSql + " {0} order by OrderNO asc, Path ASC";

            List<Organization> list = new List<Organization>();
            foreach (DataRow row in SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.Text, string.Format(sqlScriptFormat, filter)).Tables[0].Rows)
            {
                list.Add(Organization.ConvertDataRowToRole(row));
            }
            return (Object[])list.ToArray();
        }

        public Object[] Query(int pageIndex, int pageSize, string filter, string orderBy, out int recordCount)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public DataTable QueryDataList(string filter)
        {
            string sqlScriptFormat = SelectDicSql + " {0} order by dbo.fn_GetOrganizationOrderPath([DisplayPath]) asc";

            return SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.Text, string.Format(sqlScriptFormat, filter)).Tables[0];
        }

        //根据父id查询组织机构
        public DataTable GetPageListByParentID(int ParentID)
        {
            string commandName = "[dbo].[Pr_Order_GetAllOrganization]";
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@ParentID", SqlDbType.Int)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = ParentID;
           
            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];
            return dt;

        }

        /// <summary>
        /// 验证机构下学员是不是在机构限制的范围内
        /// </summary>
        /// <param name="organizationID"></param>
        public void OrganizationCheckStudentNum(int organizationID)
        {
            string commandName = "[dbo].[Pr_Site_Organization_CheckStudentNum]";
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@OrganizationID", SqlDbType.Int)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = organizationID;

            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms);
        }

        public void Setting(int orgID, string title, string limit, string footer)
        {
 
            string commandName = "UPDATE Site_Organization SET Title = @Title, MenuLimit = @MenuLimit, FooterInfo = @FooterInfo WHERE OrganizationID = @OrganizationID";
            #region Parameters
            SqlParameter[] parms = new SqlParameter[] {
                    new SqlParameter("@OrganizationID", SqlDbType.Int),
                    new SqlParameter("@MenuLimit", SqlDbType.NVarChar, 1000),
                    new SqlParameter("@FooterInfo", SqlDbType.NVarChar, 1000),
                    new SqlParameter("@Title", SqlDbType.NVarChar, 100)
                };

            parms[0].Value = orgID;
            parms[1].Value = limit;
            parms[2].Value = footer;
            parms[3].Value = title;
            #endregion
            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.Text, commandName, parms);
        }

        #endregion
    }
}
