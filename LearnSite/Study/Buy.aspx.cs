using ETMS.Components.Basic.API.Entity.Course;
using ETMS.Components.Basic.Implement.BLL.Course;
using ETMS.Components.Order.API.Entity;
using ETMS.Components.Order.Implement.BLL;
using ETMS.Utility;
using System;
using System.Collections.Generic;
using University.Mooc.AppContext;

namespace ETMS.Studying
{
    public partial class Buy : System.Web.UI.Page
    {
        /// <summary>
        /// 商品ID
        /// </summary>
        public string ProductID
        {
            get
            {
                return Request.QueryString["CourseID"];
            }
        }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string ProductName
        {
            get
            {
                if (ViewState["ProductName"] == null)
                    ViewState["ProductName"] = string.Empty;
                return ViewState["ProductName"].ToString();
            }
            set
            {
                ViewState["ProductName"] = value;
            }
        }
        /// <summary>
        /// 商品价格
        /// </summary>
        public string ProductPrice
        {
            get
            {
                if (ViewState["ProductPrice"] == null)
                    ViewState["ProductPrice"] = string.Empty;
                return ViewState["ProductPrice"].ToString();
            }
            set
            {
                ViewState["ProductPrice"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            var course = new Res_CourseLogic().GetById(ProductID.ToGuid());
            if (course != null)
            {
                ProductName = course.CourseName;
                ProductPrice = course.DiscountPrice.ToString();

                if (course.DiscountPrice == 0)
                {
                    DirectBuy(course);
                }
            }
        }

        private void DirectBuy(Res_Course course)
        {
            //不需要支付，支付类型设为未知
            int PayType = 0;
            //商品名称、商品原价、商品折扣价
            string ProductName = string.Empty;
            decimal ProductPrice = 0.00M;
            decimal DiscountPrice = 0.00M;

            if (course != null)
            {
                ProductName = course.CourseName;
                DiscountPrice = course.DiscountPrice;
                ProductPrice = course.Price;
            }
            var logic = new OrderLogic();

            //订单详细
            List<OrderDetail> detailList = new List<OrderDetail>();
            OrderDetail detail = new OrderDetail();
            detail.OrderDetailID = Guid.NewGuid();
            detail.ProductID = ProductID.ToGuid();
            detail.ProductPrice = ProductPrice;
            detail.DiscountPrice = DiscountPrice;
            detail.AgencyCode = string.Empty;
            detail.Coupon = 0;
            detailList.Add(detail);
            //订单
            OrderInfo model = new OrderInfo();
            model.OrderDescription = ProductName;
            model.OrderStatus = 0;
            model.BuyNumber = 1;
            model.TotalPrice = DiscountPrice - 0; //支付金额
            model.PayFrom = PayType; //1：支付宝；2：微信
            model.CreateTime = DateTime.Now;
            model.PayerName = UserContext.Current.RealName;
            model.UserID = UserContext.Current.UserID; ;
            model.OrderDetail = detailList;
            string orderNo = logic.GenerateOrder(model);//产生订单号

            // 金额为0，也认为要购买，但不需要接入支付
            //更新订单状态
            logic.UpdateOrderStatus(orderNo, 1);
            //学生选课
            logic.GenerateChooseCourse(orderNo);

            this.Response.Redirect(string.Format("{0}/study/excellentcourselearn.aspx", Utility.WebUtility.AppPath));
        }
    }
}