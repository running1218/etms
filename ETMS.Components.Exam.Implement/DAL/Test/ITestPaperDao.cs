using System;
using System.Collections.Generic;
using ETMS.Components.Exam.API.Entity.Test;
using ETMS.Components.Exam.API.Entity;

namespace ETMS.Components.Exam.Implement.DAL.Test
{
    public interface ITestPaperDao
    {
        ///<summary>
        /// 添加一个新的试卷基本信息
        ///</summary>
        /// <param name="testPaper">要添加的新的试卷</param>
        void AddTestPaper(TestPaper testPaper);

        ///<summary>
        /// 更新一个已存在的试卷基本信息
        ///</summary>
        /// <param name="newTestPaper">更新试卷基本信息</param>
        void Update(TestPaper newTestPaper);

        ///<summary>
        /// 删除指定的试卷信息。删除后，会将试卷基本信息、试卷与试题关系、试卷的反馈等删除掉。对于已根据此定义生成的考生答卷不进行任何处理。
        ///</summary>
        /// <param name="testPaperID">要删除的试卷ID</param>
        void Delete(Guid testPaperID);

        ///<summary>
        /// 得到某一试卷基本信息
        ///</summary>
        /// <param name="testPaperID">要获取的试卷ID</param>
        TestPaper GetByID(Guid testPaperID);

        ///<summary>
        /// 更新试题的总题数、试卷总分数
        ///</summary>
        /// <param name="questionsCnt">试卷中的总题数</param>
        /// <param name="totalScore">试卷中的总分数</param>
        void UpdateQuestionsCount(Guid testPaperID, int questionsCnt, int totalScore);

        ///<summary>
        /// 更新及格分数、答卷时长、答卷次数等信息
        ///</summary>
        /// <param name="testPaperID">试卷ID</param>
        /// <param name="jige">及格分数</param>
        /// <param name="examTime">考试的时间(单位：秒）0表示不限制。</param>
        /// <param name="examTimes">允许考试次数</param>
        void UpdateExamTimes(Guid testPaperID, int jige, int examTime, int examTimes);
        /// <summary>
        /// 复制题库的试题数据到考试库的试题备份表
        /// </summary>
        /// <param name="testPaperID"></param>
        void CopyTKQuestionData(Guid testPaperID);

        IList<TestPaper> GetTestPaperListByOperator(string sqlWhere, int pageSize, int pageNo, out int total);

        IList<TestPaper> GetMyTestPaperList(string sqlWhere, int pageSize, int pageNo, out int total);

        void SetShareState(Guid testpaperID, int state);

        void SetAuditState(Guid testpaperID, int state);
        /// <summary>
        /// 获取高级随机试卷的结构
        /// </summary>
        /// <param name="testpaperID"></param>
        /// <returns></returns>
        IList<TestPaperUnit> GetSeniorRandomTestpaperSchema(Guid testpaperID);

        IList<TestPaperUnit> GetCommonTestpaperSchema(Guid testpaperID);

        IList<IDName> GetQuestionByType(Guid testpaperID, int type);

        void SetFixTestPaperScoreAndCount(Guid testpaperID);

        void SetTestPaperCategoryID(string testpaperIDs, Guid categoryID);
    }
}
