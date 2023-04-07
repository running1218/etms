// File:    UserTestLogic.cs
// Author:  Administrator
// Created: 2011年12月17日 10:55:31
// Purpose: Definition of Class UserTestLogic

using System;
using System.Collections.Generic;
using ETMS.Components.Exam.API.Entity.ItemBank;
using ETMS.Components.Exam.API.Entity.Test;
using Autumn.Objects.Factory;
using Autumn.Context;

using ETMS.Components.Exam.API.Interface.Test;
namespace ETMS.Components.Exam.Implement.BLL.Test
{
    ///<summary>
    /// 考生测评逻辑实现
    ///</summary>
    public class UserTestLogic : IUserTestLogic, IMessageSourceAware, IInitializingObject
    {
        public TestPaperRuleLogic PaperRuleLogic { get; set; }

        #region IInitializingObject 成员

        public void AfterPropertiesSet()
        {

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
        /// 针对设计缺陷进行补救
        /// </summary>
        /// <returns></returns>
        private UserExamPaperLogic GetExamPaperInstance()
        {
            return (UserExamPaperLogic)Autumn.Context.Support.ContextRegistry.GetContext(
                ETMS.Components.Exam.API.ServiceRepository.AppContextName
                ).GetObject("UserExamPaperLogic");
        }
        #region IUserTestLogic 成员

        Guid IUserTestLogic.StartNewTest(int userID, Guid testPaperID, bool IsPreview)
        {
            UserExamPaperLogic examPaper = GetExamPaperInstance();
            if (!IsPreview)
            {
                //判断，考生考试次数是否允许继续考试
                examPaper.ValidateUserExamsTimes(userID, testPaperID);
            }

            //开始考试
            Guid UserExamID = Guid.Empty;
            //生成试卷
            UserExamID = this.PaperRuleLogic.CreateStudentTestPaper1(testPaperID, userID, IsPreview);

            //if (UserExamID != Guid.Empty && UserExamID != null)
            //{
            //    //将试卷写入到ExamResult表中，
            //    examPaper.CreateExamResultForUserExam(UserExamID);
            //}

            //开始考试
            this.InitExamPaperLogic(UserExamID, examPaper);
            examPaper.StartTest();

            return UserExamID;
        }

        IList<StudentTestView> IUserTestLogic.FindStudentTests(int studentID, Guid testPaperID, UserTestStatusType testState, int pageSize, int pageIndex, out int totalCount)
        {
            UserExamPaperLogic examPaper = GetExamPaperInstance();
            return examPaper.FindStudentTests(studentID, testPaperID, testState,
                pageSize, pageIndex, out totalCount);
        }
        /// <summary>
        /// 获取用户最后一次测试的信息
        /// </summary>
        /// <param name="studentID"></param>
        /// <param name="testPaperID"></param>
        /// <param name="testState"></param>
        /// <param name="IsPreview"></param>
        /// <returns></returns>
        public StudentTestView GetUserLastTest(int studentID, Guid testPaperID, UserTestStatusType testState, bool IsPreview)
        {
            UserExamPaperLogic examPaper = GetExamPaperInstance();
            return examPaper.GetUserLastTest(studentID, testPaperID, testState, IsPreview);
        }
        void IUserTestLogic.StartTest(Guid UserExamID)
        {
            UserExamPaperLogic examPaper = GetExamPaperInstance();
            this.InitExamPaperLogic(UserExamID, examPaper);
            examPaper.StartTest();
        }

        IList<UserQuestion> IUserTestLogic.GetAllQuestions(Guid UserExamID)
        {
            UserExamPaperLogic examPaper = GetExamPaperInstance();
            this.InitExamPaperLogic(UserExamID, examPaper);
            return examPaper.Questions;
        }

        ExamPaperSchema IUserTestLogic.GetTestPaperSchema(Guid UserExamID)
        {
            UserExamPaperLogic examPaper = GetExamPaperInstance();
            this.InitExamPaperLogic(UserExamID, examPaper);
            return examPaper.PaperSchema;
        }

        decimal IUserTestLogic.UpdateQuestionAnswer(Guid UserExamID, Guid questionID, AnswerBase userAnswer,
            int nCurQuestionNo, int totalTestTime, bool isFeedback,
            out string sQuestionFeedback, out IList<OptionFeedback> lstOptionFeedbacks)
        {
            UserExamPaperLogic examPaper = GetExamPaperInstance();
            this.InitExamPaperLogic(UserExamID, examPaper);
            decimal UserScore = 0;
            UserScore = examPaper.UpdateQuestionAnswer(questionID, userAnswer,
                nCurQuestionNo, totalTestTime, isFeedback,
                out sQuestionFeedback, out lstOptionFeedbacks);

            return UserScore;
        }

        string IUserTestLogic.GetQuestionFeedback(Guid UserExamID, Guid questionID, out decimal UserScore,
            out IList<OptionFeedback> lstOptionFeedbacks)
        {
            UserExamPaperLogic examPaper = GetExamPaperInstance();
            this.InitExamPaperLogic(UserExamID, examPaper);
            return examPaper.GetQuestionFeedback(
                questionID, out UserScore, out lstOptionFeedbacks);
        }

        ExamResult IUserTestLogic.GetExamResult(Guid UserExamID,
            Guid questionID, bool isFeedback, bool isPaperFeedback)
        {
            UserExamPaperLogic examPaper = GetExamPaperInstance();
            this.InitExamPaperLogic(UserExamID, examPaper);
            return examPaper.GetExamResult(questionID, isFeedback);
        }

        IList<TestFeedback> IUserTestLogic.GetPaperFeedback(Guid UserExamID)
        {
            UserExamPaperLogic examPaper = GetExamPaperInstance();
            this.InitExamPaperLogic(UserExamID, examPaper);
            return examPaper.GetPaperFeedback();
        }

        void IUserTestLogic.SubmitPaper(Guid UserExamID, bool isReturnResult, out decimal userScore)
        {
            UserExamPaperLogic examPaper = GetExamPaperInstance();
            this.InitExamPaperLogic(UserExamID, examPaper);
            examPaper.SubmitPaper(isReturnResult, out userScore);
        }

        string IUserTestLogic.GetQuestionSolution(Guid UserExamID, Guid questionID)
        {
            UserExamPaperLogic examPaper = GetExamPaperInstance();
            this.InitExamPaperLogic(UserExamID, examPaper);
            return examPaper.GetQuestionSolution(questionID);
        }

        UserQuestion IUserTestLogic.GetQuestion(Guid UserExamID, Guid questionID)
        {
            UserExamPaperLogic examPaper = GetExamPaperInstance();
            this.InitExamPaperLogic(UserExamID, examPaper);
            return examPaper.GetQuestion(questionID);
        }

        public UserTestResultStat GetUserTestResultStat(Guid UserExamID)
        {
            UserExamPaperLogic examPaper = GetExamPaperInstance();
            this.InitExamPaperLogic(UserExamID, examPaper);
            return examPaper.GetUserTestResultStat();
        }
        public UserExamStat GetUserExamStat(int UserID, Guid TestPaperID)
        {
            UserExamPaperLogic examPaper = GetExamPaperInstance();
            return examPaper.GetUserExamStat(UserID, TestPaperID);
        }
        public UserExamState GetUserExamState(Guid UserExamID)
        {
            UserExamPaperLogic examPaper = GetExamPaperInstance();
            this.InitExamPaperLogic(UserExamID, examPaper);
            return examPaper.GetUserExamState();
        }
        #endregion

        private void InitExamPaperLogic(Guid UserExamID, UserExamPaperLogic examPaper)
        {
            examPaper.LoadExamPaper(UserExamID);
        }




    }
}