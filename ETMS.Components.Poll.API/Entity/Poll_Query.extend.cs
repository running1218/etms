namespace ETMS.Components.Poll.API.Entity
{
    public partial class Poll_Query
    {
        /// <summary>
        /// 调查人数
        /// </summary>
        public int InvestNumber { get; set; }
        /// <summary>
        /// 提交人数
        /// </summary>
        public int SubmitNumber { get; set; }
        /// <summary>
        /// 为提交人数
        /// </summary>
        public string UnSubmitNumber { get; set; }
    }
}
