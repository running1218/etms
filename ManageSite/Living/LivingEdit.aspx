<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LivingEdit.aspx.cs" Inherits="Living_LivingEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    
</head>
<body>
    <link href="<%= ETMS.Utility.WebUtility.AppPath %>/JScript/DateTimePicker/jquery.datetimepicker.css" type="text/css" rel="Stylesheet" />
    <script lang="javascript" type="text/javascript" src="<%= ETMS.Utility.WebUtility.AppPath %>/JScript/jquery-1.11.1.min.js"></script>
    <script lang="javascript" type="text/javascript" src="<%= ETMS.Utility.WebUtility.AppPath %>/JScript/DateTimePicker/jquery.datetimepicker.js"></script>
    <script lang="javascript" type="text/javascript" src="<%=ETMS.Utility.WebUtility.AppPath%>/JScript/jsfunction.js"></script>
    <script lang="javascript" type="text/javascript" src="<%=ETMS.Utility.WebUtility.AppPath%>/JScript/ymPromptYuan.js"></script>
    
    <form id="form1" runat="server">
        <div class="dv_information">
            <table class="GridviewGray fixedTable">
                <tr>
                    <th><label class="colorRed">*</label>直播名称：</th>
                    <td>                    
                        <asp:TextBox ID="txtName" runat="server" MaxLength="100"  CssClass="inputbox_190"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvName" runat="server" ControlToValidate="txtName" ErrorMessage="请输入直播名称！" Display="None" Text="*" ForeColor="Red" ValidationGroup="Error"></asp:RequiredFieldValidator>
                    </td>                
                </tr>
                <tr>
                    <th><label class="colorRed">*</label>开始时间：</th>
                    <td>
                        <asp:TextBox ID="txtStartTime" runat="server" CssClass="inputbox_190 datetimepicker-icon"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvStartTime" runat="server" ControlToValidate="txtStartTime" ErrorMessage="请输入开始时间！" Display="None" Text="*" ForeColor="Red" ValidationGroup="Error"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <th><label class="colorRed">*</label>结束时间：</th>
                    <td>                    
                        <asp:TextBox ID="txtEndTime" runat="server" CssClass="inputbox_190 datetimepicker-icon"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvEndTime" runat="server" ControlToValidate="txtEndTime" ErrorMessage="请输入结束时间！" Display="None" Text="*" ForeColor="Red" ValidationGroup="Error"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <th><label class="colorRed">*</label>主播讲师：</th>
                    <td>
                        <asp:DropDownList ID="ddlTeacher" runat="server" Width="190px" Height="25px" CssClass="easyui-combobox"></asp:DropDownList>
                    </td>
                </tr>
            </table>
        </div>
        <div class="dv_submit">
            <asp:LinkButton ID="lbtnSave" runat="server" CssClass="btn_Save" OnClick="lbtnSave_Click"
                ValidationGroup="Error">保存</asp:LinkButton>
            <asp:ValidationSummary runat="server" ID="ValidationSummary1" ValidationGroup="Error"
                ShowMessageBox="true" ShowSummary="false" />
            <a href="javascript:closeWindow();" class="btn_Cancel padleft10">取消</a>
        </div> 

        <script lang="javascript" type="text/javascript">
            $('#<%=txtStartTime.ClientID%>').datetimepicker({ lang: 'ch', step: 30 });
            $('#<%=txtEndTime.ClientID%>').datetimepicker({ lang: 'ch', step: 30 });
            $(function () {
                isLoadFish();
            });
        </script>
    </form>
</body>
</html>
