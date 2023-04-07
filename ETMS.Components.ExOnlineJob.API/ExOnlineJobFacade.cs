using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;


namespace ETMS.Components.Courseware.Implement
{

    using ETMS.Utility;
    using ETMS.Utility.BizCache;
    using ETMS.AppContext.Component;
    using ETMS.Components.ExOnlineJob.API;
    using ETMS.Components.Basic.API;
    using ETMS.Components.Basic.Implement.DAL.Common;

    public class ExOnlineJobFacade : DefaultComponent, ICourseResourcesFacade
    {

        private static string sqlModal = @"select 
                Res_CourseRes.CourseID,Res_CourseRes.CourseResID,Res_CourseRes.CourseResTypeID,Res_CourseRes.ResBeginTime,Res_CourseRes.ResEndTime,
                Ex_OnLineJob.*
            from Res_CourseRes
                inner join Ex_OnLineJob on Ex_OnLineJob.OnLineJobID=Res_CourseRes.ResID
                where 1=1 and Res_CourseRes.CourseResTypeID='2'
                and Res_CourseRes.CourseID='{0}' {1}";

        #region ICourseResourcesFacade 成员

        public Int32 GetResourcesTotal(Guid courseID)
        {
            //如果有缓存直接从缓存取，否则调用业务逻辑取
            string key = string.Format("CourseExOnlineJob_{0}", courseID);
            return BizCacheHelper.GetOrInsertItem<Int32>(key, () =>
            {
                int totalRecords = 0;
                string sql = string.Format(sqlModal, courseID.ToString(), "");
                GetData.GetPagedListFromSQL(sql, 1, 0, "order by Res_CourseRes.CreateTime DESC", out totalRecords);
                return totalRecords;
            });
        }


        /// <summary>
        /// 返回某课程最近一个月内创建的资源数
        /// </summary>
        /// <param name="courseID"></param>
        /// <returns></returns>
        public int GetNewResourcesTotal(Guid courseID)
        {
            DateTime month = System.DateTime.Now.AddMonths(-1);
            string conditionSQL = " AND Res_CourseRes.CreateTime>= " + month.ToString("yyyy_MM-dd");
            int totalRecords = 0;
            string sql = string.Format(sqlModal, courseID.ToString(), conditionSQL);
            GetData.GetPagedListFromSQL(sql, 1, 0, "order by Res_CourseRes.CreateTime DESC", out totalRecords);
            return totalRecords;
        }

        public Basic.API.Entity.EnumResourcesType GetResourcesType()
        {
            return Basic.API.Entity.EnumResourcesType.OnLineJob;
        }



        /// <summary>
        ///获取某个课程的课件列表
        /// </summary>
        /// <param name="courseID"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="criteria"></param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        public DataTable GetResourcesList(Guid courseID, int pageIndex, int pageSize, string criteria, out int totalRecords)
        {
            string sql = string.Format(sqlModal, courseID.ToString(), criteria);
            return GetResourcesList(courseID, pageIndex, pageSize, "order by Res_CourseRes.CreateTime DESC", out totalRecords);
        }

        public DataTable GetResourcesList(Guid courseID, int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {
            if (sortExpression == "")
                sortExpression = "order by Res_CourseRes.CreateTime DESC";
            string sql = string.Format(sqlModal, courseID.ToString(), criteria);
            return GetData.GetPagedListFromSQL(sql, pageIndex, pageSize, sortExpression, out totalRecords);
        }


        #endregion



    
    
    }
}

