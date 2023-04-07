using System;
using System.Collections.Generic;

using System.ServiceModel;
using ETMS.Components.Exam.API.Entity.ItemBank;
namespace ETMS.Components.Exam.API.Interface.ItemBank
{
    /// <summary>
    /// 非前台调用方法临时测试
    /// </summary>
    [ServiceContract(Namespace = "http://Autumn.Business.LMS.Interface.ItemBank/IQuestionTempLogic")]
    public interface IQuestionTempLogic
    {
        /*
        [OperationContract]
        bool Add(QuestionExtend extend);
        [OperationContract]
        void Update(QuestionExtend extend);
        [OperationContract]
        void Delete(Guid questionID);
        [OperationContract]
        QuestionExtend GetQuestionExtend(Guid questionID);
        [OperationContract]
        bool IsExist(Guid questionID);
        */

        /*
        [OperationContract]
        Guid Add(QuestionFeedback feedback);
        [OperationContract]
        void Delete(Guid feedbackID);
        [OperationContract]
        QuestionFeedback GetFeedback(Guid questionID);
        [OperationContract]
        void ModifyRightFeedback(Guid feedbackID, string content);
        [OperationContract]
        void ModifyWrongFeedback(Guid feedbackID, string content);
        [OperationContract]
        string GetRightFeedback(Guid questionID);
        [OperationContract]
        string GetWrongFeedback(Guid questionID);
        [OperationContract]
        bool IsExist(Guid questionID);
        */

        [OperationContract]
        Guid Add(OptionFeedback feedback);

        [OperationContract]
        void Delete(Guid feedbackID);

        [OperationContract]
        IList<OptionFeedback> GetFeedback(Guid questionID);

        [OperationContract]
        void Update(Guid feedbackID, string options, string content);

        [OperationContract]
        IList<String> GetOptionFeedback(Guid questionID, string options);

        [OperationContract]
        bool IsExist(Guid questionID);

    }
}
