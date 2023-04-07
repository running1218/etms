using System;

namespace ETMS.Components.Reporting.API.Entity
{
    public partial class OnlineItemCourseWare
    {
        public Guid ItemStudentCourseID { get; set; }
        public Guid ItemCourseResID { get; set; }
        public int UserID { get; set; }
        public string CoursewareName { get; set; }
        public decimal ShowHoures { get; set; }
        public decimal StudyProcess { get; set; }
        public decimal ActualStudyTime { get; set; }
        public decimal StudyTime { get; set; }
        public string CompleteStatus { get; set; }
        public int CompleteItem { get; set; }
        public int TotalItem { get; set; }
        public int CourseWareType { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime ModifyTime { get; set; }
    }
}
