using System;
using System.Collections.Generic;
using System.Linq;
using ETMS.Components.Basic.API.Entity.Security;
using ETMS.Components.Basic.Implement.DAL.Security;
using ETMS.Components.Basic.API.Entity.Course;
using ETMS.Components.Basic.Implement.BLL.Course;
using ETMS.Utility;
using System.Data;
using ETMS.AppContext;

namespace ETMS.Components.Basic.Implement.BLL.Security
{
    public class Site_AgencyProductLogic
    {
        private static readonly Site_AgencyProductDataAccess DAL = new Site_AgencyProductDataAccess();
        public List<Site_AgencyProduct> GetAgencyCourses(int agencyID, int pageIndex, int pageSize, out int totalRecords)
        {
            return DAL.GetAgencyCourses(agencyID).PageList<Site_AgencyProduct>(pageIndex, pageSize, out totalRecords);
        }

        public Site_AgencyProduct GetAgencyCourseByID(Guid agencyProductID)
        {
            DataTable reuslt = DAL.GetAgencyCourseByID(agencyProductID);
            if (reuslt.Rows.Count > 0)
                return reuslt.Rows[0].ToEntity<Site_AgencyProduct>();
            else
                return null;
        }

        public Site_AgencyProduct GetDiscountPriceByAgencyCode(Guid CourseID,string AgencyCode)
        {
            DataTable reuslt = DAL.GetAgencyCourseByAgencyCode(CourseID, AgencyCode);
            return reuslt.Rows.Count > 0 ? reuslt.Rows[0].ToEntity<Site_AgencyProduct>() : null;
        }
        
        public int Insert(Site_AgencyProduct entity)
        {
            var course = new Res_CourseLogic().GetById(entity.CourseID);
            if (course.DiscountPrice <= entity.DiscountPrice)
            {
                throw new BusinessException("优惠价格不能小于销售价格!");
            }
            return DAL.Insert(entity);
        }

        public int Delete(Guid agencyProductID)
        {
            return DAL.Delete(agencyProductID);
        }

        public List<Res_Course> GetUnAgencyCourses(int agencyID, int orgID)
        {
            return DAL.GetUnAgencyCourses(agencyID, orgID).ToList<Res_Course>();
        }
    }
}
