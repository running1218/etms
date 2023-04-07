// File:    SingleChoiceAnswer.cs
// Author:  Administrator
// Created: 2011��12��15�� 17:49:05
// Purpose: Definition of Class SingleChoiceAnswer

using System;

namespace ETMS.Components.Exam.API.Entity.ItemBank
{
    ///<summary>
    /// ��ѡ�����
    ///</summary>
    [Serializable]
   public class SingleChoiceAnswer : AnswerBase
   {
       private string _Answer = "";
       private QuestionOption _AnswerOption = null;
       private OptionAnswer _OptionAnswer = null;

       #region --���캯��--
       public SingleChoiceAnswer(string sAnswer)
       {
           this._Answer = sAnswer;
           this._OptionAnswer = AnswerBase.Deserialize<OptionAnswer>(sAnswer);
           this.InitAnswerOption();
       }
       /// <summary>
       /// ��������Ĵ���Ϣ���ɶ�Ӧ������ѡ����Ϣ
       /// </summary>
       private void InitAnswerOption()
       {
           if (this._OptionAnswer == null)
           {
               this._AnswerOption = null;
               return;
           }
           this._AnswerOption = new QuestionOption(){OptionCode = this._OptionAnswer.OptionCode, OptionID =this._OptionAnswer.OptionID};
       }
       /// <summary>
       /// ����һ��ѡ�����ѡ��
       /// </summary>
       /// <param name="AnswerOption">��ѡ��</param>
       public SingleChoiceAnswer(QuestionOption AnswerOption)
       {
           this._AnswerOption = AnswerOption;
           this.Init();
       }
       /// <summary>
       /// ����һ����ѡ���ʵ��.
       /// </summary>
       /// <remarks>
       /// 1,ǰ�˽ӿ����������캯��; <br></br>
       /// 2,���ڹ�������ʱ�������ַ������ѡ��һ�����
       /// </remarks>
       /// <param name="sAnswer">�𰸵��ַ�����ʾ</param>
       /// <param name="AnswerOption">��ѡ��</param>
       public SingleChoiceAnswer(string sAnswer, QuestionOption AnswerOption)
       {
           int nValid = 0;
           nValid = ValidAnswerStringObject(sAnswer, AnswerOption);
           if (nValid != 0)
           {
               throw new Exception("���ַ������ѡ�һ��");
           }

           this._Answer = sAnswer;
           this._AnswerOption = AnswerOption;
           this.Init();
       }

       /// <summary>
       /// �Դ��е��ַ�������ѡ�������жԱȿ��Ƿ�һ��
       /// </summary>
       /// <param name="sAnswer"></param>
       /// <param name="AnswerOption"></param>
       /// <returns></returns>
       private int ValidAnswerStringObject(string sAnswer, QuestionOption AnswerOption)
       {
           if (string.IsNullOrEmpty(sAnswer) && AnswerOption == null)
               return 0;
           if (!string.IsNullOrEmpty(sAnswer) && AnswerOption != null)
           {
               OptionAnswer optionAnswer =AnswerBase.Deserialize<OptionAnswer>(sAnswer);
               if (optionAnswer.OptionCode == AnswerOption.OptionCode && optionAnswer.OptionID == AnswerOption.OptionID)
               {
                   return 0;
               }
               else
               {
                   //�ַ�������ѡ�һ��
                   return 2;
               }
           }
           else
           {
               //��һ�£�һ��Ϊ�գ�һ����Ϊ��
               return 1;
           }
       }

       /// <summary>
       /// ���ݴ�ѡ����ɴ𰸵�JSON�ַ����Ͷ���
       /// </summary>
       private void Init()
       {
           if (this._AnswerOption == null)
           {
               this._Answer = "";
               this._OptionAnswer = null;
               return;
           }

           OptionAnswer optionAnswer = new OptionAnswer()
           {
               OptionCode = this._AnswerOption.OptionCode,
               OptionID = this._AnswerOption.OptionID
           };
           this._OptionAnswer = optionAnswer;
           this._Answer = AnswerBase.Serialize(optionAnswer);
       }
       #endregion

       #region --��������--
       ///<summary>
       /// ѡ����Ĵ�ѡ��
       ///</summary>
       public QuestionOption AnswerOption
       {
           get { return this._AnswerOption; }
           set 
           { 
               this._AnswerOption = value;
               this.Init();
           }
       }
       #endregion

       #region --��������--
       /// <summary>
       /// JSON�ַ�������
       /// </summary>
       public OptionAnswer OptionAnswer 
       { 
           get 
           {
               if (this._AnswerOption == null)
               {
                   return null;
               }

               OptionAnswer optionAnswer = new OptionAnswer()
               {
                   OptionCode = this._AnswerOption.OptionCode,
                   OptionID = this._AnswerOption.OptionID
               };
               return optionAnswer;
           } 
       }

       /// <summary>
       /// ��JSON��ʾ��������ַ���
       /// </summary>
       public override string Answer 
       { 
           get
           {
               if (this.OptionAnswer == null)
                   return "";
               return AnswerBase.Serialize(this.OptionAnswer);
           }
       }
       #endregion
   }
    /// <summary>
    /// ��ʾ���е�ĳһ����ڵ�ѡ�⡢��ѡ����ж�������
    /// </summary>
   [Serializable]
    public class OptionAnswer
   {
       /// <summary>
       /// ��ѡ��𰸵�ѡ��
       /// </summary>
       public string OptionCode { get; set; }
       public System.Guid OptionID { get; set; }
   }
}