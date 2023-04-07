// File:    TextEntryAnswer.cs
// Author:  Administrator
// Created: 2011年12月15日 17:49:06
// Purpose: Definition of Class TextEntryAnswer

using System;
using System.Collections.Generic;


namespace ETMS.Components.Exam.API.Entity.ItemBank
{
   ///<summary>
   /// 填空题答案
   ///</summary>
   [Serializable]
   public class TextEntryAnswer : AnswerBase
   {
       //序列化后的答案
       private string answer;

       //答案实体类集合
       private IList<TextEntry> answerList;

       /// <summary>
       /// 构造函数
       /// </summary>
       /// <param name="answers">答案类集合</param>
       public TextEntryAnswer(IList<TextEntry> answers)
       {
           if(answers==null)
             throw new ETMS.AppContext.BusinessException("答案不能为null");
           else if(answers.Count==0)
               throw new ETMS.AppContext.BusinessException("答案不能为Empty");

           this.answerList = answers;
           this.answer = AnswerBase.Serialize(answers);
       }
       /// <summary>
       /// 构造函数
       /// </summary>
       /// <param name="answers">Json化字串</param>
       public TextEntryAnswer(string answers)
       {
           //if(String.IsNullOrEmpty(answers))
           //    throw new ETMS.AppContext.BusinessException("答案不能为null");

           this.answerList = AnswerBase.Deserialize<IList<TextEntry>>(answers);
           this.answer = answers;
       }


       ///<summary>
       /// 答案的Json字串
       ///</summary>
       public override string Answer
       {
           get { return this.answer; }
       }

       /// <summary>
       /// 返回答案的实体类集合
       /// </summary>
       public IList<TextEntry> AnswerList {
           get { return this.answerList; }
       }
   }

   /// <summary>
   /// 填空题答案
   /// </summary>
   public class TextEntry
   {
       /// <summary>
       /// 填空项里的值
       /// </summary>
       public string EntryValue { get; set; }

       /// <summary>
       /// 填空项里的编号
       /// </summary>
       public string EntryID { get; set; }
   }
}