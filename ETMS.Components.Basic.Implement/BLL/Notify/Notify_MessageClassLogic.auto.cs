//==================================================================================================
//Version 1.0, auto-generated.
//Generated By running1218
//Date: 2012-4-11 9:06:02.
//==================================================================================================
//==================================================================================================
//This file is generated by CodeGenerator(Ver 2.1)
//==================================================================================================

using System;
using System.Collections.Generic;
using System.Data;
using ETMS.Utility.Logging;
using ETMS.Components.Basic.API.Entity.Notify;
using ETMS.Components.Basic.Implement.DAL.Notify;
using System.Transactions;
namespace ETMS.Components.Basic.Implement.BLL.Notify
{
    /// <summary>
    /// 消息类别，指定了消息业务归类业务逻辑
    /// </summary>
    public partial class Notify_MessageClassLogic
	{
		private static readonly Notify_MessageClassDataAccess DAL = new Notify_MessageClassDataAccess();
		
		/// <summary>
		/// 增加
		/// </summary>
		public void Add(Notify_MessageClass notify_MessageClass)
		{
			DAL.Add(notify_MessageClass);
            BizLogHelper.AddOperate(notify_MessageClass);
		}


		/// <summary>
		/// 保存
		/// </summary>
		public void Update(Notify_MessageClass notify_MessageClass)
		{
            //修改前信息
            Notify_MessageClass originalEntity=GetById(notify_MessageClass.MessageClassID);
			DAL.Save(notify_MessageClass);
            BizLogHelper.UpdateOperate(originalEntity,notify_MessageClass);
		}

		/// <summary>
		/// 删除
		/// </summary>
		public void Remove(Int16 messageClassID)
		{
            doRemove(messageClassID);
		} 

		/// <summary>
		/// 批量删除(主键ID数组）
		/// </summary>
		public void Remove(Int16[] messageClassIDs)
		{
#if !DEBUG
			using (TransactionScope ts = new TransactionScope())
			{
#endif
				foreach (Int16 id in messageClassIDs  )
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
		public Notify_MessageClass GetById(Int16 messageClassID)
		{
			Notify_MessageClass notify_MessageClass = DAL.GetById(messageClassID);
			if (notify_MessageClass == null)
			{
				throw new ETMS.AppContext.BusinessException("Notify.Notify_MessageClass.NotFoundException",new object[]{messageClassID});
			}
			
			return notify_MessageClass;
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
		public IList<Notify_MessageClass> GetEntityList(int pageIndex, int pageSize, string sortExpression, string criteria,out int totalRecords)
		{				
			return DAL.GetEntityList(pageIndex, pageSize, sortExpression, criteria, out totalRecords);
		}
		
	}
	
	
}

