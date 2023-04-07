using System;

namespace ETMS.Activity.Entity
{
    public partial class PrizeResult
    {
        public Guid ReusltID { get; set; }
        public Guid ProductID { get; set; }
        public int PrizeID { get; set; }
        public int CreatorID { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
