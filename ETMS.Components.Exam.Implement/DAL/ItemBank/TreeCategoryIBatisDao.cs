using System;
using System.Collections.Generic;

using System.Collections;
using Autumn.Data.ORM.IBatis;
using ETMS.AppContext;
using ETMS.Components.Exam.API.Entity.ItemBank;
using ETMS.Components.Exam.API.Entity.Test;
namespace ETMS.Components.Exam.Implement.DAL.ItemBank
{
    /// <summary>
    /// 树形分类数据访问的实现
    /// </summary>
    public class TreeCategoryIBatisDao : ReadWriteDataMapperDaoSupport, ITreeCategoryDao
    {
        /// <summary>
        /// 添加根分类或子分类
        /// </summary>
        /// <param name="category">分类实体</param>
        public void Add(TreeCategory category)
        {
            this.DataMapperClient_Write.Insert("TreeCategory.Add", category);
        }
        /// <summary>
        /// 编辑分类
        /// </summary>
        /// <param name="category">分类实体</param>
        public void Update(TreeCategory category)
        {
            this.DataMapperClient_Write.Update("TreeCategory.Update", category);
        }
        /// <summary>
        /// 删除分类
        /// </summary>
        /// <param name="category">分类实体</param>
        public void Delete(Guid categoryID)
        {
            this.DataMapperClient_Write.Delete("TreeCategory.Delete", categoryID);
        }

        /// <summary>
        /// 返回指定分类
        /// </summary>
        /// <param name="categoryID">分类ID</param>
        /// <returns></returns>
        public TreeCategory GetByID(Guid categoryID)
        {
            return this.DataMapperClient_Read.QueryForObject<TreeCategory>("TreeCategory.GetByID", categoryID);
        }
        /// <summary>
        /// 根据父节点取分类列表
        /// </summary>
        /// <param name="parentID">父分类ID(取第一级时父节点=Guid.Empty)</param>
        /// <param name="type">分类类型(如试题分类,试卷分类)</param>
        /// <param name="ownerID">所有者用户ID</param>
        /// <param name="ownerType">所有者类型</param>
        /// <returns>分类列表集</returns>
        public IList<TreeCategory> GetAllChildrenByParentID(Guid parentID, TreeCategoryType type, Guid ownerID, OwnerType ownerType)
        {
            return this.DataMapperClient_Read.QueryForList<TreeCategory>("TreeCategory.GetAllChildrenByParentID",
                new
                {
                    ParentID = parentID,
                    CategoryType = type,
                    OwnerID = ownerID,
                    OwnerType = ownerType
                });
        }



        /// <summary>
        /// 更新节点排序号
        /// </summary>
        /// <param name="parentID">父节点ID</param>
        /// <param name="mixOrder">最小显示序号</param>
        public void UpdateNodeIndex(Guid parentID, int mixOrder)
        {
            this.DataMapperClient_Write.Update("TreeCategory.UpdateNodeIndex",
                new { ParentID = parentID, MixOrder = mixOrder });
        }
        /// <summary>
        /// 物理删除一个分类
        /// </summary>
        /// <param name="categoryID"></param>
        public void PhysicalDelete(Guid categoryID)
        {
            this.DataMapperClient_Write.Delete("TreeCategory.PhysicalDelete", categoryID);
        }
        /// <summary>
        /// 增加节点显示序号
        /// </summary>
        /// <param name="selectedNode"></param>
        public void IncreaseNodeIndex(Guid categoryID)
        {
            this.DataMapperClient_Write.Update("TreeCategory.IncreaseNodeIndex", categoryID);
        }
        /// <summary>
        /// 减少节点显示序号
        /// </summary>
        /// <param name="selectedNode"></param>
        public void ReduceNodeIndex(Guid categoryID)
        {
            this.DataMapperClient_Write.Update("TreeCategory.ReduceNodeIndex", categoryID);
        }


