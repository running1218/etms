using System;
using ETMS.Components.Exam.API.Entity.ItemBank;
using System.ServiceModel;
namespace ETMS.Components.Exam.API.Interface.ItemBank
{
    /// <summary>
    /// 试题的答题反馈的逻辑接口
    /// </summary>
    [ServiceContract(Namespace = "http://Autumn.Business.LMS.Interface.ItemBank/IQuestionFeedbackLogic")]
    public interface IQuestionFeedbackLogic
    {
        /// <summary>
        /// 添加试题反馈
        /// </summary>
        /// <param name="feedback">试题反馈对象</param>
        /// <returns>反馈ID</returns>
        [OperationContract]
        Guid Add(QuestionFeedback feedback);
        /// <summary>
        /// 删除指定试题反馈(伪删除)
        /// </summary>
        /// <param name="feedbackID">反馈ID</param>
        [OperationContract]
        void Delete(Guid feedbackID);
        /// <summary>
        /// 删除指定试题中所有答题反馈
        /// </summary>
        /// <param name="questionID">试题ID</param>
        [OperationContract]
        void DeleteInQuestion(Guid questionID);
        /// <summary>
        /// 更新试题反馈
        /// </summary>
        /// <param name="newFeedback"></param>
        [OperationContract]
        void Update(QuestionFeedback newFeedback);
        /// <summary>
        /// 通过试题ID得到试题反馈对象(仅一条)
        /// </summary>
        /// <param name="questionID">试题ID</param>
        /// <returns>试题反馈对象</returns>
        [OperationContract]
        QuestionFeedback GetFeedback(Guid questionID);
        /// <summary>
        /// 得到试题答题正确时的反馈
        /// </summary>
        /// <param name="questionID">试题ID</param>
        /// <returns>具体内容</returns>
        [OperationContract]
        string GetRightFeedback(Guid questionID);
        /// <summary>
        /// 得到试题答错时的反馈
        /// </summary>
        /// <param name="questionID">试题ID</param>
        /// <returns>具体内容</returns>
        [OperationContract]
        string GetWrongFeedback(Guid questionID);
        /// <summary>
        /// 指定试题是否存在试题反馈(存在则不允许再添加)
        /// </summary>
        /// <param name="questionID">试题ID</param>
        /// <returns>true存在</returns>
        [OperationContract]
        bool IsExist(Guid questionID);
        /// <summary>
        /// 修改正确结果时的反馈
        /// </summary>
        /// <param name="feedbackID">反馈ID</param>
        /// <param name="content">具体内容</param>
        [OperationContract]
        void UpdateRightFeedback(Guid feedbackID, string content);
        /// <summary>
        /// 修改错误结果时的反馈
        /// </summary>
        /// <param name="feedbackID">反馈ID</param>
        /// <param name="content">具体内容</param>
        [OperationContract]
        void UpdateWrongFeedback(Guid feedbackID, string content);
    }
}
