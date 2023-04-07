using System;
using ETMS.Components.Exam.API.Entity.ItemBank;
using System.ServiceModel;
namespace ETMS.Components.Exam.API.Interface.ItemBank
{
    /// <summary>
    /// 试题解题思路逻辑接口
    /// </summary>
    [ServiceContract(Namespace = "http://Autumn.Business.LMS.Interface.ItemBank/IQuestionExtendLogic")]
    public interface IQuestionExtendLogic
    {
        /// <summary>
        /// 添加解题思路
        /// </summary>
        /// <param name="extend">试题扩充对象</param>
        /// <returns>返回true为添加成功</returns>
        [OperationContract]
        bool Add(QuestionExtend extend);
        /// <summary>
        /// 删除解题思路
        /// </summary>
        /// <param name="questionID">试题ID</param>
        [OperationContract]
        void Delete(Guid questionID);
        /// <summary>
        /// 得到指定问题的解题思路
        /// </summary>
        /// <param name="questionID">试题ID</param>
        /// <returns>试题扩充对象</returns>
        [OperationContract]
        QuestionExtend GetQuestionExtend(Guid questionID);
        /// <summary>
        /// 是否存在解题思路
        /// </summary>
        /// <param name="questionID">试题ID</param>
        /// <returns>true为存在</returns>
        [OperationContract]
        bool IsExist(Guid questionID);
        /// <summary>
        /// 修改解题思路
        /// </summary>
        /// <param name="extend">试题扩充对象</param>
        [OperationContract]
        void Update(QuestionExtend extend);
    }
}
