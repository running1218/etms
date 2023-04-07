using System;
using System.Collections.Generic;
using ETMS.Components.Exam.API.Entity.Test;
using ETMS.Components.Exam.API.Entity.ItemBank;

namespace ETMS.Components.Exam.Implement.DAL.Test
{
    public interface IPaperQuestionDao
    {
        ///<summary>
        /// 得到指定试卷中各种类型的试题数。用于固定试卷，对于随机试卷没有用。
        ///</summary>
        /// <param name="testPaperID">试卷ID</param>
        IList<QuestionTypeCnt> GetQuestionTypeCntInPaper(Guid testPaperID);

        ///<summary>
        /// 更新试卷中指定类型的各试题的分数。只用于固定试卷，随机试卷对于题库中试题的更新采用另一个接口。
        ///</summary>
        /// <param name="testPaperID">试卷ID</param>
        /// <param name="questionType">要设置的试题类型</param>
        /// <param name="score">每一题的分数</param>
        void UpdateQuestionTypeScoreInPaper(Guid testPaperID, QuestionType questionType,
             decimal score);

        /// <summary>
        /// 更新指定试题的分数与序号
        /// </summary>
        /// <param name="testPaperID"></param>
        /// <param name="paperQuestionID"></param>
        /// <param name="score"></param>
        void UpdateQuestionScore(Guid testPaperID, Guid paperQuestionID, decimal score);
        void UpdateQuestionSequence(Guid testPaperID, Guid paperQuestionID, int order);
        ///<summary>
        /// 向试卷中添加一新的试题
        ///</summary>
        void AddQuestion(PaperQuestion paperQuestion);

        ///<summary>
        /// 从试卷中删除某一试题
        ///</summary>
        /// <param name="testPaperID">试卷ID</param>
        /// <param name="paperQuestionID">试卷试题ID</param>
        void Delete(Guid testPaperID, Guid paperQuestionID);

        ///<summary>
        /// 更新试卷中某一试题
        ///</summary>
        /// <param name="newPaperQuestion">替换的新试题</param>
        void Update(Guid oldQuestionID, PaperQuestion newPaperQuestion);

        ///<summary>
        /// 在试卷中查询指定类型的试题信息
        ///</summary>
        /// <param name="testPaperID">试卷ID</param>
        /// <param name="questionType">试题类型</param>
        IList<PaperQuestionView> FindQuestionView(Guid testPaperID, QuestionType questionType, int pageSize, int pageIndex, out int totalSize);

        ///<summary>
        /// 自动换题。简单-固定试卷（也可以用于高级-随机方式）中某一试题的自动换题。并得到换后试题的信息（题型、题目、难度、分数、序号等） 业务规则：同一试题分类、同一题型、同一难度中随机选择其它试题，并且选中的试题不能与原试题重复，也不能与同一试题中已存在的其它试题重复。
        ///</summary>
        /// <param name="testPaperID">试卷ID</param>
        /// <param name="srcPaperQuestionID">要被替换的试卷试题ID</param>
        Guid GetReplaceQuestionID(Guid testPaperID, QuestionType type, short difficulty, Guid questionBankID);
        void ReplaceQuestionID(Guid testPaperID, Guid oldQuestionID, Guid newQuestionID);
        PaperQuestionView GetNewQuestionView(Guid testPaperID, Guid questionID);

        ///<summary>
        /// 从题库中查询试题，指定分类、指定题型、指定难度等条件进行试题查询。
        ///</summary>
        ///<param name="existIDs">试卷中已选的试题IDs</param>
        /// <param name="ownerType">题库所属类型，个人或机构</param>
        /// <param name="ownerID">拥有者ID</param>
        /// <param name="difficulty">试题难度</param>
        /// <param name="questionTitle">题面查询条件</param>
        /// <param name="questionCategory">试题分类ID</param>
        /// <param name="questionType">试题类型</param>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageIndex"></param>
        /// <param name="totalCount"></param>
        IList<Question> FindTKQuestion(Guid ownerID,string existIDs, Guid? questionCategory, QuestionType questionType,
            int difficulty, string questionTitle, int pageSize, int pageIndex, out int totalCount);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="testPaperID"></param>
        /// <returns></returns>
        int GetTestPaperMaxQuestionIndex(Guid testPaperID);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="testPaperID"></param>
        /// <returns></returns>
        IList<Guid> GetQuestionIDsByTestPaperID(Guid testPaperID);
        IList<PaperQuestionView> GetQuestionViewList(Guid testPaperID);
        void UpdateTestpaperTotalQuantity(Guid testpaperID, int total, decimal score=100M);

        /// <summary>
        /// 删除试卷的所有试题
        /// </summary>
        /// <param name="testpaperID"></param>
        void TestPaperDeleteQuestion(Guid testpaperID);
    }
}
