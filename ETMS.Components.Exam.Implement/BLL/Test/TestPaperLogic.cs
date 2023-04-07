using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ETMS.Components.Exam.API.Entity.Test;
using ETMS.Components.Exam.API.Entity.ItemBank;

using Autumn.Context;
using Autumn.Objects.Factory;
using ETMS.Utility;
using ETMS.Components.Exam.Implement.DAL.Test;
using Autumn.Transaction.Interceptor;
using ETMS.Components.Exam.API.Entity;
using ETMS.Components.Exam.API.Interface.Test;
using ETMS.Components.Exam.API.Interface.ItemBank;
namespace ETMS.Components.Exam.Implement.BLL.Test
{
    public class TestPaperLogic : ITestPaperLogic, IMessageSourceAware, IInitializingObject
    {
        #region 注释代码
        //#region Base 接口
        ///// <summary>
        ///// 1、添加
        ///// </summary>
        ///// <param name="testPaper">试卷实体</param>
        //public Guid Add(TestPaper testPaper)
        //{
            
        //}
        ///// <summary>
        ///// 2、修改
        ///// </summary>
        ///// <param name="testPaper">试卷实体</param>
        //public void Update(TestPaper testPaper)
        //{
        //    throw new NotImplementedException();
        //}
        ///// <summary>
        ///// 3、删除
        ///// </summary>
        ///// <param name="testPaperID">试卷ID</param>
        //public void Delete(Guid testPaperID)
        //{
        //    throw new NotImplementedException();
        //}
        ///// <summary>
        ///// 4、获取
        ///// </summary>
        ///// <param name="testPaperID">试卷ID</param>
        ///// <returns>试卷实体</returns>
        //public TestPaper GetById(Guid testPaperID)
        //{
        //    throw new NotImplementedException();
        //}
        //#endregion

        //#region Extend 接口
        ///// <summary>
        ///// 根据试卷ID列表获取对应的试卷信息列表
        ///// 使用场景：根据课程下的试卷IDs取课程对应的试卷列表
        ///// </summary>
        ///// <param name="testPaperIDs">试卷IDs</param>
        ///// <returns>试卷列表</returns>
        //public IList<TestPaper> GetTestPaperByIDs(IList<Guid> testPaperIDs)
        //{
        //    throw new NotImplementedException();
        //}
        ///// <summary>
        ///// 获取试卷的试题个数
        ///// </summary>
        ///// <param name="testPaperID">试卷ID</param>
        ///// <returns>试题个数</returns>
        //public int GetQuestionCount(Guid testPaperID)
        //{
        //    throw new NotImplementedException();
        //}
        ///// <summary>
        ///// 获取试卷的试题列表（不分页）
        ///// </summary>
        ///// <param name="testPaperID">试卷ID</param>
        ///// <returns>试题列表</returns>
        //public IList<TestPaperToQuestion> GetTestPaperQuestions(Guid testPaperID)
        //{
        //    throw new NotImplementedException();
        //}
        ///// <summary>
        ///// 转换试卷试题顺序
        ///// 限制：1同题型的可以上下调整顺序
        ///// </summary>
        ///// <param name="testPaperID">试卷ID</param>
        ///// <param name="upQuestionID">上面的试题ID</param>
        ///// <param name="downQuestionID">下面的试题ID</param>
        //public void SwitchTestPaperQuestionIndex(Guid testPaperID, Guid upQuestionID, Guid downQuestionID)
        //{
        //    throw new NotImplementedException();
        //}
        ///// <summary>
        ///// 设置试卷试题分数
        ///// </summary>
        ///// <param name="testPaperID">试卷ID</param>
        ///// <param name="questionID">试题ID</param>
        ///// <param name="score">分数</param>
        //public void SetTestPaperQuestionScore(Guid testPaperID, Guid questionID, decimal score)
        //{
        //    throw new NotImplementedException();
        //}
        ///// <summary>
        ///// 删除选中的试卷试题
        ///// </summary>
        ///// <param name="testPaperID">试卷ID</param>
        ///// <param name="questionIDs">试题ID集合</param>
        //public void DeleteTestPaperQuestions(Guid testPaperID, IList<Guid> questionIDs)
        //{
        //    throw new NotImplementedException();
        //}
        //#endregion

