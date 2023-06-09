
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
    /// 公告表业务逻辑
    /// </summary>
    public partial class Inf_BulletinLogic
	{
		private static readonly Inf_BulletinDataAccess DAL = new Inf_BulletinDataAccess();
		
		/// <summary>
		/// 增加
		/// </summary>
		public void Add(Inf_Bulletin inf_Bulletin)
		{
			DAL.Add(inf_Bulletin);
            BizLogHelper.AddOperate(inf_Bulletin);
		}

		/// <summary>
		/// 删除
		/// </summary>
		public void Remove(Int32 articleID)
		{
			DAL.Remove(articleID);
		}
		
		/// <summary>
		/// 删除
		/// </summary>
		public void Remove(Inf_Bulletin inf_Bulletin)
		{
			Remove(inf_Bulletin.ArticleID);
            BizLogHelper.DeleteOperate(inf_Bulletin);
		}
		
		/// <summary>
		/// 批量删除
		/// </summary>
		public void Remove(List<Inf_Bulletin> inf_Bulletins)
		{
			using (TransactionScope ts = new TransactionScope())
			{
				foreach (Inf_Bulletin inf_Bulletin in inf_Bulletins)
				{
					Remove(inf_Bulletin);
				}
				
				ts.Complete();
			}
		}
    
		/// <summary>
		/// 保存
		/// </summary>
		public void Save(Inf_Bulletin inf_Bulletin)
		{
            //修改前信息
            Inf_Bulletin originalEntity=GetById(inf_Bulletin.ArticleID);
			DAL.Save(inf_Bulletin);
            BizLogHelper.UpdateOperate(originalEntity,inf_Bulletin);
		}
    
		/// <summary>
		/// 根据标识获取对象
		/// </summary>
		public Inf_Bulletin GetById(Int32 articleID)
		{
			Inf_Bulletin inf_Bulletin = DAL.GetById(articleID);
			if (inf_Bulletin == null)
			{
				throw new ETMS.AppContext.BusinessException("Bulletin.Inf_Bulletin.NotFoundException",new object[]{articleID});
			}
			
			return inf_Bulletin;
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

