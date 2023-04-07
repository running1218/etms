using System.Collections.Generic;

namespace ETMS.Activity.Entity
{
    public partial class Marking : MyActivityInfo
    {
        public int SubmitNum { get; set; }
        public int MarkingNum { get; set; }
        public int NoMarkingNum { get { return (SubmitNum - MarkingNum); } }
        
        public List<MarkingDetailStatic> MarkingGroup { get; set; }
    }
}
