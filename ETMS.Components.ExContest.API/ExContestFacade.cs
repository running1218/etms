using System;
using System.Data;

namespace ETMS.Components.Courseware.Implement
{
    using ETMS.Utility.BizCache;
    using ETMS.AppContext.Component;
    using ETMS.Components.Basic.API;
    using ETMS.Components.Basic.Implement.DAL.Common;

    public class ExContestFacade : DefaultComponent, ICourseResourcesFacade
    {


        private static string sqlModal = @"select 
                Res_CourseRes.CourseID,Res_CourseRes.CourseResID,Res_CourseRes.CourseResTypeID,Res_CourseRes.ResBeginTime,Res_CourseRes.ResEndTime,
                 Ex_Contest.*
            from Res_CourseRes
                inner join Ex_Contest on Ex_Contest.ContestID=Res_CourseRes.ResID
                where 1=1 and Res_CourseRes.CourseResTypeID='4'
                and Res_CourseRes.CourseID='{0}' {1}";

        #region ICourseResourcesFacade ��Ա

        public Int32 GetResourcesTotal(Guid courseID)
        {
            //����л���ֱ�Ӵӻ���ȡ���������ҵ���߼�ȡ
            //config/BizCache.config�ж��建����ڲ���
            string key = "CourseExContestCount";
            return BizCacheHelper.GetOrInsertItem<Int32>(key, courseID.ToString(), () =>
            {
                int totalRecords = 0;
                string sql = string.Format(sqlModal, courseID.ToString(), "");
                GetData.GetPagedListFromSQL(sql, 1, 0, "order by Res_CourseRes.CreateTime DESC", out totalRecords);
                return totalRecords;
            });
        }


        /// <summary>
        /// ����ĳ�γ����һ�����ڴ�������Դ��
        /// </summary>
        /// <param name="courseID"></param>
        /// <returns></returns>
        public int GetALLResourcesTotal(Guid courseID)
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
            return Basic.API.Entity.EnumResourcesType.Contest;
        }



        /// <summary>
        ///��ȡĳ���γ̵Ŀμ��б�
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

