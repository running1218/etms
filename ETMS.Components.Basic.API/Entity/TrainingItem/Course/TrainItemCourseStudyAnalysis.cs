using System;

namespace ETMS.Components.Basic.API.Entity.TrainingItem.Course
{
    public class TrainItemCourseStudyAnalysis
    {
        public Guid TrainingItemCourseID { get; set; }
        public int ChooseCourseNum { get; set; }
        public int CompletedNum { get; set; }
        public int UnStudyNum { get; set; }
        public int UnCompleteNum { get; set; }
        public int ContentCompleteNum { get; set; }
        public string ContentCompleteRate { get; set; }
        public int JobCompleteNum { get; set; }
        public string JobCompleteRate { get; set; }
        public string CourseCompleteRate { get; set; }

        public int? CourseResourceNum { get; set; }
        public int? JobNum { get; set; }
    }
}
