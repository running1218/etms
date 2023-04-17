
using System;
using System.Data;
using ETMS.Utility.Logging;
using ETMS.Components.Basic.API.Entity.Course.Resources;
using ETMS.Components.Basic.Implement.DAL.Course.Resources;
namespace ETMS.Components.Basic.Implement.BLL.Course.Resources
{
    /// <summary>
    /// 课程资源表业务逻辑
    /// </summary>
    public partial class Res_CourseResLogic
    {
        private static readonly Res_CourseResDataAccess DAL = new Res_CourseResDataAccess();

        /// <summary>
        /// 增加
        /// </summary>
        public void Add(Res_CourseRes res_CourseRes)
        {
            DAL.Add(res_CourseRes);
            BizLogHelper.AddOperate(res_CourseRes);
        }

        /// <summary>
        /// 删除
        /// </summary>
        public void Remove(Guid courseResID)
        {
            doRemove(courseResID);
        }

        /// <summary>
        /// 批量删除(主键ID数组）
        /// </summary>
        public void Remove(Guid[] courseResIDs)
        {
#if !DEBUG
			using (TransactionScope ts = new TransactionScope())
			{
#endif
            foreach (Guid id in courseResIDs)
            {
                Remove(id);
            }
#if !DEBUG
				ts.Complete();
			}
#endif
        }

        /// <summary>
        /// 保存
        /// </summary>
        public void Update(Res_CourseRes res_CourseRes)
        {
            //修改前信息
            Res_CourseRes originalEntity = GetById(res_CourseRes.CourseResID);
            DAL.Save(res_CourseRes);
            BizLogHelper.UpdateOperate(originalEntity, res_CourseRes);
        }

        /// <summary>
        /// 根据标识获取对象
        /// </summary>
        public Res_CourseRes GetById(Guid courseResID)
        {
            Res_CourseRes res_CourseRes = DAL.GetById(courseResID);
            if (res_CourseRes == null)
            {
                throw new ETMS.AppContext.BusinessException("Course.Resources.Res_CourseRes.NotFoundException", new object[] { courseResID });
            }

            return res_CourseRes;
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

    }


}

