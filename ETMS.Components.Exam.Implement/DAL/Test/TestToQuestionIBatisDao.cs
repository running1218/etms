using System;

using Autumn.Data.ORM.IBatis;
using ETMS.Components.Exam.API.Entity.ItemBank;

namespace ETMS.Components.Exam.Implement.DAL.Test
{
    /// <summary>
    /// (试卷-试题定义)数据访问的实现
    /// </summary>
    public class TP_QuestionIBatisDao : ReadWriteDataMapperDaoSupport, ITestToQuestionDao
    {
        /// <summary>
        /// 添加试题到课程试卷
        /// </summary>
        /// <param name="tp">试卷-试题定义实体</param>
        /// <returns>true为成功</returns>
        public bool Add(TestToQuestion tp)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 修改课程试卷里的试题
        /// </summary>
        /// <param name="tp">试卷-试题定义实体</param>
        public void Update(TestToQuestion tp)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 删除课程试卷里的试题
        /// </summary>
        /// <param name="testPaperID">试卷ID</param>
        /// <param name="questionID">试题ID</param>
        public void Delete(Guid testPaperID, Guid questionID)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 删除课程试卷里的全部试题
        /// </summary>
        /// <param name="testPaperID"></param>
        public void Delete(Guid testPaperID)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 得到试卷-试题定义实体
        /// </summary>
        /// <param name="testPaperID">试卷ID</param>
        /// <param name="questionID">试题ID</param>
        /// <returns></returns>
        public TestToQuestion GetTestToQuestion(Guid testPaperID, Guid questionID)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 指定的试题时否存在
        /// </summary>
        /// <param name="testPaperID">试卷ID</param>
        /// <param name="questionID">试题ID</param>
        /// <returns></returns>
        public bool IsExist(Guid testPaperID, Guid questionID)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 课程试卷里的试题的累加总分
        /// </summary>
        /// <param name="testPaperID">试卷ID</param>
        /// <param name="questionID">试题ID</param>
        /// <returns></returns>
        public int TotalScore(Guid testPaperID, Guid questionID)
        {
            throw new NotImplementedException();
        }
    }
}
