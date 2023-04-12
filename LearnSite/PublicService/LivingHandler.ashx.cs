using System;
using System.Linq;
using System.Web;
using ETMS.Utility;
using ETMS.Components.OnlinePlaying.Implement.BLL;
using ETMS.Components.OnlinePlaying;
using University.Mooc.AppContext;
using ETMS.Components.Basic.API.Entity.Course;
using ETMS.Utility.Service.FileUpload;

namespace ETMS.Studying.PublicService
{
    /// <summary>
    /// Summary description for LivingHandler
    /// </summary>
    public class LivingHandler : IHttpHandler
    {
        private HttpContext currentContext = null;
        public void ProcessRequest(HttpContext context)
        {
            currentContext = context;
            string method = currentContext.Request["Method"];
            if (string.IsNullOrEmpty(method))
            {
                ReturnResponseContent(JsonHelper.GetParametersInValidJson());
            }
            switch (method.ToLower())
            {
                case "nowvalidliving"://正在直播
                    ReturnResponseContent(GetNowValidLiving());
                    break;
                case "validliving"://课程分类
                    ReturnResponseContent(GetValidLiving());
                    break;
                case "indexliving"://课程分类
                    ReturnResponseContent(GetIndexLiving());
                    break;
                case "historyliving":
                    ReturnResponseContent(GetHistoryLiving());
                    break;
                case "getlivinginfo":
                    ReturnResponseContent(GetLivingInfo());
                    break;
                case "getplaybackinfo":
                    ReturnResponseContent(GetPlaybackInfo());
                    break;
                case "getcompetitivecourses":
                    ReturnResponseContent(GetCompetitiveCourses());
                    break;
                case "getmycompetitivecourses":
                    ReturnResponseContent(GetMyCompetitiveCourses());
                    break;
                case "getcompetitivecourse":
                    ReturnResponseContent(GetCompetitiveCourse());
                    break;
                default:
                    ReturnResponseContent(JsonHelper.GetParametersInValidJson());
                    break;
            }
        }

        public string GetNowValidLiving()
        {
            int PageIndex = string.IsNullOrEmpty(currentContext.Request["PageIndex"].ToString()) ? 1 : currentContext.Request["PageIndex"].ToString().ToInt();
            int PageSize = string.IsNullOrEmpty(currentContext.Request["PageSize"].ToString()) ? 20 : currentContext.Request["PageSize"].ToString().ToInt();
            int totalRecords = 0;

            var list = new StudentOnlinePlaying().GetNowValidLivings(PageIndex, PageSize, BaseUtility.SiteOrganizationID, out totalRecords);
            int PageTotal = totalRecords % PageSize > 0 ? totalRecords / PageSize + 1 : totalRecords / PageSize;//总页数
            return JsonHelper.SerializeObject(new { data = list, CurrentPage = PageIndex, PageTotal = PageTotal, total = totalRecords });
        }

        public string GetIndexLiving()
        {
            //var list = new StudentOnlinePlaying().GetIndexLivings(BaseUtility.SiteOrganizationID);
            var listCourse = new StudentOnlinePlaying().GetIndexLivings(BaseUtility.SiteOrganizationID).ToList<DemandCourse>();
            long tick = DateTime.Now.Ticks;
            Random rnd = new Random((int)(tick & 0xffffffffL) | (int)(tick >> 32));
            foreach (var row in listCourse)
            {
                int rowNum = CaculateChooseCourseNum(row, rnd);
                row.ThumbnailURL = StaticResourceUtility.GetFullPathByFileType(FileUploadFunctionType.CourseLogo, string.IsNullOrEmpty(row.ThumbnailURL) ? "default.jpg" : row.ThumbnailURL);
                row.FocusCount = rowNum;
            }

            var json = JsonHelper.SerializeObject(listCourse);
            json = "{\"data\":" + json + "}";
            return json;
        }
        public string GetValidLiving()
        {
            int PageIndex = string.IsNullOrEmpty(currentContext.Request["PageIndex"].ToString()) ? 1 : currentContext.Request["PageIndex"].ToString().ToInt();
            int PageSize = string.IsNullOrEmpty(currentContext.Request["PageSize"].ToString()) ? 20 : currentContext.Request["PageSize"].ToString().ToInt();
            int totalRecords = 0;
            
            var list = new StudentOnlinePlaying().GetValidLivings(PageIndex, PageSize, BaseUtility.SiteOrganizationID, out totalRecords);
            int PageTotal = totalRecords % PageSize > 0 ? totalRecords / PageSize + 1 : totalRecords / PageSize;//总页数
            return JsonHelper.SerializeObject(new {data=list, CurrentPage=PageIndex, PageTotal=PageTotal, total=totalRecords });
        }