        //#region 选择已有试题
        ///// <summary>
        ///// 查询试题
        ///// </summary>
        ///// <param name="filter">查询条件</param>
        ///// <param name="pageIndex">页号</param>
        ///// <param name="pageSize">页面大小</param>
        ///// <param name="totalRecord">输出，总记录数</param>
        ///// <returns>Question列表</returns>
        //public IList<Question> QueryQuestionListByFilter(Question filter, int pageIndex, int pageSize, out int totalRecord)
        //{
        //    throw new NotImplementedException();
        //}
        ///// <summary>
        ///// 保存选中的试题到试卷试题定义表
        ///// </summary>
        ///// <param name="testPaperID">试卷ID</param>
        ///// <param name="selectedOptionIDs">选中的试题ID</param>
        //public void SaveSelectedOptions(Guid testPaperID, IList<Guid> selectedOptionIDs)
        //{
        //    throw new NotImplementedException();
        //}
        //#endregion

        //#region 设置试卷信息和反馈信息
        ///// <summary>
        ///// 异步获取试卷总分，判断设置的值是否超过试卷总分
        ///// </summary>
        ///// <param name="testPaperID">试卷ID</param>
        ///// <returns>试卷总分</returns>
        //public decimal GetTestPaperTotalScore(Guid testPaperID)
        //{
        //    throw new NotImplementedException();
        //}
        ///// <summary>
        ///// 保存设置的试卷信息
        ///// 注意1、包括试卷反馈信息（反馈的分值区间不能小于0，大于试卷总分）
        ///// 2、修改反馈信息时是先删除后添加
        ///// </summary>
        ///// <param name="testPaper">试卷信息试题，包括反馈的设置信息</param>
        ///// <returns>试卷ID</returns>
        //public Guid SaveTestPaperInfo(TestPaper testPaper)
        //{
        //    throw new NotImplementedException();
        //}
        ///// <summary>
        ///// 获取试卷信息，包括反馈设置信息
        ///// </summary>
        ///// <param name="testPaperID">试卷ID</param>
        ///// <returns>试卷实体</returns>
        //public TestPaper GetTestPaperByID(Guid testPaperID)
        //{
        //    throw new NotImplementedException();
        //}
        //#endregion

        //#region 课程发布
        ///// <summary>
        ///// 课程发布时，对课程中引用试题的复制
        ///// </summary>
        ///// <param name="testPaperIDs">课程包括的试卷ID集合</param>
        //public void ReleaseTestPaperQuestions(IList<Guid> testPaperIDs)
        //{
        //    throw new NotImplementedException();
        //}
        //#endregion
        #endregion

        public ITestPaperDao TestPaperDao { get; set; }
        public ITestFeedbackServiceLogic TestFeedbackLogic { get; set; } 
        // 选项服务
        public IOptionGroupServiceLogic OptionGroupLogic { get; set; }
        public IOptionServiceLogic OptionLogic { get; set; }
        public ITestPaperRuleLogic TestPaperRuleLogic { get; set; }

        #region ITestPaperLogic 成员
        public Guid AddTestPaper(TestPaper testPaper)
        {
            if (testPaper.ID == Guid.Empty)
                testPaper.TestPaperID = Guid.NewGuid();
            testPaper.IsDelete = false;
            testPaper.CreatedUserID = ETMS.AppContext.UserContext.Current.UserID;
            testPaper.Status = TestStatus.Waiting;
            this.TestPaperDao.AddTestPaper(testPaper);
             
            return testPaper.TestPaperID;
        }

