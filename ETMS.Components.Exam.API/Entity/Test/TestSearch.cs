using System;

namespace ETMS.Components.Exam.API.Entity.Test
{
    /// <summary>
    /// 试卷查询实体
    /// </summary>
    [Serializable]
    public class TestSearch
    {
        /// <summary>
        /// 试卷名称(模糊)
        /// </summary>
        public string TestPaperName { get; set; }
        /// <summary>
        /// 分类ID
        /// </summary>
        public Guid CategoryID { get; set; }
        /// <summary>
        /// 关键词/知识点(试卷说明)
        /// </summary>
        public string KeyWord { get; set; }
        /// <summary>
        /// 试卷类型(0:全部，1:简单固定,2:高级固定,3:高级随机)
        /// </summary>
        public TestPaperType TestPaperType { get; set; }
        /// <summary>
        /// 所属人/机构名称
        /// </summary>
        public string OwnerName { get; set; }
        /// <summary>
        /// 审核状态
        /// </summary>
        public AuditType AuditStatus { get; set; }
        /// <summary>
        /// 分享状态
        /// </summary>
        public ShareType ShareStatus { get; set; }

        public TestSearch()
        {
            this.TestPaperName = string.Empty;
            this.CategoryID = Guid.Empty;
            this.KeyWord = string.Empty;
            this.TestPaperType = Test.TestPaperType.Null;
            this.OwnerName = string.Empty;
            this.AuditStatus = AuditType.Null;
            this.ShareStatus = ShareType.Null;
        }
    }
}
