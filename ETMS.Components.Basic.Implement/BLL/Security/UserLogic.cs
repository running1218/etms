using System;

using ETMS.AppContext;
using ETMS.Components.Basic.Implement.DAL.Common;
using ETMS.Components.Basic.API.Entity.Security;
using ETMS.Components.Basic.Implement.DAL.Security;
using System.Data;
using ETMS.Utility;

namespace ETMS.Components.Basic.Implement.BLL.Security
{
    /// <summary>
    /// 用户管理
    /// </summary>
    public class UserLogic
    {
        private IDataAccess DAL = new UserDataAccess();

        /// <summary>
        /// 切换用户状态
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <param name="modifier">修改人</param>
        public void SwitchUserStatus(int userID, string modifier)
        {
            User entity = GetUserByID(userID);
            int oldStatus = entity.Status;
            entity.Status = 1 - entity.Status;
            entity.Modifier = modifier;
            entity.ModifyTime = DateTime.Now;
            DAL.Update(entity);
            //记录日志
            ETMS.Utility.Logging.BizLogHelper.Operate(userID, string.Format("切换用户状态：原始状态={0},新状态={1}", oldStatus, entity.Status));
        }

        /// <summary>
        /// 重置用户密码
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <param name="modifier">修改人</param>
        public void ResetPassword(int userID, string modifier, string defaultPassword)
        {
            User entity = GetUserByID(userID);
            entity.PassWord = ETMS.Utility.Cryptography.MD5Utility.MD516(defaultPassword);
            entity.Modifier = modifier;
            entity.ModifyTime = DateTime.Now;
            DAL.Update(entity);
            //记录日志
            ETMS.Utility.Logging.BizLogHelper.Operate(userID, string.Format("重置用户密码：新密码={0}", defaultPassword));
        }

        /// <summary>
        /// 修改用户密码
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <param name="oldPassword">原始密码</param>
        /// <param name="newPassword">新密码</param>
        public void ChangePassword(int userID, string oldPassword, string newPassword)
        {
            User entity = GetUserByID(userID);
            if (!entity.PassWord.Equals(ETMS.Utility.Cryptography.MD5Utility.MD516(oldPassword)))
            {
                //提示信息
                throw new ETMS.AppContext.BusinessException("Security.UserLogin.OldPasswordError");
            }
            //新密码
            entity.PassWord = ETMS.Utility.Cryptography.MD5Utility.MD516(newPassword);
            entity.Modifier = entity.LoginName;
            entity.ModifyTime = DateTime.Now;
            DAL.Update(entity);
            //记录日志
            ETMS.Utility.Logging.BizLogHelper.Operate(userID, string.Format("修改用户密码：原始密码={0},新密码={1}", oldPassword, newPassword));
        }

        /// <summary>
        /// 重置用户密码
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <param name="newPassword">新密码</param>
        public void ResetPassword(int userID, string newPassword)
        {
            User entity = GetUserByID(userID);
            //新密码
            entity.PassWord = ETMS.Utility.Cryptography.MD5Utility.MD516(newPassword);
            entity.Modifier = entity.LoginName;
            entity.ModifyTime = DateTime.Now;
            DAL.Update(entity);
            //记录日志
            ETMS.Utility.Logging.BizLogHelper.Operate(userID, string.Format("重置用户密码：新密码={0}", newPassword));
        }

