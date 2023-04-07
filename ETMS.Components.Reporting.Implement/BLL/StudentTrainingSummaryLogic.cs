using System;
using System.Collections.Generic;
using System.Linq;
using ETMS.Utility;
using ETMS.Components.Reporting.API.Entity;
using ETMS.Components.Reporting.Implement.DAL;
using ETMS.AppContext;
using System.Data;

namespace ETMS.Components.Reporting.Implement.BLL
{
    public partial class StudentTrainingSummaryLogic
    {
        private static readonly StudentTrainingSummaryDataAccess DAL = new StudentTrainingSummaryDataAccess();

        public List<StudentTrainingSummary> GetTrainingSummary(DateTime itemBeginTime, DateTime itemEndTime, string realName, string workerNo, string departmentName, string postName)
        {
            return DAL.GetStudentTrainingSummary(UserContext.Current.OrganizationID, itemBeginTime, itemEndTime, realName, workerNo, departmentName, postName).ToList<StudentTrainingSummary>();
        }

        public DataTable GetTrainingSummaryTab(DateTime itemBeginTime, DateTime itemEndTime, string realName, string workerNo, string departmentName, string postName)
        {
            return DAL.GetStudentTrainingSummary(UserContext.Current.OrganizationID, itemBeginTime, itemEndTime, realName, workerNo, departmentName, postName);
        }

        public List<StudentTrainingSummary> GetTrainingSummary(DateTime itemBeginTime, DateTime itemEndTime, string realName, string workerNo, string departmentName, string postName, int pageIndex, int pageSize, out int totalRecords)
        {
            return DAL.GetStudentTrainingSummary(UserContext.Current.OrganizationID, itemBeginTime, itemEndTime, realName, workerNo, departmentName, postName).PageList<StudentTrainingSummary>(pageIndex, pageSize, out totalRecords);
        }

        public DataTable GetAllOrderList(int isCheck,string OrderNo, int OrderStatus, string LoginName, string RealName, int OrganizationID,DateTime BeginTime, DateTime EndTime, int pageIndex, int PageSize, string SortExpression, out int totalRecords)
        {
            return DAL.GetAllOrderList(isCheck, OrderNo, OrderStatus, LoginName, RealName, OrganizationID, BeginTime, EndTime, pageIndex, PageSize, SortExpression, out totalRecords);
        }
    }
}
