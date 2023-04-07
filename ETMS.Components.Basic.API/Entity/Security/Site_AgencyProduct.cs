using System;

namespace ETMS.Components.Basic.API.Entity.Security
{
    public class Site_AgencyProduct
    {
        public Guid AgencyProductID { get; set; }
        public int AgencyID { get; set; }
        public Guid CourseID { get; set; }
        public string AgencyCode { get; set; }
        public int DiscountType { get; set; }
        public decimal DiscountPrice { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public Int32 CreateUserID { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public String CreateUser { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime ModifyTime { get; set; }

        /// <summary>
        /// 修改人
        /// </summary>
        public String ModifyUser { get; set; }

        /// <summary>
        /// 扩展课程属性
        /// </summary>
        public string CourseName { get; set; }
    }
}
