// File:    IOptionGroupDao.cs
// Author:  Administrator
// Created: 2011��12��15�� 9:54:09
// Purpose: Definition of Interface IOptionGroupDao

using System.Collections.Generic;
using ETMS.Components.Exam.API.Entity.ItemBank;

namespace ETMS.Components.Exam.Implement.DAL.ItemBank
{
    ///<summary>
    /// ��ѡ����������ݷ��ʵĽӿ�
    ///</summary>
    public interface IOptionGroupDao
   {
      ///<summary>
      /// Ϊ�������һ��ѡ����
      ///</summary>
      /// <param name="optionGroup">Ҫ��ӵ�ѡ������Ϣ</param>
      void AddOptionGroup(OptionGroup optionGroup);
      
      ///<summary>
      /// ����ָ����ѡ����
      ///</summary>
      /// <param name="newOptionGroup">�����Ժ��ѡ������Ϣ������OptionGroupID�����Ѵ��ڡ�</param>
      void Update(OptionGroup newOptionGroup);
      
      ///<summary>
      /// ɾ��ָ����ѡ����
      ///</summary>
      /// <param name="optionGroupID">Ҫɾ����ѡ����ID</param>
      void Delete(System.Guid optionGroupID);
       

      ///<summary>
      /// ɾ��ĳһ����������ѡ����
      ///</summary>
      /// <param name="questionID">ѡ������������ID</param>
      void DeleteByQuestionID(System.Guid questionID);
      
      ///<summary>
      /// �õ�һ�����������е�ѡ����.
      ///</summary>
      /// <param name="questionID">����ID</param>
      IList<OptionGroup> FindOptionGroupsInQuestion(System.Guid questionID);
      
      ///<summary>
      /// �õ�ָ��ID��ѡ����Ϣ
      ///</summary>
      /// <param name="optionGroupID">ѡ����ID</param>
      OptionGroup GetOptionGroup(System.Guid optionGroupID);
   }
}