        /// <summary>
        /// 保存用户信息
        /// </summary>
        /// <param name="user"></param>
        public void Save(User user)
        {
            try
            {
                if (user.UserID == 0)
                {
                    user.CreateTime = DateTime.Now;
                    user.Creator = UserContext.Current.RealName;
                    user.Modifier = UserContext.Current.RealName;
                    user.ModifyTime = DateTime.Now;
                    if (user.OrganizationID == 0)//默认设置当前用户所处机构
                    {
                        user.OrganizationID = UserContext.Current.OrganizationID;
                    }
                    user.PassWord = ETMS.Utility.Cryptography.MD5Utility.MD516(user.PassWord);
                    DAL.Add(user);
                    //记录日志
                    ETMS.Utility.Logging.BizLogHelper.AddOperate(user);
                }
                else
                {
                    //屏蔽修改：账户、密码、所属机构 这些信息单独维护。
                    User oldInfo = GetUserByID(user.UserID);
                    user.LoginName = oldInfo.LoginName;
                    user.PassWord = oldInfo.PassWord;
                    user.OrganizationID = oldInfo.OrganizationID;

                    user.Modifier = ETMS.AppContext.UserContext.Current.RealName;
                    user.ModifyTime = DateTime.Now;
                    DAL.Update(user);
                    //记录日志
                    ETMS.Utility.Logging.BizLogHelper.UpdateOperate(oldInfo, user);
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                //枚举数据约束异常并转换为业务异常
                if (ex.Message.IndexOf("IX_U_Site_User_UserName", StringComparison.InvariantCultureIgnoreCase) != -1)
                {
                    throw new ETMS.AppContext.BusinessException("Security.User.AddFailed.UserIsExists", new object[] { user.LoginName });
                }
                //如果仍未处理，则抛出
                throw new ETMS.AppContext.BusinessException(ex.Message);
            }
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="user"></param>
        public void Remove(User user)
        {
            try
            {
                DAL.Delete(user);
                //记录日志
                ETMS.Utility.Logging.BizLogHelper.DeleteOperate(user);
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                //枚举数据约束异常并转换为业务异常
                if (ex.Message.IndexOf("FK_Site_Student_Site_User", StringComparison.InvariantCultureIgnoreCase) != -1
                    ||
                    ex.Message.IndexOf("FK_Sty_StudentSignup_Site_Student", StringComparison.InvariantCultureIgnoreCase) != -1
                    )
                {
                    throw new ETMS.AppContext.BusinessException("Security.User.DeleteFailed.UserIsUsing", new object[] { user.LoginName });
                }
                //枚举数据约束异常并转换为业务异常
                if (ex.Message.IndexOf("FK_Site_Teacher_Site_User", StringComparison.InvariantCultureIgnoreCase) != -1
                    ||
                    ex.Message.IndexOf("FK_RES_TEAC_REFER_SITE_TEA", StringComparison.InvariantCultureIgnoreCase) != -1
                    )
                {
                    throw new ETMS.AppContext.BusinessException("Security.User.DeleteFailed.UserIsUsing", new object[] { user.LoginName });
                }
                //如果仍未处理，则抛出
                throw ex;
            }
        }

        /// <summary>
        /// 根据用户ID获取用户信息
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <returns>用户信息，如果没找到此用户，则返回 NotFoundUserByIDException异常。</returns>
        public User GetUserByID(int userID)
        {
            User findUser = (User)DAL.Query((int)userID);
            if (findUser == null)
                throw new ETMS.AppContext.BusinessException("Security.UserLogic.NotFoundUser");
            return findUser;
        }

        /// <summary>
        /// 根据用户ID获取用户信息
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <returns>用户信息，如果没找到此用户，则返回 NotFoundUserByIDException异常。</returns>
        public User GetUserBaseData(int userID)
        {
           DataTable dt= ((UserDataAccess)DAL).GetUserBaseData(userID);
            return dt.Rows.Count > 0 ? dt.Rows[0].ToEntity<User>() : null;
        }

        /// <summary>
        /// 根据用户ID获取用户信息
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <returns>用户信息，如果没找到此用户，则返回 NotFoundUserByIDException异常。</returns>
        public User GetUserInfoByID(int userID)
        {
            User findUser = (User)DAL.Query((int)userID);            
            return findUser ?? new User();
        }

        /// <summary>
        /// 根据登陆名获取用户信息
        /// </summary>
        /// <param name="loginName">登陆名</param>
        /// <returns>用户信息，如果没找到此用户，则返回NotFoundUserByLoginNameException异常。</returns>
        public User GetUserByLoginName(string loginName)
        {
            User[] findUsers = (User[])DAL.Query(string.Format(" AND LoginName='{0}'", loginName.Replace("'", "''")));
            if (findUsers.Length == 0)
                return null;
            else
                return findUsers[0];
        }

        /// <summary>
        /// 查询用户列表
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="orderby"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalRecord"></param>
        /// <returns></returns>
        public User[] GetPagedList(int pageIndex, int pageSize, string filter, string orderby, out int totalRecord)
        {
            return (User[])DAL.Query(pageIndex, pageSize, filter, orderby, out totalRecord);
        }

        public User[] GetUsers(string filter)
        {
            return (User[])DAL.Query(filter);
        }
    }
}
