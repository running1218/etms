using ETMS.Components.StudyClass.API.Entity.StudyClass;
using ETMS.Components.StudyClass.Implement.DAL.StudyClass;
using System;

namespace ETMS.Components.StudyClass.Implement.BLL.StudyClass
{
    public class Sty_UserStudyProgressDetailsLogic
    {
        private static readonly Sty_UserStudyProgressDetailsDataAccess DAL = new Sty_UserStudyProgressDetailsDataAccess();
        public void Insert(Guid resourceID, Guid trainingItemCourseID, int userID, DateTime? startTime, int? progress)
        {
            if (startTime != null && startTime != default(DateTime))
            {
                DateTime endTime = DateTime.Now;
                TimeSpan ts = (TimeSpan)(endTime - startTime);


                DAL.Insert(new Sty_UserStudyProgressDetails()
                {
                    UserStudyProgressDetailsID = Guid.NewGuid(),
                    ChapterResourceID = resourceID,
                    UserID = userID,
                    StartTime = startTime,
                    EndTime = endTime,
                    StudyTime = Convert.ToDecimal(ts.TotalMilliseconds),
                    StudyProgress = progress == null ? 0 : (int)progress,
                    TrainingItemCourseID = trainingItemCourseID
                });
            }
        }
    }
}
