// File:    SingleChoiceAnswer.cs
// Author:  Administrator
// Created: 2011年12月15日 17:49:05
// Purpose: Definition of Class SingleChoiceAnswer

using System;

namespace ETMS.Components.Exam.API.Entity.ItemBank
{
    ///<summary>
    /// 单选择题答案
    ///</summary>
    [Serializable]
   public class SingleChoiceAnswer : AnswerBase
   {
       private string _Answer = "";
       private QuestionOption _AnswerOption = null;
       private OptionAnswer _OptionAnswer = null;

       #region --构造函数--
       public SingleChoiceAnswer(string sAnswer)
       {
           this._Answer = sAnswer;
           this._OptionAnswer = AnswerBase.Deserialize<OptionAnswer>(sAnswer);
           this.InitAnswerOption();
       }
       /// <summary>
       /// 根据试题的答案信息生成对应的试题选项信息
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
       /// 创建一个选择题答案选项
       /// </summary>
       /// <param name="AnswerOption">答案选项</param>
       public SingleChoiceAnswer(QuestionOption AnswerOption)
       {
           this._AnswerOption = AnswerOption;
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
       /// <param name="AnswerOption">答案选项</param>
       public SingleChoiceAnswer(string sAnswer, QuestionOption AnswerOption)
       {
           int nValid = 0;
           nValid = ValidAnswerStringObject(sAnswer, AnswerOption);
           if (nValid != 0)
           {
               throw new Exception("答案字符串与答案选项不一致");
           }

           this._Answer = sAnswer;
           this._AnswerOption = AnswerOption;
           this.Init();
       }

       /// <summary>
       /// 对答案中的字符串，答案选项对象进行对比看是否一致
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
                   //字符串，与选项不一致
                   return 2;
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

       #region --公共属性--
       ///<summary>
       /// 选择题的答案选项
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

       #region --内容属性--
       /// <summary>
       /// JSON字符串对象
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
       /// 以JSON表示的试题答案字符串
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
    /// 表示答案中的某一项，用于单选题、多选题和判断题题型
    /// </summary>
   [Serializable]
    public class OptionAnswer
   {
       /// <summary>
       /// 单选题答案的选项
       /// </summary>
       public string OptionCode { get; set; }
       public System.Guid OptionID { get; set; }
   }
}