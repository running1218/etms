// File:    UserTestStatusType.cs
// Author:  Administrator
// Created: 2011年12月15日 14:41:18
// Purpose: Definition of Enum UserTestStatusType

using System;

namespace ETMS.Components.Exam.API.Entity.Test
{
    ///<summary>
    /// 考生考试状态类型。0:  初始状态，1保存待提交;2已提交;3已判分
    ///</summary>
    [Serializable]
   public enum UserTestStatusType
   {
       /// <summary>
       /// 所有状态
       /// </summary>
       ALL=-1,
      ///<summary>
      /// 考生尚未开始测试
      ///</summary>
      NotStart = 0,
      ///<summary>
      /// 考试中(开始考试，尚未交卷)
      ///</summary>
      Testing,
      ///<summary>
      /// 已交卷，考试结束。
      ///</summary>
      TestOver,
      ///<summary>
      /// 已评分
      ///</summary>
      Marked
   }
}