        public void Update(Guid testPaperID, TestPaper newTestPaper)
        {
            newTestPaper.TestPaperID = testPaperID;
            newTestPaper.CreatedUserID = ETMS.AppContext.UserContext.Current.UserID;
            newTestPaper.Status = TestStatus.Waiting;
            // 修改试卷
            this.TestPaperDao.Update(newTestPaper); 
        }

        public void Delete(Guid testPaperID)
        {
            this.TestPaperDao.Delete(testPaperID);
        }

        public TestPaper GetByID(Guid testPaperID)
        {
            TestPaper paper = (TestPaper)CacheHelper.Get(testPaperID.ToString());
            if (paper == null)
            {
                paper = this.TestPaperDao.GetByID(testPaperID);
                CacheHelper.Add(testPaperID.ToString(), paper, new TimeSpan(0, 30, 0));
            }
            paper.Feedbacks = this.TestFeedbackLogic.GetFeedbacksInTestPaper(testPaperID);
            return paper;
        }

        public void UpdateQuestionsCount(Guid testPaperID, int questionsCnt, int totalScore)
        {
            this.TestPaperDao.UpdateQuestionsCount(testPaperID, questionsCnt, totalScore);
        }

        public void UpdateExamTimes(Guid testPaperID, int jige, int examTime, int examTimes)
        {
            decimal d = this.TestPaperDao.GetByID(testPaperID).TotalScore;
            if (jige > d)
            {
                throw new ETMS.AppContext.BusinessException("Exam.TestPaper.PassScoreOutlaw");
            }
            this.TestPaperDao.UpdateExamTimes(testPaperID, jige, examTime, examTimes);
        }

        public void CopyTKQuestionData(Guid testPaperID)
        {
            this.TestPaperDao.CopyTKQuestionData(testPaperID);
        }

        [Transaction()]
        public void DeleteBatchTestPaper(IList<Guid> ids)
        {
            foreach (Guid id in ids)
            {
                this.TestPaperDao.Delete(id);
            }
        }

        public IList<TestSearchResult> GetTestPaperListByOperator(TestSearch search, int pageSize, int pageNo, out int total)
        {
            IList<TestSearchResult> result = new List<TestSearchResult>();
            StringBuilder sqlWhere = new StringBuilder(" a.IsDelete=0");
            if (!string.IsNullOrEmpty(search.TestPaperName))
            {
                sqlWhere.AppendFormat(" and a.TestPaperName like '%{0}%'", search.TestPaperName.ToSafeSQLValue());
            }
            if (!string.IsNullOrEmpty(search.KeyWord))
            {
                sqlWhere.AppendFormat(" and a.TestPaperDesc like '%{0}%'", search.KeyWord.ToSafeSQLValue());
            }
            if (search.TestPaperType != TestPaperType.Null)
            {
                sqlWhere.AppendFormat(" and a.TestPaperType={0}", Convert.ToInt16(search.TestPaperType));
            }
            if (!string.IsNullOrEmpty(search.OwnerName))
            {
                sqlWhere.AppendFormat(" and c.OwnerName like '%{0}%'", search.OwnerName.ToSafeSQLValue());
            }
            if (search.AuditStatus != AuditType.Null)
            {
                sqlWhere.AppendFormat(" and a.Status = {0}", Convert.ToInt32(search.AuditStatus));
            }
            if (search.ShareStatus != ShareType.Null)
            {
                sqlWhere.AppendFormat(" and a.ShareStatus = {0}", Convert.ToInt32(search.ShareStatus));
            }
            // 获取试卷信息
            IList<TestPaper> testPaperList = this.TestPaperDao.GetTestPaperListByOperator(sqlWhere.ToString(), pageSize, pageNo, out total);
            IList<Guid> testIDs = (from e in testPaperList where e.TestPaperID != Guid.Empty select e.TestPaperID).ToList<Guid>(); 
            
            return result;
        }

