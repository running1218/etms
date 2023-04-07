using System;

namespace ETMS.Components.Exam.API.Entity.ItemBank
{
    /// <summary>
    /// 试题扩展实体
    /// </summary>
    [Serializable]
    public class QuestionExtend
    {
        /// <summary>
        /// 试题ID
        /// </summary>
        public Guid QuestionID { get; set; }

        /// <summary>
        /// 解题思路
        /// </summary>
        public string Solution { get; set; }
    }
}
