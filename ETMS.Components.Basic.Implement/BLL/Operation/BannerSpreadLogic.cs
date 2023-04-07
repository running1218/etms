using ETMS.Components.Basic.API.Entity.Operation;
using ETMS.Components.Basic.Implement.DAL.Operation;
using ETMS.Utility;
using ETMS.Utility.Logging;
using System;
using System.Data;

namespace ETMS.Components.Basic.Implement.BLL.Operation
{
    public class BannerSpreadLogic
    {
        private BannerSpreadDataAccess DAL = new BannerSpreadDataAccess();
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="bannerSpread">业务实体</param>
		public void Insert(BannerSpread bannerSpread)
        {
            DAL.Insert(bannerSpread);
            BizLogHelper.AddOperate(bannerSpread);
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="bannerSpread">业务实体</param>
		public void Update(BannerSpread bannerSpread)
        {
            BannerSpread originalEntity = GetByID(bannerSpread.BannerSpreadID);
            DAL.Update(bannerSpread);
            BizLogHelper.UpdateOperate(originalEntity, bannerSpread);
        }
        /// <summary>
        /// 获取顺序的最大值
        /// </summary>
        /// <returns></returns>
        public int GetMaxOrderValue()
        {
            return DAL.GetMaxOrderValue();
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="bannerSpread">业务实体</param>
        public void Remove(BannerSpread bannerSpread)
        {
            Remove(bannerSpread.BannerSpreadID);
        }

        /// <summary>
        /// 根据主键删除业务实体
        /// </summary>
        /// <param name="bannerSpreadID">Banner推广ID</param>
        public void Remove(Guid bannerSpreadID)
        {
            BannerSpread originalEntity = GetByID(bannerSpreadID);
            DAL.Remove(bannerSpreadID);
            BizLogHelper.DeleteOperate(originalEntity);
        }
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="SpreadName">推广名称</param>
        /// <param name="PublishStatus">发布状态</param> 
        /// <returns>返回查询结果</returns>
        public DataTable GetPageList(string SpreadName, string PublishStatus, int orgID)
        {
            return DAL.GetPageList(SpreadName, PublishStatus, orgID);
        }
        /// <summary>
        /// 根据Banner推广ID修改Banner排序号
        /// </summary>
        /// <param name="bannerSpreadID">Banner推广ID</param>
        /// <param name="orderNum">排序号</param>
        public void UpdateOrderNumByBannerSpreadID(Guid bannerSpreadID, int orderNum)
        {
            DAL.UpdateOrderNumByBannerSpreadID(bannerSpreadID, orderNum);
        }
        /// <summary>
        /// 根据主键ID查询数据
        /// </summary>
        /// <param name="BannerSpreadID">推广ID</param>
        public BannerSpread GetByID(Guid BannerSpreadID)
        {
            DataTable dt= DAL.GetByID(BannerSpreadID);
            return dt.Rows.Count > 0 ? dt.Rows[0].ToEntity<BannerSpread>() : null;
        }
        /// <summary>
        /// 查询首页Banner信息
        /// </summary>
        public DataTable GetBannerList(int orgID)
        {
            return DAL.GetBannerList(orgID);
           
        }
        
    }
}
