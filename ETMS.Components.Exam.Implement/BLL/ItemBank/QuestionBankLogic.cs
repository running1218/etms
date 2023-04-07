using System;
using System.Collections.Generic;
using ETMS.Components.Exam.API.Entity.ItemBank;
using Autumn.Context;
using Autumn.Objects.Factory;
using ETMS.Components.Exam.Implement.DAL.ItemBank;
using ETMS.AppContext;
using Autumn.Transaction.Interceptor;
using ETMS.Components.Exam.API.Interface.ItemBank;
namespace ETMS.Components.Exam.Implement.BLL.ItemBank
{
    public class QuestionBankLogic : IQuestionBankLogic, IMessageSourceAware, IInitializingObject
    {
        public IQuestionBankDao QuestionBankDao { get; set; }

        //public IQuestionDao QuestionLogic { get; set; }
        public IQuestionLogic QuestionLogic { get; set; }

        #region Base 接口
        /// <summary>
        /// 1、添加
        /// </summary>
        /// <param name="questionLibrary">题库实体</param>
        /// <returns>题库ID</returns>
        public Guid Add(QuestionBank questionLibrary)
        {
            questionLibrary.QuestionBankID = (questionLibrary.QuestionBankID == Guid.Empty) ? Guid.NewGuid() : questionLibrary.QuestionBankID;
            questionLibrary.CreatedUserID = ETMS.AppContext.UserContext.Current.UserID;
            this.QuestionBankDao.Add(questionLibrary);
            return questionLibrary.QuestionBankID;
        }
        /// <summary>
        /// 2、修改
        /// </summary>
        /// <param name="questionLibrary">题库实体</param>
        public void Update(QuestionBank questionLibrary)
        {
            string pathName = questionLibrary.PathName;
            string name = questionLibrary.QuestionBankName;
            if (name != pathName.Substring(pathName.LastIndexOf('/') + 1))
            {
                questionLibrary.PathName = pathName.Substring(0, pathName.LastIndexOf('/') + 1) + name;
            }
            questionLibrary.UpdatedUserID = ETMS.AppContext.UserContext.Current.UserID;
            this.QuestionBankDao.Update(questionLibrary);
        }
        /// <summary>
        /// 3、删除
        /// 提示用户：删除分类时，分类下的试题也会删除，是否要删除分类？
        /// </summary>
        /// <param name="questionBankID">题库ID</param>
        public void Delete(Guid questionBankID)
        {
            // TODO: 删除试题的逻辑

            this.QuestionBankDao.Delete(questionBankID, UserContext.Current.UserID);
        }
        /// <summary>
        /// 4、获取
        /// </summary>
        /// <param name="questionBankID">题库ID</param>
        /// <returns>题库实体</returns>
        public QuestionBank GetByID(Guid questionBankID)
        {
            return this.QuestionBankDao.GetByID(questionBankID);
        }
        #endregion

        #region Extend 接口
        /// <summary>
        /// 根据父节点的Path取直接子结点列表（异步展示题库树）
        /// 取第一级节点是path为string.Empty
        /// </summary>
        /// <param name="path">父节点Path</param>
        /// <returns></returns>
        public IList<QuestionBank> GetQuestionLibraryByParentPath(string path)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 根据父节点的ID取直接子结点列表（异步展示题库树）
        /// 取第一级节点时，参数为Guid.Empty
        /// </summary>
        /// <param name="QuestionBankID"></param>
        /// <returns></returns>
        public IList<QuestionBank> GetAllChildrenByParentID(Guid QuestionBankID)
        {
            return this.QuestionBankDao.GetAllChildrenByParentID(QuestionBankID);
        }
        /// <summary>
        /// 获取所有的题库数结点列表(同步展示题库树)
        /// </summary>
        /// <returns></returns>
        public IList<QuestionBank> GetAllQuestionLibrary()
        {
            return this.GetAllChildrenByParentID(Guid.Empty);
        }
        /// <summary>
        /// 根据课程ID获取课程对应的题库信息
        /// 为null表示课程没有对应的题库
        /// </summary>
        /// <param name="courseID">课程ID</param>
        /// <returns></returns>
        public IList<QuestionBank> GetQuestionLibraryByCourseID(Guid courseID)
        {
            return this.QuestionBankDao.GetAllQuestionLibraryByCourseID(courseID);
        }
        /// <summary>
        /// 根据课程ID检查课程是否已有对应的题库（异步实现）
        /// </summary>
        /// <param name="courseID">课程ID</param>
        /// <returns></returns>
        public bool CheckExistQuestionLibraryByCourseID(Guid courseID)
        {
            return (this.GetQuestionLibraryByCourseID(courseID).Count > 0);
        }

