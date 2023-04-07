using ETMS.Activity.Entity;
using ETMS.Utility.Data;
using System;
using System.Data;
using System.Data.SqlClient;

namespace ETMS.Activity.Implement.DAL
{
    public partial class SiginupDataAccess
    {
        public int GetSiginupCount(Guid appraisalID)
        {
            string sql = @"select count(*) from Activity_Siginup where AppraisalID=@AppraisalID";
            #region Parameters
            SqlParameter[] parms = new SqlParameter[] {
                    new SqlParameter("@AppraisalID", SqlDbType.UniqueIdentifier),
                };
            parms[0].Value = appraisalID;
            #endregion
            return Convert.ToInt32(SqlHelper.ExecuteScalar(ConnectionString.ETMSRead, CommandType.Text, sql, parms));
        }

        public DataTable GetSiginup(int userid, Guid appraisalID) {
            string sql = "select * from Activity_Siginup where [UserID]=@UserID and AppraisalID=@AppraisalID";
            SqlParameter[] parms = new SqlParameter[] {
                new SqlParameter("@AppraisalID", SqlDbType.UniqueIdentifier),
                new SqlParameter("@UserID", SqlDbType.Int)
            };
            parms[0].Value = appraisalID;
            parms[1].Value = userid;
            return SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.Text, sql, parms).Tables[0];

        }

        private string GetNum(string num) {
            string str = num;
            for (int i = 0; i < 6-num.Length; i++)
            {
                str = "0" + str;
            }
            return str;
        }
        public string Insert(Siginup entity) {
            string sql1 = "select count(*) from Activity_Siginup where AppraisalID=@AppraisalID";
            SqlParameter[] parms1 = new SqlParameter[] {
                new SqlParameter("@AppraisalID", SqlDbType.UniqueIdentifier)
            };
            parms1[0].Value = entity.AppraisalID;
            int scount = Convert.ToInt32(SqlHelper.ExecuteScalar(ConnectionString.ETMSRead, CommandType.Text, sql1, parms1));
            
            entity.SiginupNo = "YLW"+DateTime.Now.Year+ GetNum(scount.ToString());

            string sql = @"INSERT INTO [dbo].[Activity_Siginup]
           ([SiginupID]
           ,[UserID]
           ,[AppraisalID]
           ,[SiginupNo]
           ,[SiginupStatus]
           ,[GroupID]
           ,[RegionID]
           ,[School]
           ,[SiginupTime]
           ,[Name]
           ,[Age]
           ,[Sex]
           ,[Phone])
     VALUES
           (@SiginupID
           ,@UserID
           ,@AppraisalID
           ,@SiginupNo
           ,@SiginupStatus
           ,@GroupID
           ,@RegionID
           ,@School
           ,@SiginupTime
           ,@Name
           ,@Age
           ,@Sex
           ,@Phone)";
            #region Parameters
            SqlParameter[] parms = new SqlParameter[] {
                    new SqlParameter("@SiginupID", SqlDbType.UniqueIdentifier),
                    new SqlParameter("@UserID", SqlDbType.Int),
                    new SqlParameter("@AppraisalID", SqlDbType.UniqueIdentifier),
                    new SqlParameter("@SiginupNo", SqlDbType.NVarChar,20),
                    new SqlParameter("@SiginupStatus", SqlDbType.Int),
                    new SqlParameter("@GroupID", SqlDbType.Int),
                    new SqlParameter("@RegionID", SqlDbType.Int),
                    new SqlParameter("@School", SqlDbType.NVarChar, 50),
                    new SqlParameter("@SiginupTime", SqlDbType.DateTime),
                    new SqlParameter("@Name", SqlDbType.NVarChar,50),
                    new SqlParameter("@Age", SqlDbType.Int),
                    new SqlParameter("@Sex", SqlDbType.Int),
                    new SqlParameter("@Phone", SqlDbType.NVarChar, 20)
                };
            parms[0].Value = entity.SiginupID;
            parms[1].Value = entity.UserID;
            parms[2].Value = entity.AppraisalID;
            parms[3].Value = entity.SiginupNo;
            parms[4].Value = entity.SiginupStatus;
            parms[5].Value = entity.GroupID;
            parms[6].Value = entity.RegionID;
            parms[7].Value = entity.School;
            parms[8].Value = entity.SiginupTime;
            parms[9].Value = entity.Name;
            parms[10].Value = entity.Age;
            parms[11].Value = entity.Sex;
            parms[12].Value = entity.Phone;

            #endregion
            int count = SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.Text, sql, parms);
            if (count == 1)
            {
                return entity.SiginupNo;
            }
            else {
                return "";
            }
        }

        public DataTable GetMyActivities(int userID)
        {
            #region sql
            string sql = @" SELECT A.SiginupID, A.SiginupNo, A.GroupID, A.RegionID, A.School, A.SiginupTime, A.UserID
	                            ,B.[AppraisalID],B.[AppraisalTitle],B.[TypeID],B.[ShapeID],[BeginTime],[EndTime],[ImageUrl]
	                            ,C.GroupName, D.RegionName
                            FROM Activity_Siginup A
                            INNER JOIN Activity_Appraisal B ON a.AppraisalID = B.AppraisalID
                            INNER JOIN Activity_Dic_Group C ON C.GroupID = A.GroupID
                            INNER JOIN Activity_Dic_Region D ON A.RegionID = D.RegionID
                            WHERE UserID = @UserID AND A.SiginupStatus = 1
                            ORDER BY A.SiginupTime DESC";
            #endregion

            #region Parameters
            SqlParameter[] parms = new SqlParameter[] {
                    new SqlParameter("@UserID", SqlDbType.Int),
                };

            parms[0].Value = userID;
            #endregion

            return SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.Text, sql, parms).Tables[0];
        }

        public DataTable GetMyActivity(Guid siginUpID)
        {
            #region sql
            string sql = @" SELECT A.SiginupID, A.SiginupNo, A.GroupID, A.RegionID, A.School, A.SiginupTime, A.UserID
	                            ,B.[AppraisalID],B.[AppraisalTitle],B.[TypeID],B.[ShapeID],[BeginTime],[EndTime],[ImageUrl]
	                            ,C.GroupName, D.RegionName
                            FROM Activity_Siginup A
                            INNER JOIN Activity_Appraisal B ON a.AppraisalID = B.AppraisalID
                            INNER JOIN Activity_Dic_Group C ON C.GroupID = A.GroupID
                            INNER JOIN Activity_Dic_Region D ON A.RegionID = D.RegionID
                            WHERE A.SiginupID = @SiginupID AND A.SiginupStatus = 1";
            #endregion

            #region Parameters
            SqlParameter[] parms = new SqlParameter[] {
                    new SqlParameter("@SiginupID", SqlDbType.UniqueIdentifier),
                };

            parms[0].Value = siginUpID;
            #endregion

            return SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.Text, sql, parms).Tables[0];
        }
    }
}
