using System;
using System.Collections.Generic;
using Autumn.Data.ORM.IBatis;
using ETMS.Components.Exam.API.Entity.Test;

namespace ETMS.Components.Exam.Implement.DAL.Test
{
    public class TestFeedbackIBatisDao : ReadWriteDataMapperDaoSupport,ITestFeedbackDao
    {
        #region --试卷反馈信息项单项操作--

        public void AddTestFeedback(TestFeedback feedbackItem)
        {
            var oValue = new { TestFeedback = feedbackItem, UserID = AppContext.UserContext.Current.UserID };

            DataMapperClient_Write.Insert("Test.TestFeedback.Add", oValue);
        }

        public void Update(TestFeedback newFeedbackItem)
        {
            int nRowCnt = 0;

            nRowCnt = DataMapperClient_Write.Update("Test.TestFeedback.Update",
                new
                {
                    TestFeedback = newFeedbackItem,
                    UserID = AppContext.UserContext.Current.UserID
                });
        }

        public void Delete(Guid testFeedbackID)
        {
            int nRowCnt = 0;
            nRowCnt = DataMapperClient_Write.Delete("Test.TestFeedback.Delete",
                new
                {
                    TestFeedbackID = testFeedbackID,
                    UserID = AppContext.UserContext.Current.UserID
                });
        }
        #endregion

        #region --一试卷中答题反馈项操作--
        public IList<TestFeedback> FindTestFeedbacksInPaper(Guid testPaperID)
        {
            IList<TestFeedback> LstTestFeedbacks = DataMapperClient_Read.QueryForList<TestFeedback>(
                "Test.TestFeedback.FindTestFeedbacksInPaper", testPaperID);

            return LstTestFeedbacks;
        }

        public bool DeleteAllInPaper(Guid testPaperID)
        {
            int nRowCnt = 0;
            nRowCnt = DataMapperClient_Write.Delete("Test.TestFeedback.DeleteAllInPaper",
                new
                {
                    TestPaperID = testPaperID,
                    UserID = AppContext.UserContext.Current.UserID
                });

            return nRowCnt > 0;
        }

        #endregion
    }
}
