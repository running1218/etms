using System;

using Autumn.Context;
using Autumn.Objects.Factory;
using ETMS.Components.Exam.API.Entity.ItemBank;
using ETMS.Components.Exam.Implement.DAL.ItemBank;
using ETMS.Components.Exam.API.Interface.ItemBank;
namespace ETMS.Components.Exam.Implement.BLL.ItemBank
{
    public class QuestionFeedbackLogic :  IMessageSourceAware, IInitializingObject, IQuestionFeedbackLogic
    {
        public IQuestionFeedbackDao QuestionFeedbackDao { get; set; }

        #region IMessageSourceAware 成员
        public IMessageSource MessageSource
        {
            get;
            set;
        }
        #endregion
        #region IInitializingObject 成员
        public void AfterPropertiesSet()
        {
            if (QuestionFeedbackDao == null)
            {
                throw new NotImplementedException("please set QuestionFeedbackDao Property First!");
            }
        }
        #endregion

        /// <summary>
        /// 为指定试题添加反馈
        /// </summary>
        /// <param name="feedback">试题反馈对象</param>
        /// <returns>反馈ID</returns>
        public Guid Add(QuestionFeedback feedback)
        {
            try{
                if (feedback.QuestionID == Guid.Empty)
                    return Guid.Empty;

                if (!IsExist(feedback.QuestionID))
                {
                    if (feedback.FeedbackID == Guid.Empty)
                        feedback.FeedbackID = Guid.NewGuid();

                    feedback.CreatedUserID = AppContext.UserContext.Current.UserID;
                    feedback.CreatedDate = DateTime.Now;
                    feedback.UpdatedUserID = feedback.CreatedUserID;
                    feedback.UpdatedDate = feedback.CreatedDate;
                    feedback.IsDelete = false;
                    QuestionFeedbackDao.Add(feedback);
                }
            }
            catch (Autumn.Dao.DataIntegrityViolationException ex)
            {
                throw (new Exception(ex.Message));
            }
            return feedback.FeedbackID;
        }

        /// <summary>
        /// 删除指定试题反馈(伪删除)
        /// </summary>
        /// <param name="feedbackID">反馈ID</param>
        public void Delete(Guid feedbackID)
        {
            QuestionFeedbackDao.Delete(feedbackID);
        }

        /// <summary>
        /// 删除指定试题中所有答题反馈
        /// </summary>
        /// <param name="questionID">试题ID</param>
        public void DeleteInQuestion(Guid questionID)
        {
            QuestionFeedbackDao.Deletes(questionID);
        }
        /// <summary>
        /// 通过试题ID得到试题反馈对象(仅一条)
        /// </summary>
        /// <param name="questionID">试题ID</param>
        /// <returns>试题反馈对象</returns>
        public QuestionFeedback GetFeedback(Guid questionID)
        {
            return QuestionFeedbackDao.GetFeedback(questionID);
        }

        /// <summary>
        /// 修改正确结果时的反馈
        /// </summary>
        /// <param name="feedbackID">反馈ID</param>
        /// <param name="content">具体内容</param>
        public void UpdateRightFeedback(Guid feedbackID, string content)
        {
            int userID = AppContext.UserContext.Current.UserID;
            DateTime updatedDate = DateTime.Now;
            QuestionFeedbackDao.ModifyRightFeedback(feedbackID, content, userID, updatedDate);
        }

        /// <summary>
        /// 修改错误结果时的反馈
        /// </summary>
        /// <param name="feedbackID">反馈ID</param>
        /// <param name="content">具体内容</param>
        public void UpdateWrongFeedback(Guid feedbackID, string content)
        {
            int userID = AppContext.UserContext.Current.UserID;
            DateTime updatedDate = DateTime.Now;
            QuestionFeedbackDao.ModifyWrongFeedback(feedbackID, content, userID, updatedDate);
        }

        /// <summary>
        /// 得到试题答题正确时的反馈
        /// </summary>
        /// <param name="questionID">试题ID</param>
        /// <returns>具体内容</returns>
        public string GetRightFeedback(Guid questionID)
        {
            return QuestionFeedbackDao.GetRightFeedback(questionID);
        }

        /// <summary>
        /// 得到试题答错时的反馈
        /// </summary>
        /// <param name="questionID">试题ID</param>
        /// <returns>具体内容</returns>
        public string GetWrongFeedback(Guid questionID)
        {
            return QuestionFeedbackDao.GetWrongFeedback(questionID);
        }

        /// <summary>
        /// 是否已存在试题反馈(存在则不允许再添加)
        /// </summary>
        /// <param name="questionID">试题ID</param>
        /// <returns>true存在</returns>
        public bool IsExist(Guid questionID)
        {
            return QuestionFeedbackDao.IsExist(questionID);
        }

        /// <summary>
        /// 更新试题反馈
        /// </summary>
        /// <param name="newFeedback">反馈对象</param>
        public void Update(QuestionFeedback newFeedback)
        {
            if (newFeedback.FeedbackID == Guid.Empty)
                return ;
            int userID = AppContext.UserContext.Current.UserID;
            DateTime updatedDate = DateTime.Now;
            QuestionFeedbackDao.Update(newFeedback, userID, updatedDate);
        }
    }
}
