using System;

namespace ETMS.Components.Basic.API.Entity.Course
{
    public class JobProgress
    {
        /// <summary>
        /// 作业ID
        /// </summary>
        public Guid JobID { get; set; }

        /// <summary>
        /// 作业名称
        /// </summary>
        public string JobName { get; set; }

        /// <summary>
        /// 最高成绩
        /// </summary>
        public int? Score { get; set; }
                
        /// <summary>
        /// 按钮文字
        /// </summary>
        public string BtnWord { get; set; }

        public string JobStatus { get; set; }

        /// <summary>
        /// 按钮链接
        /// </summary>
        public string BtnUrl { get; set; }

        public Guid JobPaperID { get; set; }

        public Guid StudentCourseID { get; set; }

        public Guid UserExamID { get; set; }
    }
}
