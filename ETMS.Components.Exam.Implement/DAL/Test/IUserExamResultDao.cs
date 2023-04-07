using System;
using System.Collections.Generic;
using ETMS.Components.Exam.API.Entity.Test;

namespace ETMS.Components.Exam.Implement.DAL.Test
{
    /// <summary>
    /// 对考生答卷试题作答结果的数据访问接口
    /// </summary>
    public interface IUserExamResultDao
    {
        /// <summary>
        /// 添加一新的考生试题作答结果
        /// </summary>
        /// <param name="examResult"></param>
        void Add(UserExamResult examResult);
        /// <summary>
        /// 更新一答题反馈信息项
        /// </summary>
        /// <param name="newResult"></param>
        void Update(UserExamResult newResult);
        /// <summary>
        /// 删除考生试题作答结果项
        /// </summary>
        /// <param name="ResultID"></param>
        void Delete(Guid ResultID);
        /// <summary>
        /// 获取指定ID的考生试题作答结果项
        /// </summary>
        /// <param name="ResultID">试题作答结果ID</param>
        /// <returns></returns>
        UserExamResult GetByID(Guid ResultID);

        /// <summary>
        ///  更新试题的考生答案
        /// </summary>
        /// <param name="UserExamID">考生答卷ID</param>
        /// <param name="QuestionID">试题ID</param>
        /// <param name="sUserAnswer">考生分数</param>
        void UpdateUserAnswer(Guid UserExamID, Guid QuestionID, string sUserAnswer);

        /// <summary>
        ///  更新试题的考生答案
        /// </summary>
        /// <param name="UserExamID">考生答卷ID</param>
        /// <param name="QuestionID">试题ID</param>
        /// <param name="sUserAnswer">考生分数</param>
        void UpdateUserAnswer(Guid UserExamID, Guid QuestionID, string sUserAnswer, decimal ExamScore);
        /// <summary>
        /// 更新试题的考生分数
        /// </summary>
        /// <param name="UserExamID">考生答卷ID</param>
        /// <param name="QuestionID">试题ID</param>
        /// <param name="ExamScore">考生得分</param>
        void UpdateUserQuestionScore(Guid UserExamID, Guid QuestionID, decimal ExamScore);

        /// <summary>
        /// 获取某一答卷的考生总分
        /// </summary>
        /// <param name="UserExamID">考生答卷ID</param>
        /// <param name="ExamScore">答卷总分</param>
        /// <returns></returns>
        decimal GetUserScore(Guid UserExamID, out decimal ExamScore);

        /// <summary>
        /// 得到一个试卷中所有作答结果信息
        /// </summary>
        /// <param name="UserExamID"></param>
        /// <returns></returns>
        IList<UserExamResult> FindAllInUserExam(Guid UserExamID);
        /// <summary>
        /// 删除一个试卷中所有作答结果信息
        /// </summary>
        /// <param name="UserExamID"></param>
        /// <returns></returns>
        bool DeleteAllInUserExam(Guid UserExamID);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserExamID"></param>
        /// <returns></returns>
        UserExamState GetUserExamState(Guid UserExamID);
        /// <summary>
        /// 得到试卷中每一题型的试题总数与考生答对试题数
        /// </summary>
        /// <param name="UserExamID"></param>
        /// <returns></returns>
        IList<QuestionTypeUserResult> GetQuestionTypeUserResult(Guid UserExamID);

        /// <summary>
        /// 为某一指定的试卷生成原始的结果信息
        /// </summary>
        /// <param name="UserExamID"></param>
        void CreateExamResultForUserExam(Guid UserExamID);
    }
}
