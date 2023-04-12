<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master"
    AutoEventWireup="true" CodeFile="PaymentList.aspx.cs" Inherits="Fee_TeacherPay_PaymentList" %>

<%@ Register Src="Controls/PaymentList.ascx" TagName="PaymentList" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphBack" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
  
    <div class="dv_serviceTab">
        <div class="dv_Tabmenus">
            <ul>
                <li id="Tab_0" onclick="showTab('Tab_0', 'Div_Select_0','selected');setFrameHeight();">
                    <a onfocus="blur()"
                    href="javascript:void(0);"><span class="bj">未支付</span></a></li>
                <li id="Tab_1" onclick="showTab('Tab_1', 'Div_Select_1','selected');setFrameHeight();">
                    <a onfocus="blur()"
                    href="javascript:void(0);"><span class="bj">已支付</span></a></li>
            </ul>
        </div>
    </div>
    <div class="info">
        <div id="Div_Select_0" >
            <uc1:PaymentList ID="PaymentList1" runat="server" PayStatus="0" />
        </div>
        <div id="Div_Select_1" style="display:none;">
            <uc1:PaymentList ID="PaymentList2" runat="server" PayStatus="1"/>
        </div>
    </div>

<script language="javascript" type="text/javascript">
    $(function () {
        $("#Div_CourseInfo_0,#Div_CourseInfo_1").find("table:even").find("td:first").css("background", "#fbebec");
    })
    function CountRealCourseFee() {
        var countFee = 0;
        var isChoose = false;
        var inputs = document.getElementById("Div_CourseInfo_0").getElementsByTagName("input"); 
        for (var i = 0; i < inputs.length; i++) {
            var obj = inputs[i];
            if (obj.type == 'checkbox') {
                if (obj.checked) {
                    isChoose = true;
                    var tableObj = $(obj).parent().parent().parent().parent();
                    
                    countFee += Number(tableObj.find("tr:last").find("td:last").find("input[type='text']").val());
                }
            }
        }
        if (isChoose == false) {
            popAlertMsg("请选取讲师。", "提示信息", "");
        }
        else {
            popConfirmMsg("支付总金额" + countFee, "支付确认", function () {
                document.getElementById('<%= PaymentList1.getClientID() %>').click();
            })
        }

    }
    
</script>
</asp:Content>
