using System;

namespace ETMS.Components.Reporting.API.Entity
{
    public partial class OnlineItemCourse
    {
        public int UserID { get; set; }
        public string RealName { get; set; }
        public string WorkNo { get; set; }
        public int OrganizationID { get; set; }
        public string OrganizationName { get; set; }
        public int DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        public string OrgPath { get; set; }
        public int PostID { get; set; }
        public string PostName { get; set; }
        public int PostTypeID { get; set; }
        public string PostTypeName { get; set; }
        public int RankID { get; set; }
        public string RankName { get; set; }
        public Guid TrainingItemID { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public Guid CourseID { get; set; }
        public Guid ItemCourseID { get; set; }
        public string ItemCourseCode { get; set; }
        public string ItemCourseName { get; set; }
        public int ItemCourseTypeID { get; set; }
        public string ItemCourseTypeName { get; set; }
        public int ItemCourseAttrID { get; set; }
        public string ItemCourseAttrName { get; set; }
        public DateTime ItemCourseBeginTime { get; set; }
        public DateTime ItemCourseEndTime { get; set; }
        public decimal ItemCourseHours { get; set; }
        public decimal ItemPassLine { get; set; }
        public Guid ItemStudentCourseID { get; set; }
        public decimal ItemCourseSumGrade { get; set; }
        public string ItemCourseRemark { get; set; }
        public int ItemCourseScoreStatus { get; set; }
        public string ItemCourseScoreStatusDesc { get; set; }
        public int ItemCourseTeachModelID { get; set; }
        public string ItemCourseTeachModelName { get; set; }
        public int ItemCourseTrainingModelID { get; set; }
        public string ItemCourseTrainingModelName { get; set; }
        public string ItemCourseTeacher { get; set; }
        public string ItemCourseTestName { get; set; }
        public string ItemCourseTestScore { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime ModifyTime { get; set; }
        public int Status { get; set; }
    }
}
