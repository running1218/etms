// File:    IUserTPQuestionDao.cs
// Author:  Administrator
// Created: 2011��12��15�� 15:50:44
// Purpose: Definition of Interface IUserTPQuestionDao

using System.Collections.Generic;
using ETMS.Components.Exam.API.Entity.Test;

namespace ETMS.Components.Exam.Implement.DAL.Test
{
    ///<summary>
    /// �Կ����Ծ����������ݷ���
    ///</summary>
    public interface IUserTPQuestionDao
   {
      void AddTPQuestion(UserTPQuestion userTPQuestion);
      
      void Update(UserTPQuestion newUserTPQuestion);
      
      void Delete(System.Guid userTPQuestionID);
      
      ///<summary>
      /// ����ָ������ķ���
      ///</summary>
      /// <param name="answer">������</param>
      void AnswerUpdate(System.Guid userTPQuestionID, string answer);
      
      ///<summary>
      /// ����ĳһ���⿼������
      ///</summary>
      /// <param name="score">�����÷�</param>
      void ScoreUpdate(System.Guid userTPQuestionID, decimal score);
      
      ///<summary>
      /// �õ�ĳһ�Ծ�������������Ϣ
      ///</summary>
      IList<UserTPQuestion> FindAllQuestionsInUserPaper(System.Guid examID);
      
      ///<summary>
      /// ɾ��ĳһ�û��Ծ�����������
      ///</summary>
      void DeleteAllInUserPaper(System.Guid examID);
   
   }
}