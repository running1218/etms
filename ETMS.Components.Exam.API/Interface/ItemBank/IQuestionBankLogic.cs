using System;
using System.Collections.Generic;
using System.ServiceModel;
using ETMS.Components.Exam.API.Entity.ItemBank;

namespace ETMS.Components.Exam.API.Interface.ItemBank
{
    /// <summary>
    /// 题库接口类
    /// </summary>
    [ServiceContract(Namespace = "http://Autumn.Business.LMS.Interface.ItemBank/IQuestionLibraryLogic")]
    public interface IQuestionBankLogic
    {
        #region Base 接口
        /// <summary>
        /// 1、添加题库，并创建题库与课程关系
        /// </summary>
        /// <param name="questionLibrary">题库实体</param>
        /// <returns>题库ID</returns>
        [OperationContract]
        Guid Add(QuestionBank questionLibrary);
        /// <summary>
        /// 2、修改（仅修改题库基本信息）
        /// </summary>
        /// <param name="questionLibrary">题库实体</param>
        [OperationContract]
        void Update(QuestionBank questionLibrary);
        /// <summary>
        /// 3、删除，（删除课程与题库关系、删除题库（外键约束））
        /// 提示用户：删除分类时，分类下的试题也会删除，是否要删除分类？
        /// </summary>
        /// <param name="questionBankID">题库ID</param>
        [OperationContract]
        void Delete(Guid questionBankID);
        /// <summary>
        /// 4、获取
        /// </summary>
        /// <param name="questionBankID">题库ID</param>
        /// <returns>题库实体</returns>
        [OperationContract]
        QuestionBank GetByID(Guid questionBankID);
        #endregion

        #region Extend 接口
        /// <summary>
        /// 根据父节点的Path取直接子结点列表（异步展示题库树）
        /// 取第一级节点是path为string.Empty
        /// </summary>
        /// <param name="path">父节点Path</param>
        /// <returns></returns>
        [OperationContract]
        IList<QuestionBank> GetQuestionLibraryByParentPath(string path);
        /// <summary>
        /// 根据父节点的ID取直接子结点列表（异步展示题库树）
        /// 取第一级节点时，参数为Guid.Empty
        /// </summary>
        /// <param name="QuestionBankID"></param>
        /// <returns></returns>
        [OperationContract]
        IList<QuestionBank> GetAllChildrenByParentID(Guid QuestionBankID);
        /// <summary>
        /// 获取所有的题库数结点列表(同步展示题库树)
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        IList<QuestionBank> GetAllQuestionLibrary();
        /// <summary>
        /// 根据课程ID获取课程对应的题库信息
        /// 为null表示课程没有对应的题库
        /// </summary>
        /// <param name="courseID">课程ID</param>
        /// <returns></returns>
        [OperationContract]
        IList<QuestionBank> GetQuestionLibraryByCourseID(Guid courseID);
        /// <summary>
        /// 根据课程ID检查课程是否已有对应的题库（异步实现）
        /// </summary>
        /// <param name="courseID">课程ID</param>
        /// <returns></returns>
        [OperationContract]
        bool CheckExistQuestionLibraryByCourseID(Guid courseID);

        /// <summary>
        /// 获取课程的题库ID值，如果没有，插入一条课程题库关联数据和题库信息。
        /// </summary>
        /// <param name="courseID">课程ID</param>
        /// <param name="courseName">课程名称，用做题库名称</param>
        /// <returns>题库实体</returns>
        [OperationContract]
        QuestionBank GetQuestionBankByCourseID(Guid courseID, string courseName, int orgID);

        /// <summary>
        /// 获取题库下的试题列表
        /// </summary>
        /// <param name="questionBankID">题库ID</param>
        /// <param name="search">搜索条件</param>
        /// <param name="pageIndex">页号</param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="totalRecord">输出，总记录数</param>
        /// <returns>试题列表</returns>
        [OperationContract]
        IList<Question> GetQuestionByQuestionBankID(Guid questionBankID, QuestionSearch search, int pageIndex, int pageSize, out int totalRecord);

        /// <summary>
        /// 获取某一课程所有题库下的试题列表
        /// </summary>
        /// <param name="courseID">课程ID</param>
        /// <param name="search">搜索条件</param>
        /// <param name="pageIndex">页号</param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="totalRecord">输出，总记录数</param>
        /// <returns>试题列表</returns>
        IList<Question> GetQuestionByCourseID(Guid courseID, QuestionSearch search, int pageIndex, int pageSize, out int totalRecord);

        [OperationContract]
        Guid InsertUpSiblingNode(Guid selectedNodeID, QuestionBank newQuestionBank);

        [OperationContract]
        Guid InsertDownSiblingNode(Guid selectedNodeID, QuestionBank newQuestionBank);
        /// <summary>
        /// 调整试题分类显示顺序
        /// 限制：1同题型的可以上下调整顺序
        /// </summary>
        /// <param name="upQuestionID">上面的节点ID</param>
        /// <param name="downQuestionID">下面的节点ID</param>
        [OperationContract]
        void SwitchQuestionIndex(Guid upNodeID, Guid downNodeID);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="selectedNodeID"></param>
        /// <param name="upNodeID"></param>
        [OperationContract]
        void AddIndent(Guid selectedNodeID, Guid upNodeID);

        [OperationContract]
        void ReduceIndent(Guid selectedNodeID, Guid parentNodeID);
        #endregion
    }
}
