using System;
using System.Collections.Generic;
using System.Linq;


namespace ETMS.Components.Exam.API.Entity.ItemBank
{
    ///<summary>
    /// 归类题答案
    ///</summary>
    [Serializable]
    public class GroupAnswer : AnswerBase
    {
        /////<summary>
        ///// 选/项组所在的试题ID
        /////</summary>
        //public Guid QuestionID { get; set; }
        ///// <summary>
        ///// 选项组
        ///// </summary>
        //public IList<OptionGroupItem> OptionGroups { get; set; }

        /// <summary>
        /// 选项组答案
        /// </summary>
        public List<OptionGroupItemAnswer> OptionGroups { get; set; }


        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public GroupAnswer()
        {
            this.QuestionType = ItemBank.QuestionType.Group;
            this.OptionGroups = new List<OptionGroupItemAnswer>();
        }
        /// <summary>
        /// 构造函数(用json数据构造答案实体)
        /// json格式必须符合MatchAnswer规范
        /// </summary>
        public GroupAnswer(string sAnswer)
            :this()
        {
            if (string.IsNullOrEmpty(sAnswer))
            {
                return;
            }
            this.OptionGroups = AnswerBase.Deserialize<List<OptionGroupItemAnswer>>(sAnswer);
            //if (group == null)
            //    return;
            //this.QuestionID = group.QuestionID;
            //this.OptionGroups = group.OptionGroups;
            //this.QuestionType = group.QuestionType;
            this.Answer = sAnswer;
        }

        /// <summary>
        /// 用一个分组后的选项组来构造一个匹配题答案
        /// </summary>
        /// <param name="LstGroups"></param>
        public GroupAnswer(List<OptionGroupItem> LstGroups)
            :this()
        {
            if (LstGroups == null || LstGroups.Count <= 0)
                return;

            this.OptionGroups = LstGroups.Select(x => 
            {
                OptionGroupItemAnswer oAnswer = new OptionGroupItemAnswer(x);
                return oAnswer;
            }).ToList<OptionGroupItemAnswer>();
        }
        #endregion

        #region 重写ToString
        /// <summary>
        /// 将答案实体转成Json字符串
        /// </summary>
        /// <returns>Json字符串</returns>
        public override string ToString()
        {
            return AnswerBase.Serialize(this.OptionGroups);
        }
        /// <summary>
        /// 以JSON表示的试题答案字符串
        /// </summary>
        public override string Answer
        {
            get
            {
                if (this.OptionGroups == null)
                    return "";
                return AnswerBase.Serialize(this.OptionGroups);
            }
        }
        #endregion
    }

    /// <summary>
    /// 选项组答案。用于归类题与匹配题
    /// </summary>
    [Serializable]
    public class OptionGroupItemAnswer
    {
        /// <summary>
        /// 选项标题ID
        /// </summary>
        public Guid OptionGroupTitleID { get; set; }
        /// <summary>
        /// 选项标题名称
        /// </summary>
        public string OptionGroupTitle { get; set; }
        /// <summary>
        /// 其中包含的各个选项
        /// </summary>
        public List<OptionAnswer> Options { get; set; }

        public OptionGroupItemAnswer()
        {
            this.Options = new List<OptionAnswer>();
        }

        public OptionGroupItemAnswer(OptionGroupItem oGroupItem)
        {
            this.OptionGroupTitle = oGroupItem.OptionGroup.OptionGroupTitle;
            this.OptionGroupTitleID = oGroupItem.OptionGroup.OptionGroupTitleID;

            this.Options=oGroupItem.Options.Select(x =>
            {
                OptionAnswer oAnswer = new OptionAnswer() { OptionCode = x.OptionCode, OptionID = x.OptionID };
                return oAnswer;
            }).ToList<OptionAnswer>();
        }
    }
}
