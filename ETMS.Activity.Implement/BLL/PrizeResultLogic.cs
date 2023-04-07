using ETMS.Activity.Implement.DAL;
using System;
using System.Data;

namespace ETMS.Activity.Implement.BLL
{
    public partial class PrizeResultLogic
    {
        private static readonly PrizeResultDataAccess DAL = new PrizeResultDataAccess();
        /// <summary>
        /// 获取项目奖区
        /// </summary>
        /// <param name="appraisalID"></param>
        /// <returns></returns>
        public DataTable GetPrizeRegion(Guid appraisalID)
        {
            return DAL.GetPrizeRegion(appraisalID);
        }

        public DataTable GetPrizeRegion(Guid appraisalID,int regionID)
        {
            return DAL.GetPrizeRegion(appraisalID);
        }
        /// <summary>
        /// 获取所在区名次
        /// </summary>
        /// <param name="appraisalID"></param>
        /// <param name="regionID"></param>
        /// <returns></returns>
        public DataTable GetPrizeResult(Guid appraisalID, int regionID, int total = 0)
        {
            return DAL.GetPrizeResult(appraisalID, regionID, total);
        }
    }
}
