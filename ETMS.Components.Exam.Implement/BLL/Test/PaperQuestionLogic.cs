﻿using System;
using System.Collections.Generic;

using Autumn.Context;
using Autumn.Objects.Factory;
using ETMS.Components.Exam.API.Entity.Test;
using ETMS.Components.Exam.API.Entity.ItemBank;
using ETMS.Components.Exam.Implement.DAL.Test;
using ETMS.Components.Exam.API.Interface.Test;
using ETMS.Components.Exam.API.Interface.ItemBank;
namespace ETMS.Components.Exam.Implement.BLL.Test
{
    public class PaperQuestionLogic : IPaperQuestionLogic, IMessageSourceAware, IInitializingObject
    {
        public IPaperQuestionDao PaperQuestionDao { get; set; }
        public ITestPaperLogic TestPaperLogic { get; set; }
        public IQuestionLogic QuestionLogic { get; set; }

        #region IPaperQuestionLogic 方法
        ///<summary>
        /// 得到指定试卷中各种类型的试题数。用于固定试卷，对于随机试卷没有用。
        ///</summary>
        /// <param name="testPaperID">试卷ID</param>
        public IList<QuestionTypeCnt> GetQuestionTypeCntInPaper(Guid testPaperID)
        {
            return this.PaperQuestionDao.GetQuestionTypeCntInPaper(testPaperID);
        }
        ///<summary>
        /// 更新试卷中指定类型的各试题的分数。只用于固定试卷，随机试卷对于题库中试题的更新采用另一个接口。
        ///</summary>
        /// <param name="testPaperID">试卷ID</param>
        /// <param name="questionType">要设置的试题类型</param>
        /// <param name="score">每一题的分数</param>
        public void UpdateQuestionTypeScoreInPaper(Guid testPaperID, QuestionType questionType, decimal score)
        {
            this.PaperQuestionDao.UpdateQuestionTypeScoreInPaper(testPaperID, questionType, score);
        }
        ///<summary>
        /// 更新指定试题的分数与序号
        ///</summary>
        /// <param name="testPaperID">试卷ID</param>
        /// <param name="paperQuestionID">试题在试卷中的ID</param>
        /// <param name="score">分数</param>
        public void UpdateQuestionScore(Guid testPaperID, Guid questionID, decimal score)
        {
            this.PaperQuestionDao.UpdateQuestionScore(testPaperID, questionID, score);
        }

