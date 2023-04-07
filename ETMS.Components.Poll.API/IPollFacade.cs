using System.Collections.Generic;
using ETMS.AppContext.Component;
using ETMS.Components.Poll.API.Entity;
namespace ETMS.Components.Poll.API
{
    /// <summary>
    /// Pollģ�飬����ӿڶ��壬�̳�IComponent
    /// </summary>
    public interface IPollFacade : IComponent
    {
        /// <summary>
        /// ��ȡ�û��ɿ����ĵ����б�
        /// </summary>
        /// <param name="userID">�û�ID</param>
        /// <param name="organizationID">�û���������ID</param>
        /// <returns></returns>
        System.Data.DataTable GetQueryListForUser(int userID, int organizationID);


        /// <summary>
        /// ��ȡ�û��ɿ����ĵ����б�
        /// </summary>
        /// <param name="userID">�û�ID</param>
        /// <param name="organizationID">�û���������ID</param>
        /// <param name="topSize">ͷ��������</param>
        /// <returns></returns>
        IList<Poll_Query> GetQueryListForUser(int userID, int organizationID, int topSize);
    }
}

