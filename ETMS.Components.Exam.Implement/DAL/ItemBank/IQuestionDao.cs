using System;
using System.Collections.Generic;
using ETMS.Components.Exam.API.Entity.ItemBank;

namespace ETMS.Components.Exam.Implement.DAL.ItemBank
{
    public interface IQuestionDao
    {
        ///<summary>
        /// 添加一个试题
        ///</summary>
        /// <param name="question">要添加的试题的基类。必须是系统支持的几种试题的具体类实例。</param>
        void AddQuestion(Question question);

        ///<summary>
        /// 添加一个试题
        ///</summary>
        /// <param name="newQuestion">更新的试题实体（必须为试题类型的实体）</param>
        void Update(Question newQuestion);

        ///<summary>
        /// 删除指定的试题
        ///</summary>
        /// <param name="questionID">要删除的试题的ID</param>
        void Delete(Guid questionID, int updatedUserID);
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="questionIDs">试题的IDs</param>
        void DeleteBatch(IList<Guid> questionIDs, int updatedUserID);
        ///<summary>
        /// 得到某一试题。会根据试题的类型得到具体试题类型的实例。
        ///</summary>
        /// <param name="questionID">要获取的试题的ID</param>
        Question GetByID(Guid questionID);
        /// <summary>
        /// 修改答案字符串
        /// </summary>
        /// <param name="questionID">试题ID</param>
        /// <param name="answer">答案字符串</param>
        /// <param name="updatedUserID">更新用户ID</param>
        void UpdateAnswers(Guid questionID, string answer, int updatedUserID);
        /// <summary>
        /// 获取答案字符串
        /// </summary>
        /// <param name="questionID">试题ID</param>
        /// <returns>答案字符串</returns>
        string GetAnswersByID(Guid questionID);

        /// <summary>
        /// 删除指定分类下的所有试题
        /// </summary>
        /// <param name="classID">分类ID</param>
        void DeleteClassID(Guid classID);

        /// <summary>
        /// 修改指定分类的试题到新分类
        /// </summary>
        /// <param name="oldClassID">老分类</param>
        /// <param name="newClassID">新分类</param>
        void UpdateClassID(Guid oldClassID, Guid newClassID);

        /// <summary>
        /// 设置共享状态
        /// </summary>
        /// <param name="questionID"></param>
        /// <param name="state"></param>
        void SetShareState(Guid questionID, int state);

        /// <summary>
        /// 设置审批状态
        /// </summary>
        /// <param name="questionID"></param>
        /// <param name="state"></param>
        void SetAuditState(Guid questionID, int state);
    }
}
