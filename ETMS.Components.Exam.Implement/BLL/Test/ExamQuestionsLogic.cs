using System;
using System.Collections.Generic;
using System.Linq;
using Autumn.Objects.Factory;
using Autumn.Context;

using ETMS.Components.Exam.API.Entity.Test;
using ETMS.Components.Exam.Implement.DAL.Test;
using ETMS.Components.Exam.API.Entity.ItemBank;

namespace ETMS.Components.Exam.Implement.BLL.Test
{
    /// <summary>
    /// 考生答卷试题逻辑实现
    /// </summary>
    public class ExamQuestionsLogic : IMessageSourceAware, IInitializingObject
    {
        #region --错误代码--
        private static string Err_TestFeedback_Not_Found = "ItemBank.OptionGroup.Not.Found";
        private static string Err_TestFeedback_Data_Invalid = "ItemBank.OptionGroup.Data.Invalid";
        private static string Err_UserExamResult_Instance_Invalid = "ItemBank.UserExamResult.Instance.Invalid";
        private static string Err_TestFeedback_New_Invalid = "ItemBank.OptionGroup.New.Invalid";
        #endregion

        public static IUserQuestionDao UserQuestionDao { get; set; }

        #region IInitializingObject 成员

        public void AfterPropertiesSet()
        {
            if (UserQuestionDao == null)
            {
                throw new Exception("please set TestFeedbackDao Property First!");
            }
        }

        #endregion

        #region IMessageSourceAware 成员

        public IMessageSource MessageSource
        {
            get;
            set;
        }

        #endregion

        /// <summary>
        /// 得到某一考试中所有试题
        /// </summary>
        /// <param name="UserExamID"></param>
        /// <returns></returns>
        /// [缓存]
        public IList<UserQuestion> LoadQuestions(Guid UserExamID)
        {
            IList<UserQuestion> LstQuestions = GetQuestions(UserExamID);
            IList<TestOptionGroup> LstOptionGroups = GetOptionsGroup(UserExamID);
            IList<TestQuestionOption> LstOptions = GetQuestionOptions(UserExamID);

            IList<UserQuestion> LstQuestionInstances = new List<UserQuestion>();

            foreach (UserQuestion Question in LstQuestions)
            {
                UserQuestion QuestionItem=CreateQuestionTypeInstance(Question,
                    GetQuestionOptionByQuestionID(LstOptions,Question.QuestionID),
                    GetOptionGroupByQuestionID(LstOptionGroups,Question.QuestionID));

                LstQuestionInstances.Add(QuestionItem);
            }

            return LstQuestionInstances;
        }

