<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPagePop.Master" AutoEventWireup="true"
    CodeFile="MentorView.aspx.cs" Inherits="Security_MentorManage_MentorView" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<div class="dv_information">
    <table class="GridviewGray">
        <tr>
            <th>
                姓&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;名：
            </th>
            <td>
                <asp:Label ID="lblRealName" runat="server"></asp:Label>
            </td>
            <th>
                工&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;号：
            </th>
            <td>
                <asp:Label ID="lblWorkNo" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <th>
                <asp:Literal ID="ltlDepartment" runat="server" Text="<%$ Resources:UIResource, ui_department%>"></asp:Literal>：
            </th>
            <td colspan="3">
                <cc1:DictionaryLabel runat="server" ID="lblDepartment" DictionaryType="Site_DepartmentByOrgID" />
            </td>
        </tr>
        <tr>
            <th>
                <asp:Literal ID="ltlRank" runat="server" Text="<%$ Resources:UIResource, ui_rank%>"></asp:Literal>：
            </th>
            <td>
                <cc1:DictionaryLabel runat="server" ID="lblRank" DictionaryType="vw_Dic_Sys_Rank" />
            </td>
            <th>
                <asp:Literal ID="ltlPosition" runat="server" Text="<%$ Resources:UIResource, ui_position%>"></asp:Literal>：
            </th>
            <td>
                <cc1:DictionaryLabel runat="server" ID="lblPost" DictionaryType="Dic_PostByOrgID" />
            </td>
        </tr>
        <tr>
            <th>
                性&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;别：
            </th>
            <td>
                <cc1:DictionaryLabel runat="server" ID="lblSex" DictionaryType="Dic_Sex" />
            </td>
            <th>
                身份证号：
            </th>
            <td>
                <asp:Label ID="lblIdentity" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <th>
                工作职务：
            </th>
            <td>
                <asp:Label ID="lblJobTitle" runat="server"></asp:Label>
            </td>
            <th>
                直接上级：
            </th>
            <td>
                <asp:Label ID="lblParent" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <th>
                出生日期：
            </th>
            <td>
                <cc1:DateTimeLabel ID="lblBirthDay" runat="server"></cc1:DateTimeLabel>
            </td>
            <th>
                邮&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;箱：
            </th>
            <td>
                <asp:Label ID="lblEmail" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <th>
                电&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;话：
            </th>
            <td>
                <asp:Label ID="lblTelephone" runat="server"></asp:Label>
            </td>
            <th>
                手&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;机：
            </th>
            <td>
                <asp:Label ID="lblMobilePhone" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <th>
                最高学历：
            </th>
            <td>
                <asp:Label ID="lblHightestEducation" runat="server"></asp:Label>
            </td>
            <th>
                专&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;业：
            </th>
            <td>
                <asp:Label ID="lblSpecialty" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <th>
                政治面貌：
            </th>
            <td>
                <cc1:DictionaryLabel runat="server" ID="lblPolitics" DictionaryType="Dic_Sys_Politics" />
            </td>
            <th>
                入职日期：
            </th>
            <td>
                <cc1:DateTimeLabel ID="lblJobTime" runat="server"></cc1:DateTimeLabel>
            </td>
        </tr>
        <tr>
            <th>
                导师状态：
            </th>
            <td colspan="3">
                <cc1:DictionaryLabel runat="server" ID="lblStatus" DictionaryType="Dic_Status" />
            </td>
        </tr>
        <tr>
            <th>
                创建人：
            </th>
            <td>
                <asp:Label ID="lblCreator" runat="server"></asp:Label>
            </td>
            <th>
                创建时间：
            </th>
            <td>
                <asp:Label ID="lblCreateTime" runat="server"></asp:Label>
            </td>
        </tr>
        <tr style="display:none;">
            <th>
                修改人：
            </th>
            <td>
                <asp:Label ID="lblModifyUser" runat="server"></asp:Label>
            </td>
            <th>
                修改时间：
            </th>
            <td>
                <asp:Label ID="lblModifyTime" runat="server"></asp:Label>
            </td>
        </tr>
    </table></div>
    <div class="dv_submit">       
        <a href="javascript:closeWindow();" class="btn_Cancel padleft10">关闭</a>
    </div>
</asp:Content>
