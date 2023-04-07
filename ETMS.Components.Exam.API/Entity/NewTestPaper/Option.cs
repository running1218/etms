using System;

namespace ETMS.Components.Exam.API.Entity.NewTestPaper
{
    /// <summary>
    /// 选项实体类 add 2013-9-26 hjy
    /// </summary>
    public class Option
    {
        /// <summary>
        /// 试题ID
        /// </summary>
        public Guid QuestionID { get; set; }

        /// <summary>
        /// 选项ID
        /// </summary>
        public Guid OptionID { get; set; }

        /// <summary>
        /// 选项内容
        /// </summary>
        public string OptionContent { get; set; }

        /// <summary>
        /// 选项编号
        /// </summary>
        public string OptionCode { get; set; }
    }
}
