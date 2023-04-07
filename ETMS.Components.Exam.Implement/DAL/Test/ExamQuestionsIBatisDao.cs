using System;
using System.Collections.Generic;
using Autumn.Data.ORM.IBatis;
using ETMS.Components.Exam.API.Entity.Test;
using System.Data;

namespace ETMS.Components.Exam.Implement.DAL.Test
{
    public class ExamQuestionsIBatisDao : ReadWriteDataMapperDaoSupport,IExamQuestionsDao
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
                    QuestionID=QuestionID,
                    UserAnswer=sUserAnswer,
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

            DataTable oDt= DataMapperClient_Read.QueryForDataTable(
                "Test.UserExamResult.GetUserScore", UserExamID);
            if (oDt != null && oDt.Rows != null && oDt.Rows.Count > 0)
            { 
                //得到内容
                UserScore=(decimal)oDt.Rows[0]["UserScore"];
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
    }
}
