
using System;
namespace ETMS.Components.Basic.API.Entity.Security
{
    /// <summary>
    /// 学员信息(用户扩展表)业务实体
    /// </summary>
    [Serializable]
    public partial class Site_Student
    {

        /// <summary>
        /// 工号
        /// </summary>
        public String WorkerNo { get; set; }

        /// <summary>
        /// 职级
        /// </summary>
        public Int32? RankID { get; set; }

        /// <summary>
        /// 岗位
        /// </summary>
        public Int32? PostID { get; set; }
       
        /// <summary>
        /// 直接上级
        /// </summary>
        public String Superior { get; set; }

        /// <summary>
        /// 最高学历
        /// </summary>
        public String LastEducation { get; set; }

        /// <summary>
        /// 专业
        /// </summary>
        public String Specialty { get; set; }

        /// <summary>
        /// 入职日期
        /// </summary>
        public DateTime JoinTime { get; set; }
        
        /// <summary>
        /// 安置方式
        /// </summary>
        public int ResettlementWayID { get; set; }

    }
}
