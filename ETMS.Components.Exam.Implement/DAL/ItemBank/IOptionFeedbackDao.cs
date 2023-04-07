using System;
using System.Collections.Generic;
using ETMS.Components.Exam.API.Entity.ItemBank;

namespace ETMS.Components.Exam.Implement.DAL.ItemBank
{
    /// <summary>
    /// 选项反馈数据访问接口
    /// </summary>
    public interface IOptionFeedbackDao
    {
        /// <summary>
        /// 添加选项反馈
        /// </summary>
        /// <param name="feedback">选项反馈对象</param>
        /// <returns>反馈ID</returns>
        Guid Add(OptionFeedback feedback);
        
        /// <summary>
        /// 删除指定的选项反馈
        /// </summary>
        /// <param name="feedbackID">反馈ID</param>
        void Delete(Guid feedbackID);

        /// <summary>
        /// 删除指定试题的所有选项
        /// </summary>
        /// <param name="feedbackID">反馈ID</param>
        void Deletes(Guid questionID);

        /// <summary>
        /// 根据试题ID,返回所有选项组合的反馈
        /// </summary>
        /// <param name="questionID">试题ID</param>
        /// <returns>选项反馈集合</returns>
        IList<OptionFeedback> GetFeedback(Guid questionID);

        /// <summary>
        /// 修改选项反馈
        /// </summary>
        /// <param name="feedbackID">反馈ID</param>
        /// <param name="options">选项组合内容</param>
        /// <param name="content">反馈内容</param>
        void Update(Guid feedbackID, string options, string content, int updatedUserID, DateTime updatedDate);

        /// <summary>
        /// 得到匹配的试题反馈
        /// </summary>
        /// <param name="questionID">试题ID</param>
        /// <param name="options">用户答案组合</param>
        /// <returns>反馈内容</returns>
        IList<String> GetOptionFeedback(Guid questionID, string options);

        /// <summary>
        /// 判断试题是否存在选项反馈
        /// </summary>
        /// <param name="questionID">试题ID</param>
        /// <returns></returns>
        bool IsExist(Guid questionID);
    }
}
