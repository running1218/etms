// File:    IUserTestLogic.cs
// Author:  Administrator
// Created: 2011年12月17日 10:43:25
// Purpose: Definition of Interface IUserTestLogic

using System;
using System.Collections.Generic;
using ETMS.Components.Exam.API.Entity.ItemBank;
using ETMS.Components.Exam.API.Entity.Test;
using System.ServiceModel;

///<summary>
/// 测评
///</summary>
namespace ETMS.Components.Exam.API.Interface.Test
{
    ///<summary>
    /// 考生测评相关的对外逻辑接口
    ///</summary>
    [ServiceContract(Namespace = "http://Autumn.Business.LMS.Interface.Test/IUserTestLogic")]
    public interface IUserTestLogic
    {
        ///<summary>
        /// 为考生生成试卷,并开始考试
        ///</summary>
        ///<remarks>
        ///同一试卷在未结束的情况下，是否允许开始同一试卷新的考试<br></br>
        ///是否超过试卷所允许的最大考试次数；<br></br>
        ///</remarks>
        /// <param name="userID">要考试的考生ID</param>
        /// <param name="testPaperID">要进行的考试ID</param>
        /// <param name="IsPreview">是否为试卷预览而生成</param>
        /// <returns>开始的考试ID</returns>
        [OperationContract]
        Guid StartNewTest(int userID, System.Guid testPaperID, bool IsPreview);

        ///<summary>
        /// 得到指定考生的测试信息
        ///</summary>
        /// <param name="studentID">考生ID</param>
        /// <param name="testPaperID">试卷ID</param>
        /// <param name="testState">考试状态</param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="totalCount"></param>
        [OperationContract]
        IList<StudentTestView> FindStudentTests(int studentID, Guid testPaperID, UserTestStatusType testState,
           int pageSize, int pageIndex, out int totalCount);
        /// <summary>
        /// 获取用户最后一次测试的信息
        /// </summary>
        /// <param name="studentID"></param>
        /// <param name="testPaperID"></param>
        /// <param name="testState"></param>
        /// <param name="IsPreview"></param>
        /// <returns></returns>
        [OperationContract]
        StudentTestView GetUserLastTest(int studentID, Guid testPaperID, UserTestStatusType testState, bool IsPreview);
        ///<summary>
        /// 测试者继续某一考试。如果无法继续将产生异常。
        ///</summary>
        /// <param name="UserExamID">考生测试ID</param>
        [OperationContract]
        void StartTest(Guid UserExamID);

        ///<summary>
        /// 获取某一用户，指定测试ID的试卷中所有试题的题面信息（包括：试题题面、选项、分数、考试剩余时间，试题在试卷中的序号等信息）
        ///</summary>
        /// <param name="UserExamID">考生考试ID</param>
        [OperationContract]
        IList<UserQuestion> GetAllQuestions(System.Guid UserExamID);

        ///<summary>
        /// 获取某一用户，指定测试ID的试卷结构信息（试卷的基本信息，试卷中模块信息，模块结构信息等）
        ///</summary>
        ///<param name="UserExamID">考生考试ID</param>
        [OperationContract]
        ExamPaperSchema GetTestPaperSchema(Guid UserExamID);

        ///<summary>
        /// 更新试题答案
        ///</summary>
        ///<remarks>
        /// 1,提供的考生答案必须是与试题的类型相一致；<br></br>
        /// 2,不同试题类型的答案实例的类型为：
        /// SingleChoiceAnswer,MultipleChoiceAnswer,JudgementAnswer,
        /// TextEntryAnswer,ExtendedTextAnswer,
        /// MatchAnswer,GroupAnswer<br></br>
        ///</remarks>
        /// <param name="UserExamID">考生考试ID</param>
        /// <param name="questionID">试题ID</param>
        /// <param name="userAnswer">考生答案</param>
        /// <param name="nCurQuestionNo">考生当前答题的序号</param>
        /// <param name="totalTestTime">考试截止答该题时总用时（单位：秒）</param>
        /// <param name="isFeedback">是否需要评分，并返回答案反馈</param>
        /// <param name="sQuestionFeedback">试题反馈信息</param>
        /// <param name="lstOptionFeedbacks">试题的选项反馈</param>
        /// <returns>考生该题得分。只有在评分时，才会得到正确的分数</returns>
        [OperationContract]
        decimal UpdateQuestionAnswer(System.Guid UserExamID, System.Guid questionID,
           AnswerBase userAnswer, int nCurQuestionNo,
           int totalTestTime, bool isFeedback,
           out string sQuestionFeedback,
           out IList<OptionFeedback> lstOptionFeedbacks);

