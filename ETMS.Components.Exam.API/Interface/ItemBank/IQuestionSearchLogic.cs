using System;
using System.Collections.Generic;
using System.ServiceModel;
using ETMS.Components.Exam.API.Entity.ItemBank;
namespace ETMS.Components.Exam.API.Interface.ItemBank
{
    /// <summary>
    /// 试题查询接口
    /// </summary>
    [ServiceContract(Namespace = "http://Autumn.Business.LMS.Interface.ItemBank/IQuestionSearch")]
    public interface IQuestionSearchLogic
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
        [OperationContract]
        IList<QuestionSearchResult> GetQuestionList(Guid ownerID, QuestionSearch qs, int pageSize, int pageNo, out int total);

        /// <summary>
        /// 得到查询后的试题列表,用于自主学习的课程设计
        /// [不包括：匹配和归类题,当试题类型传Null时]
        /// </summary>
        /// <param name="ownerID">当前用户ID</param>
        /// <param name="qs">查询实体</param>
        /// <param name="pageSize">页尺寸</param>
        /// <param name="pageNo">页码</param>
        /// <param name="total">总记录数</param>
        /// <returns>试题列表</returns>
        [OperationContract]
        IList<QuestionSearchResult> GetQuestionListInCourse(Guid ownerID, QuestionSearch qs, int pageSize, int pageNo, out int total);

        /// <summary>
        /// 得到查询后的试题列表(知识点or标题查找)
        /// </summary>
        /// <param name="ownerID">当前用户ID</param>
        /// <param name="qs">查询实体</param>
        /// <param name="pageSize">页尺寸</param>
        /// <param name="pageNo">页码</param>
        /// <param name="total">总记录数</param>
        /// <returns>试题列表</returns>
        [OperationContract]
        IList<QuestionSearchResult> SearchQuestions(Guid ownerID, QuestionSearch qs, int pageSize, int pageNo, out int total);

        /// <summary>
        /// 得到查询后的试题列表
        /// </summary>
        /// <param name="ownerID">当前用户ID</param>
        /// <param name="questionBankID">当前分类ID</param>
        /// <param name="pageSize">页尺寸</param>
        /// <param name="pageNo">页码</param>
        /// <param name="total">总记录数</param>
        /// <returns>试题列表</returns>
        [OperationContract]
        IList<QuestionSearchResult> GetQuestions(Guid ownerID, Guid questionBankID, int pageSize, int pageNo, out int total);


        /// <summary>
        /// 得到查询后的试题列表(运营商查询)
        /// </summary>
        /// <param name="qs">查询实体</param>
        /// <param name="pageSize">页尺寸</param>
        /// <param name="pageNo">页码</param>
        /// <param name="total">总记录数</param>
        /// <returns>试题列表</returns>
        [OperationContract]
        //IList<QuestionSearchResult> GetQuestionList(QuestionSearch qs, int pageSize, int pageNo, out int total);
        IList<QuestionSearchResult> GetAllQuestionList(QuestionSearch qs, int pageSize, int pageNo, out int total);
    }
}
