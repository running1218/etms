using System;
using System.Collections.Generic;
using ETMS.Components.Exam.API.Entity.ItemBank;
namespace ETMS.Components.Exam.Implement.DAL.ItemBank
{
    /// <summary>
    /// 试题查询数据访问接口
    /// </summary>
    public interface IQuestionSearchDao
    {
        /// <summary>
        /// 得到查询后的试题列表
        /// </summary>
        /// <param name="ownerID">当前用户ID</param>
        /// <param name="qs">查询实体</param>
        /// <param name="pageSize">页尺寸</param>
        /// <param name="pageNo">页码</param>
        /// <param name="total">总记录数</param>
        /// <returns>试题列表</returns>
        IList<QuestionSearchResult> GetQuestionList(Guid ownerID, QuestionSearch qs, int pageSize, int pageNo, out int total);

        /// <summary>
        /// 得到查询后的试题列表
        /// </summary>
        /// <param name="ownerID">当前用户ID</param>
        /// <param name="qs">查询实体</param>
        /// <param name="pageSize">页尺寸</param>
        /// <param name="pageNo">页码</param>
        /// <param name="total">总记录数</param>
        /// <returns>试题列表</returns>
        IList<QuestionSearchResult> GetQuestionListInCourse(Guid ownerID, QuestionSearch qs, int pageSize, int pageNo, out int total);

        /// <summary>
        /// 得到查询后的试题列表
        /// </summary>
        /// <param name="ownerID">当前用户ID</param>
        /// <param name="questionBankID">分类ID</param>
        /// <param name="pageSize">页尺寸</param>
        /// <param name="pageNo">页码</param>
        /// <param name="total">总记录数</param>
        /// <returns>试题列表</returns>
        IList<QuestionSearchResult> GetQuestionList(Guid ownerID, Guid questionBankID, int pageSize, int pageNo, out int total);
        
        /// <summary>
        /// 得到查询后的试题列表(知识点or标题查找)
        /// </summary>
        /// <param name="ownerID">当前用户ID</param>
        /// <param name="questionBankID">分类ID</param>
        /// <param name="pageSize">页尺寸</param>
        /// <param name="pageNo">页码</param>
        /// <param name="total">总记录数</param>
        /// <returns>试题列表</returns>
        IList<QuestionSearchResult> SearchQuestions(Guid ownerID, QuestionSearch qs, int pageSize, int pageNo, out int total);

        /// <summary>
        /// 得到查询后的试题列表
        /// </summary>
        /// <param name="qs">查询实体</param>
        /// <param name="pageSize">页尺寸</param>
        /// <param name="pageNo">页码</param>
        /// <param name="total">总记录数</param>
        /// <returns>试题列表</returns>
        IList<QuestionSearchResult> GetQuestionList(QuestionSearch qs, int pageSize, int pageNo, out int total);
    }
}
