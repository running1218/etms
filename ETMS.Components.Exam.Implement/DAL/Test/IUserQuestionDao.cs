using System;
using System.Collections.Generic;
using ETMS.Components.Exam.API.Entity.Test;
using ETMS.Components.Exam.API.Entity.ItemBank;

namespace ETMS.Components.Exam.Implement.DAL.Test
{
    /// <summary>
    /// 对考生答卷中试题的数据操作操作。其中包括了对于题面，试题选项，选项组等的操作
    /// </summary>
    /// <remarks>
    /// 1，对应于数据库中的KS_ExamQuestion,KS_ExamQuestionOption,KS_ExamOptionGroup
    /// </remarks>
    public interface IUserQuestionDao
    {
        #region --对于试题的操作--
        void AddExamQuestion(UserQuestion ExamQuestion);
        /// <summary>
        /// 得到一次考试的所有试题的题面信息
        /// </summary>
        /// <param name="UserExamID"></param>
        /// <returns></returns>
        IList<UserQuestion> FindQuestionsInUserExam(Guid UserExamID);
        /// <summary>
        /// 删除一次考试的试卷的所有试题题面信息
        /// </summary>
        /// <param name="UserExamID"></param>
        bool DeleteQuestionsInUserExam(Guid UserExamID);
        /// <summary>
        /// 删除一个答卷中指定的ID的试题项
        /// </summary>
        /// <param name="UserExamID"></param>
        /// <param name="QuestionID"></param>
        bool DeleteQuestionByQuestionID(Guid UserExamID, Guid QuestionID);
        #endregion

        #region --对于选项的操作--
        void AddExamOption(TestQuestionOption ExamQuestion);
        /// <summary>
        /// 得到一次考试的所有试题的题面信息
        /// </summary>
        /// <param name="UserExamID"></param>
        /// <returns></returns>
        IList<TestQuestionOption> FindOptionsInUserExam(Guid UserExamID);
        /// <summary>
        /// 删除一次考试的试卷的所有试题题面信息
        /// </summary>
        /// <param name="UserExamID"></param>
        bool DeleteOptionsInUserExam(Guid UserExamID);
        /// <summary>
        /// 删除一个答卷中指定的ID的试题项
        /// </summary>
        /// <param name="UserExamID"></param>
        /// <param name="QuestionID"></param>
        bool DeleteOptionsByQuestionID(Guid UserExamID, Guid QuestionID);
        #endregion

        #region --对于选项组的操作--
        void AddExamOptionGroups(TestOptionGroup ExamQuestion);
        /// <summary>
        /// 得到一次考试的所有试题的题面信息
        /// </summary>
        /// <param name="UserExamID"></param>
        /// <returns></returns>
        IList<TestOptionGroup> FindOptionGroupsInUserExam(Guid UserExamID);
        /// <summary>
        /// 删除一次考试的试卷的所有试题题面信息
        /// </summary>
        /// <param name="UserExamID"></param>
        bool DeleteOptionGroupsInUserExam(Guid UserExamID);
        /// <summary>
        /// 删除一个答卷中指定的ID的试题项
        /// </summary>
        /// <param name="UserExamID"></param>
        /// <param name="QuestionID"></param>
        bool DeleteOptionGroupsByQuestionID(Guid UserExamID, Guid QuestionID);
        #endregion

        #region --得到试题反馈、选项反馈、解题思路--
        /// <summary>
        /// 得到试题全部的试题反馈
        /// </summary>
        /// <param name="UserExamID"></param>
        /// <param name="QuestionID"></param>
        /// <returns></returns>
        IList<QuestionFeedback> GetQuestionFeedbackByQuestion(Guid UserExamID, Guid QuestionID);
        /// <summary>
        /// 得到指定试题的全部选项反馈
        /// </summary>
        /// <param name="UserExamID"></param>
        /// <param name="QuestionID"></param>
        /// <returns></returns>
        IList<OptionFeedback> GetOptionFeedbackByQuestion(Guid UserExamID, Guid QuestionID);

        /// <summary>
        /// 得到试题的解题思路
        /// </summary>
        /// <param name="QuestionID"></param>
        /// <returns></returns>
        QuestionExtend GetQuestionExtendByQuestionID(Guid UserExamID, Guid QuestionID);
        #endregion
    }
}
