using ETMS.Components.Basic.Implement.BLL.Course;
using ETMS.Components.Basic.Implement.BLL.Security;
using ETMS.Components.Order.Implement.BLL;
using ETMS.Utility;
using System.Web;

namespace ETMS.Studying.PublicService
{
    /// <summary>
    /// PayOrder 的摘要说明
    /// </summary>
    public class PayOrder : IHttpHandler
    {

        private HttpContext currentContext = null;
        public void ProcessRequest(HttpContext context)
        {
            currentContext = context;
            string method = currentContext.Request["Method"];
            if (string.IsNullOrEmpty(method))
            {
                ReturnResponseContent(JsonHelper.GetParametersInValidJson());
            }
            switch (method.ToLower())
            {
                case "code"://获取优惠券值
                    ReturnResponseContent(GetCodeSalesPrice());
                    break;
                case "cancel"://取消支付
                    CancelPay();
                    break;
                case "checkpaystatus":
                    ReturnResponseContent(CheckPayStatus());
                    break;
                default:
                    ReturnResponseContent(JsonHelper.GetParametersInValidJson());
                    break;
            }
        }
        /// <summary>
        /// 取消支付
        /// </summary>
        /// <returns></returns>
        private void CancelPay()
        {
            string OrderNo = currentContext.Request["OrderNo"];
            if (!string.IsNullOrEmpty(OrderNo))
            {
                var logic = new OrderLogic();
                logic.CancelOrder(OrderNo);
            }
        }
        /// <summary>
        /// 获取优惠券码优惠后的商品价格
        /// </summary>
        /// <returns></returns>
        private string GetCodeSalesPrice()
        {
            var result = "invalid";
            string ProductID = currentContext.Request["ProductID"];
            string AgencyCode = currentContext.Request["AgencyCode"];
            var agency = new Site_AgencyProductLogic().GetDiscountPriceByAgencyCode(ProductID.ToGuid(),AgencyCode);
            if (agency != null)
            {
                var course = new Res_CourseLogic().GetById(ProductID.ToGuid());
                decimal salesPrice = 0.00M;
                if (course != null)
                {
                    salesPrice = course.DiscountPrice - agency.DiscountPrice;
                }
                result = salesPrice.ToString();;
            }
            return result;
        }

        private string CheckPayStatus()
        {
            string OrderNo = currentContext.Request["OrderNo"];
            var result = new OrderLogic().GetOrderInfo(OrderNo);
            return result == null ? "0" : result.OrderStatus.ToString();
        }
        private void ReturnResponseContent(string content)
        {
            currentContext.Response.Clear();
            currentContext.Response.ContentType = "text/json";
            currentContext.Response.Write(content);
            currentContext.Response.End();
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}