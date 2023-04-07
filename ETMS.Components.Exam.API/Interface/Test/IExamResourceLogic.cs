using System;
using System.Collections.Generic;
using System.ServiceModel;
using ETMS.Components.Exam.API.Entity.ItemBank;

namespace ETMS.Components.Exam.API.Interface.Test
{
    [ServiceContract(Namespace = "http://Autumn.Business.LMS.Interface.Test/IExamResourceLogic")]
    public interface IExamResourceLogic
    {
        /// <summary>
        /// 1、添加
        /// </summary>
        /// <param name="obj"></param>
        [OperationContract]
        void AddExamResource(QuestionResource obj);

        /// <summary>
        /// 3、删除
        /// </summary>
        /// <param name="containerID"></param>
        /// <param name="resourceID"></param>
        [OperationContract]
        void Delete(Guid containerID, Guid resourceID);

        /// <summary>
        /// 4、查找试题下的素材
        /// </summary>
        /// <param name="questionID"></param>
        /// <returns></returns>
        [OperationContract]
        IList<Guid> GetQuestionMaterialList(Guid questionID);

        /// <summary>
        /// 5、查找试卷下的素材（不包括试卷下试题的素材）
        /// </summary>
        /// <param name="testpaperID"></param>
        /// <returns></returns>
        [OperationContract]
        IList<Guid> GetTestpaperMaterialList(Guid testpaperID);
    }
}
