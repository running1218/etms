//==================================================================================================
//Version 1.0, auto-generated.
//Generated By running1218
//Date: 2012-4-18 11:41:22.
//==================================================================================================
//==================================================================================================
//This file is generated by CodeGenerator(Ver 2.1)
//==================================================================================================

using System;
using System.Collections.Generic;
using System.Data;
using ETMS.Utility.Logging;
using ETMS.Components.Evaluation.API.Entity;
using ETMS.Components.Evaluation.Implement.DAL;
using System.Transactions;
namespace ETMS.Components.Evaluation.Implement.BLL
{
    /// <summary>
    /// 评价文字记录表业务逻辑
    /// </summary>
    public partial class Evaluation_PlateResultLogic
	{
		private static readonly Evaluation_PlateResultDataAccess DAL = new Evaluation_PlateResultDataAccess();
		
		/// <summary>
		/// 增加
		/// </summary>
		public void Add(Evaluation_PlateResult evaluation_PlateResult)
		{
			DAL.Add(evaluation_PlateResult);
            BizLogHelper.AddOperate(evaluation_PlateResult);
		}


		/// <summary>
		/// 保存
		/// </summary>
		public void Update(Evaluation_PlateResult evaluation_PlateResult)
		{
            //修改前信息
            Evaluation_PlateResult originalEntity=GetById(evaluation_PlateResult.ResultSubID);
			DAL.Save(evaluation_PlateResult);
            BizLogHelper.UpdateOperate(originalEntity,evaluation_PlateResult);
		}

		/// <summary>
		/// 删除
		/// </summary>
		public void Remove(Guid resultSubID)
		{
            doRemove(resultSubID);
		} 

		/// <summary>
		/// 批量删除(主键ID数组）
		/// </summary>
		public void Remove(Guid[] resultSubIDs)
		{
#if !DEBUG
			using (TransactionScope ts = new TransactionScope())
			{
#endif
				foreach (Guid id in resultSubIDs  )
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
		public Evaluation_PlateResult GetById(Guid resultSubID)
		{
			Evaluation_PlateResult evaluation_PlateResult = DAL.GetById(resultSubID);
			if (evaluation_PlateResult == null)
			{
				throw new ETMS.AppContext.BusinessException("Evaluation.Evaluation_PlateResult.NotFoundException",new object[]{resultSubID});
			}
			
			return evaluation_PlateResult;
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
		public IList<Evaluation_PlateResult> GetEntityList(int pageIndex, int pageSize, string sortExpression, string criteria,out int totalRecords)
		{				
			return DAL.GetEntityList(pageIndex, pageSize, sortExpression, criteria, out totalRecords);
		}
		
	}
	
	
}

