// File:    StudentTestView.cs
// Author:  Administrator
// Created: 2012��1��13�� 11:31:00
// Purpose: Definition of Class StudentTestView

using System;

namespace ETMS.Components.Exam.API.Entity.Test
{
    ///<summary>
    /// ����������Ϣ������������������Ϣ���Ծ������Ϣ�����Ե�״̬�����Է���
    ///</summary>
    ///
    [Serializable]
   public class StudentTestView
   {
       public int UserID { get; set; }
       public string UserName { get; set; }
       public string FirstName { get; set; }
       public string LastName { get; set; }
       public string Photo { get; set; }

       /// <summary>
       /// ���õ��Ծ���ID
       /// </summary>
       public Guid TestPaperID { get; set; }
       /// <summary>
       /// �Ծ������
       /// </summary>
       public string TestPaperName { get; set; }
       /// <summary>
       /// �Ծ�����
       /// </summary>
       public TestPaperType TestPaperType { get; set; }

       /// <summary>
       /// ����ĳһ�ο���ID
       /// </summary>
       public Guid UserExamID { get; set; }
       /// <summary>
       /// ��ʼ����ʱ��
       /// </summary>
       public DateTime StartExamTime { get; set; }
       /// <summary>
       /// ��������ʱ��
       /// </summary>
       public DateTime EndExamTime { get; set; }

       /// <summary>
       /// ��������״̬
       /// </summary>
       public UserTestStatusType TestStatus { get; set; }
       /// <summary>
       /// �������Է���
       /// </summary>
       public decimal UserScore { get; set; }
       /// <summary>
       /// �Ծ��ܷ�
       /// </summary>
       public decimal PaperScore { get; set; }
   }
}