namespace ETMS.Components.Basic.API.Entity.Security
{
    public class Student : User
    {
        /// <summary>
        /// 工号
        /// </summary>
        public string WorkerNo { get; set; }

        /// <summary>
        /// 职级
        /// </summary>
        public int RankID { get; set; }

        /// <summary>
        /// 岗位
        /// </summary>
        public int PostID { get; set; }   
    }
}
