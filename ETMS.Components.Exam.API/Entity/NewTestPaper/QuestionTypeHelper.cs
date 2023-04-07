using System;
using System.Collections.Generic;

namespace ETMS.Components.Exam.API.Entity.NewTestPaper
{
    ///<summary>
    /// 试卷类型。0空(作为查询条件时为全部);1单选题;2多选题;3填空;4判断;5问答;6匹配;7归类
    ///</summary>
    public enum QuestionType
    {
        /// <summary>
        /// 空（作为查询条件时为全部）
        /// </summary>
        Null = 0,
        ///<summary>
        /// 1单选题;
        ///</summary>
        SingleChoice = 1,
        ///<summary>
        /// 2多选题;
        ///</summary>
        MultipleChoice = 2,
        ///<summary>
        /// 3填空;
        ///</summary>
        TextEntry = 3,
        ///<summary>
        /// 4判断;
        ///</summary>
        Judgement = 4,
        ///<summary>
        /// 5问答;
        ///</summary>
        ExtendedText,
        ///<summary>
        /// 6匹配;
        ///</summary>
        Match,
        ///<summary>
        /// 7归类
        ///</summary>
        Group
    }
    public class QuestionTypeHelper
    {
        private static Dictionary<QuestionType, string> QuestionTypeNames = new Dictionary<QuestionType, string>() 
       { 
        {QuestionType.SingleChoice,"单选题"},
        {QuestionType.MultipleChoice,"多选题"},
        {QuestionType.Judgement,"判断题"},
        {QuestionType.TextEntry,"填空题"},
        {QuestionType.ExtendedText,"问答题"},
        {QuestionType.Group,"归类题"},
        {QuestionType.Match,"匹配题"},
       };
        /// <summary>
        /// 得到指定试题类型的试题的描述
        /// </summary>
        /// <param name="oType"></param>
        /// <returns></returns>
        public static string GetQuestionTypeName(QuestionType oType)
        {
            if (QuestionTypeNames.ContainsKey(oType))
            {
                return QuestionTypeNames[oType];
            }

            return "";
        }
    }

    public class UserAnswer
    {
        public string OptionCode { get; set; }

        public Guid OptionID { get; set; }
    }

    public class UserTestPaper
    {
        public string TestPaperID { get; set; }

        public string TestPaperName { get; set; }

        public decimal TestPaperScore { get; set; }

        public decimal ExamScore { get; set; }

        public int IsShowAnswer { get; set; }
    }
}
