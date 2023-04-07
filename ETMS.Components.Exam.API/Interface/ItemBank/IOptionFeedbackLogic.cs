using System;
using ETMS.Components.Exam.API.Entity.ItemBank;
using System.Collections.Generic;
using System.ServiceModel;
namespace ETMS.Components.Exam.API.Interface.ItemBank
{
    /// <summary>
    /// 试题选项的答题反馈
    /// </summary>
    [ServiceContract(Namespace = "http://Autumn.Business.LMS.Interface.ItemBank/IOptionFeedbackLogic")]
    public interface IOptionFeedbackLogic
    {
        /// <summary>
        /// 添加选项反馈(可添加多项组合)
        /// </summary>
        /// <param name="feedback">选项反馈对象</param>
        /// <returns>反馈ID</returns>
        [OperationContract]
        Guid Add(OptionFeedback feedback);
        /// <summary>
        /// 删除指定的选项反馈
        /// </summary>
        /// <param name="feedbackID">反馈ID</param>
        [OperationContract]
        void Delete(Guid feedbackID);
        /// <summary>
        /// 删除指定试题中所有答题反馈
        /// </summary>
        /// <param name="questionID">试题ID</param>
        [OperationContract]
        void DeleteInQuestion(System.Guid questionID);
        /// <summary>
        /// 根据试题ID,返回所有选项组合的反馈
        /// </summary>
        /// <param name="questionID">试题ID</param>
        /// <returns>选项反馈集合</returns>
        [OperationContract]
        IList<OptionFeedback> GetFeedback(Guid questionID);
        /// <summary>
        /// 得到匹配的试题反馈
        /// </summary>
        /// <param name="questionID">试题ID</param>
        /// <param name="options">用户答案组合</param>
        /// <returns>反馈内容</returns>
        [OperationContract]
        IList<string> GetOptionFeedback(Guid questionID, string options);
        /// <summary>
        /// 判断试题是否存在选项反馈
        /// </summary>
        /// <param name="questionID">试题ID</param>
        /// <returns></returns>
        [OperationContract]
        bool IsExist(Guid questionID);
        /// <summary>
        /// 修改选项反馈
        /// </summary>
        /// <param name="feedbackID">反馈ID</param>
        /// <param name="options">选项组合内容</param>
        /// <param name="content">反馈内容</param>
        [OperationContract]
        void Update(Guid feedbackID, string options, string content);
        /// <summary>
        /// 更新试题选项反馈
        /// </summary>
        /// <param name="newOptionFeedback"></param>
        [OperationContract]
        void UpdateOptionFeedback(OptionFeedback newOptionFeedback);
    }
}
