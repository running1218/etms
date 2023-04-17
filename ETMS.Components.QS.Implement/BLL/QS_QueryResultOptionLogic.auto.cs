//==================================================================================================
//Version 1.0, auto-generated.
//Generated By huangzf.
//Date: 2013-1-24 10:33:04.
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
namespace ETMS.Components.QS.Implement.BLL
{
    /// <summary>
    /// 选择题作答结果表业务逻辑
    /// </summary>
    public partial class QS_QueryResultOptionLogic
	{
		private static readonly QS_QueryResultOptionDataAccess DAL = new QS_QueryResultOptionDataAccess();
		
		/// <summary>
		/// 增加
		/// </summary>
		public void Add(QS_QueryResultOption qS_QueryResultOption)
		{
			DAL.Add(qS_QueryResultOption);
            BizLogHelper.AddOperate(qS_QueryResultOption);
		}


		/// <summary>
		/// 保存
		/// </summary>
		public void Update(QS_QueryResultOption qS_QueryResultOption)
		{
            //修改前信息
            QS_QueryResultOption originalEntity=GetById(qS_QueryResultOption.QueryResultID);
			DAL.Save(qS_QueryResultOption);
            BizLogHelper.UpdateOperate(originalEntity,qS_QueryResultOption);
		}

		/// <summary>
		/// 删除
		/// </summary>
		public void Remove(Guid queryResultID)
		{
            doRemove(queryResultID);
		} 

		/// <summary>
		/// 批量删除(主键ID数组）
		/// </summary>
		public void Remove(Guid[] queryResultIDs)
		{
#if !DEBUG
			using (TransactionScope ts = new TransactionScope())
			{
#endif
				foreach (Guid id in queryResultIDs  )
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
		public QS_QueryResultOption GetById(Guid queryResultID)
		{
			QS_QueryResultOption qS_QueryResultOption = DAL.GetById(queryResultID);
			if (qS_QueryResultOption == null)
			{
				throw new ETMS.AppContext.BusinessException(".QS_QueryResultOption.NotFoundException",new object[]{queryResultID});
			}
			
			return qS_QueryResultOption;
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
		public IList<QS_QueryResultOption> GetEntityList(int pageIndex, int pageSize, string sortExpression, string criteria,out int totalRecords)
		{				
			return DAL.GetEntityList(pageIndex, pageSize, sortExpression, criteria, out totalRecords);
		}
		
	}
	
	
}

