//==================================================================================================
//Version 1.0, auto-generated.
//Generated By xueyb.
//Date: 2012-4-11 9:06:02.
//==================================================================================================
//==================================================================================================
//This file is generated by CodeGenerator(Ver 2.1), a product of ZhouYonghua(Zhou_Yonghua@163.com).
//==================================================================================================

using System;
using System.Collections.Generic;
using System.Data;
using ETMS.Utility.Logging;
using ETMS.Components.Basic.API.Entity.Notify;
using ETMS.Components.Basic.Implement.DAL.Notify;
namespace ETMS.Components.Basic.Implement.BLL.Notify
{
    /// <summary>
    /// 消息提醒清单业务逻辑
    /// </summary>
    public partial class Notify_MessageLogic
	{
		private static readonly Notify_MessageDataAccess DAL = new Notify_MessageDataAccess();
		
		/// <summary>
		/// 增加
		/// </summary>
		public void Add(Notify_Message notify_Message)
		{
			DAL.Add(notify_Message);
            BizLogHelper.AddOperate(notify_Message);
		}


		/// <summary>
		/// 保存
		/// </summary>
		public void Update(Notify_Message notify_Message)
		{
            //修改前信息
            Notify_Message originalEntity=GetById(notify_Message.MessageID);
			DAL.Save(notify_Message);
            BizLogHelper.UpdateOperate(originalEntity,notify_Message);
		}

		/// <summary>
		/// 删除
		/// </summary>
		public void Remove(Int32 messageID)
		{
            doRemove(messageID);
		} 

		/// <summary>
		/// 批量删除(主键ID数组）
		/// </summary>
		public void Remove(Int32[] messageIDs)
		{
#if !DEBUG
			using (TransactionScope ts = new TransactionScope())
			{
#endif
				foreach (Int32 id in messageIDs  )
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
		public Notify_Message GetById(Int32 messageID)
		{
			Notify_Message notify_Message = DAL.GetById(messageID);
			if (notify_Message == null)
			{
				throw new ETMS.AppContext.BusinessException("Notify.Notify_Message.NotFoundException",new object[]{messageID});
			}
			
			return notify_Message;
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
		public IList<Notify_Message> GetEntityList(int pageIndex, int pageSize, string sortExpression, string criteria,out int totalRecords)
		{				
			return DAL.GetEntityList(pageIndex, pageSize, sortExpression, criteria, out totalRecords);
		}
		
	}
	
	
}

