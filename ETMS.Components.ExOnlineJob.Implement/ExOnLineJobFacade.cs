using System;
using ETMS.AppContext.Component;
using ETMS.Components.Basic.API;
using ETMS.Components.ExOnlineJob.Implement.BLL.ExOnlineJob;


namespace ETMS.Components.ExOnlineJob.Implement
{
    public class ExOnLineJobFacade : DefaultComponent, ICourseResourcesFacade, ITrainingItemCourseResourcesFacade
    {

        private static Ex_OnLineJobLogic Logic = new Ex_OnLineJobLogic();
        public int GetResourcesTotal(Guid courseID)
        {
            return Logic.Get_OnlineJobTotal(courseID);
        }

        public int GetALLResourcesTotal(Guid courseID)
        {
            return Logic.GetALLOnlineJobTotal(courseID);
        }

        public Basic.API.Entity.EnumResourcesType GetResourcesType()
        {
            return Basic.API.Entity.EnumResourcesType.OnLineJob;
        }

        public System.Data.DataTable GetResourcesList(Guid courseID, int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public int GetTrainingItemResourcesTotal(Guid trainingItemCourseID)
        {
            return Logic.GetItemCourseOnlineJobTotal(trainingItemCourseID);
        }


        public System.Data.DataTable GetTrainingItemResourcesList(Guid trainingItemCourseID, int pageIndex, int pageSize, string criteria, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        #region ITrainingItemCourseResourcesFacade 成员


        public System.Data.DataTable GetTrainingItemNoSelectResourcesList(Guid trainingItemCourseID, int pageIndex, int pageSize, string criteria, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
