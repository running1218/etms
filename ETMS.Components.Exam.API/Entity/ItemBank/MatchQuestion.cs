// File:    MatchQuestion.cs
// Author:  Administrator
// Created: 2011��12��16�� 15:29:12
// Purpose: Definition of Class MatchQuestion

using System;
using System.Collections.Generic;

namespace ETMS.Components.Exam.API.Entity.ItemBank
{
    /// <summary>
    /// ƥ����
    /// </summary>
    [Serializable]
    public class MatchQuestion : QuestionBase
    {
        /// <summary>
        /// ����ѡ����
        /// ƥ���⣺�ֶ��飬��A�� B�� C��ȣ�ÿ������ѡ��
        /// </summary>
        public IList<OptionGroupItem> OptionGroups { get; set; }
        /// <summary>
        /// ƥ�����
        /// </summary>
        public MatchAnswer Answer { get; set; }
        /// <summary>
        /// ���캯��
        /// </summary>
        public MatchQuestion()
        {
            this.CommonQuestion = new CommonQuestion(QuestionType.Match);
            this.OptionGroups = new List<OptionGroupItem>();
            this.Answer = new MatchAnswer();
        }
    }
}