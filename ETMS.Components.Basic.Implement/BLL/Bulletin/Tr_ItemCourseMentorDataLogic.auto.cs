
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
    /// ��Ŀ�γ̵�ѧ���ϱ�ҵ���߼�
    /// </summary>
    public partial class Tr_ItemCourseMentorDataLogic
	{
		private static readonly Tr_ItemCourseMentorDataDataAccess DAL = new Tr_ItemCourseMentorDataDataAccess();
		
		/// <summary>
		/// ����
		/// </summary>
		public void Add(Tr_ItemCourseMentorData tr_ItemCourseMentorData)
		{
			DAL.Add(tr_ItemCourseMentorData);
            BizLogHelper.AddOperate(tr_ItemCourseMentorData);
		}

		/// <summary>
		/// ɾ��
		/// </summary>
		public void Remove(Guid itemCourseMentorDataID)
		{
			DAL.Remove(itemCourseMentorDataID);
		}
		
		/// <summary>
		/// ɾ��
		/// </summary>
		public void Remove(Tr_ItemCourseMentorData tr_ItemCourseMentorData)
		{
			Remove(tr_ItemCourseMentorData.ItemCourseMentorDataID);
            BizLogHelper.DeleteOperate(tr_ItemCourseMentorData);
		}
		
		/// <summary>
		/// ����ɾ��
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
		/// ����
		/// </summary>
		public void Save(Tr_ItemCourseMentorData tr_ItemCourseMentorData)
		{
            //�޸�ǰ��Ϣ
            Tr_ItemCourseMentorData originalEntity=GetById(tr_ItemCourseMentorData.ItemCourseMentorDataID);
			DAL.Save(tr_ItemCourseMentorData);
            BizLogHelper.UpdateOperate(originalEntity,tr_ItemCourseMentorData);
		}
    
		/// <summary>
		/// ���ݱ�ʶ��ȡ����
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