        /// <summary>
        /// 获取课程的题库ID值，如果没有，插入一条课程题库关联数据和题库信息。
        /// </summary>
        /// <param name="courseID"></param>
        /// <param name="courseName"></param>
        /// <returns></returns>
        public QuestionBank GetQuestionBankByCourseID(Guid courseID,string courseName,int orgID)
        { 
            IList<QuestionBank> list = GetQuestionLibraryByCourseID(courseID);
            if (list.Count == 0)
            {
                list.Add(new QuestionBank()
                {
                    CourseID = courseID,
                    QuestionBankName = courseName,  //题库的名称默认用课程名称
                    OwnerID = orgID
                });//拥有者用组织机构ID
                Add(list[0]);
            }

            return list[0]; 
        }

        /// <summary>
        /// 获取某一课程所有题库下的试题列表
        /// </summary>
        /// <param name="courseID">课程ID</param>
        /// <param name="pageIndex">页号</param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="totalRecord">输出，总记录数</param>
        /// <returns>试题列表</returns>
        public IList<Question> GetQuestionByCourseID(Guid courseID, QuestionSearch search, int pageIndex, int pageSize, out int totalRecord)
        {
            return this.QuestionBankDao.GetQuestionByCourseID(courseID, search, pageIndex, pageSize, out totalRecord);
        }
        /// <summary>
        /// 获取某一题库下的试题列表
        /// </summary>
        /// <param name="questionBankID">题库ID</param>
        /// <param name="pageIndex">页号</param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="totalRecord">输出，总记录数</param>
        /// <returns>试题列表</returns>
        public IList<Question> GetQuestionByQuestionBankID(Guid questionBankID, QuestionSearch search, int pageIndex, int pageSize, out int totalRecord)
        {
            return this.QuestionBankDao.GetQuestionByQuestionBankID(questionBankID, search, pageIndex, pageSize, out totalRecord);
        }
        #endregion

        #region 操作分类树
        [Transaction]
        public Guid InsertUpSiblingNode(Guid selectedNodeID, QuestionBank newQuestionBank)
        {
            QuestionBank selectedNode = this.QuestionBankDao.GetByID(selectedNodeID);
            if (selectedNode == null)
            {
                throw new ETMS.AppContext.BusinessException("ItemBank.SelectedNodeIsInvalid");
            }
            else
            {
                newQuestionBank.QuestionBankID = (newQuestionBank.QuestionBankID == Guid.Empty) ? Guid.NewGuid() : newQuestionBank.QuestionBankID;
                newQuestionBank.ChildrenMaxIdentity = 0;
                newQuestionBank.CreatedUserID = ETMS.AppContext.UserContext.Current.UserID;
                newQuestionBank.Depth = selectedNode.Depth;
                int mixOrder = newQuestionBank.DisplayOrder = selectedNode.DisplayOrder;
                Guid parentID = newQuestionBank.ParentQuestionBankID = selectedNode.ParentQuestionBankID;
                // 更新节点的显示顺序
                this.QuestionBankDao.UpdateNodeIndex(parentID, mixOrder);
                // 添加新节点
                this.QuestionBankDao.Add(newQuestionBank);
                return newQuestionBank.QuestionBankID;
            }
        }

