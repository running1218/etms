namespace ETMS.Components.Reporting.API.Entity
{
    public partial class OnlineItemCourseStatistics : OnlineItemCourse
    {
        public decimal DisScormStandardTime { get; set; }
        public decimal ScormStandardTime { get; set; }
        public decimal ScormActualTime { get; set; }
        public decimal ScormFinishRate { get; set; }
        public decimal FaceTrainingPlanTime { get; set; }
        public decimal FaceTrainingActualTime { get; set; }
        public decimal FaceTrainingFinishRate { get; set; }
        public decimal AvalableTotalStandardTime { get; set; }
        public decimal AvalableTotalActualTime { get; set; }
        public decimal AvalableTotalFinishRate { get; set; }
        public string ItemStudentCourseFinishStatus { get; set; }		
    }
}
