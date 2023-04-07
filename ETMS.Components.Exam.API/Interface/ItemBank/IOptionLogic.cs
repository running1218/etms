using System;
using System.Collections.Generic;
using ETMS.Components.Exam.API.Entity.ItemBank;
using System.ServiceModel;

namespace ETMS.Components.Exam.API.Interface.ItemBank
{
    /// <summary>
    /// 试题选项相关的服务接口
    /// </summary>
    [ServiceContract(Namespace = "http://Autumn.Business.LMS.Interface.ItemBank/IOptionServiceLogic")]
    public interface IOptionServiceLogic
    {
        /// <summary>
        /// 添加一个试题选项
        /// </summary>
        /// <param name="option">新添加的试题选项内容</param>
        /// <returns>新添加选项ID</returns>
        [OperationContract]
        System.Guid AddOption(QuestionOption option);
        /// <summary>
        /// 删除指定的试题选项
        /// </summary>
        /// <param name="questionOptionID">要删除的试题选项</param>
        /// <returns></returns>
        [OperationContract]
        bool Delete(System.Guid questionOptionID);
        /// <summary>
        /// 删除试题中某一选项组中所有选项
        /// </summary>
        /// <param name="questionID">试题ID</param>
        /// <param name="optionGroupID">选项组ID</param>
        /// <returns></returns>
        [OperationContract]
        bool DeleteByGroupID(Guid questionID, Guid optionGroupID);
        /// <summary>
        /// 删除指定试题中所有选项
        /// </summary>
        /// <param name="questionID">试题ID</param>
        /// <returns></returns>
        [OperationContract]
        bool DeleteByQuestionID(Guid questionID);
        /// <summary>
        /// 得到指定选项
        /// </summary>
        /// <param name="questionOptionID"></param>
        /// <returns></returns>
        [OperationContract]
        QuestionOption Load(Guid questionOptionID);
        /// <summary>
        /// 得到试题中指定选项组中所有选项
        /// </summary>
        /// <param name="questionID">试题ID</param>
        /// <param name="optionGroupTitleID">选项组ID</param>
        /// <returns></returns>
        [OperationContract]
        IList<QuestionOption> LoadAllInGroup(Guid questionID, Guid optionGroupTitleID);
        /// <summary>
        /// 得到指定试题中所有选项
        /// </summary>
        /// <param name="questionID">试题ID</param>
        /// <returns></returns>
        [OperationContract]
        IList<QuestionOption> LoadAllInQuestion(Guid questionID);
        /// <summary>
        /// 更新已存在的试题选项
        /// </summary>
        /// <remarks>
        /// 要更新的试题选项必须是已存在的试题选项
        /// </remarks>
        /// <param name="option">要更新的试题选项</param>
        /// <returns></returns>
        [OperationContract]
        bool Update(QuestionOption option);
    }
}
