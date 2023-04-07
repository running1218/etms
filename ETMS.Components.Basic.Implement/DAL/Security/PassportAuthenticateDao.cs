using System;
using System.Data;
using ETMS.Utility.Data;
using System.Data.SqlClient;
using ETMS.Components.Basic.API.Entity.Security;
namespace ETMS.Components.Basic.Implement.DAL.Security
{
    /// <summary>
    /// 统一认证--用户认证数据访问层（IBatis实现）
    /// </summary>
    public class PassportAuthenticateDao
    {

        public void SaveSignInInfo(PassportSignInInfo passportSignInInfo)
        {
            string sqlScript = @"
   UPDATE [Passport_Signin_Info] SET [SIGNIN_TIME] = @SIGNIN_TIME,[SIGNIN_TIMEOUT] = @SIGNIN_TIMEOUT WHERE [SIGNIN_ID] = @SIGNIN_ID
     IF(@@ROWCOUNT=0)
        BEGIN
            INSERT INTO [Passport_Signin_Info]([SIGNIN_ID],[SIGNIN_TIME],[SIGNIN_TIMEOUT],[USER_ID],[USER_NAME],[DOMAIN],[AUTHENTICATE_SERVER])
            VALUES(@SIGNIN_ID,@SIGNIN_TIME,@SIGNIN_TIMEOUT,@USER_ID,@USER_NAME,@DOMAIN,@AUTHENTICATE_SERVER)
        END";
            SqlParameter[] parameters = { 
                                            new SqlParameter("@SIGNIN_ID", SqlDbType.NVarChar),
                                            new SqlParameter("@SIGNIN_TIME", SqlDbType.DateTime),
                                            new SqlParameter("@SIGNIN_TIMEOUT", SqlDbType.DateTime),
                                            new SqlParameter("@USER_ID", SqlDbType.NVarChar),
                                            new SqlParameter("@USER_NAME", SqlDbType.NVarChar),
                                            new SqlParameter("@DOMAIN", SqlDbType.NVarChar),
                                            new SqlParameter("@AUTHENTICATE_SERVER", SqlDbType.NVarChar),
                                        };
            parameters[0].Value = passportSignInInfo.SIGNIN_ID;
            parameters[1].Value = passportSignInInfo.SIGNIN_TIME;
            parameters[2].Value = passportSignInInfo.SIGNIN_TIMEOUT;
            parameters[3].Value = passportSignInInfo.USER_ID;
            parameters[4].Value = passportSignInInfo.USER_NAME;
            parameters[5].Value = passportSignInInfo.DOMAIN;
            parameters[6].Value = passportSignInInfo.AUTHENTICATE_SERVER;
            SqlHelper.ExecuteNonQuery(ETMS.Components.Basic.Implement.DAL.ConnectionString.ETMSWrite, CommandType.Text, sqlScript, parameters);
        }

        public void SaveTicket(PassportTicket passportTicket)
        {
            string sqlScript = @"
UPDATE [Passport_Ticket] SET [APP_SIGNIN_TIME] = @APP_SIGNIN_TIME,[APP_SIGNIN_TIMEOUT] = @APP_SIGNIN_TIMEOUT WHERE [APP_SIGNIN_ID] = @APP_SIGNIN_ID
     IF(@@ROWCOUNT=0)
        BEGIN
           INSERT INTO [Passport_Ticket]
           ([APP_SIGNIN_ID]
           ,[SIGNIN_ID]
           ,[APP_ID]
           ,[APP_SIGNIN_TIME]
           ,[APP_SIGNIN_TIMEOUT]
           ,[APP_SIGNIN_URL]
           ,[APP_SIGNIN_IP]
           ,[APP_LOGOFF_URL]
           ,[DEL_FLAG])
     VALUES
           (@APP_SIGNIN_ID
           ,@SIGNIN_ID
           ,@APP_ID
           ,@APP_SIGNIN_TIME
           ,@APP_SIGNIN_TIMEOUT
           ,@APP_SIGNIN_URL
           ,@APP_SIGNIN_IP
           ,@APP_LOGOFF_URL
           ,'n')
        END";
            SqlParameter[] parameters = { 
                                            new SqlParameter("@APP_SIGNIN_ID", SqlDbType.NVarChar),
                                            new SqlParameter("@SIGNIN_ID", SqlDbType.NVarChar),
                                            new SqlParameter("@APP_ID", SqlDbType.NVarChar),
                                            new SqlParameter("@APP_SIGNIN_TIME", SqlDbType.DateTime),
                                            new SqlParameter("@APP_SIGNIN_TIMEOUT", SqlDbType.DateTime),
                                            new SqlParameter("@APP_SIGNIN_IP", SqlDbType.NVarChar),
                                            new SqlParameter("@APP_SIGNIN_URL", SqlDbType.NVarChar),
                                            new SqlParameter("@APP_LOGOFF_URL", SqlDbType.NVarChar),
                                        };
            parameters[0].Value = passportTicket.APP_SIGNIN_ID;
            parameters[1].Value = passportTicket.SIGNIN_ID;
            parameters[2].Value = passportTicket.APP_ID;
            parameters[3].Value = passportTicket.APP_SIGNIN_TIME;
            parameters[4].Value = passportTicket.APP_SIGNIN_TIMEOUT;
            parameters[5].Value = passportTicket.APP_SIGNIN_IP;
            parameters[6].Value = passportTicket.APP_SIGNIN_URL;
            parameters[7].Value = passportTicket.APP_LOGOFF_URL;
            SqlHelper.ExecuteNonQuery(ETMS.Components.Basic.Implement.DAL.ConnectionString.ETMSWrite, CommandType.Text, sqlScript, parameters);
        }

