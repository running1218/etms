//==================================================================================================
//Version 1.0, auto-generated.
//Generated By running1218
//Date: 2012-4-10 10:24:30.
//==================================================================================================
//==================================================================================================
//This file is generated by CodeGenerator(Ver 2.1)
//==================================================================================================

using System;
using System.Collections.Generic;
using System.Data;
using ETMS.Utility.Logging;
using ETMS.Components.Basic.API.Entity.Security;
using ETMS.Components.Basic.Implement.DAL.Security;
namespace ETMS.Components.Basic.Implement.BLL.Security
{
    /// <summary>
    /// 系统配置业务逻辑
    /// </summary>
    public partial class Site_SysConfigLogic
    {
        private static readonly Site_SysConfigDataAccess DAL = new Site_SysConfigDataAccess();

        /// <summary>
        /// 增加
        /// </summary>
        public void Add(Site_SysConfig site_SysConfig)
        {
            DAL.Add(site_SysConfig);
            BizLogHelper.AddOperate(site_SysConfig);
        }


        /// <summary>
        /// 保存
        /// </summary>
        public void Update(Site_SysConfig site_SysConfig)
        {
            //修改前信息
            Site_SysConfig originalEntity = GetById(site_SysConfig.ConfigID);
            DAL.Save(site_SysConfig);
            BizLogHelper.UpdateOperate(originalEntity, site_SysConfig);
        }

        /// <summary>
        /// 删除
        /// </summary>
        public void Remove(Int32 configID)
        {
            doRemove(configID);
        }

        /// <summary>
        /// 批量删除(主键ID数组）
        /// </summary>
        public void Remove(Int32[] configIDs)
        {
#if !DEBUG
			using (TransactionScope ts = new TransactionScope())
			{
#endif
            foreach (Int32 id in configIDs)
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
        public Site_SysConfig GetById(Int32 configID)
        {
            Site_SysConfig site_SysConfig = DAL.GetById(configID);
            if (site_SysConfig == null)
            {
                throw new ETMS.AppContext.BusinessException("SysConfig.Site_SysConfig.NotFoundException", new object[] { configID });
            }

            return site_SysConfig;
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
        public IList<Site_SysConfig> GetEntityList(int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {
            return DAL.GetEntityList(pageIndex, pageSize, sortExpression, criteria, out totalRecords);
        }

    }

}




