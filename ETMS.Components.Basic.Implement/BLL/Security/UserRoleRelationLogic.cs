using System.Collections.Generic;

using ETMS.Components.Basic.Implement.DAL.Security;
using ETMS.Components.Basic.API.Entity.Security;
namespace ETMS.Components.Basic.Implement.BLL.Security
{
    /// <summary>
    /// ��ɫ-�û���ϵ ������
    /// </summary>
    public class UserRoleRelationLogic
    {
        UserRoleDataAccess DAL = new UserRoleDataAccess();
        /// <summary>
        /// �����û���ɫ�б�
        /// </summary>
        /// <param name="userID">�û�ID</param>
        /// <param name="roleIDs">����Ľ�ɫ</param>
        /// <param name="creator">������</param>
        public void Save(int userID, string roleIDs, string creator)
        {
            DAL.Save(userID, roleIDs, creator);
        }

        /// <summary>
        /// ��ȡ�û�������Ľ�ɫ�б�
        /// </summary>
        /// <param name="userID">�û�ID</param>
        /// <returns>������Ľ�ɫ�б�</returns>
        public IList<Role> Query(int userID)
        {
            return DAL.Query(userID);
        }
    }
}
