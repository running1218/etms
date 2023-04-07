// File:    OptionGroupItem.cs
// Author:  Administrator
// Created: 2011年12月16日 15:38:40
// Purpose: Definition of Class OptionGroupItem

using System;
using System.Collections.Generic;

namespace ETMS.Components.Exam.API.Entity.ItemBank
{
    ///<summary>
    /// 一个归类题的归类项，其中包括了该选项组中所包含的多个选项。
    ///</summary>
    [Serializable]
    public class OptionGroupItem
    {
        /// <summary>
        /// 选项组信息
        /// </summary>
        public OptionGroup OptionGroup { get; set; }
        ///<summary>
        /// 所具有的选项
        ///</summary>
        public IList<QuestionOption> Options
        {
            get;
            set;
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        public OptionGroupItem()
        {
            this.Options = new List<QuestionOption>();
        }
    }
}