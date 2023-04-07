using System;
using System.Collections.Generic;
using System.Text;
using Autumn.Data.ORM.IBatis;
using ETMS.Components.Exam.API.Entity.Test;
using ETMS.Components.Exam.API.Entity.ItemBank;
using System.Collections;
using ETMS.AppContext;
using ETMS.Utility;
using System.Data.SqlClient;
using ETMS.Utility.Data;
using System.Data;
namespace ETMS.Components.Exam.Implement.DAL.Test
{
    public class PaperQuestionIBatisDao : ReadWriteDataMapperDaoSupport, IPaperQuestionDao
    {
        public IList<QuestionTypeCnt> GetQuestionTypeCntInPaper(Guid testPaperID)
        {
            return this.DataMapperClient_Read.QueryForList<QuestionTypeCnt>("Test.PaperQuestion.GetQuestionTypeCntInPaper", testPaperID);
        }

        public void UpdateQuestionTypeScoreInPaper(Guid testPaperID, QuestionType questionType, decimal score)
        {
            this.DataMapperClient_Write.Update("Test.PaperQuestion.UpdateQuestionTypeScoreInPaper", new { TestPaperID = testPaperID, TheType = questionType, QuestionScore = score });
        }

        public void UpdateQuestionScore(Guid testPaperID, Guid paperQuestionID, decimal score)
        {
            this.DataMapperClient_Write.Update("Test.PaperQuestion.UpdateQuestionScore", new { TestPaperID = testPaperID, QuestionID = paperQuestionID, QuestionScore = score });
        }
        public void UpdateQuestionSequence(Guid testPaperID, Guid paperQuestionID, int order)
        {
            this.DataMapperClient_Write.Update("Test.PaperQuestion.UpdateQuestionSequence", new { TestPaperID = testPaperID, QuestionID = paperQuestionID, ItemSequence = order });
        }
        public void AddQuestion(PaperQuestion paperQuestion)
        {
            this.DataMapperClient_Write.Insert("Test.PaperQuestion.AddQuestion", paperQuestion);
        }

        public void Delete(Guid testPaperID, Guid paperQuestionID)
        {
            this.DataMapperClient_Write.Delete("Test.PaperQuestion.Delete", new { TestPaperID = testPaperID, QuestionID = paperQuestionID });
        }

        public void Update(Guid oldQuestionID, PaperQuestion newPaperQuestion)
        {
            this.DataMapperClient_Write.Update("Test.PaperQuestion.Update", new { TestPaperID = newPaperQuestion.TestPaperID, OldQuestionID = oldQuestionID, NewQuestionID = newPaperQuestion.QuestionID });
        }

        public IList<PaperQuestionView> FindQuestionView(Guid testPaperID, QuestionType questionType, int pageSize, int pageIndex,out int totalSize)
        {
//            //添加通用分页存储过程参数            
//            Hashtable ht = new Hashtable();
//            ht.Add("tableName", " [KS_TestToQuestion] A INNER JOIN [TK_Question] B ON A.QuestionID=B.QuestionID LEFT OUTER JOIN TK_QuestionBank C ON B.QuestionBankID=C.QuestionBankID");//单表名称或多表join关联语句【必填】
//            ht.Add("fields", @" A.[TestPaperID],A.[QuestionID],A.[QuestionType],A.[ItemSequence],A.[QuestionScore],
//                A.[CreatedUserID],A.[CreatedDate],B.[QuestionTitle],B.[Difficulty],B.[ObjectID],B.[Subject],C.[QuestionBankName]");//输出字段列表【必填】
//            if (questionType == QuestionType.Null)
//            {
//                ht.Add("sqlWhere", string.Format(" A.[TestPaperID]='{0}'", testPaperID));//过滤条件【可选】
//            }
//            else
//            {
//                ht.Add("sqlWhere", string.Format(" A.[TestPaperID]='{0}' AND A.[QuestionType]={1}", testPaperID, Convert.ToInt16(questionType)));//过滤条件【可选】
//            }
//            ht.Add("groupField", ""); //分组条件及having子句   【可选】        
//            ht.Add("orderField", " A.QuestionType asc");//排序条件 【必填】
//            ht.Add("pageIndex", pageIndex);//页号【必填】
//            ht.Add("pageSize", pageSize);//每页记录数【必填】
//            ht.Add("totalRecord", 0);//总记录数【输出】

//            IList<PaperQuestionView> result = DataMapperClient_Read.QueryForList<PaperQuestionView>("Test.PaperQuestion.FindQuestionView", ht);
//            //获取总记录数
//            totalSize = (int)ht["totalRecord"];

            string commandName = "dbo.pr_Exam_sp_super_page_New";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@TestPaperID", SqlDbType.UniqueIdentifier),
					new SqlParameter("@QuestionType", SqlDbType.Int),
					new SqlParameter("@pageIndex", SqlDbType.Int),
					new SqlParameter("@pageSize", SqlDbType.Int),
					new SqlParameter("@totalRecord", SqlDbType.Int, 4, ParameterDirection.ReturnValue, false, 0, 0, String.Empty, DataRowVersion.Default, null)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = testPaperID;
            parms[1].Value = questionType == QuestionType.Null ? -1 : Convert.ToInt16(questionType);           
            parms[2].Value = pageIndex;
            parms[3].Value = pageSize;

            #endregion
            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];
            totalSize = (int)parms[4].Value;

