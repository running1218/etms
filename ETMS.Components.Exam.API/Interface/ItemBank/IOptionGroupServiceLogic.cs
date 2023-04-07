using System;
using System.Collections.Generic;
using ETMS.Components.Exam.API.Entity.ItemBank;
using System.ServiceModel;

namespace ETMS.Components.Exam.API.Interface.ItemBank
{
    /// <summary>
    /// 试题选项组服务接口
    /// </summary>
    [ServiceContract(Namespace = "http://Autumn.Business.LMS.Interface.ItemBank/IOptionGroupServiceLogic")]
    public interface IOptionGroupServiceLogic
    {
        /// <summary>
        /// 添加一个新的试题选项组，可以同时指定选项组包含的选项
        /// </summary>
        /// <param name="optionGroup">新增的选项组信息</param>
        /// <param name="options">选项组中包含的选项列表</param>
        /// <returns></returns>
        [OperationContract]
        OptionGroupItem AddOptionGroup(OptionGroup optionGroup, IList<QuestionOption> options);
        /// <summary>
        /// 删除试题中指定的选项组，如果选项组包含选项也将删除。
        /// </summary>
        /// <param name="questionID">试题ID</param>
        /// <param name="optionGroupID">选项组ID</param>
        /// <returns></returns>
        [OperationContract]
        bool Delete(Guid questionID, Guid optionGroupID);
        /// <summary>
        /// 得到试题中指定的选项组
        /// </summary>
        /// <param name="questionID">试题ID</param>
        /// <param name="optionGroupID">选项组ID</param>
        /// <returns></returns>
        [OperationContract]
        OptionGroupItem Load(Guid questionID, Guid optionGroupID);
        /// <summary>
        /// 添加指定试题中所有的选项组信息
        /// </summary>
        /// <param name="questionID">试题ID</param>
        /// <returns></returns>
        [OperationContract]
        IList<OptionGroupItem> LoadAllInQuestion(Guid questionID);
        /// <summary>
        /// 更新指定的选项组
        /// </summary>
        /// <param name="NewOptionGroupItem"></param>
        /// <returns></returns>
        [OperationContract]
        bool Update(OptionGroupItem NewOptionGroupItem);
    }
}
