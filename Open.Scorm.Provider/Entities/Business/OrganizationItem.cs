using System;

namespace Open.Scorm.Entities
{
    [Serializable]
    public class OrganizationItem
    {
        /// <summary>
        /// ID 标识
        /// </summary>
        public Guid ID
        {
            get;
            set;
        }
        /// <summary>
        /// 索引值
        /// </summary>
        public int Index { get; set; }
        /// <summary>
        /// 机构ID
        /// </summary>
        public string OrganizationID { get; set; }
        /// <summary>
        /// 机构名称
        /// </summary>
        public string OrganizationName { get; set; }
        /// <summary>
        /// 章节ID
        /// </summary>
        public string IdentifierID { get; set; }
        /// <summary>
        /// 章节名称
        /// </summary>
        public string IdentifierName { get; set; }
        /// <summary>
        /// 父章节ID
        /// </summary>
        public string IdentifierParentID { get; set; }
        /// <summary>
        /// 章节资源ID
        /// </summary>
        public string IdentifierResourceID { get; set; }
        /// <summary>
        /// 是否显示可见
        /// </summary>
        public bool Isvisible { get; set; }
    }
}
