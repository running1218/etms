using System;
using System.Collections.Generic;

namespace ETMS.Components.Exam.API.Entity.NewTestPaper
{
    /// <summary>
    /// 试卷实体类 add 2013-9-26 hjy
    /// </summary>
    public class Paper
    {
        /// <summary>
        /// 试卷ID
        /// </summary>
        public Guid TestPaperID { get; set; }

        /// <summary>
        /// 试卷名称
        /// </summary>
        public string TestPaperName { get; set; }

        /// <summary>
        /// 测试时长
        /// </summary>
        public int LimitTime { get; set; }

        /// <summary>
        /// 在线测试ID【测试和作业】
        /// </summary>
        public Guid OnLineTestID { get; set; }


        /// <summary>
        /// 开始时间
        /// </summary>
        public string BeginTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public string EndTime { get; set; }
        /// <summary>
        /// 测试类型【5-在线测试；2-在线作业】
        /// </summary>
        public int TestType { get; set; }

        /// <summary>
        ///  试卷总分
        /// </summary>
        public decimal TotalScore { get; set; }

        /// <summary>
        /// 试卷试题总数
        /// </summary>
        public int QuestionCount { get; set; }

        /// <summary>
        /// 用户提交次数
        /// </summary>
        public int UserTestCount { get; set; }

        /// <summary>
        /// 允许用户提交的次数
        /// </summary>
        public int TestCount { get; set; }

        /// <summary>
        /// 考生答卷ID
        /// </summary>
        public Guid UserExamID { get; set; }

        /// <summary>
        ///  考生得分
        /// </summary>
        public decimal UserScore { get; set; }

        /// <summary>
        /// 测试状态【作业：1-已提交和0-未提交】
        /// </summary>
        public int TestStatus { get; set; }

        /// <summary>
        /// 试题试题信息
        /// </summary>
        public IList<Question> PaperQuestion { get; set; }


    }


}
