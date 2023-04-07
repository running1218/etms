using System;

namespace ETMS.Components.Basic.API.Entity.Course
{
    public class TestProgress
    {
        /// <summary>
        /// 测试ID
        /// </summary>
        public Guid TestID { get; set; }

        /// <summary>
        /// 测试名称
        /// </summary>
        public string TestName { get; set; }

        /// <summary>
        /// 最高成绩
        /// </summary>
        public int? MaxScore { get; set; }

        /// <summary>
        /// 最大使用次数
        /// </summary>
        public int? MaxUseCount { get; set; }

        /// <summary>
        /// 已经使用次数
        /// </summary>
        public int? AlreadyUseCount { get; set; }

        /// <summary>
        /// 按钮文字
        /// </summary>
        public string BtnWord { get; set; }

        /// <summary>
        /// 按钮链接
        /// </summary>
        public string BtnUrl { get; set; }


        public Guid TestPaperID { get; set; }

        public Guid StudentCourseID { get; set; }
    }
}
