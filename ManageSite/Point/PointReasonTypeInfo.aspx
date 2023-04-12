<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPagePop.Master" AutoEventWireup="true"
    CodeFile="PointReasonTypeInfo.aspx.cs" Inherits="Point_PointReasonTypeInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="dv_information">
        <table class="GridviewGray">           
            <tr>
                <th>
                    积分原因类型：
                </th>
                <td>
                    <asp:TextBox ID="txtPointReasonTypeName" runat="server" MaxLength="50" ></asp:TextBox>
                     <span style="color: Red;">*</span><asp:RequiredFieldValidator
                        ID="RequiredFieldValidatorPointReasonTypeID" runat="server" ErrorMessage="请填写积分原因类型！" ControlToValidate="txtPointReasonTypeName"
                        ValidationGroup="Edit"></asp:RequiredFieldValidator> 
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
