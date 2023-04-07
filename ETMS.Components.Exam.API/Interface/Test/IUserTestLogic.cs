// File:    IUserTestLogic.cs
// Author:  Administrator
// Created: 2011��12��17�� 10:43:25
// Purpose: Definition of Interface IUserTestLogic

using System;
using System.Collections.Generic;
using ETMS.Components.Exam.API.Entity.ItemBank;
using ETMS.Components.Exam.API.Entity.Test;
using System.ServiceModel;

///<summary>
/// ����
///</summary>
namespace ETMS.Components.Exam.API.Interface.Test
{
    ///<summary>
    /// ����������صĶ����߼��ӿ�
    ///</summary>
    [ServiceContract(Namespace = "http://Autumn.Business.LMS.Interface.Test/IUserTestLogic")]
    public interface IUserTestLogic
    {
        ///<summary>
        /// Ϊ���������Ծ�,����ʼ����
        ///</summary>
        ///<remarks>
        ///ͬһ�Ծ���δ����������£��Ƿ�����ʼͬһ�Ծ��µĿ���<br></br>
        ///�Ƿ񳬹��Ծ������������Դ�����<br></br>
        ///</remarks>
        /// <param name="userID">Ҫ���ԵĿ���ID</param>
        /// <param name="testPaperID">Ҫ���еĿ���ID</param>
        /// <param name="IsPreview">�Ƿ�Ϊ�Ծ�Ԥ��������</param>
        /// <returns>��ʼ�Ŀ���ID</returns>
        [OperationContract]
        Guid StartNewTest(int userID, System.Guid testPaperID, bool IsPreview);

        ///<summary>
        /// �õ�ָ�������Ĳ�����Ϣ
        ///</summary>
        /// <param name="studentID">����ID</param>
        /// <param name="testPaperID">�Ծ�ID</param>
        /// <param name="testState">����״̬</param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="totalCount"></param>
        [OperationContract]
        IList<StudentTestView> FindStudentTests(int studentID, Guid testPaperID, UserTestStatusType testState,
           int pageSize, int pageIndex, out int totalCount);
        /// <summary>
        /// ��ȡ�û����һ�β��Ե���Ϣ
        /// </summary>
        /// <param name="studentID"></param>
        /// <param name="testPaperID"></param>
        /// <param name="testState"></param>
        /// <param name="IsPreview"></param>
        /// <returns></returns>
        [OperationContract]
        StudentTestView GetUserLastTest(int studentID, Guid testPaperID, UserTestStatusType testState, bool IsPreview);
        ///<summary>
        /// �����߼���ĳһ���ԡ�����޷������������쳣��
        ///</summary>
        /// <param name="UserExamID">��������ID</param>
        [OperationContract]
        void StartTest(Guid UserExamID);

        ///<summary>
        /// ��ȡĳһ�û���ָ������ID���Ծ������������������Ϣ���������������桢ѡ�����������ʣ��ʱ�䣬�������Ծ��е���ŵ���Ϣ��
        ///</summary>
        /// <param name="UserExamID">��������ID</param>
        [OperationContract]
        IList<UserQuestion> GetAllQuestions(System.Guid UserExamID);

        ///<summary>
        /// ��ȡĳһ�û���ָ������ID���Ծ�ṹ��Ϣ���Ծ�Ļ�����Ϣ���Ծ���ģ����Ϣ��ģ��ṹ��Ϣ�ȣ�
        ///</summary>
        ///<param name="UserExamID">��������ID</param>
        [OperationContract]
        ExamPaperSchema GetTestPaperSchema(Guid UserExamID);

        ///<summary>
        /// ���������
        ///</summary>
        ///<remarks>
        /// 1,�ṩ�Ŀ����𰸱������������������һ�£�<br></br>
        /// 2,��ͬ�������͵Ĵ�ʵ��������Ϊ��
        /// SingleChoiceAnswer,MultipleChoiceAnswer,JudgementAnswer,
        /// TextEntryAnswer,ExtendedTextAnswer,
        /// MatchAnswer,GroupAnswer<br></br>
        ///</remarks>
        /// <param name="UserExamID">��������ID</param>
        /// <param name="questionID">����ID</param>
        /// <param name="userAnswer">������</param>
        /// <param name="nCurQuestionNo">������ǰ��������</param>
        /// <param name="totalTestTime">���Խ�ֹ�����ʱ����ʱ����λ���룩</param>
        /// <param name="isFeedback">�Ƿ���Ҫ���֣������ش𰸷���</param>
        /// <param name="sQuestionFeedback">���ⷴ����Ϣ</param>
        /// <param name="lstOptionFeedbacks">�����ѡ���</param>
        /// <returns>��������÷֡�ֻ��������ʱ���Ż�õ���ȷ�ķ���</returns>
        [OperationContract]
        decimal UpdateQuestionAnswer(System.Guid UserExamID, System.Guid questionID,
           AnswerBase userAnswer, int nCurQuestionNo,
           int totalTestTime, bool isFeedback,
           out string sQuestionFeedback,
           out IList<OptionFeedback> lstOptionFeedbacks);

