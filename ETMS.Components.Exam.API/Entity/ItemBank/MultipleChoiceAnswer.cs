// File:    MultipleChoiceAnswer.cs
// Author:  Administrator
// Created: 2011��12��15�� 19:11:03
// Purpose: Definition of Class MultipleChoiceAnswer

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ETMS.Components.Exam.API.Entity.ItemBank
{
   ///<summary>
   /// ��ѡ���
   ///</summary>
    [Serializable]
   public class MultipleChoiceAnswer : AnswerBase
   {
       private string _Answer = "";
       private IList<QuestionOption> _AnswerOptions = null;
       private IList<OptionAnswer> _OptionAnswers = null;

       #region --���캯��--
       public MultipleChoiceAnswer(string sAnswer)
       {
           this._Answer = sAnswer;
           this._OptionAnswers = AnswerBase.Deserialize<IList<OptionAnswer>>(sAnswer);
           //����_AnswerOptions
           this.InitAnswerOptions();
       }
        /// <summary>
        /// ��������Ĵ���Ϣ���ɶ�Ӧ������ѡ����Ϣ
        /// </summary>
       private void InitAnswerOptions()
       {
           if (this._OptionAnswers == null)
           {
               this._AnswerOptions = null;
               return;
           }
           IList<QuestionOption> LstAnswerOptions = new List<QuestionOption>();
           LstAnswerOptions = this._OptionAnswers.Select(
               x =>
               {
                   QuestionOption answer = new QuestionOption() { OptionCode = x.OptionCode, OptionID = x.OptionID };
                   return answer;
               }).ToList<QuestionOption>();

           this._AnswerOptions = LstAnswerOptions;
       }
       /// <summary>
       /// ����һ��ѡ�����ѡ��
       /// </summary>
       /// <param name="AnswerOptions">��ѡ��</param>
       public MultipleChoiceAnswer(IList<QuestionOption> AnswerOptions)
       {
           this._AnswerOptions = AnswerOptions;
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
       /// <param name="AnswerOptions">��ѡ��</param>
       public MultipleChoiceAnswer(string sAnswer, IList<QuestionOption> AnswerOptions)
       {
           int nValid = 0;
           nValid = ValidAnswerStringObject(sAnswer, AnswerOptions);
           if (nValid != 0)
           {
               throw new Exception("���ַ������ѡ�һ��");
           }

           this._Answer = sAnswer;
           this._AnswerOptions = AnswerOptions;
           this.Init();
       }

       /// <summary>
       /// �Դ��е��ַ�������ѡ�������жԱȿ��Ƿ�һ��
       /// </summary>
       /// <param name="sAnswer">�𰸵��ַ�����ʾ</param>
       /// <param name="AnswerOptions">��ѡ���б�</param>
       /// <returns></returns>
       private int ValidAnswerStringObject(string sAnswer, IList<QuestionOption> AnswerOptions)
       {
           if (string.IsNullOrEmpty(sAnswer) && (AnswerOptions == null || AnswerOptions.Count<=0))
               return 0;
           if (!string.IsNullOrEmpty(sAnswer) && AnswerOptions != null && AnswerOptions.Count>0)
           {
               IList<OptionAnswer> optionAnswers = AnswerBase.Deserialize<IList<OptionAnswer>>(sAnswer);
               
               //�ж�
               if (optionAnswers == null || optionAnswers.Count != AnswerOptions.Count)
               {
                   //�ַ�������ѡ�������һ��
                   return 2;
               }
               else
               { 
                //�Ƚ��Ƿ���ͬ
                   StringBuilder sAnswerTmp = new StringBuilder();
                   StringBuilder sOptionsTmp = new StringBuilder();

                   //�õ�ѡ�������������ϳɵ��ַ�����ʾ
                   (from option in optionAnswers
                                 orderby option.OptionCode
                                 select option.OptionCode).Select(
                                x => 
                                {
                                    sAnswerTmp.Append(x);
                                    return x;
                                }
                                ).ToList<string>();

                   (from option in AnswerOptions
                    orderby option.OptionCode
                    select option.OptionCode).Select(
                             x =>
                             {
                                 sOptionsTmp.Append(x);
                                 return x;
                             }
                             ).ToList<string>();
                   if (sAnswerTmp.ToString() != sOptionsTmp.ToString())
                   {
                       //ѡ���б��ⲻһ��
                       return 3;
                   }
                   return 0;
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
           if (this._AnswerOptions == null)
           {
               this._Answer = "";
               this._OptionAnswers = null;
               return;
           }
           IList<OptionAnswer> LstOptionAnswers = new List<OptionAnswer>();

           LstOptionAnswers =this._AnswerOptions.Select(
               x => 
               {
                   OptionAnswer answer = new OptionAnswer() { OptionCode=x.OptionCode, OptionID=x.OptionID };
                   return answer;
               }).ToList<OptionAnswer>();

           this._OptionAnswers = LstOptionAnswers;
           this._Answer = AnswerBase.Serialize(LstOptionAnswers);
       }
       #endregion

       #region --��������--
       ///<summary>
       /// ����ѡ����Ĵ�ѡ���б�
       ///</summary>
       public IList<QuestionOption> AnswerOptions
       {
           get { return this._AnswerOptions; }
           set 
           { 
               this._AnswerOptions = value;
               this.Init();
           }
       }
       #endregion

       #region --��������--
       /// <summary>
       /// JSON�ַ�������
       /// </summary>
       public IList<OptionAnswer> OptionAnswers
       { 
           get
           {
               if (this._AnswerOptions == null)
               {
                   return null;
               }
               IList<OptionAnswer> LstOptionAnswers = new List<OptionAnswer>();

               LstOptionAnswers = this._AnswerOptions.Select(
                   x =>
                   {
                       OptionAnswer answer = new OptionAnswer() { OptionCode = x.OptionCode, OptionID = x.OptionID };
                       return answer;
                   }).ToList<OptionAnswer>();
               return LstOptionAnswers;
           } 
       }

       /// <summary>
       /// ��JSON��ʾ��������ַ���
       /// </summary>
       public override string Answer 
       { 
           get
           {
               if (this.OptionAnswers == null || this.OptionAnswers.Count <= 0)
                   return "";

               return AnswerBase.Serialize(this.OptionAnswers); 
           }
       }
       #endregion
   }
}