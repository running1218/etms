using System;
using System.Collections.Generic;
using ETMS.Components.Exam.API.Entity.Test;

namespace ETMS.Components.Exam.Implement.DAL.Test
{
    public interface ITestFeedbackDao
    {
        /// <summary>
        /// 添加一新的答题反馈信息项
        /// </summary>
        /// <param name="feedbackItem"></param>
        void AddTestFeedback(TestFeedback feedbackItem);
        /// <summary>
        /// 更新一答题反馈信息项
        /// </summary>
        /// <param name="newFeedbackItem"></param>
        void Update(TestFeedback newFeedbackItem);
        /// <summary>
        /// 删除一答题反馈信息项
        /// </summary>
        /// <param name="testFeedbackID"></param>
        void Delete(Guid testFeedbackID);

        /// <summary>
        /// 得到指定试卷中的所有答题反馈项列表
        /// </summary>
        /// <param name="testPaperID"></param>
        /// <returns></returns>
        IList<TestFeedback> FindTestFeedbacksInPaper(Guid testPaperID);
        /// <summary>
        /// 删除一个试卷中的所有答题反馈信息
        /// </summary>
        /// <param name="testPaperID"></param>
        /// <returns></returns>
        bool DeleteAllInPaper(Guid testPaperID);
    }
}
