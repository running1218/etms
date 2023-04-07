using System;
using System.Collections.Generic;

using Autumn.Context;
using Autumn.Objects.Factory;
using ETMS.Components.Exam.API.Entity.Test;
using ETMS.Components.Exam.API.Entity.ItemBank;
using ETMS.Components.Exam.Implement.DAL.ItemBank;
using ETMS.Components.Exam.API.Interface.Test;
using ETMS.Components.Exam.API.Interface.ItemBank;
using TestPaperBank = ETMS.Components.Exam.API.Entity.ItemBank.TreeCategory;
namespace ETMS.Components.Exam.Implement.BLL.ItemBank
{
    public class TreeCategoryLogic : ITreeCategoryLogic, IMessageSourceAware, IInitializingObject
    {
        public ITreeCategoryDao TreeCategoryDao { get; set; }
        /// <summary>
        /// 试卷服务
        /// </summary>
        public ITestPaperLogic TestPaperService { get; set; }
        #region IInitializingObject 成员
        public void AfterPropertiesSet()
        {
            if (this.TreeCategoryDao == null)
                throw new Exception("please set TreeCategoryDao Property First!");

            if (this.TestPaperService == null)
                throw new Exception("please set TestPaperService Property First!");
        }
        #endregion
        #region IMessageSourceAware 成员
        public IMessageSource MessageSource { get; set; }
        #endregion

        #region 数据是否有效
        /// <summary>
        /// 检查提交的数据是否有效
        /// </summary>
        /// <param name="category"></param>
        private void CheckTreeCategory(TreeCategory category)
        {
            string name = category.CategoryName;
            if (string.IsNullOrEmpty(name))
                throw new ETMS.AppContext.BusinessException("分类的名称不能为空！");
            //else if (name.IndexOf("/") != -1)
            //    throw new ETMS.AppContext.BusinessException("分类名称中不能包含'/'！");

            int categoryType = (int)category.CategoryType;
            if (categoryType == 0)
                throw new ETMS.AppContext.BusinessException("分类必需设定分类类别！");

            int ownerID = category.OwnerID;
            if (ownerID == 0)
                throw new ETMS.AppContext.BusinessException("分类必需要有所有者！");
        }
        #endregion

        /// <summary>
        /// 添加根分类或子分类
        /// </summary>
        /// <param name="category">分类实体</param>
        public Guid Add(TreeCategory category)
        {
            CheckTreeCategory(category);
            category.CategoryID = (category.CategoryID == Guid.Empty) ? Guid.NewGuid() : category.CategoryID;
            category.CreatedUserID = ETMS.AppContext.UserContext.Current.UserID;
            this.TreeCategoryDao.Add(category);
            return category.CategoryID;
        }
        /// <summary>
        /// 同级分类的修改
        /// </summary>
        /// <param name="category"></param>
        public void Update(TreeCategory category)
        {
            CheckTreeCategory(category);
            string name = category.CategoryName;
            string pathName = category.PathName;
            int finishFlag = pathName.LastIndexOf('/') + 1;
            if (name != pathName.Substring(finishFlag))
            {
                category.PathName = pathName.Substring(0, finishFlag) + name;
            }
            this.TreeCategoryDao.Update(category);
            this.TreeCategoryDao.UpdateChildCategories(category.CategoryID);
        }
        /// <summary>
        /// 删除分类(子分类也会被删除)
        /// </summary>
        /// <param name="categoryID"></param>
        public void Delete(Guid categoryID)
        {
            this.TreeCategoryDao.Delete(categoryID);
        }

        /// <summary>
        /// 返回指定分类
        /// </summary>
        /// <param name="categoryID">分类ID</param>
        /// <returns>分类实体</returns>
        public TreeCategory GetByID(Guid categoryID)
        {
            return this.TreeCategoryDao.GetByID(categoryID);
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
            return this.TreeCategoryDao.GetAllChildrenByParentID(parentID, type, ownerID, ownerType);
        }


