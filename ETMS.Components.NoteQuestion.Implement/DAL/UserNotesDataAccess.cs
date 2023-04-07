using ETMS.Components.NoteQuestion.API.Entity;
using ETMS.Utility;
using ETMS.Utility.Data;
using System;
using System.Data;
using System.Data.SqlClient;

namespace ETMS.Components.NoteQuestion.Implement.DAL
{
    public partial class UserNotesDataAccess
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="userNotes">业务实体</param>
        public void Insert(UserNotes userNotes)
        {
            string commandName = "[dbo].[Pr_User_Notes_Insert]";
            SqlParameter[] parameters = new SqlParameter[]
            {
                SqlHelper.CreateInputSqlParameter("@NotesID", SqlDbType.UniqueIdentifier, userNotes.NotesID),
                SqlHelper.CreateInputSqlParameter("@TrainingItemCourseID", SqlDbType.UniqueIdentifier, userNotes.TrainingItemCourseID,true),
                SqlHelper.CreateInputSqlParameter("@ContentID", SqlDbType.UniqueIdentifier, userNotes.ContentID,true),
                SqlHelper.CreateInputSqlParameter("@UserID", SqlDbType.Int, userNotes.UserID, true),
                SqlHelper.CreateInputSqlParameter("@Title", SqlDbType.NVarChar, userNotes.Title, true),
                SqlHelper.CreateInputSqlParameter("@NoteContent", SqlDbType.NVarChar, userNotes.NoteContent, true),
                SqlHelper.CreateInputSqlParameter("@IsPublic", SqlDbType.SmallInt, userNotes.IsPublic, true),
                SqlHelper.CreateInputSqlParameter("@CreateTime", SqlDbType.DateTime, userNotes.CreateTime, true),
                SqlHelper.CreateInputSqlParameter("@ModifyTime", SqlDbType.DateTime, userNotes.ModifyTime, true)
            };

            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parameters);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="userNotes">业务实体</param>
        public void Update(UserNotes userNotes)
        {
            string commandName = "[dbo].[Pr_User_Notes_Update]";
            SqlParameter[] parameters = new SqlParameter[]
            {
                SqlHelper.CreateInputSqlParameter("@NotesID", SqlDbType.UniqueIdentifier, userNotes.NotesID),
                SqlHelper.CreateInputSqlParameter("@TrainingItemCourseID", SqlDbType.UniqueIdentifier, userNotes.TrainingItemCourseID,true),
                SqlHelper.CreateInputSqlParameter("@ContentID", SqlDbType.UniqueIdentifier, userNotes.ContentID,true),
                SqlHelper.CreateInputSqlParameter("@UserID", SqlDbType.Int, userNotes.UserID, true),
                SqlHelper.CreateInputSqlParameter("@Title", SqlDbType.NVarChar, userNotes.Title, true),
                SqlHelper.CreateInputSqlParameter("@NoteContent", SqlDbType.NVarChar, userNotes.NoteContent, true),
                SqlHelper.CreateInputSqlParameter("@IsPublic", SqlDbType.SmallInt, userNotes.IsPublic, true),
                SqlHelper.CreateInputSqlParameter("@CreateTime", SqlDbType.DateTime, userNotes.CreateTime, true),
                SqlHelper.CreateInputSqlParameter("@ModifyTime", SqlDbType.DateTime, userNotes.ModifyTime, true)

            };

            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parameters);
        }

        /// <summary>
        /// 根据项目课程关系ID查询课件ID
        /// </summary>
        /// <param name="TrainingItemCourseID"></param>
        /// <returns></returns>
        public object GetCoursewareIDByTrainingItemCourseID(Guid TrainingItemCourseID)
        {
            string commandName = "[dbo].[Pr_User_Notes_GetCoursewareIDByTrainingItemCourseID]";
            SqlParameter[] parameters = new SqlParameter[]
            {
                SqlHelper.CreateInputSqlParameter("@TrainingItemCourseID", SqlDbType.UniqueIdentifier, TrainingItemCourseID)
            };
            var obj = SqlHelper.ExecuteScalar(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parameters);
            return obj;
        }

        /// <summary>
        /// 根据主键删除业务实体
        /// </summary>
        /// <param name="notesID"></param>
        public void Remove(Guid notesID)
        {
            string commandName = "[dbo].[Pr_User_Notes_Delete]";
            SqlParameter[] parameters = new SqlParameter[]
            {
                SqlHelper.CreateInputSqlParameter("@NotesID", SqlDbType.UniqueIdentifier, notesID)
            };

            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parameters);
        }

        /// <summary>
        /// 根据主键获取业务表
        /// </summary>
        /// <param name="notesID"></param>
        public DataTable GetByID(Guid notesID)
        {
            string commandName = "[dbo].[Pr_User_Notes_GetByPk]";
            SqlParameter[] parameters = new SqlParameter[]
            {
                SqlHelper.CreateInputSqlParameter("@NotesID", SqlDbType.UniqueIdentifier, notesID)
            };

            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parameters).Tables[0];
            return dt;
        }

        /// <summary>
        /// 自己的笔记
        /// </summary>
        /// <param name="pageIndex">第几页</param>
        /// <param name="pageSize">每页大小</param>
        /// <param name="orderByType">排序方式</param>
        /// <param name="UserID">用户ID</param>
        /// <param name="TrainingItemCourseID">项目课程关系ID</param>
        /// <param name="ContentID">资源ID</param>
        /// <param name="totalRecords">数据总条数</param>
        /// <returns></returns>
        public DataTable SinglePagedList(int pageIndex, int pageSize, string orderByType, int UserID, Guid TrainingItemCourseID, Guid ContentID, out int totalRecords)
        {
            totalRecords = 0;
            string commandName = "[dbo].[Pr_User_Notes_Single_GetPagedList]";
            SqlParameter[] parameters = new SqlParameter[]
            {
                SqlHelper.CreateInputSqlParameter("@PageIndex", SqlDbType.Int, pageIndex),
                SqlHelper.CreateInputSqlParameter("@PageSize", SqlDbType.Int, pageSize),
                SqlHelper.CreateInputSqlParameter("@TrainingItemCourseID", SqlDbType.UniqueIdentifier, TrainingItemCourseID),
                SqlHelper.CreateInputSqlParameter("@ContentID", SqlDbType.UniqueIdentifier, ContentID),
                SqlHelper.CreateInputSqlParameter("@UserID", SqlDbType.Int, UserID),
                SqlHelper.CreateInputSqlParameter("@OrderByType", SqlDbType.NVarChar, orderByType)
            };

            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parameters).Tables[0];
            if ((dt.Columns.Contains("TotalRecords")) && (dt.Rows.Count > 0))
                totalRecords = dt.Rows[0]["TotalRecords"].ToInt();

            return dt;
        }

        /// <summary>
        /// 别人的笔记
        /// </summary>
        /// <param name="pageIndex">第几页</param>
        /// <param name="pageSize">每页大小</param>
        /// <param name="orderByType">排序方式</param>
        /// <param name="UserID">用户ID</param>
        /// <param name="TrainingItemCourseID">项目课程关系ID</param>
        /// <param name="ContentID">资源ID</param>
        /// <param name="totalRecords">数据总条数</param>
        /// <returns></returns>
        public DataTable OtherPagedList(int pageIndex, int pageSize, string orderByType, int UserID, Guid TrainingItemCourseID, Guid ContentID, out int totalRecords)
        {
            totalRecords = 0;
            string commandName = "[dbo].[Pr_User_Notes_Other_GetPagedList]";
            SqlParameter[] parameters = new SqlParameter[]
            {
                SqlHelper.CreateInputSqlParameter("@PageIndex", SqlDbType.Int, pageIndex),
                SqlHelper.CreateInputSqlParameter("@PageSize", SqlDbType.Int, pageSize),
                SqlHelper.CreateInputSqlParameter("@TrainingItemCourseID", SqlDbType.UniqueIdentifier, TrainingItemCourseID),
                SqlHelper.CreateInputSqlParameter("@ContentID", SqlDbType.UniqueIdentifier, ContentID),
                SqlHelper.CreateInputSqlParameter("@UserID", SqlDbType.Int, UserID),
                SqlHelper.CreateInputSqlParameter("@OrderByType", SqlDbType.NVarChar, orderByType)
            };

            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parameters).Tables[0];
            if ((dt.Columns.Contains("TotalRecords")) && (dt.Rows.Count > 0))
                totalRecords = dt.Rows[0]["TotalRecords"].ToInt();

            return dt;
        }

        /// <summary>
        /// 获取笔记列表
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="TrainingItemCourseID"></param>
        /// <returns></returns>
        public DataTable GetList(int UserID, Guid TrainingItemCourseID, int PageSize, Guid ContentID, Guid? LastNoteID)
        {
            string commandName = "[dbo].[Pr_User_Notes_GetList]";
            SqlParameter[] parameters = new SqlParameter[]
            {
                SqlHelper.CreateInputSqlParameter("@TrainingItemCourseID", SqlDbType.UniqueIdentifier, TrainingItemCourseID),
                SqlHelper.CreateInputSqlParameter("@UserID", SqlDbType.Int, UserID),
                SqlHelper.CreateInputSqlParameter("@PageSize", SqlDbType.Int, PageSize),
                SqlHelper.CreateInputSqlParameter("@ContentID", SqlDbType.UniqueIdentifier, ContentID),
                SqlHelper.CreateInputSqlParameter("@LastNoteID", SqlDbType.UniqueIdentifier, LastNoteID, true)
            };
            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parameters).Tables[0];
            return dt;
        }
    }
}