        public string GetHistoryLiving()
        {
            int PageIndex = string.IsNullOrEmpty(currentContext.Request["PageIndex"].ToString()) ? 1 : currentContext.Request["PageIndex"].ToString().ToInt();
            int PageSize = string.IsNullOrEmpty(currentContext.Request["PageSize"].ToString()) ? 20 : currentContext.Request["PageSize"].ToString().ToInt();
            int totalRecords = 0;
            
            var list = new StudentOnlinePlaying().GetHistoryLivings(PageIndex, PageSize, BaseUtility.SiteOrganizationID, out totalRecords);
            int PageTotal = totalRecords % PageSize > 0 ? totalRecords / PageSize + 1 : totalRecords / PageSize;//总页数
            return JsonHelper.SerializeObject(new { data = list, CurrentPage = PageIndex, PageTotal = PageTotal, total = totalRecords });
        }

        public string GetLivingInfo()
        {
            string livingID = currentContext.Request["LivingID"].ToString();
            string userID = currentContext.Request["UserID"].ToString();
            string NikeName = currentContext.Request["NikeName"].ToString();
            new StudentOnlinePlaying().CreateUserLiving(userID, livingID);
            return new MTCloud().courseAccess(livingID, userID, NikeName, MTCloud.ROLE_USER, 7200, null);
        }

        public string GetPlaybackInfo()
        {
            string livingID = currentContext.Request["LivingID"].ToString();
            string userID = currentContext.Request["UserID"].ToString();
            string NikeName = currentContext.Request["NikeName"].ToString();
            new StudentOnlinePlaying().CreateUserLiving(userID, livingID);
            return new MTCloud().courseAccessPlayback(livingID, userID, NikeName, MTCloud.ROLE_USER, 7200, null);
        }

        /// <summary>
        /// return competitive courses
        /// </summary>
        /// <returns></returns>
        public string GetCompetitiveCourses()
        {
            try
            {
                var result = new StudentOnlinePlaying().GetCompetitiveCourses(BaseUtility.SiteOrganizationID);
                return JsonHelper.GetInvokeSuccessJson(result);
            }
            catch (BusinessException bix)
            {
                return JsonHelper.GetInvokeFailedJson(-1, ETMS.AppContext.BusinessException.GetBusinessErrorCode(bix));
            }
            catch{
                return JsonHelper.GetInvokeFailedJson(-1, "操作失败！");
            }
        }

        /// <summary>
        /// get my competitive courses
        /// </summary>
        /// <returns></returns>
        public string GetMyCompetitiveCourses()
        {
            try
            {
                var result = new StudentOnlinePlaying().GetMyCompetitiveCourses(UserContext.Current.UserID);
                return JsonHelper.GetInvokeSuccessJson(result);
            }
            catch (BusinessException bix)
            {
                return JsonHelper.GetInvokeFailedJson(-1, ETMS.AppContext.BusinessException.GetBusinessErrorCode(bix));
            }
            catch
            {
                return JsonHelper.GetInvokeFailedJson(-1, "操作失败！");
            }
        }

        /// <summary>
        /// get competitive course info
        /// </summary>
        /// <returns></returns>
        public string GetCompetitiveCourse()
        {
            try
            {
                Guid courseID = currentContext.Request["CourseID"].ToGuid();
                var result = new StudentOnlinePlaying().GetCompetitiveCourse(courseID);
                return JsonHelper.GetInvokeSuccessJson(result);
            }
            catch (BusinessException bix)
            {
                return JsonHelper.GetInvokeFailedJson(-1, ETMS.AppContext.BusinessException.GetBusinessErrorCode(bix));
            }
            catch
            {
                return JsonHelper.GetInvokeFailedJson(-1, "操作失败！");
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