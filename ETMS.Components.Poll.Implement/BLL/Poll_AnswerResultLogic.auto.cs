
using System;
using System.Collections.Generic;
using System.Data;
using ETMS.Utility.Logging;
using ETMS.Components.Poll.API.Entity;
using ETMS.Components.Poll.Implement.DAL;
using System.Transactions;
namespace ETMS.Components.Poll.Implement.BLL
{
    /// <summary>
    /// 调查答案表业务逻辑
    /// </summary>
    public partial class Poll_AnswerResultLogic
    {
        private static readonly Poll_AnswerResultDataAccess DAL = new Poll_AnswerResultDataAccess();

        /// <summary>
        /// 增加
        /// </summary>
        public void Add(Poll_AnswerResult poll_AnswerResult)
        {
            DAL.Add(poll_AnswerResult);
            BizLogHelper.AddOperate(poll_AnswerResult);
        }


        /// <summary>
        /// 保存
        /// </summary>
        public void Update(Poll_AnswerResult poll_AnswerResult)
        {
            //修改前信息
            Poll_AnswerResult originalEntity = GetById(poll_AnswerResult.AnswerResultID);
            DAL.Save(poll_AnswerResult);
            BizLogHelper.UpdateOperate(originalEntity, poll_AnswerResult);
        }

        /// <summary>
        /// 删除
        /// </summary>
        public void Remove(Int32 answerResultID)
        {
            doRemove(answerResultID);
        }

        /// <summary>
        /// 批量删除(主键ID数组）
        /// </summary>
        public void Remove(Int32[] answerResultIDs)
        {
#if !DEBUG
			using (TransactionScope ts = new TransactionScope())
			{
#endif
            foreach (Int32 id in answerResultIDs)
            {
                Remove(id);
            }
#if !DEBUG
				ts.Complete();
			}
#endif
        }


        /// <summary>
        /// 根据标识获取对象
        /// </summary>
        public Poll_AnswerResult GetById(Int32 answerResultID)
        {
            Poll_AnswerResult poll_AnswerResult = DAL.GetById(answerResultID);
            if (poll_AnswerResult == null)
            {
                throw new ETMS.AppContext.BusinessException(".Poll_AnswerResult.NotFoundException", new object[] { answerResultID });
            }

            return poll_AnswerResult;
        }

        /// <summary>
        /// 查询数据列表分页数据
        /// </summary>
        /// <param name="pageIndex">页号</param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="sortExpression">排序条件</param>
        /// <param name="criteria">筛选条件</param>
        /// <param name="totalRecords">out 记录总数</param>
        /// <returns>返回查询结果</returns>
        public DataTable GetPagedList(int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {
            return DAL.GetPagedList(pageIndex, pageSize, sortExpression, criteria, out totalRecords);
        }

        /// <summary>
        /// 查询实体分页数据
        /// </summary>
        /// <param name="pageIndex">页号</param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="sortExpression">排序条件</param>
        /// <param name="criteria">筛选条件</param>
        /// <param name="totalRecords">out 记录总数</param>
        /// <returns>返回查询结果</returns>
        public IList<Poll_AnswerResult> GetEntityList(int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {
            return DAL.GetEntityList(pageIndex, pageSize, sortExpression, criteria, out totalRecords);
        }

    }


}

