using ETMS.Components.OnlinePlaying.Implement.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using ETMS.Utility;
using ETMS.Components.OnlinePlaying;
using ETMS.Components.OnlinePlaying.API.Entity;
using ETMS.Utility.Service.FileUpload;
using ETMS.Components.Basic.API.Entity.Course;
using ETMS.Components.Basic.Implement.BLL.Course;
using ETMS.Components.OnlinePlaying.Core;
using ETMS.Components.Basic.Implement.BLL.Security;

namespace ETMS.Mobile.API.Controllers
{
    [EnableCors("*", "*", "*")]
    [RoutePrefix("Api/Living")]
    public class LivingController : ApiController
    {
        [HttpGet]
        [Route("Valid/{OrgID}/{PageIndex}/{PageSize}", Name = "获取直播列表信息")]
        public HttpResponseMessage Valid(int OrgID, int PageIndex, int PageSize)
        {
            int totalRecords = 0;
            StudentOnlinePlaying BLL = new StudentOnlinePlaying();
            var data = BLL.GetAllLivings(PageIndex, PageSize, OrgID, out totalRecords);
            long tick = DateTime.Now.Ticks;
            Random rnd = new Random((int)(tick & 0xffffffffL) | (int)(tick >> 32));
            foreach (var row in data)
            {
                int rowNum = CaculateChooseCourseNum(row, rnd);
                row.ThumbnailURL = string.IsNullOrEmpty(row.ThumbnailURL) ? "default.jpg" : row.ThumbnailURL;
                row.FocusCount = rowNum;
            }

            return ResponseJson.GetSuccessJson(data, totalRecords);
        }

        [HttpGet]
        [Route("PlayBack/{OrgID}/{PageIndex}/{PageSize}", Name = "获取回放列表信息")]
        public HttpResponseMessage PlayBack(int OrgID, int PageIndex, int PageSize)
        {
            int totalRecords = 0;
            StudentOnlinePlaying BLL = new StudentOnlinePlaying();
            var data = BLL.GetHistoryLivings(PageIndex, PageSize, OrgID, out totalRecords);
            return ResponseJson.GetSuccessJson(data, totalRecords);
        }

        [HttpGet]
        [Route("Address/{LivingID}/{UserID}/{NikeName}", Name = "获取直播地址")]
        public HttpResponseMessage GetLivingUrl(string LivingID, string UserID, string NikeName)
        {
            var json = new MTCloud().courseAccess(LivingID, UserID, NikeName, MTCloud.ROLE_USER, 7200, null);
            var result = JsonHelper.DeserializeObject<CourseAccessResult>(json);

            if (result.code == "0")
                return ResponseJson.GetSuccessJson(new { liveUrl = result.data.liveUrl });
            else
                return ResponseJson.GetBusinessFailedJson(-1, result.msg);
        }

        [HttpGet]
        [Route("PlayBackUrl/{LivingID}/{UserID}/{NikeName}", Name = "获取回放地址")]
        public HttpResponseMessage GetPlayBackUrl(string LivingID, string UserID, string NikeName)
        {
            var json = new MTCloud().courseAccessPlayback(LivingID, UserID, NikeName, MTCloud.ROLE_USER, 7200, null);
            var result = JsonHelper.DeserializeObject<CourseAccessResult>(json);

            if (result.code == "0")
                return ResponseJson.GetSuccessJson(new { playbackUrl = result.data.playbackUrl });
            else
                return ResponseJson.GetBusinessFailedJson(-1, result.msg);
        }

