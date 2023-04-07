using System;

using Autumn.Data.ORM.IBatis;
using ETMS.Components.Exam.API.Entity.ItemBank;

namespace ETMS.Components.Exam.Implement.DAL.ItemBank
{
    /// <summary>
    /// 解题思路数据访问的实现
    /// </summary>
    public class QuestionExtendIBatisDao : ReadWriteDataMapperDaoSupport, IQuestionExtendDao
    {
        /// <summary>
        /// 添加解题思路
        /// </summary>
        /// <param name="extend">试题扩充对象</param>
        /// <returns>true为成功</returns>
        public void Add(QuestionExtend extend)
        {
            DataMapperClient_Write.Insert("QuestionExtend.Insert", extend);
        }

        /// <summary>
        /// 修改解题思路
        /// </summary>
        /// <param name="extend">试题扩充对象</param>
        public void Update(QuestionExtend extend)
        {
            DataMapperClient_Write.Update("QuestionExtend.Update", extend);
        }

        /// <summary>
        /// 删除解题思路
        /// </summary>
        /// <param name="questionID">试题ID</param>
        public void Delete(Guid questionID)
        {
            DataMapperClient_Write.Delete("QuestionExtend.Delete", questionID);
        }

        /// <summary>
        /// 得到指定问题的解题思路
        /// </summary>
        /// <param name="questionID">试题ID</param>
        /// <returns>试题扩充对象</returns>
        public QuestionExtend GetQuestionExtend(Guid questionID)
        {
            return (QuestionExtend)DataMapperClient_Read.QueryForObject("QuestionExtend.GetByID", questionID);
        }

        /// <summary>
        /// 是否存在解题思路(存在删除标志不为false的记录)
        /// </summary>
        /// <param name="questionID">试题ID</param>
        /// <returns>true为存在</returns>
        public bool IsExist(Guid questionID)
        {
            int result=(int)DataMapperClient_Read.QueryForObject("QuestionExtend.IsExist", questionID);
            if (result > 0)
                return true;
            else
                return false;
        }

    }
}
