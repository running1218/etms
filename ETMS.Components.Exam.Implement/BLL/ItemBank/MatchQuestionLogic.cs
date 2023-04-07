// File:    MatchQuestionLogic.cs
// Author:  lishaobo
// Created: 2011年12月17日 10:07:12
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
    /// 匹配题操作
    /// </summary>
    public class MatchQuestionLogic : IQuestionServiceLogic, IMessageSourceAware, IInitializingObject
    {
        public ICommonQuestionLogic QuestionBaseLogic { get; set; }
        public IOptionGroupServiceLogic OptionGroupService { get; set; }

        #region IQuestionLogic 成员

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
            // 1、验证匹配题实体是否符合规范
            this.ValidQuestionObject(question);
            // 2、添加试题公用部分
            question.CommonQuestion.QuestionType = QuestionType.Match;
            CommonQuestion common = this.QuestionBaseLogic.AddQuestion(question.CommonQuestion);
            // 3、添加选项组和选项
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
            // 4、保存试题答案字段
            this.QuestionBaseLogic.UpdateAnswers(questionID, answer.ToString());
            return questionID;
        }
        [Transaction]
        public void Update(Guid questionID, QuestionBase questionBase)
        {
            MatchQuestion newQuestion = (MatchQuestion)questionBase;
            // 1、验证匹配题实体是否符合规范
            this.ValidQuestionObject(newQuestion);
            // 2、修改试题公用部分
            CommonQuestion common = newQuestion.CommonQuestion;
            this.QuestionBaseLogic.Update(questionID, common);
            // 3、添加选项组和选项
            IList<OptionGroupItem> groupList = this.OptionGroupService.LoadAllInQuestion(questionID);
            List<OptionGroupItem> LstGroups = new List<OptionGroupItem>();

            //MatchAnswer answer = new MatchAnswer();
            //answer.QuestionID = questionID;
            foreach (var group in newQuestion.OptionGroups)
            {
                //(1)保存新增的选项组
                group.OptionGroup.QuestionID = questionID;
                if (group.OptionGroup.OptionGroupTitleID == Guid.Empty)
                {
                    LstGroups.Add(this.OptionGroupService.AddOptionGroup(group.OptionGroup, group.Options));
                }
                //(2)修改已有的选项组
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
            // 4、删除不存在的选项组
            foreach (var item in groupList)
            {
                this.OptionGroupService.Delete(questionID, item.OptionGroup.OptionGroupTitleID);
            }
            MatchAnswer answer = new MatchAnswer(LstGroups);
            // 5、保存试题答案字段
            this.QuestionBaseLogic.UpdateAnswers(questionID, answer.ToString());
        }
        [Transaction]
        public void Delete(Guid questionID)
        {
            // 1、删除试题公用部分
            this.QuestionBaseLogic.Delete(questionID);
            // 2、删除选项组和选项
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
        /// 获取匹配题信息
        /// 答案信息为null
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

        #region IInitializingObject 成员
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

        #region IMessageSourceAware 成员
        /// <summary>
        /// 
        /// </summary>
        public IMessageSource MessageSource { get; set; }
        #endregion
    }
}