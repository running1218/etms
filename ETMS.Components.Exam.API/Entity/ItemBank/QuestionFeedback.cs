// File:    QuestionFeedback.cs
// Author:  Administrator
// Created: 2011年12月17日 11:39:17
// Purpose: Definition of Class QuestionFeedback

using System;


namespace ETMS.Components.Exam.API.Entity.ItemBank
{
    /// <summary>
    /// 试题答题反馈实体
    /// </summary>
    [Serializable]
    public class QuestionFeedback 
    {
        /// <summary>
        /// 反馈ID
        /// </summary>
        public Guid FeedbackID { get; set; }

        /// <summary>
        /// 试题ID
        /// </summary>
        public Guid QuestionID { get; set; }

        /// <summary>
        /// 正确反馈内容
        /// </summary>
        public string RightContent { get; set; }

        /// <summary>
        /// 错误反馈内容
        /// </summary>
        public string WrongContent { get; set; }

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