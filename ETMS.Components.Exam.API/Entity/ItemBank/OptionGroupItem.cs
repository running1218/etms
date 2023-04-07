// File:    OptionGroupItem.cs
// Author:  Administrator
// Created: 2011��12��16�� 15:38:40
// Purpose: Definition of Class OptionGroupItem

using System;
using System.Collections.Generic;

namespace ETMS.Components.Exam.API.Entity.ItemBank
{
    ///<summary>
    /// һ��������Ĺ�������а����˸�ѡ�������������Ķ��ѡ�
    ///</summary>
    [Serializable]
    public class OptionGroupItem
    {
        /// <summary>
        /// ѡ������Ϣ
        /// </summary>
        public OptionGroup OptionGroup { get; set; }
        ///<summary>
        /// �����е�ѡ��
        ///</summary>
        public IList<QuestionOption> Options
        {
            get;
            set;
        }
        /// <summary>
        /// ���캯��
        /// </summary>
        public OptionGroupItem()
        {
            this.Options = new List<QuestionOption>();
        }
    }
}