// File:    OptionGroupLogic.cs
// Author:  Administrator
// Created: 2011��12��15�� 10:33:35
// Purpose: Definition of Class OptionGroupLogic

using System;
using System.Collections.Generic;
using ETMS.Components.Exam.API.Entity.ItemBank;
using Autumn.Context;
using Autumn.Objects.Factory;
using ETMS.Components.Exam.Implement.DAL.ItemBank;
using System.Linq;
using ETMS.Components.Exam.API.Interface.ItemBank;
namespace ETMS.Components.Exam.Implement.BLL.ItemBank
{
    /// <summary>
    /// ����ѡ�����߼����ܵ�ʵ��
    /// </summary>
    internal class OptionGroupLogic : IMessageSourceAware, IInitializingObject, IOptionGroupLogic
    {
        #region --�������--
        private static string Err_OptionGroup_Not_Found = "ItemBank.OptionGroup.Not.Found";
        private static string Err_OptionGroup_Data_Invalid = "ItemBank.OptionGroup.Data.Invalid";
        private static string Err_OptionGroup_Instance_Invalid = "ItemBank.OptionGroup.Instance.Invalid";
        private static string Err_OptionGroup_New_Invalid = "ItemBank.OptionGroup.New.Invalid";
        #endregion

        public IQuestionOptionDao QuestionOptionDao{get;set;}
        public IOptionGroupDao OptionGroupDao { get; set; }

       #region IMessageSourceAware ��Ա

        public IMessageSource MessageSource
        {
            get;
            set;
        }

       #endregion

       #region IInitializingObject ��Ա

       public void AfterPropertiesSet()
       {
           if (QuestionOptionDao == null)
           {
               throw new Exception("please set CoursewareDao Property First!");
           }
           if (OptionGroupDao == null)
           {
               throw new Exception("please set CustomerDao Property First!");
           }
       }

       #endregion

       #region --����--
       ///<summary>
      /// ѡ���������е�ѡ���б�
      ///</summary>
      public IList<QuestionOption> Options
      {
         get;
         set;
      }

      ///<summary>
      /// ��Ӧ��ѡ����ʵ��
      ///</summary>
      public OptionGroup OptionGroup
      {
         get;
         set;
      }
        /// <summary>
        /// ѡ����ID
        /// </summary>
      public System.Guid OptionGroupTitleID { get; set; }
        /// <summary>
        /// ѡ������������ID
        /// </summary>
      public System.Guid QuestionID { get; set; }
       #endregion

      ///<summary>
      /// ����һ��ѡ����
      ///</summary>
      /// <param name="optionGroupID">Ҫ���ص�ѡ����ID</param>
      public IOptionGroupLogic Load(System.Guid questionID, System.Guid optionGroupID)
      {
          OptionGroupLogic oOptionGroupLogic = new OptionGroupLogic();

          oOptionGroupLogic.OptionGroup= OptionGroupDao.GetOptionGroup(optionGroupID);
          if (oOptionGroupLogic.OptionGroup == null)
          {
              throw new ETMS.AppContext.BusinessException("Err_OptionGroup_Not_Found", new Exception("û���ҵ�ѡ����"));
          }
          oOptionGroupLogic.OptionGroupTitleID = oOptionGroupLogic.OptionGroup.OptionGroupTitleID;
          oOptionGroupLogic.QuestionID = questionID;

          //�õ�ѡ�����Ӧ��ѡ��
          oOptionGroupLogic.Options= QuestionOptionDao.FindQuestionOptionsInGroup(
              questionID, optionGroupID);
          oOptionGroupLogic.OptionGroupDao = this.OptionGroupDao;
          oOptionGroupLogic.QuestionOptionDao = this.QuestionOptionDao;
          
          return oOptionGroupLogic;
      }
      
      ///<summary>
      /// ��һ�������ѡ����ȫ������
      ///</summary>
      /// <param name="questionID">ѡ��������������ID</param>
      public IList<IOptionGroupLogic> LoadAllInQuestion(System.Guid questionID)
      {
          IList<IOptionGroupLogic> LstOptionGroupLogic = new List<IOptionGroupLogic>();

         //�õ�һ�����������ѡ����
         IList<OptionGroup> LstGroups= OptionGroupDao.FindOptionGroupsInQuestion(questionID);
         if (LstGroups == null || LstGroups.Count <= 0)
         {
             throw new ETMS.AppContext.BusinessException(Err_OptionGroup_Not_Found, new Exception("û���ҵ�ѡ����"));
         }
         foreach (OptionGroup item in LstGroups)
         {
             IOptionGroupLogic itemLogic = new OptionGroupLogic();
             itemLogic.OptionGroup = item;
             //itemLogic.OptionGroupTitleID = item.OptionGroupTitleID;
             //itemLogic.QuestionID = questionID;

             itemLogic.Options = QuestionOptionDao.FindQuestionOptionsInGroup(questionID, item.OptionGroupTitleID);

             LstOptionGroupLogic.Add(itemLogic);
         }
         return LstOptionGroupLogic;
         //return (IList<IOptionGroupLogic>)LstOptionGroupLogic;
      }
      
