// File:    GroupQuestion.cs
// Author:  Administrator
// Created: 2011年12月16日 15:29:12
// Purpose: Definition of Class GroupQuestion

using System.Collections.Generic;

namespace ETMS.Components.Exam.API.Entity.ItemBank
{
    /// <summary>
    /// 归类题
    /// </summary>
    public class GroupQuestion : QuestionBase
    {
        /// <summary>
        /// 试题选项组
        /// 归类题：分两组，每组多个选项
        /// </summary>
        public IList<OptionGroupItem> OptionGroups { get; set; }
        /// <summary>
        /// 归类题答案.
        /// </summary>
        public GroupAnswer Answer { get; set; }
        /// <summary>
        /// 构造函数
        /// </summary>
        public GroupQuestion()
        {
            this.CommonQuestion = new CommonQuestion(QuestionType.Group);
            this.OptionGroups = new List<OptionGroupItem>();
            this.Answer = new GroupAnswer();
        }
    }
}