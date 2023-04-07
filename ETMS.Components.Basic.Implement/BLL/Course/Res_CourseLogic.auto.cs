using System;
using System.Collections.Generic;

using ETMS.Utility.Logging;
using ETMS.Components.Basic.API.Entity.Course;
using ETMS.Components.Basic.Implement.DAL.Course;
using System.Transactions;
using System.Data;

namespace ETMS.Components.Basic.Implement.BLL.Course
{
    /// <summary>
    /// 课程表业务逻辑
    /// </summary>
    public partial class Res_CourseLogic
    {
        private static readonly Res_CourseDataAccess DAL = new Res_CourseDataAccess();                

        /// <summary>
        /// 批量删除
        /// </summary>
        public void Remove(object[] selectedValues)
        {
            foreach (object obj in selectedValues)
            {
                Remove((Guid)obj);
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        public void Remove(Res_Course res_Course)
        {
            Remove(res_Course.CourseID);
            BizLogHelper.DeleteOperate(res_Course);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        public void Remove(List<Res_Course> res_Courses)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                foreach (Res_Course res_Course in res_Courses)
                {
                    Remove(res_Course);
                }

                ts.Complete();
            }
        }                

        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="pageIndex">页号</param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="sortExpression">排序条件</param>
        /// <param name="criteria">筛选条件</param>
        /// <param name="totalRecords">out 记录总数</param>
        /// <returns>返回查询结果</returns>
        public DataTable GetPagedList(int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {
            return DAL.GetPagedList(pageIndex, pageSize, sortExpression, criteria, out totalRecords);
        }

        public DataTable GetTeacherCoursesPagedList(int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {
            return DAL.GetTeacherCoursesPagedList(pageIndex, pageSize, sortExpression, criteria, out totalRecords);
        }
    }
}


