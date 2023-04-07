using System;
using System.Collections.Generic;

using ETMS.Components.Exam.API.Entity;
using ETMS.Components.Exam.API.Entity.ItemBank;
using ETMS.Components.Exam.API.Entity.Test;
namespace ETMS.Components.Exam.Implement.DAL.Test
{
    /// <summary>
    /// 试卷策略接口
    /// </summary>
    public interface ITestPaperRuleDao
    {
        /// <summary>
        /// (试题表中)得到指定分类的试题类型汇总
        /// </summary>
        /// <param name="categoryID">题库ID</param>
        /// <returns>统计结果</returns>
        IList<TestPaperRule> GetQuestionTypeReport(Guid categoryID);

        /// <summary>
        /// 保存策略项进策略表
        /// 1.已存在则更新老记录
        /// 2.不存在则添加
        /// 3.(难,中,易)抽取数为0则删除记录
        /// 4.(试卷定义ID)由创建试卷基本信息时得到
        /// </summary>
        void SaveTestPaperRule(TestPaperRule rule);

        /// <summary>
        /// 策略表中所有题库的各题型汇总
        /// </summary>
        /// <param name="testPaperID">试卷定义ID</param>
        /// <returns>汇总结果</returns>
        IList<TestPaperRule> GetQuestionStat(Guid testPaperID);

        /// <summary>
        /// 策略表中的所有试题汇总数
        /// </summary>
        /// <param name="testPaperID">试卷定义ID</param>
        /// <returns>试题总数</returns>
        int GetQuestionTotal(Guid testPaperID);

        /// <summary>
        /// 已添加的必出题数
        /// </summary>
        /// <param name="testPaperID">试卷定义ID</param>
        /// <returns>试题总数</returns>
        int GetPaperQuestionTotal(Guid testPaperID);

        /// <summary>
        /// 策略里减去必出题后的剩余出题数
        /// </summary>
        /// <param name="testPaperID">试卷定义ID</param>
        /// <returns></returns>
        IList<QuestionStat> GetRemainQuestionTotal(Guid testPaperID);
        /*
        /// <summary>
        /// 将策略表试题生成到试题-试卷表中(先清除再生成)
        /// </summary>
        /// <param name="testPaperID"></param>
        /// <returns></returns>
        Guid CreateStudentTestPaper(Guid testPaperID);
        */

        /// <summary>
        /// 得到试题类型的题型汇总(高级随机)
        /// </summary>
        /// <param name="testPaperID">试卷定义ID</param>
        /// <returns></returns>
        IList<QuestionStat> GetQuestionTypeTotal(Guid testPaperID);

        /// <summary>
        /// 按策略表规则抓取试题ID
        /// 准备添加到试题-试卷表中(适用高级固定)
        /// </summary>
        /// <param name="testPaperID"></param>
        /// <returns></returns>
        IList<Question> GetQuestionFromRule(Guid testPaperID);

        /// <summary>
        /// 为指定题型设置分数(各分类的题型一同处理)(适用高级随机)
        /// </summary>
        /// <param name="testPaperID">试卷ID</param>
        /// <param name="questionType">题型</param>
        /// <param name="score">要设置的分数</param>
        void UpdateQuestionTypeScore(Guid testPaperID,QuestionType questionType,decimal score);


        /// <summary>
        /// 为指定用户生成一份试卷
        /// </summary>
        /// <param name="CreaterID">创建者ID</param>
        /// <param name="testPaperID">试卷定义ID</param>
        /// <param name="UserID">用户ID</param>
        /// <param name="buildType">true为正式生成,false为预览生成</param>
        Guid CreateStudentTestPaper(int createrID, Guid testPaperID, int UserID, bool buildType);

        /// <summary>
        /// 为指定用户生成一份试卷
        /// </summary>
        /// <param name="CreaterID">创建者ID</param>
        /// <param name="testPaperID">试卷定义ID</param>
        /// <param name="UserID">用户ID</param>
        /// <param name="buildType">true为正式生成,false为预览生成</param>
        Guid CreateStudentTestPaper1(int createrID, Guid testPaperID, int UserID, bool buildType);


        /// <summary>
        /// 返回高级随机的试题列表
        /// </summary>
        /// <param name="testPaperID">试卷定义ID</param>
        /// <param name="qt">试题类型</param>
        /// <returns>试题ID(GUID列表)</returns>
        IList<IDName> ReturnAdvancedExam(Guid testPaperID, QuestionType qt);


        /// <summary>
        /// 得到剩余题数(高级随机)
        /// </summary>
        /// <param name="testPaperID">试卷定义ID</param>
        /// <param name="bankID">题库ID</param>
        /// <param name="qt">试题类型</param>
        /// <param name="diffi">难度</param>
        /// <returns></returns>
        int GetRemainQuestions(Guid testPaperID, Guid bankID, QuestionType qt, EnumDifficulty diffi);


        /// <summary>
        /// 得到受策略约束的试题列表
        /// </summary>
        /// <param name="testPaperID">试卷定义ID</param>
        /// <param name="bankID">题库ID</param>
        /// <param name="qt">试题类型</param>
        /// <param name="diffi">难度</param>
        /// <param name="title">标题</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="pageNo">当前页</param>
        /// <param name="total">总记录数</param>
        /// <returns>试题集</returns>
        IList<Question> AdvancedQuestionList(Guid testPaperID, Guid bankID,
            QuestionType qt, EnumDifficulty diffi, string title,
            int pageSize, int pageNo, out int total);


        /// <summary>
        /// 判断试题是否存在
        /// </summary>
        /// <param name="testPaperID">试卷定义ID</param>
        /// <param name="questionID">试题ID</param>
        /// <returns>true(存在)</returns>
        bool IsQuestionExist(Guid testPaperID, Guid questionID);
    }
}
