
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
    /// 项目课程导学资料表业务逻辑
    /// </summary>
    public partial class Tr_ItemCourseMentorDataLogic
	{
		private static readonly Tr_ItemCourseMentorDataDataAccess DAL = new Tr_ItemCourseMentorDataDataAccess();
		
		/// <summary>
		/// 增加
		/// </summary>
		public void Add(Tr_ItemCourseMentorData tr_ItemCourseMentorData)
		{
			DAL.Add(tr_ItemCourseMentorData);
            BizLogHelper.AddOperate(tr_ItemCourseMentorData);
		}

		/// <summary>
		/// 删除
		/// </summary>
		public void Remove(Guid itemCourseMentorDataID)
		{
			DAL.Remove(itemCourseMentorDataID);
		}
		
		/// <summary>
		/// 删除
		/// </summary>
		public void Remove(Tr_ItemCourseMentorData tr_ItemCourseMentorData)
		{
			Remove(tr_ItemCourseMentorData.ItemCourseMentorDataID);
            BizLogHelper.DeleteOperate(tr_ItemCourseMentorData);
		}
		
		/// <summary>
		/// 批量删除
		/// </summary>
		public void Remove(List<Tr_ItemCourseMentorData> tr_ItemCourseMentorDatas)
		{
			using (TransactionScope ts = new TransactionScope())
			{
				foreach (Tr_ItemCourseMentorData tr_ItemCourseMentorData in tr_ItemCourseMentorDatas)
				{
					Remove(tr_ItemCourseMentorData);
				}
				
				ts.Complete();
			}
		}
    
		/// <summary>
		/// 保存
		/// </summary>
		public void Save(Tr_ItemCourseMentorData tr_ItemCourseMentorData)
		{
            //修改前信息
            Tr_ItemCourseMentorData originalEntity=GetById(tr_ItemCourseMentorData.ItemCourseMentorDataID);
			DAL.Save(tr_ItemCourseMentorData);
            BizLogHelper.UpdateOperate(originalEntity,tr_ItemCourseMentorData);
		}
    
		/// <summary>
		/// 根据标识获取对象
		/// </summary>
		public Tr_ItemCourseMentorData GetById(Guid itemCourseMentorDataID)
		{
			Tr_ItemCourseMentorData tr_ItemCourseMentorData = DAL.GetById(itemCourseMentorDataID);
			if (tr_ItemCourseMentorData == null)
			{
				throw new ETMS.AppContext.BusinessException("Bulletin.Tr_ItemCourseMentorData.NotFoundException",new object[]{itemCourseMentorDataID});
			}
			
			return tr_ItemCourseMentorData;
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

