using ETMS.Components.Basic.API.Entity.Course;
using ETMS.Components.Basic.Implement.DAL.Course;
using ETMS.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace ETMS.Components.Basic.Implement.BLL.Course
{
    public class Sty_UserStudyProgressLogic
    {
        Sty_UserStudyProgressDataAccess DAL = new Sty_UserStudyProgressDataAccess();


        /// <summary>
        /// 根据项目课程关系ID查询课程资源学习进度
        /// </summary>
        /// <param name="TrainingItemCourseID"></param>
        /// <returns></returns>
        public List<ContentProgress> GetCourseProgressByTrainingItemCourseID(Guid TrainingItemCourseID,int UserID, ref int complete, ref int notstarted, ref int unfinished)
        {
            var collection = DAL.GetCourseProgressByTrainingItemCourseID(TrainingItemCourseID, UserID);
            var ContentTable = collection[0];
            var ProgressTable = collection[1];
            var dict = new Dictionary<Guid, ContentProgress>();
            foreach (DataRow r in ProgressTable.Rows)
            {
                var p = new ContentProgress();
                p.ContentID = r["ChapterResourceID"] != null ? r["ChapterResourceID"].ToGuid() : Guid.Empty;

                p.Progress = r["StudyProgress"] != null ? (string.IsNullOrWhiteSpace(r["StudyProgress"].ToString()) ? 0 : Convert.ToInt32(r["StudyProgress"])) : 0;
                p.Status = r["StudyStatus"] != null ? (string.IsNullOrWhiteSpace(r["StudyStatus"].ToString()) ? 0 : Convert.ToInt32(r["StudyStatus"])) : 0;
                p.StatusWord = p.Status != null ? (p.Status == 0 ? "未完成" : "已完成") : "未学习";
                p.BtnWord = p.Status != null ? (p.Status == 0 ? "继续学习" : "重新学习") : "开始学习";
                p.BtnUrl = ETMS.Utility.WebUtility.AppPath + "/Study/StudyDetail.aspx?contentID=" + p.ContentID + "&TrainingItemCourseID=" + TrainingItemCourseID + "&ContentType=" + p.Type;
                p.ProgressTime = r["ProgressTime"] != null ? (string.IsNullOrWhiteSpace(r["ProgressTime"].ToString()) ? "00:00:00" : r["ProgressTime"].ToString()) : "00:00:00";
                if (p.Status == 1)
                {
                    complete += 1;
                }
                else if (p.Status == 0)
                {
                    unfinished += 1;
                }
                else
                {
                    notstarted += 1;
                }
                dict.Add(p.ContentID.Value, p);
            }

            foreach (DataRow r in ContentTable.Rows)
            {
                var ContentID = r["ContentID"] != null ? (string.IsNullOrWhiteSpace(r["ContentID"].ToString()) ? Guid.Empty : r["ContentID"].ToGuid()) : Guid.Empty;
                var Name = r["Name"] != null ? (string.IsNullOrWhiteSpace(r["Name"].ToString()) ? string.Empty : r["Name"].ToString()) : "";
                var Type = r["Type"] != null ? (string.IsNullOrWhiteSpace(r["Type"].ToString()) ? 0 : Convert.ToInt32(r["Type"])) : 0;
                var PlayTime = r["PlayTime"] != null ? (string.IsNullOrWhiteSpace(r["PlayTime"].ToString()) ? 0 : Convert.ToInt32(r["PlayTime"])) : 0;
                var ContentTime = r["ContentTime"] != null ? (string.IsNullOrWhiteSpace(r["ContentTime"].ToString()) ? "00:00:00" : r["ContentTime"].ToString()) : "00:00:00";
                var Sort = r["Sort"] != null ? (string.IsNullOrWhiteSpace(r["Sort"].ToString()) ? 0 : Convert.ToInt32(r["Sort"])) : 0;
                if (dict.ContainsKey(ContentID))
                {
                    dict[ContentID].Name = Name;
                    dict[ContentID].Type = Type;
                    dict[ContentID].PlayTime = PlayTime;
                    dict[ContentID].ContentTime = ContentTime;
                    dict[ContentID].Sort = Sort;
                }
                else
                {
                    var p = new ContentProgress();
                    p.ContentID = ContentID;
                    p.Name = Name;
                    p.Type = Type;
                    p.PlayTime = PlayTime;
                    p.ContentTime = ContentTime;
                    p.Sort = Sort;
                    p.StatusWord = "未学习";
                    p.BtnWord = "开始学习";
                    p.Progress = 0;
                    p.Status = -1;
                    p.BtnUrl = ETMS.Utility.WebUtility.AppPath + "/Study/StudyDetail.aspx?contentID=" + p.ContentID + "&TrainingItemCourseID=" + TrainingItemCourseID + "&ContentType=" + p.Type;
                    p.ProgressTime = "00:00:00";
                    dict.Add(p.ContentID.Value, p);
                    notstarted += 1;
                }
            }
            return dict.Values.OrderBy(a => a.Sort).ToList();
        }

        /// <summary>
        /// 获取学生测评结果【在线测试和在线作业】
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="TrainingItemCourseID"></param>
        /// <returns></returns>
        public DataSet GetTestAndPaperProgress(int UserID, Guid TrainingItemCourseID)
        {
            return DAL.GetTestAndPaperProgress(UserID, TrainingItemCourseID);
        }
        /// <summary>
        /// 获取学习进度
        /// </summary>
        /// <param name="UserID">用户ID</param>
        /// <param name="TrainingItemCourseID">培训项目课程ID</param>
        /// <returns></returns>
        public decimal GetStudyProcessPercent(int UserID, Guid TrainingItemCourseID)
        {
            int contentComplete = 0, contentNotstarted = 0, contentUnfinished = 0, testjobComplete = 0;
            var contentList = GetCourseProgressByTrainingItemCourseID(TrainingItemCourseID, UserID, ref contentComplete, ref contentNotstarted, ref contentUnfinished);
            var ds = GetTestAndPaperProgress(UserID, TrainingItemCourseID);
            foreach (DataRow r in ds.Tables[0].Rows)
            {
                int count = r["UserTestCount"] != null ? (string.IsNullOrWhiteSpace(r["UserTestCount"].ToString()) ? 0 : Convert.ToInt32(r["UserTestCount"])) : 0;
                if (count > 0)
                    testjobComplete += 1;
            }
            foreach (DataRow r in ds.Tables[1].Rows)
            {
                string status = r["TestStatus"] != null ? (string.IsNullOrWhiteSpace(r["TestStatus"].ToString()) ? "未提交" : r["TestStatus"].ToString()) : "未提交";
                if (status == "已提交")
                    testjobComplete += 1;
            }

            var total = contentList.Count + ds.Tables[0].Rows.Count + ds.Tables[1].Rows.Count;
            decimal percentage = 0;
            if (total > 0 && (contentComplete + testjobComplete) > 0)
            {
                percentage = Math.Round((decimal)((contentComplete + testjobComplete) * 100 / total), 2, MidpointRounding.AwayFromZero);
            }
            return percentage;
        }
    }
}
