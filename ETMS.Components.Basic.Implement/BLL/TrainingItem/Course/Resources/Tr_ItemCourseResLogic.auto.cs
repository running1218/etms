//==================================================================================================
//Version 1.0, auto-generated.
//Generated By huangzhongfu.
//Date: 2012-3-31 8:58:53.
//==================================================================================================
//==================================================================================================
//This file is generated by CodeGenerator(Ver 2.1)
//==================================================================================================

using System;
using System.Collections.Generic;
using System.Transactions;
using System.Data;
using ETMS.Utility.Logging;
using ETMS.Components.Basic.API.Entity.TrainingItem.Course.Resources;
using ETMS.Components.Basic.Implement.DAL.TrainingItem.Course.Resources;
namespace ETMS.Components.Basic.Implement.BLL.TrainingItem.Course.Resources
{
    /// <summary>
    /// 培训项目课程资源表业务逻辑
    /// </summary>
    public partial class Tr_ItemCourseResLogic
	{
		private static readonly Tr_ItemCourseResDataAccess DAL = new Tr_ItemCourseResDataAccess();
		
		/// <summary>
		/// 增加
		/// </summary>
		public void Add(Tr_ItemCourseRes tr_ItemCourseRes)
		{
			DAL.Add(tr_ItemCourseRes);
            BizLogHelper.AddOperate(tr_ItemCourseRes);
		}

		/// <summary>
		/// 删除
		/// </summary>
		public void Remove(Guid itemCourseResID)
		{
			DAL.Remove(itemCourseResID);
		}
		
		/// <summary>
		/// 删除
		/// </summary>
		public void Remove(Tr_ItemCourseRes tr_ItemCourseRes)
		{
			Remove(tr_ItemCourseRes.ItemCourseResID);
            BizLogHelper.DeleteOperate(tr_ItemCourseRes);
		}
		
		/// <summary>
		/// 批量删除
		/// </summary>
		public void Remove(List<Tr_ItemCourseRes> tr_ItemCourseRess)
		{
			using (TransactionScope ts = new TransactionScope())
			{
				foreach (Tr_ItemCourseRes tr_ItemCourseRes in tr_ItemCourseRess)
				{
					Remove(tr_ItemCourseRes);
				}
				
				ts.Complete();
			}
		}
    
		/// <summary>
		/// 保存
		/// </summary>
		public void Save(Tr_ItemCourseRes tr_ItemCourseRes)
		{
            //修改前信息
            Tr_ItemCourseRes originalEntity=GetById(tr_ItemCourseRes.ItemCourseResID);
			DAL.Save(tr_ItemCourseRes);
            BizLogHelper.UpdateOperate(originalEntity,tr_ItemCourseRes);
		}
    
		/// <summary>
		/// 根据标识获取对象
		/// </summary>
		public Tr_ItemCourseRes GetById(Guid itemCourseResID)
		{
			Tr_ItemCourseRes tr_ItemCourseRes = DAL.GetById(itemCourseResID);
			if (tr_ItemCourseRes == null)
			{
				throw new ETMS.AppContext.BusinessException("ItemCourse.Tr_ItemCourseRes.NotFoundException",new object[]{itemCourseResID});
			}
			
			return tr_ItemCourseRes;
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

