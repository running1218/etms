using System;
using System.Collections.Generic;

using Autumn.Context;
using Autumn.Objects.Factory;
using ETMS.Components.Exam.Implement.DAL.Test;
using ETMS.Components.Exam.API.Entity.ItemBank;
using ETMS.Components.Exam.API.Interface.Test;
namespace ETMS.Components.Exam.Implement.BLL.Test
{
    public class ExamResourceLogic : IExamResourceLogic, IMessageSourceAware, IInitializingObject
    {
        public IExamResourceDao ExamResourceDao { get; set; }

        #region IExamResourceLogic 成员
        public void AddExamResource(QuestionResource obj)
        {
            if (obj.ContainerType == EnumContainerType.Null || obj.Position == EnumPosition.Null)
                throw new ETMS.AppContext.BusinessException("System.Invalid.EmptyParameter");
            this.ExamResourceDao.AddExamResource(obj);
        }

        public void Delete(Guid containerID, Guid resourceID)
        {
            if (containerID == Guid.Empty || containerID == Guid.Empty)
                throw new ETMS.AppContext.BusinessException("System.Invalid.EmptyParameter");
            this.ExamResourceDao.Delete(containerID, resourceID);
        }

        public IList<Guid> GetQuestionMaterialList(Guid questionID)
        {
            if (questionID == Guid.Empty)
                throw new ETMS.AppContext.BusinessException("System.Invalid.EmptyParameter");
            return this.ExamResourceDao.GetQuestionMaterialList(questionID);
        }

        public IList<Guid> GetTestpaperMaterialList(Guid testpaperID)
        {
            if (testpaperID == Guid.Empty)
                throw new ETMS.AppContext.BusinessException("System.Invalid.EmptyParameter");
            return this.ExamResourceDao.GetTestpaperMaterialList(testpaperID);
        }
        #endregion

        #region IInitializingObject 成员

        public void AfterPropertiesSet()
        {
            if (this.ExamResourceDao == null)
                throw new Exception("please set ExamResourceDao Property First!");
        }

        #endregion

        #region IMessageSourceAware 成员

        public IMessageSource MessageSource { get; set; }

        #endregion
    }
}