        #region 操作分类树
        /// <summary>
        /// 插入同级节点(向上)
        /// </summary>
        /// <param name="selectedNode"></param>
        /// <param name="newCategory"></param>
        /// <returns></returns>
        public Guid InsertUpSiblingNode(Guid selectedNode, TreeCategory newCategory)
        {
            //得到选中的分类节点
            TreeCategory selectCategory = this.TreeCategoryDao.GetByID(selectedNode);
            if (selectCategory == null)
                throw new ETMS.AppContext.BusinessException("选中节点不存在！");
            else
            {
                newCategory.CategoryID = (newCategory.CategoryID == Guid.Empty) ? Guid.NewGuid() : newCategory.CategoryID;
                newCategory.ChildrenCount = 0;
                newCategory.CreatedUserID = ETMS.AppContext.UserContext.Current.UserID;
                newCategory.Depth = selectCategory.Depth;
                newCategory.DisplayOrder = selectCategory.DisplayOrder;

                //新节点的父级=选中节点的父级
                Guid parentID = newCategory.ParentID = selectCategory.ParentID;

                //更新节点显示顺序
                this.TreeCategoryDao.UpdateNodeIndex(parentID, selectCategory.DisplayOrder);
                this.TreeCategoryDao.Add(newCategory);
                return newCategory.CategoryID;
            }
        }
        /// <summary>
        /// 插入同级节点(向下)
        /// </summary>
        /// <param name="selectedNode"></param>
        /// <param name="newCategory"></param>
        /// <returns></returns>
        public Guid InsertDownSiblingNode(Guid selectedNode, TreeCategory newCategory)
        {
            TreeCategory selectCategory = this.TreeCategoryDao.GetByID(selectedNode);
            if (selectCategory == null)
                throw new ETMS.AppContext.BusinessException("选中节点不存在！");
            else
            {
                newCategory.CategoryID = (newCategory.CategoryID == Guid.Empty) ? Guid.NewGuid() : newCategory.CategoryID;
                newCategory.ChildrenCount = 0;
                newCategory.CreatedUserID = ETMS.AppContext.UserContext.Current.UserID;
                newCategory.Depth = selectCategory.Depth;
                newCategory.DisplayOrder = ++selectCategory.DisplayOrder;
                Guid parentID = newCategory.ParentID = selectCategory.ParentID;

                if (parentID == Guid.Empty)
                    newCategory.Depth = 1;

                //更新节点显示顺序
                this.TreeCategoryDao.UpdateNodeIndex(parentID, selectCategory.DisplayOrder);
                this.TreeCategoryDao.Add(newCategory);
                return newCategory.CategoryID;
            }
        }
        /// <summary>
        /// 增大层级
        /// </summary>
        /// <param name="selectedNode"></param>
        /// <param name="parentID"></param>
        public void IncreaseIndent(Guid selectedNode, Guid parentID)
        {
            TreeCategory selectCategory = this.TreeCategoryDao.GetByID(selectedNode);
            if (selectCategory == null)
                throw new ETMS.AppContext.BusinessException("节点不存在！");
            else
            {
                //本身在第三级,或在第二级时不能有子级(否则子级将超过级数)
                if (selectCategory.Depth == 3 || (selectCategory.Depth == 2 && selectCategory.ChildrenCount > 0))
                    throw new ETMS.AppContext.BusinessException("节点最多只能有三级！");
                else
                {
                    //更改选中节点的数据
                    selectCategory.ParentID = parentID;
                    selectCategory.Depth += 1;
                    selectCategory.DisplayOrder = ++this.TreeCategoryDao.GetByID(parentID).ChildrenCount;

                    //删除后,重新添加
                    this.TreeCategoryDao.PhysicalDelete(selectedNode);
                    this.TreeCategoryDao.Add(selectCategory);
                    this.TreeCategoryDao.UpdateChildCategories(selectCategory.CategoryID);
                }
            }
        }
        /// <summary>
        /// 减少层级
        /// </summary>
        /// <param name="selectedNode"></param>
        /// <param name="parentID"></param>
        public void ReduceIndent(Guid selectedNode, Guid parentID)
        {
            //得到选中节点和父结点
            TreeCategory selectCategory = this.TreeCategoryDao.GetByID(selectedNode);
            TreeCategory parentCategory = this.TreeCategoryDao.GetByID(parentID);
            if (selectCategory == null || parentCategory == null)
                throw new ETMS.AppContext.BusinessException("节点不存在！");
            else
            {
                if (selectCategory.Depth == 1)
                    throw new ETMS.AppContext.BusinessException("不能再往前缩进！");
                else
                {
                    selectCategory.ParentID = parentCategory.ParentID;
                    selectCategory.Depth = parentCategory.Depth;

                    //得到父节点的父节点的显示序号
                    selectCategory.DisplayOrder = ++this.TreeCategoryDao.GetByID(parentCategory.ParentID).ChildrenCount;
                    this.TreeCategoryDao.PhysicalDelete(selectedNode);
                    this.TreeCategoryDao.Add(selectCategory);
                    this.TreeCategoryDao.UpdateChildCategories(selectCategory.CategoryID);
                }
            }
        }

        /// <summary>
        /// 增加节点显示序号
        /// </summary>
        /// <param name="selectedNode"></param>
        public void IncreaseNodeIndex(Guid selectedNode)
        {
            this.TreeCategoryDao.IncreaseNodeIndex(selectedNode);
        }
        /// <summary>
        /// 减少节点显示序号
        /// </summary>
        /// <param name="selectedNode"></param>
        public void ReduceNodeIndex(Guid selectedNode)
        {
            this.TreeCategoryDao.ReduceNodeIndex(selectedNode);
        }
        #endregion

