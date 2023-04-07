using System;
using System.Collections.Generic;
using ETMS.Components.Exam.API.Entity.ItemBank;
using Autumn.Data.ORM.IBatis;
using ETMS.AppContext;
using System.Collections;
using ETMS.Utility;
namespace ETMS.Components.Exam.Implement.DAL.ItemBank
{
    public class QuestionBankIBatisDao : ReadWriteDataMapperDaoSupport, IQuestionBankDao
    {
        #region Base 接口
        /// <summary>
        /// 1、添加
        /// </summary>
        /// <param name="questionLibrary">题库实体</param>
        public void Add(QuestionBank questionLibrary)
        {
            this.DataMapperClient_Write.Insert("ItemBank.QuestionBank.Add", questionLibrary);
        }
        /// <summary>
        /// 2、修改
        /// </summary>
        /// <param name="questionLibrary">题库实体</param>
        public void Update(QuestionBank questionLibrary)
        {
            this.DataMapperClient_Write.Update("ItemBank.QuestionBank.Update", questionLibrary);
        }
        /// <summary>
        /// 3、删除
        /// </summary>
        /// <param name="questionBankID">题库ID</param>
        public void Delete(Guid questionBankID, int updatedUserID)
        {
            this.DataMapperClient_Write.Delete("ItemBank.QuestionBank.Delete", new { QuestionBankID = questionBankID, UpdatedUserID = updatedUserID });
        }
        /// <summary>
        /// 4、获取
        /// </summary>
        /// <param name="questionBankID">题库ID</param>
        /// <returns>题库实体</returns>
        public QuestionBank GetByID(Guid questionBankID)
        {
            return this.DataMapperClient_Read.QueryForObject<QuestionBank>("ItemBank.QuestionBank.GetByID", questionBankID);
        }
        #endregion

