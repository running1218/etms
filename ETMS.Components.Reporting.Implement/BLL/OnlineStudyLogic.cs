using System.Collections.Generic;
using System.Linq;
using ETMS.Components.Reporting.API.Entity;
using ETMS.Components.Reporting.Implement.DAL;
using ETMS.Utility;
using System.Data;

namespace ETMS.Components.Reporting.Implement.BLL
{
    public partial class OnlineStudyLogic
    {
        private OnlineStudyDataAccess DAL = new OnlineStudyDataAccess();

        /// <summary>
        /// 在线课件学习监控报表
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="userOrganizationID"></param>
        /// <returns></returns>
        public List<OnlineIemCourseWareDetail> GetOnlineStudyCourseWareDetail(OnlineItemCourse entity, int userOrganizationID)
        {
            return DAL.GetOnlineStudyDetails(entity, userOrganizationID).ToList<OnlineIemCourseWareDetail>();
        }

        /// <summary>
        /// 在线学习汇总报表 学习情况与测试
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="userOrganizationID"></param>
        /// <returns></returns>
        public List<OnlineItemCourseStatistics> GetOnlineStudyStatisticsDetals(OnlineItemCourse entity, int userOrganizationID)
        {
            return DAL.GetOnlineStudyStatistics(entity, userOrganizationID).ToList<OnlineItemCourseStatistics>();
        }

        /// <summary>
        /// 在线学习情况监控 分页查询
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="userOrganizationID"></param>
        /// <param name="produrceName"></param>
        /// <returns></returns>
        public DataTable GetOnlineStudyPageList(OnlineItemCourse entity, int userOrganizationID, int pageIndex, int pageSize, out int totalRecords) {
            return DAL.GetOnlineStudyPageList(entity, userOrganizationID, pageIndex, pageSize, out totalRecords);
        }
       
        /// <summary>
        /// 在线学习情况监控 无分页 导出数据用
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="userOrganizationID"></param>
        /// <param name="produrceName"></param>
        /// <returns></returns>
        public DataTable GetOnlineStudyList(OnlineItemCourse entity, int userOrganizationID) { 
         return DAL.GetOnlineStudyList(entity,userOrganizationID);
        }

        /// <summary>
        /// 培训课程学习情况 分页查询
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="userOrganizationID"></param>
        /// <param name="produrceName"></param>
        /// <returns></returns>
        public DataTable GetTraningCourseLearnPageList(OnlineItemCourse entity, int userOrganizationID, int pageIndex, int pageSize, out int totalRecords)
        {
            return DAL.GetTraningCourseLearnPageList(entity, userOrganizationID, pageIndex, pageSize, out totalRecords);
        }
    }
}
