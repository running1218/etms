
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
    /// 公告已读记录表业务逻辑
    /// </summary>
    public partial class Inf_BulletinReadLogic
	{
		private static readonly Inf_BulletinReadDataAccess DAL = new Inf_BulletinReadDataAccess();
		
		/// <summary>
		/// 增加
		/// </summary>
		public void Add(Inf_BulletinRead inf_BulletinRead)
		{
			DAL.Add(inf_BulletinRead);
            BizLogHelper.AddOperate(inf_BulletinRead);
		}

		/// <summary>
		/// 删除
		/// </summary>
		public void Remove(Int32 articleClickID)
		{
			DAL.Remove(articleClickID);
		}
		
		/// <summary>
		/// 删除
		/// </summary>
		public void Remove(Inf_BulletinRead inf_BulletinRead)
		{
			Remove(inf_BulletinRead.ArticleClickID);
            BizLogHelper.DeleteOperate(inf_BulletinRead);
		}
		
		/// <summary>
		/// 批量删除
		/// </summary>
		public void Remove(List<Inf_BulletinRead> inf_BulletinReads)
		{
			using (TransactionScope ts = new TransactionScope())
			{
				foreach (Inf_BulletinRead inf_BulletinRead in inf_BulletinReads)
				{
					Remove(inf_BulletinRead);
				}
				
				ts.Complete();
			}
		}
    
		/// <summary>
		/// 保存
		/// </summary>
		public void Save(Inf_BulletinRead inf_BulletinRead)
		{
            //修改前信息
            Inf_BulletinRead originalEntity=GetById(inf_BulletinRead.ArticleClickID);
			DAL.Save(inf_BulletinRead);
            BizLogHelper.UpdateOperate(originalEntity,inf_BulletinRead);
		}
    
		/// <summary>
		/// 根据标识获取对象
		/// </summary>
		public Inf_BulletinRead GetById(Int32 articleClickID)
		{
			Inf_BulletinRead inf_BulletinRead = DAL.GetById(articleClickID);
			if (inf_BulletinRead == null)
			{
				throw new ETMS.AppContext.BusinessException("Bulletin.Inf_BulletinRead.NotFoundException",new object[]{articleClickID});
			}
			
			return inf_BulletinRead;
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

        public int GetReadNum(int articlID)
        {
            return DAL.GetReadNum(articlID);
        }
	}
	
	
}

