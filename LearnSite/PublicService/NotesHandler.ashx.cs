using University.Mooc.AppContext;
using ETMS.Components.NoteQuestion.API.Entity;
using ETMS.Components.NoteQuestion.Implement.BLL;
using ETMS.Utility;
using System;
using System.Data;
using System.Web;

namespace ETMS.Studying.PublicService
{
    /// <summary>
    /// NotesHandler 的摘要说明
    /// </summary>
    public class NotesHandler : IHttpHandler
    {

        private HttpContext currentContext = null;

        public void ProcessRequest(HttpContext context)
        {
            currentContext = context;

            string method = context.Request["Method"];

            var CurrentUserID = UserContext.Current.UserID;

            if (string.IsNullOrEmpty(method))
            {
                ReturnResponseContent(JsonHelper.GetParametersInValidJson());
                return;
            }
            switch (method.ToLower())
            {
                case "updatenotes":
                    ReturnResponseContent(UpdateNotes(CurrentUserID));
                    break;
                case "deletenotes":
                    ReturnResponseContent(DeleteNotes(CurrentUserID));
                    break;
                case "insertnotes":
                    ReturnResponseContent(InsertNotes(CurrentUserID));
                    break;
                case "selectnotes":
                    ReturnResponseContent(SelectNotes(CurrentUserID));
                    break;
                case "sharenotes":
                    ReturnResponseContent(ShareNotes());
                    break;
                default:
                    ReturnResponseContent(JsonHelper.GetParametersInValidJson());
                    break;
            }
        }

        /// <summary>
        /// 分享
        /// </summary>
        /// <returns></returns>
        public string ShareNotes()
        {
            Guid NotesID = currentContext.Request["NotesID"].ToGuid();

            if (NotesID == Guid.Empty)
                return JsonHelper.GetParametersInValidJson();
            try
            {
                UserNotesLogic userNotesLogic = new UserNotesLogic();

                UserNotes userNotes = userNotesLogic.GetByID(NotesID);
                if (userNotes != null && userNotes.UserID == UserContext.Current.UserID)
                {
                    userNotes.IsPublic = (short)((userNotes.IsPublic + 1) % 2);
                    userNotesLogic.Update(userNotes);
                    return JsonHelper.GetInvokeSuccessJson();
                }
                else
                {
                    return JsonHelper.GetInvokeFailedJson(-3, "参数错误");
                }

            }
            catch (BusinessException ex)
            {
                return JsonHelper.GetInvokeFailedJson(-2, ex.Message);
            }
            catch
            {
                return JsonHelper.GetInvokeFailedJson(-1, "删除失败");
            }
        }

        public string UpdateNotes(int CurrentUserID)
        {
            string NoteContent = currentContext.Request["NoteContent"];
            string title = currentContext.Request["title"];
            Guid NotesID = currentContext.Request["NotesID"].ToGuid();

            Int16 IsPublic;
            if (string.IsNullOrEmpty(currentContext.Request["IsPublic"]))
            {
                return JsonHelper.GetParametersInValidJson();
            }
            else
            {
                if (!Int16.TryParse(currentContext.Request["IsPublic"], out IsPublic))
                {
                    return JsonHelper.GetParametersInValidJson();
                }
            }


            if ((string.IsNullOrEmpty(title)) || (IsPublic != 1 && IsPublic != 0))
                return JsonHelper.GetParametersInValidJson();


            try
            {
                UserNotesLogic userNotesLogic = new UserNotesLogic();

                UserNotes userNotes = userNotesLogic.GetByID(NotesID);
                if (userNotes != null && userNotes.UserID == CurrentUserID)
                {
                    userNotes.NoteContent = NoteContent;
                    userNotes.Title = title;
                    userNotes.ModifyTime = DateTime.Now;
                    userNotes.IsPublic = IsPublic;
                    userNotesLogic.Update(userNotes);
                    return JsonHelper.GetInvokeSuccessJson();
                }
                else
                {
                    return JsonHelper.GetInvokeFailedJson(-3, "参数错误");
                }

            }
            catch (BusinessException ex)
            {
                return JsonHelper.GetInvokeFailedJson(-2, ex.Message);
            }
            catch
            {
                return JsonHelper.GetInvokeFailedJson(-1, "保存失败");
            }
        }

