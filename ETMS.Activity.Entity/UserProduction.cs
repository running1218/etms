using System;

namespace ETMS.Activity.Entity
{
    public partial class UserProduction
    {
        public Guid SiginupID { get; set; }
        public string SiginupNo { get; set; }
        public Guid ProductID { get; set; }
        public string ProductName { get; set; }
        public string Extention { get; set; }
        public string Address { get; set; }
        public decimal Score { get; set; }
        public string Comment { get; set; }
        public int AppraiseStatus { get; set; }
    }
}