        public void DeleteRelativeSignInInfo(string signInID)
        {
            string sqlScript = @"
       DELETE FROM Passport_Signin_Info WHERE [SIGNIN_ID]=@SIGNIN_ID;
      DELETE FROM passport_ticket WHERE [SIGNIN_ID]=@SIGNIN_ID;";
            SqlParameter[] parameters = { new SqlParameter("@SIGNIN_ID", SqlDbType.NVarChar) };
            parameters[0].Value = signInID;
            SqlHelper.ExecuteNonQuery(ETMS.Components.Basic.Implement.DAL.ConnectionString.ETMSWrite, CommandType.Text, sqlScript, parameters);
        }

        public DataTable GetAllRelativeAppsLogOffCallBackUrl(string sessionID)
        {
            string sqlScript = @"  
    SELECT DISTINCT [APP_LOGOFF_URL] AS [Name] FROM PASSPORT_TICKET WHERE [SIGNIN_ID]=@SIGNIN_ID";
            SqlParameter[] parameters = { new SqlParameter("@SIGNIN_ID", SqlDbType.NVarChar) };
            parameters[0].Value = sessionID;
            return SqlHelper.ExecuteDataset(ETMS.Components.Basic.Implement.DAL.ConnectionString.ETMSRead, CommandType.Text, sqlScript, parameters).Tables[0];
        }

        public DataTable GetUserAccessLogByUserID(int userID)
        {
            string sqlScript = @"  
    SELECT [AccessLogID]
      ,[UserID]
      ,[LastLoginDate]
      ,[FailedPasswordAttemptCount]
      ,[FailedPasswordAttemptStart]
      ,[ModifyTime]
      FROM [Passport_UserAccessLog]
      WHERE [UserID]=@UserID";
            SqlParameter[] parameters = { new SqlParameter("@UserID", SqlDbType.Int) };
            parameters[0].Value = userID;
            DataTable dt = SqlHelper.ExecuteDataset(ETMS.Components.Basic.Implement.DAL.ConnectionString.ETMSRead, CommandType.Text, sqlScript, parameters).Tables[0];
            return dt;
        }

        public void SaveUserAccessFailedLog(int userID)
        {
            string sqlScript = @"  
      update Passport_UserAccessLog set [FailedPasswordAttemptStart]=getdate(),[FailedPasswordAttemptCount]=1,[ModifyTime]=getdate()
      where [UserID]=@UserID and [FailedPasswordAttemptStart] is null;
      if(@@ROWCOUNT=0)
          begin
              update Passport_UserAccessLog set [FailedPasswordAttemptCount]=[FailedPasswordAttemptCount]+1,[ModifyTime]=getdate()
              where [UserID]=@UserID;
              if(@@ROWCOUNT=0)        
              begin
                insert into Passport_UserAccessLog([AccessLogID],[UserID],[FailedPasswordAttemptCount],[FailedPasswordAttemptStart],[ModifyTime])
                values(newid(),@UserID,1,getdate(),getdate())
              end
          end";
            SqlParameter[] parameters = { new SqlParameter("@UserID", SqlDbType.Int) };
            parameters[0].Value = userID;
            SqlHelper.ExecuteNonQuery(ETMS.Components.Basic.Implement.DAL.ConnectionString.ETMSWrite, CommandType.Text, sqlScript, parameters);
        }

        public void SaveUserAccessSuccessLog(int userID, string ssid)
        {
            string sqlScript = @"
      update Passport_UserAccessLog set [FailedPasswordAttemptStart]=null,[FailedPasswordAttemptCount]=0,[LastLoginDate]=getdate(),[ModifyTime]=getdate()
            , LastLoginIP = @LastLoginIP, LoginStatus = 1
      where [UserID]=@UserID;
      if(@@ROWCOUNT=0)
          begin            
              insert into Passport_UserAccessLog([AccessLogID],[UserID],[FailedPasswordAttemptCount],[LastLoginDate],[ModifyTime],LastLoginIP, LoginStatus)
              values(newid(),@UserID,0,getdate(),getdate(), @LastLoginIP, 1)             
          end";
            SqlParameter[] parameters = { new SqlParameter("@UserID", SqlDbType.Int), new SqlParameter("@LastLoginIP", SqlDbType.NVarChar) };
            parameters[0].Value = userID;
            parameters[1].Value = ssid;
            SqlHelper.ExecuteNonQuery(ETMS.Components.Basic.Implement.DAL.ConnectionString.ETMSWrite, CommandType.Text, sqlScript, parameters);
        }

