using System;

namespace ETMS.Activity.Entity
{
    public partial class Production
    {
        public Guid ProductID { get; set; }
        public Guid SiginupID { get; set; }
        public int ProductType { get; set; }
        public int Appraiser { get; set; }
        public int AppraiseStatus { get; set; }
        public DateTime UploadTime { get; set; }
        public DateTime AppraiseTime { get; set; }
        public decimal? Score { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string Extention { get; set; }
        public string Address { get; set; }
        public string Comment { get; set; }
        public int? TransStatus { get; set; }
        public string TransFilePath { get; set; }
        public DateTime? TransTime { get; set; }
        public int IsExcellent { get; set; }

        //扩展部分
        /// <summary>
        /// 活动作品类型
        /// </summary>
        public string TypeName { get; set; }
    }
}
