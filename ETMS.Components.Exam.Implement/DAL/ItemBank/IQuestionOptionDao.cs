// File:    IQuestionOptionDao.cs
// Author:  Administrator
// Created: 2011��12��15�� 9:54:10
// Purpose: Definition of Interface IQuestionOptionDao

using System;
using System.Collections.Generic;
using ETMS.Components.Exam.API.Entity.ItemBank;

namespace ETMS.Components.Exam.Implement.DAL.ItemBank
{
   ///<summary>
   /// ������ѡ������ݷ��ʵĽӿ�
   ///</summary>
   public interface IQuestionOptionDao
   {
      ///<summary>
      /// ���һ������ѡ��
      ///</summary>
      ///<param name="questionOption">Ҫ��ӵ�����ѡ��</param>
      void AddOption(QuestionOption questionOption);
      /// <summary>
      /// �����Ѵ��ڵ�����ѡ����Ϣ
      /// </summary>
      /// <param name="newQuestionOption">���µ�����ѡ��</param>
      void Update(QuestionOption newQuestionOption);
      /// <summary>
      /// ɾ��ָ��ID������ѡ��
      /// </summary>
      /// <param name="questionOptionID">����ѡ��ID</param>
      void Delete(System.Guid questionOptionID);
      /// <summary>
      /// ɾ��ָ�������ȫ������ѡ��
      /// </summary>
      /// <param name="questionID">����ID</param>
      void DeleteByQuestionID(System.Guid questionID);
      /// <summary>
      /// ɾ��ĳһ�����ж��ѡ��
      /// </summary>
      /// <param name="questionID">ѡ����������ID</param>
      /// <param name="LstOptionsID">Ҫɾ����ѡ��ID�б�</param>
      void DeleteByOptionsID(System.Guid questionID, IList<System.Guid> LstOptionsID);
      /// <summary>
      /// ɾ��ĳһ������ָ��ѡ�����ѡ��
      /// </summary>
      /// <param name="questionID">ѡ����������ID</param>
      /// <param name="optionGroupID">ѡ������ѡ����</param>
      void DeleteByGroupID(System.Guid questionID, System.Guid optionGroupID);
      /// <summary>
      /// �õ�ָ��ѡ��ID������ѡ��
      /// </summary>
      /// <param name="questionOptionID">����ѡ��ID</param>
      /// <returns></returns>
      QuestionOption GetQuestionOption(System.Guid questionOptionID);
      /// <summary>
      /// �õ�ָ�����������ѡ����Ϣ
      /// </summary>
      /// <param name="questionID">����ID</param>
      /// <returns></returns>
      IList<QuestionOption> FindQuestionOptionsInQuestion(Guid questionID);
      
      ///<summary>
      /// �õ�һ��ѡ�����е�����ѡ�
      ///</summary>
      ///<param name="questionID">ѡ����������ID</param>
      ///<param name="optionGroupID">ѡ����ID</param>
      ///<returns></returns>
      IList<QuestionOption> FindQuestionOptionsInGroup(System.Guid questionID, System.Guid optionGroupID);
   
   }
}