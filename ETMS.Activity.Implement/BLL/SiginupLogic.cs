using ETMS.Activity.Entity;
using ETMS.Activity.Implement.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using ETMS.Utility;
using System.Data;

namespace ETMS.Activity.Implement.BLL
{
    public partial class SiginupLogic
    {
        SiginupDataAccess DAL = new SiginupDataAccess();
        public int GetSiginupCount(Guid appraisalID)
        {
            return DAL.GetSiginupCount(appraisalID);
        }

        public string Insert(Siginup entity)
        {
            return DAL.Insert(entity);
        }

        public DataTable GetSiginup(int userid, Guid appraisalID)
        {
            return DAL.GetSiginup(userid, appraisalID);
        }

        /// <summary>
        /// 获取我参与的所有活动
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public List<MyActivityInfo> GetMyActivities(int userID)
        {
            return DAL.GetMyActivities(userID).ToList<MyActivityInfo>();
        }

        /// <summary>
        /// 获取我参与的，正在进行的活动
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public List<MyActivityInfo> GetMyActivitiesGoing(int userID)
        {
            List<MyActivityInfo> list = DAL.GetMyActivities(userID).ToList<MyActivityInfo>();
            return list.Where(f => f.EndTime >= DateTime.Now).ToList();
        }

        /// <summary>
        /// 获取我参与的，已经结束的活动
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public List<MyActivityInfo> GetMyActivitiesCompleted(int userID)
        {
            List<MyActivityInfo> list = DAL.GetMyActivities(userID).ToList<MyActivityInfo>();
            return list.Where(f => f.EndTime < DateTime.Now).ToList();
        }

        public MyActivityInfo GetMyActivity(Guid siginUpID)
        {
            return DAL.GetMyActivity(siginUpID).ToList<MyActivityInfo>()[0];
        }
    }
}