      ///<summary>
      /// ���µ�ǰѡ����,������ѡ������ѡ��
      ///</summary>
      public bool Update()
      {
          ThrowNotInitializedExeception();

          string sMsg = "";
          if (this.OptionGroup == null || this.OptionGroup.OptionGroupTitleID != this.OptionGroupTitleID)
          {
              throw new ETMS.AppContext.BusinessException(Err_OptionGroup_Data_Invalid, new Exception("ѡ���������ݴ���"));
          }

         //�Ƚ���һЩ�߼����
          int nValid = this.ValidOptionsInGroup(this.Options, this.OptionGroup.OptionGroupTitleID);
          if (nValid != 0)
          {
              sMsg = "ѡ���������ݴ���";
              switch (nValid)
              { 
                  case 1:
                      sMsg = "ѡ���д����ظ�";
                      break;
                  case 2:
                      sMsg = "ѡ�����ѡ������";
                      break;
              }
              throw new ETMS.AppContext.BusinessException(Err_OptionGroup_Data_Invalid, new Exception(sMsg));
          }
        //ɾ���Ѳ����ڵ�ѡ��
          IOptionGroupLogic OldGroup = this.Load(this.QuestionID, this.OptionGroupTitleID);
          if (OldGroup != null && OldGroup.Options.Count > 0)
          {
              IList<QuestionOption> LstDeletedOptions = this.GetDeletedQuestionOptions(OldGroup.Options, this.Options);
              var LstDeletedOptionsID = from optionitem in LstDeletedOptions
                                        select optionitem.OptionID;

              //ɾ��ָ�������еĶ��ѡ��
              QuestionOptionDao.DeleteByOptionsID(this.QuestionID, LstDeletedOptionsID.ToList<Guid>());
          }
          //����ѡ����
          OptionGroupDao.Update(this.OptionGroup);
          //���¸���ѡ��
          foreach (QuestionOption option in this.Options)
          {
              if (option.OptionID != null && option.OptionID != Guid.Empty)
              {
                  //�Ѵ��ڵĸ���
                  QuestionOptionDao.Update(option);
              }
              else
              { 
                //�½���ѡ��
                  option.OptionID = Guid.NewGuid();
                  QuestionOptionDao.AddOption(option);
              }
          }

          return true;
      }
        /// <summary>
        /// ͨ������ѡ��ĶԱȵõ���ɾ������ѡ��
        /// </summary>
        /// <param name="OldLstOptions"></param>
        /// <param name="NewLstOptions"></param>
        /// <returns></returns>
      private IList<QuestionOption> GetDeletedQuestionOptions(IList<QuestionOption> OldLstOptions
          ,IList<QuestionOption> NewLstOptions)
      {
          if (NewLstOptions == null || NewLstOptions.Count <= 0)
              return OldLstOptions;
          if (OldLstOptions == null || OldLstOptions.Count <= 0)
              return null;

          IList<QuestionOption> LstDeletedOptions = new List<QuestionOption>();
          //�Ա�
          foreach (QuestionOption option in OldLstOptions)
          {
              var LstTmp = from itemOption in NewLstOptions
                           where itemOption.OptionID == option.OptionID
                           select itemOption;
              if (LstTmp == null || LstTmp.Count<QuestionOption>()<= 0)
              {
                  LstDeletedOptions.Add(option);
              }
          }

          return LstDeletedOptions;
      }
        /// <summary>
        /// ��֤��ͬһ��ѡ�����е�����ѡ���Ƿ���ȷ
        /// </summary>
        /// <param name="options"></param>
        /// <returns>0:��֤ͨ����1:ѡ���д����ظ��� 2:ѡ�����ѡ������</returns>
      private int ValidOptionsInGroup(IList<QuestionOption> options,System.Guid OptionGroupTitleID)
      {
          if (options == null || options.Count <= 1)
              return 0;
          //����Ƿ������ͬ��ѡ����⣬��ѡ������
          foreach (QuestionOption option in options)
          {
              if (option.OptionGroupTitleID == null || option.OptionGroupTitleID != OptionGroupTitleID)
                  return 2;

              if (!ValidOptionInOptions(option, options))
              {
                  return 1; 
              }
          }
          return 0;
      }
        /// <summary>
        /// ���ĳһѡ����һ��ѡ���б����Ƿ������ͬ��ѡ��
        /// </summary>
        /// <param name="option">������ѡ��</param>
        /// <param name="options">ѡ���б�</param>
        /// <returns></returns>
      private bool ValidOptionInOptions(QuestionOption option, IList<QuestionOption> options)
      { 
        if (options == null || options.Count <= 1)
              return true;
          //����Ƿ������ͬ��ѡ����⣬��ѡ������
          foreach (QuestionOption item in options)
          {
              if (item.OptionID != option.OptionID)
              {
                  if (
                      (item.OptionCode == option.OptionCode && !string.IsNullOrEmpty(item.OptionCode))
                      ||
                      item.OptionContent == option.OptionContent)
                      return false;
              }
          }
          return true;
      }
      private bool ValidIsInitialized()
      { 
        if (this.OptionGroupTitleID == null || this.OptionGroupTitleID == Guid.Empty)
          {
              return false;
        }
          if (this.QuestionID == null || this.QuestionID == Guid.Empty)
          {
              return false;
          }

          return true;
      }
      /// <summary>
      /// ���ʵ���������Ƿ�������������ֱ���׳��쳣��
      /// </summary>
        private void ThrowNotInitializedExeception()
      {
          bool bIsInit = this.ValidIsInitialized();
          if (!bIsInit)
          {
              throw new ETMS.AppContext.BusinessException(Err_OptionGroup_Instance_Invalid, new Exception("δ��ȷ�������ݣ�����ȷ��������ѡ�����ݼ���"));
          }
      }
        ///<summary>
      /// ɾ����ǰѡ����
      ///</summary>
      ///<remarks>
      ///��ɾ��ѡ�����ͬʱ���Ὣѡ�����Ӧ��ѡ��ɾ������
      ///</remarks>
      public bool Delete()
      {
          ThrowNotInitializedExeception();

          OptionGroupDao.Delete(this.OptionGroupTitleID);
          QuestionOptionDao.DeleteByGroupID(this.QuestionID, this.OptionGroupTitleID);
          return true;
      }
      
