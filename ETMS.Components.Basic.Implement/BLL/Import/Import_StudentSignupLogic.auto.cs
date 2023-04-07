//==================================================================================================
//Version 1.0, auto-generated.
//Generated By huangzhf.
//Date: 2012-5-19 11:09:46.
//==================================================================================================
//==================================================================================================
//This file is generated by CodeGenerator(Ver 2.1), a product of ZhouYonghua(Zhou_Yonghua@163.com).
//==================================================================================================

using System;
using System.Collections.Generic;
using System.Data;
using ETMS.Utility.Logging;
using ETMS.Components.Basic.API.Entity.Import;
using ETMS.Components.Basic.Implement.DAL.Import;
namespace ETMS.Components.Basic.Implement.BLL.Import
{
    /// <summary>
    /// 项目学员导入明细表业务逻辑
    /// </summary>
    public partial class Import_StudentSignupLogic
	{
		private static readonly Import_StudentSignupDataAccess DAL = new Import_StudentSignupDataAccess();
		
		/// <summary>
		/// 增加
		/// </summary>
		public void Add(Import_StudentSignup import_StudentSignup)
		{
			DAL.Add(import_StudentSignup);
            BizLogHelper.AddOperate(import_StudentSignup);
		}


		/// <summary>
		/// 保存
		/// </summary>
		public void Update(Import_StudentSignup import_StudentSignup)
		{
            //修改前信息
            Import_StudentSignup originalEntity=GetById(import_StudentSignup.DetailID);
			DAL.Save(import_StudentSignup);
            BizLogHelper.UpdateOperate(originalEntity,import_StudentSignup);
		}

		/// <summary>
		/// 删除
		/// </summary>
		public void Remove(Int32 detailID)
		{
            doRemove(detailID);
		} 

		/// <summary>
		/// 批量删除(主键ID数组）
		/// </summary>
		public void Remove(Int32[] detailIDs)
		{
#if !DEBUG
			using (TransactionScope ts = new TransactionScope())
			{
#endif
				foreach (Int32 id in detailIDs  )
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
		public Import_StudentSignup GetById(Int32 detailID)
		{
			Import_StudentSignup import_StudentSignup = DAL.GetById(detailID);
			if (import_StudentSignup == null)
			{
				throw new ETMS.AppContext.BusinessException("Import.Import_StudentSignup.NotFoundException",new object[]{detailID});
			}
			
			return import_StudentSignup;
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
		public IList<Import_StudentSignup> GetEntityList(int pageIndex, int pageSize, string sortExpression, string criteria,out int totalRecords)
		{				
			return DAL.GetEntityList(pageIndex, pageSize, sortExpression, criteria, out totalRecords);
		}
		
	}
	
	
}

