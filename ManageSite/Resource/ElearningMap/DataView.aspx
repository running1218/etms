<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPagePop.Master" AutoEventWireup="true" CodeFile="DataView.aspx.cs" Inherits="Resource_ElearningMap_DataView" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">      
     <div class="info">
        <div id="Div_CourseInfo" class="dv_information">
            <table class="GridviewGray th120">
                <tr>
                    <th style="width:20%">
                        资料编码：
                    </th>
                    <td colspan="3">
                        <asp:Literal ID="ltlDataCode" runat="server"></asp:Literal>
                    </td>           
                </tr>
                <tr>
                    <th>
                        资料名称：
                    </th>
                    <td colspan="3">
                        <asp:Literal ID="ltlDataName" runat="server"></asp:Literal>
                    </td>           
                </tr>
                <tr>
                    <th>
                        学习内容：
                    </th>
                    <td colspan="3">
                        <asp:Literal ID="ltlDataContent" runat="server"></asp:Literal>
                    </td>
                </tr>    
                <tr> 
                    <th>
                        学习纲要：
                    </th>
                    <td colspan="3">
                        <asp:Literal ID="ltlOutLine" runat="server"></asp:Literal>
                    </td>      
                </tr>
                <tr>                
                    <th style="width:20%">
                        资料状态：
                    </th>
                    <td style="width:30%">
                        <cc1:DictionaryLabel ID="lblCourseStatus" DictionaryType="Dic_Status" runat="server" />
                    </td>
                    <th style="width:15%">学习方式：</th>
                    <td>
                        <cc1:DictionaryLabel ID="lblStudyModel" DictionaryType="Dic_Sys_StudyModel" runat="server" />
                    </td>
                </tr>
                <tr>
                    <th>
                        预计时长：
                    </th>
                    <td>
                        <asp:Literal ID="ltlTimeLength" runat="server"></asp:Literal>
                    </td>
                    <th>次　数：</th>
                    <td>
                        <asp:Literal ID="ltlTimes" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <th>
                        实施人：
                    </th>
                    <td>
                        <asp:Literal ID="ltlImplementor" runat="server"></asp:Literal>
                    </td>
                    <th>责任方：</th>
                    <td>
                        <asp:Literal ID="ltlDutyMan" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr> 
                    <th>
                        培训资料所在：
                    </th>
                    <td colspan="3">
                        <asp:Literal ID="ltlDataURL" runat="server"></asp:Literal>
                    </td>      
                </tr>
                <tr> 
                    <th>
                        评量方式：
                    </th>
                    <td colspan="3">
                        <asp:Literal ID="ltlEvaluationMode" runat="server"></asp:Literal>
                    </td>      
                </tr>
            </table>
        </div>
    </div>
</asp:Content>

