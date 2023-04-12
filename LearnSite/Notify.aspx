<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Notify.aspx.cs" Inherits="ETMS.Studying.Study.PayNotify" %>
<!DOCTYPE html>
<html lang="zh-CN">
    <head id="Head1" runat="server">
        <title>支付消息通知</title>
        <style type="text/css">
            .qrcode img{
                position:absolute;
                left:40%;
                top:40%;
            }
            .pay-after {
                position: absolute;
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
        </style>
    </head>
    <body style="background:#333;">
        <script lang="javascript" type="text/javascript" src="<%=ETMS.Utility.WebUtility.AppPath%>/Scripts/library/jquery-3.1.1.min.js"></script>
        <form id="form1" runat="server">
            <div>
                <p class="pay-after" style="display:none;">
                    <span>支付成功，系统将在</span><span class="c-time"></span>&nbsp;<span>秒后自动跳转至【我的精品课程】页面,或</span><a class="second" href='<%=ETMS.Utility.WebUtility.AppPath %>/study/excellentcourselearn.aspx'>点击此处</a>
                </p>
            </div>
            <asp:HiddenField ID="hfPayNo" runat="server" />
        </form> 
        
        <script lang="javascript">
            var AppPath = '<%=ETMS.Utility.WebUtility.AppPath%>';

            $(document).ready(function () {
                afterPay();
            });
            function afterPay() {
                $.ajax({
                    url: AppPath + "/PublicService/PayOrder.ashx",
                    type: 'POST',
                    dataType: "json",
                    data: { Method: "checkpaystatus", OrderNo: $('#hfPayNo').val() },
                    success: function (result) {
                        if (result == "1") {
                            $('.pay-after').css('display', 'block');
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
