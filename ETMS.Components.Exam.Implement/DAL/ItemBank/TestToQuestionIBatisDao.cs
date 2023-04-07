using System;
using Autumn.Data.ORM.IBatis;
using ETMS.Components.Exam.API.Entity.ItemBank;
namespace ETMS.Components.Exam.Implement.DAL.ItemBank
{
    /// <summary>
    /// (试卷-试题定义)数据访问的实现
    /// </summary>
    public class TestToQuestionIBatisDao : ReadWriteDataMapperDaoSupport, ITestToQuestionDao
    {
        /// <summary>
        /// 添加试题到课程试卷
        /// </summary>
        /// <param name="tp">试卷-试题定义实体</param>
        /// <returns>true为成功</returns>
        public void Add(TestToQuestion tp)
        {
            DataMapperClient_Write.Insert("TestToQuestion.Insert", tp);
        }

        /// <summary>
        /// 修改课程试卷里的试题
        /// </summary>
        /// <param name="tp">试卷-试题定义实体</param>
        public void Update(TestToQuestion tp)
        {
            DataMapperClient_Write.Update("TestToQuestion.Update", tp);
        }

        /// <summary>
        /// 删除课程试卷里的试题
        /// </summary>
        /// <param name="testPaperID">试卷ID</param>
        /// <param name="questionID">试题ID</param>
        public void Delete(Guid testPaperID, Guid questionID)
        {
            DataMapperClient_Write.Delete("TestToQuestion.Delete", new
            {
                TestPaperID = testPaperID,
                QuestionID = questionID
            });
        }

        /// <summary>
        /// 删除课程试卷里的全部试题
        /// </summary>
        /// <param name="testPaperID"></param>
        public void Deletes(Guid testPaperID)
        {
            DataMapperClient_Write.Delete("TestToQuestion.Deletes", testPaperID);
        }

        /// <summary>
        /// 得到试卷-试题定义实体
        /// </summary>
        /// <param name="testPaperID">试卷ID</param>
        /// <param name="questionID">试题ID</param>
        /// <returns></returns>
        public TestToQuestion GetTPQuestion(Guid testPaperID, Guid questionID)
        {
            return (TestToQuestion)DataMapperClient_Read.QueryForObject("TestToQuestion.GetByID", new
            {
                TestPaperID = testPaperID,
                QuestionID = questionID
            });
        }

        /// <summary>
        /// 指定的试题时否存在
        /// </summary>
        /// <param name="testPaperID">试卷ID</param>
        /// <param name="questionID">试题ID</param>
        /// <returns></returns>
        public bool IsExist(Guid testPaperID, Guid questionID)
        {
            int result = (int)DataMapperClient_Read.QueryForObject("TestToQuestion.IsExist", new
            {
                TestPaperID = testPaperID,
                QuestionID = questionID
            });
            if (result > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 课程试卷里的试题的累加总分
        /// </summary>
        /// <param name="testPaperID">试卷ID</param>
        /// <param name="questionID">试题ID</param>
        /// <returns></returns>
        public int TotalScore(Guid testPaperID, Guid questionID)
        {
            int result = (int)DataMapperClient_Read.QueryForObject("TestToQuestion.TotalScore", new
            {
                TestPaperID = testPaperID,
                QuestionID = questionID
            });
            return result;
        }

        /// <summary>
        /// 得到指定试卷里最小的可用序号
        /// </summary>
        /// <param name="testPaperID"></param>
        /// <returns></returns>
        public int GetValidSequence(Guid testPaperID)
        {
            int result = (int)DataMapperClient_Read.QueryForObject("TestToQuestion.GetValidSequence", testPaperID);
            return result;
        }
    }
}
