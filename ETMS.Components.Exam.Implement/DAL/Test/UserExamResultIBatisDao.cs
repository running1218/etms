using System;
using System.Collections.Generic;
using Autumn.Data.ORM.IBatis;
using ETMS.Components.Exam.API.Entity.Test;
using System.Data;

namespace ETMS.Components.Exam.Implement.DAL.Test
{
    public class UserExamResultIBatisDao : ReadWriteDataMapperDaoSupport, IUserExamResultDao
    {
        #region --对考生答案项操作--

        public void Add(UserExamResult examResult)
        {
            var oValue = new { UserExamResult = examResult, UserID = AppContext.UserContext.Current.UserID };

            DataMapperClient_Write.Insert("Test.UserExamResult.Add", oValue);
        }

        public void Update(UserExamResult newResult)
        {
            int nRowCnt = 0;

            nRowCnt = DataMapperClient_Write.Update("Test.UserExamResult.Update",
                new
                {
                    UserExamResult = newResult,
                    UserID = AppContext.UserContext.Current.UserID
                });
        }
        public void Delete(Guid resultID)
        {
            int nRowCnt = 0;
            nRowCnt = DataMapperClient_Write.Delete("Test.UserExamResult.Delete",
                new
                {
                    UserExamResultID = resultID,
                    UserID = AppContext.UserContext.Current.UserID
                });
        }
        public UserExamResult GetByID(Guid ResultID)
        {
            UserExamResult oUserExamResult = DataMapperClient_Read.QueryForObject<UserExamResult>(
                "Test.UserExamResult.GetByID", ResultID);

            return oUserExamResult;
        }
        #endregion

        #region --对考生答案与分数操作--
        public void UpdateUserAnswer(Guid UserExamID, Guid QuestionID, string sUserAnswer)
        {
            int nRowCnt = 0;

            nRowCnt = DataMapperClient_Write.Update("Test.UserExamResult.UpdateUserAnswer",
                new
                {
                    UserExamID = UserExamID,
                    QuestionID = QuestionID,
                    UserAnswer = sUserAnswer,
                    UserID = AppContext.UserContext.Current.UserID
                });
        }
        /// <summary>
        /// 修改试题的答案和分数。
        /// </summary>
        /// <param name="UserExamID"></param>
        /// <param name="QuestionID"></param>
        /// <param name="sUserAnswer"></param>
        /// <param name="ExamScore"></param>
        public void UpdateUserAnswer(Guid UserExamID, Guid QuestionID, string sUserAnswer, decimal ExamScore)
        {
            int nRowCnt = 0;

            nRowCnt = DataMapperClient_Write.Update("Test.UserExamResult.UpdateUserAnswerScore",
                new
                {
                    UserExamID = UserExamID,
                    QuestionID = QuestionID,
                    UserAnswer = sUserAnswer,
                    ExamScore = ExamScore,
                    UserID = AppContext.UserContext.Current.UserID
                });
        }

        public void UpdateUserQuestionScore(Guid UserExamID, Guid QuestionID, decimal ExamScore)
        {
            int nRowCnt = 0;

            nRowCnt = DataMapperClient_Write.Update("Test.UserExamResult.UpdateUserQuestionScore",
                new
                {
                    UserExamID = UserExamID,
                    QuestionID = QuestionID,
                    ExamScore = ExamScore,
                    UserID = AppContext.UserContext.Current.UserID
                });
        }

        public decimal GetUserScore(Guid UserExamID, out decimal ExamScore)
        {
            decimal UserScore = 0;
            ExamScore = 0;

            DataTable oDt = DataMapperClient_Read.QueryForDataTable(
                "Test.UserExamResult.GetUserScore", UserExamID);
            if (oDt != null && oDt.Rows != null && oDt.Rows.Count > 0)
            {
                //得到内容
                UserScore = (decimal)oDt.Rows[0]["UserScore"];
                ExamScore = (decimal)oDt.Rows[0]["PaperScore"];
            }
            return UserScore;
        }
        #endregion

        #region --对某一试卷的操作--
        public IList<UserExamResult> FindAllInUserExam(Guid UserExamID)
        {
            IList<UserExamResult> LstUserExamResults = DataMapperClient_Read.QueryForList<UserExamResult>(
                "Test.UserExamResult.FindAllInUserExam", UserExamID);

            return LstUserExamResults;
        }

        public bool DeleteAllInUserExam(Guid UserExamID)
        {
            int nRowCnt = 0;
            nRowCnt = DataMapperClient_Write.Delete("Test.UserExamResult.DeleteAllInUserExam",
                new
                {
                    UserExamID = UserExamID,
                    UserID = AppContext.UserContext.Current.UserID
                });

            return nRowCnt > 0;
        }

        #endregion

        #region IUserExamResultDao 成员


        public UserExamState GetUserExamState(Guid UserExamID)
        {
            UserExamState LstUserExamResults = DataMapperClient_Read.QueryForObject<UserExamState>(
                "Test.UserExamResult.GetUserExamState", UserExamID);

            return LstUserExamResults;
        }
        /// <summary>
        /// 为某一指定的试卷生成原始的结果信息
        /// </summary>
        /// <param name="UserExamID"></param>
        public void CreateExamResultForUserExam(Guid UserExamID)
        {
            int nRowCnt = 0;

            nRowCnt = DataMapperClient_Write.Update("Test.UserExamResult.CreateExamResultForUserExam",
                new
                {
                    UserExamID = UserExamID,
                    CreateUserID = AppContext.UserContext.Current.UserID
                });
        }
        /// <summary>
        /// 得到试卷中每一题型的试题总数与考生答对试题数
        /// </summary>
        /// <param name="UserExamID"></param>
        /// <returns></returns>
        public IList<QuestionTypeUserResult> GetQuestionTypeUserResult(Guid UserExamID)
        {
            return DataMapperClient_Read.QueryForList<QuestionTypeUserResult>(
                "Test.UserExamResult.GetQuestionTypeUserResult", UserExamID);
        }
        #endregion
    }
}
