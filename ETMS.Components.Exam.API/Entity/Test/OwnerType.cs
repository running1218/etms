// File:    OwnerType.cs
// Author:  Administrator
// Created: 2012年1月12日 14:47:16
// Purpose: Definition of Enum OwnerType

using System;

namespace ETMS.Components.Exam.API.Entity.Test
{
    ///<summary>
    /// 拥有者类型,用于题库分类信息、试卷分类信息等
    ///</summary>
    [Serializable]
   public enum OwnerType
   {
      ///<summary>
      /// 个人
      ///</summary>
      Personal = 1,
      ///<summary>
      /// 机构
      ///</summary>
      Organ
   }
}