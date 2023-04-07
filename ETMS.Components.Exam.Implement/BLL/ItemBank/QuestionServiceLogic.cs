using System;
using System.Collections.Generic;
using System.Linq;
using Autumn.Objects.Factory;
using Autumn.Context;
using ETMS.Components.Exam.API.Entity.ItemBank;
using Autumn.Transaction.Interceptor;
using ETMS.Components.Exam.API.Interface.ItemBank;
namespace ETMS.Components.Exam.Implement.BLL.ItemBank
{
    /// <summary>
    /// 试题服务接口，为所有类型的试题提供了一个统一的处理接口
    /// </summary>
    public class QuestionServiceLogic : IMessageSourceAware, IInitializingObject, IQuestionServiceLogic
    {
        private static string Err_QuestionService_Invalid_Data = "ItemBank.QuestionService.Invalid.Data";

        #region --各种题型逻辑接口--
        public IQuestionServiceLogic SingleChoiceQuestionLogic { get; set; }
        public IQuestionServiceLogic MultipleChoiceQuestionLogic { get; set; }
        public IQuestionServiceLogic JudgementQuestionLogic { get; set; }
        public IQuestionServiceLogic MatchQuestionLogic { get; set; }
        public IQuestionServiceLogic GroupQuestionLogic { get; set; }
        public IQuestionServiceLogic TextEntryQuestionLogic { get; set; }
        public IQuestionServiceLogic ExtendTextQuestionLogic { get; set; } 
        /// <summary>
        /// 试题题干部分的逻辑接口
        /// </summary>
        public IQuestionLogic QuestionTitleLogic { get; set; }
        #endregion

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
            
        }

        #endregion

        #region IQuestionService 成员
        [Transaction()]
        public Guid AddQuestion(QuestionBase question)
        {
            ValidQuestion(question, System.Guid.Empty);

            IQuestionServiceLogic MyQuestionService = GetQuestionService(question.CommonQuestion.Question.QuestionType);
            // 新增试题
            Guid questionID = MyQuestionService.AddQuestion(question);
            
            return questionID;
        }

        public void Update(Guid questionID, QuestionBase newQuestion)
        {
            ValidQuestion(newQuestion, questionID);
            IQuestionServiceLogic MyQuestionService = GetQuestionService(newQuestion.CommonQuestion.QuestionType);
            
            MyQuestionService.Update(questionID, newQuestion);
        }

        public void Delete(Guid questionID)
        {
            Question question = this.QuestionTitleLogic.GetByID(questionID);
            if (question == null)
                return ;
            //得到类型
            IQuestionServiceLogic MyQuestionService = this.GetQuestionService(question.QuestionType);
            MyQuestionService.Delete(questionID);
        }
        
        [Transaction]
        public void DeleteBatch(IList<Guid> questionIDs)
        {
            if (questionIDs == null || questionIDs.Count <= 0)
                return;

            //迭代删除每一个试题
            Array.ForEach<Guid>(questionIDs.ToArray<Guid>(),
                x => 
                {
                    this.Delete(x);
                });
        }

        public QuestionBase GetByID(Guid questionID)
        {
            Question question=this.QuestionTitleLogic.GetByID(questionID);
            if (question == null)
                return null;
            //得到类型
            IQuestionServiceLogic MyQuestionService = this.GetQuestionService(question.QuestionType);
            //得到试题
            return MyQuestionService.GetByID(questionID);
        }

        #endregion
        /// <summary>
        /// 对要添加或修改的修改做基本的验证
        /// </summary>
        /// <param name="question"></param>
        private void ValidQuestion(QuestionBase question,System.Guid questionID)
        {
            if (question == null || question.CommonQuestion == null || question.CommonQuestion.Question == null)
            {
                throw new ETMS.AppContext.BusinessException(Err_QuestionService_Invalid_Data,new Exception("试题数据不完整或为NULL"));
            }

            QuestionType MyQuestionType = question.CommonQuestion.QuestionType;

            #region --//如果非单选与多选，提示不允许有选项反馈--

            if (!(MyQuestionType == QuestionType.SingleChoice
                || MyQuestionType == QuestionType.MultipleChoice))
            {
                if (question.CommonQuestion.LstOptionFeedbacks != null && question.CommonQuestion.LstOptionFeedbacks.Count > 0)
                {
                    throw new ETMS.AppContext.BusinessException(Err_QuestionService_Invalid_Data,
                        new Exception("试题数据不一致，只允许单选题与多选题存在选项反馈"));
                }
            }
            #endregion

            if (questionID != Guid.Empty)
            { 
                //检查试题的ID是否一致
                if (question.CommonQuestion.Question.QuestionID != questionID)
                {
                    throw new ETMS.AppContext.BusinessException(Err_QuestionService_Invalid_Data, new Exception("试题数据不一致，试题对象不属于指定的试题ID"));
                }
                //对其它包含有试题ID的进行检查
                if (question.CommonQuestion.QuestionExtend != null)
                {
                    if (question.CommonQuestion.QuestionExtend.QuestionID != Guid.Empty &&
                        question.CommonQuestion.QuestionExtend.QuestionID != questionID)
                    {
                        throw new ETMS.AppContext.BusinessException(Err_QuestionService_Invalid_Data, new Exception("试题数据不一致，解题思路对象不属于指定的试题ID"));
                    }
                }
                //检查答案反馈
                if (question.CommonQuestion.QuestionFeedback != null)
                {
                    if (question.CommonQuestion.QuestionFeedback.QuestionID != Guid.Empty &&
                        question.CommonQuestion.QuestionFeedback.QuestionID != questionID)
                    {
                        throw new ETMS.AppContext.BusinessException(Err_QuestionService_Invalid_Data, new Exception("试题数据不一致，试题反馈对象不属于指定的试题ID"));
                    }
                }
                //检查选项反馈
                if (question.CommonQuestion.LstOptionFeedbacks != null && question.CommonQuestion.LstOptionFeedbacks.Count > 0)
                {
                    var LstTmp = (from item in question.CommonQuestion.LstOptionFeedbacks
                                  where item.QuestionID != Guid.Empty && item.QuestionID != questionID
                                  select item).ToList<OptionFeedback>();
                    if (LstTmp.Count > 0)
                    {
                        throw new ETMS.AppContext.BusinessException(Err_QuestionService_Invalid_Data, 
                            new Exception("试题数据不一致，选项反馈对象不属于指定的试题ID"));
                    }
                }
            }
        }
        /// <summary>
        /// 根据试题类型来创建不同的试题题型逻辑接口
        /// </summary>
        /// <param name="questionType"></param>
        /// <returns></returns>
        private IQuestionServiceLogic GetQuestionService(QuestionType questionType)
        {
            switch (questionType)
            { 
                case QuestionType.SingleChoice :
                    return this.SingleChoiceQuestionLogic;
                case QuestionType.MultipleChoice :
                    return this.MultipleChoiceQuestionLogic;
                case QuestionType.Judgement :
                    return this.JudgementQuestionLogic;
                case QuestionType.Match:
                    return this.MatchQuestionLogic;
                case QuestionType.Group :
                    return this.GroupQuestionLogic;
                case QuestionType.TextEntry:
                    return this.TextEntryQuestionLogic;
                case QuestionType.ExtendedText:
                    return this.ExtendTextQuestionLogic;
                default :
                    throw new NotImplementedException("暂不支持的题型");
            }
        }
    }
}
