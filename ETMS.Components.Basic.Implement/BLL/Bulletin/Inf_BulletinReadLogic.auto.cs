
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
    /// �����Ѷ���¼��ҵ���߼�
    /// </summary>
    public partial class Inf_BulletinReadLogic
	{
		private static readonly Inf_BulletinReadDataAccess DAL = new Inf_BulletinReadDataAccess();
		
		/// <summary>
		/// ����
		/// </summary>
		public void Add(Inf_BulletinRead inf_BulletinRead)
		{
			DAL.Add(inf_BulletinRead);
            BizLogHelper.AddOperate(inf_BulletinRead);
		}

		/// <summary>
		/// ɾ��
		/// </summary>
		public void Remove(Int32 articleClickID)
		{
			DAL.Remove(articleClickID);
		}
		
		/// <summary>
		/// ɾ��
		/// </summary>
		public void Remove(Inf_BulletinRead inf_BulletinRead)
		{
			Remove(inf_BulletinRead.ArticleClickID);
            BizLogHelper.DeleteOperate(inf_BulletinRead);
		}
		
		/// <summary>
		/// ����ɾ��
		/// </summary>
		public void Remove(List<Inf_BulletinRead> inf_BulletinReads)
		{
			using (TransactionScope ts = new TransactionScope())
			{
				foreach (Inf_BulletinRead inf_BulletinRead in inf_BulletinReads)
				{
					Remove(inf_BulletinRead);
				}
				
				ts.Complete();
			}
		}
    
		/// <summary>
		/// ����
		/// </summary>
		public void Save(Inf_BulletinRead inf_BulletinRead)
		{
            //�޸�ǰ��Ϣ
            Inf_BulletinRead originalEntity=GetById(inf_BulletinRead.ArticleClickID);
			DAL.Save(inf_BulletinRead);
            BizLogHelper.UpdateOperate(originalEntity,inf_BulletinRead);
		}
    
		/// <summary>
		/// ���ݱ�ʶ��ȡ����
		/// </summary>
		public Inf_BulletinRead GetById(Int32 articleClickID)
		{
			Inf_BulletinRead inf_BulletinRead = DAL.GetById(articleClickID);
			if (inf_BulletinRead == null)
			{
				throw new ETMS.AppContext.BusinessException("Bulletin.Inf_BulletinRead.NotFoundException",new object[]{articleClickID});
			}
			
			return inf_BulletinRead;
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

        public int GetReadNum(int articlID)
        {
            return DAL.GetReadNum(articlID);
        }
	}
	
	
}

