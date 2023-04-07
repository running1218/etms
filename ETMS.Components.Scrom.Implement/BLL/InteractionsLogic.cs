using System;
using ETMS.Components.Scrom.Implement.DAL;
namespace ETMS.Components.Scrom.Implement.BLL
{
    public class InteractionsLogic
    {
        private static InteractionsDataAccess InteractionsDal = new InteractionsDataAccess();
        /// <summary>
        /// 增加新的交互（基础信息）
        /// </summary>
        /// <param name="resourceID">资源ID</param>
        /// <param name="interactionID">交互ID——Scorm课件指定的ID</param>
        /// <returns></returns>
        public int AddInteraction(Guid resourceID, string interactionID)
        {
            return InteractionsDal.AddInteraction(resourceID,interactionID);
        }

        /// <summary>
        /// 获取数据库中已有的资源的交互数
        /// </summary>
        /// <param name="resourceID">资源ID</param>
        /// <returns></returns>
        public int GetInteractionCount(Guid resourceID)
        {
            return InteractionsDal.GetInteractionCount(resourceID);
        }

        /// <summary>
        /// 根据交互的索引获取交互的ＩＤ
        /// </summary>
        /// <param name="resourceID">资源ID</param>
        /// <param name="n">交互的索引</param>
        /// <returns></returns>
        public string GetInteractionByIndex(Guid resourceID, int n)
        {
            return InteractionsDal.GetInteractionByIndex(resourceID,n);
        }

        /// <summary>
        /// 记录用户交互唯一标识(如果数据库中没有对应的记录，则增加数据，否则修改数据) 
        /// </summary>
        /// <param name="resourceID">资源ID</param>
        /// <param name="n">交互的索引</param>
        /// <param name="userID">用户ID</param>
        /// <param name="response">value</param>
        /// <returns></returns>
        public void SetInteractionID(Guid resourceID, int n, int userID, string value)
        {
            //InteractionsDal.SetInteractionID(resourceID, n, userID, response);
        }

        /// <summary>
        /// 记录用户交互的答案(如果数据库中没有对应的记录，则增加数据，否则修改数据) 
        /// </summary>
        /// <param name="resourceID">资源ID</param>
        /// <param name="n">交互的索引</param>
        /// <param name="userID">用户ID</param>
        /// <param name="response">用户答案</param>
        /// <returns></returns>
        public void SetInteractionResponse(Guid resourceID, Guid itemCourseResID, int n, int userID, string response)
        {
            InteractionsDal.SetInteractionResponse(resourceID, itemCourseResID,n, userID, response);
        }

        /// <summary>
        /// 记录用户交互的结果(如果数据库中没有对应的记录，则增加数据，否则修改数据) 
        /// </summary>
        /// <param name="resourceID">资源ID</param>
        /// <param name="n">交互的索引</param>
        /// <param name="userID">用户ID</param>
        /// <param name="result">交互的结果</param>
        /// <returns></returns>
        public void SetInteractionResult(Guid resourceID,Guid itemCourseResID, int n, int userID, string result)
        {
            InteractionsDal.SetInteractionResult(resourceID,itemCourseResID, n, userID, result);
        }

        /// <summary>
        /// 获取用户交互的答案
        /// </summary>
        /// <param name="resourceID">资源ID</param>
        /// <param name="n">交互的索引</param>
        /// <param name="userID">用户ID</param>
        /// <returns></returns>
        public string GetInteractionResponse(Guid resourceID,Guid itemCourseResID, int n, int userID)
        {
            return InteractionsDal.GetInteractionResponse(resourceID, itemCourseResID,n, userID);
        }

    }
}
