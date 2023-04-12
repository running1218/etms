<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPagePop.Master" AutoEventWireup="true"
    CodeFile="FeeReportView.aspx.cs" Inherits="FeeReportView" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <!--查找条件-->
        <div class="dv_information">
            <table class="GridviewGray fixedTable">
                <tr>
                    <th>
                        讲师姓名：
                    </th>
                    <td>
                        <asp:Literal ID="ltlTeacherName" runat="server"></asp:Literal>
                    </td>
                    <th>
                        组织机构：
                    </th>
                    <td>
                        <asp:Literal ID="ltlOrgName" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <th>
                        部&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;门：
                    </th>
                    <td>
                        <cc1:DictionaryLabel ID="lblDepartment" runat="server" DictionaryType="Site_DepartmentByOrgID"></cc1:DictionaryLabel>
                    </td>
                    <th>
                        培训日期：
                    </th>
                    <td>
                        <asp:Literal ID="ltlItemTime" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <th>
                        讲师等级：
                    </th>
                    <td colspan="3">
                    <cc1:DictionaryLabel ID="ltlTeacherLevel" DictionaryType="Dic_Sys_TeacherLevel"
                    runat="server" />
                    </td>
                </tr>
                <tr>
                    <th>
                        项目名称：
                    </th>
                    <td colspan="3">
                        <asp:Literal ID="ltlItemName" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <th>
                        课程名称：
                    </th>
                    <td colspan="3">
                        <asp:Literal ID="ltlCourseName" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <th>
                        培训时间说明：
                    </th>
                    <td>
                    <cc1:DictionaryLabel ID="ltlTrainingTimeDesc" DictionaryType="Dic_Sys_TrainingTimeDesc"
                                runat="server" />
                    </td>
                    <th>
                        培训时段：
                    </th>
                    <td>
                        <asp:Literal ID="ltlTimeBeginEnd" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <th>
                        课酬标准：
                    </th>
                    <td>
                        <asp:Literal ID="ltlCourseFee" runat="server"></asp:Literal>元
                    </td>
                    <th>
                        计划课时：
                    </th>
                    <td>
                        <asp:Literal ID="ltlCourseHours" runat="server"></asp:Literal>小时
                    </td>
                </tr>
                <tr>
                    <th>
                        实际课时：
                    </th>
                    <td>
                        <asp:Literal ID="ltlRealCourseHours" runat="server"></asp:Literal>小时
                    </td>
                    <th>
                        实际课酬：
                    </th>
                    <td>
                        <asp:Literal ID="ltlRealCourseFee" runat="server"></asp:Literal>元
                    </td>
                </tr>
                <tr>
                    <th>
                        支付时间：
                    </th>
                    <td>
                        <asp:Literal ID="ltlCreateTime" runat="server"></asp:Literal>
                    </td>
                    <th>
                        支 付 人：
                    </th>
                    <td>
                        <asp:Literal ID="ltlCreator" runat="server"></asp:Literal>
                    </td>
                </tr>
            </table>
        </div>
        
    </div>
    <div class="dv_submit">
            <input type="button" value="关闭" onclick="javascript:closeWindow();" class="btn_Close"/>
        </div>
</asp:Content>
