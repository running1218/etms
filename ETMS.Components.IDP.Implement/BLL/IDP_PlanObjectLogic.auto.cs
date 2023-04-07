//==================================================================================================
//Version 1.0, auto-generated.
//Generated By xueyb.
//Date: 2012-5-7 11:45:02.
//==================================================================================================
//==================================================================================================
//This file is generated by CodeGenerator(Ver 2.1), a product of ZhouYonghua(Zhou_Yonghua@163.com).
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
    /// IDP计划目标表业务逻辑
    /// </summary>
    public partial class IDP_PlanObjectLogic
	{
		private static readonly IDP_PlanObjectDataAccess DAL = new IDP_PlanObjectDataAccess();
		
		/// <summary>
		/// 增加
		/// </summary>
		public void Add(IDP_PlanObject iDP_PlanObject)
		{
			DAL.Add(iDP_PlanObject);
            BizLogHelper.AddOperate(iDP_PlanObject);
		}


		/// <summary>
		/// 保存
		/// </summary>
		public void Update(IDP_PlanObject iDP_PlanObject)
		{
            //修改前信息
            IDP_PlanObject originalEntity=GetById(iDP_PlanObject.IDPPlanObjectID);
			DAL.Save(iDP_PlanObject);
            BizLogHelper.UpdateOperate(originalEntity,iDP_PlanObject);
		}

		/// <summary>
		/// 删除
		/// </summary>
		public void Remove(Guid iDPPlanObjectID)
		{
            doRemove(iDPPlanObjectID);
		} 

		/// <summary>
		/// 批量删除(主键ID数组）
		/// </summary>
		public void Remove(Guid[] iDPPlanObjectIDs)
		{
#if !DEBUG
			using (TransactionScope ts = new TransactionScope())
			{
#endif
				foreach (Guid id in iDPPlanObjectIDs  )
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
		public IDP_PlanObject GetById(Guid iDPPlanObjectID)
		{
			IDP_PlanObject iDP_PlanObject = DAL.GetById(iDPPlanObjectID);
			if (iDP_PlanObject == null)
			{
				throw new ETMS.AppContext.BusinessException("IDP.IDP_PlanObject.NotFoundException",new object[]{iDPPlanObjectID});
			}
			
			return iDP_PlanObject;
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
		public IList<IDP_PlanObject> GetEntityList(int pageIndex, int pageSize, string sortExpression, string criteria,out int totalRecords)
		{				
			return DAL.GetEntityList(pageIndex, pageSize, sortExpression, criteria, out totalRecords);
		}
		
	}
	
	
}

