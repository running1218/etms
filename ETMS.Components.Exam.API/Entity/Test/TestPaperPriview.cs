using System;
using System.Collections.Generic;
using ETMS.Components.Exam.API.Entity.ItemBank;


namespace ETMS.Components.Exam.API.Entity.Test
{
    /// <summary>
    /// 试卷预览实体
    /// </summary>
    [Serializable]
    public class TestPaperPriview
    {

    }
    /// <summary>
    /// 试卷单元
    /// </summary>
    [Serializable]
    public class TestPaperUnit
    {
        ///<summary>
        /// 试题类型
        ///</summary>
        public QuestionType QuestionType { get; set; }

        ///<summary>
        /// 该试题个数
        ///</summary>
        public int QuestionCount { get; set; }
        
        /// <summary>
        /// 这部分试题总分
        /// </summary>
        public decimal QuestionScoreSum { get; set; }
        
        /// <summary>
        /// 这部分下的试题列表
        /// </summary>
        public IList<BasePriview> QuestionList { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public TestPaperUnit()
        {
            this.QuestionType = ItemBank.QuestionType.Null;
            this.QuestionList = new List<BasePriview>();
        }
    }

    /// <summary>
    /// 试题预览实体
    /// </summary>
    [Serializable]
    public class BasePriview
    {
        /// <summary>
        /// 试题ID
        /// </summary>
        public Guid QuestionID { get; set; }
        /// <summary>
        /// 试题题面
        /// </summary>
        public string QuestionName { get; set; }
        /// <summary>
        /// 试题分数
        /// </summary>
        public decimal QuestionScore { get; set; }
        /// <summary>
        /// 试题选项
        /// </summary>
        public IList<QuestionOption> QuestionOptions { get; set; }
        /// <summary>
        /// 试题选项组
        /// </summary>
        public IList<OptionGroupItem> OptionGroups { get; set; }
        /// <summary>
        /// 构造函数
        /// </summary>
        public BasePriview()
        {
            this.QuestionOptions = new List<QuestionOption>();
            this.OptionGroups = new List<OptionGroupItem>();
        }
    }
}
