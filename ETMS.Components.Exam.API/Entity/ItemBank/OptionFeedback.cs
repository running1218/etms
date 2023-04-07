// File:    OptionFeedback.cs
// Author:  Administrator
// Created: 2011��12��17�� 11:39:17
// Purpose: Definition of Class OptionFeedback

using System;


namespace ETMS.Components.Exam.API.Entity.ItemBank
{
    /// <summary>
    /// ѡ����ⷴ��ʵ��
    /// </summary>
    [Serializable]
    public class OptionFeedback
    {
        /// <summary>
        /// ����ID
        /// </summary>
        public Guid OptionFeedbackID { get; set; }

        /// <summary>
        /// ����ID
        /// </summary>
        public Guid QuestionID { get; set; }

        /// <summary>
        /// ѡ�����
        /// </summary>
        public string Options { get; set; }

        /// <summary>
        /// ��������
        /// </summary>
        public string Content { get; set; }

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