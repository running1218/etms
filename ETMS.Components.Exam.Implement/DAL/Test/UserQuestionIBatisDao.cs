using System;
using System.Collections.Generic;
using Autumn.Data.ORM.IBatis;
using ETMS.Components.Exam.API.Entity.Test;
using ETMS.Components.Exam.API.Entity.ItemBank;
using System.Data.SqlClient;
using ETMS.Utility.Data;
using System.Data;
using ETMS.Utility;

namespace ETMS.Components.Exam.Implement.DAL.Test
{
    public class UserQuestionIBatisDao : ReadWriteDataMapperDaoSupport, IUserQuestionDao
    {
        #region IUserQuestionDao 成员

        public void AddExamQuestion(UserQuestion ExamQuestion)
        {
            var oValue = new { ExamQuestion = ExamQuestion, UserID = AppContext.UserContext.Current.UserID };

            DataMapperClient_Write.Insert("Test.UserQuestion.AddExamQuestion", oValue);
        }

        /// <summary>
        /// 查询出某个用户的考试试题  edit zhangsz 2013-09-02
        /// </summary>
        /// <param name="UserExamID"></param>
        /// <returns></returns>
        public IList<UserQuestion> FindQuestionsInUserExam(Guid UserExamID)
        {
            //IList<UserQuestion> LstExams = DataMapperClient_Read.QueryForList<UserQuestion>(
            //    "Test.UserQuestion.FindQuestionsInUserExam", UserExamID);

            //return LstExams;

            string commandName = "dbo.Pr_KS_KS_ExamQuestionGetListByUserExamID";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@UserExamID", SqlDbType.UniqueIdentifier)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = UserExamID;

            #endregion
            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];

            IList<UserQuestion> LstExams = new List<UserQuestion>();
            foreach (DataRow row in dt.Rows)
            {
                LstExams.Add(PopulateUserQuestionFromDataRow(row));
            }
            return LstExams;
        }

        /// <summary>
        /// 从DataRow中读取数据到业务对象
        /// </summary>
        private UserQuestion PopulateUserQuestionFromDataRow(DataRow row)
        {
            UserQuestion userQuestion = new UserQuestion();
            if (!Convert.IsDBNull(row["ExamQuestionID"]))
            {
                userQuestion.ExamQuestionID = row["ExamQuestionID"].ToGuid();
            }
            if (!Convert.IsDBNull(row["UserExamID"]))
            {
                userQuestion.UserExamID = row["UserExamID"].ToGuid();
            }
            if (!Convert.IsDBNull(row["TestPaperID"]))
            {
                userQuestion.TestPaperID = row["TestPaperID"].ToGuid();
            }
            if (!Convert.IsDBNull(row["QuestionID"]))
            {
                userQuestion.QuestionID = row["QuestionID"].ToGuid();
            }
            if (!Convert.IsDBNull(row["ParentQuestionID"]))
            {
                userQuestion.ParentQuestionID = row["ParentQuestionID"].ToGuid();
            }
            if (!Convert.IsDBNull(row["SubItemIndex"]))
            {
                userQuestion.SubItemIndex = row["SubItemIndex"].ToInt();
            }
            if (!Convert.IsDBNull(row["QuestionType"]))
            {
                userQuestion.QuestionType = (QuestionType)row["QuestionType"].ToInt();
            }
            if (!Convert.IsDBNull(row["QuestionTitle"]))
            {
                userQuestion.QuestionTitle = row["QuestionTitle"].ToString();
            }
            if (!Convert.IsDBNull(row["ObjectID"]))
            {
                userQuestion.ObjectID = row["ObjectID"].ToInt();
            }
            if (!Convert.IsDBNull(row["Subject"]))
            {
                userQuestion.Subject = row["Subject"].ToString();
            }
            if (!Convert.IsDBNull(row["KnowledgePoints"]))
            {
                userQuestion.KnowledgePoints = row["KnowledgePoints"].ToString();
            }
            if (!Convert.IsDBNull(row["Difficulty"]))
            {
                userQuestion.Difficulty = row["Difficulty"].ToInt();
            }
            if (!Convert.IsDBNull(row["QuestionNo"]))
            {
                userQuestion.QuestionNo = row["QuestionNo"].ToInt();
            }
            if (!Convert.IsDBNull(row["QuestionScore"]))
            {
                userQuestion.QuestionScore =(decimal)row["QuestionScore"];
            }
            //if (!Convert.IsDBNull(row["UserAnswer"]))
            //{
            //    userQuestion.UserAnswer =AnswerBase.Deserialize<AnswerBase>(row["UserAnswer"].ToString());
            //}
            if (!Convert.IsDBNull(row["Answer"]))
            {
                userQuestion.AnswerStr = row["Answer"].ToString();
            }
            if (!Convert.IsDBNull(row["UserAnswer"]))
            {
                userQuestion.UserAnswerStr = row["UserAnswer"].ToString();
            }
            return userQuestion;
        }


