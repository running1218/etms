// File:    IUserTestPaperDao.cs
// Author:  Administrator
// Created: 2011��12��15�� 15:27:10
// Purpose: Definition of Interface IUserTestPaperDao

using System.Collections.Generic;
using ETMS.Components.Exam.API.Entity.Test;

namespace ETMS.Components.Exam.Implement.DAL.Test
{
    ///<summary>
    /// �Կ����Ծ���Ϣ�����ݷ��ʵĽӿ�
    ///</summary>
    public interface IUserTestPaperDao
   {
      ///<summary>
      /// ���һ�����������Ծ�
      ///</summary>
      void AddUserTestPaper(UserTestPaper userTestPaper);
      
      ///<summary>
      /// ���¿��������Ծ�
      ///</summary>
      void Update(UserTestPaper newUserTestPaper);
      
      ///<summary>
      /// ɾ��ָ���Ŀ����Ծ�
      ///</summary>
      void Delete(System.Guid userTestPaperID);
      
      ///<summary>
      /// ���������ķ���
      ///</summary>
      /// <param name="score">�����ķ�����</param>
      void AdjustScore(System.Guid adjustUserID, System.Guid userTestPaperID, decimal score);
      
      ///<summary>
      /// ��ʼ����
      ///</summary>
      void StartTest(System.Guid userTestPaperID);
      
      ///<summary>
      /// ���濼��ʱ��
      ///</summary>
      /// <param name="testTime">������ʹ�õ�ʱ�䣨��λ���룩</param>
      void SaveTestTime(System.Guid userTestPaperID, int testTime);
      
      ///<summary>
      /// �ύ�Ծ�
      ///</summary>
      /// <param name="testTime">������ʹ�õ�ʱ�䣨��λ���룩</param>
      void SubmitTestPaper(System.Guid userTestPaperID, int testTime);
      
      ///<summary>
      /// �õ�ĳһ����ĳһ�Ծ��Ե��Ծ���Ϣ
      ///</summary>
      /// <param name="testPaperID">�Ծ�ID</param>
      IList<UserTestPaper> FindTestPaperForUserAndPaperID(int userID, System.Guid testPaperID);
   
   }
}