        public void LogoffUserAccessSuccessLog(int userID)
        {
            string sqlScript = @"
      update Passport_UserAccessLog set LoginStatus = 0
      where [UserID]=@UserID ";
            SqlParameter[] parameters = { new SqlParameter("@UserID", SqlDbType.Int) };
            parameters[0].Value = userID;
            SqlHelper.ExecuteNonQuery(ETMS.Components.Basic.Implement.DAL.ConnectionString.ETMSWrite, CommandType.Text, sqlScript, parameters);
        }

        public DataTable GetPassportAccessLog(int userID)
        { 
            string sqlScript = @"
                Select [AccessLogID],[UserID],[FailedPasswordAttemptCount],[LastLoginDate],[ModifyTime],LastLoginIP, LoginStatus
                From Passport_UserAccessLog 
                Where UserID = @UserID";
            SqlParameter[] parameters = { new SqlParameter("@UserID", SqlDbType.Int)};
            parameters[0].Value = userID;
            return SqlHelper.ExecuteDataset(ETMS.Components.Basic.Implement.DAL.ConnectionString.ETMSWrite, CommandType.Text, sqlScript, parameters).Tables[0];
        }

        public PassportSignInInfo GetSignInInfo(string SIGNIN_ID)
        {
            string sqlScript = @"  
 SELECT [SIGNIN_ID]
      ,[SORT_ID]
      ,[SIGNIN_TIME]
      ,[SIGNIN_TIMEOUT]
      ,[USER_ID]
      ,[USER_NAME]
      ,[DOMAIN]
      ,[AUTHENTICATE_SERVER]
      FROM [Passport_Signin_Info] WHERE [SIGNIN_ID]=@SIGNIN_ID";
            SqlParameter[] parameters = { new SqlParameter("@SIGNIN_ID", SqlDbType.NVarChar) };
            parameters[0].Value = SIGNIN_ID;
            DataTable dt = SqlHelper.ExecuteDataset(ETMS.Components.Basic.Implement.DAL.ConnectionString.ETMSRead, CommandType.Text, sqlScript, parameters).Tables[0];
            if (dt.Rows.Count == 0)
                return null;
            else
            {
                return new PassportSignInInfo()
                {
                    SIGNIN_ID = dt.Rows[0]["SIGNIN_ID"].ToString(),
                    SIGNIN_TIME = (DateTime)dt.Rows[0]["SIGNIN_TIME"],
                    SIGNIN_TIMEOUT = (DateTime)dt.Rows[0]["SIGNIN_TIMEOUT"],
                    USER_ID = (int)dt.Rows[0]["USER_ID"],
                    USER_NAME = dt.Rows[0]["USER_NAME"].ToString(),
                    AUTHENTICATE_SERVER = dt.Rows[0]["AUTHENTICATE_SERVER"].ToString(),
                    DOMAIN = dt.Rows[0]["Domain"].ToString(),
                };
            }
        }

        public PassportTicket GetTicket(string ticketID)
        {
            string sqlScript = @"  
SELECT [APP_SIGNIN_ID]
      ,[SIGNIN_ID]
      ,[APP_ID]
      ,[APP_SIGNIN_TIME]
      ,[APP_SIGNIN_TIMEOUT]
      ,[APP_SIGNIN_URL]
      ,[APP_SIGNIN_IP]
      ,[APP_LOGOFF_URL]
      ,[DEL_FLAG]
      FROM [SSO].[dbo].[Passport_Ticket] WHERE [APP_SIGNIN_ID]=@APP_SIGNIN_ID";
            SqlParameter[] parameters = { new SqlParameter("@APP_SIGNIN_ID", SqlDbType.NVarChar) };
            parameters[0].Value = ticketID;
            DataTable dt = SqlHelper.ExecuteDataset(ETMS.Components.Basic.Implement.DAL.ConnectionString.ETMSRead, CommandType.Text, sqlScript, parameters).Tables[0];
            if (dt.Rows.Count == 0)
                return null;
            else
            {
                return new PassportTicket()
                {
                    APP_SIGNIN_ID = dt.Rows[0]["APP_SIGNIN_ID"].ToString(),
                    APP_ID = (string)dt.Rows[0]["APP_ID"],
                    APP_SIGNIN_TIME = (DateTime)dt.Rows[0]["APP_SIGNIN_TIME"],
                    APP_SIGNIN_TIMEOUT = (DateTime)dt.Rows[0]["APP_SIGNIN_TIMEOUT"],
                    APP_SIGNIN_URL = (string)dt.Rows[0]["APP_SIGNIN_URL"],
                    APP_SIGNIN_IP = dt.Rows[0]["APP_SIGNIN_IP"].ToString(),
                    APP_LOGOFF_URL = dt.Rows[0]["APP_LOGOFF_URL"].ToString(),
                };
            }
        }
    }
}
