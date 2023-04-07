using System;

using ETMS.Components.Exam.API.Entity.ItemBank;
using System.ServiceModel;
namespace ETMS.Components.Exam.API.Interface.ItemBank
{
    /// <summary>
    /// 课程与试题引用关系
    /// </summary>
    [ServiceContract(Namespace = "http://Autumn.Business.LMS.Interface.ItemBank/ITestToQuestionLogic")]
    public interface ITestToQuestionLogic
    {
        /// <summary>
        /// 添加试题到课程试卷
        /// </summary>
        /// <param name="tp">试卷-试题定义实体</param>
        /// <returns>true为成功</returns>
        [OperationContract]
        void Add(TestToQuestion tp);

        /// <summary>
        /// 修改课程试卷里的试题
        /// </summary>
        /// <param name="tp">试卷-试题定义实体</param>
        [OperationContract]
        void Update(TestToQuestion tp);

        /// <summary>
        /// 删除课程试卷里的试题
        /// </summary>
        /// <param name="testPaperID">试卷ID</param>
        /// <param name="questionID">试题ID</param>
        [OperationContract]
        void Delete(Guid testPaperID, Guid questionID);

        /// <summary>
        /// 删除课程试卷里的全部试题
        /// </summary>
        /// <param name="testPaperID"></param>
        [OperationContract]
        void Deletes(Guid testPaperID);

        /// <summary>
        /// 得到试卷-试题定义实体
        /// </summary>
        /// <param name="testPaperID">试卷ID</param>
        /// <param name="questionID">试题ID</param>
        /// <returns></returns>
        [OperationContract]
        TestToQuestion GetTPQuestion(Guid testPaperID, Guid questionID);

        /// <summary>
        /// 指定的试题时否存在
        /// </summary>
        /// <param name="testPaperID">试卷ID</param>
        /// <param name="questionID">试题ID</param>
        /// <returns></returns>
        [OperationContract]
        bool IsExist(Guid testPaperID, Guid questionID);

        /// <summary>
        /// 课程试卷里的试题的累加总分
        /// </summary>
        /// <param name="testPaperID">试卷ID</param>
        /// <param name="questionID">试题ID</param>
        /// <returns></returns>
        [OperationContract]
        int TotalScore(Guid testPaperID, Guid questionID);

        /// <summary>
        /// 得到指定试卷里最小的可用序号
        /// </summary>
        /// <param name="testPaperID"></param>
        /// <returns></returns>
        [OperationContract]
        int GetValidSequence(Guid testPaperID);
    }
}
