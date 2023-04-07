// File:    OptionGroup.cs
// Author:  Administrator
// Created: 2011��12��15�� 9:33:46
// Purpose: Definition of Class OptionGroup

using System;

namespace ETMS.Components.Exam.API.Entity.ItemBank
{
    ///<summary>
    /// ѡ����ʵ���ࡣ
    ///</summary>
    [Serializable]
    public class OptionGroup
    {
        ///<summary>
        /// ѡ�������ID
        ///</summary>
        public Guid OptionGroupTitleID
        {
            get;
            set;
        }
        ///<summary>
        /// ѡ�������
        /// ƥ����ΪA��B��C��D
        /// ������Ϊ�û�����ı���
        ///</summary>
        public string OptionGroupTitle
        {
            get;
            set;
        }
        ///<summary>
        /// ѡ/�������ڵ�����ID
        ///</summary>
        public Guid QuestionID
        {
            get;
            set;
        }
    }
}
