using System;
using System.Collections.Generic;

namespace Open.Scorm.Entities
{
    [Serializable]
    public partial class OrganizationResource
    {
        public List<OrganizationNode> OrganizationNodes { get; set; }
        public List<ResourceItem> ResourceItems { get; set; }
    }
}
