//==================================================================================================
//Version 1.0, auto-generated.
//Generated By xueyb.
//Date: 2012-3-30 14:32:19.
//==================================================================================================
//==================================================================================================
//This file is generated by CodeGenerator(Ver 2.1), a product of ZhouYonghua(Zhou_Yonghua@163.com).
//==================================================================================================

using System;
using System.Data;
using ETMS.Utility.Logging;
using ETMS.Components.Basic.API.Entity.Security;
using ETMS.Components.Basic.Implement.DAL.Security;
namespace ETMS.Components.Basic.Implement.BLL.Security
{
    /// <summary>
    /// 学员信息(用户扩展表)业务逻辑
    /// </summary>
    public partial class Site_StudentLogic
    {
        private static readonly Site_StudentDataAccess DAL = new Site_StudentDataAccess();

        /// <summary>
        /// 增加
        /// </summary>
        public void Add(Site_Student site_Student)
        {
            DAL.Add(site_Student);
            BizLogHelper.AddOperate(site_Student);
        }

        /// <summary>
        /// 删除
        /// </summary>
        public void Remove(Int32 userID)
        {
            doRemove(userID);
        }

        /// <summary>
        /// 批量删除(主键ID数组）
        /// </summary>
        public void Remove(Int32[] userIDs)
        {
#if !DEBUG
			using (TransactionScope ts = new TransactionScope())
			{
#endif
            foreach (Int32 id in userIDs)
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
        public void Update(Site_Student site_Student)
        {
            //修改前信息
            Site_Student originalEntity = GetById(site_Student.UserID);
            DAL.Save(site_Student);
            BizLogHelper.UpdateOperate(originalEntity, site_Student);
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

