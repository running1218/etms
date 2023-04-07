using System;
using System.Collections.Generic;
using System.ServiceModel;
using ETMS.Components.Exam.API.Entity;
using ETMS.Components.Exam.API.Entity.ItemBank;
namespace ETMS.Components.Exam.API.Interface.ItemBank
{
    /// <summary>
    /// 试题接口类
    /// </summary>
    [ServiceContract(Namespace = "http://Autumn.Business.LMS.Interface.ItemBank/IQuestionLogic")]
    public interface IQuestionLogic
    {
        #region Base 接口
        ///<summary>
        /// 添加一个试题
        ///</summary>
        /// <param name="question">要添加的试题的基类。必须是系统支持的几种试题的具体类实例。</param>
        [OperationContract]
        Guid AddQuestion(Question question);

        ///<summary>
        /// 添加一个试题
        ///</summary>
        /// <param name="questionID">要更新的试题ID</param>
        /// <param name="newQuestion">更新的试题实体（必须为试题类型的实体）</param>
        [OperationContract]
        void Update(Guid questionID, Question newQuestion);

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
        Question GetByID(Guid questionID);
        /// <summary>
        /// 修改答案字符串
        /// </summary>
        /// <param name="questionID">试题ID</param>
        /// <param name="answer">答案字符串</param>
        [OperationContract]
        void UpdateAnswers(Guid questionID, string answer);
        /// <summary>
        /// 获取答案字符串
        /// </summary>
        /// <param name="questionID">试题ID</param>
        /// <returns>答案字符串</returns>
        [OperationContract]
        string GetAnswersByID(Guid questionID);

        /// <summary>
        /// 删除指定分类下的所有试题
        /// </summary>
        /// <param name="classID">分类ID</param>
        [OperationContract]
        void DeleteClassID(Guid classID);

        /// <summary>
        /// 修改指定分类的试题到新分类
        /// </summary>
        /// <param name="oldClassID">老分类</param>
        /// <param name="newClassID">新分类</param>
        [OperationContract]
        void UpdateClassID(Guid oldClassID, Guid newClassID);

        /// <summary>
        /// 设置共享状态
        /// </summary>
        /// <param name="questionID">试题ID</param>
        /// <param name="state">共享状态</param>
        [OperationContract]
        void SetShareState(Guid questionID, ShareType state);

        /// <summary>
        /// 设置审批状态
        /// </summary>
        /// <param name="questionID">试题ID</param>
        /// <param name="state">审批状态</param>
        [OperationContract]
        void SetAuditState(Guid questionID, AuditType state);
        #endregion
    }
}
