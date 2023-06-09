
using System;
using System.Collections.Generic;
using System.Transactions;
using System.Data;
using ETMS.Utility.Logging;
using ETMS.Components.Basic.API.Entity.Bulletin;
using ETMS.Components.Basic.Implement.DAL.Bulletin;
namespace ETMS.Components.Basic.Implement.BLL.Bulletin
{
    /// <summary>
    /// 公告发布对象表业务逻辑
    /// </summary>
    public partial class Inf_BulletinObjectLogic
	{
		private static readonly Inf_BulletinObjectDataAccess DAL = new Inf_BulletinObjectDataAccess();
		
		/// <summary>
		/// 增加
		/// </summary>
		public void Add(Inf_BulletinObject inf_BulletinObject)
		{
			DAL.Add(inf_BulletinObject);
            BizLogHelper.AddOperate(inf_BulletinObject);
		}

		/// <summary>
		/// 删除
		/// </summary>
		public void Remove(Guid bulletinObjectID)
		{
			DAL.Remove(bulletinObjectID);
		}
		
		/// <summary>
		/// 删除
		/// </summary>
		public void Remove(Inf_BulletinObject inf_BulletinObject)
		{
			Remove(inf_BulletinObject.BulletinObjectID);
            BizLogHelper.DeleteOperate(inf_BulletinObject);
		}
		
		/// <summary>
		/// 批量删除
		/// </summary>
		public void Remove(List<Inf_BulletinObject> inf_BulletinObjects)
		{
			using (TransactionScope ts = new TransactionScope())
			{
				foreach (Inf_BulletinObject inf_BulletinObject in inf_BulletinObjects)
				{
					Remove(inf_BulletinObject);
				}
				
				ts.Complete();
			}
		}
    
		/// <summary>
		/// 保存
		/// </summary>
		public void Save(Inf_BulletinObject inf_BulletinObject)
		{
            //修改前信息
            Inf_BulletinObject originalEntity=GetById(inf_BulletinObject.BulletinObjectID);
			DAL.Save(inf_BulletinObject);
            BizLogHelper.UpdateOperate(originalEntity,inf_BulletinObject);
		}
    
		/// <summary>
		/// 根据标识获取对象
		/// </summary>
		public Inf_BulletinObject GetById(Guid bulletinObjectID)
		{
			Inf_BulletinObject inf_BulletinObject = DAL.GetById(bulletinObjectID);
			if (inf_BulletinObject == null)
			{
				throw new ETMS.AppContext.BusinessException("Bulletin.Inf_BulletinObject.NotFoundException",new object[]{bulletinObjectID});
			}
			
			return inf_BulletinObject;
		}		
		 
		/// <summary>
        	/// 查询分页数据
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
		
	}
	
	
}

