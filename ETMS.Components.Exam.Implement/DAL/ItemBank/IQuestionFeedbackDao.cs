using System;
using ETMS.Components.Exam.API.Entity.ItemBank;

namespace ETMS.Components.Exam.Implement.DAL.ItemBank
{
    /// <summary>
    /// 试题反馈数据访问接口
    /// </summary>
    public interface IQuestionFeedbackDao
    {
        /// <summary>
        /// 添加试题反馈
        /// </summary>
        /// <param name="feedback">试题反馈对象</param>
        /// <returns>反馈ID</returns>
        Guid Add(QuestionFeedback feedback);

        /// <summary>
        /// 删除指定试题反馈
        /// </summary>
        /// <param name="feedbackID">反馈ID</param>
        void Delete(Guid feedbackID);

        /// <summary>
        /// 删除指定试题所有反馈
        /// </summary>
        /// <param name="questionID">试题ID</param>
        void Deletes(Guid questionID);

        /// <summary>
        /// 通过试题ID得到试题反馈对象(仅一条)
        /// </summary>
        /// <param name="questionID">试题ID</param>
        /// <returns>试题反馈对象</returns>
        QuestionFeedback GetFeedback(Guid questionID);

        /// <summary>
        /// 修改正确结果时的反馈
        /// </summary>
        /// <param name="feedbackID">反馈ID</param>
        /// <param name="content">具体内容</param>
        void ModifyRightFeedback(Guid feedbackID, string content, int updatedUserID, DateTime updatedDate);

        /// <summary>
        /// 修改错误结果时的反馈
        /// </summary>
        /// <param name="feedbackID">反馈ID</param>
        /// <param name="content">具体内容</param>
        void ModifyWrongFeedback(Guid feedbackID, string content, int updatedUserID, DateTime updatedDate);

        /// <summary>
        /// 更新试题反馈
        /// </summary>
        /// <param name="newFeedback"></param>
        void Update(QuestionFeedback newFeedback, int updatedUserID, DateTime updatedDate);

        /// <summary>
        /// 得到试题答题正确时的反馈
        /// </summary>
        /// <param name="questionID">试题ID</param>
        /// <returns>具体内容</returns>
        string GetRightFeedback(Guid questionID);

        /// <summary>
        /// 得到试题答错时的反馈
        /// </summary>
        /// <param name="questionID">试题ID</param>
        /// <returns>具体内容</returns>
        string GetWrongFeedback(Guid questionID);

        /// <summary>
        /// 指定试题是否存在试题反馈(存在则不允许再添加)
        /// </summary>
        /// <param name="questionID">试题ID</param>
        /// <returns>true存在</returns>
        bool IsExist(Guid questionID);
    }
}
