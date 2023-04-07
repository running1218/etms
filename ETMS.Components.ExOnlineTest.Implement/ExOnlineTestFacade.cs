using System;
using ETMS.AppContext.Component;
using ETMS.Components.Basic.API;
using ETMS.Components.ExOnlineTest.Implement.BLL;


namespace ETMS.Components.ExOnlineTest.Implement
{
    public class ExOnlineTestFacade : DefaultComponent, ICourseResourcesFacade, ITrainingItemCourseResourcesFacade
    {
        private static Ex_OnLineTestLogic Logic = new Ex_OnLineTestLogic(); 
        public int GetResourcesTotal(Guid courseID)
        {
           return Logic.Get_OnlineTestTotal(courseID);
        }

        public int GetALLResourcesTotal(Guid courseID)
        {
            return Logic.GetALLOnlineTestTotal(courseID);
        }

        public Basic.API.Entity.EnumResourcesType GetResourcesType()
        {
            throw new NotImplementedException();
        }

        public System.Data.DataTable GetResourcesList(Guid courseID, int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public int GetTrainingItemResourcesTotal(Guid trainingItemCourseID)
        {
            return Logic.GetItemCourseOnlineTestTotal(trainingItemCourseID);
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
