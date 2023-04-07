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
    /// �û�����
    /// </summary>
    public class UserLogic
    {
        private IDataAccess DAL = new UserDataAccess();

        /// <summary>
        /// �л��û�״̬
        /// </summary>
        /// <param name="userID">�û�ID</param>
        /// <param name="modifier">�޸���</param>
        public void SwitchUserStatus(int userID, string modifier)
        {
            User entity = GetUserByID(userID);
            int oldStatus = entity.Status;
            entity.Status = 1 - entity.Status;
            entity.Modifier = modifier;
            entity.ModifyTime = DateTime.Now;
            DAL.Update(entity);
            //��¼��־
            ETMS.Utility.Logging.BizLogHelper.Operate(userID, string.Format("�л��û�״̬��ԭʼ״̬={0},��״̬={1}", oldStatus, entity.Status));
        }

        /// <summary>
        /// �����û�����
        /// </summary>
        /// <param name="userID">�û�ID</param>
        /// <param name="modifier">�޸���</param>
        public void ResetPassword(int userID, string modifier, string defaultPassword)
        {
            User entity = GetUserByID(userID);
            entity.PassWord = ETMS.Utility.Cryptography.MD5Utility.MD516(defaultPassword);
            entity.Modifier = modifier;
            entity.ModifyTime = DateTime.Now;
            DAL.Update(entity);
            //��¼��־
            ETMS.Utility.Logging.BizLogHelper.Operate(userID, string.Format("�����û����룺������={0}", defaultPassword));
        }

        /// <summary>
        /// �޸��û�����
        /// </summary>
        /// <param name="userID">�û�ID</param>
        /// <param name="oldPassword">ԭʼ����</param>
        /// <param name="newPassword">������</param>
        public void ChangePassword(int userID, string oldPassword, string newPassword)
        {
            User entity = GetUserByID(userID);
            if (!entity.PassWord.Equals(ETMS.Utility.Cryptography.MD5Utility.MD516(oldPassword)))
            {
                //��ʾ��Ϣ
                throw new ETMS.AppContext.BusinessException("Security.UserLogin.OldPasswordError");
            }
            //������
            entity.PassWord = ETMS.Utility.Cryptography.MD5Utility.MD516(newPassword);
            entity.Modifier = entity.LoginName;
            entity.ModifyTime = DateTime.Now;
            DAL.Update(entity);
            //��¼��־
            ETMS.Utility.Logging.BizLogHelper.Operate(userID, string.Format("�޸��û����룺ԭʼ����={0},������={1}", oldPassword, newPassword));
        }

        /// <summary>
        /// �����û�����
        /// </summary>
        /// <param name="userID">�û�ID</param>
        /// <param name="newPassword">������</param>
        public void ResetPassword(int userID, string newPassword)
        {
            User entity = GetUserByID(userID);
            //������
            entity.PassWord = ETMS.Utility.Cryptography.MD5Utility.MD516(newPassword);
            entity.Modifier = entity.LoginName;
            entity.ModifyTime = DateTime.Now;
            DAL.Update(entity);
            //��¼��־
            ETMS.Utility.Logging.BizLogHelper.Operate(userID, string.Format("�����û����룺������={0}", newPassword));
        }

        /// <summary>
        /// �����û���Ϣ
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
                    if (user.OrganizationID == 0)//Ĭ�����õ�ǰ�û���������
                    {
                        user.OrganizationID = UserContext.Current.OrganizationID;
                    }
                    user.PassWord = ETMS.Utility.Cryptography.MD5Utility.MD516(user.PassWord);
                    DAL.Add(user);
                    //��¼��־
                    ETMS.Utility.Logging.BizLogHelper.AddOperate(user);
                }
                else
                {
                    //�����޸ģ��˻������롢�������� ��Щ��Ϣ����ά����
                    User oldInfo = GetUserByID(user.UserID);
                    user.LoginName = oldInfo.LoginName;
                    user.PassWord = oldInfo.PassWord;
                    user.OrganizationID = oldInfo.OrganizationID;

                    user.Modifier = ETMS.AppContext.UserContext.Current.RealName;
                    user.ModifyTime = DateTime.Now;
                    DAL.Update(user);
                    //��¼��־
                    ETMS.Utility.Logging.BizLogHelper.UpdateOperate(oldInfo, user);
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                //ö������Լ���쳣��ת��Ϊҵ���쳣
                if (ex.Message.IndexOf("IX_U_Site_User_UserName", StringComparison.InvariantCultureIgnoreCase) != -1)
                {
                    throw new ETMS.AppContext.BusinessException("Security.User.AddFailed.UserIsExists", new object[] { user.LoginName });
                }
                //�����δ�������׳�
                throw new ETMS.AppContext.BusinessException(ex.Message);
            }
        }

        /// <summary>
        /// ɾ���û�
        /// </summary>
        /// <param name="user"></param>
        public void Remove(User user)
        {
            try
            {
                DAL.Delete(user);
                //��¼��־
                ETMS.Utility.Logging.BizLogHelper.DeleteOperate(user);
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                //ö������Լ���쳣��ת��Ϊҵ���쳣
                if (ex.Message.IndexOf("FK_Site_Student_Site_User", StringComparison.InvariantCultureIgnoreCase) != -1
                    ||
                    ex.Message.IndexOf("FK_Sty_StudentSignup_Site_Student", StringComparison.InvariantCultureIgnoreCase) != -1
                    )
                {
                    throw new ETMS.AppContext.BusinessException("Security.User.DeleteFailed.UserIsUsing", new object[] { user.LoginName });
                }
                //ö������Լ���쳣��ת��Ϊҵ���쳣
                if (ex.Message.IndexOf("FK_Site_Teacher_Site_User", StringComparison.InvariantCultureIgnoreCase) != -1
                    ||
                    ex.Message.IndexOf("FK_RES_TEAC_REFER_SITE_TEA", StringComparison.InvariantCultureIgnoreCase) != -1
                    )
                {
                    throw new ETMS.AppContext.BusinessException("Security.User.DeleteFailed.UserIsUsing", new object[] { user.LoginName });
                }
                //�����δ�������׳�
                throw ex;
            }
        }

        /// <summary>
        /// �����û�ID��ȡ�û���Ϣ
        /// </summary>
        /// <param name="userID">�û�ID</param>
        /// <returns>�û���Ϣ�����û�ҵ����û����򷵻� NotFoundUserByIDException�쳣��</returns>
        public User GetUserByID(int userID)
        {
            User findUser = (User)DAL.Query((int)userID);
            if (findUser == null)
                throw new ETMS.AppContext.BusinessException("Security.UserLogic.NotFoundUser");
            return findUser;
        }

        /// <summary>
        /// �����û�ID��ȡ�û���Ϣ
        /// </summary>
        /// <param name="userID">�û�ID</param>
        /// <returns>�û���Ϣ�����û�ҵ����û����򷵻� NotFoundUserByIDException�쳣��</returns>
        public User GetUserBaseData(int userID)
        {
           DataTable dt= ((UserDataAccess)DAL).GetUserBaseData(userID);
            return dt.Rows.Count > 0 ? dt.Rows[0].ToEntity<User>() : null;
        }

        /// <summary>
        /// �����û�ID��ȡ�û���Ϣ
        /// </summary>
        /// <param name="userID">�û�ID</param>
        /// <returns>�û���Ϣ�����û�ҵ����û����򷵻� NotFoundUserByIDException�쳣��</returns>
        public User GetUserInfoByID(int userID)
        {
            User findUser = (User)DAL.Query((int)userID);            
            return findUser ?? new User();
        }

        /// <summary>
        /// ���ݵ�½����ȡ�û���Ϣ
        /// </summary>
        /// <param name="loginName">��½��</param>
        /// <returns>�û���Ϣ�����û�ҵ����û����򷵻�NotFoundUserByLoginNameException�쳣��</returns>
        public User GetUserByLoginName(string loginName)
        {
            User[] findUsers = (User[])DAL.Query(string.Format(" AND LoginName='{0}'", loginName.Replace("'", "''")));
            if (findUsers.Length == 0)
                return null;
            else
                return findUsers[0];
        }

        /// <summary>
        /// ��ѯ�û��б�
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
