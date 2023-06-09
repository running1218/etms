
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
    /// 公告级别（系统字典表）业务逻辑
    /// </summary>
    public partial class Inf_dic_InfoLevelLogic
	{
		private static readonly Inf_dic_InfoLevelDataAccess DAL = new Inf_dic_InfoLevelDataAccess();
		
		/// <summary>
		/// 增加
		/// </summary>
		public void Add(Inf_dic_InfoLevel inf_dic_InfoLevel)
		{
			DAL.Add(inf_dic_InfoLevel);
            BizLogHelper.AddOperate(inf_dic_InfoLevel);
		}

		/// <summary>
		/// 删除
		/// </summary>
		public void Remove(Int32 infoLevelID)
		{
			DAL.Remove(infoLevelID);
		}
		
		/// <summary>
		/// 删除
		/// </summary>
		public void Remove(Inf_dic_InfoLevel inf_dic_InfoLevel)
		{
			Remove(inf_dic_InfoLevel.InfoLevelID);
            BizLogHelper.DeleteOperate(inf_dic_InfoLevel);
		}
		
		/// <summary>
		/// 批量删除
		/// </summary>
		public void Remove(List<Inf_dic_InfoLevel> inf_dic_InfoLevels)
		{
			using (TransactionScope ts = new TransactionScope())
			{
				foreach (Inf_dic_InfoLevel inf_dic_InfoLevel in inf_dic_InfoLevels)
				{
					Remove(inf_dic_InfoLevel);
				}
				
				ts.Complete();
			}
		}
    
		/// <summary>
		/// 保存
		/// </summary>
		public void Save(Inf_dic_InfoLevel inf_dic_InfoLevel)
		{
            //修改前信息
            Inf_dic_InfoLevel originalEntity=GetById(inf_dic_InfoLevel.InfoLevelID);
			DAL.Save(inf_dic_InfoLevel);
            BizLogHelper.UpdateOperate(originalEntity,inf_dic_InfoLevel);
		}
    
		/// <summary>
		/// 根据标识获取对象
		/// </summary>
		public Inf_dic_InfoLevel GetById(Int32 infoLevelID)
		{
			Inf_dic_InfoLevel inf_dic_InfoLevel = DAL.GetById(infoLevelID);
			if (inf_dic_InfoLevel == null)
			{
				throw new ETMS.AppContext.BusinessException("Bulletin.Inf_dic_InfoLevel.NotFoundException",new object[]{infoLevelID});
			}
			
			return inf_dic_InfoLevel;
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

