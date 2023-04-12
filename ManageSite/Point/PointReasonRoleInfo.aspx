<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPagePop.Master" AutoEventWireup="true"
    CodeFile="PointReasonRoleInfo.aspx.cs" Inherits="Point_PointReasonRoleInfo" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="dv_information">
        <table class="GridviewGray">
            <tr>
                <th>
                    积分原因类型：
                </th>
                <td>
                    <asp:DropDownList ID="ddlPointReasonTypeID" runat="server" />
                    <%--<cc1:DictionaryDropDownList ID="ddlPointReasonTypeID" DictionaryType="Dic_PointReasonType"
                        runat="server" IsShowChoose="true" />--%>
                    <span style="color: Red;">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidatorPointReasonTypeID"
                        runat="server" ErrorMessage="请填写积分原因分类！" ControlToValidate="ddlPointReasonTypeID"
                        ValidationGroup="Edit" Display="None"></asp:RequiredFieldValidator>
                </td>
            </tr>
             <tr>
                <th>
                    积分原因：
                </th>
                <td>
                    <asp:TextBox ID="txtPointReason" runat="server" CssClass="inputbox_300" MaxLength="50"></asp:TextBox>
                    <span style="color: Red;">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidatorPointReason"
                        runat="server" ErrorMessage="请填写积分原因！" ControlToValidate="txtPointReason" ValidationGroup="Edit" Display="None"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <th>
                    状&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;态：
                </th>
                <td>
                    <cc1:DictionaryRadioButtonList DictionaryType="Dic_Status" runat="server" ID="rbnStatus" />
                </td>
            </tr>
            <tr>
                <th>
                    积&nbsp;&nbsp;分&nbsp;&nbsp;值：
                </th>
                <td>
                    <asp:TextBox ID="txtGivePoints" runat="server" CssClass="inputbox_60" MaxLength="5"></asp:TextBox>[若扣分，请输入负数。]
                    <span style="color: Red;">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidatorGivePoints"
                        runat="server" ErrorMessage="请填写积分值！" ControlToValidate="txtGivePoints" ValidationGroup="Edit" Display="None"/>
                    <asp:RegularExpressionValidator ValidationExpression="^-?[1-9]\d*$" ControlToValidate="txtGivePoints" ID="RegularExpressionValidator1" runat="server" ErrorMessage="积分值格式错误！" ValidationGroup="Edit" Display="None" />
                </td>
            </tr>
           
        </table>
    </div>
    <div class="dv_submit">
        <asp:Button ID="btnUpdate" runat="server" Text="保存" CssClass="btn_Save" OnClick="btnUpdate_Click"
            CommandName="edit" ValidationGroup="Edit" />
        <asp:ValidationSummary runat="server" ID="ValidationSummary1" ValidationGroup="Edit"
            ShowMessageBox="true" ShowSummary="false" />
        <asp:Button ID="btnReturn" SkinID="Return" runat="server" Text="返回" OnClientClick="closeWindow()"
            CssClass="btn_Cancel" />
    </div>
</asp:Content>
