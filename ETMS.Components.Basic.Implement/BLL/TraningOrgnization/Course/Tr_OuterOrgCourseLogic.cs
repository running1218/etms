//==================================================================================================
//Version 1.0, auto-generated.
//Generated By huangzhf.
//Date: 2012-4-24 21:06:16.
//==================================================================================================
//==================================================================================================
//This file is generated by CodeGenerator(Ver 2.1), a product of ZhouYonghua(Zhou_Yonghua@163.com).
//==================================================================================================

using System;
using ETMS.Utility;
using ETMS.Utility.Logging;
using ETMS.Components.Basic.API.Entity.TraningOrgnization.Course;
using ETMS.Components.Basic.API;
namespace ETMS.Components.Basic.Implement.BLL.TraningOrgnization.Course
{
    /// <summary>
    /// �ⲿ��ѵ�����γ̱�ҵ���߼�
    /// </summary>
    public partial class Tr_OuterOrgCourseLogic
	{
 		/// <summary>
		/// �������
		/// </summary>
		public void Save(Tr_OuterOrgCourse tr_OuterOrgCourse)
		{
            try
            {
			    if(tr_OuterOrgCourse.OuterOrgCourseID.IsEmpty())
                {
                    //��������ID��������ΪGUID��Ч��Int���������ݿ�����������
                    tr_OuterOrgCourse.OuterOrgCourseID=tr_OuterOrgCourse.OuterOrgCourseID.NewID();;
                    Add(tr_OuterOrgCourse);
                }
                else
                {
                    Update(tr_OuterOrgCourse);
                }
            }
           catch (System.Data.SqlClient.SqlException ex)
            {
                //ö������Լ���쳣��ת��Ϊҵ���쳣
                if (ex.Message.IndexOf("Index_UT_CourseCode", StringComparison.InvariantCultureIgnoreCase) != -1)
                {
                    throw new ETMS.AppContext.BusinessException(string.Format(BizErrorDefine.TraningOrgnization_Course_Tr_OuterOrgCourse_CodeExists, tr_OuterOrgCourse.CourseCode));
                }
                else if (ex.Message.IndexOf("Index_U_Tr_OuterOrgCourseName",StringComparison.InvariantCultureIgnoreCase) != -1)
                {
				    throw new ETMS.AppContext.BusinessException("TraningOrgnization.Course.Tr_OuterOrgCourse.NameExists");
                }
                //�����δ���������׳�
                throw ex;
            } 
		} 

        /// <summary>
		/// ɾ��
		/// </summary>
		public void doRemove(Guid outerOrgCourseID)
		{
            try
            {
			     DAL.Remove(outerOrgCourseID);
                 //��¼ɾ����־������IDɾ����
                 BizLogHelper.Operate(outerOrgCourseID,"ɾ��");
            }
           catch (System.Data.SqlClient.SqlException ex)
            {
                //ö������Լ���쳣��ת��Ϊҵ���쳣�������Ѿ�ʹ��
                if (ex.Message.IndexOf("FK_",StringComparison.InvariantCultureIgnoreCase) != -1)
                {
				    throw new ETMS.AppContext.BusinessException("TraningOrgnization.Course.Tr_OuterOrgCourse.DataUsed");
                } 
                //�����δ���������׳�
                throw ex;
            }  
		}  
	}	
}
