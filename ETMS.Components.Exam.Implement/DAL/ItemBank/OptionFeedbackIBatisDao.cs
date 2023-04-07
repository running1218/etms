using System;
using System.Collections.Generic;

using System.Collections;
using Autumn.Data.ORM.IBatis;
using ETMS.Components.Exam.API.Entity.ItemBank;
namespace ETMS.Components.Exam.Implement.DAL.ItemBank
{
    /// <summary>
    /// 选项反馈数据访问的实现
    /// </summary>
    public class OptionFeedbackIBatisDao : ReadWriteDataMapperDaoSupport, IOptionFeedbackDao
    {
        /// <summary>
        /// 添加选项反馈
        /// </summary>
        /// <param name="feedback">选项反馈对象</param>
        /// <returns>反馈ID</returns>
        public Guid Add(OptionFeedback feedback)
        {
            DataMapperClient_Write.Insert("OptionFeedback.Insert", feedback);
            return feedback.OptionFeedbackID;
        }

        /// <summary>
        /// 删除指定的选项反馈
        /// </summary>
        /// <param name="feedbackID">反馈ID</param>
        public void Delete(Guid feedbackID)
        {
            DataMapperClient_Write.Delete("OptionFeedback.Delete", feedbackID);
        }

        /// <summary>
        /// 删除指定试题的所有选项
        /// </summary>
        /// <param name="questionID"></param>
        public void Deletes(Guid questionID)
        {
            DataMapperClient_Write.Delete("OptionFeedback.Deletes", questionID);
        }

        /// <summary>
        /// 根据试题ID,返回所有选项组合的反馈
        /// </summary>
        /// <param name="questionID">试题ID</param>
        /// <returns>选项反馈集合</returns>
        public IList<OptionFeedback> GetFeedback(Guid questionID)
        {
            return DataMapperClient_Read.QueryForList<OptionFeedback>("OptionFeedback.GetByID", questionID);
        }

        /// <summary>
        /// 修改选项反馈
        /// </summary>
        /// <param name="feedbackID">反馈ID</param>
        /// <param name="options">选项组合内容</param>
        /// <param name="content">反馈内容</param>
        public void Update(Guid feedbackID, string options, string content, int updatedUserID, DateTime updatedDate)
        {
            DataMapperClient_Write.Update("OptionFeedback.Update", new
            {
                Content = content,
                Options = options,
                OptionFeedbackID=feedbackID,
                UpdatedUserID = updatedUserID,
                UpdatedDate = updatedDate
            });
        }

        /// <summary>
        /// 得到匹配的试题反馈
        /// </summary>
        /// <param name="questionID">试题ID</param>
        /// <param name="options">用户答案组合</param>
        /// <returns>反馈内容</returns>
        public IList<String> GetOptionFeedback(Guid questionID, string options)
        {
            Hashtable ht = new Hashtable();
            ht.Add("QuestionID", questionID);
            ht.Add("Options", options);
            //string result = (string)DataMapperClient_Read.QueryForObject("OptionFeedback.Content", questionID);
            //return result;
            return DataMapperClient_Read.QueryForList<String>("OptionFeedback.Content", ht);
        }

        /// <summary>
        /// 判断试题是否存在选项反馈
        /// </summary>
        /// <param name="questionID">试题ID</param>
        /// <returns></returns>
        public bool IsExist(Guid questionID)
        {
            int result = (int)DataMapperClient_Read.QueryForObject("OptionFeedback.IsExist", questionID);
            if (result > 0)
                return true;
            else
                return false;
        }
    }
}
