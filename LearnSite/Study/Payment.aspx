<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Payment.aspx.cs" Inherits="ETMS.Studying.Study.Payment" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    
    <style type="text/css">
        /*.qrcode img{
            position:absolute;
            left:200px;
            top:200px;
        }*/
        .pay-after {
            top: 0;
            left: 0;
            background: #fff;
            padding:8px 0px;
            width:100%;
            text-align:center;
        }
        .second {
            color:blue;
            padding-right:10px;
        }
        .c-time{color:red; font-weight:700;}

        .pay-tool-left {
            overflow:hidden;
            float:left;
        }
        .pay-notice {
            background-color:#fafafa;
            height:60px;
            width:100%;
        }
        .pay-notice p {
            font-size:16px;
            line-height:60px;
            text-indent:20px;
        }
        .pay-amount {
            color: red;
            font-weight: 700;
        }
        .pay-from {
            color:#fe8101;
            font-size:16px;
            text-indent:30px;
            padding-top:20px;
        }
        .pay-tool-left{
            width:250px;
            margin:50px 0px 0px 200px;
            height:300px;
            border:2px solid #ededed;
            border-radius:5px;
        }
        .pay-tool p{
            font-size:12px;
        }
        .pay-tool-left-code {        
            margin:25px 25px;
        }
        .pay-tool-desc p{
            background:#fafafa;
            height:46px;
            line-height:46px;
            text-align:center;
        }
        .pay-tool-right {
            overflow:hidden;
            float:left;
            margin:20px 0px 0px 100px;
        }
            .pay-tool-right img {               
                width:360px;
                height:500px;
            }
        .pay-product-name {
            float:right;
            padding-right:20px;
        }
    </style>
    
</head>
<body >
    <script lang="javascript" type="text/javascript" src="<%=ETMS.Utility.WebUtility.AppPath%>/Scripts/library/jquery-3.1.1.min.js"></script>
    <form id="form1" runat="server">
    <div class="qrcode">        
        <p class="pay-after" style="display:none;">
            <span>支付成功，系统将在</span><span class="c-time"></span>&nbsp;<span>秒后自动跳转至【我的精品课程】页面,或</span><a class="second" href='<%=ETMS.Utility.WebUtility.AppPath %>/study/excellentcourselearn.aspx'>点击此处</a>
        </p>
        <div class="pay-notice">
            <p>请您及时付款，以便订单尽快获得处理！应付金额 <span class="pay-amount">￥<asp:Literal ID="ltlAmount" runat="server"></asp:Literal></span><span class="pay-product-name">商品名称：<asp:Literal ID="ltlProductName" runat="server"></asp:Literal></span></p>
        </div>
        <p class="pay-from">
            微信支付
        </p>
        <div class="pay-tool">
            <div class="pay-tool-left">
                <div class="pay-tool-left-code">
                    <asp:Image ID="imgPayCode" runat="server" Width="200px" Height="200px" />
                </div>
                <div class="pay-tool-desc">
                    <p>请使用微信扫一扫，扫描二维码支付</p>
                </div>
            </div>
            <div class="pay-tool-right">
                <img alt="" src="../styles/images/common/webxin-pay.jpg" />
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hfOrderNo" runat="server" Value="Y20170509000005" />
    </form>

    <script lang="javascript">
        var AppPath = '<%=ETMS.Utility.WebUtility.AppPath%>';
        var interCount = 0;
        $(function () {
            interCount = setInterval(afterPay, 3000);
        });

        function afterPay() {
            $.ajax({
                url: AppPath + "/PublicService/PayOrder.ashx",
                type: 'POST',
                dataType: "json",
                data: { Method: "checkpaystatus", OrderNo: $('#hfOrderNo').val()},
                success: function (result) {
                    if (result == "1") {
                        $('.pay-after').css('display', 'block');
                        clearInterval(interCount);
                        $('.c-time').html(5);
                        jump(5);
                    }
                    else {
                        //do nothing
                    }
                },
                error: function (err) {
                    $('.pay-after').html('支付失败');
                }
            });
        }

        function jump(count) {
            window.setTimeout(function () {
                count--;
                if (count > 0) {
                    $('.c-time').html(count);
                    jump(count);
                } else {
                    location.href = AppPath + '/study/excellentcourselearn.aspx';
                }
            }, 1000);
        }
    </script>
</body>
</html>