// File:    QuestionLogic.cs
// Author:  Administrator
// Created: 2011��12��17�� 10:24:15
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
    /// ������ض�������߼���
    ///</summary>
    public class QuestionLogic : IQuestionLogic, IMessageSourceAware, IInitializingObject
    {
        public IQuestionDao QuestionDao { get; set; }

        #region Base �ӿ�
        ///<summary>
        /// ���һ������
        ///</summary>
        /// <param name="question">Ҫ��ӵ�����Ļ��ࡣ������ϵͳ֧�ֵļ�������ľ�����ʵ����</param>
        public Guid AddQuestion(Question question)
        {
            question.QuestionID = (question.QuestionID == Guid.Empty) ? Guid.NewGuid() : question.QuestionID;
            question.CreatedUserID = ETMS.AppContext.UserContext.Current.UserID;
            this.QuestionDao.AddQuestion(question);
            return question.QuestionID;
        }

        ///<summary>
        /// ���һ������
        ///</summary>
        /// <param name="questionID">Ҫ���µ�����ID</param>
        /// <param name="newQuestion">���µ�����ʵ�壨����Ϊ�������͵�ʵ�壩</param>
        public void Update(Guid questionID, Question newQuestion)
        {
            newQuestion.QuestionID = questionID;
            newQuestion.AuditStatus = AuditType.Waiting;
            newQuestion.UpdatedUserID = ETMS.AppContext.UserContext.Current.UserID;
            this.QuestionDao.Update(newQuestion);
        }

        ///<summary>
        /// ɾ��ָ��������
        ///</summary>
        /// <param name="questionID">Ҫɾ���������ID</param>
        public void Delete(Guid questionID)
        {
            this.QuestionDao.Delete(questionID, UserContext.Current.UserID);
        }

        ///<summary>
        /// ����ɾ��ָ��������
        ///</summary>
        /// <param name="questionIDs">Ҫɾ���������IDs</param>
        public void DeleteBatch(IList<Guid> questionIDs)
        {
            this.QuestionDao.DeleteBatch(questionIDs, UserContext.Current.UserID);
        }

        ///<summary>
        /// �õ�ĳһ���⡣�������������͵õ������������͵�ʵ����
        ///</summary>
        /// <param name="questionID">Ҫ��ȡ�������ID</param>
        public Question GetByID(Guid questionID)
        {
            return this.QuestionDao.GetByID(questionID);
        }
        /// <summary>
        /// �޸Ĵ��ַ���
        /// </summary>
        /// <param name="questionID">����ID</param>
        /// <param name="answer">���ַ���</param>
        public void UpdateAnswers(Guid questionID, string answer)
        {
            if (String.IsNullOrEmpty(answer) || questionID.Equals(Guid.Empty))
                throw new ETMS.AppContext.BusinessException("ItemBank.Question.ParametersInvalid ����Ĳ����������Ϸ�����");
            else
                this.QuestionDao.UpdateAnswers(questionID, answer, UserContext.Current.UserID);
        }
        /// <summary>
        /// ��ȡ���ַ���
        /// </summary>
        /// <param name="questionID">����ID</param>
        /// <returns>���ַ���</returns>
        public string GetAnswersByID(Guid questionID)
        {
            if (questionID.Equals(Guid.Empty))
                throw new ETMS.AppContext.BusinessException("ItemBank.Question.ParametersInvalid");
            else
                return this.QuestionDao.GetAnswersByID(questionID);
        }

        /// <summary>
        /// ɾ��ָ�������µ���������
        /// </summary>
        /// <param name="classID">����ID</param>
        public void DeleteClassID(Guid classID)
        {
            if (classID.Equals(Guid.Empty))
                throw new ETMS.AppContext.BusinessException("ItemBank.Question.ParametersInvalid");
            else
                this.QuestionDao.DeleteClassID(classID);
        }
        /// <summary>
        /// �޸�ָ����������⵽�·���
        /// </summary>
        /// <param name="oldClassID">�Ϸ���</param>
        /// <param name="newClassID">�·���</param>
        public void UpdateClassID(Guid oldClassID, Guid newClassID)
        {
            if (oldClassID.Equals(Guid.Empty) || newClassID.Equals(Guid.Empty))
                throw new ETMS.AppContext.BusinessException("ItemBank.Question.ParametersInvalid ����Ĳ����������Ϸ�����");
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

        #region IInitializingObject ��Ա

        public void AfterPropertiesSet()
        {
            if (this.QuestionDao == null)
                throw new Exception("please set QuestionDao Property First!");
        }

        #endregion

        #region IMessageSourceAware ��Ա

        public IMessageSource MessageSource { get; set; }

        #endregion
    }
}