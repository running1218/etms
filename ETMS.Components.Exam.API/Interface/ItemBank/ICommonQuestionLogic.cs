using System;
using ETMS.Components.Exam.API.Entity.ItemBank;
using System.ServiceModel;
namespace ETMS.Components.Exam.API.Interface.ItemBank
{
    /// <summary>
    /// 
    /// </summary>
    [ServiceContract(Namespace = "http://Autumn.Business.LMS.Interface.ItemBank/ICommonQuestionLogic")]
    public interface ICommonQuestionLogic
    {
        ///<summary>
        /// 添加一个试题
        ///</summary>
        /// <param name="question">要添加的试题的基类。必须是系统支持的几种试题的具体类实例。</param>
        /// <returns>包含有ID信息的试题类</returns>
        [OperationContract]
        CommonQuestion AddQuestion(CommonQuestion questionItem);
        ///<summary>
        /// 删除指定的试题
        ///</summary>
        /// <param name="questionID">要删除的试题的ID</param>
        [OperationContract]
        void Delete(Guid questionID);
        ///<summary>
        /// 得到某一试题。会根据试题的类型得到具体试题类型的实例。
        ///</summary>
        /// <param name="questionID">要获取的试题的ID</param>
        [OperationContract]
        CommonQuestion GetByID(Guid questionID);
        //<summary>
        /// 更新一个试题基本信息
        ///</summary>
        ///<remarks>
        ///1,更新的试题必须已存在，如果不存在，无法进行更新；<br></br>
        ///2,更新时，可以为试题增加答案反馈，选项反馈和解题思路；<br></br>
        ///3,如果解题思路，答案反馈，选项反馈已存在的部分将更新；<br></br>
        ///</remarks>
        /// <param name="questionID">要更新的试题ID</param>
        /// <param name="newQuestion">更新的试题实体（必须为试题类型的实体）</param>
        [OperationContract]
        void Update(Guid questionID, CommonQuestion newQuestion);
        /// <summary>
        /// 修改答案字符串
        /// </summary>
        /// <param name="questionID">试题ID</param>
        /// <param name="answer">答案字符串</param>
        [OperationContract]
        void UpdateAnswers(Guid questionID, string answer);
    }
}
