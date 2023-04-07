using System;
using System.Data;
using ETMS.Components.Basic.Implement.DAL.Course;
using ETMS.Components.Basic.API.Entity.Course;

namespace ETMS.Components.Basic.Implement.BLL.Course
{
    public class CourseOpenRangeLogic
    {
        private CourseOpenRangeDataAccess courseOpenRangeDal = new CourseOpenRangeDataAccess();
        /// <summary>
        /// 跟据课程ID 返回开放组织机构
        /// </summary>
        /// <param name="courseID">课程ID</param>
        /// <param name="orgCount">返回组织机构数量</param>
        /// <returns></returns>
        public DataTable GetList(Guid courseID, out int orgCount)
        {
            return courseOpenRangeDal.GetList(courseID, out orgCount);
        }
        
        /// <summary>
        /// 增加
        /// </summary>
        public void Add(Res_CourseOpenRange res_CourseOpen)
        {
            courseOpenRangeDal.Add(res_CourseOpen);
        }
                
        /// <summary>
        /// 删除
        /// </summary>
        public void Remove(Guid courseID, int orgID)
        {
            courseOpenRangeDal.Remove(courseID, orgID);
        }
    }
}
