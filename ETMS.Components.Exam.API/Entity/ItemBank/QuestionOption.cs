// File:    QuestionOption.cs
// Author:  Administrator
// Created: 2011年12月15日 9:38:28
// Purpose: Definition of Class QuestionOption

using System;

namespace ETMS.Components.Exam.API.Entity.ItemBank
{
    ///<summary>
    /// 试题的选项实体类
    ///</summary>
    [Serializable]
   public class QuestionOption
   {
      
      ///<summary>
      /// 选项的ID
      ///</summary>
      public System.Guid OptionID
      {
         get;
         set;
      }
      
      
      ///<summary>
      /// 选项所在的试题ID
      ///</summary>
      public System.Guid QuestionID
      {
         get;
         set;
      }
      
      
      ///<summary>
      /// 选项所在选项组ID。对于归类题、匹配题有效，其余题型可以为NULL
      ///</summary>
      public System.Guid? OptionGroupTitleID
      {
         get;
         set;
      }
      
      
      ///<summary>
      /// 选项本身的标题。诸如选择题之中A，B，C，D之类
      /// 如果是匹配题或归类题，就是数字，如1、2、3、4等
      ///</summary>
      public string OptionCode
      {
         get;
         set;
      }
      
      
      ///<summary>
      /// 选项内容。是一个富文本的串。
      ///</summary>
      public string OptionContent
      {
         get;
         set;
      }
   }
}