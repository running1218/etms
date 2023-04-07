using System;
using ETMS.Components.Scrom.Implement.DAL;
namespace ETMS.Components.Scrom.Implement.BLL
{
    public class InteractionsLogic
    {
        private static InteractionsDataAccess InteractionsDal = new InteractionsDataAccess();
        /// <summary>
        /// �����µĽ�����������Ϣ��
        /// </summary>
        /// <param name="resourceID">��ԴID</param>
        /// <param name="interactionID">����ID����Scorm�μ�ָ����ID</param>
        /// <returns></returns>
        public int AddInteraction(Guid resourceID, string interactionID)
        {
            return InteractionsDal.AddInteraction(resourceID,interactionID);
        }

        /// <summary>
        /// ��ȡ���ݿ������е���Դ�Ľ�����
        /// </summary>
        /// <param name="resourceID">��ԴID</param>
        /// <returns></returns>
        public int GetInteractionCount(Guid resourceID)
        {
            return InteractionsDal.GetInteractionCount(resourceID);
        }

        /// <summary>
        /// ���ݽ�����������ȡ�����ģɣ�
        /// </summary>
        /// <param name="resourceID">��ԴID</param>
        /// <param name="n">����������</param>
        /// <returns></returns>
        public string GetInteractionByIndex(Guid resourceID, int n)
        {
            return InteractionsDal.GetInteractionByIndex(resourceID,n);
        }

        /// <summary>
        /// ��¼�û�����Ψһ��ʶ(������ݿ���û�ж�Ӧ�ļ�¼�����������ݣ������޸�����) 
        /// </summary>
        /// <param name="resourceID">��ԴID</param>
        /// <param name="n">����������</param>
        /// <param name="userID">�û�ID</param>
        /// <param name="response">value</param>
        /// <returns></returns>
        public void SetInteractionID(Guid resourceID, int n, int userID, string value)
        {
            //InteractionsDal.SetInteractionID(resourceID, n, userID, response);
        }

        /// <summary>
        /// ��¼�û������Ĵ�(������ݿ���û�ж�Ӧ�ļ�¼�����������ݣ������޸�����) 
        /// </summary>
        /// <param name="resourceID">��ԴID</param>
        /// <param name="n">����������</param>
        /// <param name="userID">�û�ID</param>
        /// <param name="response">�û���</param>
        /// <returns></returns>
        public void SetInteractionResponse(Guid resourceID, Guid itemCourseResID, int n, int userID, string response)
        {
            InteractionsDal.SetInteractionResponse(resourceID, itemCourseResID,n, userID, response);
        }

        /// <summary>
        /// ��¼�û������Ľ��(������ݿ���û�ж�Ӧ�ļ�¼�����������ݣ������޸�����) 
        /// </summary>
        /// <param name="resourceID">��ԴID</param>
        /// <param name="n">����������</param>
        /// <param name="userID">�û�ID</param>
        /// <param name="result">�����Ľ��</param>
        /// <returns></returns>
        public void SetInteractionResult(Guid resourceID,Guid itemCourseResID, int n, int userID, string result)
        {
            InteractionsDal.SetInteractionResult(resourceID,itemCourseResID, n, userID, result);
        }

        /// <summary>
        /// ��ȡ�û������Ĵ�
        /// </summary>
        /// <param name="resourceID">��ԴID</param>
        /// <param name="n">����������</param>
        /// <param name="userID">�û�ID</param>
        /// <returns></returns>
        public string GetInteractionResponse(Guid resourceID,Guid itemCourseResID, int n, int userID)
        {
            return InteractionsDal.GetInteractionResponse(resourceID, itemCourseResID,n, userID);
        }

    }
}
