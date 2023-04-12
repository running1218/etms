<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPagePop.Master" AutoEventWireup="true"
    CodeFile="CoursePeriodView.aspx.cs" Inherits="TraningImplement_ProjectCoursePeriodResult_CoursePeriodView" %>

<%@ Register Src="../ProjectCoursePeriod/Controls/CoursePeriodView.ascx" TagName="CoursePeriodView"
    TagPrefix="uc1" %>
<%@ Register Src="../ProjectCoursePeriod/Controls/SetsStudentListView.ascx" TagName="SetsStudentListView"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="dv_serviceTab">
        <div class="dv_Tabmenus">
            <ul>
                <li id="Tab_0" onclick="showTab('Tab_0', 'Div_Select_0','selected')"><a onfocus="blur()"
                    href="javascript:void(0);"><span class="bj">课时信息</span></a></li>
                <li id="Tab_1" onclick="showTab('Tab_1', 'Div_Select_1','selected')"><a onfocus="blur()"
                    href="javascript:void(0);"><span class="bj">学员信息</span></a></li>
            </ul>
        </div>
    </div>
    <div class="dv_information">
        <div class="info">
            <div id="Div_Select_0" class="dv_pageInformation" style="display: none">
                <uc1:CoursePeriodView ID="CoursePeriodView1" runat="server" />
            </div>
            <div id="Div_Select_1" style="display: none">
                <uc2:SetsStudentListView ID="SetsStudentListView1" runat="server" />
            </div>
        </div>
    </div>
    <div class="dv_submit">
        <input type="button" class="btn_Cancel" value="关闭" onclick="javascript:closeWindow();" /></div>
</asp:Content>
