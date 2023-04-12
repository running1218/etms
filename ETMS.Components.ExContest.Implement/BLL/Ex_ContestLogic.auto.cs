//==================================================================================================
//Version 1.0, auto-generated.
//Generated By xueyb.
//Date: 2012-4-1 16:17:20.
//==================================================================================================
//==================================================================================================
//This file is generated by CodeGenerator(Ver 2.1), a product of ZhouYonghua(Zhou_Yonghua@163.com).
//==================================================================================================

using System;
using System.Data;
using ETMS.Utility.Logging;
using ETMS.Components.ExContest.API.Entity;
using ETMS.Components.ExContest.Implement.DAL;
namespace ETMS.Components.ExContest.Implement.BLL
{
    /// <summary>
    /// ���ؾ�����ҵ���߼�
    /// </summary>
    public partial class Ex_ContestLogic
	{
		private static readonly Ex_ContestDataAccess DAL = new Ex_ContestDataAccess();
		
		/// <summary>
		/// ����
		/// </summary>
		public void Add(Ex_Contest ex_Contest)
		{
			DAL.Add(ex_Contest);
            BizLogHelper.AddOperate(ex_Contest);
		}

		/// <summary>
		/// ɾ��
		/// </summary>
		public void Remove(Guid contestID)
		{
            doRemove(contestID);
		} 

		/// <summary>
		/// ����ɾ��(����ID���飩
		/// </summary>
		public void Remove(Guid[] contestIDs)
		{
#if !DEBUG
			using (TransactionScope ts = new TransactionScope())
			{
#endif
				foreach (Guid id in contestIDs  )
				{
					Remove(id);
				}
#if !DEBUG
				ts.Complete();
			}
#endif
		} 
    
		/// <summary>
		/// ����
		/// </summary>
		public void Update(Ex_Contest ex_Contest)
		{
            //�޸�ǰ��Ϣ
            Ex_Contest originalEntity=GetById(ex_Contest.ContestID);
			DAL.Save(ex_Contest);
            BizLogHelper.UpdateOperate(originalEntity,ex_Contest);
		}
    
		/// <summary>
		/// ���ݱ�ʶ��ȡ����
		/// </summary>
		public Ex_Contest GetById(Guid contestID)
		{
			Ex_Contest ex_Contest = DAL.GetById(contestID);
			if (ex_Contest == null)
			{
				throw new ETMS.AppContext.BusinessException("ExContest.Ex_Contest.NotFoundException",new object[]{contestID});
			}
			
			return ex_Contest;
		}		
		 
		/// <summary>
        	/// ��ѯ��ҳ����
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
		
	}
	
	
}
