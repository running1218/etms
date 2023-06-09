//==================================================================================================
//Version 1.0, auto-generated.
//Generated By huangzf.
//Date: 2013-3-15 17:28:33.
//==================================================================================================
//==================================================================================================
//This file is generated by CodeGenerator(Ver 2.1)
//==================================================================================================

using System;
using System.Collections.Generic;
using System.Data;
using ETMS.Utility.Logging;
using ETMS.Components.Basic.API.Entity.Teacher;
using ETMS.Components.Basic.Implement.DAL.Teacher;
using System.Transactions;
namespace ETMS.Components.Basic.Implement.BLL.Teacher
{
    /// <summary>
    /// 讲师表业务逻辑
    /// </summary>
    public partial class Site_TeacherLogic
    {
        private static readonly Site_TeacherDataAccess DAL = new Site_TeacherDataAccess();

        /// <summary>
        /// 增加
        /// </summary>
        public void Add(Site_Teacher site_Teacher)
        {
            DAL.Add(site_Teacher);
            BizLogHelper.AddOperate(site_Teacher);
        }


        /// <summary>
        /// 保存
        /// </summary>
        public void Update(Site_Teacher site_Teacher)
        {
            //修改前信息
            Site_Teacher originalEntity = GetById(site_Teacher.TeacherID);
            DAL.Save(site_Teacher);
            BizLogHelper.UpdateOperate(originalEntity, site_Teacher);
        }

        /// <summary>
        /// 删除
        /// </summary>
        public void Remove(Int32 teacherID)
        {
            doRemove(teacherID);
        }

        /// <summary>
        /// 批量删除(主键ID数组）
        /// </summary>
        public void Remove(Int32[] teacherIDs)
        {
#if !DEBUG
			using (TransactionScope ts = new TransactionScope())
			{
#endif
            foreach (Int32 id in teacherIDs)
            {
                Remove(id);
            }
#if !DEBUG
				ts.Complete();
			}
#endif
        }


        /// <summary>
        /// 根据标识获取对象
        /// </summary>
        public Site_Teacher GetById(Int32 teacherID)
        {
            Site_Teacher site_Teacher = DAL.GetById(teacherID);
            if (site_Teacher == null)
            {
                throw new ETMS.AppContext.BusinessException("Teacher.Site_Teacher.NotFoundException", new object[] { teacherID });
            }

            return site_Teacher;
        }

        /// <summary>
        /// 查询数据列表分页数据
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

        /// <summary>
        /// 查询实体分页数据
        /// </summary>
        /// <param name="pageIndex">页号</param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="sortExpression">排序条件</param>
        /// <param name="criteria">筛选条件</param>
        /// <param name="totalRecords">out 记录总数</param>
        /// <returns>返回查询结果</returns>
        public IList<Site_Teacher> GetEntityList(int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {
            return DAL.GetEntityList(pageIndex, pageSize, sortExpression, criteria, out totalRecords);
        }

    }


}

