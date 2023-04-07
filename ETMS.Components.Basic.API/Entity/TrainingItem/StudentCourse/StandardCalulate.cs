using System;

namespace ETMS.Components.Basic.API.Entity.TrainingItem
{
    public class StandardCalulate
    {
        public Guid? StudentCourseID { get; set; }
        public int? StudentID { get; set; }
        public Guid? TrainingItemID { get; set; }
        public Guid? TrainingItemCourseID { get; set; }
        public Guid? CourseID { get; set; }
        public int? CourseResourceNum { get; set; }
        public int? StudiedNum { get; set; }
        public decimal? CourseProgress { get; set; }
        public decimal? CourseScore { get; set; }
        public decimal? TestingScore { get; set; }
        public decimal? ActualScore { get; set; }
    }
}
