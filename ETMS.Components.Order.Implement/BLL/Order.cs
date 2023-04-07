using System;
using ETMS.Components.Order.API.Entity;
using ETMS.Components.Order.Implement.DAL;
using System.Data;
using ETMS.Utility;

namespace ETMS.Components.Order.Implement.BLL
{
    public class OrderLogic
    {
        private static readonly OrderDataAccess DAL = new OrderDataAccess();

        /// <summary>
        /// generalte order
        /// </summary>
        /// <param name="orderInfo"></param>
        /// <returns></returns>
        public string GenerateOrder(OrderInfo orderInfo)
        {
            return DAL.GenerateOrder(orderInfo);
        }        

        /// <summary>
        /// cancel order
        /// </summary>
        /// <param name="orderNo"></param>
        public void CancelOrder(string orderNo)
        {
            DAL.CancelOrder(orderNo);
        }

        /// <summary>
        /// update order status and student choose course status
        /// </summary>
        /// <param name="orderNo"></param>
        /// <param name="payStatus"></param>
        public void UpdateOrderStatus(string orderNo, int orderStatus)
        {
            DAL.UpdateOrderStatus(orderNo, orderStatus);
        }
    
        /// <summary>
        /// 查询我的订单信息
        /// </summary>
        /// <param name="pageIndex">页号</param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="UserID">用户ID</param>
        /// <param name="totalRecords">out 记录总数</param>
        /// <returns>返回查询结果</returns>
        public DataTable GetOrderList(int pageIndex, int pageSize,int UserID, out int totalRecords)
        {
            return DAL.GetOrderList(pageIndex, pageSize, UserID, out totalRecords);
        }

        /// <summary>
        /// 查询订单明细
        /// </summary>
        /// <param name="pageIndex">页号</param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="OrderNo">订单编号</param>
        /// <param name="totalRecords">out 记录总数</param>
        /// <returns>返回查询结果</returns>
        public DataTable GetOrderDetail(string OrderNo)
        {
            return DAL.GetOrderDetail(OrderNo);
        }

        /// <summary>
        /// 支付成功后给学生自动选课
        /// </summary>
        /// <param name="orderNo"></param>
        /// <returns></returns>
        public void GenerateChooseCourse(string orderNo)
        {
            DAL.GenerateChooseCourse(orderNo);
        }

        public OrderInfo GetOrderInfo(string orderNo)
        {
            DataTable result = DAL.GetOrderInfo(orderNo);
            if (null != result && result.Rows.Count > 0)
                return result.Rows[0].ToEntity<OrderInfo>();

            return null;
        }

        public DataTable GetOrders(DateTime startTime, DateTime endTime, string userName, int orderStatus, int orgID, int pageIndex, int pageSize, out int totalRecords)
        {
            return DAL.GetOrders(startTime, endTime, userName, orderStatus, orgID, pageIndex, pageSize, out totalRecords);
        }
    }
}
