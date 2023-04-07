using System;
using ETMS.Components.Basic.API.Entity.Security;
using System.Data;
using System.Data.SqlClient;
using ETMS.Utility.Data;

namespace ETMS.Components.Basic.Implement.DAL.Security
{
    public class Site_AgencyProductDataAccess
    {
        public DataTable GetAgencyCourses(int agencyID)
        {
            string command = @"SELECT A.[AgencyProductID]
	                            ,A.[AgencyID]
                                ,A.[CourseID]
                                ,A.[AgencyCode]
                                ,A.[DiscountType]
                                ,A.[DiscountPrice]
                                ,A.[CreateTime]
                                ,A.[CreateUserID]
                                ,A.[CreateUser]
                                ,A.[ModifyTime]
                                ,A.[ModifyUser]
	                            ,B.CourseName
                            FROM [Site_AgencyProduct] A
                            LEFT JOIN dbo.Res_Course B ON B.CourseID = A.CourseID
                            WHERE	AgencyID = @AgencyID
                            ORDER BY CreateTime DESC";
            SqlParameter[] parms = new SqlParameter[] { new SqlParameter("@AgencyID", SqlDbType.Int) };

            parms[0].Value = agencyID;
            return SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.Text, command, parms).Tables[0];
        }

        public DataTable GetAgencyCourseByID(Guid agencyProductID)
        {
            string command = @"SELECT [AgencyProductID]
	                          ,[AgencyID]
                              ,[CourseID]
                              ,[AgencyCode]
                              ,[DiscountType]
                              ,[DiscountPrice]
                              ,[CreateTime]
                              ,[CreateUserID]
                              ,[CreateUser]
                              ,[ModifyTime]
                              ,[ModifyUser]
                          FROM [Site_AgencyProduct]
                          WHERE	AgencyProductID = @AgencyProductID";
            SqlParameter[] parms = new SqlParameter[] { new SqlParameter("@AgencyProductID", SqlDbType.UniqueIdentifier) };

            parms[0].Value = agencyProductID;
            return SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.Text, command, parms).Tables[0];
        }

        public DataTable GetAgencyCourseByAgencyCode(Guid CourseID,string AgencyCode)
        {
            string command = @"SELECT [AgencyProductID]
	                          ,[AgencyID]
                              ,[CourseID]
                              ,[AgencyCode]
                              ,[DiscountType]
                              ,[DiscountPrice]
                              ,[CreateTime]
                              ,[CreateUserID]
                              ,[CreateUser]
                              ,[ModifyTime]
                              ,[ModifyUser]
                          FROM [Site_AgencyProduct]
                          WHERE	CourseID=@CourseID AND AgencyCode = @AgencyCode";
            SqlParameter[] parms = new SqlParameter[] {
                new SqlParameter("@CourseID", SqlDbType.UniqueIdentifier),
                new SqlParameter("@AgencyCode", SqlDbType.NVarChar)

            };
            parms[0].Value = CourseID;
            parms[1].Value = AgencyCode;
            return SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.Text, command, parms).Tables[0];
        }

        public int Insert(Site_AgencyProduct entity)
        {
            #region SQL
            string commandName = @"INSERT INTO dbo.Site_AgencyProduct
                                            (AgencyProductID,
                                              AgencyID,
                                              CourseID,
                                              AgencyCode,
                                              DiscountType,
                                              DiscountPrice,
                                              CreateTime,
                                              CreateUserID,
                                              CreateUser,
                                              ModifyTime,
                                              ModifyUser
                                            )
                                    VALUES(@AgencyProductID,
                                              @AgencyID,
                                              @CourseID,
                                              @AgencyCode,
                                              @DiscountType,
                                              @DiscountPrice,
                                              @CreateTime,
                                              @CreateUserID,
                                              @CreateUser,
                                              @ModifyTime,
                                              @ModifyUser
                                            )";
            #endregion
            #region Parameters
            SqlParameter[] parms = new SqlParameter[] {
                    new SqlParameter("@AgencyProductID", SqlDbType.UniqueIdentifier),
                    new SqlParameter("@AgencyID", SqlDbType.Int),
                    new SqlParameter("@CourseID", SqlDbType.UniqueIdentifier),
                    new SqlParameter("@AgencyCode", SqlDbType.NVarChar, 20),
                    new SqlParameter("@DiscountType", SqlDbType.Int),
                    new SqlParameter("@DiscountPrice", SqlDbType.Decimal),
                    new SqlParameter("@CreateTime", SqlDbType.DateTime),
                    new SqlParameter("@CreateUserID", SqlDbType.Int),
                    new SqlParameter("@CreateUser", SqlDbType.NVarChar, 50),
                    new SqlParameter("@ModifyTime", SqlDbType.DateTime),
                    new SqlParameter("@ModifyUser", SqlDbType.NVarChar, 50)
                    };
            parms[0].Value = entity.AgencyProductID;
            parms[1].Value = entity.AgencyID;
            parms[2].Value = entity.CourseID;
            parms[3].Value = entity.AgencyCode;
            parms[4].Value = entity.DiscountType;
            parms[5].Value = entity.DiscountPrice;
            parms[6].Value = entity.CreateTime;
            parms[7].Value = entity.CreateUserID;
            parms[8].Value = entity.CreateUser;
            parms[9].Value = entity.ModifyTime;
            parms[10].Value = entity.ModifyUser;

            #endregion
            return SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.Text, commandName, parms);
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

        public int Delete(Guid agencyProductID)
        {
            string command = "DELETE FROM Site_AgencyProduct WHERE AgencyProductID = @AgencyProductID";
            #region Parameters
            SqlParameter[] parms = new SqlParameter[] {
                    new SqlParameter("@AgencyProductID", SqlDbType.UniqueIdentifier)
                    };

            parms[0].Value = agencyProductID;

            #endregion
            return SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.Text, command, parms);
        }

        /// <summary>
        /// 代理商未代理的课程s
        /// </summary>
        /// <param name="agencyID"></param>
        /// <returns></returns>
        public DataTable GetUnAgencyCourses(int agencyID, int orgID)
        {
            string command = @"SELECT A.CourseID, A.CourseName
                                FROM dbo.Res_Course A
                                LEFT JOIN dbo.Site_AgencyProduct B ON A.CourseID = B.CourseID AND B.AgencyID = @AgencyID
                                WHERE A.OrgID = @OrgID  
	                                AND A.CourseModel = 2
                                    AND A.IsPay = 1
	                                AND B.AgencyProductID IS NULL
                                ORDER BY A.CreateTime DESC";
            SqlParameter[] parms = new SqlParameter[] {
                new SqlParameter("@AgencyID", SqlDbType.Int),
                new SqlParameter("@OrgID", SqlDbType.Int)
            };


            parms[0].Value = agencyID;
            parms[1].Value = orgID;
            return SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.Text, command, parms).Tables[0];
        }
    }
}
