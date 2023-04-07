using ETMS.Components.NoteQuestion.API.Entity;
using ETMS.Components.NoteQuestion.Implement.DAL;
using ETMS.Utility;
using ETMS.Utility.Logging;
using System;
using System.Data;

namespace ETMS.Components.NoteQuestion.Implement.BLL
{
    public partial class UserNotesLogic
    {
        private UserNotesDataAccess DAL = new UserNotesDataAccess();

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="userNotes">业务实体</param>
        public void Insert(UserNotes userNotes)
        {
            DAL.Insert(userNotes);
            BizLogHelper.AddOperate(userNotes);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="userNotes">业务实体</param>
        public void Update(UserNotes userNotes)
        {
            UserNotes originalEntity = GetByID(userNotes.NotesID);
            DAL.Update(userNotes);
            BizLogHelper.UpdateOperate(originalEntity, userNotes);
        }

        /// <summary>
        /// 根据项目课程关系ID查询课件ID
        /// </summary>
        /// <param name="TrainingItemCourseID"></param>
        /// <returns></returns>
        public Guid GetCoursewareIDByTrainingItemCourseID(Guid TrainingItemCourseID)
        {
            var obj = DAL.GetCoursewareIDByTrainingItemCourseID(TrainingItemCourseID);
            return obj.ToGuid();
        }


        /// <summary>
        /// 根据主键删除业务实体
        /// </summary>
        /// <param name="notesID"></param>
        public void Remove(Guid notesID)
        {
            UserNotes originalEntity = GetByID(notesID);
            DAL.Remove(notesID);
            BizLogHelper.DeleteOperate(originalEntity);
        }

        /// <summary>
        /// 根据主键获取业务实体
        /// </summary>
        /// <param name="notesID"></param>
        public UserNotes GetByID(Guid notesID)
        {
            DataTable dt = DAL.GetByID(notesID);
            return dt.Rows.Count > 0 ? dt.Rows[0].ToEntity<UserNotes>() : null;
        }

        /// <summary>
        /// 资源笔记分页查询业务表
        /// </summary>
        /// <param name="pageIndex">第几页</param>
        /// <param name="pageSize">每页大小</param>
        /// <param name="orderByType">排序方式</param>
        /// <param name="NotesType">自己的笔记或者大家的笔记</param>
        /// <param name="TrainingItemCourseID">项目课程ID</param>
        /// <param name="ContentID">资源ID</param>
        /// <param name="CurrentUserID">当前用户ID</param>
        /// <param name="totalRecords">数据总条数</param>
        /// <returns></returns>
        public DataTable GetSingleOrOtherPagedList(int pageIndex, int pageSize, string orderByType, string NotesType, Guid TrainingItemCourseID, Guid ContentID, int CurrentUserID, out int totalRecords)
        {

            if (orderByType.ToLower() == "desc")
            {
                orderByType = "desc";
            }
            else
            {
                orderByType = "asc";
            }

            switch (NotesType)
            {
                case "1":
                    return DAL.SinglePagedList(pageIndex, pageSize, orderByType, CurrentUserID, TrainingItemCourseID, ContentID, out totalRecords);
                default:
                    return DAL.OtherPagedList(pageIndex, pageSize, orderByType, CurrentUserID, TrainingItemCourseID, ContentID, out totalRecords);
            }
        }

        /// <summary>
        /// 获取笔记列表
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="TrainingItemCourseID"></param>
        /// <param name="PageSize"></param>
        /// <param name="LastNoteID"></param>
        /// <returns></returns>
        public DataTable GetList(int UserID, Guid TrainingItemCourseID, int PageSize, Guid ContentID, Guid? LastNoteID)
        {
            return DAL.GetList(UserID, TrainingItemCourseID, PageSize, ContentID, LastNoteID);
        }

    }
}
