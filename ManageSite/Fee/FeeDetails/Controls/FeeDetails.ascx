<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FeeDetails.ascx.cs" Inherits="Fee_FeeDetails_Controls_FeeDetails" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>

<div class="dv_information">
<p  class="colorRed" style="text-indent:20px;">注：“PR单号”是指第三方财务系统的报销单号，属选填项。</p>
    <table class="GridviewGray">
        <tr>
            <th width="100">项目名称：</th>
            <td colspan="3"><asp:DropDownList ID="ddlItemID" CssClass="select_190" runat="server">
            </asp:DropDownList></td>
            
        </tr>
        <tr id="trFeeCostDetailNo" runat="server">
            <th>流 水 号：</th>
            <td colspan="3">
            <asp:Literal ID="ltlFeeCostDetailNo" runat="server"></asp:Literal></td>
        </tr>
        <tr>
            <th >流水名称：</th>
            <td colspan="3">
                <asp:TextBox ID="txtFeeName" runat="server" CssClass="inputbox_120"></asp:TextBox><font color="red">*</font>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" Text="*" Display="None" runat="server" ErrorMessage="请填写流水名称！" ControlToValidate="txtFeeName" ValidationGroup="Saves"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <th >金&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;额：</th>
            <td >
            <cc1:CustomTextBox ID="txtAmount" runat="server"  CssClass="inputbox_120" MaxLength="12" ContentType="Decimal" ValidationGroup="Saves" />
            <%--<asp:TextBox ID="txtAmount" runat="server"  CssClass="inputbox_60" MaxLength="6"></asp:TextBox>--%>元<font color="red">*</font>
            
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Text="*" Display="None" runat="server" ErrorMessage="请填写金额！" ControlToValidate="txtAmount" ValidationGroup="Saves"></asp:RequiredFieldValidator>    
          <%--  <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="金额必须是2位小数的正数！" ValidationGroup="Saves" Display="None" ControlToValidate="txtAmount" ValidationExpression="\d{1,6}(\.\d{2})?"></asp:RegularExpressionValidator>--%>
            </td>
            <th width="100">经 手 人：</th>
            <td >
            <asp:TextBox ID="txtHandler" runat="server" CssClass="inputbox_120"></asp:TextBox><font color="red">*</font>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" Text="*" Display="None" runat="server" ErrorMessage="请填写经手人！" ControlToValidate="txtHandler" ValidationGroup="Saves"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <th>发生日期：</th>
            <td>
                <cc1:DateTimeTextBox ID="txtCostDate" runat="server"></cc1:DateTimeTextBox><font color="red">*</font>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" Text="*" Display="None" runat="server" ErrorMessage="请填写发生日期！" ControlToValidate="txtCostDate" ValidationGroup="Saves"></asp:RequiredFieldValidator>
            </td>
            <th>用&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;途：</th>
            <td>
                <asp:TextBox ID="txtPurpose" runat="server" CssClass="inputbox_120"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th>PR 单 号：</th>
            <td>
               <asp:TextBox ID="txtPRNo" runat="server" CssClass="inputbox_120"></asp:TextBox>
            </td>
            <th>已有发票：</th>
            <td>
                <cc1:DictionaryRadioButtonList runat="server" ID="rbtnIsGetInvoice" DictionaryType='Dic_TrueOrFalseBool'/>
            </td>
        </tr>
        <tr>
            <th>报销日期：</th>
            <td colspan="3">
                <cc1:DateTimeTextBox ID="txtReimbursementDate" runat="server"></cc1:DateTimeTextBox>
            </td>
            
        </tr>
        <tr>
            <th>备&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;注：</th>
            <td colspan="3">
                <asp:TextBox ID="txtRemark" TextMode="MultiLine" Height="100"  CssClass="inputbox_area360" runat="server"></asp:TextBox></td>
        </tr>  
    </table>
    
</div>
<div class="dv_submit">
    <asp:LinkButton ID="lbnSave" runat="server" CssClass="btn_Save" onclick="lbnSave_Click"  ValidationGroup="Saves" >保存</asp:LinkButton>
    <asp:ValidationSummary runat="server" ID="ValidationSummary1" ValidationGroup="Saves"
        ShowMessageBox="true" ShowSummary="false" />
        <a href="javascript:closeWindow();" class="btn_Cancel padleft10">取消</a>
</div>