<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPagePop.Master"
    AutoEventWireup="true" CodeFile="MoveOrganization.aspx.cs" Inherits="Security_Organization_MoveOrganization" %>

<%@ Register Src="../Controls/OrganizationDropDownList.ascx" TagName="OrganizationDropDownList"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h2 class="dv_title">
        <!-- 功能描述 -->
        调整机构
    </h2>
    <div class="dv_information">
        <table class="GridviewGray">
            <tr>
                <th>
                    待调整的机构：
                </th>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>
                    调整到机构：
                </th>
                <td>
                    <uc1:OrganizationDropDownList ID="OrganizationDropDownList1" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    <div class="dv_submit">
        <asp:Button ID="Button1" runat="server" Text="调整" SkinID="Tiao" OnClick="Button1_Click" /><asp:Button
            ID="btnReturn" runat="server" SkinID="Return" Text="取消" OnClientClick="closeWindow()" />
    </div>
</asp:Content>
