// File:    MatchQuestionLogic.cs
// Author:  lishaobo
// Created: 2011��12��17�� 10:07:12
// Purpose: Definition of Class MatchQuestionLogic

using System;
using System.Collections.Generic;
using Autumn.Context;
using Autumn.Objects.Factory;
using ETMS.Components.Exam.API.Entity.ItemBank;
using System.Linq;
using Autumn.Transaction.Interceptor;
using ETMS.Components.Exam.API.Interface.ItemBank;
namespace ETMS.Components.Exam.Implement.BLL.ItemBank
{
    /// <summary>
    /// ƥ�������
    /// </summary>
    public class MatchQuestionLogic : IQuestionServiceLogic, IMessageSourceAware, IInitializingObject
    {
        public ICommonQuestionLogic QuestionBaseLogic { get; set; }
        public IOptionGroupServiceLogic OptionGroupService { get; set; }

        #region IQuestionLogic ��Ա

        private void ValidQuestionObject(QuestionBase questionBase)
        {
            MatchQuestion question = (MatchQuestion)questionBase;
            Question q = question.CommonQuestion.Question;
            if (question.OptionGroups.Count < 2)
                throw new ETMS.AppContext.BusinessException("ItemBank.Question.LessOptionGroups");
            foreach (var group in question.OptionGroups)
            {
                if (group.Options.Count == 0)
                    throw new ETMS.AppContext.BusinessException("ItemBank.Question.WithoutGroups");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="question"></param>
        /// <returns></returns>
        [Transaction]
        public Guid AddQuestion(QuestionBase questionBase)
        {
            MatchQuestion question = (MatchQuestion)questionBase;
            // 1����֤ƥ����ʵ���Ƿ���Ϲ淶
            this.ValidQuestionObject(question);
            // 2��������⹫�ò���
            question.CommonQuestion.QuestionType = QuestionType.Match;
            CommonQuestion common = this.QuestionBaseLogic.AddQuestion(question.CommonQuestion);
            // 3�����ѡ�����ѡ��
            Guid questionID = common.Question.QuestionID;
            List<OptionGroupItem> LstGroups = new List<OptionGroupItem>();
            //GroupAnswer answer = new GroupAnswer();
            //answer.QuestionID = questionID;
            foreach (var group in question.OptionGroups)
            {
                group.OptionGroup.QuestionID = questionID;
                LstGroups.Add(this.OptionGroupService.AddOptionGroup(group.OptionGroup, group.Options));
            }
            GroupAnswer answer = new GroupAnswer(LstGroups);
            // 4������������ֶ�
            this.QuestionBaseLogic.UpdateAnswers(questionID, answer.ToString());
            return questionID;
        }
        [Transaction]
        public void Update(Guid questionID, QuestionBase questionBase)
        {
            MatchQuestion newQuestion = (MatchQuestion)questionBase;
            // 1����֤ƥ����ʵ���Ƿ���Ϲ淶
            this.ValidQuestionObject(newQuestion);
            // 2���޸����⹫�ò���
            CommonQuestion common = newQuestion.CommonQuestion;
            this.QuestionBaseLogic.Update(questionID, common);
            // 3�����ѡ�����ѡ��
            IList<OptionGroupItem> groupList = this.OptionGroupService.LoadAllInQuestion(questionID);
            List<OptionGroupItem> LstGroups = new List<OptionGroupItem>();

            //MatchAnswer answer = new MatchAnswer();
            //answer.QuestionID = questionID;
            foreach (var group in newQuestion.OptionGroups)
            {
                //(1)����������ѡ����
                group.OptionGroup.QuestionID = questionID;
                if (group.OptionGroup.OptionGroupTitleID == Guid.Empty)
                {
                    LstGroups.Add(this.OptionGroupService.AddOptionGroup(group.OptionGroup, group.Options));
                }
                //(2)�޸����е�ѡ����
                else
                {
                    Guid newOptionGroupID = group.OptionGroup.OptionGroupTitleID;
                    OptionGroupItem oldGroup = groupList.Where<OptionGroupItem>(m => m.OptionGroup.OptionGroupTitleID == newOptionGroupID).First<OptionGroupItem>();
                    if (oldGroup != null)
                    {
                        this.OptionGroupService.Update(group);
                        LstGroups.Add(this.OptionGroupService.Load(questionID, newOptionGroupID));
                        groupList.Remove(oldGroup);
                    }
                }
            }
            // 4��ɾ�������ڵ�ѡ����
            foreach (var item in groupList)
            {
                this.OptionGroupService.Delete(questionID, item.OptionGroup.OptionGroupTitleID);
            }
            MatchAnswer answer = new MatchAnswer(LstGroups);
            // 5������������ֶ�
            this.QuestionBaseLogic.UpdateAnswers(questionID, answer.ToString());
        }
        [Transaction]
        public void Delete(Guid questionID)
        {
            // 1��ɾ�����⹫�ò���
            this.QuestionBaseLogic.Delete(questionID);
            // 2��ɾ��ѡ�����ѡ��
            IList<OptionGroupItem> groupList = this.OptionGroupService.LoadAllInQuestion(questionID);
            foreach (var item in groupList)
            {
                this.OptionGroupService.Delete(questionID, item.OptionGroup.OptionGroupTitleID);
            }
        }

        public void DeleteBatch(IList<Guid> questionIDs)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// ��ȡƥ������Ϣ
        /// ����ϢΪnull
        /// </summary>
        /// <param name="questionID"></param>
        /// <returns></returns>
        [Transaction]
        public QuestionBase GetByID(Guid questionID)
        {
            MatchQuestion match = new MatchQuestion();
            match.CommonQuestion = this.QuestionBaseLogic.GetByID(questionID);
            match.OptionGroups = this.OptionGroupService.LoadAllInQuestion(questionID);
            string matchAnswer = match.CommonQuestion.Question.Answers;
            if (!string.IsNullOrEmpty(matchAnswer))
                match.Answer = new MatchAnswer(matchAnswer);
            //match.Answer = null;
            return match;
        }
        #endregion

        #region IInitializingObject ��Ա
        /// <summary>
        /// 
        /// </summary>
        public void AfterPropertiesSet()
        {
            if (this.QuestionBaseLogic == null)
                throw new Exception("please set QuestionBaseLogic Property First!");
            if (this.OptionGroupService == null)
                throw new Exception("please set OptionGroupService Property First!");
        }
        #endregion

        #region IMessageSourceAware ��Ա
        /// <summary>
        /// 
        /// </summary>
        public IMessageSource MessageSource { get; set; }
        #endregion
    }
}