        /// <summary>
        /// 返回所有分类列表
        /// </summary>
        /// <param name="type">分类类型(如试题分类,试卷分类)</param>
        /// <param name="ownerID">所有者用户ID</param>
        /// <param name="ownerType">所有者类型</param>
        /// <returns></returns>
        public IList<TreeCategory> GetAllCategory(TreeCategoryType type, Guid ownerID, OwnerType ownerType)
        {
            return this.DataMapperClient_Read.QueryForList<TreeCategory>("TreeCategory.GetAllCategory",
                new
                {
                    CategoryType = type,
                    OwnerID = ownerID,
                    OwnerType = ownerType
                });
        }
        /// <summary>
        /// 上下调换节点位置
        /// </summary>
        /// <param name="upNodeID"></param>
        /// <param name="downNodeID"></param>
        public void SwitchNodeIndex(Guid upNodeID, Guid downNodeID)
        {
            this.DataMapperClient_Write.Update("TreeCategory.SwitchNodeIndex", new { UpNodeID = upNodeID, DownNodeID = downNodeID, UpdatedUserID = UserContext.Current.UserID });
        }

        /// <summary>
        /// 判断是否存在有效分类
        /// </summary>
        /// <param name="ownerID">所有者用户ID</param>
        /// <param name="type">分类类型(如试题分类=1,试卷分类=2)</param>
        /// <returns>true(存在分类)</returns>
        public bool IsExist(Guid ownerID, TreeCategoryType type)
        {
            int result = (int)DataMapperClient_Read.QueryForObject("TreeCategory.IsExist", new
            {
                OwnerID = ownerID,
                Type = type
            });
            if (result > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 更新指定分类下的子分类
        /// </summary>
        /// <param name="categoryID">分类ID</param>
        public void UpdateChildCategories(Guid categoryID)
        {
            Hashtable ht = new Hashtable();
            ht.Add("CategoryID", categoryID);
            this.DataMapperClient_Write.Update("TreeCategory.UpdateChildCategories", ht);
        }

        /// <summary>
        /// 根据课程ID获取课程对应的卷库信息
        /// 为集合为0表示课程没有对应的卷库
        /// </summary>
        /// <param name="courseID">课程ID</param>
        /// <returns></returns>
        public IList<TreeCategory> GetTestPaperLibraryByCourseID(Guid courseID)
        {
            return this.DataMapperClient_Read.QueryForList<TreeCategory>("TreeCategory.GetAllTestPaperLibraryByCourseID", courseID);
        }

        /// <summary>
        /// 获取课程考试资源对应的试卷
        /// </summary>
        /// <param name="examResID">考试资源Id</param>
        /// <returns></returns>
        public TestPaper GetCourserExamResTestPaper(Guid examResID)
        {
            return this.DataMapperClient_Read.QueryForObject<TestPaper>("TreeCategory.GetCourseExamResTestPaper", examResID);
        }
        /// <summary>
        /// 添加卷库与课程关系
        /// </summary>
        /// <param name="courseID">课程ID</param>
        /// <param name="testPaperBankID">卷库ID</param>
        /// <param name="CreatedUserID">创建人ID</param>
        /// <returns></returns>
        public Guid AddTestPaperBankCourseMap(Guid courseID, Guid testPaperBankID, int createdUserID)
        {
            Guid mapID = Guid.NewGuid();
            DataMapperClient_Write.Insert("TreeCategory.AddTestPaperBankCourseMap",
                new
                {
                    MapID = mapID,
                    CourseID = courseID,
                    TestPaperBankID = testPaperBankID,
                    CreatedUserID = createdUserID
                });
            return mapID;
        }

        /// <summary>
        /// 添加课程考试资源试卷关系
        /// </summary>
        /// <param name="courseID">课程ID</param>
        /// <param name="testPaperBankID">卷库ID</param>
        /// <param name="CreatedUserID">创建人ID</param>
        /// <returns></returns>
        public Guid AddCourseExamResTestPaperMap(Guid testPaperID, Guid examResID, int examResType, int createdUserID)
        {
            Guid mapID = Guid.NewGuid();
            DataMapperClient_Write.Insert("TreeCategory.AddCourseExamResTestPaperMap",
                new
                {
                    MapID = mapID,
                    TestPaperID = testPaperID,
                    ExamResID = examResID,
                    ExamResType = examResType,
                    CreatedUserID = createdUserID
                });
            return mapID;
        }
    }
}
