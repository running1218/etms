// File:    AnswerBase.cs
// Author:  Administrator
// Created: 2011��12��15�� 17:47:25
// Purpose: Definition of Class AnswerBase

using System;
using System.Text;
using System.Web.Script.Serialization;
namespace ETMS.Components.Exam.API.Entity.ItemBank
{
    ///<summary>
    /// �����
    ///</summary>
    [Serializable]
   public abstract class AnswerBase
   {
      
      ///<summary>
      /// �𰸵��ַ�����ʾ
      ///</summary>
      public virtual string Answer
      {
         get;
         set;
      }
      
      
      ///<summary>
      /// ��������
      ///</summary>
      public QuestionType QuestionType
      {
         get;
         set;
      }

      #region --�ṩ�Դ𰸵�JSON���л��뷴���л�����--
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
       /// �����л���
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