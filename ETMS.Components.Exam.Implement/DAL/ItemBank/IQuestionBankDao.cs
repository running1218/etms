using System;
using System.Collections.Generic;
using ETMS.Components.Exam.API.Entity.ItemBank;

namespace ETMS.Components.Exam.Implement.DAL.ItemBank
{
    public interface IQuestionBankDao
    {
        #region Base 接口
        /// <summary>
        /// 1、添加
        /// </summary>
        /// <param name="questionLibrary">题库实体</param>
        /// <returns>题库ID</returns>
        void Add(QuestionBank questionLibrary);
        /// <summary>
        /// 2、修改
        /// </summary>
        /// <param name="questionLibrary">题库实体</param>
        void Update(QuestionBank questionLibrary);
        /// <summary>
        /// 3、删除
        /// </summary>
        /// <param name="questionBankID">题库ID</param>
        void Delete(Guid questionBankID, int updatedUserID);
        /// <summary>
        /// 4、获取
        /// </summary>
        /// <param name="questionBankID">题库ID</param>
        /// <returns>题库实体</returns>
        QuestionBank GetByID(Guid questionBankID);
        #endregion

        #region 分类树操作
        /// <summary>
        /// 更新节点显示顺序
        /// </summary>
        /// <param name="parentID">父节点ID</param>
        /// <param name="mixOrder">需要更新的最小显示顺序</param>
        void UpdateNodeIndex(Guid parentID, int mixOrder);
        /// <summary>
        /// 上下调换节点位置
        /// </summary>
        /// <param name="upNodeID"></param>
        /// <param name="downNodeID"></param>
        void SwitchQuestionIndex(Guid upNodeID, Guid downNodeID);
        /// <summary>
        /// 物理删除
        /// </summary>
        /// <param name="questionBankID"></param>
        void DeleteByID(Guid questionBankID);
        /// <summary>
        /// 根据父节点的ID取直接子结点列表（异步展示题库树）
        /// 取第一级节点时，参数为Guid.Empty
        /// </summary>
        /// <param name="QuestionBankID"></param>
        /// <returns></returns>
        IList<QuestionBank> GetAllChildrenByParentID(Guid QuestionBankID);

        /// <summary>
        /// 根据父节点parentPath取直接子结点列表（异步展示题库树）
        /// 取第一级节点时，参数为Guid.Empty
        /// </summary>
        /// <param name="parentPath"></param>
        /// <returns></returns>
        IList<QuestionBank> GetAllChildrenByParentID(string parentPath);

        /// <summary>
        /// 获取课程对应的题库列表
        /// </summary>
        /// <param name="courseID">课程ID</param>
        /// <returns></returns>
        IList<QuestionBank> GetAllQuestionLibraryByCourseID(Guid courseID);

        /// <summary>
        /// 获取某一题库下的试题列表
        /// </summary>
        /// <param name="questionBankID">题库ID</param>
        /// <param name="pageIndex">页号</param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="totalRecord">输出，总记录数</param>
        /// <returns>试题列表</returns>
        IList<Question> GetQuestionByQuestionBankID(Guid questionBankID, QuestionSearch search, int pageIndex, int pageSize, out int totalRecord);

        /// <summary>
        /// 获取某一课程所有题库下的试题列表
        /// </summary>
        /// <param name="courseID">课程ID</param>
        /// <param name="pageIndex">页号</param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="totalRecord">输出，总记录数</param>
        /// <returns>试题列表</returns>
        IList<Question> GetQuestionByCourseID(Guid courseID, QuestionSearch search, int pageIndex, int pageSize, out int totalRecord);
        #endregion
    }
}
