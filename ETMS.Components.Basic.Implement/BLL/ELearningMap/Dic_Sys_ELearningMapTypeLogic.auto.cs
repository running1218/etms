//==================================================================================================
//Version 1.0, auto-generated.
//Generated By xueyb.
//Date: 2012-3-29 22:16:00.
//==================================================================================================
//==================================================================================================
//This file is generated by CodeGenerator(Ver 2.1), a product of ZhouYonghua(Zhou_Yonghua@163.com).
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
    /// ѧϰ��ͼ���ͣ�ϵͳ�ֵ����ҵ���߼�
    /// </summary>
    public partial class Dic_Sys_ELearningMapTypeLogic
	{
		private static readonly Dic_Sys_ELearningMapTypeDataAccess DAL = new Dic_Sys_ELearningMapTypeDataAccess();
		
		/// <summary>
		/// ����
		/// </summary>
		public void Add(Dic_Sys_ELearningMapType dic_Sys_ELearningMapType)
		{
			DAL.Add(dic_Sys_ELearningMapType);
            BizLogHelper.AddOperate(dic_Sys_ELearningMapType);
		}

		/// <summary>
		/// ɾ��
		/// </summary>
		public void Remove(Int32 eLearningMapTypeID)
		{
			DAL.Remove(eLearningMapTypeID);
		}
		
		/// <summary>
		/// ɾ��
		/// </summary>
		public void Remove(Dic_Sys_ELearningMapType dic_Sys_ELearningMapType)
		{
			Remove(dic_Sys_ELearningMapType.ELearningMapTypeID);
            BizLogHelper.DeleteOperate(dic_Sys_ELearningMapType);
		}
		
		/// <summary>
		/// ����ɾ��
		/// </summary>
		public void Remove(List<Dic_Sys_ELearningMapType> dic_Sys_ELearningMapTypes)
		{
			using (TransactionScope ts = new TransactionScope())
			{
				foreach (Dic_Sys_ELearningMapType dic_Sys_ELearningMapType in dic_Sys_ELearningMapTypes)
				{
					Remove(dic_Sys_ELearningMapType);
				}
				
				ts.Complete();
			}
		}
    
		/// <summary>
		/// ����
		/// </summary>
		public void Save(Dic_Sys_ELearningMapType dic_Sys_ELearningMapType)
		{
            //�޸�ǰ��Ϣ
            Dic_Sys_ELearningMapType originalEntity=GetById(dic_Sys_ELearningMapType.ELearningMapTypeID);
			DAL.Save(dic_Sys_ELearningMapType);
            BizLogHelper.UpdateOperate(originalEntity,dic_Sys_ELearningMapType);
		}
    
		/// <summary>
		/// ���ݱ�ʶ��ȡ����
		/// </summary>
		public Dic_Sys_ELearningMapType GetById(Int32 eLearningMapTypeID)
		{
			Dic_Sys_ELearningMapType dic_Sys_ELearningMapType = DAL.GetById(eLearningMapTypeID);
			if (dic_Sys_ELearningMapType == null)
			{
				throw new ETMS.AppContext.BusinessException("ELearningMap.Dic_Sys_ELearningMapType.NotFoundException",new object[]{eLearningMapTypeID});
			}
			
			return dic_Sys_ELearningMapType;
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