        ///<summary>
        /// �õ��Ծ���ĳһ����Ĵ��ⷴ��������ѡ���
        ///</summary>
        /// <param name="UserExamID">��������ID</param>
        /// <param name="questionID">����ID</param>
        /// <param name="UserScore">���ؿ�������÷�</param>
        /// <param name="lstOptionFeedbacks">����ѡ���</param>
        /// <returns>�õ�����Ĵ��ⷴ��</returns>
        [OperationContract]
        string GetQuestionFeedback(System.Guid UserExamID,
           System.Guid questionID, out decimal UserScore,
           out IList<OptionFeedback> lstOptionFeedbacks);

        ///<summary>
        /// �õ�����ĳһ���ԵĽ��
        ///</summary>
        ///<param name="UserExamID">��������ID</param>
        /// <param name="questionID">����ID</param>
        /// <param name="isFeedback">�Ƿ���Ҫ�������ⷴ��</param>
        /// <param name="isPaperFeedback">�Ƿ���Ҫ�Ծ���</param>
        [OperationContract]
        ExamResult GetExamResult(Guid UserExamID, Guid questionID, bool isFeedback, bool isPaperFeedback);

        ///<summary>
        /// �õ��������Ե��Ծ�����Ϣ
        ///</summary>
        ///<param name="UserExamID">��������ID</param>
        ///<returns>�Ծ���</returns>
        [OperationContract]
        IList<TestFeedback> GetPaperFeedback(Guid UserExamID);

        ///<summary>
        /// �ύ�Ծ�
        ///</summary>
        /// <param name="UserExamID">��������ID</param>
        /// <param name="isReturnResult">�Ƿ񷵻ؽ��</param>
        /// <param name="testResult">�����÷�</param>
        [OperationContract]
        void SubmitPaper(System.Guid UserExamID, bool isReturnResult, out decimal testResult);

        ///<summary>
        /// �õ��Ծ���ָ������Ľ���˼·
        ///</summary>
        /// <param name="UserExamID">��������ID</param>
        /// <param name="questionID">����ID</param>
        /// <returns>�������˼·</returns>
        [OperationContract]
        string GetQuestionSolution(System.Guid UserExamID, System.Guid questionID);

        ///<summary>
        /// �õ���������������Ϣ��������������棬ѡ�С�⣬�����𰸵ȡ�
        ///</summary>
        ///<remarks>
        /// 1,�õ��������ǰ���������ͻ��������ʵ����<br></br>
        /// 2,��ͬ�������͵ķֱ�Ϊ��TestSingleChoiceQuestion,TestMultipleChoiceQuestion,
        /// TestJudgementQuestion,TestTextEntryQuestion,TestEntendTextQuestion,
        /// TestMatchQuestion,TestGroupQuestion
        /// </remarks>
        /// <param name="UserExamID">��������ID</param>
        /// <param name="questionID">����ID</param>
        /// <returns>�������</returns>
        [OperationContract]
        UserQuestion GetQuestion(System.Guid UserExamID, System.Guid questionID);

        /// <summary>
        /// �õ�ĳһ�ο��Խ�������ʾ��ͳ�ƽ����
        /// </summary>
        /// <param name="UserExamID"></param>
        /// <returns></returns>
        [OperationContract]
        UserTestResultStat GetUserTestResultStat(Guid UserExamID);

        /// <summary>
        /// �õ�ĳһ��������ָ���Ծ�Ŀ���ͳ����Ϣ����߷֣���ͷ֣��ѿ��Դ���
        /// </summary>
        /// <remarks>
        /// ���صĽ���а�������߷֣���ͷ֣��ѿ��Դ���
        /// </remarks>
        /// <param name="UserID"></param>
        /// <param name="TestPaperID"></param>
        /// <returns></returns>
        [OperationContract]
        UserExamStat GetUserExamStat(int UserID, Guid TestPaperID);
        /// <summary>
        /// �õ�������ָ�����Ե�״̬��Ϣ�������������Ѵ��������Ծ���ʱ�䣬����ʱ��ʣ��ʱ�����Ϣ
        /// </summary>
        /// <remarks>
        /// ���ؽ���а�����<br></br>
        /// �����������Ѵ��������Ծ���ʱ�䣬����ʱ��ʣ��ʱ�����Ϣ
        /// </remarks>
        /// <param name="UserExamID"></param>
        /// <returns></returns>
        [OperationContract]
        UserExamState GetUserExamState(Guid UserExamID);
    }
}