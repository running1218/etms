using System;
using System.Collections.Generic;
using System.ServiceModel;
using ETMS.Components.Exam.API.Entity.ItemBank;

namespace ETMS.Components.Exam.API.Interface.ItemBank
{
    /// <summary>
    /// 各种试题类型接口类
    /// </summary>
    [ServiceContract(Namespace = "http://Autumn.Business.LMS.Interface.ItemBank/IQuestionServiceLogic")]
    public interface IQuestionServiceLogic
    {
        #region Base 接口
        ///<summary>
        /// 添加一个试题
        ///</summary>
        /// <param name="question">要添加的试题的基类。必须是系统支持的几种试题的具体类实例。</param>
        [OperationContract]
        System.Guid AddQuestion(QuestionBase question);

        ///<summary>
        /// 添加一个试题
        ///</summary>
        /// <param name="questionID">要更新的试题ID</param>
        /// <param name="newQuestion">更新的试题实体（必须为试题类型的实体）</param>
        [OperationContract]
        void Update(Guid questionID, QuestionBase newQuestion);

        ///<summary>
        /// 删除指定的试题
        ///</summary>
        /// <param name="questionID">要删除的试题的ID</param>
        [OperationContract]
        void Delete(Guid questionID);

        ///<summary>
        /// 批量删除指定的试题
        ///</summary>
        /// <param name="questionIDs">要删除的试题的IDs</param>
        [OperationContract]
        void DeleteBatch(IList<Guid> questionIDs);

        ///<summary>
        /// 得到某一试题。会根据试题的类型得到具体试题类型的实例。
        ///</summary>
        /// <param name="questionID">要获取的试题的ID</param>
        [OperationContract]
        QuestionBase GetByID(Guid questionID);
        #endregion
    }
}
