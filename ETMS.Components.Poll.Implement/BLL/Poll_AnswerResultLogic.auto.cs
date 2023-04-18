
using System;
using System.Collections.Generic;
using System.Data;
using ETMS.Utility.Logging;
using ETMS.Components.Poll.API.Entity;
using ETMS.Components.Poll.Implement.DAL;
using System.Transactions;
namespace ETMS.Components.Poll.Implement.BLL
{
    /// <summary>
    /// ����𰸱�ҵ���߼�
    /// </summary>
    public partial class Poll_AnswerResultLogic
    {
        private static readonly Poll_AnswerResultDataAccess DAL = new Poll_AnswerResultDataAccess();

        /// <summary>
        /// ����
        /// </summary>
        public void Add(Poll_AnswerResult poll_AnswerResult)
        {
            DAL.Add(poll_AnswerResult);
            BizLogHelper.AddOperate(poll_AnswerResult);
        }


        /// <summary>
        /// ����
        /// </summary>
        public void Update(Poll_AnswerResult poll_AnswerResult)
        {
            //�޸�ǰ��Ϣ
            Poll_AnswerResult originalEntity = GetById(poll_AnswerResult.AnswerResultID);
            DAL.Save(poll_AnswerResult);
            BizLogHelper.UpdateOperate(originalEntity, poll_AnswerResult);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        public void Remove(Int32 answerResultID)
        {
            doRemove(answerResultID);
        }

        /// <summary>
        /// ����ɾ��(����ID���飩
        /// </summary>
        public void Remove(Int32[] answerResultIDs)
        {
#if !DEBUG
			using (TransactionScope ts = new TransactionScope())
			{
#endif
            foreach (Int32 id in answerResultIDs)
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
        public Poll_AnswerResult GetById(Int32 answerResultID)
        {
            Poll_AnswerResult poll_AnswerResult = DAL.GetById(answerResultID);
            if (poll_AnswerResult == null)
            {
                throw new ETMS.AppContext.BusinessException(".Poll_AnswerResult.NotFoundException", new object[] { answerResultID });
            }

            return poll_AnswerResult;
        }

        /// <summary>
        /// ��ѯ�����б��ҳ����
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

        /// <summary>
        /// ��ѯʵ���ҳ����
        /// </summary>
        /// <param name="pageIndex">ҳ��</param>
        /// <param name="pageSize">ҳ���С</param>
        /// <param name="sortExpression">��������</param>
        /// <param name="criteria">ɸѡ����</param>
        /// <param name="totalRecords">out ��¼����</param>
        /// <returns>���ز�ѯ���</returns>
        public IList<Poll_AnswerResult> GetEntityList(int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {
            return DAL.GetEntityList(pageIndex, pageSize, sortExpression, criteria, out totalRecords);
        }

    }


}