        public bool DeleteQuestionsInUserExam(Guid UserExamID)
        {
            int nRowCnt = 0;
            nRowCnt = DataMapperClient_Write.Delete("Test.UserQuestion.DeleteQuestionsInUserExam",
                new
                {
                    UserExamID = UserExamID,
                    UserID = AppContext.UserContext.Current.UserID
                });

            return nRowCnt > 0;
        }

        public bool DeleteQuestionByQuestionID(Guid UserExamID, Guid QuestionID)
        {
            int nRowCnt = 0;
            nRowCnt = DataMapperClient_Write.Delete("Test.UserQuestion.DeleteQuestionByQuestionID",
                new
                {
                    UserExamID = UserExamID,
                    QuestionID = QuestionID,
                    UserID = AppContext.UserContext.Current.UserID
                });

            return nRowCnt > 0;
        }

        public void AddExamOption(TestQuestionOption ExamQuestion)
        {
            var oValue = new { TestQuestionOption = ExamQuestion, UserID = AppContext.UserContext.Current.UserID };

            DataMapperClient_Write.Insert("Test.UserQuestion.AddExamOption", oValue);
        }

        /// <summary>
        /// 查询出某个用户的考试试题选项  edit zhangsz 2013-09-03
        /// </summary>
        /// <param name="UserExamID"></param>
        /// <returns></returns>
        public IList<TestQuestionOption> FindOptionsInUserExam(Guid UserExamID)
        {
            //IList<TestQuestionOption> LstOptions = DataMapperClient_Read.QueryForList<TestQuestionOption>(
            //    "Test.UserQuestion.FindOptionsInUserExam", UserExamID);

            //return LstOptions;

            string commandName = "dbo.Pr_KS_KS_ExamQuestionOptionGetListByUserExamID";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@UserExamID", SqlDbType.UniqueIdentifier)
                };
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = UserExamID;

