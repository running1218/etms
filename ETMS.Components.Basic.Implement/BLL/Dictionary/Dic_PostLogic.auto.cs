
using System;
using System.Data;

using ETMS.AppContext;
using ETMS.Utility.Logging;
using ETMS.Components.Basic.API.Entity.Dictionary;
using ETMS.Components.Basic.Implement.DAL.Dictionary;
using System.Transactions;
namespace ETMS.Components.Basic.Implement.BLL.Dictionary
{
    /// <summary>
    /// ҵ���߼�
    /// </summary>
    public partial class Dic_PostLogic
    {
        private static readonly Dic_PostDataAccess DAL = new Dic_PostDataAccess();

        /// <summary>
        /// ����
        /// </summary>
        public void Add(Dic_Post dic_Post)
        {
            try
            {
                DAL.Add(dic_Post);
                BizLogHelper.AddOperate(dic_Post);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("IX_Dic_Post_U_Org_Code"))
                {
                    throw new BusinessException(string.Format("��λ���룺��{0}���Ѿ����ڣ������ظ���", dic_Post.PostCode));
                }
                else if (ex.Message.Contains("IX_Dic_Post_U_Org_Name"))
                {
                    throw new BusinessException(string.Format("��λ���ƣ���{0}���Ѿ����ڣ������ظ���", dic_Post.PostName));
                }
            }
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        public void Remove(Int32 postID)
        {
            doRemove(postID);
        }

        /// <summary>
        /// ����ɾ��(����ID���飩
        /// </summary>
        public void Remove(Int32[] postIDs)
        {
#if !DEBUG
			using (TransactionScope ts = new TransactionScope())
			{
#endif
            foreach (Int32 id in postIDs)
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
        public void Update(Dic_Post dic_Post)
        {
            //�޸�ǰ��Ϣ
            Dic_Post originalEntity = GetById(dic_Post.PostID);
            if (originalEntity.Status == 1 && dic_Post.Status == 0)//����״̬
            {
                //����λ�Ƿ���ѧԱʹ�ã�����У����ֹͣ��
                Security.Site_StudentLogic studentLogic = new Security.Site_StudentLogic();
                int totalRecords = 0;
                studentLogic.GetManagePagedListAdv(1, 0, "", string.Format(" AND PostID={0}", dic_Post.PostID), out totalRecords);
                if (totalRecords > 0)
                {
                    throw new ETMS.AppContext.BusinessException("Dictionary.Dic_Post.DisableFaild");
                }
            }
            DAL.Save(dic_Post);
            BizLogHelper.UpdateOperate(originalEntity, dic_Post);
        }

        /// <summary>
        /// ���ݱ�ʶ��ȡ����
        /// </summary>
        public Dic_Post GetById(Int32? postID)
        {
            Dic_Post dic_Post = DAL.GetById(postID);            
            return dic_Post;
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

