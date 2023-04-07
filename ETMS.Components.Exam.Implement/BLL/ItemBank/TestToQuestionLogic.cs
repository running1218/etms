using System;

using Autumn.Context;
using Autumn.Objects.Factory;
using ETMS.Components.Exam.API.Entity.ItemBank;
using ETMS.Components.Exam.Implement.DAL.ItemBank;
using ETMS.Components.Exam.API.Interface.ItemBank;
namespace ETMS.Components.Exam.Implement.BLL.ItemBank
{
    public class TestToQuestionLogic : IMessageSourceAware, IInitializingObject, ITestToQuestionLogic
    {
        public ITestToQuestionDao TestToQuestionDao { get; set; }

        #region IMessageSourceAware 成员
        public IMessageSource MessageSource
        {
            get;
            set;
        }
        #endregion
        #region IInitializingObject 成员
        public void AfterPropertiesSet()
        {
            if (TestToQuestionDao == null)
            {
                throw new NotImplementedException("please set TestToQuestionDao Property First!");
            }
        }
        #endregion

        /// <summary>
        /// 添加试题到课程试卷
        /// </summary>
        /// <param name="tp">试卷-试题定义实体</param>
        /// <returns>true为成功</returns>
        public void Add(TestToQuestion tp)
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
        public void Deletes(Guid testPaperID)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 得到试卷-试题定义实体
        /// </summary>
        /// <param name="testPaperID">试卷ID</param>
        /// <param name="questionID">试题ID</param>
        /// <returns></returns>
        public TestToQuestion GetTPQuestion(Guid testPaperID, Guid questionID)
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

        /// <summary>
        /// 得到指定试卷里最小的可用序号
        /// </summary>
        /// <param name="testPaperID"></param>
        /// <returns></returns>
        public int GetValidSequence(Guid testPaperID)
        {
            throw new NotImplementedException();
        }

    }
}
