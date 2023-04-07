using System;
using System.Collections.Generic;
using System.Linq;
using ETMS.Activity.Implement.DAL;
using ETMS.Activity.Entity;
using ETMS.Utility;
using System.Data;

namespace ETMS.Activity.Implement.BLL
{
    public partial class ProductionLogic
    {
        private static readonly ProductionDataAccess DAL = new ProductionDataAccess();

        public List<Production> GetMyProductions(int userID)
        {
            return DAL.GetMyProductions(userID).ToList<Production>();
        }

        public List<Production> GetProductions(Guid siginUpID)
        {
            return DAL.GetProductions(siginUpID).ToList<Production>();
        }

        public int Insert(Production entity)
        {
            return DAL.Insert(entity);
        }

        public int Update(Production entity)
        {
            return DAL.Update(entity);
        }

        public int Mark(Production product)
        {
            return DAL.Mark(product);
        }

        public int Delete(Guid productionID)
        {
            return DAL.Delete(productionID);
        }

        public List<Marking> GetMarkingActivity(int organizationID, string title, DateTime beginTime, DateTime endTime, int pageIndex, int pageSize, out int totalRecords)
        {
            var activities = new AppraisalLogic().GetPageList(organizationID, title, beginTime, endTime, pageIndex, pageSize, out totalRecords).ToList<Marking>();

            foreach (var item in activities)
            {
                var markStatics = DAL.GetMarkingStatics(item.AppraisalID).ToList<MarkingStatics>()[0];
                item.SubmitNum = markStatics.SubmitNum;
                item.MarkingNum = markStatics.MarkingNum;

                item.MarkingGroup = GetMarkingDetail(item.AppraisalID);
            }

            return activities;
        }

        private List<MarkingDetailStatic> GetMarkingDetail(Guid apprisialID)
        {
            var result = DAL.GetMarkingDetailStatics(apprisialID).ToList<MarkingDetailStatic>();
            return result;
        }

        /// <summary>
        /// 获取活动+组+类型作品
        /// </summary>
        /// <param name="apprisialID"></param>
        /// <param name="groupID"></param>
        /// <param name="typeID"></param>
        /// <returns></returns>
        public List<UserProduction> GetProductions(Guid apprisialID, int groupID, int typeID)
        {
            return DAL.GetProductions(apprisialID, groupID, typeID).ToList<UserProduction>();
        }

        public Production GetProduction(Guid id)
        {
            var list = DAL.GetProduction(id).ToList<Production>();
            return list.Count > 0 ? list[0] : null;
        }

        public DataTable GetShowProductions(Guid apprisialID, int regionID, int pageSize)
        {
            return DAL.GetShowProductions(apprisialID, regionID, pageSize);
        }

        public List<Excellent> GetProductionsByAppraisalID(Guid appraisalID, string productionName, int pageIndex, int pageSize, out int totalRecords)
        {
            return DAL.GetProductionsByAppraisalID(appraisalID, productionName).PageList<Excellent>(pageIndex, pageSize, out totalRecords);
        }

        public int SetExcellent(Guid productionID, int isExcellent)
        {
            return DAL.SetExcellent(productionID, isExcellent);
        }

        public List<Excellent> GetPrizeByAppraisalID(Guid appraisalID, string productionName, int pageIndex, int pageSize, out int totalRecords)
        {
            return DAL.GetPrizeByAppraisalID(appraisalID, productionName).PageList<Excellent>(pageIndex, pageSize, out totalRecords);
        }

        public Excellent GetPrizeByProductionID(Guid productionID)
        {
            return DAL.GetPrizeByProductionID(productionID).ToList<Excellent>().SingleOrDefault();
        }

        public int SetPrize(Guid productionID, int prize, int userID)
        {
            PrizeResult entity = new PrizeResult();
            entity.ReusltID = Guid.NewGuid();
            entity.PrizeID = prize;
            entity.ProductID = productionID;
            entity.CreateTime = DateTime.Now;
            entity.CreatorID = userID;

            return new PrizeResultDataAccess().Insert(entity);
        }

        public int CancelPrize(Guid productionID)
        {
            return new PrizeResultDataAccess().Delete(productionID);
        }
    }
}
