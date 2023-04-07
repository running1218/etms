using System;
using System.Collections.Generic;

using Autumn.Context;
using Autumn.Objects.Factory;
using ETMS.Components.Exam.API.Entity.ItemBank;
using ETMS.Components.Exam.Implement.DAL.ItemBank;
using ETMS.Components.Exam.API.Interface.ItemBank;
namespace ETMS.Components.Exam.Implement.BLL.ItemBank
{
    public class OptionFeedbackLogic : IMessageSourceAware, IInitializingObject, IOptionFeedbackLogic
    {
        public IOptionFeedbackDao OptionFeedbackDao { get; set; }

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
            if (OptionFeedbackDao == null)
            {
                throw new NotImplementedException("please set OptionFeedback Property First!");
            }
        }
        #endregion


        /// <summary>
        /// 添加选项反馈(可添加多项组合)
        /// </summary>
        /// <param name="feedback">选项反馈对象</param>
        /// <returns>反馈ID</returns>
        public Guid Add(OptionFeedback feedback)
        {
            try
            {
                if (feedback.OptionFeedbackID == Guid.Empty)
                    feedback.OptionFeedbackID = Guid.NewGuid() ;

                feedback.CreatedUserID = ETMS.AppContext.UserContext.Current.UserID;
                feedback.CreatedDate = DateTime.Now;
                feedback.UpdatedUserID = feedback.CreatedUserID;
                feedback.UpdatedDate = feedback.CreatedDate;
                feedback.IsDelete = false;
                OptionFeedbackDao.Add(feedback);
            }
            catch (Autumn.Dao.DataIntegrityViolationException ex)
            {
                throw (new Exception(ex.Message));
            }
            return feedback.OptionFeedbackID;
        }

        /// <summary>
        /// 删除指定的选项反馈
        /// </summary>
        /// <param name="feedbackID">反馈ID</param>
        public void Delete(Guid feedbackID)
        {
            OptionFeedbackDao.Delete(feedbackID);
        }
        /// <summary>
        /// 删除指定试题中所有答题反馈
        /// </summary>
        /// <param name="questionID">试题ID</param>
        public void DeleteInQuestion(Guid questionID)
        {
            OptionFeedbackDao.Deletes(questionID);
        }
        /// <summary>
        /// 根据试题ID,返回所有选项组合的反馈
        /// </summary>
        /// <param name="questionID">试题ID</param>
        /// <returns>选项反馈集合</returns>
        public IList<OptionFeedback> GetFeedback(Guid questionID)
        {
            return OptionFeedbackDao.GetFeedback(questionID);
        }

        /// <summary>
        /// 修改选项反馈
        /// </summary>
        /// <param name="feedbackID">反馈ID</param>
        /// <param name="options">选项组合内容</param>
        /// <param name="content">反馈内容</param>
        public void Update(Guid feedbackID, string options, string content)
        {
            if(feedbackID == Guid.Empty)
                return;

            int userID = ETMS.AppContext.UserContext.Current.UserID;
            DateTime updatedDate = DateTime.Now;
            OptionFeedbackDao.Update(feedbackID, options, content, userID, updatedDate);
        }

        /// <summary>
        /// 得到匹配的试题反馈
        /// </summary>
        /// <param name="questionID">试题ID</param>
        /// <param name="options">用户答案组合</param>
        /// <returns>反馈内容</returns>
        public IList<String> GetOptionFeedback(Guid questionID, string options)
        {
            return OptionFeedbackDao.GetOptionFeedback(questionID, options);
        }

        /// <summary>
        /// 判断试题是否存在选项反馈
        /// </summary>
        /// <param name="questionID">试题ID</param>
        /// <returns></returns>
        public bool IsExist(Guid questionID)
        {
            return OptionFeedbackDao.IsExist(questionID);
        }

        /// <summary>
        /// 修改选项反馈
        /// </summary>
        /// <param name="tmp">反馈对象</param>
        public void UpdateOptionFeedback(OptionFeedback tmp)
        {
            Update(tmp.OptionFeedbackID,tmp.Options,tmp.Content);
        }

    }
}
