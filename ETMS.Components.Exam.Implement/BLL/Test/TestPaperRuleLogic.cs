using System;
using System.Collections.Generic;

using Autumn.Context;
using Autumn.Objects.Factory;
using ETMS.Components.Exam.API.Entity;
using ETMS.Components.Exam.Implement.DAL.Test;
using ETMS.Components.Exam.API.Entity.Test;
using ETMS.Components.Exam.API.Entity.ItemBank;
using ETMS.Components.Exam.API.Interface.Test;
namespace ETMS.Components.Exam.Implement.BLL.Test
{
    /// <summary>
    /// 试卷策略
    /// </summary>
    public class TestPaperRuleLogic : ITestPaperRuleLogic, IMessageSourceAware, IInitializingObject
    {
        public ITestPaperRuleDao TestPaperRuleDao { get; set; }
        public IPaperQuestionLogic PaperQuestionLogic { get; set; }

        #region IInitializingObject 成员
        public void AfterPropertiesSet()
        {
            if (this.TestPaperRuleDao == null)
                throw new Exception("please set TestPaperRuleDao Property First!");
        }
        #endregion
        #region IMessageSourceAware 成员
        public IMessageSource MessageSource { get; set; }
        #endregion
        #region 数据是否有效
        /// <summary>
        /// 检查提交的数据是否有效
        /// </summary>
        /// <param name="category"></param>
        private void CheckTestPaperRule(TestPaperRule rule)
        {
            Guid testId = rule.TestPaperID;
            if(testId == Guid.Empty)
                throw new ETMS.AppContext.BusinessException("试卷定以ID不能为空！");

            if(rule.LowSelectQty>rule.LowTotalQty)
                throw new ETMS.AppContext.BusinessException("挑选题数超出了总题数！");
            if(rule.MediumSelectQty>rule.MediumTotalQty)
                throw new ETMS.AppContext.BusinessException("挑选题数超出了总题数！");
            if(rule.HighSelectQty>rule.HighTotalQty)
                throw new ETMS.AppContext.BusinessException("挑选题数超出了总题数！");
            /*
            string name = category.CategoryName;
            if (string.IsNullOrEmpty(name))
                throw new ETMS.AppContext.BusinessException("分类的名称不能为空！");
            else if (name.IndexOf("/") != -1)
                throw new ETMS.AppContext.BusinessException("分类名称中不能包含'/'！");

            int categoryType = (int)category.CategoryType;
            if (categoryType == 0)
                throw new ETMS.AppContext.BusinessException("分类必需设定分类类别！");
             */
        }
        #endregion
        /// <summary>
        /// 得到指定分类的试题类型汇总
        /// </summary>
        /// <param name="categoryID"></param>
        /// <returns></returns>
        public IList<TestPaperRule> GetQuestionTypeReport(Guid categoryID)
        {
            return this.TestPaperRuleDao.GetQuestionTypeReport(categoryID);
        }

        /// <summary>
        /// 保存策略项
        /// 1.已存在则更新老记录
        /// 2.不存在则添加
        /// 3.(难,中,易)抽取数为0则删除记录
        /// </summary>
        public void SaveTestPaperRule(TestPaperRule rule)
        {
            CheckTestPaperRule(rule);
            rule.CreatedUserID = ETMS.AppContext.UserContext.Current.UserID;
            //rule.RuleID = (rule.RuleID == Guid.Empty) ? Guid.NewGuid() : rule.RuleID;
            this.TestPaperRuleDao.SaveTestPaperRule(rule);
        }

        /// <summary>
        /// 指定试卷中所有类型中各难度级别的试题数
        /// </summary>
        /// <param name="testPaperID"></param>
        /// <returns></returns>
        public IList<TestPaperRule> GetQuestionStat(Guid testPaperID)
        {
            return this.TestPaperRuleDao.GetQuestionStat(testPaperID);
        }
        /// <summary>
        /// 策略表中的试题总数
        /// </summary>
        /// <param name="testPaperID">试卷定义ID</param>
        /// <returns>试题总数</returns>
        public int GetQuestionTotal(Guid testPaperID)
        {
            return this.TestPaperRuleDao.GetQuestionTotal(testPaperID);
        }

        /// <summary>
        /// 已添加的必出题数
        /// </summary>
        /// <param name="testPaperID">试卷定义ID</param>
        /// <returns>试题总数</returns>
        public int GetPaperQuestionTotal(Guid testPaperID)
        {
            return this.TestPaperRuleDao.GetPaperQuestionTotal(testPaperID);
        }

        /// <summary>
        /// 策略里减去必出题后的剩余出题数
        /// </summary>
        /// <param name="testPaperID"></param>
        /// <returns></returns>
        public IList<QuestionStat> GetRemainQuestionTotal(Guid testPaperID)
        {
            return this.TestPaperRuleDao.GetRemainQuestionTotal(testPaperID);
        }

        /// <summary>
        /// 得到试题类型的题型汇总(高级随机)
        /// </summary>
        /// <param name="testPaperID">试卷定义ID</param>
        /// <returns></returns>
        public IList<QuestionStat> GetQuestionTypeTotal(Guid testPaperID)
        {
            return this.TestPaperRuleDao.GetQuestionTypeTotal(testPaperID);
        }

