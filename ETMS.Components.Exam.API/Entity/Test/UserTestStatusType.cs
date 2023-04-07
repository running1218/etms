// File:    UserTestStatusType.cs
// Author:  Administrator
// Created: 2011��12��15�� 14:41:18
// Purpose: Definition of Enum UserTestStatusType

using System;

namespace ETMS.Components.Exam.API.Entity.Test
{
    ///<summary>
    /// ��������״̬���͡�0:  ��ʼ״̬��1������ύ;2���ύ;3���з�
    ///</summary>
    [Serializable]
   public enum UserTestStatusType
   {
       /// <summary>
       /// ����״̬
       /// </summary>
       ALL=-1,
      ///<summary>
      /// ������δ��ʼ����
      ///</summary>
      NotStart = 0,
      ///<summary>
      /// ������(��ʼ���ԣ���δ����)
      ///</summary>
      Testing,
      ///<summary>
      /// �ѽ������Խ�����
      ///</summary>
      TestOver,
      ///<summary>
      /// ������
      ///</summary>
      Marked
   }
}