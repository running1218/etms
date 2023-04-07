using System;

namespace ETMS.Components.Basic.API.Entity.Security
{
    public partial class Site_Agency
    {
        public int AgencyID { get; set; }
        public string AgencyCode { get; set; }
        public string AgencyName { get; set; }
        public int Status { get; set; }
        public int OrgID { get; set; }
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
    }
}
