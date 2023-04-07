// File:    GroupQuestionLogic.cs
// Author:  lishaobo
// Created: 2011年12月17日 10:07:16
// Purpose: Definition of Class GroupQuestionLogic

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
    /// 归类题操作
    /// </summary>
    public class GroupQuestionLogic : IQuestionServiceLogic, IMessageSourceAware, IInitializingObject
    {
        public ICommonQuestionLogic QuestionBaseLogic { get; set; }
        public IOptionGroupServiceLogic OptionGroupService { get; set; }

        #region IQuestionLogic 接口

        private void ValidQuestionObject(QuestionBase questionBase)
        {
            GroupQuestion question = (GroupQuestion)questionBase;
            Question q = question.CommonQuestion.Question;
            if (question.OptionGroups.Count != 2)
                throw new ETMS.AppContext.BusinessException("ItemBank.Question.OptionGroupsInvalid");
            foreach (var group in question.OptionGroups)
            {
                if (group.Options.Count == 0)
                    throw new ETMS.AppContext.BusinessException("ItemBank.Question.WithoutGroups");
            }
        }

        [Transaction]
        public Guid AddQuestion(QuestionBase questionBase)
        {
            GroupQuestion question = (GroupQuestion)questionBase;
            // 1、验证匹配题实体是否符合规范
            this.ValidQuestionObject(question);
            // 2、添加试题公用部分
            question.CommonQuestion.QuestionType = QuestionType.Group;
            CommonQuestion common = this.QuestionBaseLogic.AddQuestion(question.CommonQuestion);
            // 3、添加选项组和选项
            Guid questionID = common.Question.QuestionID;
            List<OptionGroupItem> LstGroups = new List<OptionGroupItem>();
            //GroupAnswer answer = new GroupAnswer();
            //answer.QuestionID = questionID;
            foreach (var group in question.OptionGroups)
            {
                group.OptionGroup.QuestionID = questionID;
                OptionGroupItem oGroupItem=this.OptionGroupService.AddOptionGroup(group.OptionGroup, group.Options);
                LstGroups.Add(oGroupItem);
            }
            GroupAnswer answer = new GroupAnswer(LstGroups);
            // 4、保存试题答案字段
            this.QuestionBaseLogic.UpdateAnswers(questionID, answer.ToString());
            return questionID;
        }

        [Transaction]
        public void Update(Guid questionID, QuestionBase questionBase)
        {
            GroupQuestion newQuestion = (GroupQuestion)questionBase;
            // 1、验证匹配题实体是否符合规范
            this.ValidQuestionObject(newQuestion);
            // 2、修改试题公用部分
            CommonQuestion common = newQuestion.CommonQuestion;
            this.QuestionBaseLogic.Update(questionID, common);
            // 3、添加选项组和选项
            IList<OptionGroupItem> groupList = this.OptionGroupService.LoadAllInQuestion(questionID);
            List<OptionGroupItem> LstGroups = new List<OptionGroupItem>();

            //GroupAnswer answer = new GroupAnswer();
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
            GroupAnswer answer = new GroupAnswer(LstGroups);
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

        [Transaction]
        public QuestionBase GetByID(Guid questionID)
        {
            GroupQuestion group = new GroupQuestion();
            group.CommonQuestion = this.QuestionBaseLogic.GetByID(questionID);
            group.OptionGroups = this.OptionGroupService.LoadAllInQuestion(questionID);
            string groupAnswer = group.CommonQuestion.Question.Answers;
            if (!string.IsNullOrEmpty(groupAnswer))
                group.Answer = new GroupAnswer(groupAnswer);
            return group;
        }
        #endregion

        #region IInitializingObject 成员

        public void AfterPropertiesSet()
        {
            if (this.OptionGroupService == null)
                throw new Exception("please set OptionGroupService Property First!");
            if (this.QuestionBaseLogic == null)
                throw new Exception("please set QuestionBaseLogic Property First!");
        }

        #endregion

        #region IMessageSourceAware 成员

        public IMessageSource MessageSource { get; set; }

        #endregion
    }
}