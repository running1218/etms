<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Default.master" AutoEventWireup="true" CodeBehind="Buy.aspx.cs" Inherits="ETMS.Studying.Buy" %>
<asp:Content ID="Content1" ContentPlaceHolderID="DefaultPlaceHolder" runat="server">
    <link href="<%=ETMS.Utility.WebUtility.AppPath%>/Styles/boutiqueCourse.css" type="text/css" rel="stylesheet" />
    <div class="paymentPage">
        <div class="paymentTitleBox">
            <div class="paymentTitle">
                    <span class="courseName">商品名称：<%=ProductName %></span>
                    <div class="right"><span class="moneyA">实付金额：</span><span class="moneys"><i>¥</i><span id="orderPrice"><%=ProductPrice %></span></span></div>
            </div>
        </div>

        <div class="patternOfPayment">
            <div class="patternOfPaymentContent">
                <div class="firstBox">
                    <div class="title">请选择支付方式</div>
                    <div class="pattern" id="pattern">
                        <a class="on" href="javascript:void(0);" id="alipay"><span class="pattern1">支付宝</span></a>
                        <a href="javascript:void(0);" id="weixinPay"><span class="pattern3">微信</span></a>
                    </div> 
                    <div class="title" style="height:10px;"><span class="discount-code">使用优惠码</span><input type="checkbox" id="codeOperation_icon" class="bottom" /></div>
                    <div class="code_input">
                        <input type="text" id="discount_code" />
                        <input type="button" value="确定" id="confirm" />
                        <span class="error_info"></span>
                    </div>
               </div>
                <div class="btn clearfix" id="PayConfirmSelect">
                      <input type="button" id="SubmitPay" class="confirm on" title="确认支付" value="确认支付">
                     <%-- <input type="button" id="CancelPay" class="cancel" title="取消支付" value="取消支付">--%>
                 </div>
            </div>
        </div>
    </div>
    <script>
        
        //支付方式切换
        $('#pattern a').click(function () {
            $(this).addClass('on').siblings().removeClass('on');
        })
        //优惠码
        $('#confirm').click(function () {
            var code = $('#discount_code').val();
            var productId = '<%= ProductID%>';
            $.ajax({
                url: AppPath + "/PublicService/PayOrder.ashx?Method=code&ProductID=" + productId + "&AgencyCode=" + code,
                type: 'get',
                cache: false,
                async: true,
                dataType: "text",
                success: function (result) {
                    if (result != "invalid") {
                        $('#orderPrice').text(result);
                    } else {
                        $('.error_info').text('此优惠码无效');
                        setTimeout(function () {
                            $('.error_info').hide().empty();
                        }, 3000)
                    };
                    
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    
                }
            });
        });
        //折叠&收起
        $("#codeOperation_icon").click(function () {
            if ($(this).hasClass('top')) {
                $(this).removeClass().addClass('bottom');
                $('.code_input').slideUp();
            } else {
                $(this).removeClass().addClass('top');
                $('.code_input').slideDown();
            }
        })
        //支付
        $("#SubmitPay").bind("click", function () {
            var payType = 1;
            var productId = '<%= ProductID%>';
            if ($("#pattern a[class='on']").attr("id") == "weixinPay")
            {
                payType = 2;
            }
            var code = $('#discount_code').val();
            window.open(AppPath + "/Study/Payment.aspx?PayType=" + payType + "&ProductID=" + productId + "&AgencyCode=" + code, "_self");
        });
    </script>
</asp:Content>
