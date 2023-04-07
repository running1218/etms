//==================================================================================================
//Version 1.0, auto-generated.
//Generated By xueyb.
//Date: 2012-4-17 15:53:39.
//==================================================================================================
//==================================================================================================
//This file is generated by CodeGenerator(Ver 2.1), a product of ZhouYonghua(Zhou_Yonghua@163.com).
//==================================================================================================

using System;
using System.Collections.Generic;
using System.Data;
using ETMS.Utility.Logging;
using ETMS.Components.Poll.API.Entity;
using ETMS.Components.Poll.Implement.DAL;
namespace ETMS.Components.Poll.Implement.BLL
{
    /// <summary>
    /// 业务逻辑
    /// </summary>
    public partial class Poll_UserResourceQueryResultLogic
    {
        private static readonly Poll_UserResourceQueryResultDataAccess DAL = new Poll_UserResourceQueryResultDataAccess();

        /// <summary>
        /// 增加
        /// </summary>
        public void Add(Poll_UserResourceQueryResult poll_UserResourceQueryResult)
        {
            DAL.Add(poll_UserResourceQueryResult);
            BizLogHelper.AddOperate(poll_UserResourceQueryResult);
        }


        /// <summary>
        /// 保存
        /// </summary>
        public void Update(Poll_UserResourceQueryResult poll_UserResourceQueryResult)
        {
            //修改前信息
            Poll_UserResourceQueryResult originalEntity = GetById(poll_UserResourceQueryResult.BatchID);
            DAL.Save(poll_UserResourceQueryResult);
            BizLogHelper.UpdateOperate(originalEntity, poll_UserResourceQueryResult);
        }

        /// <summary>
        /// 删除
        /// </summary>
        public void Remove(Int32 batchID)
        {
            doRemove(batchID);
        }

        /// <summary>
        /// 批量删除(主键ID数组）
        /// </summary>
        public void Remove(Int32[] batchIDs)
        {
#if !DEBUG
			using (TransactionScope ts = new TransactionScope())
			{
#endif
            foreach (Int32 id in batchIDs)
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
        public Poll_UserResourceQueryResult GetById(Int32 batchID)
        {
            Poll_UserResourceQueryResult poll_UserResourceQueryResult = DAL.GetById(batchID);
            if (poll_UserResourceQueryResult == null)
            {
                throw new ETMS.AppContext.BusinessException(".Poll_UserResourceQueryResult.NotFoundException", new object[] { batchID });
            }

            return poll_UserResourceQueryResult;
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
        public IList<Poll_UserResourceQueryResult> GetEntityList(int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {
            return DAL.GetEntityList(pageIndex, pageSize, sortExpression, criteria, out totalRecords);
        }

        public DataTable GetEntityListByQueryID(int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {
            return DAL.GetEntityListByQueryID(pageIndex, pageSize, sortExpression, criteria, out totalRecords);
        }

        public DataTable GetUnSubmitUserByQueryID(int pageIndex, int pageSize, int queryID, string resourceTypeCode, out int totalRecords)
        {
            return DAL.GetUnSubmitUserByQueryID(pageIndex, pageSize, queryID, resourceTypeCode, out totalRecords);
        }

        public DataTable GetSubmitTotalUserByQuerID(int queryID, string resourceTypeCode)
        {
            return DAL.GetSubmitTotalUserByQuerID(queryID, resourceTypeCode);
        }
    }


}

