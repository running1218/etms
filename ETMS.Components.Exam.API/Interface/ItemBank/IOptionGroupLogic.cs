using System;
using ETMS.Components.Exam.API.Entity.ItemBank;
using System.Collections.Generic;
using System.ServiceModel;
namespace ETMS.Components.Exam.API.Interface.ItemBank
{
    /// <summary>
    /// 试题选项组的逻辑接口（非服务接口）
    /// </summary>
    [ServiceContract(Namespace = "http://Autumn.Business.LMS.Interface.ItemBank/IOptionGroupLogic")]
    public interface IOptionGroupLogic
    {
        [OperationContract]
        IOptionGroupLogic AddOptionGroup(OptionGroup optionGroup, IList<QuestionOption> options);
        [OperationContract]
        bool Delete();
        [OperationContract]
        IOptionGroupLogic Load(Guid questionID, Guid optionGroupID);
        [OperationContract]
        IList<IOptionGroupLogic> LoadAllInQuestion(Guid questionID);

        OptionGroup OptionGroup {
            [OperationContract]
            get;
            [OperationContract]
            set; 
        }

        IList<QuestionOption> Options {
            [OperationContract]
            get;
            [OperationContract]
            set; 
        }
        [OperationContract]
        bool Update();
    }
}
