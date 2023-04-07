namespace ETMS.Components.Reporting.API.Entity
{
    public partial class OrgnizationTrainingSummary
    {
        /// <summary>
        /// 机构ID
        /// </summary>
        public int OrganizationID { get; set; }
        /// <summary>
        /// 机构名称
        /// </summary>
        public string OrganizationName { get; set; }       
        /// <summary>
        /// 培训项目数
        /// </summary>
        public int ItemNum { get; set; }
        /// <summary>
        /// 学员数
        /// </summary>
        public int StudentNum { get; set; }
        /// <summary>
        /// 在线课程数
        /// </summary>
        public int OnLineCourseNum { get; set; }
        /// <summary>
        /// 在线课时数
        /// </summary>
        public decimal OnLineCourseHours { get; set; }
        /// <summary>
        /// 离线课程数
        /// </summary>
        public int OffLineCourseNum { get; set; }
        /// <summary>
        /// 离线课时数
        /// </summary>
        public decimal OffLineCourseHours { get; set; }
        /// <summary>
        /// 课时总数
        /// </summary>
        public decimal CourseSummaryHours
        {
            get
            {
                return OnLineCourseHours + OffLineCourseHours;
            }
        }
        /// <summary>
        /// 参见考试数
        /// </summary>
        public int JoinExamNum { get; set; }
        /// <summary>
        /// 组织考试数
        /// </summary>
        public int ExamNum { get; set; } 
        /// <summary>
        /// 考试平均分数
        /// </summary>
        public decimal AverageScore
        {
            get
            {
                if (JoinExamNum != 0)
                    return Score / JoinExamNum;
                else
                    return 0;
            }
        }
        /// <summary>
        /// 总分
        /// </summary>
        public decimal Score { get; set; }
        /// <summary>
        /// 内部导师数
        /// </summary>
        public int InnerTeacherNum { get; set; }
    }
}
