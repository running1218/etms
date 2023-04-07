using System;
using System.Collections.Generic;
using ETMS.Components.Exam.API.Entity.ItemBank;

namespace ETMS.Components.Exam.Implement.DAL.Test
{
    public interface IExamResourceDao
    {
        /// <summary>
        /// 1、添加
        /// </summary>
        /// <param name="customer"></param>
        void AddExamResource(QuestionResource obj);

        /// <summary>
        /// 3、删除
        /// </summary>
        /// <param name="id">The id.</param>
        void Delete(Guid containerID, Guid resourceID);

        /// <summary>
        /// 4、查找试题下的素材
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IList<Guid> GetQuestionMaterialList(Guid questionID);

        /// <summary>
        /// 5、查找试卷下的素材（不包括试卷下试题的素材）
        /// </summary>
        /// <param name="questionID"></param>
        /// <returns></returns>
        IList<Guid> GetTestpaperMaterialList(Guid testpaperID);
    }
}
