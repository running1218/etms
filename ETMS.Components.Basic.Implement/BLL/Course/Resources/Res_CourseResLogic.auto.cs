
using System;
using System.Data;
using ETMS.Utility.Logging;
using ETMS.Components.Basic.API.Entity.Course.Resources;
using ETMS.Components.Basic.Implement.DAL.Course.Resources;
namespace ETMS.Components.Basic.Implement.BLL.Course.Resources
{
    /// <summary>
    /// �γ���Դ��ҵ���߼�
    /// </summary>
    public partial class Res_CourseResLogic
    {
        private static readonly Res_CourseResDataAccess DAL = new Res_CourseResDataAccess();

        /// <summary>
        /// ����
        /// </summary>
        public void Add(Res_CourseRes res_CourseRes)
        {
            DAL.Add(res_CourseRes);
            BizLogHelper.AddOperate(res_CourseRes);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        public void Remove(Guid courseResID)
        {
            doRemove(courseResID);
        }

        /// <summary>
        /// ����ɾ��(����ID���飩
        /// </summary>
        public void Remove(Guid[] courseResIDs)
        {
#if !DEBUG
			using (TransactionScope ts = new TransactionScope())
			{
#endif
            foreach (Guid id in courseResIDs)
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
        public void Update(Res_CourseRes res_CourseRes)
        {
            //�޸�ǰ��Ϣ
            Res_CourseRes originalEntity = GetById(res_CourseRes.CourseResID);
            DAL.Save(res_CourseRes);
            BizLogHelper.UpdateOperate(originalEntity, res_CourseRes);
        }

        /// <summary>
        /// ���ݱ�ʶ��ȡ����
        /// </summary>
        public Res_CourseRes GetById(Guid courseResID)
        {
            Res_CourseRes res_CourseRes = DAL.GetById(courseResID);
            if (res_CourseRes == null)
            {
                throw new ETMS.AppContext.BusinessException("Course.Resources.Res_CourseRes.NotFoundException", new object[] { courseResID });
            }

            return res_CourseRes;
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

