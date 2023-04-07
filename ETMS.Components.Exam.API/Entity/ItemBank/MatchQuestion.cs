// File:    MatchQuestion.cs
// Author:  Administrator
// Created: 2011年12月16日 15:29:12
// Purpose: Definition of Class MatchQuestion

using System;
using System.Collections.Generic;

namespace ETMS.Components.Exam.API.Entity.ItemBank
{
    /// <summary>
    /// 匹配题
    /// </summary>
    [Serializable]
    public class MatchQuestion : QuestionBase
    {
        /// <summary>
        /// 试题选项组
        /// 匹配题：分多组，如A组 B组 C组等，每组两个选项
        /// </summary>
        public IList<OptionGroupItem> OptionGroups { get; set; }
        /// <summary>
        /// 匹配题答案
        /// </summary>
        public MatchAnswer Answer { get; set; }
        /// <summary>
        /// 构造函数
        /// </summary>
        public MatchQuestion()
        {
            this.CommonQuestion = new CommonQuestion(QuestionType.Match);
            this.OptionGroups = new List<OptionGroupItem>();
            this.Answer = new MatchAnswer();
        }
    }
}