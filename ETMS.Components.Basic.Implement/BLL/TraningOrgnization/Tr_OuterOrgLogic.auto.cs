//==================================================================================================
//Version 1.0, auto-generated.
//Generated By huangzf.
//Date: 2013-3-15 15:40:47.
//==================================================================================================
//==================================================================================================
//This file is generated by CodeGenerator(Ver 2.1), a product of ZhouYonghua(Zhou_Yonghua@163.com).
//==================================================================================================

using System;
using System.Collections.Generic;
using System.Data;
using ETMS.Utility.Logging;
using ETMS.Components.Basic.API.Entity.TraningOrgnization;
using ETMS.Components.Basic.Implement.DAL.TraningOrgnization;
namespace ETMS.Components.Basic.Implement.BLL.TraningOrgnization
{
    /// <summary>
    /// 外部培训机构表业务逻辑
    /// </summary>
    public partial class Tr_OuterOrgLogic
    {
        private static readonly Tr_OuterOrgDataAccess DAL = new Tr_OuterOrgDataAccess();

        /// <summary>
        /// 增加
        /// </summary>
        public void Add(Tr_OuterOrg tr_OuterOrg)
        {
            DAL.Add(tr_OuterOrg);
            BizLogHelper.AddOperate(tr_OuterOrg);
        }


        /// <summary>
        /// 保存
        /// </summary>
        public void Update(Tr_OuterOrg tr_OuterOrg)
        {
            //修改前信息
            Tr_OuterOrg originalEntity = GetById(tr_OuterOrg.OuterOrgID);
            DAL.Save(tr_OuterOrg);
            BizLogHelper.UpdateOperate(originalEntity, tr_OuterOrg);
        }

        /// <summary>
        /// 删除
        /// </summary>
        public void Remove(Guid outerOrgID)
        {
            doRemove(outerOrgID);
        }

        /// <summary>
        /// 批量删除(主键ID数组）
        /// </summary>
        public void Remove(Guid[] outerOrgIDs)
        {
#if !DEBUG
			using (TransactionScope ts = new TransactionScope())
			{
#endif
            foreach (Guid id in outerOrgIDs)
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
        public Tr_OuterOrg GetById(Guid outerOrgID)
        {
            Tr_OuterOrg tr_OuterOrg = DAL.GetById(outerOrgID);
            if (tr_OuterOrg == null)
            {
                throw new ETMS.AppContext.BusinessException("TraningOrgnization.Tr_OuterOrg.NotFoundException", new object[] { outerOrgID });
            }

            return tr_OuterOrg;
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
        public IList<Tr_OuterOrg> GetEntityList(int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {
            return DAL.GetEntityList(pageIndex, pageSize, sortExpression, criteria, out totalRecords);
        }

    }


}

