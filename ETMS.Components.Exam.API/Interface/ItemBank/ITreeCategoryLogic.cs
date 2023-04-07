using System;
using System.Collections.Generic;

using System.ServiceModel;
using ETMS.Components.Exam.API.Entity.Test;
using ETMS.Components.Exam.API.Entity.ItemBank;
namespace ETMS.Components.Exam.API.Interface.ItemBank
{
    /// <summary>
    /// 分类操作接口
    /// </summary>
    [ServiceContract(Namespace = "http://Autumn.Business.LMS.Interface.ItemBank/ITreeCategoryLogic")]
    public interface ITreeCategoryLogic
    {
        /// <summary>
        /// 添加根分类或子分类
        /// </summary>
        /// <param name="category">分类实体</param>
        /// <returns>分类ID</returns>
        [OperationContract]
        Guid Add(TreeCategory category);

        /// <summary>
        /// 编辑同级分类
        /// </summary>
        /// <param name="category">分类实体</param>
        [OperationContract]
        void Update(TreeCategory category);

        /// <summary>
        /// 删除分类(子分类也会被删除)
        /// </summary>
        /// <param name="categoryID">分类ID</param>
        [OperationContract]
        void Delete(Guid categoryID);

        /// <summary>
        /// 返回指定分类
        /// </summary>
        /// <param name="categoryID">分类ID</param>
        /// <returns>分类实体</returns>
        [OperationContract]
        TreeCategory GetByID(Guid categoryID);

        /// <summary>
        /// 根据父节点取分类列表
        /// </summary>
        /// <param name="parentID">父分类ID(取第一级时父节点=Guid.Empty)</param>
        /// <param name="type">分类类型(如试题分类,试卷分类)</param>
        /// <param name="ownerID">所有者用户ID</param>
        /// <param name="ownerType">所有者类型</param>
        /// <returns>分类列表集</returns>
        [OperationContract]
        IList<TreeCategory> GetAllChildrenByParentID(Guid parentID, TreeCategoryType type, Guid ownerID, OwnerType ownerType);

        /// <summary>
        /// 插入同级节点
        /// </summary>
        /// <param name="selectedNode"></param>
        /// <param name="newCategory"></param>
        /// <returns></returns>
        [OperationContract]
        Guid InsertUpSiblingNode(Guid selectedNode, TreeCategory newCategory);

        /// <summary>
        /// 插入同级节点
        /// </summary>
        /// <param name="selectedNode"></param>
        /// <param name="newCategory"></param>
        /// <returns></returns>
        [OperationContract]
        Guid InsertDownSiblingNode(Guid selectedNode, TreeCategory newCategory);

        /// <summary>
        /// 增大层级
        /// </summary>
        /// <param name="selectedNode"></param>
        /// <param name="parentID"></param>
        [OperationContract]
        void IncreaseIndent(Guid selectedNode, Guid parentID);

        /// <summary>
        /// 减少层级
        /// </summary>
        /// <param name="selectedNode"></param>
        /// <param name="parentID"></param>
        [OperationContract]
        void ReduceIndent(Guid selectedNode, Guid parentID);

        /// <summary>
        /// 增加节点显示序号
        /// </summary>
        /// <param name="selectedNode"></param>
        [OperationContract]
        void IncreaseNodeIndex(Guid selectedNode);

        /// <summary>
        /// 减少节点显示序号
        /// </summary>
        /// <param name="selectedNode"></param>
        [OperationContract]
        void ReduceNodeIndex(Guid selectedNode);

        /// <summary>
        /// 返回所有分类列表
        /// </summary>
        /// <param name="type">分类类型(如试题分类,试卷分类)</param>
        /// <param name="ownerID">所有者用户ID</param>
        /// <param name="ownerType">所有者类型</param>
        /// <returns></returns>
        [OperationContract]
        IList<TreeCategory> GetAllCategory(TreeCategoryType type, Guid ownerID, OwnerType ownerType);

        /// <summary>
        /// 调整分类显示顺序
        /// </summary>
        /// <param name="upQuestionID">上面的节点ID</param>
        /// <param name="downQuestionID">下面的节点ID</param>
        [OperationContract]
        void SwitchNodeIndex(Guid upNodeID, Guid downNodeID);

        /// <summary>
        /// 判断是否存在有效分类
        /// </summary>
        /// <param name="ownerID">所有者用户ID</param>
        /// <param name="type">分类类型(如试题分类=1,试卷分类=2)</param>
        /// <returns>true(存在分类)</returns>
        [OperationContract]
        bool IsExist(Guid ownerID, TreeCategoryType type);

        /// <summary>
        /// 获取课程考试资源对应的试卷(针对管理端）
        /// 自动逻辑：
        /// 如果课程对应的卷库不存在，自动创建卷库
        /// 如果试卷不存在，则自动创建试卷，并建立试卷与考试资源关系
        /// </summary>
        /// <param name="courseID">课程ID</param>
        /// <param name="courseName">课程名称</param>
        /// <param name="orgID">所属机构</param>
        /// <param name="examResID">考试资源ID</param>
        /// <param name="examResName">考试资源名称</param>
        /// <param name="examResType">考试资源类型{1：在线作业，2：在线考试，3：闯关竞赛}</param>
        /// <returns>试卷信息</returns>
        TestPaper GetCourseExamResTestPaperForManage(Guid courseID, string courseName, int orgID, Guid examResID, string examResName, int examResType);

        /// <summary>
        /// 获取课程考试资源对应的试卷信息（针对学生端）
        /// </summary>
        /// <param name="examResID">考试资源ID</param>
        /// <returns>试卷不存在时返回null</returns>
        TestPaper GetCourseExamResTestPaperForStudent(Guid examResID);
    }
}
