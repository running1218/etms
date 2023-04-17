//==================================================================================================
//Version 1.0, auto-generated.
//Generated By running1218
//Date: 2012-4-12 9:56:35.
//==================================================================================================
//==================================================================================================
//This file is generated by CodeGenerator(Ver 2.1)
//==================================================================================================

using System;
using System.Collections.Generic;
using System.Data;
using ETMS.Utility.Logging;
using ETMS.Components.ExOnlineTest.API.Entity.ExOnlineTest;
using ETMS.Components.ExOnlineTest.Implement.DAL.ExOnlineTest;
namespace ETMS.Components.ExOnlineTest.Implement.BLL.ExOnlineTest
{
    /// <summary>
    /// 业务逻辑
    /// </summary>
    public partial class Ex_StudentOnlineTestLogic
	{
		private static readonly Ex_StudentOnlineTestDataAccess DAL = new Ex_StudentOnlineTestDataAccess();
		
		/// <summary>
		/// 增加
		/// </summary>
		public void Add(Ex_StudentOnlineTest ex_StudentOnlineTest)
		{
			DAL.Add(ex_StudentOnlineTest);
            BizLogHelper.AddOperate(ex_StudentOnlineTest);
		}


		/// <summary>
		/// 保存
		/// </summary>
		public void Update(Ex_StudentOnlineTest ex_StudentOnlineTest)
		{
            //修改前信息
            Ex_StudentOnlineTest originalEntity=GetById(ex_StudentOnlineTest.StudentOnlineTestID);
			DAL.Save(ex_StudentOnlineTest);
            BizLogHelper.UpdateOperate(originalEntity,ex_StudentOnlineTest);
		}

		/// <summary>
		/// 删除
		/// </summary>
		public void Remove(Guid studentOnlineTestID)
		{
            doRemove(studentOnlineTestID);
		} 

		/// <summary>
		/// 批量删除(主键ID数组）
		/// </summary>
		public void Remove(Guid[] studentOnlineTestIDs)
		{
#if !DEBUG
			using (TransactionScope ts = new TransactionScope())
			{
#endif
				foreach (Guid id in studentOnlineTestIDs  )
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
		public Ex_StudentOnlineTest GetById(Guid studentOnlineTestID)
		{
			Ex_StudentOnlineTest ex_StudentOnlineTest = DAL.GetById(studentOnlineTestID);
			if (ex_StudentOnlineTest == null)
			{
				throw new ETMS.AppContext.BusinessException("ExOnlineTest.Ex_StudentOnlineTest.NotFoundException",new object[]{studentOnlineTestID});
			}
			
			return ex_StudentOnlineTest;
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
		public IList<Ex_StudentOnlineTest> GetEntityList(int pageIndex, int pageSize, string sortExpression, string criteria,out int totalRecords)
		{				
			return DAL.GetEntityList(pageIndex, pageSize, sortExpression, criteria, out totalRecords);
		}
		
	}
	
	
}

