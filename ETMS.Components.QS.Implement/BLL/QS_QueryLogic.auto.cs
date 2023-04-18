//==================================================================================================
//Version 1.0, auto-generated.
//Generated By huangzf.
//Date: 2013/1/29 13:38:26.
//==================================================================================================
//==================================================================================================
//This file is generated by CodeGenerator(Ver 2.1)
//==================================================================================================

using System;
using System.Collections.Generic;
using System.Data;
using ETMS.Utility.Logging;
using ETMS.Components.QS.API.Entity;
using ETMS.Components.QS.Implement.DAL;
using System.Transactions;
namespace ETMS.Components.QS.Implement.BLL
{
    /// <summary>
    /// 问卷调查表业务逻辑
    /// </summary>
    public partial class QS_QueryLogic
	{
		private static readonly QS_QueryDataAccess DAL = new QS_QueryDataAccess();
		
		/// <summary>
		/// 增加
		/// </summary>
		public void Add(QS_Query qS_Query)
		{
			DAL.Add(qS_Query);
            BizLogHelper.AddOperate(qS_Query);
		}


		/// <summary>
		/// 保存
		/// </summary>
		public void Update(QS_Query qS_Query)
		{
            //修改前信息
            QS_Query originalEntity=GetById(qS_Query.QueryID);
			DAL.Save(qS_Query);
            BizLogHelper.UpdateOperate(originalEntity,qS_Query);
		}

		/// <summary>
		/// 删除
		/// </summary>
		public void Remove(Guid queryID)
		{
            doRemove(queryID);
		} 

		/// <summary>
		/// 批量删除(主键ID数组）
		/// </summary>
		public void Remove(Guid[] queryIDs)
		{
#if !DEBUG
			using (TransactionScope ts = new TransactionScope())
			{
#endif
				foreach (Guid id in queryIDs  )
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
		public QS_Query GetById(Guid queryID)
		{
			QS_Query qS_Query = DAL.GetById(queryID);
			if (qS_Query == null)
			{
				throw new ETMS.AppContext.BusinessException(".QS_Query.NotFoundException",new object[]{queryID});
			}
			
			return qS_Query;
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
		public DataTable GetPagedList(int pageIndex, int pageSize, string sortExpression, string criteria,out int totalRecords)
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
		public IList<QS_Query> GetEntityList(int pageIndex, int pageSize, string sortExpression, string criteria,out int totalRecords)
		{				
			return DAL.GetEntityList(pageIndex, pageSize, sortExpression, criteria, out totalRecords);
		}
		
	}
	
	
}

