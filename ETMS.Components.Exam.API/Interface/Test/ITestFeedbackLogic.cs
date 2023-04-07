// File:    ITestFeedbackLogic.cs
// Author:  Administrator
// Created: 2012年1月12日 14:53:11
// Purpose: Definition of Interface ITestFeedbackLogic

using System;
using System.Collections.Generic;
using ETMS.Components.Exam.API.Entity.Test;
using System.ServiceModel;

///<summary>
/// 测评
///</summary>
namespace ETMS.Components.Exam.API.Interface.Test
{
   ///<summary>
   /// 一个试卷中所有答题反馈的全部内容相关逻辑接口（非服务接口）
   ///</summary>
   [ServiceContract(Namespace = "http://Autumn.Business.LMS.Interface.Test/ITestFeedbackLogic")]
   public interface ITestFeedbackLogic
   {
       [OperationContract]
       void Load(Guid testPaperID);
       [OperationContract]
       Guid AddItem(TestFeedback feedbackItem);
       [OperationContract]
       void UpdateItem(TestFeedback newFeedbackItem);
       [OperationContract]
       void DeleteItem(Guid testFeedbackID);

       [OperationContract]
       bool Delete();
       [OperationContract]
       bool Update();

       Guid TestPaperID {
           [OperationContract]
           get;
           [OperationContract]
           set; 
       }

       IList<TestFeedback> Feedbacks {
           [OperationContract]
           get;
           [OperationContract]
           set; 
       }
   }
}