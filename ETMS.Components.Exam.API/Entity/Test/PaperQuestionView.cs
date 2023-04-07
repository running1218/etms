// File:    PaperQuestionView.cs
// Author:  Administrator
// Created: 2012年1月12日 17:45:19
// Purpose: Definition of Class PaperQuestionView

using System;

namespace ETMS.Components.Exam.API.Entity.Test
{
    ///<summary>
    /// 试卷中试题的浏览显示信息
    ///</summary>
    [Serializable]
    public class PaperQuestionView : PaperQuestion
    {
        ///<summary>
        /// 试题题面信息
        ///</summary>
        public string QuestionTitle { get; set; }
        /// <summary>
        /// 试题难度
        /// </summary>
        public short Difficulty { get; set; }
        ///// <summary>
        ///// 适应对象ID(0空|全部；1幼儿教育；2初等教育；3中等教育；4中等职业教育；5高等教育；6高等职业教育；7继续教育；8职业培训)
        ///// 通过Autumn.Business.LMS.ServiceRepository.DictionaryService.GetAllItems(EnumBizDictionary.dic_Object)取得字典数据
        ///// </summary>
        //public short ObjectID { get; set; }
        ///// <summary>
        ///// 所属学科
        ///// </summary>
        //public string Subject { get; set; }
        ///// <summary>
        ///// 试题分类名称
        ///// </summary>
        //public string QuestionBankName { get; set; }
    }
}