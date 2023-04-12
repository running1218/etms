//==================================================================================================
//Version 1.0, auto-generated.
//Generated By xueyb.
//Date: 2012-04-18 22:30:52.
//==================================================================================================
//==================================================================================================
//This file is generated by CodeGenerator(Ver 2.1), a product of ZhouYonghua(Zhou_Yonghua@163.com).
//==================================================================================================

using System;
using ETMS.Utility;
using ETMS.Utility.Logging;
using ETMS.Components.StudyClass.API.Entity.StudyClass;
namespace ETMS.Components.StudyClass.Implement.BLL.StudyClass
{
    /// <summary>
    /// ��ί��ҵ���߼�
    /// </summary>
    public partial class Sty_ClassMonitorLogic
	{
 		/// <summary>
		/// �������
		/// </summary>
		public void Save(Sty_ClassMonitor sty_ClassMonitor)
		{
            try
            {
			    if(sty_ClassMonitor.ClassMonitorID.IsEmpty())
                {
                    //��������ID��������ΪGUID��Ч��Int���������ݿ�����������
                    sty_ClassMonitor.ClassMonitorID=sty_ClassMonitor.ClassMonitorID.NewID();;
                    Add(sty_ClassMonitor);
                }
                else
                {
                    Update(sty_ClassMonitor);
                }
            }
           catch (System.Data.SqlClient.SqlException ex)
            {
                //ö������Լ���쳣��ת��Ϊҵ���쳣
                if (ex.Message.IndexOf("Index_U_Sty_ClassMonitorCode",StringComparison.InvariantCultureIgnoreCase) != -1)
                {
				    throw new ETMS.AppContext.BusinessException("StudyClass.Sty_ClassMonitor.CodeExists");
                }
                else if (ex.Message.IndexOf("Index_U_Sty_ClassMonitorName",StringComparison.InvariantCultureIgnoreCase) != -1)
                {
				    throw new ETMS.AppContext.BusinessException("StudyClass.Sty_ClassMonitor.NameExists");
                }
                //�����δ���������׳�
                throw ex;
            } 
		} 

        /// <summary>
		/// ɾ��
		/// </summary>
		protected void doRemove(Guid classMonitorID)
		{
            try
            {
			     DAL.Remove(classMonitorID);
                 //��¼ɾ����־������IDɾ����
                 BizLogHelper.Operate(classMonitorID,"ɾ��");
            }
           catch (System.Data.SqlClient.SqlException ex)
            {
                //ö������Լ���쳣��ת��Ϊҵ���쳣�������Ѿ�ʹ��
                if (ex.Message.IndexOf("FK_",StringComparison.InvariantCultureIgnoreCase) != -1)
                {
				    throw new ETMS.AppContext.BusinessException("StudyClass.Sty_ClassMonitor.DataUsed");
                } 
                //�����δ���������׳�
                throw ex;
            }  
		}

        /// <summary>
        /// ���ݰ༶ѧԱɾ���༶ְ��
        /// </summary>
        /// <param name="classStudentID"></param>
        public void DeleteByClassStudentID(Guid classStudentID)
        {
            try
            { 
                DAL.RemoveStudentPositions(classStudentID);
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                //ö������Լ���쳣��ת��Ϊҵ���쳣�������Ѿ�ʹ��
                if (ex.Message.IndexOf("FK_", StringComparison.InvariantCultureIgnoreCase) != -1)
                {
                    throw new ETMS.AppContext.BusinessException("StudyClass.Sty_ClassMonitor.DataUsed");
                }
                //�����δ���������׳�
                throw ex;
            }  
        }
	}	
}
