// File:    QuestionLogic.cs
// Author:  Administrator
// Created: 2011年12月17日 10:24:15
// Purpose: Definition of Class QuestionLogic

using System;
using System.Collections.Generic;
using ETMS.Components.Exam.API.Entity.ItemBank;
using Autumn.Objects.Factory;
using Autumn.Context;
using ETMS.Components.Exam.Implement.DAL.ItemBank;
using ETMS.AppContext;
using ETMS.Components.Exam.API.Entity;
using ETMS.Components.Exam.API.Interface.ItemBank;
namespace ETMS.Components.Exam.Implement.BLL.ItemBank
{
    ///<summary>
    /// 试题相关对外服务逻辑层
    ///</summary>
    public class QuestionLogic : IQuestionLogic, IMessageSourceAware, IInitializingObject
    {
        public IQuestionDao QuestionDao { get; set; }

        #region Base 接口
        ///<summary>
        /// 添加一个试题
        ///</summary>
        /// <param name="question">要添加的试题的基类。必须是系统支持的几种试题的具体类实例。</param>
        public Guid AddQuestion(Question question)
        {
            question.QuestionID = (question.QuestionID == Guid.Empty) ? Guid.NewGuid() : question.QuestionID;
            question.CreatedUserID = ETMS.AppContext.UserContext.Current.UserID;
            this.QuestionDao.AddQuestion(question);
            return question.QuestionID;
        }

        ///<summary>
        /// 添加一个试题
        ///</summary>
        /// <param name="questionID">要更新的试题ID</param>
        /// <param name="newQuestion">更新的试题实体（必须为试题类型的实体）</param>
        public void Update(Guid questionID, Question newQuestion)
        {
            newQuestion.QuestionID = questionID;
            newQuestion.AuditStatus = AuditType.Waiting;
            newQuestion.UpdatedUserID = ETMS.AppContext.UserContext.Current.UserID;
            this.QuestionDao.Update(newQuestion);
        }

        ///<summary>
        /// 删除指定的试题
        ///</summary>
        /// <param name="questionID">要删除的试题的ID</param>
        public void Delete(Guid questionID)
        {
            this.QuestionDao.Delete(questionID, UserContext.Current.UserID);
        }

        ///<summary>
        /// 批量删除指定的试题
        ///</summary>
        /// <param name="questionIDs">要删除的试题的IDs</param>
        public void DeleteBatch(IList<Guid> questionIDs)
        {
            this.QuestionDao.DeleteBatch(questionIDs, UserContext.Current.UserID);
        }

        ///<summary>
        /// 得到某一试题。会根据试题的类型得到具体试题类型的实例。
        ///</summary>
        /// <param name="questionID">要获取的试题的ID</param>
        public Question GetByID(Guid questionID)
        {
            return this.QuestionDao.GetByID(questionID);
        }
        /// <summary>
        /// 修改答案字符串
        /// </summary>
        /// <param name="questionID">试题ID</param>
        /// <param name="answer">答案字符串</param>
        public void UpdateAnswers(Guid questionID, string answer)
        {
            if (String.IsNullOrEmpty(answer) || questionID.Equals(Guid.Empty))
                throw new ETMS.AppContext.BusinessException("ItemBank.Question.ParametersInvalid 传入的参数包含不合法参数");
            else
                this.QuestionDao.UpdateAnswers(questionID, answer, UserContext.Current.UserID);
        }
        /// <summary>
        /// 获取答案字符串
        /// </summary>
        /// <param name="questionID">试题ID</param>
        /// <returns>答案字符串</returns>
        public string GetAnswersByID(Guid questionID)
        {
            if (questionID.Equals(Guid.Empty))
                throw new ETMS.AppContext.BusinessException("ItemBank.Question.ParametersInvalid");
            else
                return this.QuestionDao.GetAnswersByID(questionID);
        }

        /// <summary>
        /// 删除指定分类下的所有试题
        /// </summary>
        /// <param name="classID">分类ID</param>
        public void DeleteClassID(Guid classID)
        {
            if (classID.Equals(Guid.Empty))
                throw new ETMS.AppContext.BusinessException("ItemBank.Question.ParametersInvalid");
            else
                this.QuestionDao.DeleteClassID(classID);
        }
        /// <summary>
        /// 修改指定分类的试题到新分类
        /// </summary>
        /// <param name="oldClassID">老分类</param>
        /// <param name="newClassID">新分类</param>
        public void UpdateClassID(Guid oldClassID, Guid newClassID)
        {
            if (oldClassID.Equals(Guid.Empty) || newClassID.Equals(Guid.Empty))
                throw new ETMS.AppContext.BusinessException("ItemBank.Question.ParametersInvalid 传入的参数包含不合法参数");
            else
                this.QuestionDao.UpdateClassID(oldClassID, newClassID);
        }

        public void SetShareState(Guid questionID, ShareType state)
        {
            this.QuestionDao.SetShareState(questionID, Convert.ToInt32(state));
        }

        public void SetAuditState(Guid questionID, AuditType state)
        {
            this.QuestionDao.SetAuditState(questionID, Convert.ToInt32(state));
        }
        #endregion

        #region IInitializingObject 成员

        public void AfterPropertiesSet()
        {
            if (this.QuestionDao == null)
                throw new Exception("please set QuestionDao Property First!");
        }

        #endregion

        #region IMessageSourceAware 成员

        public IMessageSource MessageSource { get; set; }

        #endregion
    }
}