//==================================================================================================
//Version 1.0, auto-generated.
//Generated By xueyb.
//Date: 2012-4-17 15:53:39.
//==================================================================================================
//==================================================================================================
//This file is generated by CodeGenerator(Ver 2.1), a product of ZhouYonghua(Zhou_Yonghua@163.com).
//==================================================================================================

using System;
using System.Collections.Generic;
using System.Data;
using ETMS.Utility.Logging;
using ETMS.Components.Poll.API.Entity;
using ETMS.Components.Poll.Implement.DAL;
namespace ETMS.Components.Poll.Implement.BLL
{
    /// <summary>
    /// ��������ҵ���߼�
    /// </summary>
    public partial class Poll_QueryResultLogic
	{
		private static readonly Poll_QueryResultDataAccess DAL = new Poll_QueryResultDataAccess();
		
		/// <summary>
		/// ����
		/// </summary>
		public void Add(Poll_QueryResult poll_QueryResult)
		{
			DAL.Add(poll_QueryResult);
            BizLogHelper.AddOperate(poll_QueryResult);
		}


		/// <summary>
		/// ����
		/// </summary>
		public void Update(Poll_QueryResult poll_QueryResult)
		{
            //�޸�ǰ��Ϣ
            Poll_QueryResult originalEntity=GetById(poll_QueryResult.QueryResultID);
			DAL.Save(poll_QueryResult);
            BizLogHelper.UpdateOperate(originalEntity,poll_QueryResult);
		}

		/// <summary>
		/// ɾ��
		/// </summary>
		public void Remove(Int32 queryResultID)
		{
            doRemove(queryResultID);
		} 

		/// <summary>
		/// ����ɾ��(����ID���飩
		/// </summary>
		public void Remove(Int32[] queryResultIDs)
		{
#if !DEBUG
			using (TransactionScope ts = new TransactionScope())
			{
#endif
				foreach (Int32 id in queryResultIDs  )
				{
					Remove(id);
				}
#if !DEBUG
				ts.Complete();
			}
#endif
		} 
    
    
		/// <summary>
		/// ���ݱ�ʶ��ȡ����
		/// </summary>
		public Poll_QueryResult GetById(Int32 queryResultID)
		{
			Poll_QueryResult poll_QueryResult = DAL.GetById(queryResultID);
			if (poll_QueryResult == null)
			{
				throw new ETMS.AppContext.BusinessException(".Poll_QueryResult.NotFoundException",new object[]{queryResultID});
			}
			
			return poll_QueryResult;
		}		
		 
		/// <summary>
        /// ��ѯ�����б���ҳ����
        /// </summary>
        /// <param name="pageIndex">ҳ��</param>
        /// <param name="pageSize">ҳ���С</param>
        /// <param name="sortExpression">��������</param>
        /// <param name="criteria">ɸѡ����</param>
        /// <param name="totalRecords">out ��¼����</param>
        /// <returns>���ز�ѯ���</returns>
		public DataTable GetPagedList(int pageIndex, int pageSize, string sortExpression, string criteria,out int totalRecords)
		{				
			return DAL.GetPagedList(pageIndex, pageSize, sortExpression, criteria, out totalRecords);
		}
				
        /// <summary>
        /// ��ѯʵ���ҳ����
        /// </summary>
        /// <param name="pageIndex">ҳ��</param>
        /// <param name="pageSize">ҳ���С</param>
        /// <param name="sortExpression">��������</param>
        /// <param name="criteria">ɸѡ����</param>
        /// <param name="totalRecords">out ��¼����</param>
        /// <returns>���ز�ѯ���</returns>
		public IList<Poll_QueryResult> GetEntityList(int pageIndex, int pageSize, string sortExpression, string criteria,out int totalRecords)
		{				
			return DAL.GetEntityList(pageIndex, pageSize, sortExpression, criteria, out totalRecords);
		}
		
	}
	
	
}
