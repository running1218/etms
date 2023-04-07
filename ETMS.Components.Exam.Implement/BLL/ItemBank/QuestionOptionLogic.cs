// File:    QuestionOptionLogic.cs
// Author:  Administrator
// Created: 2011年12月15日 11:25:10
// Purpose: Definition of Class QuestionOptionLogic

using System;
using System.Collections.Generic;
using Autumn.Context;
using Autumn.Objects.Factory;
using ETMS.Components.Exam.API.Entity.ItemBank;
using ETMS.Components.Exam.Implement.DAL.ItemBank;
using System.Linq;
using ETMS.Components.Exam.API.Interface.ItemBank;
namespace ETMS.Components.Exam.Implement.BLL.ItemBank
{
    ///<summary>
    /// 试题选项的逻辑功能
    ///</summary>
    public class QuestionOptionLogic : IMessageSourceAware, IInitializingObject, IQuestionOptionLogic
    {
        #region --错误代码--
        private static string Err_QuestionOption_New_Invalid = "ItemBank.QuestionOption.New.Invalid";
        private static string Err_QuestionOption_Instance_Invalid = "ItemBank.QuestionOption.Instance.Invalid";
        #endregion

        #region --属性--
        public IQuestionOptionDao QuestionOptionDao { get; set; }
        ///<summary>
        /// 试题中选项的实体
        ///</summary>
        public QuestionOption QuestionOption
        {
            get;
            set;
        }
        /// <summary>
        /// 加载试题选项的ID
        /// </summary>
        public System.Guid QuestionOptionID { get; set; }
        #endregion

        #region IInitializingObject 成员

        public void AfterPropertiesSet()
       {
           if (QuestionOptionDao == null)
           {
               throw new Exception("please set CustomerDao Property First!");
           }
       }

       #endregion

       #region IMessageSourceAware 成员

        public IMessageSource MessageSource
        {
            get;
            set;
        }

       #endregion

       #region --获取试题选项--
       ///<summary>
       /// 加载一个选项
       ///</summary>
       ///<param name="questionOptionID">试题选项ID</param>
       public IQuestionOptionLogic Load(System.Guid questionOptionID)
       {
           QuestionOption option = QuestionOptionDao.GetQuestionOption(questionOptionID);
           QuestionOptionLogic optionLogic = new QuestionOptionLogic();
           optionLogic.QuestionOption = option;
           optionLogic.QuestionOptionID = questionOptionID;
           optionLogic.QuestionOptionDao = this.QuestionOptionDao;

           return optionLogic;
       }
       ///<summary>
       /// 加载一个试题中的所有选项
       ///</summary>
       ///<param name="questionID">试题ID</param>
       ///<returns>同一试题中所有选项列表</returns>
       public IList<IQuestionOptionLogic> LoadAllInQuestion(System.Guid questionID)
       {
           IList<IQuestionOptionLogic> LstOptionLogic = new List<IQuestionOptionLogic>();
           IList<QuestionOption> LstOptions = QuestionOptionDao.FindQuestionOptionsInQuestion(questionID);

           LstOptionLogic = LstOptions.Select(
               x =>
               {
                   QuestionOptionLogic tmp = new QuestionOptionLogic();
                   tmp.QuestionOption = x;
                   tmp.QuestionOptionID = x.OptionID;
                   return tmp;
               }
           ).ToList<IQuestionOptionLogic>();

           return LstOptionLogic;
       }

