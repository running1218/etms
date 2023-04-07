namespace ETMS.Components.Reporting.API.Entity
{
    public partial class StudentTrainingSummary
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserID { get; set; }
        /// <summary>
        /// 员工编号
        /// </summary>
        public string WorkerNo { get; set; }
        /// <summary>
        /// 员工姓名
        /// </summary>
        public string RealName { get; set; }
        /// <summary>
        /// 员工部门
        /// </summary>
        public int DepartmentID { get; set; }
        /// <summary>
        /// 部门名称
        /// </summary>
        public string DepartMentName { get; set; }
        /// <summary>
        /// 员工职级
        /// </summary>
        public int RankID { get; set; }
        /// <summary>
        /// 员工岗位
        /// </summary>
        public int PostID { get; set; }
        /// <summary>
        /// 岗位名称
        /// </summary>
        public string PostName { get; set; }
        /// <summary>
        /// 员工职务
        /// </summary>
        public string TitleName { get; set; }
        /// <summary>
        /// 培训项目数
        /// </summary>
        public int ItemNum { get; set; }
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
    }
}