        public string DeleteNotes(int CurrentUserID)
        {
            Guid NotesID = currentContext.Request["NotesID"].ToGuid();

            if (NotesID == Guid.Empty)
                return JsonHelper.GetParametersInValidJson();
            try
            {
                UserNotesLogic userNotesLogic = new UserNotesLogic();
                userNotesLogic.Remove(NotesID);
                return JsonHelper.GetInvokeSuccessJson();

            }
            catch (BusinessException ex)
            {
                return JsonHelper.GetInvokeFailedJson(-2, ex.Message);
            }
            catch
            {
                return JsonHelper.GetInvokeFailedJson(-1, "删除失败");
            }
        }

        public string InsertNotes(int CurrentUserID)
        {
            string NoteContent = currentContext.Request["NoteContent"];
            string Title = currentContext.Request["Title"];
            var ContentID = currentContext.Request["ContentID"].ToGuid();
            var TrainingItemCourseID = currentContext.Request["TrainingItemCourseID"].ToGuid();

            Int16 IsPublic;
            if (string.IsNullOrEmpty(currentContext.Request["IsPublic"]))
            {
                return JsonHelper.GetParametersInValidJson();
            }
            else
            {
                if (!Int16.TryParse(currentContext.Request["IsPublic"], out IsPublic))
                {
                    return JsonHelper.GetParametersInValidJson();
                }
            }


            if ((string.IsNullOrEmpty(Title)) || (IsPublic != 1 && IsPublic != 0))
                return JsonHelper.GetParametersInValidJson();

            try
            {
                UserNotesLogic userNotesLogic = new UserNotesLogic();

                UserNotes userNotes = new UserNotes();
                userNotes.NotesID = Guid.NewGuid();
                userNotes.TrainingItemCourseID = TrainingItemCourseID;
                userNotes.UserID = CurrentUserID;
                userNotes.CreateTime = DateTime.Now;
                userNotes.ModifyTime = DateTime.Now;
                userNotes.NoteContent = NoteContent;
                userNotes.Title = Title;
                userNotes.ContentID = ContentID;
                userNotes.IsPublic = IsPublic;
                userNotesLogic.Insert(userNotes);
                return JsonHelper.GetInvokeSuccessJson();
            }
            catch (BusinessException ex)
            {
                return JsonHelper.GetInvokeFailedJson(-2, ex.Message);
            }
            catch
            {
                return JsonHelper.GetInvokeFailedJson(-1, "保存失败");
            }

        }


        int ToInt(string p)
        {
            int value = 0;
            int.TryParse(p, out value);
            return value;
        }

        public string SelectNotes(int CurrentUserID)
        {
            string NotesType = string.IsNullOrEmpty(currentContext.Request["NotesType"]) ? string.Empty : currentContext.Request["NotesType"].Trim();
            string orderByType = string.IsNullOrEmpty(currentContext.Request["orderByType"]) ? "desc" : currentContext.Request["orderByType"].Trim();
            int PageSize = string.IsNullOrEmpty(currentContext.Request["PageSize"]) ? 0 : ToInt(currentContext.Request["PageSize"].Trim());
            int PageOn = string.IsNullOrEmpty(currentContext.Request["PageOn"]) ? 0 : ToInt(currentContext.Request["PageOn"].Trim());
            var ContentID = currentContext.Request["ContentID"].ToGuid();
            var TrainingItemCourseID = currentContext.Request["TrainingItemCourseID"].ToGuid();

            if ((orderByType == string.Empty) || (PageSize == 0) || (PageOn == 0))
                return JsonHelper.GetParametersInValidJson();
            try
            {
                UserNotesLogic userNotesLogic = new UserNotesLogic();
                int totalRecords;
                DataTable userNotesTable = userNotesLogic.GetSingleOrOtherPagedList(PageOn, PageSize, orderByType, NotesType, TrainingItemCourseID, ContentID, CurrentUserID, out totalRecords);
                return JsonHelper.GetInvokeSuccessJson(userNotesTable, totalRecords);
            }
            catch (BusinessException ex)
            {
                return JsonHelper.GetInvokeFailedJson(-2, ex.Message);
            }
            catch (Exception ex)
            {
                return JsonHelper.GetInvokeFailedJson(-1, "查询失败");
            }
        }

        private void ReturnResponseContent(string content)
        {
            currentContext.Response.Clear();
            currentContext.Response.ContentType = "text/json";
            currentContext.Response.Write(content);
            currentContext.Response.End();
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

    }
}