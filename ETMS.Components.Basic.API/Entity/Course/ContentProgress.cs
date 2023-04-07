using System;

namespace ETMS.Components.Basic.API.Entity.Course
{
    public class ContentProgress
    {
        /// <summary>
        /// 资源ID
        /// </summary>
        public Guid? ContentID { get; set; }

        /// <summary>
        /// 资源名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 资源类别
        /// </summary>
        public int? Type { get; set; }

        /// <summary>
        /// 资源总时长
        /// </summary>
        public int? PlayTime { get; set; }

        /// <summary>
        /// 学习进度
        /// </summary>
        public int? Progress { get; set; }

        /// <summary>
        /// 学习状态
        /// </summary>
        public int? Status { get; set; }

        /// <summary>
        /// 状态文字
        /// </summary>
        public string StatusWord { get; set; }

        /// <summary>
        /// 按钮链接
        /// </summary>
        public string BtnUrl { get; set; }

        /// <summary>
        /// 按钮文字
        /// </summary>
        public string BtnWord { get; set; }

        /// <summary>
        /// 视频时长时间
        /// </summary>
        public string ContentTime { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int? Sort { get; set; }

        /// <summary>
        /// 学习进度时间
        /// </summary>
        public string ProgressTime { get; set; }
    }
}
