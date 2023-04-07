using ETMS.Components.NoteQuestion.API.Entity;
using ETMS.Components.NoteQuestion.Implement.DAL;
using ETMS.Utility;
using ETMS.Utility.Logging;
using System;
using System.Data;

namespace ETMS.Components.NoteQuestion.Implement.BLL
{
    public partial class QA_AnswerLogic
    {
        private QA_AnswerDataAccess DAL = new QA_AnswerDataAccess();

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="qAAnswer">业务实体</param>
        public void Insert(QA_Answer qAAnswer)
        {
            DAL.Insert(qAAnswer);
            BizLogHelper.AddOperate(qAAnswer);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="qAAnswer">业务实体</param>
        public void Update(QA_Answer qAAnswer)
        {
            QA_Answer originalEntity = GetByID(qAAnswer.AnswerID);
            originalEntity.AnswerContent = qAAnswer.AnswerContent;
            DAL.Update(originalEntity);
            BizLogHelper.UpdateOperate(originalEntity, qAAnswer);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="qAAnswer">业务实体</param>
		public void Remove(QA_Answer qAAnswer)
        {
            Remove(qAAnswer.AnswerID);
        }

        /// <summary>
        /// 根据主键删除业务实体
        /// </summary>
        /// <param name="answerID">回答ID</param>
		public void Remove(Guid answerID)
        {
            QA_Answer originalEntity = GetByID(answerID);
            DAL.Remove(answerID);
            BizLogHelper.DeleteOperate(originalEntity);
        }

        /// <summary>
        /// 根据主键获取业务实体
        /// </summary>
        /// <param name="answerID">回答ID</param>
        public QA_Answer GetByID(Guid answerID)
        {
            return DAL.GetByID(answerID).Rows[0].ToEntity<QA_Answer>();
        }

        /// <summary>
        /// 分页查询业务表
        /// </summary>
        /// <param name="pageIndex">第几页，从1开始</param>
        /// <param name="pageSize">每页记录数量</param>
        /// <param name="qAAnswer">业务实体</param>
        /// <param name="totalRecords">符合条件的记录总数</param>
        /// <returns>业务表</returns>
        public DataTable GetPagedList(int pageIndex, int pageSize, QA_Answer qAAnswer, out int totalRecords)
        {
            return DAL.GetPagedList(pageIndex, pageSize, qAAnswer, out totalRecords);
        }

        /// <summary>
        /// 分页查询业务表
        /// </summary>
        /// <param name="pageIndex">第几页，从1开始</param>
        /// <param name="pageSize">每页记录数量</param>
        /// <param name="questionIDs">问题IDs</param>
        /// <param name="totalRecords">符合条件的记录总数</param>
        /// <returns>业务表</returns>
        public DataTable GetPagedListTmpl(int pageIndex, int pageSize, string questionIDs, out int totalRecords)
        {
            return DAL.GetPagedListTmpl(pageIndex, pageSize, questionIDs, out totalRecords);
        }

        /// <summary>
        /// 获取回复列表
        /// </summary>
        /// <param name="QuestionID"></param>
        /// <param name="PageSize"></param>
        /// <param name="LastNoteID"></param>
        /// <returns></returns>
        public DataSet GetList(Guid QuestionID, int PageSize, Guid? LastAnswerID)
        {
            return DAL.GetList(QuestionID, PageSize, LastAnswerID);
        }
    }
}
