using System;
using ETMS.Components.Exam.API.Entity.NewTestPaper;
using ETMS.Components.Exam.Implement.DAL.NewTestPaper;
//using System.Web.Script.Serialization;
using ETMS.Utility;

/* add 2013-9-26 hujy*/
namespace ETMS.Components.Exam.Implement.BLL.NewTestPaper
{
    public class ExamLogic : AExamHomework
    {
        public ExamLogic() 
        {
            aExamHomeWorkData = new ExamDataAccess();
        }

        /// <summary>
        /// 查询试卷列表
        /// </summary>
        /// <param name="testPaperID"></param>
        /// <param name="onlineTestID"></param>
        /// <returns></returns>
        public override Paper GetExamTestPaper(Guid testPaperID, Guid onlineTestID)
        {
            string cacheKey = "T" + testPaperID;
            Paper pager = new Paper();
            object o = CacheHelper.Get(cacheKey);
            if (o == null)
            {
                pager = aExamHomeWorkData.GetExamTestPaper(testPaperID, onlineTestID);
                CacheHelper.Add(cacheKey, pager, TimeSpan.FromDays(1));
            }
            else
            {
                pager = (Paper)o;
            }
            return pager;
        }

        /// <summary>
        /// 开始考试
        /// </summary>
        /// <param name="studentID"></param>
        /// <param name="testPaperID"></param>
        /// <param name="trainingItemCourseID"></param>
        /// <param name="onlineTestID"></param>
        /// <param name="studentCourseID"></param>
        /// <returns></returns>
        public override Guid StartNewTest(int studentID, Guid testPaperID, Guid trainingItemCourseID, Guid onlineTestID, Guid studentCourseID)
        {
            return aExamHomeWorkData.StartNewTest(studentID, testPaperID, trainingItemCourseID, onlineTestID, studentCourseID);
        }


        /// <summary>
        /// 提交试卷
        /// </summary>
        /// <param name="userExamID"></param>
        /// <param name="status"></param>
        /// <param name="userID"></param>
        /// <param name="testPaperID"></param>
        public override void SubmitTestPaper(Guid userExamID, int status, int userID, Guid testPaperID)
        {
            aExamHomeWorkData.SubmitTestPaper(userExamID, status, userID, testPaperID);
        }

        public override UserTestPaper GetUserTestPaper(Guid userExamID, Guid onLineTestID)
        {
            return aExamHomeWorkData.GetUserTestPaper(userExamID, onLineTestID);
        }

    }
}
