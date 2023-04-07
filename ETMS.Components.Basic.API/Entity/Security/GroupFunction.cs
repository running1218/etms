using System;
using System.Collections.Generic;

namespace ETMS.Components.Basic.API.Entity.Security
{
    [Serializable]
    public partial class GroupFunction
    {
        public int GroupID { get; set; }
        public string GroupCode { get; set; }
        public string GroupName { get; set; }
        public int OrderNo { get; set; }
        public List<GFunction> Functions { get; set; }
    }

    public partial class GFunction
    {
        public int FunctionID { get; set; }
        public string FunctionName { get; set; }
        public int OrderNo { get; set; }
        public string PageUrl { get; set; }
    }
}
