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
    /// ѧϰ��ͼ��γ̹�ϵ��ҵ���߼�
    /// </summary>
    public partial class Res_StudyMapReferCourseLogic
	{
		private static readonly Res_StudyMapReferCourseDataAccess DAL = new Res_StudyMapReferCourseDataAccess();
		
		/// <summary>
		/// ����
		/// </summary>
		public void Add(Res_StudyMapReferCourse res_StudyMapReferCourse)
		{
			DAL.Add(res_StudyMapReferCourse);
            BizLogHelper.AddOperate(res_StudyMapReferCourse);
		}

		/// <summary>
		/// ɾ��
		/// </summary>
		public void Remove(Guid studyMapReferCourseID)
		{
			DAL.Remove(studyMapReferCourseID);
		}
		
		/// <summary>
		/// ɾ��
		/// </summary>
		public void Remove(Res_StudyMapReferCourse res_StudyMapReferCourse)
		{
			Remove(res_StudyMapReferCourse.StudyMapReferCourseID);
            BizLogHelper.DeleteOperate(res_StudyMapReferCourse);
		}
		
		/// <summary>
		/// ����ɾ��
		/// </summary>
		public void Remove(List<Res_StudyMapReferCourse> res_StudyMapReferCourses)
		{
			using (TransactionScope ts = new TransactionScope())
			{
				foreach (Res_StudyMapReferCourse res_StudyMapReferCourse in res_StudyMapReferCourses)
				{
					Remove(res_StudyMapReferCourse);
				}
				
				ts.Complete();
			}
		}
    
		/// <summary>
		/// ����
		/// </summary>
		public void Save(Res_StudyMapReferCourse res_StudyMapReferCourse)
		{
            //�޸�ǰ��Ϣ
            Res_StudyMapReferCourse originalEntity=GetById(res_StudyMapReferCourse.StudyMapReferCourseID);
			DAL.Save(res_StudyMapReferCourse);
            BizLogHelper.UpdateOperate(originalEntity,res_StudyMapReferCourse);
		}
    
		/// <summary>
		/// ���ݱ�ʶ��ȡ����
		/// </summary>
		public Res_StudyMapReferCourse GetById(Guid studyMapReferCourseID)
		{
			Res_StudyMapReferCourse res_StudyMapReferCourse = DAL.GetById(studyMapReferCourseID);
			if (res_StudyMapReferCourse == null)
			{
				throw new ETMS.AppContext.BusinessException("ELearningMap.Res_StudyMapReferCourse.NotFoundException",new object[]{studyMapReferCourseID});
			}
			
			return res_StudyMapReferCourse;
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
