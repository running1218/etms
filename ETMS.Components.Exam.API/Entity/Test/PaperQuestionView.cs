// File:    PaperQuestionView.cs
// Author:  Administrator
// Created: 2012��1��12�� 17:45:19
// Purpose: Definition of Class PaperQuestionView

using System;

namespace ETMS.Components.Exam.API.Entity.Test
{
    ///<summary>
    /// �Ծ�������������ʾ��Ϣ
    ///</summary>
    [Serializable]
    public class PaperQuestionView : PaperQuestion
    {
        ///<summary>
        /// ����������Ϣ
        ///</summary>
        public string QuestionTitle { get; set; }
        /// <summary>
        /// �����Ѷ�
        /// </summary>
        public short Difficulty { get; set; }
        ///// <summary>
        ///// ��Ӧ����ID(0��|ȫ����1�׶�������2���Ƚ�����3�еȽ�����4�е�ְҵ������5�ߵȽ�����6�ߵ�ְҵ������7����������8ְҵ��ѵ)
        ///// ͨ��Autumn.Business.LMS.ServiceRepository.DictionaryService.GetAllItems(EnumBizDictionary.dic_Object)ȡ���ֵ�����
        ///// </summary>
        //public short ObjectID { get; set; }
        ///// <summary>
        ///// ����ѧ��
        ///// </summary>
        //public string Subject { get; set; }
        ///// <summary>
        ///// �����������
        ///// </summary>
        //public string QuestionBankName { get; set; }
    }
}