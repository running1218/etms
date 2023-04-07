using System;

using ETMS.Components.Exam.API.Entity.ItemBank;
namespace ETMS.Components.Exam.Implement.DAL.ItemBank
{
    /// <summary>
    /// (试卷-试题定义)数据访问接口
    /// </summary>
    public interface ITestToQuestionDao
    {
        /// <summary>
        /// 添加试题到课程试卷
        /// </summary>
        /// <param name="tp">试卷-试题定义实体</param>
        /// <returns>true为成功</returns>
        void Add(TestToQuestion tp);

        /// <summary>
        /// 修改课程试卷里的试题
        /// </summary>
        /// <param name="tp">试卷-试题定义实体</param>
        void Update(TestToQuestion tp);

        /// <summary>
        /// 删除课程试卷里的试题
        /// </summary>
        /// <param name="testPaperID">试卷ID</param>
        /// <param name="questionID">试题ID</param>
        void Delete(Guid testPaperID, Guid questionID);

        /// <summary>
        /// 删除课程试卷里的全部试题
        /// </summary>
        /// <param name="testPaperID"></param>
        void Deletes(Guid testPaperID);

        /// <summary>
        /// 得到试卷-试题定义实体
        /// </summary>
        /// <param name="testPaperID">试卷ID</param>
        /// <param name="questionID">试题ID</param>
        /// <returns></returns>
        TestToQuestion GetTPQuestion(Guid testPaperID, Guid questionID);

        /// <summary>
        /// 指定的试题时否存在
        /// </summary>
        /// <param name="testPaperID">试卷ID</param>
        /// <param name="questionID">试题ID</param>
        /// <returns></returns>
        bool IsExist(Guid testPaperID, Guid questionID);

        /// <summary>
        /// 课程试卷里的试题的累加总分
        /// </summary>
        /// <param name="testPaperID">试卷ID</param>
        /// <param name="questionID">试题ID</param>
        /// <returns></returns>
        int TotalScore(Guid testPaperID, Guid questionID);

        /// <summary>
        /// 得到指定试卷里最小的可用序号
        /// </summary>
        /// <param name="testPaperID"></param>
        /// <returns></returns>
        int GetValidSequence(Guid testPaperID);
    }
}
