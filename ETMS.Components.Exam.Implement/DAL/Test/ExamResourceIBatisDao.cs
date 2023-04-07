using System;
using System.Collections.Generic;
using ETMS.Components.Exam.API.Entity.ItemBank;
using Autumn.Data.ORM.IBatis;

namespace ETMS.Components.Exam.Implement.DAL.Test
{
    public class ExamResourceIBatisDao : ReadWriteDataMapperDaoSupport, IExamResourceDao
    {
        public void AddExamResource(QuestionResource obj)
        {
            this.DataMapperClient_Write.Insert("Test.ExamResource.AddExamResource", obj);
        }

        public void Delete(Guid containerID, Guid resourceID)
        {
            this.DataMapperClient_Write.Delete("Test.ExamResource.Delete", new { ContainerID = containerID, ResourceID = resourceID });
        }

        public IList<Guid> GetQuestionMaterialList(Guid questionID)
        {
            return this.DataMapperClient_Read.QueryForList<Guid>("Test.ExamResource.GetQuestionMaterialList", questionID);
        }

        public IList<Guid> GetTestpaperMaterialList(Guid testpaperID)
        {
            return this.DataMapperClient_Read.QueryForList<Guid>("Test.ExamResource.GetTestpaperMaterialList", testpaperID);
        }
    }
}
