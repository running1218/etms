// File:    IPaperQuestionLogic.cs
// Author:  Administrator
// Created: 2012年1月12日 17:24:36
// Purpose: Definition of Interface IPaperQuestionLogic

using System;
using System.Collections.Generic;
using ETMS.Components.Exam.API.Entity.Test;
using ETMS.Components.Exam.API.Entity.ItemBank;
using System.ServiceModel;

///<summary>
/// 测评
///</summary>
namespace ETMS.Components.Exam.API.Interface.Test
{
    ///<summary>
    /// 试卷试题相关功能
    ///</summary>
    [ServiceContract(Namespace = "http://Autumn.Business.LMS.Interface.Test/IPaperQuestionLogic")]
    public interface IPaperQuestionLogic
    {
        ///<summary>
        /// 得到指定试卷中各种类型的试题数。用于固定试卷，对于随机试卷没有用。
        ///</summary>
        /// <param name="testPaperID">试卷ID</param>
        [OperationContract]
        IList<QuestionTypeCnt> GetQuestionTypeCntInPaper(Guid testPaperID);

        ///<summary>
        /// 更新试卷中指定类型的各试题的分数。只用于固定试卷，随机试卷对于题库中试题的更新采用另一个接口。
        ///</summary>
        /// <param name="testPaperID">试卷ID</param>
        /// <param name="questionType">要设置的试题类型</param>
        /// <param name="score">每一题的分数</param>
        [OperationContract]
        void UpdateQuestionTypeScoreInPaper(Guid testPaperID, QuestionType questionType, decimal score);

        ///<summary>
        /// 更新指定试题的分数
        ///</summary>
        /// <param name="testPaperID">试卷ID</param>
        /// <param name="paperQuestionID">试题在试卷中的ID</param>
        /// <param name="score">分数</param>
        [OperationContract]
        void UpdateQuestionScore(Guid testPaperID, Guid questionID, decimal score);
        /// <summary>
        /// 更新指定试题的序号
        /// </summary>
        /// <param name="testPaperID">试卷ID</param>
        /// <param name="questionID">试题在试卷中的ID</param>
        /// <param name="order">序号</param>
        [OperationContract]
        void UpdateQuestionSequence(Guid testPaperID, Guid questionID, int order);
        ///<summary>
        /// 向试卷中添加一新的试题
        ///</summary>
        /// <param name="paperQuestion">要添加的试题信息</param>
        [OperationContract]
        Guid AddQuestion(PaperQuestion paperQuestion);

        ///<summary>
        /// 从试卷中删除某一试题
        ///</summary>
        /// <param name="testPaperID">试卷ID</param>
        /// <param name="paperQuestionID">试卷试题ID</param>
        [OperationContract]
        void Delete(Guid testPaperID, Guid paperQuestionID);

        ///<summary>
        /// 更新试卷中某一试题
        ///</summary>
        /// <param name="testPaperID">试卷ID</param>
        /// <param name="paperQuestionID">要替换的试题ID</param>
        /// <param name="newPaperQuestion">替换的新试题</param>
        [OperationContract]
        void Update(Guid testPaperID, Guid paperQuestionID, PaperQuestion newPaperQuestion);

        ///<summary>
        /// 在试卷中查询指定类型的试题信息
        ///</summary>
        /// <param name="testPaperID">试卷ID</param>
        /// <param name="questionType">试题类型</param>
        [OperationContract]
        IList<PaperQuestionView> FindQuestionView(Guid testPaperID, QuestionType questionType, int pageSize, int pageIndex, out int totalSize);

        ///<summary>
        /// 自动换题。高集-固定试卷（也可以用于高级-随机方式）中某一试题的自动换题。
        /// 并得到换后试题的信息（题型、题目、难度、分数、序号等） 业务规则：
        /// 同一试题分类、同一题型、同一难度中随机选择其它试题，
        /// 并且选中的试题不能与原试题重复，也不能与同一试题中已存在的其它试题重复。
        ///</summary>
        /// <param name="testPaperID">试卷ID</param>
        /// <param name="srcPaperQuestionID">要被替换的试卷试题ID</param>
        [OperationContract]
        PaperQuestionView ReplaceQuestion(Guid testPaperID, Guid srcPaperQuestionID);

        ///<summary>
        /// 检查所定义的试卷是否完整。需要进行多方面的检查，
        /// 比如：检查试卷总分与及格分，试卷总分与试卷反馈中指定的分数、检查试题是否合法等。
        ///</summary>
        /// <param name="testPaperID">要检查的试卷ID</param>
        [OperationContract]
        bool ValidateTestPaper(Guid testPaperID);
        
        /// <summary>
        /// 从题库中查询试题，指定分类、指定题型、指定难度等条件进行试题查询。
        /// </summary>
        /// <param name="testPaperID">试卷ID(过滤试卷中已有的试题)</param>
        /// <param name="questionCategory">试题分类ID(Guid.Empty为全部)</param>
        /// <param name="questionType">试题类型(Null为全部)</param>
        /// <param name="difficulty">试题难度(0为全部)</param>
        /// <param name="questionTitle">题目</param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="totalCount"></param>
        /// <returns>试题ID：QuestionID，题型：QuestionType，题目：QuestionTitle，试题分类名称：QuestionBankName，难度：Difficulty，适用对象：ObjectID，学科：Subject</returns>
        [OperationContract]
        IList<Question> FindTKQuestion(Guid ownerID,Guid testPaperID, Guid questionCategory, QuestionType questionType,
           int difficulty, string questionTitle, int pageSize, int pageIndex, out int totalCount);

        //[OperationContract]
        //IList<Question> FindTKQuestion(Guid testPaperID, Guid questionCategory, QuestionType questionType,
        //   int difficulty, string questionTitle, int pageSize, int pageIndex, out int totalCount);
        /// <summary>
        /// 添加选择的试题
        /// </summary>
        /// <param name="testPaperID">试卷ID</param>
        /// <param name="questionList">试题ID和试题类型列表</param>
        [OperationContract]
        void AddQuestionToTestPaper(Guid testPaperID, IList<KeyValuePair<Guid, QuestionType>> questionList);
        
        /// <summary>
        /// 列出试卷中已添加的试题列表
        /// </summary>
        /// <param name="testPaperID">试卷ID</param>
        /// <param name="totalCount">试题数目</param>
        /// <returns></returns>
        [OperationContract]
        IList<PaperQuestionView> GetQuestionViewList(Guid testPaperID, out int totalCount);

        /// <summary>
        /// 根据试卷ID获取试卷下的试题ID列表
        /// </summary>
        /// <param name="testPaperID"></param>
        /// <returns></returns>
        [OperationContract]
        IList<Guid> GetQuestionIDsByTestPaperID(Guid testPaperID);

        /// <summary>
        /// 更新试卷的试题数量
        /// </summary>
        /// <param name="testpaperID"></param>
        /// <param name="total"></param>
        [OperationContract]
        void UpdateTestpaperTotalQuantity(Guid testpaperID, int total, decimal score=100M);

        /// <summary>
        /// 删除试卷的所有试题
        /// </summary>
        /// <param name="testpaperID"></param>
        [OperationContract]
        void TestPaperDeleteQuestion(Guid testpaperID);
    }
}