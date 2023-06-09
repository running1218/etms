//==================================================================================================
//Version 1.0, auto-generated.
//Generated By running1218
//Date: 2012-4-13 20:34:59.
//==================================================================================================
//==================================================================================================
//This file is generated by CodeGenerator(Ver 2.1)
//==================================================================================================

using System;
using System.Collections.Generic;
using System.Data;
using ETMS.Utility.Logging;
using ETMS.Components.Basic.API.Entity.Security;
using ETMS.Components.Basic.Implement.DAL.Security;
using System.Transactions;
namespace ETMS.Components.Basic.Implement.BLL.Security
{
    /// <summary>
    /// 系统异常日志业务逻辑
    /// </summary>
    public partial class Log_SystemExceptionLogic
	{
		private static readonly Log_SystemExceptionDataAccess DAL = new Log_SystemExceptionDataAccess();
		
		/// <summary>
		/// 增加
		/// </summary>
		public void Add(Log_SystemException log_SystemException)
		{
			DAL.Add(log_SystemException);
            BizLogHelper.AddOperate(log_SystemException);
		}


		/// <summary>
		/// 保存
		/// </summary>
		public void Update(Log_SystemException log_SystemException)
		{
            //修改前信息
            Log_SystemException originalEntity=GetById(log_SystemException.SysExLogID);
			DAL.Save(log_SystemException);
            BizLogHelper.UpdateOperate(originalEntity,log_SystemException);
		}

		/// <summary>
		/// 删除
		/// </summary>
		public void Remove(Int64 sysExLogID)
		{
            doRemove(sysExLogID);
		} 

		/// <summary>
		/// 批量删除(主键ID数组）
		/// </summary>
		public void Remove(Int64[] sysExLogIDs)
		{
#if !DEBUG
			using (TransactionScope ts = new TransactionScope())
			{
#endif
				foreach (Int64 id in sysExLogIDs  )
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
		public Log_SystemException GetById(Int64 sysExLogID)
		{
			Log_SystemException log_SystemException = DAL.GetById(sysExLogID);
			if (log_SystemException == null)
			{
				throw new ETMS.AppContext.BusinessException("Security.Log_SystemException.NotFoundException",new object[]{sysExLogID});
			}
			
			return log_SystemException;
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
		public IList<Log_SystemException> GetEntityList(int pageIndex, int pageSize, string sortExpression, string criteria,out int totalRecords)
		{				
			return DAL.GetEntityList(pageIndex, pageSize, sortExpression, criteria, out totalRecords);
		}
		
	}
	
	
}

