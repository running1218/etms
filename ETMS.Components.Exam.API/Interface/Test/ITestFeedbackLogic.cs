// File:    ITestFeedbackLogic.cs
// Author:  Administrator
// Created: 2012��1��12�� 14:53:11
// Purpose: Definition of Interface ITestFeedbackLogic

using System;
using System.Collections.Generic;
using ETMS.Components.Exam.API.Entity.Test;
using System.ServiceModel;

///<summary>
/// ����
///</summary>
namespace ETMS.Components.Exam.API.Interface.Test
{
   ///<summary>
   /// һ���Ծ������д��ⷴ����ȫ����������߼��ӿڣ��Ƿ���ӿڣ�
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