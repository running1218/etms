using System;
using System.Collections.Generic;

namespace Open.Scorm.Entities
{
    [Serializable]
    public partial class ResourceItem
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
        /// 资源ID
        /// </summary>
        public string ResourceID { get; set; }
        /// <summary>
        /// 资源类型[adlcp:scormtype]
        /// </summary>
        public string ScormType { get; set; }
        /// <summary>
        /// 资源路径
        /// </summary>
        public string ResourcePath { get; set; }
        /// <summary>
        /// 资源类型[type]
        /// </summary>
        public string ResourceType { get; set; }
        /// <summary>
        /// 资源文件集[file]
        /// </summary>
        public List<ResourceFile> Files { get; set; }
    }
}
