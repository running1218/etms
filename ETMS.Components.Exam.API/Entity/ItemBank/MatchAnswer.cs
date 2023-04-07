using System;
using System.Collections.Generic;


namespace ETMS.Components.Exam.API.Entity.ItemBank
{
    ///<summary>
    /// 匹配题答案
    ///</summary>
    [Serializable]
    public class MatchAnswer : GroupAnswer
    {
        /////<summary>
        ///// 选/项组所在的试题ID
        /////</summary>
        //public Guid QuestionID { get; set; }
        ///// <summary>
        ///// 选项组
        ///// </summary>
        //public IList<OptionGroupItem> OptionGroups { get; set; }
        
        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public MatchAnswer()
            :base()
        {
            this.QuestionType = ItemBank.QuestionType.Match;
            //this.OptionGroups = new List<OptionGroupItem>();
        }
        /// <summary>
        /// 构造函数(用json数据构造答案实体)
        /// json格式必须符合MatchAnswer规范
        /// </summary>
        public MatchAnswer(string sAnswer)
            :base (sAnswer)
        {
            //var match = AnswerBase.Deserialize<MatchAnswer>(json);
            //this.QuestionID = match.QuestionID;
            //this.OptionGroups = match.OptionGroups;
            this.QuestionType = ItemBank.QuestionType.Match;
            //this.Answer = json;
        }
        public MatchAnswer(List<OptionGroupItem> LstGroups)
            : base(LstGroups)
        {
            this.QuestionType = ItemBank.QuestionType.Match;
        }
        #endregion

        //#region 重写ToString
        ///// <summary>
        ///// 将答案实体转成Json字符串
        ///// </summary>
        ///// <returns>Json字符串</returns>
        //public override string ToString()
        //{
        //    return AnswerBase.Serialize(this);
        //}
        //#endregion
    }

    ///// <summary>
    ///// 选项分组试题（匹配题，归类题公用）
    ///// 现在用OptionGroupItem代替AnswerOptionGroup，注释掉AnswerOptionGroup
    ///// </summary>
    //public class AnswerOptionGroup
    //{
    //    /// <summary>
    //    /// 分组ID
    //    /// </summary>
    //    public Guid OptionGroupTitleID { get; set; }
    //    /// <summary>
    //    /// 选项ID
    //    /// </summary>
    //    public IList<Guid> OptionIDs { get; set; }
    //    /// <summary>
    //    /// 构造函数
    //    /// </summary>
    //    public AnswerOptionGroup()
    //    {
    //        this.OptionIDs = new List<Guid>();
    //    }
    //}
}
