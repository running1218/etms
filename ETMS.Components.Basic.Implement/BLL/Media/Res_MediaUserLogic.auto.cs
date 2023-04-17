//==================================================================================================
//Version 1.0, auto-generated.
//Generated By running1218
//Date: 2014/12/12 16:31:46.
//==================================================================================================
//==================================================================================================
//This file is generated by CodeGenerator(Ver 2.1)
//==================================================================================================

using System;
using System.Collections.Generic;
using System.Data;
using ETMS.Utility.Logging;
using ETMS.Components.Basic.API.Entity;
using ETMS.Components.Basic.Implement.DAL;
namespace ETMS.Components.Basic.Implement.BLL
{
    /// <summary>
    /// 业务逻辑
    /// </summary>
    public partial class Res_MediaUserLogic
	{
		private static readonly Res_MediaUserDataAccess DAL = new Res_MediaUserDataAccess();
		
		/// <summary>
		/// 增加
		/// </summary>
		public void Add(Res_MediaUser res_MediaUser)
		{
			DAL.Add(res_MediaUser);
            BizLogHelper.AddOperate(res_MediaUser);
		}


		/// <summary>
		/// 保存
		/// </summary>
		public void Update(Res_MediaUser res_MediaUser)
		{
            //修改前信息
            Res_MediaUser originalEntity=GetById(res_MediaUser.MediaUserID);
			DAL.Save(res_MediaUser);
            BizLogHelper.UpdateOperate(originalEntity,res_MediaUser);
		}

		/// <summary>
		/// 删除
		/// </summary>
		public void Remove(Guid mediaUserID)
		{
            doRemove(mediaUserID);
		} 

		/// <summary>
		/// 批量删除(主键ID数组）
		/// </summary>
		public void Remove(Guid[] mediaUserIDs)
		{
#if !DEBUG
			using (TransactionScope ts = new TransactionScope())
			{
#endif
				foreach (Guid id in mediaUserIDs  )
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
		public Res_MediaUser GetById(Guid mediaUserID)
		{
			Res_MediaUser res_MediaUser = DAL.GetById(mediaUserID);
			if (res_MediaUser == null)
			{
				throw new ETMS.AppContext.BusinessException("Media.Res_MediaUser.NotFoundException",new object[]{mediaUserID});
			}
			
			return res_MediaUser;
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
		public IList<Res_MediaUser> GetEntityList(int pageIndex, int pageSize, string sortExpression, string criteria,out int totalRecords)
		{				
			return DAL.GetEntityList(pageIndex, pageSize, sortExpression, criteria, out totalRecords);
		}
		
	}
	
	
}

