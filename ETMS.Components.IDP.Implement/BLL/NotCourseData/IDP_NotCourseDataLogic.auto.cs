//==================================================================================================
//Version 1.0, auto-generated.
//Generated By huangzhf.
//Date: 2012-5-4 9:58:08.
//==================================================================================================
//==================================================================================================
//This file is generated by CodeGenerator(Ver 2.1)
//==================================================================================================

using System;
using System.Collections.Generic;
using System.Data;
using ETMS.Utility.Logging;
using ETMS.Components.IDP.API.Entity.NotCourseData;
using ETMS.Components.IDP.Implement.DAL.NotCourseData;
namespace ETMS.Components.IDP.Implement.BLL.NotCourseData
{
    /// <summary>
    /// IDP非课程资料表业务逻辑
    /// </summary>
    public partial class IDP_NotCourseDataLogic
	{
		private static readonly IDP_NotCourseDataDataAccess DAL = new IDP_NotCourseDataDataAccess();
		
		/// <summary>
		/// 增加
		/// </summary>
		public void Add(IDP_NotCourseData iDP_NotCourseData)
		{
			DAL.Add(iDP_NotCourseData);
            BizLogHelper.AddOperate(iDP_NotCourseData);
		}


		/// <summary>
		/// 保存
		/// </summary>
		public void Update(IDP_NotCourseData iDP_NotCourseData)
		{
            //修改前信息
            IDP_NotCourseData originalEntity=GetById(iDP_NotCourseData.IDP_NotCourseDataID);
			DAL.Save(iDP_NotCourseData);
            BizLogHelper.UpdateOperate(originalEntity,iDP_NotCourseData);
		}

		/// <summary>
		/// 删除
		/// </summary>
		public void Remove(Guid iDP_NotCourseDataID)
		{
            doRemove(iDP_NotCourseDataID);
		} 

		/// <summary>
		/// 批量删除(主键ID数组）
		/// </summary>
		public void Remove(Guid[] iDP_NotCourseDataIDs)
		{
				foreach (Guid id in iDP_NotCourseDataIDs  )
				{
					Remove(id);
				}
		} 
    
    
		/// <summary>
		/// 根据标识获取对象
		/// </summary>
		public IDP_NotCourseData GetById(Guid iDP_NotCourseDataID)
		{
			IDP_NotCourseData iDP_NotCourseData = DAL.GetById(iDP_NotCourseDataID);
			if (iDP_NotCourseData == null)
			{
				throw new ETMS.AppContext.BusinessException("NotCourseData.IDP_NotCourseData.NotFoundException",new object[]{iDP_NotCourseDataID});
			}
			
			return iDP_NotCourseData;
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
		public IList<IDP_NotCourseData> GetEntityList(int pageIndex, int pageSize, string sortExpression, string criteria,out int totalRecords)
		{				
			return DAL.GetEntityList(pageIndex, pageSize, sortExpression, criteria, out totalRecords);
		}
		
	}
	
	
}

