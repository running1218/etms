using System;
using System.Collections.Generic;
using ETMS.Components.Exam.API.Entity.ItemBank;
using Autumn.Data.ORM.IBatis;
using ETMS.AppContext;

namespace ETMS.Components.Exam.Implement.DAL.ItemBank
{
    public class QuestionIBatisDao : ReadWriteDataMapperDaoSupport, IQuestionDao
    {
        ///<summary>
        /// 添加一个试题
        ///</summary>
        /// <param name="question">要添加的试题的基类。必须是系统支持的几种试题的具体类实例。</param>
        public void AddQuestion(Question question)
        {
            this.DataMapperClient_Write.Insert("ItemBank.Question.AddQuestion", question);
        }
        ///<summary>
        /// 添加一个试题
        ///</summary>
        /// <param name="newQuestion">更新的试题实体（必须为试题类型的实体）</param>
        public void Update(Question newQuestion)
        {
            this.DataMapperClient_Write.Update("ItemBank.Question.Update", newQuestion);
        }
        ///<summary>
        /// 删除指定的试题
        ///</summary>
        /// <param name="questionID">要删除的试题的ID</param>
        public void Delete(Guid questionID, int updatedUserID)
        {
            try
            {
                this.DataMapperClient_Write.Delete("ItemBank.Question.Delete", new { QuestionID = questionID, UpdatedUserID = updatedUserID });
            }
            catch (Exception ex) {
                if (ex.Message.IndexOf("FK_KS_TESTT_REFERENCE_TK_QUEST") != -1)
                {
                    throw new ETMS.AppContext.BusinessException("被引用的试题不允许删除！");
                }
            }
        }
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="questionIDs">试题的IDs</param>
        public void DeleteBatch(IList<Guid> questionIDs, int updatedUserID)
        {
            this.DataMapperClient_Write.Delete("ItemBank.Question.DeleteBatch", new { QuestionIDs = questionIDs, UpdatedUserID = updatedUserID });
        }
        ///<summary>
        /// 得到某一试题。会根据试题的类型得到具体试题类型的实例。
        ///</summary>
        /// <param name="questionID">要获取的试题的ID</param>
        public Question GetByID(Guid questionID)
        {
            return this.DataMapperClient_Write.QueryForObject<Question>("ItemBank.Question.GetByID", questionID);
        }
        /// <summary>
        /// 修改答案字符串
        /// </summary>
        /// <param name="questionID">试题ID</param>
        /// <param name="answer">答案字符串</param>
        /// <param name="updatedUserID">更新用户ID</param>
        public void UpdateAnswers(Guid questionID, string answer, int updatedUserID)
        {
            this.DataMapperClient_Write.Update("ItemBank.Question.UpdateAnswers", new { QuestionID = questionID, Answers = answer, UpdatedUserID = updatedUserID });
        }
        /// <summary>
        /// 获取答案字符串
        /// </summary>
        /// <param name="questionID">试题ID</param>
        /// <returns>答案字符串</returns>
        public string GetAnswersByID(Guid questionID)
        {
            return this.DataMapperClient_Read.QueryForObject<string>("ItemBank.Question.GetQuestionAnswers", questionID);
        }

        /// <summary>
        /// 删除指定分类下的所有试题
        /// </summary>
        /// <param name="classID">分类ID</param>
        public void DeleteClassID(Guid classID)
        {
            this.DataMapperClient_Write.Update("ItemBank.Question.DeleteClassID", classID);
        }
        /// <summary>
        /// 修改指定分类的试题到新分类
        /// </summary>
        /// <param name="oldClassID">老分类</param>
        /// <param name="newClassID">新分类</param>
        public void UpdateClassID(Guid oldClassID, Guid newClassID)
        {
            this.DataMapperClient_Write.Update("ItemBank.Question.UpdateClassID",
                new {NewID = newClassID,OldID = oldClassID}
            );
        }

        public void SetShareState(Guid questionID, int state)
        {
            this.DataMapperClient_Write.Update("ItemBank.Question.SetShareState",
                new { QuestionID = questionID, UpdatedUserID = UserContext.Current.UserID, ShareStatus = state });
        }

        public void SetAuditState(Guid questionID, int state)
        {
            this.DataMapperClient_Write.Update("ItemBank.Question.SetAuditState",
                new { QuestionID = questionID, UpdatedUserID = UserContext.Current.UserID, AuditStatus = state });
        }
    }
}
