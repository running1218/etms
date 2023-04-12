using ETMS.Utility;
using System;
using System.Web;
using University.Mooc.AppContext;
using ETMS.Activity.Entity;
using ETMS.Activity.Implement.BLL;
using ETMS.Utility.Service.FileUpload;
using System.Data;

namespace ETMS.Studying.PublicService
{
    /// <summary>
    /// Summary description for Appraisal
    /// </summary>
    public class Appraisal : IHttpHandler
    {
        private static readonly AppraisalLogic logic = new AppraisalLogic();
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
                case "getappraisallist":
                    ReturnResponseContent(GetAppraisalList());
                    break;
                case "signup":
                    ReturnResponseContent(AppraisalSignup());
                    break;
                case "getmyactivitiesgoing":
                    ReturnResponseContent(GetMyActivitiesGoing());
                    break;
                case "getmyactivitiescompleted":
                    ReturnResponseContent(GetMyActivitiesCompleted());
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 活动主页列表
        /// </summary>
        /// <returns></returns>
        public string GetAppraisalList()
        {
            try
            {
                int PageIndex = string.IsNullOrEmpty(currentContext.Request["PageIndex"].ToString()) ? 1 : currentContext.Request["PageIndex"].ToString().ToInt();
                int PageSize = string.IsNullOrEmpty(currentContext.Request["PageSize"].ToString()) ? 20 : currentContext.Request["PageSize"].ToString().ToInt();
                int totalRecords = 0;

                var result = logic.GetAppraisalList(BaseUtility.SiteOrganizationID, PageIndex, PageSize, out totalRecords);

                foreach (var item in result)
                {
                    item.ImageUrl = StaticResourceUtility.GetFullPathByFileType(FileUploadFunctionType.MediaLogo, string.IsNullOrEmpty(item.ImageUrl) ? "default.jpg" : item.ImageUrl);
                }

                int PageTotal = totalRecords % PageSize > 0 ? totalRecords / PageSize + 1 : totalRecords / PageSize;//总页数
                return JsonHelper.GetInvokeSuccessJson(new { Result = result, CurrentPage = PageIndex, PageTotal = PageTotal, TotalRecords = totalRecords });
            }
            catch
            {
                return JsonHelper.GetInvokeFailedJson(-1, "获取活动列表失败");
            }
        }


        public string AppraisalSignup()
        {
            try
            {
                Guid activityID = string.IsNullOrEmpty(currentContext.Request["ActivityID"].ToString()) ? Guid.Empty : currentContext.Request["ActivityID"].ToString().ToGuid();
                string Name = currentContext.Request["Name"].ToString();
                int Age = currentContext.Request["Age"].ToString().ToInt();
                int Sex = currentContext.Request["Sex"].ToString().ToInt();
                string Phone = currentContext.Request["Phone"].ToString();
                string School = currentContext.Request["School"].ToString();
                int Area = currentContext.Request["Area"].ToString().ToInt();
                int Team = currentContext.Request["Team"].ToString().ToInt();

                Siginup user = new Siginup();
                user.SiginupID = Guid.NewGuid();
                user.UserID = UserContext.Current.UserID;
                user.AppraisalID = activityID;
                //user.SiginupNo = "YLW" + DateTime.Now.Year;//YLW+Year+报名总人数
                user.SiginupStatus = 1;
                user.GroupID = Team;
                user.RegionID = Area;
                user.School = School;
                user.SiginupTime = DateTime.Now;
                user.Name = Name;
                user.Age = Age;
                user.Sex = Sex;
                user.Phone = Phone;
                string SiginupNo = "";
                SiginupLogic logic = new SiginupLogic();
                DataTable dt = logic.GetSiginup(user.UserID, user.AppraisalID);
                if (dt.Rows.Count == 0)
                {
                    SiginupNo = logic.Insert(user);
                    return JsonHelper.GetInvokeSuccessJson(SiginupNo);
                }
                else {
                    return JsonHelper.GetInvokeFailedJson(-1, "您已经报过名，无法重复报名");
                }
            }
            catch(Exception ex)
            {
                return JsonHelper.GetInvokeFailedJson(-1, "报名失败，请联系管理员");
            }
        }

        /// <summary>
        /// 正在进行的活动列表
        /// </summary>
        /// <returns></returns>
        public string GetMyActivitiesGoing()
        {
            try
            {
                var result = new SiginupLogic().GetMyActivitiesGoing(UserContext.Current.UserID);
                return JsonHelper.GetInvokeSuccessJson(result);
            }
            catch
            {
                return JsonHelper.GetInvokeFailedJson(-1, "获取我的正在进行的活动列表失败");
            }
        }

        /// <summary>
        /// 获取已经结束的活动列表
        /// </summary>
        /// <returns></returns>
        public string GetMyActivitiesCompleted()
        {
            try
            {
                var result = new SiginupLogic().GetMyActivitiesCompleted(UserContext.Current.UserID);
                return JsonHelper.GetInvokeSuccessJson(result);
            }
            catch
            {
                return JsonHelper.GetInvokeFailedJson(-1, "获取我的结束的活动列表失败");
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