            #endregion
            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];

            IList<TestQuestionOption> LstOptions = new List<TestQuestionOption>();
            foreach (DataRow row in dt.Rows)
            {
                LstOptions.Add(PopulateTestQuestionOptionFromDataRow(row));
            }
            return LstOptions;

        }

        private TestQuestionOption PopulateTestQuestionOptionFromDataRow(DataRow row)
        {
            TestQuestionOption option = new TestQuestionOption();
            if (!Convert.IsDBNull(row["UserExamID"]))
            {
                option.UserExamID = row["UserExamID"].ToGuid();
            }
            if (!Convert.IsDBNull(row["QuestionID"]))
            {
                option.QuestionID = row["QuestionID"].ToGuid();
            }
            if (!Convert.IsDBNull(row["OptionID"]))
            {
                option.OptionID = row["OptionID"].ToGuid();
            }
            if (!Convert.IsDBNull(row["QuestionOptionCode"]))
            {
                option.QuestionOptionCode = row["QuestionOptionCode"].ToString();
            }
            if (!Convert.IsDBNull(row["OptionCode"]))
            {
                option.OptionCode = row["OptionCode"].ToString();
            }
            if (!Convert.IsDBNull(row["OptionContent"]))
            {
                option.OptionContent = row["OptionContent"].ToString();
            }
            return option;
        }

        public bool DeleteOptionsInUserExam(Guid UserExamID)
        {
            int nRowCnt = 0;
            nRowCnt = DataMapperClient_Write.Delete("Test.UserQuestion.DeleteOptionsInUserExam",
                new
                {
                    UserExamID = UserExamID,
                    UserID = AppContext.UserContext.Current.UserID
                });

            return nRowCnt > 0;
        }

        public bool DeleteOptionsByQuestionID(Guid UserExamID, Guid QuestionID)
        {
            int nRowCnt = 0;
            nRowCnt = DataMapperClient_Write.Delete("Test.UserQuestion.DeleteOptionsByQuestionID",
                new
                {
                    UserExamID = UserExamID,
                    QuestionID = QuestionID,
                    UserID = AppContext.UserContext.Current.UserID
                });

            return nRowCnt > 0;
        }


        public void AddExamOptionGroups(TestOptionGroup ExamQuestion)
        {
            var oValue = new { TestOptionGroup = ExamQuestion, UserID = AppContext.UserContext.Current.UserID };

            DataMapperClient_Write.Insert("Test.UserQuestion.AddExamOptionGroups", oValue);
        }

        public IList<TestOptionGroup> FindOptionGroupsInUserExam(Guid UserExamID)
        {
            IList<TestOptionGroup> LstGroups = DataMapperClient_Read.QueryForList<TestOptionGroup>(
                "Test.UserQuestion.FindOptionGroupsInUserExam", UserExamID);

            return LstGroups;
        }

        public bool DeleteOptionGroupsInUserExam(Guid UserExamID)
        {
            int nRowCnt = 0;
            nRowCnt = DataMapperClient_Write.Delete("Test.UserQuestion.DeleteOptionGroupsInUserExam",
                new
                {
                    UserExamID = UserExamID,
                    UserID = AppContext.UserContext.Current.UserID
                });

            return nRowCnt > 0;
        }

        public bool DeleteOptionGroupsByQuestionID(Guid UserExamID, Guid QuestionID)
        {
            int nRowCnt = 0;
            nRowCnt = DataMapperClient_Write.Delete("Test.UserQuestion.DeleteOptionGroupsByQuestionID",
                new
                {
                    UserExamID = UserExamID,
                    QuestionID=QuestionID,
                    UserID = AppContext.UserContext.Current.UserID
                });

            return nRowCnt > 0;
        }

        #endregion

        #region --得到试题反馈、选项反馈、解题思路--


        public IList<QuestionFeedback> GetQuestionFeedbackByQuestion(Guid UserExamID, Guid QuestionID)
        {
            IList<QuestionFeedback> LstExams = DataMapperClient_Read.QueryForList<QuestionFeedback>(
                "Test.UserQuestion.GetQuestionFeedbackByQuestion", 
                new
                {
                    UserExamID = UserExamID,
                    QuestionID = QuestionID
                });

            return LstExams;
        }

        public IList<OptionFeedback> GetOptionFeedbackByQuestion(Guid UserExamID, Guid QuestionID)
        {
            IList<OptionFeedback> LstExams = DataMapperClient_Read.QueryForList<OptionFeedback>(
                "Test.UserQuestion.GetOptionFeedbackByQuestion", 
                new
                {
                    UserExamID = UserExamID,
                    QuestionID = QuestionID
                });

            return LstExams;
        }

        public QuestionExtend GetQuestionExtendByQuestionID(Guid UserExamID, Guid QuestionID)
        {
            QuestionExtend LstExams = DataMapperClient_Read.QueryForObject<QuestionExtend>(
                "Test.UserQuestion.GetQuestionExtendByQuestionID", 
                new
                {
                    UserExamID = UserExamID,
                    QuestionID = QuestionID
                });

            return LstExams;
        }

        #endregion
    }
}
