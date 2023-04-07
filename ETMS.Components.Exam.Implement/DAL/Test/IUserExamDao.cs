using System;
using System.Collections.Generic;
using ETMS.Components.Exam.API.Entity.Test;

namespace ETMS.Components.Exam.Implement.DAL.Test
{
    public interface IUserExamDao
    {
        #region --用户试卷的状态--
        /// <summary>
        /// 得到考生某一答卷状态
        /// </summary>
        /// <param name="UserExamID"></param>
        /// <returns></returns>
        UserTestStatusType GetTestStatusType(Guid UserExamID);
        /// <summary>
        /// 更新考生某一答卷状态
        /// </summary>
        /// <param name="UserExamID"></param>
        /// <param name="NewTestStatusType"></param>
        void UpdateTestStatusType(Guid UserExamID, UserTestStatusType NewTestStatusType);
        #endregion

        /// <summary>
        /// 得到某一个考生，指定试卷的所有次考试信息
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="TestPaperID"></param>
        /// <returns></returns>
        IList<UserExam> FindAllUserExamsFor(int UserID, Guid TestPaperID);
        /// <summary>
        /// 得到试卷中某一试题信息
        /// </summary>
        /// <param name="UserExamID"></param>
        /// <param name="QuestionID"></param>
        /// <returns></returns>
        UserExam GetUserExamByUserExamID(Guid UserExamID);

        /// <summary>
        /// 更新考生考试的登录时间
        /// </summary>
        /// <param name="UserExamID"></param>
        void UpdateStartExamDateTime(Guid UserExamID);
        /// <summary>
        /// 考生考试结束
        /// </summary>
        /// <param name="UserExamID"></param>
        void TestOver(Guid UserExamID);
        /// <summary>
        /// 更新考生成绩
        /// </summary>
        /// <param name="UserExamID"></param>
        void UpdateUserScore(Guid UserExamID,decimal UserScore);
        /// <summary>
        /// 更新考生成绩
        /// </summary>
        /// <param name="UserExamID"></param>
        void UpdateUserScoreOver(Guid UserExamID, UserTestStatusType NewTestStatusType);
        /// <summary>
        /// 更新考生的答题时间
        /// </summary>
        /// <param name="UserExamID"></param>
        /// <param name="nElapsedTime">使用总时间</param>
        /// <param name="nCurQuestionNo">当前题号</param>
        void UpdateExamElapsedTime(Guid UserExamID, int nElapsedTime,int nCurQuestionNo);
        ///<summary>
        /// 得到指定考生的测试信息
        ///</summary>
        /// <param name="studentID">考生ID</param>
        /// <param name="testPaperID">试卷ID</param>
        /// <param name="testState">考试状态</param>
        IList<StudentTestView> FindStudentTests(int studentID, Guid testPaperID, UserTestStatusType testState,
            int pageSize, int pageIndex, out int totalCount);
        /// <summary>
        /// 得到某一个考生，指定试卷的考试的次数
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="TestPaperID"></param>
        /// <returns></returns>
        int FindAllUserExamsCountFor(int UserID, Guid TestPaperID);

        IList<StudentTestView> FindStudentTests(int studentID, Guid testPaperID, UserTestStatusType testState);
        /// <summary>
        /// 得到某一个考生，指定试卷的考试统计信息
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="TestPaperID"></param>
        /// <returns></returns>
        UserExamStat GetUserExamStatByTestPaper(int UserID, Guid TestPaperID);

        StudentTestView GetUserLastTest(int studentID, Guid testPaperID, UserTestStatusType testState, bool IsPreview);
    }
}
