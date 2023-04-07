using System;

namespace ETMS.Components.Exam.API.Entity
{
    /// <summary>
    /// 试题和试卷资源
    /// </summary>
    [Serializable]
    public class ExamResource
    {
        /// <summary>
        /// 资源ID
        /// </summary>
        public Guid ResourceID { get; set; }

        /// <summary>
        /// 浏览数
        /// </summary>
        public int ViewNumber { get; set; }

        /// <summary>
        /// 下载数
        /// </summary>
        public int DownloadNumber { get; set; }

        /// <summary>
        /// 评论数
        /// </summary>
        public int AppraiseNumber { get; set; }

        /// <summary>
        /// 分享状态
        /// </summary>
        public ShareType ShareState { get; set; }

        /// <summary>
        /// 审核状态
        /// </summary>
        public EnumReviewStatus AuditState { get; set; }
        
        /// <summary>
        /// 构造函数
        /// </summary>
        public ExamResource()
        {
            this.ResourceID = Guid.Empty;
            this.ShareState = ShareType.Null;
            this.AuditState = EnumReviewStatus.Null;
        }
    }
}
