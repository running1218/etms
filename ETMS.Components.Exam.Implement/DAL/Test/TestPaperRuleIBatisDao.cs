using System;
using System.Collections.Generic;

using System.Collections;
using Autumn.Data.ORM.IBatis;
using ETMS.Components.Exam.API.Entity;
using ETMS.Components.Exam.API.Entity.Test;
using ETMS.Components.Exam.API.Entity.ItemBank;
namespace ETMS.Components.Exam.Implement.DAL.Test
{
    /// <summary>
    /// 试卷策略数据访问的实现
    /// </summary>
    public class TestPaperRuleIBatisDao : ReadWriteDataMapperDaoSupport, ITestPaperRuleDao
    {
        /// <summary>
        /// 得到指定分类的试题类型汇总
        /// </summary>
        /// <param name="categoryID"></param>
        /// <returns></returns>
        public IList<TestPaperRule> GetQuestionTypeReport(Guid categoryID)
        {
            return this.DataMapperClient_Read.QueryForList<TestPaperRule>("TestPaperRule.GetQuestionTypeReport", categoryID);
        }

        /// <summary>
        /// 保存策略项(规则)
        /// 1.已存在则更新老记录(TestPaperID,QuestionBankID,QuestionType对应一条记录)
        /// 2.不存在则添加
        /// 3.(难,中,易)抽取数为0则删除记录
        /// 4.(试卷定义ID)由创建试卷基本信息时得到
        /// </summary>
        public void SaveTestPaperRule(TestPaperRule rule)
        {
            //this.DataMapperClient_Write.Update("TestPaperRule.SaveTestPaperRule", rule);
            Hashtable ht = new Hashtable();
            ht.Add("PaperID", rule.TestPaperID);
            ht.Add("BankID", rule.QuestionBankID);
            ht.Add("QType", (int)rule.QuestionType);
            ht.Add("LowSelect", rule.LowSelectQty);
            ht.Add("MediumSelect", rule.MediumSelectQty);
            ht.Add("HighSelect", rule.HighSelectQty);
            ht.Add("UserID", rule.CreatedUserID);
            this.DataMapperClient_Write.Update("TestPaperRule.SaveTestPaperRule", ht);
        }

        /// <summary>
        /// 得到策略表中所有题库的各题型汇总
        /// </summary>
        /// <param name="testPaperID">试卷定义ID</param>
        /// <returns>汇总结果</returns>
        public IList<TestPaperRule> GetQuestionStat(Guid testPaperID)
        {
            return this.DataMapperClient_Read.QueryForList<TestPaperRule>("TestPaperRule.GetQuestionStat", testPaperID);
        }
        /// <summary>
        /// 策略表中的试题总数
        /// </summary>
        /// <param name="testPaperID">试卷定义ID</param>
        /// <returns>试题总数</returns>
        public int GetQuestionTotal(Guid testPaperID)
        {
            object result = DataMapperClient_Read.QueryForObject("TestPaperRule.GetQuestionTotal", testPaperID);
            if (result == null)
                return 0;
            else
                return (int)result;
        }


        /// <summary>
        /// 按策略表规则抓取试题ID
        /// 准备添加到试题-试卷表中(适用高级固定)
        /// </summary>
        /// <param name="testPaperID"></param>
        /// <returns></returns>
        public IList<Question> GetQuestionFromRule(Guid testPaperID)
        {
            return this.DataMapperClient_Read.QueryForList<Question>("TestPaperRule.GetQuestionFromRule", testPaperID);
        }
        /// <summary>
        /// 已添加的必出题数
        /// </summary>
        /// <param name="testPaperID">试卷定义ID</param>
        /// <returns>试题总数</returns>
        public int GetPaperQuestionTotal(Guid testPaperID)
        {
            int result = (int)DataMapperClient_Read.QueryForObject("TestPaperRule.GetPaperQuestionTotal", testPaperID);
            return result;
        }
        /// <summary>
        /// 策略里减去必出题后的剩余出题数
        /// </summary>
        /// <param name="testPaperID"></param>
        /// <returns></returns>
        public IList<QuestionStat> GetRemainQuestionTotal(Guid testPaperID)
        {
            return this.DataMapperClient_Read.QueryForList<QuestionStat>("TestPaperRule.GetRemainQuestionTotal", testPaperID);
        }

        /// <summary>
        /// 得到试题类型的题型汇总(高级随机)
        /// </summary>
        /// <param name="testPaperID">试卷定义ID</param>
        /// <returns></returns>
        public IList<QuestionStat> GetQuestionTypeTotal(Guid testPaperID)
        {
            Hashtable ht = new Hashtable();
            ht.Add("PaperID", testPaperID);
            return this.DataMapperClient_Read.QueryForList<QuestionStat>("TestPaperRule.GetQuestionTypeTotal", ht);
        }
        /// <summary>
        /// 为指定题型设置分数(各分类的题型一同处理)
        /// </summary>
        /// <param name="testPaperID">试卷ID</param>
        /// <param name="questionType">题型</param>
        /// <param name="score">要设置的分数</param>
        public void UpdateQuestionTypeScore(Guid testPaperID, QuestionType questionType, decimal score)
        {
            DataMapperClient_Write.Update("TestPaperRule.UpdateQuestionTypeScore", new
            {
                TestPaperID = testPaperID,
                QuestionType = questionType,
                Score = score
            });
        }

