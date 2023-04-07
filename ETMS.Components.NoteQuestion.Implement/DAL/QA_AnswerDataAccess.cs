using ETMS.Components.NoteQuestion.API.Entity;
using ETMS.Utility;
using ETMS.Utility.Data;
using System;
using System.Data;
using System.Data.SqlClient;

namespace ETMS.Components.NoteQuestion.Implement.DAL
{
    public partial class QA_AnswerDataAccess
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="qAAnswer">业务实体</param>
        public void Insert(QA_Answer qAAnswer)
        {
            string commandName = "[dbo].[Pr_QA_Answer_Insert]";
            SqlParameter[] parameters = new SqlParameter[]
            {
                SqlHelper.CreateInputSqlParameter("@AnswerID", SqlDbType.UniqueIdentifier, qAAnswer.AnswerID),
                SqlHelper.CreateInputSqlParameter("@UserID", SqlDbType.Int, qAAnswer.UserID),
                SqlHelper.CreateInputSqlParameter("@AnswerContent", SqlDbType.NVarChar, qAAnswer.AnswerContent),
                SqlHelper.CreateInputSqlParameter("@CreateTime", SqlDbType.DateTime, qAAnswer.CreateTime),
                SqlHelper.CreateInputSqlParameter("@QuestionID", SqlDbType.UniqueIdentifier, qAAnswer.QuestionID)
            };

            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parameters);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="qAAnswer">业务实体</param>
        public void Update(QA_Answer qAAnswer)
        {
            string commandName = "[dbo].[Pr_QA_Answer_Update]";
            SqlParameter[] parameters = new SqlParameter[]
            {
                SqlHelper.CreateInputSqlParameter("@AnswerID", SqlDbType.UniqueIdentifier, qAAnswer.AnswerID),
                SqlHelper.CreateInputSqlParameter("@UserID", SqlDbType.Int, qAAnswer.UserID),
                SqlHelper.CreateInputSqlParameter("@AnswerContent", SqlDbType.NVarChar, qAAnswer.AnswerContent),
                SqlHelper.CreateInputSqlParameter("@CreateTime", SqlDbType.DateTime, qAAnswer.CreateTime),
                SqlHelper.CreateInputSqlParameter("@QuestionID", SqlDbType.UniqueIdentifier, qAAnswer.QuestionID)
            };

            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parameters);
        }

        /// <summary>
        /// 根据主键删除业务实体
        /// </summary>
        /// <param name="answerID">回答ID</param>
        public void Remove(Guid answerID)
        {
            string commandName = "[dbo].[Pr_QA_Answer_Delete]";
            SqlParameter[] parameters = new SqlParameter[]
            {
                SqlHelper.CreateInputSqlParameter("@AnswerID", SqlDbType.UniqueIdentifier, answerID)
            };

            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parameters);
        }

        /// <summary>
        /// 根据主键获取业务表
        /// </summary>
        /// <param name="answerID">回答ID</param>
		public DataTable GetByID(Guid answerID)
        {
            string commandName = "[dbo].[Pr_QA_Answer_GetByPk]";
            SqlParameter[] parameters = new SqlParameter[]
            {
                SqlHelper.CreateInputSqlParameter("@AnswerID", SqlDbType.UniqueIdentifier, answerID)
            };

            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parameters).Tables[0];
            return dt;
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
            totalRecords = 0;
            string commandName = "[dbo].[Pr_QA_Answer_GetPagedList]";
            SqlParameter[] parameters = new SqlParameter[]
            {
                SqlHelper.CreateInputSqlParameter("@PageIndex", SqlDbType.Int, pageIndex),
                SqlHelper.CreateInputSqlParameter("@PageSize", SqlDbType.Int, pageSize),
                SqlHelper.CreateInputSqlParameter("@QuestionID", SqlDbType.UniqueIdentifier, qAAnswer.QuestionID)
            };

            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parameters).Tables[0];
            if ((dt.Columns.Contains("TotalRecords")) && (dt.Rows.Count > 0))
                totalRecords = dt.Rows[0]["TotalRecords"].ToInt();

            return dt;
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
            totalRecords = 0;
            string commandName = "[dbo].[Pr_QA_Answer_GetPagedListTmpl]";
            SqlParameter[] parameters = new SqlParameter[]
            {
                SqlHelper.CreateInputSqlParameter("@PageIndex", SqlDbType.Int, pageIndex),
                SqlHelper.CreateInputSqlParameter("@PageSize", SqlDbType.Int, pageSize),
                SqlHelper.CreateInputSqlParameter("@QuestionIDs", SqlDbType.NVarChar,questionIDs)
            };

            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parameters).Tables[0];
            if ((dt.Columns.Contains("TotalRecords")) && (dt.Rows.Count > 0))
                totalRecords = dt.Rows[0]["TotalRecords"].ToInt();

            return dt;
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
            string commandName = "[dbo].[Pr_QA_Answer_GetList]";
            SqlParameter[] parameters = new SqlParameter[]
            {
                SqlHelper.CreateInputSqlParameter("@PageSize", SqlDbType.Int, PageSize),
                SqlHelper.CreateInputSqlParameter("@QuestionID", SqlDbType.UniqueIdentifier, QuestionID),
                SqlHelper.CreateInputSqlParameter("@LastAnswerID", SqlDbType.UniqueIdentifier, LastAnswerID, true)
            };
            DataSet ds = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parameters);
            return ds;
        }

    }
}
