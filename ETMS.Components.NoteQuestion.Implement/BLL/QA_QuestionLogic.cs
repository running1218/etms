using ETMS.Components.NoteQuestion.API.Entity;
using ETMS.Components.NoteQuestion.Implement.DAL;
using ETMS.Utility;
using ETMS.Utility.Logging;
using System;
using System.Data;

namespace ETMS.Components.NoteQuestion.Implement.BLL
{
    public partial class QA_QuestionLogic
    {
        private QA_QuestionDataAccess DAL = new QA_QuestionDataAccess();

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="qAQuestion">业务实体</param>
        public void Insert(QA_Question qAQuestion)
        {
            DAL.Insert(qAQuestion);
            BizLogHelper.AddOperate(qAQuestion);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="qAQuestion"></param>
        public void Update(QA_Question qAQuestion)
        {
            DataTable dt = GetByID(qAQuestion.QuestionID);
            QA_Question originalEntity = dt.Rows.Count > 0 ? dt.Rows[0].ToEntity<QA_Question>() : null;
            originalEntity.QuestionTitle = qAQuestion.QuestionTitle;
            originalEntity.QuestionContent = qAQuestion.QuestionContent;
            DAL.Update(originalEntity);
            BizLogHelper.UpdateOperate(originalEntity, qAQuestion);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="questionID"></param>
        /// <returns></returns>
        public string Remove(Guid questionID)
        {
            DataTable dt = GetByID(questionID);
            QA_Question originalEntity = dt.Rows.Count > 0 ? dt.Rows[0].ToEntity<QA_Question>() : null;
            if (originalEntity == null)
                return JsonHelper.GetInvokeSuccessJson(new
                {
                    Status = false,
                    Code = 0,
                    Message = "删除失败！"
                });
            if (originalEntity.AnswerCount != 0)
                return JsonHelper.GetInvokeSuccessJson(new
                {
                    Status = false,
                    Code = 0,
                    Message = "问答有回复，不能删除！"
                });
            DAL.Remove(questionID);
            BizLogHelper.DeleteOperate(originalEntity);
            return JsonHelper.GetInvokeSuccessJson(new
            {
                Status = true,
                Code = 1,
                Message = "删除成功！"
            });
        }


        /// <summary>
        /// 根据主键获取业务实体
        /// </summary>
        /// <param name="questionID">问答 问题ID</param>
        public DataTable GetByID(Guid questionID)
        {
            return DAL.GetByID(questionID);
        }

        /// <summary>
        /// 分页查询业务表
        /// </summary>
        /// <param name="pageIndex">第几页</param>
        /// <param name="pageSize">每页数量</param>
        /// <param name="UserID">用户ID</param>
        /// <param name="isPersonalQuestion">自己还是大家的</param>
        /// <param name="sortExpression">排序方式</param>
        /// <param name="TrainingItemCourseID">项目课程关系ID</param>
        /// <param name="ContentID">资源ID</param>
        /// <param name="totalRecords">数据总数</param>
        /// <returns></returns>
        public DataTable GetPagedList(int pageIndex, int pageSize, int UserID, int isPersonalQuestion, string sortExpression, Guid TrainingItemCourseID, Guid ContentID, out int totalRecords)
        {
            return DAL.GetPagedList(pageIndex, pageSize, UserID, isPersonalQuestion, sortExpression, TrainingItemCourseID, ContentID, out totalRecords);
        }

        /// <summary>
        /// 获取问题列表
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="TrainingItemCourseID"></param>
        /// <param name="PageSize"></param>
        /// <param name="LastQuestionID"></param>
        /// <returns></returns>
        public DataTable GetList(int UserID, Guid TrainingItemCourseID, int PageSize, Guid ContentID, Guid? LastQuestionID = null)
        {
            return DAL.GetList(UserID, TrainingItemCourseID, PageSize, ContentID, LastQuestionID);
        }
    }
}
