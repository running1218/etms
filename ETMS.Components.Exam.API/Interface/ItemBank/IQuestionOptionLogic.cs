using System;
using ETMS.Components.Exam.API.Entity.ItemBank;
using System.Collections.Generic;
using System.ServiceModel;

namespace ETMS.Components.Exam.API.Interface.ItemBank
{
    /// <summary>
    /// 试题选项相关的业务逻辑接口
    /// </summary>
    /// <remarks>
    /// 1，本接口是试题选项的逻辑功能接口，而非试题选项的服务接口。<br></br>
    /// 2，如果要使用服务接口，可参考<seealso cref="IOptionService"/>。服务接口提供了便于使用的接口方法。
    /// </remarks>
    [ServiceContract(Namespace = "http://Autumn.Business.LMS.Interface.ItemBank/IQuestionOptionLogic")]
    public interface IQuestionOptionLogic
    {
        [OperationContract]
        IQuestionOptionLogic AddOption(QuestionOption option);
        [OperationContract]
        bool Delete();
        [OperationContract]
        bool DeleteByGroupID(Guid questionID, Guid optionGroupID);
        [OperationContract]
        bool DeleteByQuestionID(Guid questionID);
        [OperationContract]
        IQuestionOptionLogic Load(Guid questionOptionID);
        [OperationContract]
        IList<IQuestionOptionLogic> LoadAllInGroup(Guid questionID, Guid optionGroupTitleID);
        [OperationContract]
        IList<IQuestionOptionLogic> LoadAllInQuestion(Guid questionID);

        QuestionOption QuestionOption {
            [OperationContract]
            get;
            [OperationContract]
            set; 
        }
        [OperationContract]
        bool Update();
    }
}
