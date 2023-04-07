using ETMS.Components.StudyClass.API;
using ETMS.Components.StudyClass.API.Entity.StudyClass;
using ETMS.Components.StudyClass.Implement.DAL.StudyClass;
using System;
using System.Data;

namespace ETMS.Components.StudyClass.Implement.BLL.StudyClass
{
    public partial class Sty_UserStudyProgressLogic
    {
        private static readonly Sty_UserStudyProgressDataAccess DAL = new Sty_UserStudyProgressDataAccess();
        public CourseContentStudyProgress GetUserStudyProgress(int UserID, Guid ResourceID, Guid TrainingItemCourseID,string code) {
            return DAL.GetUserStudyProgress(UserID, ResourceID, TrainingItemCourseID, code);
        }

        public Sty_UserStudyProgress GetUserStudyProgressByChapterResourceID(Guid resourceID, Guid trainingItemCourseID, int userID) {
            return DAL.GetUserStudyProgress(resourceID, trainingItemCourseID, userID);
        }

        /// <summary>
        /// 完成学习
        /// </summary>
        /// <param name="resourceID">资源编号</param>
        /// <param name="userID">用户编号</param>
        /// <param name="progress">进度</param>
        public void StudyCompleted(Guid resourceID, Guid trainingItemCourseID, int userID, int progress)
        {
            UpdateStudyStatus(resourceID, trainingItemCourseID, userID, StudyStatus.Completed, progress);
        }

        /// <summary>
        /// 初始化学习
        /// </summary>
        /// <param name="resourceID">资源编号</param>
        /// <param name="trainingItemCourseID">项目课程ID</param>
        /// <param name="userID">用户编号</param>
        public void StudyInitialize(Guid resourceID, Guid trainingItemCourseID, int userID)
        {
            UpdateStudyStatus(resourceID, trainingItemCourseID,userID, null, null);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="courseUserStudyProgress">业务实体</param>
		public void Insert(Sty_UserStudyProgress currentProgress)
        {
            DAL.Insert(currentProgress);
            //BizLogHelper.AddOperate(courseUserStudyProgress);
        }

        /// <summary>
        /// 更新学习状态
        /// </summary>
        /// <param name="resourceID">资源编号</param>
        /// <param name="userID">用户编号</param>
        /// <param name="status">完成状态</param>
        /// <param name="progress">进度</param>
        public void UpdateStudyStatus(Guid resourceID, Guid trainingItemCourseID, int userID, StudyStatus? status, int? progress)
        {
            Sty_UserStudyProgress currentProgress = GetUserStudyProgressByChapterResourceID(resourceID, trainingItemCourseID, userID);
            if (currentProgress == null)
            {
                Sty_UserStudyProgress newProgress = new Sty_UserStudyProgress();
                newProgress.UserStudyProgressID = Guid.NewGuid();
                newProgress.UserID = userID;
                newProgress.ChapterResourceID = resourceID;
                newProgress.StudyStatus = status == null ? (short)StudyStatus.Studying : (short)status;
                newProgress.StudyProgress = progress == null ? 0 : (int)progress;
                newProgress.StudyTime = 0;
                newProgress.CreateTime = DateTime.Now;
                newProgress.ModifyTime = newProgress.CreateTime;
                newProgress.TrainingItemCourseID = trainingItemCourseID;
                Insert(newProgress);
            }
            else
            {
                Sty_UserStudyProgress newProgress = new Sty_UserStudyProgress();
                newProgress.UserStudyProgressID = currentProgress.UserStudyProgressID;
                newProgress.UserID = currentProgress.UserID;
                newProgress.ChapterResourceID = currentProgress.ChapterResourceID;
                newProgress.TrainingItemCourseID = currentProgress.TrainingItemCourseID;
                if (status == null)
                {
                    newProgress.StudyStatus = currentProgress.StudyStatus;
                }
                else
                {
                    newProgress.StudyStatus = currentProgress.StudyStatus == (int)StudyStatus.Completed ? (short)StudyStatus.Completed : (short)status;
                }

                newProgress.StudyProgress = progress == null ? currentProgress.StudyProgress : (int)progress;
                //newProgress.StudyTime = newProgress.StudyTime + newProgress.StudyProgress;
                newProgress.CreateTime = currentProgress.CreateTime;
                newProgress.ModifyTime = DateTime.Now;

                DAL.Update(newProgress);
                //BizLogHelper.UpdateOperate(currentProgress, newProgress);
            }
        }

        /// <summary>
        /// 终止学习
        /// </summary>
        /// <param name="resourceID">资源编号</param>
        /// <param name="userID">用户编号</param>
        /// <param name="progress">进度</param>
        public void StudyTerminate(Guid resourceID, Guid trainingItemCourseID, int userID, int progress)
        {
            UpdateStudyStatus(resourceID, trainingItemCourseID, userID, null, progress);
        }

        /// <summary>
        /// 获取用户最后学习的资源信息
        /// </summary>
        /// <param name="TrainingItemCourseID">培训项目课程ID</param>
        /// <param name="UserID">用户ID</param>
        /// <returns></returns>
        public DataTable GetUserStudyLastContent(Guid TrainingItemCourseID, int UserID)
        {
            return DAL.GetUserStudyLastContent(TrainingItemCourseID, UserID);
        }

    }
}
