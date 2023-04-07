// File:    TestFeedback.cs
// Author:  Administrator
// Created: 2011��12��17�� 11:45:25
// Purpose: Definition of Class TestFeedback

using System;


namespace ETMS.Components.Exam.API.Entity.Test
{
    ///<summary>
    /// �Ծ�����
    ///</summary>
    [Serializable]
   public class TestFeedback 
   {
       /// <summary>
       /// ����ID
       /// </summary>
       public Guid FeedbackID { get; set; }

       /// <summary>
       /// �Ծ���ID
       /// </summary>
       public Guid TestPaperID { get; set; }

       /// <summary>
       /// ��ֵ��ʼֵ
       /// </summary>
       public Decimal BeginScore { get; set; }

       /// <summary>
       /// ��ֵ����ֵ
       /// </summary>
       public Decimal EndScore { get; set; }

       /// <summary>
       /// ��������
       /// </summary>
       public String Content { get; set; }

       /// <summary>
       /// �Ƿ�ɾ��
       /// </summary>
       public bool IsDelete { get; set; }

       public TestFeedback() { }
       /// <summary>
       /// ���캯��
       /// </summary>
       /// <param name="testPaperID"></param>
       public TestFeedback(Guid testPaperID)
       {
            this.TestPaperID = testPaperID;
       }

       // ժҪ:
       //     ��������
       public DateTime CreatedDate { get; set; }
       //
       // ժҪ:
       //     ������
       public int CreatedUserID { get; set; }
       //
       // ժҪ:
       //     ����޸�����
       public DateTime UpdatedDate { get; set; }
       //
       // ժҪ:
       //     ����޸���
       public int UpdatedUserID { get; set; }
   }
}