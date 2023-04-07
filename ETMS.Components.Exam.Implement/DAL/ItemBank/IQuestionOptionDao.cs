// File:    IQuestionOptionDao.cs
// Author:  Administrator
// Created: 2011年12月15日 9:54:10
// Purpose: Definition of Interface IQuestionOptionDao

using System;
using System.Collections.Generic;
using ETMS.Components.Exam.API.Entity.ItemBank;

namespace ETMS.Components.Exam.Implement.DAL.ItemBank
{
   ///<summary>
   /// 对试题选项的数据访问的接口
   ///</summary>
   public interface IQuestionOptionDao
   {
      ///<summary>
      /// 添加一个试题选项
      ///</summary>
      ///<param name="questionOption">要添加的试题选项</param>
      void AddOption(QuestionOption questionOption);
      /// <summary>
      /// 更新已存在的试题选项信息
      /// </summary>
      /// <param name="newQuestionOption">更新的试题选项</param>
      void Update(QuestionOption newQuestionOption);
      /// <summary>
      /// 删除指定ID的试题选项
      /// </summary>
      /// <param name="questionOptionID">试题选项ID</param>
      void Delete(System.Guid questionOptionID);
      /// <summary>
      /// 删除指定试题的全部试题选项
      /// </summary>
      /// <param name="questionID">试题ID</param>
      void DeleteByQuestionID(System.Guid questionID);
      /// <summary>
      /// 删除某一试题中多个选项
      /// </summary>
      /// <param name="questionID">选项所在试题ID</param>
      /// <param name="LstOptionsID">要删除的选项ID列表</param>
      void DeleteByOptionsID(System.Guid questionID, IList<System.Guid> LstOptionsID);
      /// <summary>
      /// 删除某一试题中指定选项组的选项
      /// </summary>
      /// <param name="questionID">选项所在试题ID</param>
      /// <param name="optionGroupID">选项所在选项组</param>
      void DeleteByGroupID(System.Guid questionID, System.Guid optionGroupID);
      /// <summary>
      /// 得到指定选项ID的试题选项
      /// </summary>
      /// <param name="questionOptionID">试题选项ID</param>
      /// <returns></returns>
      QuestionOption GetQuestionOption(System.Guid questionOptionID);
      /// <summary>
      /// 得到指定试题的所有选项信息
      /// </summary>
      /// <param name="questionID">试题ID</param>
      /// <returns></returns>
      IList<QuestionOption> FindQuestionOptionsInQuestion(Guid questionID);
      
      ///<summary>
      /// 得到一个选项组中的所有选项。
      ///</summary>
      ///<param name="questionID">选项所在试题ID</param>
      ///<param name="optionGroupID">选项组ID</param>
      ///<returns></returns>
      IList<QuestionOption> FindQuestionOptionsInGroup(System.Guid questionID, System.Guid optionGroupID);
   
   }
}