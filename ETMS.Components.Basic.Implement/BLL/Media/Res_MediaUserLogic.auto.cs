//==================================================================================================
//Version 1.0, auto-generated.
//Generated By xueyb.
//Date: 2014/12/12 16:31:46.
//==================================================================================================
//==================================================================================================
//This file is generated by CodeGenerator(Ver 2.1), a product of ZhouYonghua(Zhou_Yonghua@163.com).
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
    /// ҵ���߼�
    /// </summary>
    public partial class Res_MediaUserLogic
	{
		private static readonly Res_MediaUserDataAccess DAL = new Res_MediaUserDataAccess();
		
		/// <summary>
		/// ����
		/// </summary>
		public void Add(Res_MediaUser res_MediaUser)
		{
			DAL.Add(res_MediaUser);
            BizLogHelper.AddOperate(res_MediaUser);
		}


		/// <summary>
		/// ����
		/// </summary>
		public void Update(Res_MediaUser res_MediaUser)
		{
            //�޸�ǰ��Ϣ
            Res_MediaUser originalEntity=GetById(res_MediaUser.MediaUserID);
			DAL.Save(res_MediaUser);
            BizLogHelper.UpdateOperate(originalEntity,res_MediaUser);
		}

		/// <summary>
		/// ɾ��
		/// </summary>
		public void Remove(Guid mediaUserID)
		{
            doRemove(mediaUserID);
		} 

		/// <summary>
		/// ����ɾ��(����ID���飩
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
		/// ���ݱ�ʶ��ȡ����
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
		public IList<Res_MediaUser> GetEntityList(int pageIndex, int pageSize, string sortExpression, string criteria,out int totalRecords)
		{				
			return DAL.GetEntityList(pageIndex, pageSize, sortExpression, criteria, out totalRecords);
		}
		
	}
	
	
}