        [HttpGet]
        [Route("ItemsOfCourse/{courseID}/{orgID}", Name = "获取课程直播列表")]
        public HttpResponseMessage GetCourseLivings(string courseID, int orgID)
        {
            OnlinePlayingLogic logic = new OnlinePlayingLogic();
            var list = new List<Res_Living>();
            try
            {

                list = logic.GetLivingByCourseID(courseID.ToGuid());
                var openSource = new Res_ContentLogic().GetCourseOpenResource(orgID, courseID.ToGuid());
                foreach (var row in list)
                {
                    int count = openSource.Count(f => f.LivingID == row.LivingID);
                    if (count > 0)
                    {
                        row.IsOpen = 1;
                    }
                    else
                    {
                        row.IsOpen = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                ResponseJson.GetFailedJson(ex);
            }
            return ResponseJson.GetSuccessJson(list);
        }
        [HttpGet]
        [Route("LivingUrl/{livingID}/{userID}", Name = "获取课程直播地址信息")]
        public HttpResponseMessage GetLivingUrl(string LivingID, string userID)
        {
            string message = string.Empty;
            string livingUrl = string.Empty;

            OnlinePlayingLogic livingLogic = new OnlinePlayingLogic();
            LivingProvider livingProvider = new LivingProvider();
            Res_Living entity = livingLogic.GetLiving(LivingID);
            var user = new UserLogic().GetUserByID(userID.ToInt());

            //小班直播不支持移动端
            if (entity.LivingType != 3)
            {
                return ResponseJson.GetBusinessFailedJson(0, "移动端暂不支持，请在PC端学习");
            }
            else if (string.IsNullOrEmpty(LivingID) || string.IsNullOrEmpty(userID) || userID == "0")
            {
                return ResponseJson.GetBusinessFailedJson(0, "参数错误，请重刷新后重新进入！");
            }

            //small class
            if (entity.LivingType == 1 || entity.LivingType == 2)
            {
                // watch living record
                if (DateTime.Now >= entity.EndTime)
                {
                    var records = livingProvider.GetRecordList(LivingID);
                    if (null != records && records.Count > 0)
                    {
                        StudyRecord(LivingID, userID, entity);
                        records = records.OrderByDescending(f => f.duration).ToList();                        
                        livingUrl = records[0].playpath;
                    }
                }// enter living room
                else
                {
                    StudyRecord(LivingID, userID, entity);
                    livingUrl = livingProvider.EnterTKRoom(LivingID, user.LoginName, string.Empty, 2);                    
                }

            }// big calss
            else if (entity.LivingType == 3)
            {
                // watch living record
                if (DateTime.Now >= entity.EndTime)
                {                    
                    var records = livingProvider.MTCloudPlaybackRoom(LivingID, user.LoginName, user.LoginName, MTCloud.ROLE_USER);
                    CourseAccessResult model = JsonHelper.DeserializeObject<CourseAccessResult>(records);
                    if (model != null && model.data != null)
                    {
                        livingUrl = model.data.playbackUrl;
                        if (!string.IsNullOrEmpty(livingUrl))
                            StudyRecord(LivingID, userID, entity);
                    }                        
                }// enter living room
                else
                {
                    var result = livingProvider.EnterMTCloudRoom(LivingID, user.LoginName, user.LoginName, MTCloud.ROLE_USER);
                    CourseAccessResult record = JsonHelper.DeserializeObject<CourseAccessResult>(result);
                    if (record != null && record.data != null)
                    {
                        livingUrl = record.data.liveUrl;
                        if (!string.IsNullOrEmpty(livingUrl))
                            StudyRecord(LivingID, userID, entity);
                    }
                }

            }//unknow
            else
            {
                //do nothing
            }

            return ResponseJson.GetSuccessJson(livingUrl);
        }

        private void StudyRecord(string livingID, string userID, Res_Living entity)
        {
            if (userID != "0"&& userID != "")
            {
                if (DateTime.Now.AddMinutes(30) > entity.StartTime)
                {
                    new StudentOnlinePlaying().CreateUserLiving(userID, livingID);
                }
            }
        }
        private int CaculateChooseCourseNum(DemandCourse row, Random rnd)
        {
            int num = 0;
            int randamNum = 0;
            num = row.FocusCount;
            var courseID = row.CourseID.ToString();
            if (ETMS.Utility.CacheHelper.Get(courseID) != null)
            {
                randamNum = CacheHelper.Get(courseID).ToString().ToInt();
            }
            else
            {
                randamNum = rnd.Next(100, 1000);
                CacheHelper.Add(courseID.ToString(), randamNum, TimeSpan.FromDays(1000));
            }
            num += randamNum;

            return num;
        }


    }
}
