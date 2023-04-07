using System;
using System.Collections.Generic;
using ETMS.Components.Exam.API.Entity.NewTestPaper;
using System.Data.SqlClient;
using ETMS.Utility.Data;
using System.Data;
using ETMS.Utility;

namespace ETMS.Components.Exam.Implement.DAL.NewTestPaper
{
    public abstract class AExamHomeWorkData
    {

        /// <summary>
        /// 试卷转化实体
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        protected Paper PopulateKS_TestPaperFromDataRow(DataRow dr)
        {
            Paper paperEntity = new Paper() { TestPaperID = dr[0].ToGuid(), TestPaperName = dr[1].ToString(), LimitTime = dr[2].ToInt(), OnLineTestID = dr[3].ToGuid() };

            return paperEntity;
        }

        /// <summary>
        /// 查询试题列表
        /// </summary>
        /// <param name="testPaperID"></param>
        /// <returns></returns>
        public IList<Question> GetTestPaperQuestion(Guid testPaperID,Guid UserExamID)
        {
            string commandName = "Pr_KS_TestToQuestion_GetTestPaperUserQuestion";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@TestPaperID", SqlDbType.UniqueIdentifier),
                    new SqlParameter("@UserExamID", SqlDbType.UniqueIdentifier)
                    
                };
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }
            parms[0].Value = testPaperID;
            parms[1].Value = UserExamID;
            #endregion
            DataTable tab = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];

            return PopulateKS_TestToUserQuestionFromDataRow(tab);

        }


        /// <summary>
        /// 查询试题列表
        /// </summary>
        /// <param name="testPaperID"></param>
        /// <returns></returns>
        public IList<Question> GetTestPaperQuestion(Guid testPaperID)
        {
            string commandName = "Pr_KS_TestToQuestion_GetTestPaperQuestion";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@TestPaperID", SqlDbType.UniqueIdentifier)

                };
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }
            parms[0].Value = testPaperID;
            #endregion
            DataTable tab = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];

            return PopulateKS_TestToQuestionFromDataRow(tab);

        }

        /// <summary>
        /// 试题转化实体
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private IList<Question> PopulateKS_TestToUserQuestionFromDataRow(DataTable dt)
        {
            IList<Question> questions = new List<Question>();
            foreach (DataRow dr in dt.Rows)
            {
                //获取用户作答结果
                QuestionType questionType = (QuestionType)Enum.Parse(typeof(QuestionType), dr[2].ToString());
                List<UserAnswer> userAnswers = new List<UserAnswer>();
                UserAnswer userAnswer;
                string strUserAnswer=string.Empty;
                string sourceUserAnswer = dr[7].ToString();
                //选项分单选题、多选题、判断题
                switch (questionType)
                {
                    case QuestionType.Null:
                        break;
                    case QuestionType.SingleChoice:
                    case QuestionType.Judgement:
                        if(!string.IsNullOrEmpty(sourceUserAnswer))
                        {
                            userAnswer = JsonHelper.DeserializeObject<UserAnswer>(sourceUserAnswer);
                            strUserAnswer = userAnswer.OptionID.ToString();
                        }
                        break;
                    case QuestionType.MultipleChoice:
                        if (!string.IsNullOrEmpty(sourceUserAnswer))
                        {
                            userAnswers = JsonHelper.DeserializeObject<List<UserAnswer>>(sourceUserAnswer);
                            foreach (UserAnswer answer in userAnswers)
                            {
                                strUserAnswer += answer.OptionID.ToString() + ",";
                            }
                            strUserAnswer = strUserAnswer.TrimEnd(',');
                        }
                        break;
                    case QuestionType.TextEntry:
                        break;
                    case QuestionType.ExtendedText:
                        break;
                    case QuestionType.Match:
                        break;
                    case QuestionType.Group:
                        break;
                    default:
                        break;
                }

                Question question = new Question { TestPaperID = dr[0].ToGuid(), QuestionID = dr[1].ToGuid(), QuestionType = dr[2].ToInt(), QuestionScore = decimal.Parse(dr[3].ToString()), QuestionTitle = dr[4].ToString(), QuestionAnswer = dr[5].ToString(), ItemSequence=dr[6].ToInt(),UserAnswer= strUserAnswer, UserScore=dr[8].ToString().ToDecimal() };
                questions.Add(question);
            }

            return questions;
        }
        /// <summary>
        /// 试题转化实体
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private IList<Question> PopulateKS_TestToQuestionFromDataRow(DataTable dt)
        {
            IList<Question> questions = new List<Question>();
            foreach (DataRow dr in dt.Rows)
            {
                Question question = new Question { TestPaperID = dr[0].ToGuid(), QuestionID = dr[1].ToGuid(), QuestionType = dr[2].ToInt(), QuestionScore = decimal.Parse(dr[3].ToString()), QuestionTitle = dr[4].ToString(), QuestionAnswer = dr[5].ToString(), ItemSequence = dr[6].ToInt() };
                questions.Add(question);
            }

            return questions;
        }
        /// <summary>
        /// 查询试题选项
        /// </summary>
        /// <param name="testPaperID"></param>
        /// <returns></returns>
        public IList<Option> GetQuestionOption(Guid testPaperID)
        {
            string commandName = "Pr_KS_QuestionOption_GetQuestionOption";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@TestPaperID", SqlDbType.UniqueIdentifier)
                };
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }
            parms[0].Value = testPaperID;

            #endregion
            DataTable tab = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];

            return PopulateKS_QuestionOptionFromDataRow(tab);

        }

        /// <summary>
        /// 选项转化实体
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private IList<Option> PopulateKS_QuestionOptionFromDataRow(DataTable dt)
        {
            IList<Option> options = new List<Option>();
            foreach (DataRow dr in dt.Rows)
            {
                Option opeion = new Option { QuestionID = dr[0].ToGuid(), OptionID = dr[1].ToGuid(), OptionCode = dr[2].ToString(), OptionContent = dr[3].ToString() };
                options.Add(opeion);
            }

            return options;
        }

        /// <summary>
        /// 查询试题的答案
        /// </summary>
        /// <param name="userExamID"></param>
        /// <returns></returns>
        public IList<QuestionResult> GetQuestionResult(Guid userExamID)
        {
            string commandName = "Pr_KS_UserExamResult_GetQuestionResult";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@UserExamID", SqlDbType.UniqueIdentifier)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }
            parms[0].Value = userExamID;

            #endregion
            DataTable tab = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];

            return PopulateKS_UserExamResultFromDataRow(tab);


        }

        /// <summary>
        /// 试题答案转化实体
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private IList<QuestionResult> PopulateKS_UserExamResultFromDataRow(DataTable dt)
        {
            IList<QuestionResult> questionResults = new List<QuestionResult>();
            foreach (DataRow dr in dt.Rows)
            {
                QuestionResult questionResult = new QuestionResult() { UserExamID = dr[0].ToGuid(), QuestionID = dr[1].ToGuid(), UserAnswer = dr[2].ToString(), UserScore = decimal.Parse(dr[3].ToString()) };
                questionResults.Add(questionResult);
            }

            return questionResults;
        }




        /// <summary>
        /// 保存试题
        /// </summary>
        /// <param name="userExamID"></param>
        /// <param name="testPaperID"></param>
        /// <param name="questionID"></param>
        /// <param name="questionName"></param>
        /// <param name="questionAnswer"></param>
        /// <param name="userAnswer"></param>
        /// <param name="userScore"></param>
        /// <param name="userID"></param>
        public void SaveQuestionAnswer(Guid userExamID, Guid testPaperID, Guid questionID, string questionName, string questionAnswer, string userAnswer, decimal userScore, int userID)
        {
            string commandName = "Pr_KS_UserExamResult_SaveQuestionAnswer";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@UserExamID", SqlDbType.UniqueIdentifier),
                    new SqlParameter("@TestPaperID", SqlDbType.UniqueIdentifier),
                    new SqlParameter("@QuestionID", SqlDbType.UniqueIdentifier),
                    new SqlParameter("@QuestionName", SqlDbType.NVarChar,512),
                    new SqlParameter("@QuestionAnswer", SqlDbType.NVarChar,512),
                    new SqlParameter("@UserAnswer", SqlDbType.NVarChar,512),
                    new SqlParameter("@UserScore", SqlDbType.Decimal),
                    new SqlParameter("@UserID", SqlDbType.Int)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }
            parms[0].Value = userExamID;
            parms[1].Value = testPaperID;
            parms[2].Value = questionID;
            parms[3].Value = questionName;
            parms[4].Value = questionAnswer;
            parms[5].Value = userAnswer;
            parms[6].Value = userScore;
            parms[7].Value = userID;
            #endregion
            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms);
        }

        public void CopyTestPaperQuestionInfo(Guid testPaperID)
        {
            string commandName = "Pr_KS_Question_CopyData";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@TestPaperID", SqlDbType.UniqueIdentifier)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }
            parms[0].Value = testPaperID;
            #endregion
            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms);

        }

        public abstract UserTestPaper GetUserTestPaper(Guid userExamID, Guid onLineTestID);

        /// <summary>
        /// 开始考试
        /// </summary>
        /// <param name="studentID"></param>
        /// <param name="testPaperID"></param>
        /// <returns></returns>
        public abstract Guid StartNewTest(int studentID, Guid testPaperID, Guid trainingItemCourseID, Guid onlineTestID, Guid studentCourseID);

        /// <summary>
        /// 查询试卷考试信息
        /// </summary>
        /// <param name="testPaperID">试卷ID</param>
        /// <param name="onlineTestID">测试ID</param>
        /// <returns></returns>
        public abstract Paper GetExamTestPaper(Guid testPaperID, Guid onlineTestID);

        /// <summary>
        /// 提交试卷
        /// </summary>
        /// <param name="userExamID"></param>
        /// <param name="status"></param>
        public abstract void SubmitTestPaper(Guid userExamID, int status, int userID, Guid testPaperID);

    }
}
