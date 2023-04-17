//==================================================================================================
//Version 1.0, auto-generated.
//Generated By running1218
//Date: 2012-5-6 11:46:50.
//==================================================================================================
//==================================================================================================
//This file is generated by CodeGenerator(Ver 2.1)
//==================================================================================================

using System;
using System.Collections.Generic;
using System.Data;
using ETMS.Utility.Logging;
using ETMS.Components.IDP.API.Entity;
using ETMS.Components.IDP.Implement.DAL;
namespace ETMS.Components.IDP.Implement.BLL
{
    /// <summary>
    /// IDP计划学习内容明细表业务逻辑
    /// </summary>
    public partial class IDP_PlanContentDetailLogic
	{
		private static readonly IDP_PlanContentDetailDataAccess DAL = new IDP_PlanContentDetailDataAccess();
		
		/// <summary>
		/// 增加
		/// </summary>
		public void Add(IDP_PlanContentDetail iDP_PlanContentDetail)
		{
			DAL.Add(iDP_PlanContentDetail);
            BizLogHelper.AddOperate(iDP_PlanContentDetail);
		}


		/// <summary>
		/// 保存
		/// </summary>
		public void Update(IDP_PlanContentDetail iDP_PlanContentDetail)
		{
            //修改前信息
            IDP_PlanContentDetail originalEntity=GetById(iDP_PlanContentDetail.PlanContentDetailID);
			DAL.Save(iDP_PlanContentDetail);
            BizLogHelper.UpdateOperate(originalEntity,iDP_PlanContentDetail);
		}

		/// <summary>
		/// 删除
		/// </summary>
		public void Remove(Guid planContentDetailID)
		{
            doRemove(planContentDetailID);
		} 

		/// <summary>
		/// 批量删除(主键ID数组）
		/// </summary>
		public void Remove(Guid[] planContentDetailIDs)
		{
#if !DEBUG
			using (TransactionScope ts = new TransactionScope())
			{
#endif
				foreach (Guid id in planContentDetailIDs  )
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
		public IDP_PlanContentDetail GetById(Guid planContentDetailID)
		{
			IDP_PlanContentDetail iDP_PlanContentDetail = DAL.GetById(planContentDetailID);
			if (iDP_PlanContentDetail == null)
			{
				throw new ETMS.AppContext.BusinessException("IDP.IDP_PlanContentDetail.NotFoundException",new object[]{planContentDetailID});
			}
			
			return iDP_PlanContentDetail;
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
		public IList<IDP_PlanContentDetail> GetEntityList(int pageIndex, int pageSize, string sortExpression, string criteria,out int totalRecords)
		{				
			return DAL.GetEntityList(pageIndex, pageSize, sortExpression, criteria, out totalRecords);
		}
		
	}
	
	
}