            IList<PaperQuestionView> result = new List<PaperQuestionView>();
            foreach (DataRow row in dt.Rows)
            {
                result.Add(PopulatePaperQuestionViewFromDataRow(row));
            }

            return result;
        }

        
		/// <summary>
		/// 从DataRow中读取数据到业务对象
		/// </summary>
        private PaperQuestionView PopulatePaperQuestionViewFromDataRow(DataRow row)
        {
            PaperQuestionView paview = new PaperQuestionView();

            if (!Convert.IsDBNull(row["TestPaperID"]))
            {
                paview.TestPaperID = row["TestPaperID"].ToGuid();
            }
            if (!Convert.IsDBNull(row["QuestionID"]))
            {
                paview.QuestionID = row["QuestionID"].ToGuid();
            }
            if (!Convert.IsDBNull(row["QuestionType"]))
            {
                paview.QuestionType = (QuestionType)row["QuestionType"].ToInt();
            }
            if (!Convert.IsDBNull(row["ItemSequence"]))
            {
                paview.ItemSequence = row["ItemSequence"].ToInt();
            }
            if (!Convert.IsDBNull(row["QuestionScore"]))
            {
                paview.QuestionScore = (decimal)row["QuestionScore"];
            }
            if (!Convert.IsDBNull(row["CreatedUserID"]))
            {
                paview.CreatedUserID =row["CreatedUserID"].ToInt();
            }
            if (!Convert.IsDBNull(row["CreatedDate"]))
            {
                paview.CreatedDate = row["CreatedDate"].ToDateTime();
            }
            if (!Convert.IsDBNull(row["QuestionTitle"]))
            {
                paview.QuestionTitle = row["QuestionTitle"].ToString();
            }
            if (!Convert.IsDBNull(row["Difficulty"]))
            {
                paview.Difficulty = (short)row["Difficulty"];
            }
            return paview;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="testPaperID"></param>
        /// <param name="type"></param>
        /// <param name="difficulty"></param>
        /// <param name="questionBankID"></param>
        /// <returns></returns>
        public Guid GetReplaceQuestionID(Guid testPaperID,QuestionType type, short difficulty, Guid questionBankID)
        {
            return this.DataMapperClient_Read.QueryForObject<Guid>("Test.PaperQuestion.GetReplaceQuestionID", new { TestPaperID = testPaperID, TheType = type, Difficulty = difficulty, QuestionBankID = questionBankID });
        }

        public void ReplaceQuestionID(Guid testPaperID, Guid oldQuestionID, Guid newQuestionID)
        {
            this.DataMapperClient_Write.Update("Test.PaperQuestion.ReplaceQuestionID", new { TestPaperID = testPaperID, OldQuestionID = oldQuestionID, NewQuestionID = newQuestionID });
        }

        public PaperQuestionView GetNewQuestionView(Guid testPaperID, Guid questionID)
        {
            return this.DataMapperClient_Read.QueryForObject<PaperQuestionView>("Test.PaperQuestion.GetNewQuestionView", new { TestPaperID = testPaperID, QuestionID = questionID });
        }
        ///<summary>
        /// 从题库中查询试题，指定分类、指定题型、指定难度等条件进行试题查询。
        ///</summary>
        public IList<Question> FindTKQuestion(Guid ownerID,string existIDs, Guid? questionCategory, QuestionType questionType, int difficulty, string questionTitle, int pageSize, int pageIndex, out int totalCount)
        {
            //添加通用分页存储过程参数            
            Hashtable ht = new Hashtable();
            //ht.Add("tableName", " TK_QuestionBank A INNER JOIN TK_Question B ON A.QuestionBankID=B.QuestionBankID");//单表名称或多表join关联语句【必填】
            ht.Add("tableName", " KS_TreeCategory A INNER JOIN TK_Question B ON A.CategoryID=B.QuestionBankID");
            ht.Add("fields", @" B.QuestionID, B.QuestionType, B.QuestionTitle,B.[ObjectID]
                              ,B.QuestionBankID
                              ,B.[Subject]
                              ,B.[KnowledgePoints]
                              ,B.[Difficulty]
                              ,B.[Answers]
                              ,B.[RandomFlag]
                              ,B.[SubItemIndex]
                              ,B.[SourceQuestionID]
                              ,B.[CreatedUserID]
                              ,B.[CreatedDate]
                              ,B.[ParentQuestionID],A.CategoryName QuestionBankName");//输出字段列表【必填】
            StringBuilder sb = new StringBuilder(" B.IsDelete=0 ");  // and B.AuditStatus=99 去掉审核通过检查

            if (ownerID != Guid.Empty)
              sb.AppendFormat("and A.OwnerID='{0}' ", ownerID);

            if (!string.IsNullOrEmpty(existIDs))
                sb.AppendFormat(" AND B.QuestionID not in ({0})", existIDs);
            if (questionCategory != Guid.Empty)
                sb.AppendFormat(" AND B.QuestionBankID='{0}'", questionCategory);
            if (questionType != QuestionType.Null)
                sb.AppendFormat(" AND B.QuestionType={0}", Convert.ToInt16(questionType));
            if (difficulty != 0)
                sb.AppendFormat(" AND B.Difficulty={0}", difficulty);
            if (!string.IsNullOrEmpty(questionTitle))
                sb.AppendFormat(" AND B.QuestionTitle like '%{0}%'", questionTitle.ToSafeSQLValue());
            ht.Add("sqlWhere", sb.ToString());//过滤条件【可选】
            ht.Add("groupField", ""); //分组条件及having子句   【可选】        
            ht.Add("orderField", " B.CreatedDate desc");//排序条件 【必填】
            ht.Add("pageIndex", pageIndex);//页号【必填】
            ht.Add("pageSize", pageSize);//每页记录数【必填】
            ht.Add("totalRecord", 0);//总记录数【输出】

            IList<Question> result = DataMapperClient_Read.QueryForList<Question>("Test.PaperQuestion.FindTKQuestion", ht);
            //获取总记录数
            totalCount = (int)ht["totalRecord"];
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="testPaperID"></param>
        /// <returns></returns>
        public int GetTestPaperMaxQuestionIndex(Guid testPaperID)
        {
            return this.DataMapperClient_Read.QueryForObject<int>("Test.PaperQuestion.GetTestPaperMaxQuestionIndex", testPaperID);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="testPaperID"></param>
        /// <returns></returns>
        public IList<Guid> GetQuestionIDsByTestPaperID(Guid testPaperID)
        {
            return this.DataMapperClient_Read.QueryForList<Guid>("Test.PaperQuestion.GetQuestionIDsByTestPaperID", testPaperID);
        }

        public IList<PaperQuestionView> GetQuestionViewList(Guid testPaperID)
        {
            return this.DataMapperClient_Read.QueryForList<PaperQuestionView>("Test.PaperQuestion.GetQuestionViewList", testPaperID);
        }

        public void UpdateTestpaperTotalQuantity(Guid testpaperID, int total, decimal score = 100M)
        {
            this.DataMapperClient_Write.Update("Test.PaperQuestion.UpdateTestpaperTotalQuantity",
                new { TestPaperID = testpaperID, TotalQuantity = total,TotalScore=score, UpdatedUserID = UserContext.Current.UserID });
        }
        public void TestPaperDeleteQuestion(Guid testpaperID)
        {
            this.DataMapperClient_Write.Delete("Test.PaperQuestion.TestPaperDeleteQuestion", testpaperID);
        }
    }
}
