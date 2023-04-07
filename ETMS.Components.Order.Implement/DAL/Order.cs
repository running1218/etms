using System;
using ETMS.Utility.Data;
using System.Data;
using System.Data.SqlClient;
using ETMS.Components.Order.API.Entity;

namespace ETMS.Components.Order.Implement.DAL
{
    public class OrderDataAccess
    {
        /// <summary>
        /// generate order
        /// </summary>
        /// <param name="orderInfo"></param>
        /// <returns></returns>
        public string GenerateOrder(OrderInfo orderInfo)
        {
            string orderNo = string.Empty;

            using (SqlConnection sqlConn = new SqlConnection(ConnectionString.ETMSWrite))
            {
                if (sqlConn.State == ConnectionState.Closed)
                    sqlConn.Open();

                SqlTransaction sqlTrans = sqlConn.BeginTransaction();

                try
                {
                    orderNo = GenerateOrderInfo(orderInfo, sqlTrans);

                    foreach (OrderDetail productInfo in orderInfo.OrderDetail)
                    {
                        // order detail
                        GenerateOrderDetail(productInfo, orderNo, sqlTrans);
                    }

                    sqlTrans.Commit();
                }
                catch
                {
                    sqlTrans.Rollback();
                }
            }

            return orderNo;
        }

        /// <summary>
        /// order information
        /// </summary>
        /// <param name="orderInfo"></param>
        /// <param name="sqlTrans"></param>
        /// <returns></returns>
        private string GenerateOrderInfo(OrderInfo orderInfo, SqlTransaction sqlTrans)
        {
            string commandName = "Pr_OrderInfo_Insert";
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@OrderNo", SqlDbType.NVarChar,50),
					new SqlParameter("@OrderDescription", SqlDbType.NVarChar),
					new SqlParameter("@OrderStatus", SqlDbType.Int),
					new SqlParameter("@BuyNumber", SqlDbType.Int),
					new SqlParameter("@TotalPrice", SqlDbType.Decimal),
                    new SqlParameter("@PayFrom", SqlDbType.Int),
                    new SqlParameter("@UserID", SqlDbType.Int),
                    new SqlParameter("@PayerName", SqlDbType.NVarChar),
                    new SqlParameter("@CreateTime", SqlDbType.DateTime),
                    new SqlParameter("@PayTime", SqlDbType.DateTime),
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Direction = ParameterDirection.Output;
            parms[1].Value = orderInfo.OrderDescription;
            parms[2].Value = orderInfo.OrderStatus;
            parms[3].Value = orderInfo.BuyNumber;
            parms[4].Value = orderInfo.TotalPrice;
            parms[5].Value = orderInfo.PayFrom;
            parms[6].Value = orderInfo.UserID;
            parms[7].Value = orderInfo.PayerName;
            parms[8].Value = orderInfo.CreateTime;
            parms[9].Value = orderInfo.PayTime;
            
            SqlHelper.ExecuteNonQuery(sqlTrans, CommandType.StoredProcedure, commandName, parms);
            return parms[0].Value.ToString();
        }

        /// <summary>
        /// order detail
        /// </summary>
        /// <param name="productInfo"></param>
        /// <param name="productNo"></param>
        /// <param name="sqlTrans"></param>
        private void GenerateOrderDetail(OrderDetail productInfo, string orderNo, SqlTransaction sqlTrans)
        {
            string commandName = "Pr_OrderDetail_Insert";
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@OrderDetailID", SqlDbType.UniqueIdentifier),
					new SqlParameter("@OrderNo", SqlDbType.NVarChar),
					new SqlParameter("@ProductID", SqlDbType.UniqueIdentifier),
					new SqlParameter("@ProductPrice", SqlDbType.Decimal),
					new SqlParameter("@DiscountPrice", SqlDbType.Decimal),
					new SqlParameter("@Coupon", SqlDbType.Decimal),
                    new SqlParameter("@AgencyCode", SqlDbType.NVarChar, 20)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = productInfo.OrderDetailID;
            parms[1].Value = orderNo;
            parms[2].Value = productInfo.ProductID;
            parms[3].Value = productInfo.ProductPrice;
            parms[4].Value = productInfo.DiscountPrice;
            parms[5].Value = productInfo.Coupon;
            parms[6].Value = productInfo.AgencyCode;

            SqlHelper.ExecuteNonQuery(sqlTrans, CommandType.StoredProcedure, commandName, parms);
        }
              
