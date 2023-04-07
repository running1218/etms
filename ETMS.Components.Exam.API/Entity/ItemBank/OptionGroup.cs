// File:    OptionGroup.cs
// Author:  Administrator
// Created: 2011年12月15日 9:33:46
// Purpose: Definition of Class OptionGroup

using System;

namespace ETMS.Components.Exam.API.Entity.ItemBank
{
    ///<summary>
    /// 选项组实体类。
    ///</summary>
    [Serializable]
    public class OptionGroup
    {
        ///<summary>
        /// 选项组标题ID
        ///</summary>
        public Guid OptionGroupTitleID
        {
            get;
            set;
        }
        ///<summary>
        /// 选项组标题
        /// 匹配题为A，B，C，D
        /// 归类题为用户输入的标题
        ///</summary>
        public string OptionGroupTitle
        {
            get;
            set;
        }
        ///<summary>
        /// 选/项组所在的试题ID
        ///</summary>
        public Guid QuestionID
        {
            get;
            set;
        }
    }
}
