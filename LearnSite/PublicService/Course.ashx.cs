using University.Mooc.AppContext;
using ETMS.Components.Basic.API.Entity.Course;
using ETMS.Components.Basic.Implement.BLL.Bulletin;
using ETMS.Components.Basic.Implement.BLL.Course;
using ETMS.Utility;
using ETMS.Utility.Service.FileUpload;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ETMS.Studying.PublicService
{
    /// <summary>
    /// Summary description for Course
    /// </summary>
    public class Course : IHttpHandler
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
                case "coursetype"://课程分类
                    ReturnResponseContent(GetCourseType());
                    break;
                case "demandcourselist":
                    ReturnResponseContent(GetCourseList());
                    break;
                //查询课程公告
                case "getnoticelist":
                    ReturnResponseContent(GetNoticeList(2));
                    break;
                case "getguidancelist":
                    ReturnResponseContent(GetNoticeList(3));
                    break;
                case "getcourseprogress":
                    ReturnResponseContent(GetCourseProgress());
                    break;
                default:
                    ReturnResponseContent(JsonHelper.GetParametersInValidJson());
                    break;
            }
        }

        /// <summary>
        /// 获取课程公告列表
        /// </summary>
        /// <returns></returns>
        private string GetNoticeList(int articleType)
        {
            var TrainingItemCourseID = currentContext.Request["TrainingItemCourseID"].ToGuid();
            var bll = new Inf_BulletinLogic();
            var dt = bll.GetCourseNoticeList(TrainingItemCourseID, articleType);
            return JsonHelper.GetInvokeSuccessJson(dt);
        }

        //获取课程分类
        private string GetCourseType()
        {
            DataTable dtCourseType = new Res_CourseLogic().GetCourseTypesByOrgID(BaseUtility.SiteOrganizationID);
            var json = JsonHelper.SerializeObject(dtCourseType);
            return json;
        }
        //获取课程列表
        private string GetCourseList()
        {
            string SortExpression = "";//currentContext.Request["SortExpression"] == null ? "FocusCount DESC" : string.Format(" FocusCount {0} ",  currentContext.Request["SortExpression"].ToString());
            int PageIndex = string.IsNullOrEmpty(currentContext.Request["PageIndex"].ToString()) ? 1 : currentContext.Request["PageIndex"].ToString().ToInt();
            int PageSize = string.IsNullOrEmpty(currentContext.Request["PageSize"].ToString()) ? 20 : currentContext.Request["PageSize"].ToString().ToInt();

            string SearchContent = currentContext.Request["SearchKey"] == null ? "" : currentContext.Request["SearchKey"].ToString();
            int typeID = currentContext.Request["TypeID"] == null ? 0 : Convert.ToInt32( currentContext.Request["TypeID"].ToString());
            int classID = currentContext.Request["ClassID"] == null ? 0 : Convert.ToInt32(currentContext.Request["ClassID"].ToString());
            int totalRecords = 0;

            var dataResult = new Rec_CourseLogic().GetCoursePagedList(PageIndex, PageSize, SortExpression, typeID, classID, SearchContent, BaseUtility.SiteOrganizationID, out totalRecords).ToList<DemandCourse>();
            int PageTotal = totalRecords % PageSize > 0 ? totalRecords / PageSize + 1 : totalRecords / PageSize;//总页数
            long tick = DateTime.Now.Ticks;
            Random rnd = new Random((int)(tick & 0xffffffffL) | (int)(tick >> 32));
            foreach (var row in dataResult)
            {
                int rowNum = CaculateChooseCourseNum(row, rnd);
                row.ThumbnailURL = StaticResourceUtility.GetFullPathByFileType(FileUploadFunctionType.CourseLogo, string.IsNullOrEmpty(row.ThumbnailURL) ? "default.jpg" : row.ThumbnailURL);
                row.FocusCount = rowNum;
            }
            var json = JsonHelper.SerializeObject(dataResult);
            json = "{\"data\":" + json + ",\"CurrentPage\":" + PageIndex + ",\"PageTotal\":" + PageTotal + ",\"total\":" + totalRecords + "}";
            return json;
        }

        private int CaculateChooseCourseNum(DemandCourse row, Random rnd)
        {
            string courseID = row.CourseID.ToString();
            string courseModel = row.CourseModel.ToString();
            int num = row.FocusCount;

            if (courseModel == "2")
            {
                if (row.StartDate < DateTime.Now)
                {
                    int randamNum = 0;

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
                }
            }

            return num;
        }

        /// <summary>
        /// 获取学习进度
        /// </summary>
        /// <returns></returns>
        private string GetCourseProgress()
        {
            var TrainingItemCourseID = currentContext.Request["TrainingItemCourseID"].ToGuid();
            var progressLogic = new Sty_UserStudyProgressLogic();
            int contentComplete = 0, contentNotstarted = 0, contentUnfinished = 0, testjobComplete = 0, testjobNotstarted = 0;
            var contentList = progressLogic.GetCourseProgressByTrainingItemCourseID(TrainingItemCourseID, UserContext.Current.UserID, ref contentComplete, ref contentNotstarted, ref contentUnfinished);
            var ds = progressLogic.GetTestAndPaperProgress(UserContext.Current.UserID, TrainingItemCourseID);
            var testList = new List<TestProgress>();
            foreach (DataRow r in ds.Tables[0].Rows)
            {
                var test = new TestProgress();
                test.TestID = r["TestID"] != null ? (string.IsNullOrWhiteSpace(r["TestID"].ToString()) ? Guid.Empty : r["TestID"].ToGuid()) : Guid.Empty;
                test.TestName = r["TestName"] != null ? (string.IsNullOrWhiteSpace(r["TestName"].ToString()) ? string.Empty : r["TestName"].ToString()) : string.Empty;
                test.MaxUseCount = r["TestCount"] != null ? (string.IsNullOrWhiteSpace(r["TestCount"].ToString()) ? 0 : Convert.ToInt32(r["TestCount"])) : 0;
                test.AlreadyUseCount = r["UserTestCount"] != null ? (string.IsNullOrWhiteSpace(r["UserTestCount"].ToString()) ? 0 : Convert.ToInt32(r["UserTestCount"])) : 0;
                test.MaxScore = r["Score"] != null ? (string.IsNullOrWhiteSpace(r["Score"].ToString()) ? 0 : Convert.ToInt32(r["Score"])) : 0;
                test.BtnWord = test.AlreadyUseCount > 0 ? (test.AlreadyUseCount >= test.MaxUseCount ? "查看测评" : "开始测评") : "开始测评";
                test.TestPaperID = r["TestPaperID"] != null ? (string.IsNullOrWhiteSpace(r["TestPaperID"].ToString()) ? Guid.Empty : r["TestPaperID"].ToGuid()) : Guid.Empty;
                test.StudentCourseID = r["StudentCourseID"] != null ? (string.IsNullOrWhiteSpace(r["StudentCourseID"].ToString()) ? Guid.Empty : r["StudentCourseID"].ToGuid()) : Guid.Empty;
                var param = "?TestPaperID=" + test.TestPaperID + "&TrainingItemCourseID=" + TrainingItemCourseID + "&OnlineTestID=" + test.TestID + "&StudentCourseID=" + test.StudentCourseID + "&TestType=5";
                test.BtnUrl = test.AlreadyUseCount >= test.MaxUseCount ? (ETMS.Utility.WebUtility.AppPath + "/Study/TestResultList.aspx" + param) : (ETMS.Utility.WebUtility.AppPath + "/Study/DoHomework.aspx" + param);
                testList.Add(test);
                if (test.AlreadyUseCount > 0)
                    testjobComplete += 1;
                else
                    testjobNotstarted += 1;
            }
            var jobList = new List<JobProgress>();
            foreach (DataRow r in ds.Tables[1].Rows)
            {
                var job = new JobProgress();
                job.JobID = r["TestID"] != null ? (string.IsNullOrWhiteSpace(r["TestID"].ToString()) ? Guid.Empty : r["TestID"].ToGuid()) : Guid.Empty;
                job.JobName = r["TestName"] != null ? (string.IsNullOrWhiteSpace(r["TestName"].ToString()) ? string.Empty : r["TestName"].ToString()) : string.Empty;
                job.UserExamID = r["UserExamID"] != null ? (string.IsNullOrWhiteSpace(r["UserExamID"].ToString()) ? Guid.Empty : r["UserExamID"].ToGuid()) : Guid.Empty;
                job.Score = r["Score"] != null ? (string.IsNullOrWhiteSpace(r["Score"].ToString()) ? 0 : Convert.ToInt32(r["Score"])) : 0;
                job.JobStatus = r["TestStatus"] != null ? (string.IsNullOrWhiteSpace(r["TestStatus"].ToString()) ? "未提交" : r["TestStatus"].ToString()) : "未提交";
                job.BtnWord = job.JobStatus == "已提交" ? "查看测评" : "开始测评";
                job.JobPaperID = r["TestPaperID"] != null ? (string.IsNullOrWhiteSpace(r["TestPaperID"].ToString()) ? Guid.Empty : r["TestPaperID"].ToGuid()) : Guid.Empty;
                job.StudentCourseID = r["StudentCourseID"] != null ? (string.IsNullOrWhiteSpace(r["StudentCourseID"].ToString()) ? Guid.Empty : r["StudentCourseID"].ToGuid()) : Guid.Empty;
                var param = "?TestPaperID=" + job.JobPaperID + "&TrainingItemCourseID=" + TrainingItemCourseID + "&OnlineTestID=" + job.JobID + "&StudentCourseID=" + job.StudentCourseID + "&TestType=2&UserExamID=" + job.UserExamID;
                job.BtnUrl = job.JobStatus == "已提交" ? (ETMS.Utility.WebUtility.AppPath + "/Study/SeeAnswerResult.aspx" + param) : (ETMS.Utility.WebUtility.AppPath + "/Study/DoHomework.aspx" + param);
                jobList.Add(job);
                if (job.JobStatus == "已提交")
                    testjobComplete += 1;
                else
                    testjobNotstarted += 1;
            }

            var total = contentList.Count + testList.Count + jobList.Count;
            var percentage = Math.Round((decimal)((contentComplete + testjobComplete) * 100 / total), 2, MidpointRounding.AwayFromZero);
            return JsonHelper.GetInvokeSuccessJson(new { Total = total, Content = contentList, Test = testList, Job = jobList, ContentComplete = contentComplete, ContentNotstarted = contentNotstarted, ContentUnfinished = contentUnfinished, TestjobComplete = testjobComplete, TestjobNotstarted = testjobNotstarted, Percentage = percentage });
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