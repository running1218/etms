using System;
using System.Collections.Generic;

namespace Open.Scorm.Entities
{
    [Serializable]
    public partial class OrganizationNode
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
        /// 组织机构名称【title】
        /// </summary>
        public string OrgTitle { get; set; }
        /// <summary>
        /// 结构编码【structure】
        /// </summary>
        public string StructureCode { get; set; }
        /// <summary>
        /// 【identifier】
        /// </summary>
        public string Identifier { get; set; }
        /// <summary>
        /// 章节
        /// </summary>
        public List<OrganizationItem> OrganizationItems { get; set; }
    }
}
