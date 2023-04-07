using System;
using ETMS.Components.Exam.API.Entity.ItemBank;

namespace ETMS.Components.Exam.Implement.DAL.ItemBank
{
    /// <summary>
    /// 解题思路数据访问接口
    /// </summary>
    public interface IQuestionExtendDao
    {
        /// <summary>
        /// 添加解题思路
        /// </summary>
        /// <param name="extend">试题扩充对象</param>
        /// <returns>true为成功</returns>
        void Add(QuestionExtend extend);

        /// <summary>
        /// 修改解题思路
        /// </summary>
        /// <param name="extend">试题扩充对象</param>
        void Update(QuestionExtend extend);

        /// <summary>
        /// 删除解题思路
        /// </summary>
        /// <param name="questionID">试题ID</param>
        void Delete(Guid questionID);

        /// <summary>
        /// 得到指定问题的解题思路
        /// </summary>
        /// <param name="questionID">试题ID</param>
        /// <returns>试题扩充对象</returns>
        QuestionExtend GetQuestionExtend(Guid questionID);

        /// <summary>
        /// 是否存在解题思路(存在删除标志不为false的记录)
        /// </summary>
        /// <param name="questionID">试题ID</param>
        /// <returns>true为存在</returns>
        bool IsExist(Guid questionID);
    }
}
