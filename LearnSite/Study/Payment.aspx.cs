using University.Mooc.AppContext;
using ETMS.Components.Basic.Implement.BLL.Course;
using ETMS.Components.Basic.Implement.BLL.Security;
using ETMS.Components.Order.API.Entity;
using ETMS.Components.Order.Implement.BLL;
using ETMS.Utility;
using System;
using System.Collections.Generic;
using System.Web;

namespace ETMS.Studying.Study
{
    public partial class Payment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int PayType = Request["PayType"].ToInt();
                string ProductID = Request["ProductID"];
                string AgencyCode = Request["AgencyCode"];
                //商品名称、商品原价、商品折扣价
                string ProductName = string.Empty;
                decimal ProductPrice = 0.00M;
                decimal DiscountPrice = 0.00M;
                var course = new Res_CourseLogic().GetById(ProductID.ToGuid());
                if (course != null)
                {
                    ProductName = course.CourseName;
                    DiscountPrice = course.DiscountPrice;
                    ProductPrice = course.Price;
                }
                var logic = new OrderLogic();
                //获取优惠券优惠额
                var agency = new Site_AgencyProductLogic().GetDiscountPriceByAgencyCode(ProductID.ToGuid(), AgencyCode);
                decimal CodeDiscountPrice = agency == null ? 0 : agency.DiscountPrice;
                //订单详细
                List<OrderDetail> detailList = new List<OrderDetail>();
                OrderDetail detail = new OrderDetail();
                detail.OrderDetailID = Guid.NewGuid();
                detail.ProductID = ProductID.ToGuid();
                detail.ProductPrice = ProductPrice;
                detail.DiscountPrice = DiscountPrice;
                detail.AgencyCode = AgencyCode;
                detail.Coupon = CodeDiscountPrice;
                detailList.Add(detail);
                //订单
                OrderInfo model = new OrderInfo();
                model.OrderDescription = ProductName;
                model.OrderStatus = 0;
                model.BuyNumber = 1;
                model.TotalPrice = DiscountPrice - CodeDiscountPrice; //支付金额
                model.PayFrom = PayType; //1：支付宝；2：微信
                model.CreateTime = DateTime.Now;
                model.PayerName = UserContext.Current.RealName;
                model.UserID = UserContext.Current.UserID; ;
                model.OrderDetail = detailList;
                string orderNo = logic.GenerateOrder(model);//产生订单号
                //下一句只在开发时使用，避免重复订单
                //orderNo = "T" + orderNo;

                double Amount = Convert.ToDouble(model.TotalPrice);//支付金额
                ltlAmount.Text = Amount.ToString();
                ltlProductName.Text = ProductName;

                // 金额为0，也认为要购买，但不需要接入支付
                if (Amount == 0)
                {
                    //更新订单状态
                    logic.UpdateOrderStatus(orderNo, 1);
                    //学生选课
                    logic.GenerateChooseCourse(orderNo);

                    this.Response.Redirect(string.Format("{0}/study/excellentcourselearn.aspx", Utility.WebUtility.AppPath));
                }
                else
                {
                    if (PayType == 1)//支付宝支付
                    {
                        PaymentHelper.CreateAlipayOrder(ProductName, Amount, orderNo, string.Format(PaymentHelper.PayNotifyUrl, BaseUtility.Domain));
                    }
                    else//微信支付
                    {
                        try
                        {
                            string url = PaymentHelper.MakeQRCode(ProductName, Amount, orderNo, string.Format(PaymentHelper.WeChatNotifyUrl, BaseUtility.Domain));//new NativePay().GetUnionPayUrl(ProductName, Amount, orderNo, string.Format(PaymentHelper.WeChatNotifyUrl, BaseUtility.Domain));
                            Utility.Logging.ErrorLogHelper.WriteLog(url);
                            imgPayCode.ImageUrl = WebUtility.AppPath + "/MakeQRCode.aspx?data=" + HttpUtility.UrlEncode(url);
                            hfOrderNo.Value = orderNo;
                        }
                        catch (Exception ex)
                        {
                            Utility.Logging.ErrorLogHelper.WriteLog(ex.ToString());
                        }
                    }
                }
            }
        }
    }
}