        /// <summary>
        /// cancel order
        /// </summary>
        /// <param name="orderNo"></param>
        public void CancelOrder(string orderNo)
        {
            using (SqlConnection sqlConn = new SqlConnection(ConnectionString.ETMSWrite))
            {
                if (sqlConn.State == ConnectionState.Closed)
                    sqlConn.Open();

                SqlTransaction sqlTrans = sqlConn.BeginTransaction();

                try
                {
                    string commandName = "Pr_Order_Cancel";
                    SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
                    if (parms == null)
                    {
                        parms = new SqlParameter[] {
					new SqlParameter("@OrderNo", SqlDbType.NVarChar)};
                        SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
                    }

                    parms[0].Value = orderNo;
                    SqlHelper.ExecuteNonQuery(sqlTrans, CommandType.StoredProcedure, commandName, parms);
                    sqlTrans.Commit();
                }
                catch
                {
                    sqlTrans.Rollback();
                    throw;
                }
            }
        }

        public void UpdateOrderStatus(string orderNo, int orderStatus)
        {
            string commandName = "Pr_Order_PayStatus";
            #region Parameters

            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@OrderNo", SqlDbType.NVarChar), 
                    new SqlParameter("@OrderStatus", SqlDbType.Int)
                };
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = orderNo;
            parms[1].Value = orderStatus;

            #endregion
            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
        }

        /// <summary>
        /// 查询我的订单信息
        /// </summary>
        /// <param name="pageIndex">页号</param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="sortExpression">排序条件</param>
        /// <param name="UserID">用户ID</param>
        /// <param name="totalRecords">out 记录总数</param>
        /// <returns>返回查询结果</returns>
        public DataTable GetOrderList(int pageIndex, int pageSize,  int UserID, out int totalRecords)
        {
            string commandName = "dbo.Pr_GetUserOrderList";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@PageIndex", SqlDbType.Int),
					new SqlParameter("@PageSize", SqlDbType.Int),
					new SqlParameter("@UserID", SqlDbType.Int),
					new SqlParameter("@ReturnValue", SqlDbType.Int, 4, ParameterDirection.ReturnValue, false, 0, 0, String.Empty, DataRowVersion.Default, null)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = pageIndex;
            parms[1].Value = pageSize;
            parms[2].Value = UserID;
            #endregion
            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];
            totalRecords = (int)parms[3].Value;
            return dt;
        }

        /// <summary>
        /// 查询订单明细
        /// </summary>
        /// <param name="OrderNo">订单编号</param>
        /// <returns>返回查询结果</returns> 
        public DataTable GetOrderDetail(string OrderNo)
        { 
            string commandName = "dbo.Pr_Order_GetOrderDetailByOrderNo";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@OrderNo", SqlDbType.NVarChar)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }
            parms[0].Value = OrderNo;
            #endregion
            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];
            return dt;
        }

        /// <summary>
        /// 支付成功后给学员选课
        /// </summary>
        /// <param name="orderNo"></param>
        public void GenerateChooseCourse(string orderNo)
        {
            string commandName = "Pr_GenerateChooseCourse";
            #region Parameters

            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@OrderNo", SqlDbType.NVarChar)
                };
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = orderNo;

            #endregion
            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);

        }

        public DataTable GetOrderInfo(string OrderNo)
        {
            string commandName = @"SELECT [OrderNo],[OrderDescription],[OrderStatus],[BuyNumber],[TotalPrice],[PayFrom],[UserID]
                                    ,[PayerName],[CreateTime],[PayTime],[IsChooseCourse]
                                    FROM Order_Info 
                                    Where OrderNo = @OrderNo";
            #region Parameters
            SqlParameter[] parms = new SqlParameter[] {
                    new SqlParameter("@OrderNo", SqlDbType.NVarChar)
                };
                
            parms[0].Value = OrderNo;
            #endregion
            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.Text, commandName, parms).Tables[0];
            return dt;
        }

        public DataTable GetOrders(DateTime startTime, DateTime endTime, string userName, int orderStatus, int orgID, int pageIndex, int pageSize, out int totalRecords)
        {
            string commandName = "Pr_Order_Info_GetPayList";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@StartTime", SqlDbType.DateTime),
                    new SqlParameter("@EndTime", SqlDbType.DateTime),
                    new SqlParameter("@PayStatus", SqlDbType.Int),
                    new SqlParameter("@LoginName", SqlDbType.NVarChar,50),
                    new SqlParameter("@OrgID", SqlDbType.Int),
                    new SqlParameter("@PageIndex", SqlDbType.Int),
                    new SqlParameter("@PageSize", SqlDbType.Int),
                    new SqlParameter("@ReturnValue", SqlDbType.Int, 4, ParameterDirection.ReturnValue, false, 0, 0, String.Empty, DataRowVersion.Default, null)
                };
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = startTime;
            parms[1].Value = endTime;
            parms[2].Value = orderStatus;
            parms[3].Value = userName;
            parms[4].Value = orgID;
            parms[5].Value = pageIndex;
            parms[6].Value = pageSize;
            #endregion
            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];
            totalRecords = (int)parms[7].Value;
            return dt;
        }
    }
}
