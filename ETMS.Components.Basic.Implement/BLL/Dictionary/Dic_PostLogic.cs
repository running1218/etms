//==================================================================================================
//Version 1.0, auto-generated.
//Generated By xueyb.
//Date: 2012-4-1 9:30:29.
//==================================================================================================
//==================================================================================================
//This file is generated by CodeGenerator(Ver 2.1), a product of ZhouYonghua(Zhou_Yonghua@163.com).
//==================================================================================================

using System;
using ETMS.Utility;
using ETMS.Utility.Logging;
using ETMS.Components.Basic.API.Entity.Dictionary;
namespace ETMS.Components.Basic.Implement.BLL.Dictionary
{
    /// <summary>
    /// ҵ���߼�
    /// </summary>
    public partial class Dic_PostLogic
	{

        /// <summary>
        /// ��ȡ��ǰ�����¿��ø�λ
        /// </summary>
        /// <param name="organizationID"></param>
        /// <returns></returns>
        public System.Data.DataTable GetAllEnablePostByOrgID(int organizationID)
        {
            string filter = string.Format(" AND OrganizationID={0}", organizationID);
            return DAL.QueryDataList(filter);
        }
        /// <summary>
        /// �������
        /// </summary>
        public void Save(Dic_Post dic_Post)
        {
            try
            {
                if (dic_Post.PostID.IsEmpty())
                {
                    //��������ID��������ΪGUID��Ч��Int���������ݿ�����������
                    dic_Post.PostID = dic_Post.PostID.NewID(); ;
                    Add(dic_Post);
                }
                else
                {
                    Update(dic_Post);
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                //ö������Լ���쳣��ת��Ϊҵ���쳣
                if (ex.Message.IndexOf("IX_Dic_Post_U_Org_Code", StringComparison.InvariantCultureIgnoreCase) != -1)
                {
                    throw new ETMS.AppContext.BusinessException("Dictionary.Dic_Post.CodeExists");
                }
                else if (ex.Message.IndexOf("IX_Dic_Post_U_Org_Name", StringComparison.InvariantCultureIgnoreCase) != -1)
                {
                    throw new ETMS.AppContext.BusinessException("Dictionary.Dic_Post.NameExists");
                }
                //�����δ���������׳�
                throw ex;
            }
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        protected void doRemove(Int32 postID)
        {
            try
            {
                DAL.Remove(postID);
                //��¼ɾ����־������IDɾ����
                BizLogHelper.Operate(postID, "ɾ��");
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                //ö������Լ���쳣��ת��Ϊҵ���쳣�������Ѿ�ʹ��
                if (ex.Message.IndexOf("FK_", StringComparison.InvariantCultureIgnoreCase) != -1)
                {
                    throw new ETMS.AppContext.BusinessException("Dictionary.Dic_Post.IsUsing");
                }
                //�����δ���������׳�
                throw ex;
            }
        }  
	}
	
	
}