        #region --基本数据访问--
        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserExamID"></param>
        /// <returns></returns>
        /// [缓存]
        private  IList<UserQuestion> GetQuestions(Guid UserExamID)
        {
            return UserQuestionDao.FindQuestionsInUserExam(UserExamID);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserExamID"></param>
        /// <returns></returns>
        /// [缓存]
        private IList<TestQuestionOption> GetQuestionOptions(Guid UserExamID)
        {
            return UserQuestionDao.FindOptionsInUserExam(UserExamID);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserExamID"></param>
        /// <returns></returns>
        /// [缓存]
        private IList<TestOptionGroup> GetOptionsGroup(Guid UserExamID)
        {
            return UserQuestionDao.FindOptionGroupsInUserExam(UserExamID);
        }
        private UserQuestion GetQuestionByQuestionID(IList<UserQuestion> LstObjects, Guid QuestionID)
        {
            var LstTmps = from item in LstObjects
                          where item.QuestionID == QuestionID
                          select item;
            if (LstTmps != null && LstTmps.Count<UserQuestion>() > 0)
            {
                return LstTmps.ToList<UserQuestion>()[0];
            }

            return null;
        }
        private IList<TestQuestionOption> GetQuestionOptionByQuestionID(IList<TestQuestionOption> LstObjects, Guid QuestionID)
        {
            var LstTmps = from item in LstObjects
                          where item.QuestionID == QuestionID
                          select item;
            if (LstTmps != null && LstTmps.Count<TestQuestionOption>() > 0)
            {
                return LstTmps.ToList<TestQuestionOption>();
            }

            return null;
        }
        private IList<TestOptionGroup> GetOptionGroupByQuestionID(IList<TestOptionGroup> LstObjects, Guid QuestionID)
        {
            var LstTmps = from item in LstObjects
                          where item.QuestionID == QuestionID
                          select item;
            if (LstTmps != null && LstTmps.Count<TestOptionGroup>() > 0)
            {
                return LstTmps.ToList<TestOptionGroup>();
            }

            return null;
        }
        #endregion

        /// <summary>
        /// 得到试卷中某一试题信息
        /// </summary>
        /// <param name="UserExamID"></param>
        /// <param name="QuestionID"></param>
        /// <returns></returns>
        public UserQuestion LoadQuestion(Guid UserExamID, Guid QuestionID)
        {
            IList<UserQuestion> LstQuestions = LoadQuestions(UserExamID);
            if (LstQuestions == null || LstQuestions.Count <= 0)
                return null;

            var LstTmps = from item in LstQuestions
                          where item.QuestionID == QuestionID
                          select item;
            if (LstTmps == null || LstTmps.Count<UserQuestion>() <= 0)
            {
                return null;
            }

            return LstTmps.ToList<UserQuestion>()[0];
        }
        /// <summary>
        /// 得到试题全部的试题反馈
        /// </summary>
        /// <param name="QuestionID"></param>
        /// <returns></returns>
        /// [缓存]
        public IList<QuestionFeedback> GetQuestionFeedbackByQuestion(Guid UserExamID,Guid QuestionID)
        {
            return UserQuestionDao.GetQuestionFeedbackByQuestion(UserExamID, QuestionID);
        }
        /// <summary>
        /// 得到指定试题的全部选项反馈
        /// </summary>
        /// <param name="QuestionID"></param>
        /// <returns></returns>
        /// [缓存]
        public IList<OptionFeedback> GetOptionFeedbackByQuestion(Guid UserExamID, Guid QuestionID)
        {
            return UserQuestionDao.GetOptionFeedbackByQuestion(UserExamID,QuestionID);
        }
        
        /// <summary>
        /// 得到试题的解题思路
        /// </summary>
        /// <param name="QuestionID"></param>
        /// <returns></returns>
        /// [缓存]
        public QuestionExtend GetQuestionExtendByQuestionID(Guid UserExamID, Guid QuestionID)
        {
            return UserQuestionDao.GetQuestionExtendByQuestionID(UserExamID,QuestionID);
        }

        /// <summary>
        /// 根据试题类型生成具体的试题实例
        /// </summary>
        /// <param name="QuestionTitle"></param>
        /// <param name="LstOptions"></param>
        /// <param name="LstGroups"></param>
        /// <returns></returns>
        public UserQuestion CreateQuestionTypeInstance(UserQuestion QuestionTitle,
            IList<TestQuestionOption> LstOptions,IList<TestOptionGroup> LstGroups
            )
        {
            UserQuestion QuestionInst;
            if (QuestionTitle == null)
                return null;

            switch (QuestionTitle.QuestionType)
            { 
                case QuestionType.SingleChoice :
                    QuestionInst = new TestSingleChoiceQuestion(QuestionTitle, QuestionTitle.AnswerStr, LstOptions);
                    break;
                case QuestionType.MultipleChoice:
                    QuestionInst = new TestMultipleChoiceQuestion(QuestionTitle, QuestionTitle.AnswerStr, LstOptions);
                    break;
                case QuestionType.Judgement :
                    QuestionInst = new TestJudgementQuestion(QuestionTitle, QuestionTitle.AnswerStr, LstOptions);
                    break;
                case QuestionType.TextEntry :
                    QuestionInst = new TestTextEntryQuestion(QuestionTitle, QuestionTitle.AnswerStr);
                    break;
                case QuestionType.ExtendedText:
                    QuestionInst = new TestExtendedTextQuestion(QuestionTitle, QuestionTitle.AnswerStr);
                    break;
                case QuestionType.Match:
                    QuestionInst = new TestMatchQuestion(QuestionTitle, QuestionTitle.AnswerStr, LstOptions,LstGroups);
                    break;
                case QuestionType.Group :
                    QuestionInst = new TestGroupQuestion(QuestionTitle, QuestionTitle.AnswerStr, LstOptions,LstGroups);
                    break;
                default :
                    QuestionInst=null;
                    break;
            }

            return QuestionInst;
        }

        private bool ValidIsInitialized()
        {
            //if (this.UserExamID == null || this.UserExamID == Guid.Empty)
            //{
            //    return false;
            //}

            return true;
        }
        /// <summary>
        /// 检查实例中数据是否完整，不完整直接抛出异常。
        /// </summary>
        private void ThrowNotInitializedExeception()
        {
            bool bIsInit = this.ValidIsInitialized();
            if (!bIsInit)
            {
                throw new ETMS.AppContext.BusinessException(Err_UserExamResult_Instance_Invalid, 
                    new Exception("未正确加载数据，请正确加载试题选项数据加载"));
            }
        }
    }

    
}
