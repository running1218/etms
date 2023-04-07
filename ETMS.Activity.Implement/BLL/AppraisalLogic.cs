using System;
using System.Collections.Generic;
using System.Linq;
using ETMS.Activity.Implement.DAL;
using ETMS.Activity.Entity;
using System.Data;
using ETMS.Utility;

namespace ETMS.Activity.Implement.BLL
{
    public partial class AppraisalLogic
    {
        private static readonly AppraisalDataAccess DAL = new AppraisalDataAccess();

        public DataTable GetPageList(int organizationID, string title, DateTime beginTime, DateTime endTime, int pageIndex, int pageSize, out int totalRecords)
        {
            return DAL.GetPageList(organizationID, title, beginTime, endTime, pageIndex, pageSize, out totalRecords);
        }

        public int Insert(Appraisal entity)
        {
            return DAL.Insert(entity);
        }

        public int Update(Appraisal entity)
        {
            return DAL.Update(entity);
        }

        public int Delete(Guid appraisalID)
        {
            return DAL.Delete(appraisalID);
        }

        public Appraisal GetAppraisalByID(Guid appraisalID)
        {
            return DAL.GetAppraisalByID(appraisalID).ToList<Appraisal>()[0];
        }

        public bool IsSingnUp(Guid appraisalID)
        {
            var data = DAL.IsSingnUp(appraisalID);
            return data.Rows[0]["Num"].ToInt() > 0;
        }

        public void Top(Guid appraisalID)
        {
            var entity = GetAppraisalByID(appraisalID);
            if (entity.IsTop)
            {
                entity.IsTop = false;
                Update(entity);
            }
            else
            {
                DAL.CancelOthersTop();
                entity.IsTop = true;
                Update(entity);
            }
        }

        /// <summary>
        /// 获取首页推荐活动，有推荐取推荐，没有推荐取最新开始时间的活动
        /// </summary>
        /// <param name="orgID"></param>
        /// <returns></returns>
        public Appraisal GetTopOne(int orgID)
        {
            var source = DAL.GetTopx(1, orgID);

            if (source.Rows.Count > 0)
                return source.ToList<Appraisal>()[0];
            else
                return null;
        }

        /// <summary>
        /// 获取活动首页列表
        /// </summary>
        /// <param name="orgID"></param>
        /// <returns></returns>
        public List<Appraisal> GetAppraisalList(int orgID, int pageIndex, int pageSize, out int totalRecords)
        {
            return DAL.GetAppraisalList(orgID).PageList<Appraisal>(pageIndex, pageSize, out totalRecords);
        }

        public DataTable GetAppraisal(Guid appraisalID) {
            return DAL.GetAppraisal(appraisalID);
        }
        public DataTable GetDicRegion(string Ids)
        {
            return DAL.GetDicRegion(Ids);
        }
        public DataTable GetDicGroup(string Ids)
        {
            return DAL.GetDicGroup(Ids);
        }

        public DataTable GetDicRegionList()
        {
            return DAL.GetDicRegionList();
        }
        public DataTable GetDicGroupList()
        {
            return DAL.GetDicGroupList();
        }
    }
}