        /// <summary>
        /// 为指定用户生成一份试卷
        /// </summary>
        /// <param name="testPaperID">试卷定义ID</param>
        /// <param name="UserID">用户ID</param>
        /// <param name="buildType">true为正式生成,false为预览生成</param>
        public Guid CreateStudentTestPaper(int createrID, Guid testPaperID, int userID, bool buildType)
        {
            Hashtable ht = new Hashtable();
            ht.Add("CreaterID", createrID);
            ht.Add("PaperID", testPaperID);
            ht.Add("UserID", userID);
            ht.Add("BuildType", buildType);
            ht.Add("ExamID", Guid.Empty);

            DataMapperClient_Write.Update("TestPaperRule.CreateStudentTestPaper", ht);


            /*
            DataMapperClient_Write.Update("TestPaperRule.CreateStudentTestPaper", new
            {
                CreaterID=createrID,
                PaperID = testPaperID,
                UserID = userID,
                BuildType = buildType
            });
            */
            return (Guid)ht["ExamID"];
        }

        /// <summary>
        /// 为指定用户生成一份试卷
        /// </summary>
        /// <param name="testPaperID">试卷定义ID</param>
        /// <param name="UserID">用户ID</param>
        /// <param name="buildType">true为正式生成,false为预览生成</param>
        public Guid CreateStudentTestPaper1(int createrID, Guid testPaperID, int userID, bool buildType)
        {
            Hashtable ht = new Hashtable();
            ht.Add("CreaterID", createrID);
            ht.Add("PaperID", testPaperID);
            ht.Add("UserID", userID);
            ht.Add("BuildType", buildType);
            ht.Add("ExamID", Guid.Empty);

            DataMapperClient_Write.Update("TestPaperRule.CreateStudentTestPaper1", ht);


            /*
            DataMapperClient_Write.Update("TestPaperRule.CreateStudentTestPaper", new
            {
                CreaterID=createrID,
                PaperID = testPaperID,
                UserID = userID,
                BuildType = buildType
            });
            */
            return (Guid)ht["ExamID"];
        }

        /// <summary>
        /// 返回高级随机的试题列表
        /// </summary>
        /// <param name="testPaperID">试卷定义ID</param>
        /// <param name="qt">试题类型</param>
        /// <returns>试题ID(GUID列表)</returns>
        public IList<IDName> ReturnAdvancedExam(Guid testPaperID, QuestionType qt)
        {
            Hashtable ht = new Hashtable();
            ht.Add("PaperID", testPaperID);
            ht.Add("QuestionType", (int)qt);
            return this.DataMapperClient_Read.QueryForList<IDName>("TestPaperRule.ReturnAdvancedExam", ht);
        }


        /// <summary>
        /// 得到剩余题数(高级随机)
        /// </summary>
        /// <param name="testPaperID">试卷定义ID</param>
        /// <param name="bankID">题库ID</param>
        /// <param name="qt">试题类型</param>
        /// <param name="diffi">难度</param>
        /// <returns></returns>
        public int GetRemainQuestions(Guid testPaperID, Guid bankID, QuestionType qt, EnumDifficulty diffi)
        {
            Hashtable ht = new Hashtable();
            ht.Add("PaperID", testPaperID);
            ht.Add("BankID", bankID);
            ht.Add("QType", (int)qt);
            ht.Add("Diffi", (int)diffi);
            ht.Add("Remain", 0);

            DataMapperClient_Write.QueryForObject("TestPaperRule.GetRemainQuestions", ht);
            return (int)ht["Remain"];
        }

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
        public IList<Question> AdvancedQuestionList(Guid testPaperID, Guid bankID,
            QuestionType qt, EnumDifficulty diffi, string title,
            int pageSize, int pageNo, out int total)
        {
            Hashtable ht = new Hashtable();
            ht.Add("PaperID", testPaperID);
            ht.Add("BankID", bankID);
            ht.Add("QType", (int)qt);
            ht.Add("Diffi", (int)diffi);
            ht.Add("Title", title);
            ht.Add("PageSize", pageSize);
            ht.Add("CurrentPage", pageNo);
            ht.Add("ItemCount", 0);

            total = 0;
            IList<Question> resultList = new List<Question>();

            resultList = DataMapperClient_Write.QueryForList<Question>("TestPaperRule.AdvancedQuestionList", ht);
            total = Convert.ToInt32(ht["ItemCount"]);
            return resultList;
        }


        /// <summary>
        /// 判断试题是否存在
        /// </summary>
        /// <param name="testPaperID">试卷定义ID</param>
        /// <param name="questionID">试题ID</param>
        /// <returns>true(存在)</returns>
        public bool IsQuestionExist(Guid testPaperID, Guid questionID)
        {
            int result = (int)DataMapperClient_Read.QueryForObject("TestPaperRule.IsQuestionExist", new
            {
                TestPaperID = testPaperID,
                QuestionID = questionID
            });
            if (result > 0)
                return true;
            else
                return false;
        }


    }
}