        /// <summary>
        /// 返回所有分类列表
        /// </summary>
        /// <param name="type">分类类型(如试题分类,试卷分类)</param>
        /// <param name="ownerID">所有者用户ID</param>
        /// <param name="ownerType">所有者类型</param>
        /// <returns></returns>
        public IList<TreeCategory> GetAllCategory(TreeCategoryType type, Guid ownerID, OwnerType ownerType)
        {
            IList<TreeCategory> results = this.TreeCategoryDao.GetAllCategory(type, ownerID, ownerType);
            for (int i = 0; i < results.Count; i++)
            {
                string pathName = results[i].PathName;
                results[i].PathName = GetPathName(pathName);
            }


            return results;
        }
        private string GetPathName(string name)
        {
            string str = "";
            int iPos = name.IndexOf(@"/");
            while (iPos != -1)
            {
                name = name.Substring(iPos + 1);
                iPos = name.IndexOf(@"/");
                if (iPos > 0)
                    str += "_";
            }
            string result = str + name;
            return result;
        }

        /// <summary>
        /// 上下调换节点位置
        /// </summary>
        /// <param name="upNodeID"></param>
        /// <param name="downNodeID"></param>
        public void SwitchNodeIndex(Guid upNodeID, Guid downNodeID)
        {
            this.TreeCategoryDao.SwitchNodeIndex(upNodeID, downNodeID);
        }

        /// <summary>
        /// 判断是否存在有效分类
        /// </summary>
        /// <param name="ownerID">所有者用户ID</param>
        /// <param name="type">分类类型(如试题分类=1,试卷分类=2)</param>
        /// <returns>true(存在分类)</returns>
        public bool IsExist(Guid ownerID, TreeCategoryType type)
        {
            return this.TreeCategoryDao.IsExist(ownerID, type);
        }

        #region 卷库与课程
        /// <summary>
        /// 根据课程ID获取课程对应的卷库信息
        /// 为集合为0表示课程没有对应的卷库
        /// </summary>
        /// <param name="courseID">课程ID</param>
        /// <returns></returns>
        public IList<TestPaperBank> GetTestPaperLibraryByCourseID(Guid courseID)
        {
            return this.TreeCategoryDao.GetTestPaperLibraryByCourseID(courseID);
        }
        /// <summary>
        /// 根据课程ID检查课程是否已有对应的题库（异步实现）
        /// </summary>
        /// <param name="courseID">课程ID</param>
        /// <returns></returns>
        public bool CheckExistTestPaperLibraryByCourseID(Guid courseID)
        {
            return (this.GetTestPaperLibraryByCourseID(courseID).Count > 0);
        }

        /// <summary>
        /// 获取课程的卷库，如果没有，插入一条课程题库关联数据和题库信息。
        /// </summary>
        /// <param name="courseID"></param>
        /// <param name="courseName"></param>
        /// <returns></returns>
        public TestPaperBank GetTestPaperBankByCourseID(Guid courseID, string courseName, int orgID)
        {
            IList<TestPaperBank> list = GetTestPaperLibraryByCourseID(courseID);
            if (list.Count == 0)
            {
                list.Add(new TestPaperBank()
                {
                    CategoryName = courseName,  //题库的名称默认用课程名称
                    OwnerID = orgID//拥有者用组织机构ID
                });
                {
                    //添加卷库
                    Add(list[0]);
                    //添加卷库与课程关系
                    this.TreeCategoryDao.AddTestPaperBankCourseMap(courseID, list[0].CategoryID, ETMS.AppContext.UserContext.Current.UserID);
                }
            }
            return list[0];
        }

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
        public TestPaper GetCourseExamResTestPaperForManage(Guid courseID, string courseName, int orgID, Guid examResID, string examResName, int examResType)
        {
            //获取考试资源与试卷关系，如果存在 ，直接返回。
            TestPaper testPaper = this.TreeCategoryDao.GetCourserExamResTestPaper(examResID);
            if (testPaper != null)
            {
                return testPaper;
            }

            //如果试卷不存在，自动创建试卷，并建立关系
            //1、获取课程对应卷库
            TestPaperBank testPaperBank = GetTestPaperBankByCourseID(courseID, courseName, orgID);
            //2、创建试卷
            testPaper = new TestPaper()
            {
                TestPaperName = examResName,//考试资源名称
                TestPaperCategory = testPaperBank.CategoryID,//试卷分类
                TestPaperType = TestPaperType.SimpleFix,
                TotalScore =100,
                PassedScore =60,
                MaxCount =1

            };
            //3、保存试卷
            TestPaperService.AddTestPaper(testPaper);

            //4、保存课程考试资源与试卷关系
            this.TreeCategoryDao.AddCourseExamResTestPaperMap(testPaper.TestPaperID, examResID, examResType, ETMS.AppContext.UserContext.Current.UserID);

            //返回试卷
            return testPaper;
        }

        /// <summary>
        /// 获取课程考试资源对应的试卷信息（针对学生端）
        /// </summary>
        /// <param name="examResID">考试资源ID</param>
        /// <returns>试卷不存在时返回null</returns>
        public TestPaper GetCourseExamResTestPaperForStudent(Guid examResID)
        {
            return this.TreeCategoryDao.GetCourserExamResTestPaper(examResID);
        }
        #endregion
    }
}