      ///<summary>
      /// ���һ���µ�ѡ����
      ///</summary>
      /// <param name="optionGroup">��ӵ�ѡ������Ϣ</param>
      /// <param name="options">ѡ��������������ѡ��</param>
      public IOptionGroupLogic AddOptionGroup(OptionGroup optionGroup, IList<QuestionOption> options)
      {
          string sErrMsg = "";
          int nValid = ValidNewOptionGroup(optionGroup, options,out sErrMsg);
          if (nValid != 0)
          {
              throw new ETMS.AppContext.BusinessException(Err_OptionGroup_New_Invalid, new Exception(sErrMsg));
          }

          OptionGroupLogic oRet = new OptionGroupLogic();
          optionGroup.OptionGroupTitleID = Guid.NewGuid();
          OptionGroupDao.AddOptionGroup(optionGroup);
          if (options != null)
          {
              foreach (QuestionOption option in options)
              {
                  option.OptionID = Guid.NewGuid();
                  option.OptionGroupTitleID = optionGroup.OptionGroupTitleID;
                  option.QuestionID = optionGroup.QuestionID;
                  QuestionOptionDao.AddOption(option);
              }
          }

          oRet.OptionGroup = optionGroup;
          oRet.Options = options;
          oRet.QuestionID = optionGroup.QuestionID;
          oRet.OptionGroupTitleID = optionGroup.OptionGroupTitleID;

          return oRet;
      }
        /// <summary>
        /// ��֤�µ�ѡ���������Ƿ�����
        /// </summary>
        /// <param name="optionGroup">ѡ������Ϣ</param>
        /// <param name="options">ѡ���б�</param>
        /// <returns>0:��ȷ��1��ѡ������ϢΪNULL 2:ѡ������δָ������ID 3:ѡ��������ID��ѡ����������ID��һ��</returns>
      private int  ValidNewOptionGroup(OptionGroup optionGroup, IList<QuestionOption> options,out string sErrMsg)
      {
          sErrMsg = "";
          if (optionGroup == null)
          {
              sErrMsg = "ѡ������ϢΪNULL";
              return 1;
          }
          if (optionGroup.QuestionID == null || optionGroup.QuestionID == Guid.Empty)
          {
              sErrMsg = "ѡ������δָ������ID";
              return 2;
          }
          // ���ٲ�ע��
          // ���������Ĵ���337�У����Ա����������
          //if (options != null)
          //{
          //    var lst = from option in options
          //              where option.QuestionID != optionGroup.QuestionID
          //              select option;
          //    if (lst.Count<QuestionOption>() > 0)
          //    {
          //        sErrMsg = "ѡ��������ID��ѡ����������ID��һ��";
          //        return 3;
          //    }
          //}

          return 0;
      }
   }
}