        ///<summary>
        /// 更新指定试题的分数与序号
        ///</summary>
        /// <param name="testPaperID">试卷ID</param>
        /// <param name="paperQuestionID">试题在试卷中的ID</param>
        /// <param name="order">序号</param>
        public void UpdateQuestionSequence(Guid testPaperID, Guid questionID, int order)
        {
            this.PaperQuestionDao.UpdateQuestionSequence(testPaperID, questionID, order);
        }
        ///<summary>
        /// 向试卷中添加一新的试题
        ///</summary>
        /// <param name="paperQuestion">要添加的试题信息</param>
        public Guid AddQuestion(PaperQuestion paperQuestion)
        {
            paperQuestion.CreatedUserID = ETMS.AppContext.UserContext.Current.UserID;
            this.PaperQuestionDao.AddQuestion(paperQuestion);
            return paperQuestion.QuestionID;
        }
        ///<summary>
        /// 从试卷中删除某一试题
        ///</summary>
        /// <param name="testPaperID">试卷ID</param>
        /// <param name="paperQuestionID">试卷试题ID</param>
        public void Delete(Guid testPaperID, Guid paperQuestionID)
        {
            //删除试卷和试题的关系
            this.PaperQuestionDao.Delete(testPaperID, paperQuestionID);
        }
        ///<summary>
        /// 更新试卷中某一试题
        ///</summary>
        /// <param name="testPaperID">试卷ID</param>
        /// <param name="paperQuestionID">要替换的试题ID</param>
        /// <param name="newPaperQuestion">替换的新试题</param>
        public void Update(Guid testPaperID, Guid paperQuestionID, PaperQuestion newPaperQuestion)
        {
            //this.PaperQuestionDao.Delete(testPaperID, paperQuestionID);
            newPaperQuestion.TestPaperID = testPaperID;
            this.PaperQuestionDao.Update(paperQuestionID, newPaperQuestion);
        }
        ///<summary>
        /// 在试卷中查询指定类型的试题信息  edit zhangsz2013-09-03  添加缓存
        ///</summary>
        /// <param name="testPaperID">试卷ID</param>
        /// <param name="questionType">试题类型</param>
        public IList<PaperQuestionView> FindQuestionView(Guid testPaperID, QuestionType questionType, int pageSize, int pageIndex, out int totalSize)
        {
            string key = string.Format("{0}_{1}_{2}_{3}"
                , testPaperID.ToString()
                , questionType != null ? questionType.ToString() : "0"
                , pageSize.ToString()
                , pageIndex.ToString());

            IList<PaperQuestionView> paperQuestionView = new List<PaperQuestionView>();
            //paperQuestionView = (List<PaperQuestionView>)CacheHelper.Get(key);
            if (paperQuestionView == null || paperQuestionView.Count == 0)
            {
                paperQuestionView = this.PaperQuestionDao.FindQuestionView(testPaperID, questionType, pageSize, pageIndex, out totalSize);
                //CacheHelper.Add(key, paperQuestionView, new TimeSpan(0, 30, 0));
            }
            else
            {
                totalSize = paperQuestionView.Count;
            }

            return paperQuestionView;
        }
        ///<summary>
        /// 自动换题。简单-固定试卷（也可以用于高级-随机方式）中某一试题的自动换题。并得到换后试题的信息（题型、题目、难度、分数、序号等） 业务规则：同一试题分类、同一题型、同一难度中随机选择其它试题，并且选中的试题不能与原试题重复，也不能与同一试题中已存在的其它试题重复。
        ///</summary>
        /// <param name="testPaperID">试卷ID</param>
        /// <param name="srcPaperQuestionID">要被替换的试卷试题ID</param>
        public PaperQuestionView ReplaceQuestion(Guid testPaperID, Guid srcPaperQuestionID)
        {
            // 1、取得试题的试题分类、题型、难度
            Question src = this.QuestionLogic.GetByID(srcPaperQuestionID);
            // 2、取得要换的题
            Guid newID = this.PaperQuestionDao.GetReplaceQuestionID(testPaperID, src.QuestionType, src.Difficulty, src.QuestionBankID);
            // 3、更新试卷-试题关系表
            if (newID == Guid.Empty)  // 没有可以替换的试题，返回原试题ID
                newID = srcPaperQuestionID;
            else
                this.PaperQuestionDao.ReplaceQuestionID(testPaperID, srcPaperQuestionID, newID);
            // 4、获取试题信息
            return this.PaperQuestionDao.GetNewQuestionView(testPaperID, newID);
        }
        ///<summary>
        /// 检查所定义的试卷是否完整。需要进行多方面的检查，比如：检查试卷总分与及格分，试卷总分与试卷反馈中指定的分数、检查试题是否合法等。
        ///</summary>
        /// <param name="testPaperID">要检查的试卷ID</param>
        /// <param name="nError">检查未通过时的错误类型ID</param>
        /// <param name="sErrMsg">错误描述</param>
        public bool ValidateTestPaper(Guid testPaperID)
        {
            //1、获取试卷的基本信息
            TestPaper tp = this.TestPaperLogic.GetByID(testPaperID);
            //2、检查试卷总分和及格分数
            if (tp.TotalScore <= 0)
                throw new ETMS.AppContext.BusinessException("试卷的总分要大于零，请重新设置！");
            if (tp.PassedScore > tp.TotalScore)
                throw new ETMS.AppContext.BusinessException("及格分数不能大于试卷总分，请重新设置！");
            //3、检查考卷总分与试卷反馈中制定的分数
            if (tp.Feedbacks.Count > 0)
            {
                foreach (TestFeedback item in tp.Feedbacks)
                {
                    if (item.EndScore > tp.TotalScore)
                        throw new ETMS.AppContext.BusinessException("评价分数段的分数不能大于试卷总分！");
                }
            }

            return true;
        }
        ///<summary>
        /// 从题库中查询试题，指定分类、指定题型、指定难度等条件进行试题查询。
        ///</summary>
        /// <param name="ownerType">题库所属类型，个人或机构</param>
        /// <param name="ownerID">拥有者ID</param>
        /// <param name="difficulty">试题难度</param>
        /// <param name="questionTitle">题面查询条件</param>
        /// <param name="questionCategory">试题分类ID</param>
        /// <param name="questionType">试题类型</param>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageIndex"></param>
        /// <param name="totalCount"></param>
        public IList<Question> FindTKQuestion(Guid ownerID, Guid testPaperID, Guid questionCategory, QuestionType questionType,
           int difficulty, string questionTitle, int pageSize, int pageIndex, out int totalCount)
        {
            string strIDs = string.Empty;
            IList<Guid> list = this.PaperQuestionDao.GetQuestionIDsByTestPaperID(testPaperID);
            if (list.Count > 0)
            {
                strIDs = string.Format("'{0}'", Autumn.Util.StringUtils.EnumerableToCommaDelimitedString(list).Replace(",", "','"));
            }
            return this.PaperQuestionDao.FindTKQuestion(ownerID, strIDs, questionCategory, questionType, difficulty, questionTitle, pageSize, pageIndex, out totalCount);
        }
        //     public IList<Question> FindTKQuestion(Guid testPaperID, Guid questionCategory, QuestionType questionType,
        //int difficulty, string questionTitle, int pageSize, int pageIndex, out int totalCount)
        //     {
        //         return FindTKQuestion(Guid.Empty, testPaperID, questionCategory, questionType,difficulty,
        //             questionTitle, pageSize, pageIndex, out totalCount);
        //     }

