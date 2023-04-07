// File:    MultipleChoiceAnswer.cs
// Author:  Administrator
// Created: 2011年12月15日 19:11:03
// Purpose: Definition of Class MultipleChoiceAnswer

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ETMS.Components.Exam.API.Entity.ItemBank
{
   ///<summary>
   /// 多选题答案
   ///</summary>
    [Serializable]
   public class MultipleChoiceAnswer : AnswerBase
   {
       private string _Answer = "";
       private IList<QuestionOption> _AnswerOptions = null;
       private IList<OptionAnswer> _OptionAnswers = null;

       #region --构造函数--
       public MultipleChoiceAnswer(string sAnswer)
       {
           this._Answer = sAnswer;
           this._OptionAnswers = AnswerBase.Deserialize<IList<OptionAnswer>>(sAnswer);
           //生成_AnswerOptions
           this.InitAnswerOptions();
       }
        /// <summary>
        /// 根据试题的答案信息生成对应的试题选项信息
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
       /// 创建一个选择题答案选项
       /// </summary>
       /// <param name="AnswerOptions">答案选项</param>
       public MultipleChoiceAnswer(IList<QuestionOption> AnswerOptions)
       {
           this._AnswerOptions = AnswerOptions;
           this.Init();
       }
       /// <summary>
       /// 创建一个单选题答案实体.
       /// </summary>
       /// <remarks>
       /// 1,前端接口请其它构造函数; <br></br>
       /// 2,用于构造试题时，将答案字符串与答案选项一起加载
       /// </remarks>
       /// <param name="sAnswer">答案的字符串表示</param>
       /// <param name="AnswerOptions">答案选项</param>
       public MultipleChoiceAnswer(string sAnswer, IList<QuestionOption> AnswerOptions)
       {
           int nValid = 0;
           nValid = ValidAnswerStringObject(sAnswer, AnswerOptions);
           if (nValid != 0)
           {
               throw new Exception("答案字符串与答案选项不一致");
           }

           this._Answer = sAnswer;
           this._AnswerOptions = AnswerOptions;
           this.Init();
       }

       /// <summary>
       /// 对答案中的字符串，答案选项对象进行对比看是否一致
       /// </summary>
       /// <param name="sAnswer">答案的字符串表示</param>
       /// <param name="AnswerOptions">答案选项列表</param>
       /// <returns></returns>
       private int ValidAnswerStringObject(string sAnswer, IList<QuestionOption> AnswerOptions)
       {
           if (string.IsNullOrEmpty(sAnswer) && (AnswerOptions == null || AnswerOptions.Count<=0))
               return 0;
           if (!string.IsNullOrEmpty(sAnswer) && AnswerOptions != null && AnswerOptions.Count>0)
           {
               IList<OptionAnswer> optionAnswers = AnswerBase.Deserialize<IList<OptionAnswer>>(sAnswer);
               
               //判断
               if (optionAnswers == null || optionAnswers.Count != AnswerOptions.Count)
               {
                   //字符串，与选项个数不一致
                   return 2;
               }
               else
               { 
                //比较是否都相同
                   StringBuilder sAnswerTmp = new StringBuilder();
                   StringBuilder sOptionsTmp = new StringBuilder();

                   //得到选项标题排序后的组合成的字符串显示
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
                       //选项中标题不一致
                       return 3;
                   }
                   return 0;
               }
           }
           else
           {
               //不一致，一个为空，一个不为空
               return 1;
           }
       }

       /// <summary>
       /// 根据答案选项，生成答案的JSON字符串和对象
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

       #region --公共属性--
       ///<summary>
       /// 多项选择题的答案选项列表
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

       #region --内容属性--
       /// <summary>
       /// JSON字符串对象
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
       /// 以JSON表示的试题答案字符串
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