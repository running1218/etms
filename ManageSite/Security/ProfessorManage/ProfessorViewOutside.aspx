<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPagePop.Master" AutoEventWireup="true"
    CodeFile="ProfessorViewOutside.aspx.cs" Inherits="ETMS.WebApp.Manage.Resource.ProfessorManage.ProfessorViewOutside" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2 class="dv_title">
        外聘讲师
    </h2>
    <!--表单录入-->
    <div class="dv_information">
        <table class="GridviewGray fixedTable">
            <tr>
                <th>
                    编 号：
                </th>
                <td>
                    <asp:Label ID="lblTeacherCode" runat="server"></asp:Label>
                </td>
                <th>
                    姓 名：
                </th>
                <td>
                    <asp:Label ID="lblTeacherName" runat="server"></asp:Label>
                </td>
                <td rowspan="4">
                    <asp:Image ID="imgPhoto" runat="server" Width="90" Height="100" />
                </td>
            </tr>
            <tr>
                <th>
                    培训机构：
                </th>
                <td>
                    <cc1:DictionaryLabel ID="lblOuterOrgID" runat="server" DictionaryType="Tr_OuterOrg" />
                </td>
                <th>
                    状 态：
                </th>
                <td>
                    <cc1:DictionaryLabel ID="lblTeacherStatus" runat="server" DictionaryType="Dic_Status" />
                </td>
            </tr>
            <tr>
                <th>
                    讲师等级：
                </th>
                <td>
                    <cc1:DictionaryLabel ID="lblTeacherLevelID" runat="server" DictionaryType="Dic_Sys_TeacherLevel" />
                    <%--<asp:Label ID="lblTeacherLevelID" runat="server"></asp:Label>--%>
                </td>
                <th>
                    性 别：
                </th>
                <td>
                    <%--<cc1:DictionaryLabel ID="lblSex" runat="server" DictionaryType="Dic_Sex" />--%>
                    <asp:Label ID="lblSex" runat="server" />
                </td>
            </tr>
            <tr>
                <th>
                    课酬（元/天）：
                </th>
                <td>
                    <asp:Label ID="lblClassReward" runat="server"></asp:Label>
                </td>
                <th>
                    职 务：
                </th>
                <td>
                    <asp:Label ID="lblPosition" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>
                    出生日期：
                </th>
                <td>
                    <cc1:DateTimeLabel ID="lblBirthDay" runat="server" />
                </td>
                <th>
                    用&nbsp;&nbsp;户&nbsp;&nbsp;名：
                </th>
                <td colspan="2">
                    <asp:Label ID="lblUserName" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>
                    邮&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;箱：
                </th>
                <td>
                    <asp:Label ID="lblMail" runat="server"></asp:Label>
                </td>
                <th>
                    手&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;机：
                </th>
                <td colspan="2">
                    <asp:Label ID="lblTel" runat="server" TextMode="Password"></asp:Label>
                </td>
            </tr>
            <tr class="hide">
                <th>
                    合作关系：
                </th>
                <td colspan="4">
                    <asp:Label ID="LblCortpration" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <th style="vertical-align: top">
                    简 介：
                </th>
                <td colspan="4">
                    <asp:Literal ID="lblBrife" runat="server" />
                </td>
            </tr>
            <tr class="hide">
                <th style="vertical-align: top">
                    服务企业：
                </th>
                <td colspan="4">
                    <asp:Label ID="lblServiceEnterprise" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <th style="vertical-align: top">
                    工作履历：
                </th>
                <td colspan="4">
                    <asp:Literal ID="lblWorkExperience" runat="server" />
                </td>
            </tr>
            <tr>
                <th style="vertical-align: top">
                    专 长：
                </th>
                <td colspan="4">
                    <asp:Literal ID="lblExpertise" runat="server" />
                </td>
            </tr>
            <tr class="hide">
                <th style="vertical-align: top">
                    代表作品：
                </th>
                <td colspan="4">
                    <asp:Literal ID="lblRepresentativeWorks" runat="server" />
                </td>
            </tr>            
        </table>
    </div>
    <!--提交表单-->
    <div class="dv_submit">
        <input value="关闭" type="button" onclick="javascript:closeWindow();" class="btn_Close" />
    </div>
</asp:Content>
