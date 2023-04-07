// File:    QuestionType.cs
// Author:  Administrator
// Created: 2011��12��15�� 15:16:39
// Purpose: Definition of Enum QuestionType

using System.Collections.Generic;

namespace ETMS.Components.Exam.API.Entity.ItemBank
{
    ///<summary>
    /// �Ծ����͡�0��(��Ϊ��ѯ����ʱΪȫ��);1��ѡ��;2��ѡ��;3���;4�ж�;5�ʴ�;6ƥ��;7����
    ///</summary>
    public enum QuestionType 
    {
        /// <summary>
        /// �գ���Ϊ��ѯ����ʱΪȫ����
        /// </summary>
        Null = 0,
        ///<summary>
        /// 1��ѡ��;
        ///</summary>
        SingleChoice = 1,
        ///<summary>
        /// 2��ѡ��;
        ///</summary>
        MultipleChoice = 2,
        ///<summary>
        /// 3���;
        ///</summary>
        TextEntry = 3,
        ///<summary>
        /// 4�ж�;
        ///</summary>
        Judgement = 4,
        ///<summary>
        /// 5�ʴ�;
        ///</summary>
        ExtendedText,
        ///<summary>
        /// 6ƥ��;
        ///</summary>
        Match,
        ///<summary>
        /// 7����
        ///</summary>
        Group
    }
    /// <summary>
    /// �������Ͱ�����
    /// </summary>
    public class QuestionTypeHelper
    {
        private static Dictionary<QuestionType, string> QuestionTypeNames = new Dictionary<QuestionType, string>() 
       { 
        {QuestionType.SingleChoice,"��ѡ��"},
        {QuestionType.MultipleChoice,"��ѡ��"},
        {QuestionType.Judgement,"�ж���"},
        {QuestionType.TextEntry,"�����"},
        {QuestionType.ExtendedText,"�ʴ���"},
        {QuestionType.Group,"������"},
        {QuestionType.Match,"ƥ����"},
       };
        /// <summary>
        /// �õ�ָ���������͵����������
        /// </summary>
        /// <param name="oType"></param>
        /// <returns></returns>
        public static string GetQuestionTypeName(QuestionType oType)
        {
            if (QuestionTypeNames.ContainsKey(oType))
            {
                return QuestionTypeNames[oType];
            }

            return "";
        }
    }
}