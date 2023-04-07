using System;
using Autumn.Data.ORM.IBatis;
using ETMS.Components.Exam.API.Entity.ItemBank;
namespace ETMS.Components.Exam.Implement.DAL.ItemBank
{
    /// <summary>
    /// 试题反馈数据访问的实现
    /// </summary>
    public class QuestionFeedbackIBatisDao : ReadWriteDataMapperDaoSupport, IQuestionFeedbackDao
    {
        /// <summary>
        /// 添加试题反馈
        /// </summary>
        /// <param name="feedback">试题反馈对象</param>
        /// <returns>反馈ID</returns>
        public Guid Add(QuestionFeedback feedback)
        {
            DataMapperClient_Write.Insert("QuestionFeedback.Insert", feedback);
            return feedback.FeedbackID;
        }

        /// <summary>
        /// 删除指定试题反馈
        /// </summary>
        /// <param name="feedbackID">反馈ID</param>
        public void Delete(Guid feedbackID)
        {
            DataMapperClient_Write.Delete("QuestionFeedback.Delete", feedbackID);
        }

        /// <summary>
        /// 删除指定试题所有反馈
        /// </summary>
        /// <param name="questionID">试题ID</param>
        public void Deletes(Guid questionID)
        {
            DataMapperClient_Write.Delete("QuestionFeedback.Deletes", questionID);
        }

        /// <summary>
        /// 通过试题ID得到试题反馈对象(仅一条)
        /// </summary>
        /// <param name="questionID">试题ID</param>
        /// <returns>试题反馈对象</returns>
        public QuestionFeedback GetFeedback(Guid questionID)
        {
            return (QuestionFeedback)DataMapperClient_Read.QueryForObject("QuestionFeedback.GetByID", questionID);
        }

        /// <summary>
        /// 修改正确结果时的反馈
        /// </summary>
        /// <param name="feedbackID">反馈ID</param>
        /// <param name="content">具体内容</param>
        /// <param name="updatedUserID">修改者ID</param>
        /// <param name="updatedDate">修改时间</param>
        public void ModifyRightFeedback(Guid feedbackID, string content, int updatedUserID, DateTime updatedDate)
        {
            DataMapperClient_Write.Update("QuestionFeedback.ModifyRight", new
            {
                FeedbackID = feedbackID,
                RightContent = content,
                UpdatedUserID = updatedUserID,
                UpdatedDate = updatedDate
            });
        }

        /// <summary>
        /// 修改错误结果时的反馈
        /// </summary>
        /// <param name="feedbackID">反馈ID</param>
        /// <param name="content">具体内容</param>
        /// <param name="updatedUserID">修改者ID</param>
        /// <param name="updatedDate">修改时间</param>
        public void ModifyWrongFeedback(Guid feedbackID, string content, int updatedUserID, DateTime updatedDate)
        {
            DataMapperClient_Write.Update("QuestionFeedback.ModifyWrong", new
            {
                FeedbackID = feedbackID,
                WrongContent = content,
                UpdatedUserID = updatedUserID,
                UpdatedDate = updatedDate
            });
        }

        /// <summary>
        /// 修改反馈对象
        /// </summary>
        /// <param name="newFeedback">反馈对象</param>
        /// <param name="updatedUserID">修改者ID</param>
        /// <param name="updatedDate">修改时间</param>
        public void Update(QuestionFeedback newFeedback, int updatedUserID, DateTime updatedDate)
        {
            DataMapperClient_Write.Update("QuestionFeedback.Update", new
            {
                FeedbackID = newFeedback.FeedbackID,
                RightContent = newFeedback.RightContent,
                WrongContent = newFeedback.WrongContent,
                UpdatedUserID = updatedUserID,
                UpdatedDate = updatedDate
            });
        }

        /// <summary>
        /// 得到试题答题正确时的反馈
        /// </summary>
        /// <param name="questionID">试题ID</param>
        /// <returns>具体内容</returns>
        public string GetRightFeedback(Guid questionID)
        {
            string result = (string)DataMapperClient_Read.QueryForObject("QuestionFeedback.RightContent", questionID);
            return result;
        }

        /// <summary>
        /// 得到试题答错时的反馈
        /// </summary>
        /// <param name="questionID">试题ID</param>
        /// <returns>具体内容</returns>
        public string GetWrongFeedback(Guid questionID)
        {
            string result =(string)DataMapperClient_Read.QueryForObject("QuestionFeedback.WrongContent", questionID);
            return result;
        }

        /// <summary>
        /// 指定试题是否存在试题反馈(存在则不允许再添加)
        /// </summary>
        /// <param name="questionID">试题ID</param>
        /// <returns>true存在</returns>
        public bool IsExist(Guid questionID)
        {
            int result = (int)DataMapperClient_Read.QueryForObject("QuestionFeedback.IsExist", questionID);
            if (result > 0)
                return true;
            else
                return false;
        }
    }
}
