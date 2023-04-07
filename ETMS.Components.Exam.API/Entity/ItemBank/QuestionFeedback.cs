// File:    QuestionFeedback.cs
// Author:  Administrator
// Created: 2011��12��17�� 11:39:17
// Purpose: Definition of Class QuestionFeedback

using System;


namespace ETMS.Components.Exam.API.Entity.ItemBank
{
    /// <summary>
    /// ������ⷴ��ʵ��
    /// </summary>
    [Serializable]
    public class QuestionFeedback 
    {
        /// <summary>
        /// ����ID
        /// </summary>
        public Guid FeedbackID { get; set; }

        /// <summary>
        /// ����ID
        /// </summary>
        public Guid QuestionID { get; set; }

        /// <summary>
        /// ��ȷ��������
        /// </summary>
        public string RightContent { get; set; }

        /// <summary>
        /// ����������
        /// </summary>
        public string WrongContent { get; set; }

        /// <summary>
        /// �Ƿ�ɾ��
        /// </summary>
        public bool IsDelete { get; set; }

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