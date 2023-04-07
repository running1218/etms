using System;
using System.Collections.Generic;

using ETMS.Components.Exam.API.Entity.ItemBank;
using ETMS.Components.Exam.API.Entity.Test;

using TestPaperBank = ETMS.Components.Exam.API.Entity.ItemBank.TreeCategory;
namespace ETMS.Components.Exam.Implement.DAL.ItemBank
{
    /// <summary>
    /// 树形分类接口
    /// </summary>
    public interface ITreeCategoryDao
    {
        /// <summary>
        /// 添加根分类或子分类
        /// </summary>
        /// <param name="category">分类实体</param>
        void Add(TreeCategory category);

        /// <summary>
        /// 编辑分类
        /// </summary>
        /// <param name="category">分类实体</param>
        void Update(TreeCategory category);

        /// <summary>
        /// 删除分类
        /// </summary>
        /// <param name="categoryID">分类ID</param>
        void Delete(Guid categoryID);

        /// <summary>
        /// 返回指定分类
        /// </summary>
        /// <param name="categoryID">分类ID</param>
        /// <returns></returns>
        TreeCategory GetByID(Guid categoryID);

        /// <summary>
        /// 根据父节点取分类列表
        /// </summary>
        /// <param name="parentID">父分类ID(取第一级时父节点=Guid.Empty)</param>
        /// <param name="type">分类类型(如试题分类,试卷分类)</param>
        /// <param name="ownerID">所有者用户ID</param>
        /// <param name="ownerType">所有者类型</param>
        /// <returns>分类列表集</returns>
        IList<TreeCategory> GetAllChildrenByParentID(Guid parentID, TreeCategoryType type, Guid ownerID, OwnerType ownerType);

        /// <summary>
        /// 更新节点排序号
        /// </summary>
        /// <param name="parentID">父节点ID</param>
        /// <param name="mixOrder">最小显示序号</param>
        void UpdateNodeIndex(Guid parentID, int mixOrder);

        /// <summary>
        /// 物理删除一个分类
        /// </summary>
        /// <param name="categoryID"></param>
        void PhysicalDelete(Guid categoryID);

        /// <summary>
        /// 增加节点显示序号
        /// </summary>
        /// <param name="selectedNode"></param>
        void IncreaseNodeIndex(Guid selectedNode);

        /// <summary>
        /// 减少节点显示序号
        /// </summary>
        /// <param name="selectedNode"></param>
        void ReduceNodeIndex(Guid selectedNode);

        /// <summary>
        /// 返回所有分类列表
        /// </summary>
        /// <param name="type">分类类型(如试题分类=1,试卷分类=2)</param>
        /// <param name="ownerID">所有者用户ID</param>
        /// <param name="ownerType">所有者类型</param>
        /// <returns></returns>
        IList<TreeCategory> GetAllCategory(TreeCategoryType type, Guid ownerID, OwnerType ownerType);

        /// <summary>
        /// 上下调换节点位置
        /// </summary>
        /// <param name="upNodeID"></param>
        /// <param name="downNodeID"></param>
        void SwitchNodeIndex(Guid upNodeID, Guid downNodeID);

        /// <summary>
        /// 判断是否存在有效分类
        /// </summary>
        /// <param name="ownerID">所有者用户ID</param>
        /// <param name="type">分类类型(如试题分类=1,试卷分类=2)</param>
        /// <returns>true(存在分类)</returns>
        bool IsExist(Guid ownerID, TreeCategoryType type);

        /// <summary>
        /// 更新指定分类下的子分类
        /// </summary>
        /// <param name="categoryID">分类ID</param>
        void UpdateChildCategories(Guid categoryID);


        /// <summary>
        /// 根据课程ID获取课程对应的卷库信息
        /// 为集合为0表示课程没有对应的卷库
        /// </summary>
        /// <param name="courseID">课程ID</param>
        /// <returns></returns>
        IList<TestPaperBank> GetTestPaperLibraryByCourseID(Guid courseID);

        /// <summary>
        /// 添加卷库与课程关系
        /// </summary>
        /// <param name="courseID">课程ID</param>
        /// <param name="testPaperBankID">卷库ID</param>
        /// <param name="CreatedUserID">创建人ID</param>
        /// <returns></returns>
        Guid AddTestPaperBankCourseMap(Guid courseID, Guid testPaperBankID, int createdUserID);

        /// <summary>
        /// 获取课程考试资源对应的试卷
        /// </summary>
        /// <param name="examResID">考试资源Id</param>
        /// <returns></returns>
        TestPaper GetCourserExamResTestPaper(Guid examResID);

        /// <summary>
        /// 添加课程考试资源试卷关系
        /// </summary>
        /// <param name="courseID">课程ID</param>
        /// <param name="testPaperBankID">卷库ID</param>
        /// <param name="CreatedUserID">创建人ID</param>
        /// <returns></returns>
        Guid AddCourseExamResTestPaperMap(Guid testPaperID, Guid examResID, int examResType, int createdUserID);
    }
}
