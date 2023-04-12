<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPagePop.Master"
    AutoEventWireup="true" CodeFile="MoveDepartment.aspx.cs" Inherits="Security_Department_MoveDepartment" %>

<%@ Register Src="../Controls/DepartmentDropDownList.ascx" TagName="DepartmentDropDownList"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">    
    <div class="dv_information">
        <table  class="GridviewGray th120">
            <tr>
                <th width="120px">
                    待调整的<asp:Literal ID="ltlDepartment" runat="server" Text="<%$ Resources:UIResource, ui_department%>"></asp:Literal>：
                </th>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>
                    调整到<asp:Literal ID="ltlDepartment1" runat="server" Text="<%$ Resources:UIResource, ui_department%>"></asp:Literal>：
                </th>
                <td>
                    <uc1:DepartmentDropDownList ID="DepartmentDropDownList1" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    <div class="dv_submit">
        <asp:Button ID="Button1" runat="server" Text="调整" SkinID="Edit"  CssClass="btn_Tiao" OnClick="Button1_Click" /><asp:Button
            ID="btnReturn" runat="server" SkinID="Return" Text="取消" OnClientClick="closeWindow()" />
    </div>
</asp:Content>
