using System;

namespace ETMS.Activity.Entity
{
    public partial class ReviewResult
    {
        public Guid ReviewID { get; set; }
        public Guid ProductID { get; set; }
        public int ReviewNum { get; set; }
        public DateTime ReviewTime { get; set; }
    }
}
