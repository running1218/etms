using System;

namespace ETMS.Components.Exam.API.Entity.Test
{
    /// <summary>
    /// 试卷查询结果
    /// </summary>
    [Serializable]
    public class TestSearchResult
    {
        /// <summary>
        /// 分享评价信息
        /// </summary>
        public ExamResource Resource { get; set; }
        /// <summary>
        /// 试卷信息
        /// </summary>
        public TestPaper TestPaperInfo{get;set;}
        /// <summary>
        /// 构造函数
        /// </summary>
        public TestSearchResult()
        {
            this.Resource = new ExamResource();
           this.TestPaperInfo = new TestPaper();
        }
    }
}
