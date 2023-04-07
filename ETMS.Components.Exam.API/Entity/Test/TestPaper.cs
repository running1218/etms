using System;
using System.Collections.Generic;
namespace ETMS.Components.Exam.API.Entity.Test
{
    /// <summary>
    /// 试卷定义
    /// </summary>
    [Serializable]
    public class TestPaper
    {
        public Guid ID { get; set; }

        /// <summary>
        /// 试卷ID
        /// </summary>
        public Guid TestPaperID
        {
            get
            {
                return this.ID;
            }
            set
            {
                this.ID = value;
            }
        }

        /// <summary>
        /// 试卷名称
        /// </summary>
        public string TestPaperName { get; set; }

        /// <summary>
        /// 试卷类型(1:简单固定,2:高级固定,3:高级随机)
        /// </summary>
        public TestPaperType TestPaperType { get; set; }

        /// <summary>
        /// 试卷分类
        /// </summary>
        public Guid TestPaperCategory { get; set; }

        /// <summary>
        /// 试卷说明
        /// </summary>
        public String TestPaperDesc { get; set; }

        /// <summary>
        /// 试卷总题数
        /// </summary>
        public int TotalQuantity { get; set; }

        /// <summary>
        /// 试卷总分
        /// </summary>
        public decimal TotalScore { get; set; }

        /// <summary>
        /// 试卷及格分数
        /// </summary>
        public decimal PassedScore { get; set; }

        /// <summary>
        /// 最大考试次数
        /// </summary>
        public int MaxCount { get; set; }

        /// <summary>
        /// 考试时长（分钟）
        /// </summary>
        public int MaxTime { get; set; }

        /// <summary>
        /// 状态(0:空；1：等待审核；99：审核通过；98：审核不通过)
        /// </summary>
        public TestStatus Status { get; set; }

        /// <summary>
        /// 审核人
        /// </summary>
        public Guid ApproverID { get; set; }

        /// <summary>
        /// 审核时间
        /// </summary>
        public DateTime? ApproveTime { get; set; }

        /// <summary>
        /// 适应对象ID
        /// </summary>
        public short ObjectID { get; set; }

        /// <summary>
        /// 所属学科
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDelete { get; set; }

        /// <summary>
        /// 试卷反馈列表
        /// </summary>
        public IList<TestFeedback> Feedbacks { get; set; }
        /// <summary>
        /// 所属人/机构
        /// </summary>
        public string OwnerName { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdatedDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedUserID { get; set; }
        /// <summary>
        /// 构造函数
        /// </summary>
        public TestPaper()
        {
            this.TotalScore = 0M;
            this.PassedScore = 0M;
            this.MaxCount = 0;
            this.MaxTime = 0;
            this.Feedbacks = new List<TestFeedback>();
            this.Status = TestStatus.Waiting;
        }
    }
}