        /// <summary>
        /// 添加选择的试题
        /// </summary>
        /// <param name="testPaperID">试卷ID</param>
        /// <param name="questionList">题目列表</returns>
        public void AddQuestionToTestPaper(Guid testPaperID, IList<KeyValuePair<Guid, QuestionType>> questionList)
        {
            int index = this.PaperQuestionDao.GetTestPaperMaxQuestionIndex(testPaperID);
            foreach (var pair in questionList)
            {
                index++;
                PaperQuestion pq = new PaperQuestion(testPaperID, pair.Key);
                pq.CreatedUserID = ETMS.AppContext.UserContext.Current.UserID;
                pq.ItemSequence = index;
                pq.QuestionType = pair.Value;
                this.PaperQuestionDao.AddQuestion(pq);
            }
        }
        /// <summary>
        /// 列出试卷中已添加的试题列表
        /// </summary>
        /// <param name="testPaperID">试卷ID</param>
        /// <param name="totalCount">试题数目</param>
        /// <returns></returns>
        public IList<PaperQuestionView> GetQuestionViewList(Guid testPaperID, out int totalCount)
        {
            IList<PaperQuestionView> list = this.PaperQuestionDao.GetQuestionViewList(testPaperID);
            totalCount = list.Count;
            // 更新试卷试题总数
            //this.PaperQuestionDao.UpdateTestpaperTotalQuantity(testPaperID, totalCount);
            return list;
        }
        /// <summary>
        /// 根据试卷ID获取试卷下的试题ID列表
        /// </summary>
        /// <param name="testPaperID"></param>
        /// <returns></returns>
        public IList<Guid> GetQuestionIDsByTestPaperID(Guid testPaperID)
        {
            return this.PaperQuestionDao.GetQuestionIDsByTestPaperID(testPaperID);
        }

        public void UpdateTestpaperTotalQuantity(Guid testpaperID, int total, decimal score = 100M)
        {
            this.PaperQuestionDao.UpdateTestpaperTotalQuantity(testpaperID, total, score);
        }

        public void TestPaperDeleteQuestion(Guid testpaperID)
        {
            this.PaperQuestionDao.TestPaperDeleteQuestion(testpaperID);
        }
        #endregion

        #region IInitializingObject 成员

        public void AfterPropertiesSet()
        {
            if (this.PaperQuestionDao == null)
                throw new Exception("please set PaperQuestionDao Property First!");
        }

        #endregion

        #region IMessageSourceAware 成员

        public IMessageSource MessageSource { get; set; }

        #endregion
    }
}
