//==================================================================================================
//Version 1.0, auto-generated.
//Generated By running1218
//Date: 2012-3-29 22:16:00.
//==================================================================================================
//==================================================================================================
//This file is generated by CodeGenerator(Ver 2.1)
//==================================================================================================

using System;
using System.Collections.Generic;
using System.Transactions;
using System.Data;
using ETMS.Utility.Logging;
using ETMS.Components.Basic.API.Entity.ELearningMap;
using ETMS.Components.Basic.Implement.DAL.ELearningMap;

namespace ETMS.Components.Basic.Implement.BLL.ELearningMap
{
    /// <summary>
    /// 学习地图表业务逻辑
    /// </summary>
    public partial class Res_StudyMapLogic
	{
		private static readonly Res_StudyMapDataAccess DAL = new Res_StudyMapDataAccess();
		
		/// <summary>
		/// 增加
		/// </summary>
		public void Add(Res_StudyMap res_StudyMap)
		{
			DAL.Add(res_StudyMap);
            BizLogHelper.AddOperate(res_StudyMap);
		}

		/// <summary>
		/// 删除
		/// </summary>
		public void Remove(Guid studyMapID)
		{
			DAL.Remove(studyMapID);
		}
		
		/// <summary>
		/// 删除
		/// </summary>
		public void Remove(Res_StudyMap res_StudyMap)
		{
			Remove(res_StudyMap.StudyMapID);
            BizLogHelper.DeleteOperate(res_StudyMap);
		}
		
		/// <summary>
		/// 批量删除
		/// </summary>
		public void Remove(List<Res_StudyMap> res_StudyMaps)
		{
			using (TransactionScope ts = new TransactionScope())
			{
				foreach (Res_StudyMap res_StudyMap in res_StudyMaps)
				{
					Remove(res_StudyMap);
				}
				
				ts.Complete();
			}
		}
    
		/// <summary>
		/// 保存
		/// </summary>
		public void Save(Res_StudyMap res_StudyMap)
		{
            //修改前信息
            Res_StudyMap originalEntity=GetById(res_StudyMap.StudyMapID);
			DAL.Save(res_StudyMap);
            BizLogHelper.UpdateOperate(originalEntity,res_StudyMap);
		}
    
		/// <summary>
		/// 根据标识获取对象
		/// </summary>
		public Res_StudyMap GetById(Guid studyMapID)
		{
			Res_StudyMap res_StudyMap = DAL.GetById(studyMapID);
			if (res_StudyMap == null)
			{
				throw new ETMS.AppContext.BusinessException("ELearningMap.Res_StudyMap.NotFoundException",new object[]{studyMapID});
			}
			
			return res_StudyMap;
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

