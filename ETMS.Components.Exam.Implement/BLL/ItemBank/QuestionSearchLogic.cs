using System;
using System.Collections.Generic;
using Autumn.Context;
using Autumn.Objects.Factory;
using ETMS.Components.Exam.API.Entity.ItemBank;
using ETMS.Components.Exam.Implement.DAL.ItemBank;
using ETMS.Components.Exam.API.Interface.ItemBank;
namespace ETMS.Components.Exam.Implement.BLL.ItemBank
{
    public class QuestionSearchLogic : IMessageSourceAware, IInitializingObject, IQuestionSearchLogic
    {
        public IQuestionSearchDao QuestionSearchDao { get; set; } 
        #region IMessageSourceAware 成员
        public IMessageSource MessageSource
        {
            get;
            set;
        }
        #endregion
        #region IInitializingObject 成员
        public void AfterPropertiesSet()
        {
            if (QuestionSearchDao == null)
            {
                throw new NotImplementedException("please set QuestionSearchDao Property First!");
            }
        }
        #endregion


        /// <summary>
        /// 得到查询后的试题列表(用户查询)
        /// </summary>
        /// <param name="questionBankID">分类ID</param>
        /// <param name="pageSize">页尺寸</param>
        /// <param name="pageNo">页码</param>
        /// <param name="total">总记录数</param>
        /// <returns>试题列表</returns>
        public IList<QuestionSearchResult> GetQuestions(Guid ownerID, Guid questionBankID, int pageSize, int pageNo, out int total)
        {
            IList<QuestionSearchResult> results = QuestionSearchDao.GetQuestionList(ownerID, questionBankID, pageSize, pageNo, out total);
            return (results);
        }
        /// <summary>
        /// 得到查询后的试题列表(用户查询,知识点和标题and查找)
        /// </summary>
        /// <param name="ownerID">所有人</param>
        /// <param name="qs">查询实体</param>
        /// <param name="pageSize">页尺寸</param>
        /// <param name="pageNo">页码</param>
        /// <param name="total">总记录数</param>
        /// <returns>试题列表</returns>
        public IList<QuestionSearchResult> GetQuestionList(Guid ownerID, QuestionSearch qs, int pageSize, int pageNo, out int total)
        {
            IList<QuestionSearchResult> results = QuestionSearchDao.GetQuestionList(ownerID, qs, pageSize, pageNo, out total);
            return (results);
        }

        /// <summary>
        /// 得到查询后的试题列表(用户查询,知识点和标题and查找)-课程设计
        /// </summary>
        /// <param name="ownerID">所有人</param>
        /// <param name="qs">查询实体</param>
        /// <param name="pageSize">页尺寸</param>
        /// <param name="pageNo">页码</param>
        /// <param name="total">总记录数</param>
        /// <returns>试题列表</returns>
        public IList<QuestionSearchResult> GetQuestionListInCourse(Guid ownerID, QuestionSearch qs, int pageSize, int pageNo, out int total)
        {
            IList<QuestionSearchResult> results = QuestionSearchDao.GetQuestionListInCourse(ownerID, qs, pageSize, pageNo, out total);
            return (results);
        }


        /// <summary>
        /// 得到查询后的试题列表(用户查询,知识点or标题查找)
        /// </summary>
        /// <param name="ownerID">所有人</param>
        /// <param name="qs">查询实体</param>
        /// <param name="pageSize">页尺寸</param>
        /// <param name="pageNo">页码</param>
        /// <param name="total">总记录数</param>
        /// <returns>试题列表</returns>
        public IList<QuestionSearchResult> SearchQuestions(Guid ownerID, QuestionSearch qs, int pageSize, int pageNo, out int total)
        {
            IList<QuestionSearchResult> results = QuestionSearchDao.SearchQuestions(ownerID, qs, pageSize, pageNo, out total);
            return (results);
        }
        /// <summary>
        /// 得到查询后的试题列表(运营商查询)
        /// </summary>
        /// <param name="qs">查询实体</param>
        /// <param name="pageSize">页尺寸</param>
        /// <param name="pageNo">页码</param>
        /// <param name="total">总记录数</param>
        /// <returns>试题列表</returns>
        public IList<QuestionSearchResult> GetAllQuestionList(QuestionSearch qs, int pageSize, int pageNo, out int total)
        {
            IList<QuestionSearchResult> results = QuestionSearchDao.GetQuestionList(qs, pageSize, pageNo, out total);
            return (results);
        }
    }
}
