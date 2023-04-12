<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPagePop.Master" AutoEventWireup="true" CodeFile="CloseIDP.aspx.cs" Inherits="IDP_ManageIDP_CloseIDP" %>

<%@ Register src="~/IDP/ManageIDP/Contorls/IDPInfoShow.ascx" tagname="IDPInfoShow" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="dv_information">
<uc1:IDPInfoShow ID="IDPInfoShow1" runat="server" />
<div class="dv_pageInformation">
        <table class="GridviewGray">
            <tr>
                <th>
                    学习计划完成率：
                </th>
                <td>
                    <asp:TextBox ID="txtCompletionRate" runat="server" CssClass="inputbox_60" MaxLength="3"></asp:TextBox>%
                    <font color="red">*</font><asp:RequiredFieldValidator ID="RequiredFieldValidatorType"
                    runat="server" ControlToValidate="txtCompletionRate" Display="None" ErrorMessage="请输入学习计划完成率！"
                    ValidationGroup="Error"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="学习计划完成率必须是0-100的正整数！" ValidationGroup="Error" Display="Dynamic" ControlToValidate="txtCompletionRate" ValidationExpression="^(?:0|[1-9][0-9]?|100)$"></asp:RegularExpressionValidator>
                    
                </td>
            </tr>
            <tr>
                <th>
                    评价：
                </th>
                <td>
                    <asp:TextBox ID="txtRemark" TextMode="MultiLine" runat="server" CssClass="inputbox_area300" Height="150" Width="320"></asp:TextBox>
                    <font color="red">*</font><asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                    runat="server" ControlToValidate="txtRemark" Display="None" ErrorMessage="请输入评价信息！"
                    ValidationGroup="Error"></asp:RequiredFieldValidator>
                </td>
            </tr>
        </table>
</div>
    </div>

    <div class="dv_submit">
    <asp:LinkButton ID="lbtnSave" runat="server" CssClass="btn_Save" OnClick="lbtnSave_Click"
        ValidationGroup="Error">关闭</asp:LinkButton>
    <asp:ValidationSummary runat="server" ID="ValidationSummary1" ValidationGroup="Error"
        ShowMessageBox="true" ShowSummary="false" />
    <a href="javascript:closeWindow();" class="btn_Cancel">取消</a>
</div>

</asp:Content>

