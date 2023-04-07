using System;
using ETMS.Components.Scrom.Implement.DAL;
namespace ETMS.Components.Scrom.Implement.BLL
{
    public partial class ObjectivesLogic
    {
        private static readonly ObjectivesDataAccess ObjectivesDal = new ObjectivesDataAccess();
        /// <summary>
        /// SetValue("cmi.objectives.n.id",id) 增加一个知识点
        /// </summary>
        /// <param name="ResourceID"></param>
        /// <param name="ObjectiveID"></param>
        /// <returns></returns>
        public int AddObjective(Guid ResourceID, string ObjectiveID)
        {
            return ObjectivesDal.AddObjective(ResourceID, ObjectiveID);
        }

        /// <summary>
        /// 根据n获取知识点代码
        /// </summary>
        /// <param name="resourceID">资源ID</param>
        /// <param name="n">知识点的索引</param>
        /// <returns></returns>
        public string GetObjectiveByIndex(Guid resourceID, int n)
        {
            return ObjectivesDal.GetObjectiveByIndex(resourceID, n);
        }

        /// <summary>
        /// 知识点数量 返回整数
        /// </summary>
        /// <param name="resourceID"></param>
        /// <returns></returns>
        public int GetObjectivesCount(Guid resourceID)
        {
            return ObjectivesDal.GetObjectivesCount(resourceID);
        }

        /// <summary>
        /// 获取知识点得分0-100
        /// </summary>
        /// <param name="ResourceID"></param>
        /// <param name="UserID"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public string GetObjectivesScoreRaw(Guid ResourceID,Guid itemCourseResID, int UserID, int n)
        {
            return ObjectivesDal.GetObjectivesScoreRaw(ResourceID,itemCourseResID, UserID, n);
        }

        /// <summary>
        /// 设置知识点得分0-100
        /// </summary>
        /// <param name="ResourceID"></param>
        /// <param name="UserID"></param>
        /// <param name="n"></param>
        /// <param name="ScoreRaw"></param>
        public void SetObjectivesScoreRaw(Guid ResourceID, int UserID, int n, string ScoreRaw)
        {
            ObjectivesDal.SetObjectivesScoreRaw(ResourceID, UserID, n, ScoreRaw);
        }

        /// <summary>
        /// 获取知识点状态
        /// </summary>
        /// <param name="ResourceID"></param>
        /// <param name="UserID"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public string GetObjectivesStatus(Guid ResourceID, int UserID, int n)
        {
            return ObjectivesDal.GetObjectivesStatus(ResourceID,UserID,n);
        }

        /// <summary>
        /// 设置知识点状态
        /// </summary>
        /// <param name="ResourceID"></param>
        /// <param name="UserID"></param>
        /// <param name="n"></param>
        /// <param name="Status"></param>
        public void SetObjectivesStatus(Guid ResourceID, int UserID, int n, string Status)
        {
            ObjectivesDal.SetObjectivesStatus(ResourceID, UserID, n, Status);
        }

    }
}