       ///<summary>
       /// 加载一个试题指定选项组中所有选项
       ///</summary>
       ///<param name="questionID">试题ID</param>
       ///<param name="optionGroupTitleID">选项组ID</param>
       ///<returns>选项列表</returns>
       public IList<IQuestionOptionLogic> LoadAllInGroup(System.Guid questionID, System.Guid optionGroupTitleID)
       {
           IList<IQuestionOptionLogic> LstOptionLogic = new List<IQuestionOptionLogic>();
           IList<QuestionOption> LstOptions =QuestionOptionDao.FindQuestionOptionsInGroup(questionID, optionGroupTitleID);

           LstOptionLogic = LstOptions.Select(
               x =>
               {
                   QuestionOptionLogic tmp = new QuestionOptionLogic();
                   tmp.QuestionOption = x;
                   tmp.QuestionOptionID = x.OptionID;
                   return tmp;
               }
           ).ToList<IQuestionOptionLogic>();

           return LstOptionLogic;
       }
       #endregion
      

      ///<summary>
      /// 添加一个试题选项
      ///</summary>
      public IQuestionOptionLogic AddOption(QuestionOption option)
      {
          int nValid = 0;
          string sErrMsg = "";
          nValid = ValidQuestionOption(option,out sErrMsg);
          if (nValid != 0)
          {
              throw new ETMS.AppContext.BusinessException(Err_QuestionOption_New_Invalid, new Exception(sErrMsg));
          }
          option.OptionID = Guid.NewGuid();
          //数据库操作
          QuestionOptionDao.AddOption(option);

          QuestionOptionLogic optionLogic = new QuestionOptionLogic();
          optionLogic.QuestionOption = option;
          optionLogic.QuestionOptionID = option.OptionID;

          return optionLogic;
      }
        
      /// <summary>
      /// 更新当前试题选项
      /// </summary>
      /// <returns></returns>
        public bool Update()
      {
          ThrowExceptionNotInitialized();
          QuestionOptionDao.Update(this.QuestionOption);

          return true;
      }
      
       /// <summary>
       /// 删除当前试题选项
       /// </summary>
       /// <returns></returns>
      public bool Delete()
      {
          ThrowExceptionNotInitialized();
          QuestionOptionDao.Delete(this.QuestionOptionID);
          return true;
      }

      ///<summary>
      /// 删除一个试题中所有选项 
      ///</summary>
      /// <param name="questionID">试题ID</param>
      public bool DeleteByQuestionID(System.Guid questionID)
      {
          QuestionOptionDao.DeleteByQuestionID(questionID);
          return true;
      }
      
      ///<summary>
      /// 删除一个试题选项组中所有选项 
      ///</summary>
      /// <param name="questionID">试题ID</param>
      /// <param name="optionGroupID">试题选项组ID</param>
      public bool DeleteByGroupID(System.Guid questionID, System.Guid optionGroupID)
      {
          QuestionOptionDao.DeleteByGroupID(questionID, optionGroupID);
          return true;
      }

      #region --验证--
      private void ThrowExceptionNotInitialized()
      {
          int nValid = 0;
          string sErrMsg = "";
          nValid = ValidIsInitialized(out sErrMsg);
          if (nValid != 0)
          {
              throw new ETMS.AppContext.BusinessException(Err_QuestionOption_Instance_Invalid, new Exception(sErrMsg));
          }
      }
      private int ValidIsInitialized(out string sErrMsg)
      {
          sErrMsg = "";
          if (this.QuestionOption == null || this.QuestionOptionID == null || this.QuestionOptionID == Guid.Empty)
          {
              sErrMsg = "未正确加载数据";
              return 1;
          }
          if (this.QuestionOption.OptionID != this.QuestionOptionID)
          {
              sErrMsg = "数据错误，不允许修改选项ID";
              return 2;
          }

          return 0;
      }
      /// <summary>
      /// 验证某一试题选项是否合乎规范
      /// </summary>
      /// <param name="option"></param>
      /// <returns></returns>
      private int ValidQuestionOption(QuestionOption option, out string sErrMsg)
      {
          sErrMsg = "";
          if (option == null)
          {
              sErrMsg = "试题选项为NULL";
              return 1;
          }
          if (option.QuestionID == null || option.QuestionID == Guid.Empty)
          {
              sErrMsg = "未指定选项所在的试题ID";
              return 2;
          }

          return 0;
      }
        #endregion
    }
}