        public IList<TestSearchResult> GetMyTestPaperList(Guid ownerID, TestSearch search, int pageSize, int pageNo, out int total)
        {
            IList<TestSearchResult> result = new List<TestSearchResult>();
            StringBuilder sqlWhere = new StringBuilder(" a.IsDelete=0");
            if (ownerID != Guid.Empty)
            {
                sqlWhere.AppendFormat(" and b.OwnerID='{0}'", ownerID);
            }
            if (search.CategoryID != Guid.Empty)
            {
                sqlWhere.AppendFormat(" and b.CategoryID='{0}'", search.CategoryID);
            }
            if (!string.IsNullOrEmpty(search.TestPaperName) && !string.IsNullOrEmpty(search.KeyWord))
            {
                sqlWhere.AppendFormat(" and (a.TestPaperName like '%{0}%' or a.TestPaperDesc like '%{1}%') ", search.TestPaperName.ToSafeSQLValue(), search.KeyWord.ToSafeSQLValue());
            }
            else
            {
                if (!string.IsNullOrEmpty(search.TestPaperName))
                {
                    sqlWhere.AppendFormat(" and a.TestPaperName like '%{0}%'", search.TestPaperName.ToSafeSQLValue());
                }
                else if (!string.IsNullOrEmpty(search.KeyWord))
                {
                    sqlWhere.AppendFormat(" and a.TestPaperDesc like '%{0}%'", search.KeyWord.ToSafeSQLValue());
                }
            }
            if (search.TestPaperType != TestPaperType.Null)
            {
                sqlWhere.AppendFormat(" and a.TestPaperType={0}", Convert.ToInt16(search.TestPaperType));
            }
            if (search.AuditStatus != AuditType.Null)
            {
                sqlWhere.AppendFormat(" and a.Status = {0}", Convert.ToInt32(search.AuditStatus));
            }
            if (search.ShareStatus != ShareType.Null)
            {
                sqlWhere.AppendFormat(" and a.ShareStatus = {0}", Convert.ToInt32(search.ShareStatus));
            }
            // 获取试卷信息
            IList<TestPaper> testPaperList = this.TestPaperDao.GetTestPaperListByOperator(sqlWhere.ToString(), pageSize, pageNo, out total);
            IList<Guid> testIDs = (from e in testPaperList where e.TestPaperID != Guid.Empty select e.TestPaperID).ToList<Guid>();
             
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="testPaperID"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public IList<TestPaperUnit> GetTestPaperSchema(Guid testPaperID, TestPaperType type)
        {
            //throw new NotImplementedException();
            IList<TestPaperUnit> units = new List<TestPaperUnit>();
            if (type == TestPaperType.AdvancedRandom)
            {
                units = this.TestPaperDao.GetSeniorRandomTestpaperSchema(testPaperID);
                GetUnits(testPaperID, units, true);
            }
            else
            {
                units = this.TestPaperDao.GetCommonTestpaperSchema(testPaperID);
                GetUnits(testPaperID, units, false);
            }
            return units;
        }

        private void GetUnits(Guid testPaperID, IList<TestPaperUnit> units, bool isAdvanced)
        {
            foreach (TestPaperUnit item in units)
            {
                decimal score = (item.QuestionCount == 0) ? 0M : item.QuestionScoreSum / item.QuestionCount;
                switch (item.QuestionType)
                {
                    case QuestionType.SingleChoice:
                    case QuestionType.MultipleChoice:
                    case QuestionType.Judgement:
                        item.QuestionList = GetOptionQuestions(testPaperID, item.QuestionType, score, isAdvanced);
                        break;
                    case QuestionType.ExtendedText:
                    case QuestionType.TextEntry:
                        item.QuestionList = GetTextQuestions(testPaperID, item.QuestionType, score, isAdvanced);
                        break;
                    case QuestionType.Match:
                    case QuestionType.Group:
                        item.QuestionList = GetOptionGroupQuestions(testPaperID, item.QuestionType, score, isAdvanced);
                        break;
                    default:
                        break;
                }
            }
        }

        private IList<BasePriview> GetOptionQuestions(Guid testPaperID, QuestionType type, decimal score, bool isAdvanced)
        {
            IList<BasePriview> result = new List<BasePriview>();
            int t = Convert.ToInt32(type);
            IList<IDName> list = null;
            if (isAdvanced)
                list = this.TestPaperRuleLogic.ReturnAdvancedExam(testPaperID, type);
            else
                list = this.TestPaperDao.GetQuestionByType(testPaperID, t);
            foreach (IDName q in list)
            {
                BasePriview pp = new BasePriview();
                IList<QuestionOption> options = this.OptionLogic.LoadAllInQuestion(q.ID);
                pp.QuestionID = q.ID;
                pp.QuestionName = q.Name;
                pp.QuestionScore = score;
                pp.QuestionOptions = options;
                result.Add(pp);
            }
            return result;
        }

        private IList<BasePriview> GetTextQuestions(Guid testPaperID, QuestionType type, decimal score, bool isAdvanced)
        {
            IList<BasePriview> result = new List<BasePriview>();
            int t = Convert.ToInt32(type);
            IList<IDName> list = null;
            if (isAdvanced)
                list = this.TestPaperRuleLogic.ReturnAdvancedExam(testPaperID, type);
            else
                list = this.TestPaperDao.GetQuestionByType(testPaperID, t);
            foreach (IDName q in list)
            {
                BasePriview pp = new BasePriview();
                pp.QuestionID = q.ID;
                pp.QuestionName = q.Name;
                pp.QuestionScore = score;
                result.Add(pp);
            }
            return result;
        }

        private IList<BasePriview> GetOptionGroupQuestions(Guid testPaperID, QuestionType type, decimal score, bool isAdvanced)
        {
            IList<BasePriview> result = new List<BasePriview>();
            int t = Convert.ToInt32(type);
            IList<IDName> list = null;
            if (isAdvanced)
                list = this.TestPaperRuleLogic.ReturnAdvancedExam(testPaperID, type);
            else
                list = this.TestPaperDao.GetQuestionByType(testPaperID, t);
            foreach (IDName q in list)
            {
                BasePriview pp = new BasePriview();
                IList<OptionGroupItem> GroupItems = this.OptionGroupLogic.LoadAllInQuestion(q.ID);
                pp.QuestionID = q.ID;
                pp.QuestionName = q.Name;
                pp.QuestionScore = score;
                pp.OptionGroups = GroupItems;
                result.Add(pp);
            }
            return result;
        }


        public void SetShareState(Guid testpaperID, EnumShareStatus state)
        {
            if (state != EnumShareStatus.Null)
                this.TestPaperDao.SetShareState(testpaperID, Convert.ToInt32(state));
        }

        public void SetAuditState(Guid testpaperID, TestStatus state)
        {
            if (state != TestStatus.Null)
                this.TestPaperDao.SetAuditState(testpaperID, Convert.ToInt32(state));
        }

        public void SetFixTestPaperScoreAndCount(Guid testpaperID)
        {
            if (testpaperID == Guid.Empty)
                throw new ETMS.AppContext.BusinessException("System.Invalid.EmptyParameter");
            this.TestPaperDao.SetFixTestPaperScoreAndCount(testpaperID);
        }

        /// <summary>
        /// 将试卷移动至别的分类
        /// </summary>
        /// <param name="testpaperIDs">试卷ID列表</param>
        /// <param name="categoryID">分类ID</param>
        public void SetTestPaperCategoryID(IList<Guid> testpaperIDs, Guid categoryID)
        {
            string strIDs = string.Format("'{0}'", Autumn.Util.StringUtils.EnumerableToCommaDelimitedString(testpaperIDs).Replace(",", "','"));
            this.TestPaperDao.SetTestPaperCategoryID(strIDs, categoryID);
        }
        #endregion

        #region IInitializingObject 成员

        public void AfterPropertiesSet()
        {
            if (this.TestPaperDao == null)
                throw new Exception("please set QuestionDao Property First!");
        }

        #endregion

        #region IMessageSourceAware 成员

        public IMessageSource MessageSource { get; set; }

        #endregion
    }
}
