// File:    OptionFeedback.cs
// Author:  Administrator
// Created: 2011年12月17日 11:39:17
// Purpose: Definition of Class OptionFeedback

using System;


namespace ETMS.Components.Exam.API.Entity.ItemBank
{
    /// <summary>
    /// 选项答题反馈实体
    /// </summary>
    [Serializable]
    public class OptionFeedback
    {
        /// <summary>
        /// 反馈ID
        /// </summary>
        public Guid OptionFeedbackID { get; set; }

        /// <summary>
        /// 试题ID
        /// </summary>
        public Guid QuestionID { get; set; }

        /// <summary>
        /// 选项组合
        /// </summary>
        public string Options { get; set; }

        /// <summary>
        /// 反馈内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDelete { get; set; }

        // 摘要:
        //     创建日期
        public DateTime CreatedDate { get; set; }
        //
        // 摘要:
        //     创建人
        public int CreatedUserID { get; set; }
        //
        // 摘要:
        //     最后修改日期
        public DateTime UpdatedDate { get; set; }
        //
        // 摘要:
        //     最后修改人
        public int UpdatedUserID { get; set; }
    }
}