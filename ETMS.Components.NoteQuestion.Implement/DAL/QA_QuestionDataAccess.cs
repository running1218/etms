using ETMS.Components.NoteQuestion.API.Entity;
using ETMS.Utility.Data;
using System;
using System.Data;
using System.Data.SqlClient;

namespace ETMS.Components.NoteQuestion.Implement.DAL
{
    public partial class QA_QuestionDataAccess
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="qAQuestion">业务实体</param>
        public void Insert(QA_Question qAQuestion)
        {
            string commandName = "[dbo].[Pr_QA_Question_Insert]";
            SqlParameter[] parameters = new SqlParameter[]
            {
                SqlHelper.CreateInputSqlParameter("@QuestionID", SqlDbType.UniqueIdentifier, qAQuestion.QuestionID),
                SqlHelper.CreateInputSqlParameter("@TrainingItemCourseID", SqlDbType.UniqueIdentifier, qAQuestion.TrainingItemCourseID),
                SqlHelper.CreateInputSqlParameter("@ContentID", SqlDbType.UniqueIdentifier, qAQuestion.ContentID),
                SqlHelper.CreateInputSqlParameter("@UserID", SqlDbType.Int, qAQuestion.UserID),
                SqlHelper.CreateInputSqlParameter("@QuestionTitle", SqlDbType.NVarChar, qAQuestion.QuestionTitle),
                SqlHelper.CreateInputSqlParameter("@QuestionContent", SqlDbType.NVarChar, qAQuestion.QuestionContent),
                SqlHelper.CreateInputSqlParameter("@CreateTime", SqlDbType.DateTime, qAQuestion.CreateTime),
                SqlHelper.CreateInputSqlParameter("@AnswerCount", SqlDbType.Int, qAQuestion.AnswerCount)
            };

            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parameters);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="qAQuestion">业务实体</param>
        public void Update(QA_Question qAQuestion)
        {
            string commandName = "[dbo].[Pr_QA_Question_Update]";
            SqlParameter[] parameters = new SqlParameter[]
            {
                SqlHelper.CreateInputSqlParameter("@QuestionID", SqlDbType.UniqueIdentifier, qAQuestion.QuestionID),
                SqlHelper.CreateInputSqlParameter("@TrainingItemCourseID", SqlDbType.UniqueIdentifier, qAQuestion.TrainingItemCourseID),
                SqlHelper.CreateInputSqlParameter("@ContentID", SqlDbType.UniqueIdentifier, qAQuestion.ContentID),
                SqlHelper.CreateInputSqlParameter("@UserID", SqlDbType.Int, qAQuestion.UserID),
                SqlHelper.CreateInputSqlParameter("@QuestionTitle", SqlDbType.NVarChar, qAQuestion.QuestionTitle),
                SqlHelper.CreateInputSqlParameter("@QuestionContent", SqlDbType.NVarChar, qAQuestion.QuestionContent),
                SqlHelper.CreateInputSqlParameter("@CreateTime", SqlDbType.DateTime, qAQuestion.CreateTime),
                SqlHelper.CreateInputSqlParameter("@AnswerCount", SqlDbType.Int, qAQuestion.AnswerCount)
            };

            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parameters);
        }

        /// <summary>
        /// 根据主键删除业务实体
        /// </summary>
        /// <param name="questionID">问答 问题ID</param>
        public void Remove(Guid questionID)
        {
            string commandName = "[dbo].[Pr_QA_Question_Delete]";
            SqlParameter[] parameters = new SqlParameter[]
            {
                SqlHelper.CreateInputSqlParameter("@QuestionID", SqlDbType.UniqueIdentifier, questionID)
            };

            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parameters);
        }

        /// <summary>
        /// 根据主键获取业务表
        /// </summary>
        /// <param name="questionID">问答 问题ID</param>
		public DataTable GetByID(Guid questionID)
        {
            string commandName = "[dbo].[Pr_QA_Question_GetByPk]";
            SqlParameter[] parameters = new SqlParameter[]
            {
                SqlHelper.CreateInputSqlParameter("@QuestionID", SqlDbType.UniqueIdentifier, questionID)
            };

            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parameters).Tables[0];
            return dt;
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
        public DataTable GetPagedList(int pageIndex, int pageSize, int UserID, int isPersonalQuestion, string sortExpression, Guid TrainingItemCourseID, Guid? ContentID, out int totalRecords)
        {
            totalRecords = 0;
            string commandName = "[dbo].[Pr_QA_Question_GetPagedList]";
            SqlParameter[] parameters = new SqlParameter[]
            {
                SqlHelper.CreateInputSqlParameter("@PageIndex", SqlDbType.Int, pageIndex),
                SqlHelper.CreateInputSqlParameter("@PageSize", SqlDbType.Int, pageSize),
                SqlHelper.CreateInputSqlParameter("@UserID", SqlDbType.Int, UserID),
                SqlHelper.CreateInputSqlParameter("@SortExpression", SqlDbType.NVarChar, sortExpression),
                SqlHelper.CreateInputSqlParameter("@IsPersonalQuestion", SqlDbType.Int, isPersonalQuestion),
                SqlHelper.CreateInputSqlParameter("@TrainingItemCourseID", SqlDbType.UniqueIdentifier, TrainingItemCourseID),
                SqlHelper.CreateInputSqlParameter("@ContentID", SqlDbType.UniqueIdentifier, ContentID),
                new SqlParameter("@ReturnValue", SqlDbType.Int, 4, ParameterDirection.ReturnValue, false, 0, 0, String.Empty, DataRowVersion.Default, null)
            };

            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parameters).Tables[0];
            totalRecords = (int)parameters[7].Value;

            return dt;
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
            string commandName = "[dbo].[Pr_QA_Question_GetList]";
            SqlParameter[] parameters = new SqlParameter[]
            {
                SqlHelper.CreateInputSqlParameter("@UserID", SqlDbType.Int, UserID),
                SqlHelper.CreateInputSqlParameter("@TrainingItemCourseID", SqlDbType.UniqueIdentifier, TrainingItemCourseID),
                SqlHelper.CreateInputSqlParameter("@PageSize", SqlDbType.Int, PageSize),
                SqlHelper.CreateInputSqlParameter("@ContentID", SqlDbType.UniqueIdentifier, ContentID, true),
                SqlHelper.CreateInputSqlParameter("@LastQuestionID", SqlDbType.UniqueIdentifier, LastQuestionID, true)
            };
            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parameters).Tables[0];
            return dt;
        }
    }
}
