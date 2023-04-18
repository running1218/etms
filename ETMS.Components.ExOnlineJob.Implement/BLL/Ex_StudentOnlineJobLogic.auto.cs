//==================================================================================================
//Version 1.0, auto-generated.
//Generated By running1218
//Date: 2012-4-12 9:58:40.
//==================================================================================================
//==================================================================================================
//This file is generated by CodeGenerator(Ver 2.1)
//==================================================================================================

using System;
using System.Collections.Generic;
using System.Data;
using ETMS.Utility.Logging;
using ETMS.Components.ExOnlineJob.API.Entity.ExOnlineJob;
using ETMS.Components.ExOnlineJob.Implement.DAL.ExOnlineJob;
namespace ETMS.Components.ExOnlineJob.Implement.BLL.ExOnlineJob
{
    /// <summary>
    /// 业务逻辑
    /// </summary>
    public partial class Ex_StudentOnlineJobLogic
	{
		private static readonly Ex_StudentOnlineJobDataAccess DAL = new Ex_StudentOnlineJobDataAccess();
		
		/// <summary>
		/// 增加
		/// </summary>
		public void Add(Ex_StudentOnlineJob ex_StudentOnlineJob)
		{
			DAL.Add(ex_StudentOnlineJob);
            BizLogHelper.AddOperate(ex_StudentOnlineJob);
		}


		/// <summary>
		/// 保存
		/// </summary>
		public void Update(Ex_StudentOnlineJob ex_StudentOnlineJob)
		{
            //修改前信息
            Ex_StudentOnlineJob originalEntity=GetById(ex_StudentOnlineJob.StudentOnlineJobID);
			DAL.Save(ex_StudentOnlineJob);
            BizLogHelper.UpdateOperate(originalEntity,ex_StudentOnlineJob);
		}

		/// <summary>
		/// 删除
		/// </summary>
		public void Remove(Guid studentOnlineJobID)
		{
            doRemove(studentOnlineJobID);
		} 

		/// <summary>
		/// 批量删除(主键ID数组）
		/// </summary>
		public void Remove(Guid[] studentOnlineJobIDs)
		{
				foreach (Guid id in studentOnlineJobIDs  )
				{
					Remove(id);
				}
		} 
    
    
		/// <summary>
		/// 根据标识获取对象
		/// </summary>
		public Ex_StudentOnlineJob GetById(Guid studentOnlineJobID)
		{
			Ex_StudentOnlineJob ex_StudentOnlineJob = DAL.GetById(studentOnlineJobID);
			if (ex_StudentOnlineJob == null)
			{
				throw new ETMS.AppContext.BusinessException("ExOnlineJob.Ex_StudentOnlineJob.NotFoundException",new object[]{studentOnlineJobID});
			}
			
			return ex_StudentOnlineJob;
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
		public IList<Ex_StudentOnlineJob> GetEntityList(int pageIndex, int pageSize, string sortExpression, string criteria,out int totalRecords)
		{				
			return DAL.GetEntityList(pageIndex, pageSize, sortExpression, criteria, out totalRecords);
		}
		
	}
	
	
}