        public Guid InsertDownSiblingNode(Guid selectedNodeID, QuestionBank newQuestionBank)
        {
            QuestionBank selectedNode = this.QuestionBankDao.GetByID(selectedNodeID);
            if (selectedNode == null)
            {
                throw new ETMS.AppContext.BusinessException("ItemBank.SelectedNodeIsInvalid");
            }
            else
            {
                newQuestionBank.QuestionBankID = (newQuestionBank.QuestionBankID == Guid.Empty) ? Guid.NewGuid() : newQuestionBank.QuestionBankID;
                newQuestionBank.ChildrenMaxIdentity = 0;
                newQuestionBank.CreatedUserID = ETMS.AppContext.UserContext.Current.UserID;
                newQuestionBank.Depth = selectedNode.Depth;
                newQuestionBank.DisplayOrder = ++selectedNode.DisplayOrder;
                Guid parentID = newQuestionBank.ParentQuestionBankID = selectedNode.ParentQuestionBankID;
                // 更新节点的显示顺序
                this.QuestionBankDao.UpdateNodeIndex(parentID, newQuestionBank.DisplayOrder);
                // 添加新节点
                this.QuestionBankDao.Add(newQuestionBank);
                return newQuestionBank.QuestionBankID;
            }
        }

        public void SwitchQuestionIndex(Guid upNodeID, Guid downNodeID)
        {
            this.QuestionBankDao.SwitchQuestionIndex(upNodeID, downNodeID);
        }

        public void AddIndent(Guid selectedNodeID, Guid upNodeID)
        {
            QuestionBank selectedNode = this.QuestionBankDao.GetByID(selectedNodeID);
            if (selectedNode == null)
            {
                throw new ETMS.AppContext.BusinessException("ItemBank.SelectedNodeIsInvalid");
            }
            else
            {
                if (selectedNode.Depth == 3 || (selectedNode.Depth == 2 && selectedNode.ChildrenMaxIdentity > 0))
                    throw new ETMS.AppContext.BusinessException("ItemBank.CatAddIndent");
                else
                {
                    selectedNode.ParentQuestionBankID = upNodeID;
                    selectedNode.Depth += 1;
                    selectedNode.DisplayOrder = ++this.QuestionBankDao.GetByID(upNodeID).ChildrenMaxIdentity;
                    this.QuestionBankDao.DeleteByID(selectedNodeID);
                    this.QuestionBankDao.Add(selectedNode);
                }
            }
        }

        public void ReduceIndent(Guid selectedNodeID, Guid parentNodeID)
        {
            QuestionBank selectedNode = this.QuestionBankDao.GetByID(selectedNodeID);
            QuestionBank parentNode = this.QuestionBankDao.GetByID(parentNodeID);
            if (selectedNode == null || parentNode == null)
            {
                throw new ETMS.AppContext.BusinessException("ItemBank.SelectedNodeIsInvalid");
            }
            else
            {
                if (selectedNode.Depth == 1)
                    throw new ETMS.AppContext.BusinessException("ItemBank.CatReduceIndent");
                else
                {
                    selectedNode.ParentQuestionBankID = parentNode.ParentQuestionBankID;
                    selectedNode.Depth = parentNode.Depth;
                    selectedNode.DisplayOrder = ++this.QuestionBankDao.GetByID(parentNode.ParentQuestionBankID).ChildrenMaxIdentity;
                    this.QuestionBankDao.DeleteByID(selectedNodeID);
                    this.QuestionBankDao.Add(selectedNode);
                }
            }
        }
        #endregion

        #region IInitializingObject 成员

        public void AfterPropertiesSet()
        {
            if (this.QuestionBankDao == null)
                throw new Exception("please set QuestionBankDao Property First!");
        }

        #endregion

        #region IMessageSourceAware 成员

        public IMessageSource MessageSource { get; set; }

        #endregion
    }
}
