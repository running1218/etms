//==================================================================================================
//Version 1.0, auto-generated.
//Generated By xinyb.
//Date: 2012-05-02 11:00:37.
//==================================================================================================
//==================================================================================================
//This file is generated by CodeGenerator(Ver 2.1), a product of ZhouYonghua(Zhou_Yonghua@163.com).
//==================================================================================================

using System;
using System.Collections.Generic;
using System.Data;
using ETMS.Utility.Logging;
using ETMS.Components.Mentor.API.Entity.Mentor;
using ETMS.Components.Mentor.Implement.DAL.Mentor;
namespace ETMS.Components.Mentor.Implement.BLL.Mentor
{
    /// <summary>
    /// ��ʦ��ҵ���߼�
    /// </summary>
    public partial class Site_MentorLogic
	{
		private static readonly Site_MentorDataAccess DAL = new Site_MentorDataAccess();
		
		/// <summary>
		/// ����
		/// </summary>
		public void Add(Site_Mentor site_Mentor)
		{
			DAL.Add(site_Mentor);
            BizLogHelper.AddOperate(site_Mentor);
		}


		/// <summary>
		/// ����
		/// </summary>
		public void Update(Site_Mentor site_Mentor)
		{
            //�޸�ǰ��Ϣ
            Site_Mentor originalEntity=GetById(site_Mentor.MentorID);
			DAL.Save(site_Mentor);
            BizLogHelper.UpdateOperate(originalEntity,site_Mentor);
		}

		/// <summary>
		/// ɾ��
		/// </summary>
		public void Remove(Int32 mentorID)
		{
            doRemove(mentorID);
		} 

		/// <summary>
		/// ����ɾ��(����ID���飩
		/// </summary>
		public void Remove(Int32[] mentorIDs)
		{
#if !DEBUG
			using (TransactionScope ts = new TransactionScope())
			{
#endif
				foreach (Int32 id in mentorIDs  )
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
		public Site_Mentor GetById(Int32 mentorID)
		{
			Site_Mentor site_Mentor = DAL.GetById(mentorID);
			if (site_Mentor == null)
			{
				throw new ETMS.AppContext.BusinessException("Mentor.Site_Mentor.NotFoundException",new object[]{mentorID});
			}
			
			return site_Mentor;
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
		public IList<Site_Mentor> GetEntityList(int pageIndex, int pageSize, string sortExpression, string criteria,out int totalRecords)
		{				
			return DAL.GetEntityList(pageIndex, pageSize, sortExpression, criteria, out totalRecords);
		}
		
	}
	
	
}
