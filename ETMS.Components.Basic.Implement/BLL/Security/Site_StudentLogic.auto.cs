//==================================================================================================
//Version 1.0, auto-generated.
//Generated By xueyb.
//Date: 2012-3-30 14:32:19.
//==================================================================================================
//==================================================================================================
//This file is generated by CodeGenerator(Ver 2.1), a product of ZhouYonghua(Zhou_Yonghua@163.com).
//==================================================================================================

using System;
using System.Data;
using ETMS.Utility.Logging;
using ETMS.Components.Basic.API.Entity.Security;
using ETMS.Components.Basic.Implement.DAL.Security;
namespace ETMS.Components.Basic.Implement.BLL.Security
{
    /// <summary>
    /// ѧԱ��Ϣ(�û���չ��)ҵ���߼�
    /// </summary>
    public partial class Site_StudentLogic
    {
        private static readonly Site_StudentDataAccess DAL = new Site_StudentDataAccess();

        /// <summary>
        /// ����
        /// </summary>
        public void Add(Site_Student site_Student)
        {
            DAL.Add(site_Student);
            BizLogHelper.AddOperate(site_Student);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        public void Remove(Int32 userID)
        {
            doRemove(userID);
        }

        /// <summary>
        /// ����ɾ��(����ID���飩
        /// </summary>
        public void Remove(Int32[] userIDs)
        {
#if !DEBUG
			using (TransactionScope ts = new TransactionScope())
			{
#endif
            foreach (Int32 id in userIDs)
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
        public void Update(Site_Student site_Student)
        {
            //�޸�ǰ��Ϣ
            Site_Student originalEntity = GetById(site_Student.UserID);
            DAL.Save(site_Student);
            BizLogHelper.UpdateOperate(originalEntity, site_Student);
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
        public DataTable GetPagedList(int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {
            return DAL.GetPagedList(pageIndex, pageSize, sortExpression, criteria, out totalRecords);
        }
    }
}
