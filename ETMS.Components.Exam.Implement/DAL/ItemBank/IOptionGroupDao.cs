// File:    IOptionGroupDao.cs
// Author:  Administrator
// Created: 2011年12月15日 9:54:09
// Purpose: Definition of Interface IOptionGroupDao

using System.Collections.Generic;
using ETMS.Components.Exam.API.Entity.ItemBank;

namespace ETMS.Components.Exam.Implement.DAL.ItemBank
{
    ///<summary>
    /// 对选项组进行数据访问的接口
    ///</summary>
    public interface IOptionGroupDao
   {
      ///<summary>
      /// 为试题添加一个选项组
      ///</summary>
      /// <param name="optionGroup">要添加的选项组信息</param>
      void AddOptionGroup(OptionGroup optionGroup);
      
      ///<summary>
      /// 更新指定的选项组
      ///</summary>
      /// <param name="newOptionGroup">更新以后的选项组信息。其中OptionGroupID必须已存在。</param>
      void Update(OptionGroup newOptionGroup);
      
      ///<summary>
      /// 删除指定的选项组
      ///</summary>
      /// <param name="optionGroupID">要删除的选项组ID</param>
      void Delete(System.Guid optionGroupID);
       

      ///<summary>
      /// 删除某一试题中所有选项组
      ///</summary>
      /// <param name="questionID">选项组所在试题ID</param>
      void DeleteByQuestionID(System.Guid questionID);
      
      ///<summary>
      /// 得到一个试题中所有的选项组.
      ///</summary>
      /// <param name="questionID">试题ID</param>
      IList<OptionGroup> FindOptionGroupsInQuestion(System.Guid questionID);
      
      ///<summary>
      /// 得到指定ID的选项信息
      ///</summary>
      /// <param name="optionGroupID">选项组ID</param>
      OptionGroup GetOptionGroup(System.Guid optionGroupID);
   }
}