        #region 分类树操作
        /// <summary>
        /// 更新节点显示顺序
        /// </summary>
        /// <param name="parentID">父节点ID</param>
        /// <param name="mixOrder">需要更新的最小显示顺序</param>
        public void UpdateNodeIndex(Guid parentID, int mixOrder)
        {
            this.DataMapperClient_Write.Update("ItemBank.QuestionBank.UpdateNodeIndex", new { ParentID = parentID, MixOrder = mixOrder, UpdatedUserID = UserContext.Current.UserID });
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="upNodeID"></param>
        /// <param name="downNodeID"></param>
        public void SwitchQuestionIndex(Guid upNodeID, Guid downNodeID)
        {
            this.DataMapperClient_Write.Update("ItemBank.QuestionBank.SwitchQuestionIndex", new { UpNodeID = upNodeID, DownNodeID = downNodeID, UpdatedUserID = UserContext.Current.UserID });
        }
        /// <summary>
        /// 物理删除
        /// </summary>
        /// <param name="questionBankID"></param>
        public void DeleteByID(Guid questionBankID)
        {
            this.DataMapperClient_Write.Delete("ItemBank.QuestionBank.DeleteByID", questionBankID);
        }
        /// <summary>
        /// 根据父节点的ID取直接子结点列表（异步展示题库树）
        /// 取第一级节点时，参数为Guid.Empty
        /// </summary>
        /// <param name="QuestionBankID"></param>
        /// <returns></returns>
        public IList<QuestionBank> GetAllChildrenByParentID(Guid QuestionBankID)
        {
            return this.DataMapperClient_Read.QueryForList<QuestionBank>("ItemBank.QuestionBank.GetAllChildrenByParentID", QuestionBankID);
        }

        /// <summary>
        /// 根据父节点parentPath取直接子结点列表（异步展示题库树）
        /// 取第一级节点时，参数为Guid.Empty
        /// </summary>
        /// <param name="parentPath"></param>
        /// <returns></returns>
        public IList<QuestionBank> GetAllChildrenByParentID(string parentPath)
        {
            return this.DataMapperClient_Read.QueryForList<QuestionBank>("ItemBank.QuestionBank.GetAllChildrenByParentPath", parentPath);
        }

        /// <summary>
        /// 获取课程对应的题库列表
        /// </summary>
        /// <param name="courseID">课程ID</param>
        /// <returns></returns>
        public IList<QuestionBank> GetAllQuestionLibraryByCourseID(Guid courseID)
        {
            return this.DataMapperClient_Read.QueryForList<QuestionBank>("ItemBank.QuestionBank.GetAllQuestionLibraryByCourseID", courseID);
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
            //添加通用分页存储过程参数            
            Hashtable ht = new Hashtable();
            ht.Add("tableName", @" TK_Question ");//单表名称或多表join关联语句【必填】
            ht.Add("fields", @" [QuestionID],[QuestionType],[QuestionTitle],[ParentQuestionID],[ObjectID],[QuestionBankID],[Subject],[KnowledgePoints]
      ,[Difficulty],[Answers],[RandomFlag],[SubItemIndex],[SourceQuestionID],[CreatedUserID],[CreatedDate],[UpdatedDate]");//输出字段列表【必填】
            string sqlWhere = string.Format(" [QuestionBankID]='{0}' and [IsDelete]=0", questionBankID); 
            if (search.Difficulty != 0)//难度
            {
                sqlWhere += string.Format(" and Difficulty={0}", search.Difficulty);
            }
            if (search.Type != QuestionType.Null)//题型
            {
                sqlWhere += string.Format(" and QuestionType={0}", (int)search.Type);
            }
            if (!string.IsNullOrEmpty(search.QuestionTitle))//题目名称
            {
                sqlWhere += string.Format(" and QuestionTitle like '%{0}%'", search.QuestionTitle.ToSafeSQLValue());
            }
            if (!string.IsNullOrEmpty(search.KnowledgePoints))//知识点
            {
                sqlWhere += string.Format(" and KnowledgePoints like '%{0}%'", search.KnowledgePoints.ToSafeSQLValue());
            }
            ht.Add("sqlWhere", sqlWhere);
            ht.Add("groupField", ""); //分组条件及having子句   【可选】        
            ht.Add("orderField", "  [QuestionType],[QuestionTitle]");//排序条件 【必填】
            ht.Add("pageIndex", pageIndex);//页号【必填】
            ht.Add("pageSize", pageSize);//每页记录数【必填】
            ht.Add("totalRecord", 0);//总记录数【输出】

            IList<Question> result = DataMapperClient_Read.QueryForList<Question>("ItemBank.QuestionBank.GetAllQuestion", ht);
            //获取总记录数
            totalRecord = (int)ht["totalRecord"];
            return result;
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
            //添加通用分页存储过程参数            
            Hashtable ht = new Hashtable();
            ht.Add("tableName", @" TK_Question a inner join TK_QuestionBankCourseMap b on a.[QuestionBankID]=b.[QuestionBankID] ");//单表名称或多表join关联语句【必填】
            ht.Add("fields", @" [QuestionID],[QuestionType],[QuestionTitle],[ParentQuestionID],[ObjectID],a.[QuestionBankID],[Subject],[KnowledgePoints]
      ,[Difficulty],[Answers],[RandomFlag],[SubItemIndex],[SourceQuestionID],a.[CreatedUserID],a.[CreatedDate],a.[UpdatedDate]");//输出字段列表【必填】
            string sqlWhere = string.Format(" b.[CourseID]='{0}' and [IsDelete]=0", courseID);
            if (search.Difficulty != 0)//难度
            {
                sqlWhere += string.Format(" and Difficulty={0}", search.Difficulty);
            }
            if (search.Type != QuestionType.Null)//题型
            {
                sqlWhere += string.Format(" and QuestionType={0}", (int)search.Type);
            }
            if (!string.IsNullOrEmpty(search.QuestionTitle))//题目名称
            {
                sqlWhere += string.Format(" and QuestionTitle like '%{0}%'", search.QuestionTitle.ToSafeSQLValue());
            }
            if (!string.IsNullOrEmpty(search.KnowledgePoints))//知识点
            {
                sqlWhere += string.Format(" and KnowledgePoints like '%{0}%'", search.KnowledgePoints.ToSafeSQLValue());
            }
            ht.Add("sqlWhere", sqlWhere);
            ht.Add("groupField", ""); //分组条件及having子句   【可选】        
            ht.Add("orderField", "  a.[QuestionType],a.[QuestionTitle]");//排序条件 【必填】
            ht.Add("pageIndex", pageIndex);//页号【必填】
            ht.Add("pageSize", pageSize);//每页记录数【必填】
            ht.Add("totalRecord", 0);//总记录数【输出】

            IList<Question> result = DataMapperClient_Read.QueryForList<Question>("ItemBank.QuestionBank.GetAllQuestionNew", ht);
            //获取总记录数
            totalRecord = (int)ht["totalRecord"];
            return result;
        }
        #endregion
    }
}
