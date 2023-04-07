// File:    QuestionOptionLogic.cs
// Author:  Administrator
// Created: 2011��12��15�� 11:25:10
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
    /// ����ѡ����߼�����
    ///</summary>
    public class QuestionOptionLogic : IMessageSourceAware, IInitializingObject, IQuestionOptionLogic
    {
        #region --�������--
        private static string Err_QuestionOption_New_Invalid = "ItemBank.QuestionOption.New.Invalid";
        private static string Err_QuestionOption_Instance_Invalid = "ItemBank.QuestionOption.Instance.Invalid";
        #endregion

        #region --����--
        public IQuestionOptionDao QuestionOptionDao { get; set; }
        ///<summary>
        /// ������ѡ���ʵ��
        ///</summary>
        public QuestionOption QuestionOption
        {
            get;
            set;
        }
        /// <summary>
        /// ��������ѡ���ID
        /// </summary>
        public System.Guid QuestionOptionID { get; set; }
        #endregion

        #region IInitializingObject ��Ա

        public void AfterPropertiesSet()
       {
           if (QuestionOptionDao == null)
           {
               throw new Exception("please set CustomerDao Property First!");
           }
       }

       #endregion

       #region IMessageSourceAware ��Ա

        public IMessageSource MessageSource
        {
            get;
            set;
        }

       #endregion

       #region --��ȡ����ѡ��--
       ///<summary>
       /// ����һ��ѡ��
       ///</summary>
       ///<param name="questionOptionID">����ѡ��ID</param>
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
       /// ����һ�������е�����ѡ��
       ///</summary>
       ///<param name="questionID">����ID</param>
       ///<returns>ͬһ����������ѡ���б�</returns>
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
       /// ����һ������ָ��ѡ����������ѡ��
       ///</summary>
       ///<param name="questionID">����ID</param>
       ///<param name="optionGroupTitleID">ѡ����ID</param>
       ///<returns>ѡ���б�</returns>
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
      /// ���һ������ѡ��
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
          //���ݿ����
          QuestionOptionDao.AddOption(option);

          QuestionOptionLogic optionLogic = new QuestionOptionLogic();
          optionLogic.QuestionOption = option;
          optionLogic.QuestionOptionID = option.OptionID;

          return optionLogic;
      }
        
      /// <summary>
      /// ���µ�ǰ����ѡ��
      /// </summary>
      /// <returns></returns>
        public bool Update()
      {
          ThrowExceptionNotInitialized();
          QuestionOptionDao.Update(this.QuestionOption);

          return true;
      }
      
       /// <summary>
       /// ɾ����ǰ����ѡ��
       /// </summary>
       /// <returns></returns>
      public bool Delete()
      {
          ThrowExceptionNotInitialized();
          QuestionOptionDao.Delete(this.QuestionOptionID);
          return true;
      }

      ///<summary>
      /// ɾ��һ������������ѡ�� 
      ///</summary>
      /// <param name="questionID">����ID</param>
      public bool DeleteByQuestionID(System.Guid questionID)
      {
          QuestionOptionDao.DeleteByQuestionID(questionID);
          return true;
      }
      
      ///<summary>
      /// ɾ��һ������ѡ����������ѡ�� 
      ///</summary>
      /// <param name="questionID">����ID</param>
      /// <param name="optionGroupID">����ѡ����ID</param>
      public bool DeleteByGroupID(System.Guid questionID, System.Guid optionGroupID)
      {
          QuestionOptionDao.DeleteByGroupID(questionID, optionGroupID);
          return true;
      }

      #region --��֤--
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
              sErrMsg = "δ��ȷ��������";
              return 1;
          }
          if (this.QuestionOption.OptionID != this.QuestionOptionID)
          {
              sErrMsg = "���ݴ��󣬲������޸�ѡ��ID";
              return 2;
          }

          return 0;
      }
      /// <summary>
      /// ��֤ĳһ����ѡ���Ƿ�Ϻ��淶
      /// </summary>
      /// <param name="option"></param>
      /// <returns></returns>
      private int ValidQuestionOption(QuestionOption option, out string sErrMsg)
      {
          sErrMsg = "";
          if (option == null)
          {
              sErrMsg = "����ѡ��ΪNULL";
              return 1;
          }
          if (option.QuestionID == null || option.QuestionID == Guid.Empty)
          {
              sErrMsg = "δָ��ѡ�����ڵ�����ID";
              return 2;
          }

          return 0;
      }
        #endregion
    }
}