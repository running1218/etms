using System;
using System.Collections.Generic;
using ETMS.Components.Basic.API.Entity.Security;
using ETMS.Utility.Cryptography;
using ETMS.Components.Basic.Implement.DAL.Security;
using System.Data;
namespace ETMS.Components.Basic.Implement.BLL.Security
{
    /// <summary>
    /// 
    /// </summary>
    public class PassportAuthenticateLogic
    {
        #region 依赖注入属性
        /// <summary>
        /// DAO实例
        /// </summary>
        public PassportAuthenticateDao DAL = new PassportAuthenticateDao();

        private int m_FailedPasswordLockPeriod = 30;//默认用户锁周期：30分钟;
        private int m_FailedPasswordMaxTimes = 10;//默认用户尝试登录次数10次
        /// <summary>
        /// 密码错误尝试锁定周期(单位分钟）
        /// 默认用户锁周期：30分钟;
        /// </summary>
        public int FailedPasswordLockPeriod
        {
            get
            {
                return m_FailedPasswordLockPeriod;
            }
            set
            {
                m_FailedPasswordLockPeriod = value;
            }
        }

        /// <summary>
        /// 密码尝试最大次数
        /// 默认用户尝试登录次数10次
        /// </summary>
        public int FailedPasswordMaxTimes
        {
            get
            {
                return m_FailedPasswordMaxTimes;
            }
            set
            {
                m_FailedPasswordMaxTimes = value;
            }
        }

        #endregion

        public IUser GetUserInfoByID(int userID)
        {
            UserLogic userLogic = new UserLogic();
            return userLogic.GetUserByID(userID);

        }
        public IUser Authenticate(string loginName, string password, string ssid,bool isEncrypt=false)
        {
            UserLogic userLogic = new UserLogic();
            OrganizationLogic orgLogic = new OrganizationLogic();
            //0、根据用户名获取用户信息
            User userInfo = userLogic.GetUserByLoginName(loginName);
            //1、如果用户存在,（排除逻辑删除用户）
            if (userInfo != null && userInfo.Status != -1)
            {
                //1.1 获取用户登录日志
                System.Data.DataTable accessLogInfo = DAL.GetUserAccessLogByUserID(userInfo.UserID);

                bool isFaieldLock = false;
                bool isPassworkError = false;
                //存在登录日志，则需要判定当前用户是否不允许尝试登录
                if (accessLogInfo.Rows.Count > 0 && (Int16)accessLogInfo.Rows[0]["FailedPasswordAttemptCount"] > 0)
                {
                    //1.2.1 登录尝试次数判断
                    if ((Int16)accessLogInfo.Rows[0]["FailedPasswordAttemptCount"] > FailedPasswordMaxTimes //超过最大尝试次数
                        && DateTime.Now.Subtract((DateTime)accessLogInfo.Rows[0]["FailedPasswordAttemptStart"]) <= new TimeSpan(0, FailedPasswordLockPeriod, 0))//且在锁定的时间内
                    {
                        isFaieldLock = true;//规定的时间内尝试过多次数                       
                    }
                }

                //1.2 验证密码（16位MD5）
                string md5Password = password;
                if (!isEncrypt)
                {
                    md5Password = MD5Utility.MD516(password);
                }
                isPassworkError = !userInfo.PassWord.Equals(md5Password, StringComparison.InvariantCultureIgnoreCase);
                if (isPassworkError)
                {
                    if (!isFaieldLock)//未超过：规定的时间内尝试过多次数，记录登录失败日志，并返回用户名密码错误信息！        
                    {
                        //记录用户登录失败日志（失败次数累计，失败开始时间）
                        DAL.SaveUserAccessFailedLog(userInfo.UserID);
                        //用户名或密码错误
                        throw new ETMS.AppContext.BusinessException("Authenticate.UserNameOrPasswordError");
                    }
                    else//超过：规定的时间内尝试过多次数  ，返回账户由于多次失败登录，导致账户一定时间内被锁定！
                    {
                        throw new ETMS.AppContext.BusinessException("Authenticate.FailedInLocking");
                    }
                }
                //1.3 判断用户的状态、审核状态是否可用
                if (userInfo.Status == 0)
                {
                    //抛出用户状态异常
                    throw new ETMS.AppContext.BusinessException("Authenticate.UserStatus.Disable");
                }
                //1.4 判断用户所在机构是否停用
                if (orgLogic.GetNodeByID(userInfo.OrganizationID).State == 0)
                {
                    //抛出用户状态异常
                    throw new ETMS.AppContext.BusinessException("Authenticate.OrgnizationStatus.Disable");
                }

                //记录用户登录成功日志
                DAL.SaveUserAccessSuccessLog(userInfo.UserID, ssid);

                return userInfo;

            }
            else
            {
                //用户名或密码错误（未找到用户）
                throw new ETMS.AppContext.BusinessException("Authenticate.UserNameOrPasswordError");
            }
        }

        public void SaveUserAccessSuccessLog(int userID, string sessionID)
        {
            DAL.SaveUserAccessSuccessLog(userID, sessionID);
        }
        public void SaveSignInInfo(PassportSignInInfo passportSignInInfo)
        {
            //如果是进程内过期时间策略，则由于DateTime.MinVaule过期时间无法存入数据库，因此，转化为'1900-1-1'来表示！
            if (passportSignInInfo.SIGNIN_TIMEOUT.Equals(DateTime.MinValue))
            {
                passportSignInInfo.SIGNIN_TIMEOUT = DateTime.Parse("1900-1-1");
            }
            DAL.SaveSignInInfo(passportSignInInfo);
        }

        public void SaveTicket(PassportTicket passportTicket)
        {
            //如果是进程内过期时间策略，则由于DateTime.MinVaule过期时间无法存入数据库，因此，转化为'1900-1-1'来表示！
            if (passportTicket.APP_SIGNIN_TIMEOUT.Equals(DateTime.MinValue))
            {
                passportTicket.APP_SIGNIN_TIMEOUT = DateTime.Parse("1900-1-1");
            }
            DAL.SaveTicket(passportTicket);
        }

        public void DeleteRelativeSignInInfo(string signInID)
        {
            DAL.DeleteRelativeSignInInfo(signInID);
        }

        public IList<string> GetAllRelativeAppsLogOffCallBackUrl(string sessionID)
        {
            System.Data.DataTable dt = DAL.GetAllRelativeAppsLogOffCallBackUrl(sessionID);
            IList<string> result = new List<string>();
            foreach (System.Data.DataRow row in dt.Rows)
            {
                result.Add(row[0].ToString());
            }
            return result;
        }

        public PassportSignInInfo GetSignInInfo(string SIGNIN_ID)
        {
            return DAL.GetSignInInfo(SIGNIN_ID);
        }

        public PassportTicket GetTicket(string ticketID)
        {
            return DAL.GetTicket(ticketID);
        }

        public void LogoffUserAccessSuccessLog(int userID)
        {
            DAL.LogoffUserAccessSuccessLog(userID);
        }

        public DataTable GetPassportAccessLog(int userID)
        {
            return DAL.GetPassportAccessLog(userID);
        }
    }
}
