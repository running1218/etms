// File:    GroupQuestion.cs
// Author:  Administrator
// Created: 2011��12��16�� 15:29:12
// Purpose: Definition of Class GroupQuestion

using System.Collections.Generic;

namespace ETMS.Components.Exam.API.Entity.ItemBank
{
    /// <summary>
    /// ������
    /// </summary>
    public class GroupQuestion : QuestionBase
    {
        /// <summary>
        /// ����ѡ����
        /// �����⣺�����飬ÿ����ѡ��
        /// </summary>
        public IList<OptionGroupItem> OptionGroups { get; set; }
        /// <summary>
        /// �������.
        /// </summary>
        public GroupAnswer Answer { get; set; }
        /// <summary>
        /// ���캯��
        /// </summary>
        public GroupQuestion()
        {
            this.CommonQuestion = new CommonQuestion(QuestionType.Group);
            this.OptionGroups = new List<OptionGroupItem>();
            this.Answer = new GroupAnswer();
        }
    }
}