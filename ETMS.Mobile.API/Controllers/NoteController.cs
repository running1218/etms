using ETMS.Components.NoteQuestion.API.Entity;
using ETMS.Components.NoteQuestion.Implement.BLL;
using ETMS.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ETMS.Mobile.API.Controllers
{
    [EnableCors("*", "*", "*")]
    [RoutePrefix("Api/Note")]
    public class NoteController : ApiController
    {
        [Route("List/{TrainingItemCourseID}/{UserID}/{PageSize}/{ContentID}/{LastNoteID}", Name = "获取培训项目课程的笔记列表信息")]
        public HttpResponseMessage GetList(Guid TrainingItemCourseID, int UserID, int PageSize, Guid ContentID, Guid? LastNoteID)
        {
            if (LastNoteID == Guid.Empty)
                LastNoteID = null;
            var data = new UserNotesLogic().GetList(UserID, TrainingItemCourseID, PageSize, ContentID, LastNoteID);
            return ResponseJson.GetSuccessJson(data);
        }

        [Route("Insert/{TrainingItemCourseID}/{ContentID}/{UserID}/{Title}/{NoteContent}/{IsPublic}", Name = "新增")]
        public HttpResponseMessage PostInsert(Guid TrainingItemCourseID, Guid ContentID, int UserID, string Title, string NoteContent, int IsPublic)
        {
            var note = new UserNotes();
            note.NotesID = Guid.NewGuid();
            note.TrainingItemCourseID = TrainingItemCourseID;
            note.ContentID = ContentID;
            note.UserID = UserID;
            note.Title = Title;
            note.NoteContent = NoteContent;
            note.IsPublic = (short)IsPublic;
            note.CreateTime = DateTime.Now;
            note.ModifyTime = DateTime.Now;
            new UserNotesLogic().Insert(note);
            return ResponseJson.GetSuccessJson();
        }

        [Route("Update/{NoteID}/{Title}/{NoteContent}/{IsPublic}", Name = "编辑")]
        public HttpResponseMessage PostUpdate(Guid NoteID, string Title, string NoteContent, int IsPublic)
        {
            var logic = new UserNotesLogic();
            var note = logic.GetByID(NoteID);
            if (note != null)
            {
                note.Title = Title;
                note.NoteContent = NoteContent;
                note.IsPublic = (short)IsPublic;
                new UserNotesLogic().Update(note);
                return ResponseJson.GetSuccessJson();
            }
            else
            {
                return ResponseJson.GetFailedJson();
            }
        }

        [Route("Remove/{NoteID}", Name = "删除笔记")]
        public HttpResponseMessage PostRemove(Guid NoteID)
        {
            new UserNotesLogic().Remove(NoteID);
            return ResponseJson.GetSuccessJson();
        }

        [Route("Note/{NoteID}", Name = "查询单个笔记")]
        public HttpResponseMessage GetNote(Guid NoteID)
        {
            var note = new UserNotesLogic().GetByID(NoteID);
            return ResponseJson.GetSuccessJson(note);
        }
    }
}
