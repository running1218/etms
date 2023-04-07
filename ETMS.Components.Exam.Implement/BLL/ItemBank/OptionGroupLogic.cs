// File:    OptionGroupLogic.cs
// Author:  Administrator
// Created: 2011年12月15日 10:33:35
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
    /// 试题选项组逻辑功能的实现
    /// </summary>
    internal class OptionGroupLogic : IMessageSourceAware, IInitializingObject, IOptionGroupLogic
    {
        #region --错误代码--
        private static string Err_OptionGroup_Not_Found = "ItemBank.OptionGroup.Not.Found";
        private static string Err_OptionGroup_Data_Invalid = "ItemBank.OptionGroup.Data.Invalid";
        private static string Err_OptionGroup_Instance_Invalid = "ItemBank.OptionGroup.Instance.Invalid";
        private static string Err_OptionGroup_New_Invalid = "ItemBank.OptionGroup.New.Invalid";
        #endregion

        public IQuestionOptionDao QuestionOptionDao{get;set;}
        public IOptionGroupDao OptionGroupDao { get; set; }

       #region IMessageSourceAware 成员

        public IMessageSource MessageSource
        {
            get;
            set;
        }

       #endregion

       #region IInitializingObject 成员

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

       #region --属性--
       ///<summary>
      /// 选项组中所有的选项列表
      ///</summary>
      public IList<QuestionOption> Options
      {
         get;
         set;
      }

      ///<summary>
      /// 对应的选项组实体
      ///</summary>
      public OptionGroup OptionGroup
      {
         get;
         set;
      }
        /// <summary>
        /// 选项组ID
        /// </summary>
      public System.Guid OptionGroupTitleID { get; set; }
        /// <summary>
        /// 选项组所在试题ID
        /// </summary>
      public System.Guid QuestionID { get; set; }
       #endregion

      ///<summary>
      /// 加载一个选项组
      ///</summary>
      /// <param name="optionGroupID">要加载的选项组ID</param>
      public IOptionGroupLogic Load(System.Guid questionID, System.Guid optionGroupID)
      {
          OptionGroupLogic oOptionGroupLogic = new OptionGroupLogic();

          oOptionGroupLogic.OptionGroup= OptionGroupDao.GetOptionGroup(optionGroupID);
          if (oOptionGroupLogic.OptionGroup == null)
          {
              throw new ETMS.AppContext.BusinessException("Err_OptionGroup_Not_Found", new Exception("没有找到选项组"));
          }
          oOptionGroupLogic.OptionGroupTitleID = oOptionGroupLogic.OptionGroup.OptionGroupTitleID;
          oOptionGroupLogic.QuestionID = questionID;

          //得到选项组对应的选项
          oOptionGroupLogic.Options= QuestionOptionDao.FindQuestionOptionsInGroup(
              questionID, optionGroupID);
          oOptionGroupLogic.OptionGroupDao = this.OptionGroupDao;
          oOptionGroupLogic.QuestionOptionDao = this.QuestionOptionDao;
          
          return oOptionGroupLogic;
      }
      
      ///<summary>
      /// 将一个试题的选项组全部加载
      ///</summary>
      /// <param name="questionID">选项组所处的试题ID</param>
      public IList<IOptionGroupLogic> LoadAllInQuestion(System.Guid questionID)
      {
          IList<IOptionGroupLogic> LstOptionGroupLogic = new List<IOptionGroupLogic>();

         //得到一个试题的所有选项组
         IList<OptionGroup> LstGroups= OptionGroupDao.FindOptionGroupsInQuestion(questionID);
         if (LstGroups == null || LstGroups.Count <= 0)
         {
             throw new ETMS.AppContext.BusinessException(Err_OptionGroup_Not_Found, new Exception("没有找到选项组"));
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
      /// 更新当前选项组,并更新选项组中选项
      ///</summary>
      public bool Update()
      {
          ThrowNotInitializedExeception();

          string sMsg = "";
          if (this.OptionGroup == null || this.OptionGroup.OptionGroupTitleID != this.OptionGroupTitleID)
          {
              throw new ETMS.AppContext.BusinessException(Err_OptionGroup_Data_Invalid, new Exception("选项组中数据错误"));
          }

         //先进行一些逻辑检查
          int nValid = this.ValidOptionsInGroup(this.Options, this.OptionGroup.OptionGroupTitleID);
          if (nValid != 0)
          {
              sMsg = "选项组中数据错误";
              switch (nValid)
              { 
                  case 1:
                      sMsg = "选项中存在重复";
                      break;
                  case 2:
                      sMsg = "选项不存在选项组中";
                      break;
              }
              throw new ETMS.AppContext.BusinessException(Err_OptionGroup_Data_Invalid, new Exception(sMsg));
          }
        //删除已不存在的选项
          IOptionGroupLogic OldGroup = this.Load(this.QuestionID, this.OptionGroupTitleID);
          if (OldGroup != null && OldGroup.Options.Count > 0)
          {
              IList<QuestionOption> LstDeletedOptions = this.GetDeletedQuestionOptions(OldGroup.Options, this.Options);
              var LstDeletedOptionsID = from optionitem in LstDeletedOptions
                                        select optionitem.OptionID;

              //删除指定试题中的多个选项
              QuestionOptionDao.DeleteByOptionsID(this.QuestionID, LstDeletedOptionsID.ToList<Guid>());
          }
          //更新选项组
          OptionGroupDao.Update(this.OptionGroup);
          //更新各个选项
          foreach (QuestionOption option in this.Options)
          {
              if (option.OptionID != null && option.OptionID != Guid.Empty)
              {
                  //已存在的更新
                  QuestionOptionDao.Update(option);
              }
              else
              { 
                //新建的选项
                  option.OptionID = Guid.NewGuid();
                  QuestionOptionDao.AddOption(option);
              }
          }

          return true;
      }
        /// <summary>
        /// 通过二个选项的对比得到已删除掉的选项
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
          //对比
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
        /// 验证在同一个选项组中的所有选项是否正确
        /// </summary>
        /// <param name="options"></param>
        /// <returns>0:验证通过，1:选项中存在重复， 2:选项不存在选项组中</returns>
      private int ValidOptionsInGroup(IList<QuestionOption> options,System.Guid OptionGroupTitleID)
      {
          if (options == null || options.Count <= 1)
              return 0;
          //检查是否存在相同的选项标题，或选项内容
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
        /// 检查某一选项在一个选项列表中是否存在相同的选项
        /// </summary>
        /// <param name="option">被检查的选项</param>
        /// <param name="options">选项列表</param>
        /// <returns></returns>
      private bool ValidOptionInOptions(QuestionOption option, IList<QuestionOption> options)
      { 
        if (options == null || options.Count <= 1)
              return true;
          //检查是否存在相同的选项标题，或选项内容
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
      /// 检查实例中数据是否完整，不完整直接抛出异常。
      /// </summary>
        private void ThrowNotInitializedExeception()
      {
          bool bIsInit = this.ValidIsInitialized();
          if (!bIsInit)
          {
              throw new ETMS.AppContext.BusinessException(Err_OptionGroup_Instance_Invalid, new Exception("未正确加载数据，请正确加载试题选项数据加载"));
          }
      }
        ///<summary>
      /// 删除当前选项组
      ///</summary>
      ///<remarks>
      ///在删除选项组的同时，会将选项组对应的选项删除掉。
      ///</remarks>
      public bool Delete()
      {
          ThrowNotInitializedExeception();

          OptionGroupDao.Delete(this.OptionGroupTitleID);
          QuestionOptionDao.DeleteByGroupID(this.QuestionID, this.OptionGroupTitleID);
          return true;
      }
      
      ///<summary>
      /// 添加一个新的选项组
      ///</summary>
      /// <param name="optionGroup">添加的选项组信息</param>
      /// <param name="options">选项组中所包含的选项</param>
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
        /// 验证新的选项组数据是否完整
        /// </summary>
        /// <param name="optionGroup">选项组信息</param>
        /// <param name="options">选项列表</param>
        /// <returns>0:正确，1：选项组信息为NULL 2:选项组中未指定试题ID 3:选项中试题ID与选项组中试题ID不一致</returns>
      private int  ValidNewOptionGroup(OptionGroup optionGroup, IList<QuestionOption> options,out string sErrMsg)
      {
          sErrMsg = "";
          if (optionGroup == null)
          {
              sErrMsg = "选项组信息为NULL";
              return 1;
          }
          if (optionGroup.QuestionID == null || optionGroup.QuestionID == Guid.Empty)
          {
              sErrMsg = "选项组中未指定试题ID";
              return 2;
          }
          // 李少波注释
          // 利用新增的代码337行，可以避免这个问题
          //if (options != null)
          //{
          //    var lst = from option in options
          //              where option.QuestionID != optionGroup.QuestionID
          //              select option;
          //    if (lst.Count<QuestionOption>() > 0)
          //    {
          //        sErrMsg = "选项中试题ID与选项组中试题ID不一致";
          //        return 3;
          //    }
          //}

          return 0;
      }
   }
}