using System;
using System.Collections.Generic;
using Autumn.Data.ORM.IBatis;
using ETMS.Components.Exam.API.Entity.Test;
using ETMS.AppContext;
using System.Collections;
using ETMS.Components.Exam.API.Entity;

namespace ETMS.Components.Exam.Implement.DAL.Test
{
    public class TestPaperIBatisDao : ReadWriteDataMapperDaoSupport, ITestPaperDao
    {

        public void AddTestPaper(TestPaper testPaper)
        {
            this.DataMapperClient_Write.Insert("Test.TestPaper.AddTestPaper", testPaper);
        }

        public void Update(TestPaper newTestPaper)
        {
            this.DataMapperClient_Write.Update("Test.TestPaper.UpdateTestPaper", newTestPaper);
        }

        public void Delete(Guid testPaperID)
        {
            this.DataMapperClient_Write.Delete("Test.TestPaper.DeleteTestPaper", new { UpdatedUserID = UserContext.Current.UserID, TestPaperID = testPaperID });
        }

        public TestPaper GetByID(Guid testPaperID)
        {
            return this.DataMapperClient_Read.QueryForObject<TestPaper>("Test.TestPaper.GetTestPaperByID", testPaperID);
        }

        public void UpdateQuestionsCount(Guid testPaperID, int questionsCnt, int totalScore)
        {
            this.DataMapperClient_Write.Update("Test.TestPaper.UpdateQuestionsCount", new { TestPaperID = testPaperID, TotalQuantity = questionsCnt, TotalScore = totalScore, UpdatedUserID = UserContext.Current.UserID });
        }

        public void UpdateExamTimes(Guid testPaperID, int jige, int examTime, int examTimes)
        {
            this.DataMapperClient_Write.Update("Test.TestPaper.UpdateExamTimes", new { TestPaperID = testPaperID, PassedScore = jige, MaxCount = examTimes,MaxTime=examTime, UpdatedUserID = UserContext.Current.UserID });
        }
        /// <summary>
        /// 复制题库的试题数据到考试库的试题备份表
        /// </summary>
        /// <param name="testPaperID"></param>
        public void CopyTKQuestionData(Guid testPaperID)
        {
            this.DataMapperClient_Write.Insert("Test.TestPaper.CopyTKQuestionData", testPaperID);
        }

        public IList<TestPaper> GetTestPaperListByOperator(string sqlWhere, int pageSize, int pageIndex, out int totalSize)
        {
            //添加通用分页存储过程参数            
            Hashtable ht = new Hashtable();
            ht.Add("tableName", @" KS_TestPaper a LEFT OUTER JOIN KS_TreeCategory b ON a.TestPaperCategory=b.CategoryID LEFT OUTER JOIN 
                (SELECT OrganizationID as ID,OrganizationName as OwnerName FROM Organizations UNION SELECT UserID as ID, UserName as OwnerName FROM USERS) c ON b.OwnerID=c.ID ");//单表名称或多表join关联语句【必填】
            ht.Add("fields", " a.TestPaperID,a.TestPaperName,a.TestPaperType,a.Status,c.OwnerName,a.CreatedUserID,a.CreatedDate,ISNULL(a.UpdatedDate,a.CreatedDate) UpdatedDate");//输出字段列表【必填】
            ht.Add("sqlWhere", sqlWhere);
            ht.Add("groupField", ""); //分组条件及having子句   【可选】        
            ht.Add("orderField", " ISNULL(a.UpdatedDate,a.CreatedDate) desc");//排序条件 【必填】
            ht.Add("pageIndex", pageIndex);//页号【必填】
            ht.Add("pageSize", pageSize);//每页记录数【必填】
            ht.Add("totalRecord", 0);//总记录数【输出】

            IList<TestPaper> result = DataMapperClient_Read.QueryForList<TestPaper>("Test.TestPaperSearch.GetResults", ht);
            //获取总记录数
            totalSize = (int)ht["totalRecord"];
            return result;
        }

        public IList<TestPaper> GetMyTestPaperList(string sqlWhere, int pageSize, int pageIndex, out int totalSize)
        {
            //添加通用分页存储过程参数            
            Hashtable ht = new Hashtable();
            ht.Add("tableName", " KS_TestPaper a LEFT OUTER JOIN KS_TreeCategory b ON a.TestPaperCategory=b.CategoryID");//单表名称或多表join关联语句【必填】
            ht.Add("fields", @" a.TestPaperID,a.TestPaperName,a.TestPaperType,a.Status,'' as OwnerName,a.CreatedUserID,a.CreatedDate,ISNULL(a.UpdatedDate,a.CreatedDate) UpdatedDate ");//输出字段列表【必填】
            ht.Add("sqlWhere", sqlWhere);
            ht.Add("groupField", ""); //分组条件及having子句   【可选】        
            ht.Add("orderField", " ISNULL(a.UpdatedDate,a.CreatedDate) desc");//排序条件 【必填】
            ht.Add("pageIndex", pageIndex);//页号【必填】
            ht.Add("pageSize", pageSize);//每页记录数【必填】
            ht.Add("totalRecord", 0);//总记录数【输出】

            IList<TestPaper> result = DataMapperClient_Read.QueryForList<TestPaper>("Test.TestPaperSearch.GetResults", ht);
            //获取总记录数
            totalSize = (int)ht["totalRecord"];
            return result;

        }
        public void SetShareState(Guid testpaperID, int state)
        {
            this.DataMapperClient_Write.Update("Test.TestPaper.SetShareState", new { TestPaperID = testpaperID, UpdatedUserID = UserContext.Current.UserID, ShareState = state });
        }

        public void SetAuditState(Guid testpaperID, int state)
        {
            this.DataMapperClient_Write.Update("Test.TestPaper.SetAuditState", new { TestPaperID = testpaperID, UpdatedUserID = UserContext.Current.UserID, AuditState = state });
        }

        public IList<TestPaperUnit> GetSeniorRandomTestpaperSchema(Guid testpaperID)
        {
            return this.DataMapperClient_Read.QueryForList<TestPaperUnit>("Test.TestPaper.GetSeniorRandomTestpaperSchema", testpaperID);
        }

        public IList<TestPaperUnit> GetCommonTestpaperSchema(Guid testpaperID)
        {
            return this.DataMapperClient_Read.QueryForList<TestPaperUnit>("Test.TestPaper.GetCommonTestpaperSchema", testpaperID);
        }


        public IList<IDName> GetQuestionByType(Guid testpaperID, int type)
        {
            return this.DataMapperClient_Read.QueryForList<IDName>("Test.PaperQuestion.GetQuestionByType", new { TestpaperID = testpaperID, QuestionType = type });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="testpaperID"></param>
        public void SetFixTestPaperScoreAndCount(Guid testpaperID)
        {
            this.DataMapperClient_Write.Update("Test.TestPaper.SetFixTestPaperScoreAndCount", new { TestPaperID = testpaperID, UpdatedUserID = UserContext.Current.UserID });
        }

        public void SetTestPaperCategoryID(string testpaperIDs, Guid categoryID)
        {
            this.DataMapperClient_Write.Update("Test.TestPaper.SetTestPaperCategoryID", new { TestPaperIDs = testpaperIDs, CategoryID = categoryID, UpdatedUserID = UserContext.Current.UserID });
        }
    }
}
