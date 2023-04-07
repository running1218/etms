using System;

namespace ETMS.Components.Point.API.Entity
{
    public partial class StudentPointRanking
    {
        /// <summary>
        /// 排名
        /// </summary>
        public Int64 Ranking { get; set; }
        /// <summary>
        /// 当前积分
        /// </summary>
        public Int64 CurrentTotalPoint { get; set; }
        /// <summary>
        /// 学员姓名
        /// </summary>
        public string RealName { get; set; }
        /// <summary>
        /// 学员ID
        /// </summary>
        public int StudentID { get; set; }
    }
}
