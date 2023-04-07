using System;
using System.Collections.Generic;

using ETMS.Components.Exam.API.Entity;
using ETMS.Components.Exam.API.Entity.ItemBank;
using ETMS.Components.Exam.API.Entity.Test;
using System.ServiceModel;
namespace ETMS.Components.Exam.API.Interface.Test
{
    ///<summary>
    /// 试卷策略的对外逻辑接口
    ///</summary>
    [ServiceContract(Namespace = "http://Autumn.Business.LMS.Interface.Test/ITestPaperRuleLogic")]
    public interface ITestPaperRuleLogic
    {
        /// <summary>
        /// 得到指定分类(题库ID)的试题类型汇总
        /// </summary>
        /// <param name="categoryID">题库ID</param>
        /// <returns>各类型对应的难,中,易试题数</returns>
        [OperationContract]
        IList<TestPaperRule> GetQuestionTypeReport(Guid categoryID);

        /// <summary>
        /// 保存策略项
        /// 1.已存在则更新老记录
        /// 2.不存在则添加
        /// 3.(难,中,易)抽取数为0则删除记录
        /// </summary>
        [OperationContract]
        void SaveTestPaperRule(TestPaperRule rule);

        /// <summary>
        /// 指定试卷中所有类型中各难度级别的试题数
        /// </summary>
        /// <param name="testPaperID">试卷定义ID</param>
        /// <returns></returns>
        [OperationContract]
        IList<TestPaperRule> GetQuestionStat(Guid testPaperID);

        /// <summary>
        /// 策略表中的试题总数
        /// </summary>
        /// <param name="testPaperID">试卷定义ID</param>
        /// <returns>试题总数</returns>
        [OperationContract]
        int GetQuestionTotal(Guid testPaperID);


        /// <summary>
        /// 已添加的必出题数
        /// </summary>
        /// <param name="testPaperID">试卷定义ID</param>
        /// <returns>试题总数</returns>
        [OperationContract]
        int GetPaperQuestionTotal(Guid testPaperID);

        /// <summary>
        /// 策略里减去必出题后的剩余出题数
        /// </summary>
        /// <param name="testPaperID">试卷定义ID</param>
        /// <returns></returns>
        [OperationContract]
        IList<QuestionStat> GetRemainQuestionTotal(Guid testPaperID);

        /// <summary>
        /// 得到试题类型的题型汇总(高级随机)
        /// </summary>
        /// <param name="testPaperID">试卷定义ID</param>
        /// <returns></returns>
        [OperationContract]
        IList<QuestionStat> GetQuestionTypeTotal(Guid testPaperID);

        /// <summary>
        /// 为指定题型设置分数(各分类的题型一同处理)(适用高级随机)
        /// </summary>
        /// <param name="testPaperID">试卷ID</param>
        /// <param name="questionType">题型</param>
        /// <param name="score">要设置的分数</param>
        [OperationContract]
        void UpdateQuestionTypeScore(Guid testPaperID, QuestionType questionType, decimal score);





        /// <summary>
        /// 按策略表规则抓取试题ID
        /// 准备添加到试题-试卷表中(适用高级固定)
        /// </summary>
        /// <param name="testPaperID">试卷定义ID</param>
        /// <returns></returns>
        //IList<Question> GetQuestionFromRule(Guid testPaperID);
        [OperationContract]
        IList<KeyValuePair<Guid, QuestionType>> GetQuestionFromRule(Guid testPaperID);

        /// <summary>
        /// 按策略表规则,抓取试题到"试卷-试题"表中
        /// </summary>
        /// <param name="testPaperID">试卷定义ID</param>
        [OperationContract]
        void CreateQuestionFromRule(Guid testPaperID);

        /// <summary>
        /// 将指定试题集加入到"试卷-试题"表中
        /// </summary>
        /// <param name="testPaperID">试卷定义ID</param>
        /// <param name="results">要加入的试题集</param>
        [OperationContract]
        void CreateQuestion(Guid testPaperID, List<KeyValuePair<Guid, QuestionType>> results);



 
        /// <summary>
        /// 为指定用户生成一份试卷
        /// </summary>
        /// <param name="testPaperID">试卷定义ID</param>
        /// <param name="UserID">用户ID</param>
        /// <param name="buildType">true为正式生成,false为预览生成</param>
        /// <returns>返回考生的答卷ID</returns>
        [OperationContract]
        Guid CreateStudentTestPaper(Guid testPaperID, int UserID, bool buildType);

        /// <summary>
        /// 返回高级随机的试题列表
        /// </summary>
        /// <param name="testPaperID">试卷定义ID</param>
        /// <param name="qt">试题类型</param>
        /// <returns>试题ID(GUID列表)</returns>
        [OperationContract]
        IList<IDName> ReturnAdvancedExam(Guid testPaperID, QuestionType qt);

        /// <summary>
        /// 返回高级随机的试题总数
        /// </summary>
        /// <param name="testPaperID">试卷定义ID</param>
        /// <returns>试题总数</returns>
        [OperationContract]
        int AdvancedQuestionTotal(Guid testPaperID);


        /// <summary>
        /// 得到剩余题数(高级随机)
        /// </summary>
        /// <param name="testPaperID">试卷定义ID</param>
        /// <param name="bankID">题库ID</param>
        /// <param name="qt">试题类型</param>
        /// <param name="diffi">难度</param>
        /// <returns></returns>
        [OperationContract]
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
        [OperationContract]
        IList<Question> AdvancedQuestionList(Guid testPaperID, Guid bankID,
            QuestionType qt, EnumDifficulty diffi, string title,
            int pageSize, int pageNo, out int total);


        /// <summary>
        /// 判断试题是否存在
        /// </summary>
        /// <param name="testPaperID">试卷定义ID</param>
        /// <param name="questionID">试题ID</param>
        /// <returns>true(存在)</returns>
        [OperationContract]
        bool IsQuestionExist(Guid testPaperID, Guid questionID);
    }
}
