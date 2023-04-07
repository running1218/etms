using System;

using Autumn.Context;
using Autumn.Objects.Factory;
using ETMS.Components.Exam.API.Entity.ItemBank;
using ETMS.Components.Exam.Implement.DAL.ItemBank;
using ETMS.Components.Exam.API.Interface.ItemBank;
namespace ETMS.Components.Exam.Implement.BLL.ItemBank
{
    public class QuestionExtendLogic : IMessageSourceAware, IInitializingObject, IQuestionExtendLogic
    {
        public IQuestionExtendDao QuestionExtendDao { get; set; }

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
            if (QuestionExtendDao == null)
            {
                throw new NotImplementedException("please set QuestionExtendDao Property First!");
            }
        }
        #endregion

        /// <summary>
        /// 添加解题思路
        /// </summary>
        /// <param name="extend">试题扩充对象</param>
        /// <returns>返回true为添加成功</returns>
        public bool Add(QuestionExtend extend)
        {
            try{
                bool result = false;
                if(!IsExist(extend.QuestionID))
                {
                    QuestionExtendDao.Add(extend);
                    result=true;
                }
                return result;
            }
            catch (Autumn.Dao.DataIntegrityViolationException ex)
            {
                throw (new Exception(ex.Message));
            }
        }

        /// <summary>
        /// 修改解题思路.如果存在就更新，如果试题不存在解题思路就添加
        /// </summary>
        /// <param name="extend">试题扩充对象</param>
        public void Update(QuestionExtend extend)
        {
            if (extend == null)
                return;

            if (this.IsExist(extend.QuestionID))
            {
                QuestionExtendDao.Update(extend);
            }
            else
            {
                this.Add(extend);
            }
        }

        /// <summary>
        /// 删除解题思路
        /// </summary>
        /// <param name="questionID">试题ID</param>
        public void Delete(Guid questionID)
        {
            QuestionExtendDao.Delete(questionID);
        }

        /// <summary>
        /// 得到指定问题的解题思路
        /// </summary>
        /// <param name="questionID">试题ID</param>
        /// <returns>试题扩充对象</returns>
        public QuestionExtend GetQuestionExtend(Guid questionID)
        {
            QuestionExtend tmp=null;
            tmp = QuestionExtendDao.GetQuestionExtend(questionID);
            return tmp;
        }

        /// <summary>
        /// 是否存在解题思路
        /// </summary>
        /// <param name="questionID">试题ID</param>
        /// <returns>true为存在</returns>
        public bool IsExist(Guid questionID)
        {
            return QuestionExtendDao.IsExist(questionID);
        }
    }
}
