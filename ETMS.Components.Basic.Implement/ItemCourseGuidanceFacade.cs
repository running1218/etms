using System;

namespace ETMS.Components.Basic.Implement
{
    using ETMS.AppContext.Component;
    using ETMS.Components.Basic.API;
    using ETMS.Components.Basic.Implement.BLL.Bulletin;



    /// <summary>
    /// 导学资料接口实现类
    /// 黄中福：2012-05-20
    /// </summary>
    public class ItemCourseGuidanceFacade : DefaultComponent, ITrainingItemCourseResourcesFacade
    {


        Tr_ItemCourseMentorDataLogic logic = new Tr_ItemCourseMentorDataLogic();


        #region ITrainingItemCourseResourcesFacade 成员

        public int GetResourcesTotal(Guid courseID)
        {
            return -1;
        }

        public int GetTrainingItemResourcesTotal(Guid trainingItemCourseID)
        {
            return logic.GetGuidanceDataTotalByTrainingItemCourseID(trainingItemCourseID);
        }

        public System.Data.DataTable GetTrainingItemResourcesList(Guid trainingItemCourseID, int pageIndex, int pageSize, string criteria, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public System.Data.DataTable GetTrainingItemNoSelectResourcesList(Guid trainingItemCourseID, int pageIndex, int pageSize, string criteria, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