        /// <summary>
        /// 为指定题型设置分数(各分类的题型一同处理)(适用高级随机)
        /// </summary>
        /// <param name="testPaperID">试卷ID</param>
        /// <param name="questionType">题型</param>
        /// <param name="score">要设置的分数</param>
        public void UpdateQuestionTypeScore(Guid testPaperID, QuestionType questionType, decimal score)
        {
            //更改策略表中的分数
            this.TestPaperRuleDao.UpdateQuestionTypeScore(testPaperID,questionType,score);

            //更改"试卷-试题"中的分数
            PaperQuestionLogic.UpdateQuestionTypeScoreInPaper(testPaperID, questionType, score);
        }




        /// <summary>
        /// 按策略表规则抓取试题ID
        /// 准备添加到试题-试卷表中(适用高级固定)
        /// </summary>
        /// <param name="testPaperID">试卷定义ID</param>
        /// <returns>返回试题ID和试题类型</returns>
        public IList<KeyValuePair<Guid, QuestionType>> GetQuestionFromRule(Guid testPaperID)
        {
            IList<KeyValuePair<Guid, QuestionType>> resultList = new List<KeyValuePair<Guid, QuestionType>>();
            IList<Question> results=this.TestPaperRuleDao.GetQuestionFromRule(testPaperID);
            foreach (Question tmp in results)
            {
                resultList.Add(new KeyValuePair<Guid,QuestionType>(tmp.QuestionID, tmp.QuestionType));
            }
            return resultList;
        }
        /// <summary>
        /// 按策略表规则,抓取试题到"试卷-试题"表中
        /// </summary>
        /// <param name="testPaperID">试卷定义ID</param>
        public void CreateQuestionFromRule(Guid testPaperID)
        {
            IList<KeyValuePair<Guid, QuestionType>> resultList = GetQuestionFromRule(testPaperID);
            PaperQuestionLogic.TestPaperDeleteQuestion(testPaperID);
            PaperQuestionLogic.AddQuestionToTestPaper(testPaperID, resultList);
        }
        /// <summary>
        /// 将指定试题集加入到"试卷-试题"表中
        /// </summary>
        /// <param name="testPaperID">试卷定义ID</param>
        /// <param name="results">要加入的试题集</param>
        public void CreateQuestion(Guid testPaperID,
            List<KeyValuePair<Guid, QuestionType>> results)
        {
            PaperQuestionLogic.AddQuestionToTestPaper(testPaperID, results);
        }

        /// <summary>
        /// 为指定用户生成一份试卷
        /// </summary>
        /// <param name="testPaperID">试卷定义ID</param>
        /// <param name="UserID">用户ID</param>
        /// <param name="buildType">true为正式生成,false为预览生成</param>
        /// <returns>返回考生的答卷ID</returns>
        public Guid CreateStudentTestPaper(Guid testPaperID,int userID,bool buildType)
        {
            int createrID = ETMS.AppContext.UserContext.Current.UserID;
            Guid eid=TestPaperRuleDao.CreateStudentTestPaper(createrID, testPaperID, userID, buildType);
            return eid;
        }


        /// <summary>
        /// 为指定用户生成一份试卷
        /// </summary>
        /// <param name="testPaperID">试卷定义ID</param>
        /// <param name="UserID">用户ID</param>
        /// <param name="buildType">true为正式生成,false为预览生成</param>
        /// <returns>返回考生的答卷ID</returns>
        public Guid CreateStudentTestPaper1(Guid testPaperID, int userID, bool buildType)
        {
            int createrID = ETMS.AppContext.UserContext.Current.UserID;
            Guid eid = TestPaperRuleDao.CreateStudentTestPaper1(createrID, testPaperID, userID, buildType);
            return eid;
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
            return TestPaperRuleDao.GetRemainQuestions(testPaperID, bankID, qt, diffi);
        }


        /// <summary>
        /// 返回高级随机的试题列表
        /// </summary>
        /// <param name="testPaperID">试卷定义ID</param>
        /// <param name="qt">试题类型</param>
        /// <returns>试题ID(GUID列表)</returns>
        public IList<IDName> ReturnAdvancedExam(Guid testPaperID, QuestionType qt)
        {
            return this.TestPaperRuleDao.ReturnAdvancedExam(testPaperID,qt);
        }

        /// <summary>
        /// 返回高级随机的试题总数
        /// </summary>
        /// <param name="testPaperID">试卷定义ID</param>
        /// <returns>试题总数</returns>
        public int AdvancedQuestionTotal(Guid testPaperID)
        {
            int ruleTotal = this.TestPaperRuleDao.GetQuestionTotal(testPaperID);
            int total = 0;
            //total=this.TestPaperRuleDao.GetPaperQuestionTotal(testPaperID);
            total += ruleTotal;
            PaperQuestionLogic.UpdateTestpaperTotalQuantity(testPaperID, total);
            return total;
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
            return this.TestPaperRuleDao.AdvancedQuestionList(testPaperID, bankID,
            qt, diffi, title, pageSize, pageNo, out total);
        }

        /// <summary>
        /// 判断试题是否存在
        /// </summary>
        /// <param name="testPaperID">试卷定义ID</param>
        /// <param name="questionID">试题ID</param>
        /// <returns>true(存在)</returns>
        public bool IsQuestionExist(Guid testPaperID, Guid questionID)
        {
            return this.TestPaperRuleDao.IsQuestionExist(testPaperID, questionID);
        }
    }
}
