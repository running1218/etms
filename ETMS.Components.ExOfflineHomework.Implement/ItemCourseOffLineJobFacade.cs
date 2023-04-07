using System;


namespace ETMS.Components.ExOfflineHomework.Implement
{
    using ETMS.AppContext.Component;
    using ETMS.Components.Basic.API;
    using ETMS.Components.ExOfflineHomework.Implement.BLL;



    public class ItemCourseOffLineJobFacade : DefaultComponent, ITrainingItemCourseResourcesFacade
    {


        Res_e_OffLineJobLogic logic = new Res_e_OffLineJobLogic();






        #region ITrainingItemCourseResourcesFacade ≥…‘±

        public int GetResourcesTotal(Guid courseID)
        {
            return -1;
        }

        public int GetTrainingItemResourcesTotal(Guid trainingItemCourseID)
        {
            return logic.GetOfflineJobTotalByTrainingItemCourseID(trainingItemCourseID);
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

