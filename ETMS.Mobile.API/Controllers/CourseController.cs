using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using ETMS.Components.Basic.Implement.BLL.Course;
using ETMS.Components.Basic.API.Entity.Course;
using ETMS.Utility;
using ETMS.Utility.Service.FileUpload;
using ETMS.Components.Basic.Implement.BLL.Dictionary;
using ETMS.Components.Basic.Implement.BLL.Course.Resources;
using ETMS.Components.StudyClass.API.Entity.StudyClass;
using ETMS.Utility.Service;
using ETMS.Components.Basic.Implement.BLL.Bulletin;
using System.Configuration;

namespace ETMS.Mobile.API.Controllers
{
    [EnableCors("*", "*", "*")]
    [RoutePrefix("Api/Course")]
    public class CourseController : ApiController
    {
        [Route("{PageSize}/{PageIndex}/{SortRule}/{OrgID}", Name ="获取课程列表信息")]
        public HttpResponseMessage GetCoursePageList(int PageSize, int PageIndex,string SortRule, int OrgID, string CourseName=null,int CourseTypeID=0)
        {
            string Crieria = string.Empty;
            string SortExpression = string.Empty;

            if (!string.IsNullOrEmpty(CourseName))
            {
                Crieria += string.Format(" and CourseName like '%{0}%' ", CourseName);

            }
            if (CourseTypeID > 0)
            {
                Crieria += string.Format(" and CourseTypeID={0}", CourseTypeID);
            }
            if (SortRule.ToLower() == "study")
            {
                SortExpression = " FocusCount DESC ";
            }
            else
            {
                SortExpression = " IsTop DESC,Sort ASC ";
            }
            int totalRecords = 0;
            DataTable dtCourse = new Rec_CourseLogic().GetDemandCoursePagedList(PageIndex, PageSize, SortExpression, Crieria, OrgID, out totalRecords);
            List<DemandCourse> listCourse = new List<DemandCourse>();
            if (dtCourse != null)
            {
                foreach (DataRow dr in dtCourse.Rows)
                {
                    listCourse.Add(new DemandCourse()
                    {
                        CourseID = dr["CourseID"].ToGuid(),
                        CourseName = dr["CourseName"].ToString(),
                        CourseHours = dr["CourseHours"].ToString().ToInt(),
                        ThumbnailURL = StaticResourceUtility.GetFullPathByFileType(FileUploadFunctionType.CourseLogo, string.IsNullOrEmpty(dr["ThumbnailURL"].ToString()) ? "default.jpg" : dr["ThumbnailURL"].ToString()),
                        FocusCount = dr["FocusCount"].ToString().ToInt(),
                        CourseTypeID = dr["CourseTypeID"].ToString().ToInt(),
                        TeacherName = dr["TeacherName"].ToString()
                    });
                }
            }
            return ResponseJson.GetSuccessJson(listCourse);
        }

        [Route("CourseType/{OrgID}", Name ="获取课程分类信息")]
        public HttpResponseMessage GetCourseType(int OrgID)
        {
            DataTable dtCourseType = new Res_CourseLogic().GetCourseTypesByOrgID(OrgID);
            return ResponseJson.GetSuccessJson(dtCourseType);

        }
        [Route("Catalog/{CourseID}/{OrgID}", Name = "获取课程资源列表信息")]
        public HttpResponseMessage GetCourseResourceList(Guid CourseID, int OrgID)
        {
            int totalRecordCount = 0;
            DataTable dtResource = new Res_ContentLogic().GetPagedList(1, int.MaxValue - 1, " RCCR.[Sort] ASC ", " AND RCCR.[Status]=1 ", CourseID, out totalRecordCount);
            var openSource = new Res_ContentLogic().GetCourseOpenResource(OrgID, CourseID);
            foreach (DataRow row in dtResource.Rows)
            {
                int count = openSource.Count(f => f.ResourceID == row["ContentID"].ToGuid());
                if (count > 0)
                {
                    row["IsOpen"] = 1;
                }
                else
                {
                    row["IsOpen"] = 0;
                }
            }
            return ResponseJson.GetSuccessJson(dtResource);
        }
        [Route("Resource/{CourseID}/{ResourceID}", Name = "获取课程资源详细信息")]
        public HttpResponseMessage GetCourseResourceContent(Guid CourseID,Guid ResourceID)
        {
            CourseContentStudyProgress obj = new Res_ContentVideoLogic().GetResourceByCourseAndContentID(CourseID, ResourceID, System.Configuration.ConfigurationManager.AppSettings["TransCodingStream"]).Rows[0].ToEntity<CourseContentStudyProgress>();
            FileUploadConfig fileUploadConfig = ServiceRepository.FileUploadStrategyService.GetStrategy("MediaResourceVideo");
            obj.UrlRoot = fileUploadConfig.UrlRoot;
            obj.ThumbnailURL = StaticResourceUtility.GetFullPathByFileType(FileUploadFunctionType.CourseLogo, string.IsNullOrEmpty(obj.ThumbnailURL) ? "default.jpg" : obj.ThumbnailURL);
            return ResponseJson.GetSuccessJson(obj);
        }

        [Route("Notice/{TrainingItemCourseID}", Name = "获取课程公告列表信息")]
        public HttpResponseMessage GetCourseNoticeList(Guid TrainingItemCourseID)
        {
            var dt = new Inf_BulletinLogic().GetCourseNoticeList(TrainingItemCourseID);
            return ResponseJson.GetSuccessJson(dt);
        }

        [Route("Info/{courseID}", Name = "获取课程信息")]
        public HttpResponseMessage GetCourseInfo(string courseID)
        {
            var course = new Res_CourseLogic().GetById(courseID.ToGuid());
            if (course.StartDate != DateTime.MinValue)
            {
                course.StarDateString = course.StartDate.ToDate();
            }
            if (course.EndDate != DateTime.MinValue)
            {
                course.EndDateString = course.EndDate.ToDate();
            }

            long tick = DateTime.Now.Ticks;
            Random rnd = new Random((int)(tick & 0xffffffffL) | (int)(tick >> 32));
            course.FocusCount = CaculateChooseCourseNum(course, rnd);

            return ResponseJson.GetSuccessJson(course);
        }

        private int CaculateChooseCourseNum(Res_Course row, Random rnd)
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
