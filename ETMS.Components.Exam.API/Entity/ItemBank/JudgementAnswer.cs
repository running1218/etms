// File:    JudgementAnswer.cs
// Author:  Administrator
// Created: 2011年12月15日 19:10:20
// Purpose: Definition of Class JudgementAnswer

using System;

namespace ETMS.Components.Exam.API.Entity.ItemBank
{
    ///<summary>
    /// 判断题答案
    ///</summary>
    ///<remarks>
    /// 判断题是一种特殊类型的单选题，所以此处直接从单选题答案继承而来
    ///</remarks>
    [Serializable]
   public class JudgementAnswer : SingleChoiceAnswer
   {
        public JudgementAnswer(string sAnswer)
            : base(sAnswer)
        { }
       /// <summary>
       /// 创建一个判断题答案实体.
       /// </summary>
       /// <remarks>
       /// 1,前端接口请其它构造函数; <br></br>
       /// 2,用于构造试题时，将答案字符串与答案选项一起加载
       /// </remarks>
       /// <param name="sAnswer">答案的字符串表示</param>
       /// <param name="AnswerOption">答案选项</param>
       public JudgementAnswer(string sAnswer, QuestionOption AnswerOption)
           :base(sAnswer,AnswerOption)
       { }
       /// <summary>
       /// 创建一个判断题答案实体
       /// </summary>
       /// <param name="AnswerOption">答案选项</param>
       public JudgementAnswer(QuestionOption AnswerOption)
           : base(AnswerOption)
       { }
   }
}