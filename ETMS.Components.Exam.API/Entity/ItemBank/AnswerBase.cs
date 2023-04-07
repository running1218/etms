// File:    AnswerBase.cs
// Author:  Administrator
// Created: 2011年12月15日 17:47:25
// Purpose: Definition of Class AnswerBase

using System;
using System.Text;
using System.Web.Script.Serialization;
namespace ETMS.Components.Exam.API.Entity.ItemBank
{
    ///<summary>
    /// 试题答案
    ///</summary>
    [Serializable]
   public abstract class AnswerBase
   {
      
      ///<summary>
      /// 答案的字符串表示
      ///</summary>
      public virtual string Answer
      {
         get;
         set;
      }
      
      
      ///<summary>
      /// 试题类型
      ///</summary>
      public QuestionType QuestionType
      {
         get;
         set;
      }

      #region --提供对答案的JSON序列化与反序列化功能--
      public static string Serialize(object answer)
      {
          if (answer == null)
              return "";

          StringBuilder sb = new StringBuilder();
          JavaScriptSerializer MySerializer = new JavaScriptSerializer();
          MySerializer.Serialize(answer, sb);

          return sb.ToString();
      }
       /// <summary>
       /// 反序列化答案
       /// </summary>
       /// <typeparam name="T"></typeparam>
       /// <param name="sAnswer"></param>
       /// <returns></returns>
      public static T Deserialize<T>(string sAnswer)
      {
          if (string.IsNullOrEmpty(sAnswer))
              return default(T);

          T answer = default(T);
          StringBuilder sb = new StringBuilder();
          JavaScriptSerializer MySerializer = new JavaScriptSerializer();
          answer=MySerializer.Deserialize<T>(sAnswer);

          return answer;
      }
      #endregion
   }
}