        ///<summary>
        /// 得到试卷中某一试题的答题反馈，包括选项反馈
        ///</summary>
        /// <param name="UserExamID">考生考试ID</param>
        /// <param name="questionID">试题ID</param>
        /// <param name="UserScore">返回考生试题得分</param>
        /// <param name="lstOptionFeedbacks">试题选项反馈</param>
        /// <returns>得到试题的答题反馈</returns>
        [OperationContract]
        string GetQuestionFeedback(System.Guid UserExamID,
           System.Guid questionID, out decimal UserScore,
           out IList<OptionFeedback> lstOptionFeedbacks);

        ///<summary>
        /// 得到考生某一测试的结果
        ///</summary>
        ///<param name="UserExamID">考生测试ID</param>
        /// <param name="questionID">试题ID</param>
        /// <param name="isFeedback">是否需要包含试题反馈</param>
        /// <param name="isPaperFeedback">是否需要试卷反馈</param>
        [OperationContract]
        ExamResult GetExamResult(Guid UserExamID, Guid questionID, bool isFeedback, bool isPaperFeedback);

        ///<summary>
        /// 得到考生考试的试卷反馈信息
        ///</summary>
        ///<param name="UserExamID">考生测试ID</param>
        ///<returns>试卷反馈</returns>
        [OperationContract]
        IList<TestFeedback> GetPaperFeedback(Guid UserExamID);

        ///<summary>
        /// 提交试卷
        ///</summary>
        /// <param name="UserExamID">考生考试ID</param>
        /// <param name="isReturnResult">是否返回结果</param>
        /// <param name="testResult">考生得分</param>
        [OperationContract]
        void SubmitPaper(System.Guid UserExamID, bool isReturnResult, out decimal testResult);

        ///<summary>
        /// 得到试卷中指定试题的解题思路
        ///</summary>
        /// <param name="UserExamID">考生考试ID</param>
        /// <param name="questionID">试题ID</param>
        /// <returns>试题解题思路</returns>
        [OperationContract]
        string GetQuestionSolution(System.Guid UserExamID, System.Guid questionID);

        ///<summary>
        /// 得到考生考试试题信息，包括试题的题面，选项，小题，考生答案等。
        ///</summary>
        ///<remarks>
        /// 1,得到的试题是按试题的类型化后的试题实例。<br></br>
        /// 2,不同试题类型的分别为：TestSingleChoiceQuestion,TestMultipleChoiceQuestion,
        /// TestJudgementQuestion,TestTextEntryQuestion,TestEntendTextQuestion,
        /// TestMatchQuestion,TestGroupQuestion
        /// </remarks>
        /// <param name="UserExamID">考生考试ID</param>
        /// <param name="questionID">试题ID</param>
        /// <returns>试题对象</returns>
        [OperationContract]
        UserQuestion GetQuestion(System.Guid UserExamID, System.Guid questionID);

        /// <summary>
        /// 得到某一次考试结束后，显示的统计结果。
        /// </summary>
        /// <param name="UserExamID"></param>
        /// <returns></returns>
        [OperationContract]
        UserTestResultStat GetUserTestResultStat(Guid UserExamID);

        /// <summary>
        /// 得到某一个考生，指定试卷的考试统计信息。最高分，最低分，已考试次数
        /// </summary>
        /// <remarks>
        /// 返回的结果中包括：最高分，最低分，已考试次数
        /// </remarks>
        /// <param name="UserID"></param>
        /// <param name="TestPaperID"></param>
        /// <returns></returns>
        [OperationContract]
        UserExamStat GetUserExamStat(int UserID, Guid TestPaperID);
        /// <summary>
        /// 得到考生，指定考试的状态信息。试题总数，已答题数，试卷总时间，已用时，剩余时间等信息
        /// </summary>
        /// <remarks>
        /// 返回结果中包括：<br></br>
        /// 试题总数，已答题数，试卷总时间，已用时，剩余时间等信息
        /// </remarks>
        /// <param name="UserExamID"></param>
        /// <returns></returns>
        [OperationContract]
        UserExamState GetUserExamState(Guid UserExamID);
    }
}