<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master"
    AutoEventWireup="true" CodeFile="AddTestPaper.aspx.cs" Inherits="QuestionDB_TestPaper_AddTestPaper" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="Controls/TestpaperInfoSenior.ascx" TagName="TestpaperInfoSenior"
    TagPrefix="uc1" %>
<%@ Register Src="~/QuestionDB/TestPaper/Controls/ItemDB2BeSelect.ascx" TagName="ItemDB2BeSelect"
    TagPrefix="uc2" %>
<%@ Register Src="Controls/ItemDBSelected.ascx" TagName="ItemDBSelected" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphBack" runat="Server">
    <a href="<%= getBackUrl()%>"
        class="btn_Return">返回</a>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="dv_searchbox fixedTable">
    <div class="dv_pageInformation">
        <p style="margin-bottom: 5px;">
            <b>课程编码：</b><asp:Literal ID="ltlCourseCode" runat="server"></asp:Literal>
            &nbsp;&nbsp;&nbsp;&nbsp;<b>课程名称：</b><asp:Literal ID="ltlCourseName" runat="server"></asp:Literal>
            &nbsp;&nbsp;&nbsp;&nbsp;<b><asp:Literal ID="ltlExerciseTypeName" runat="server"></asp:Literal>：</b><asp:Literal
                ID="ltlExerciseName" runat="server"></asp:Literal>
            &nbsp;&nbsp;&nbsp;&nbsp;<b>状态：</b><cc1:DictionaryLabel ID="lblOnLineJobStatus" DictionaryType="Dic_Status"
                runat="server" />
                <b><asp:Literal
                ID="ltlTotalScoreTitle" runat="server" Text="总分："></asp:Literal></b><asp:Literal
                ID="ltlTotalScore" runat="server" Text="100"></asp:Literal>
        </p>
    </div>
    </div>
    <!--表单录入-->
    <div class="dv_serviceTab">
        <div class="dv_Tabmenus" style="min-width: 800px;">
            <ul>
                <li id="Tab_0" onclick="showTab('Tab_0', 'Div_Select_0','selected')">
                      <a onfocus="blur()"
                    href="javascript:void(0);"><span class="bj">
                      已选题目</span></a></li>
                <li id="Tab_1" onclick="showTab('Tab_1', 'Div_Select_1','selected')"><a onfocus="blur()"
                    href="javascript:void(0);"><span class="bj">策略出题</span></a></li>
                <li id="Tab_2" onclick="showTab('Tab_2', 'Div_Select_2','selected')"><a onfocus="blur()"
                    href="javascript:void(0);"><span class="bj">手动出题</span></a></li>
            </ul>
        </div>
    </div>
    <div class="info">
        <div id="Div_Select_0" class="dv_pageInformation dv_tabinfor">
            <uc3:ItemDBSelected ID="ItemDBSelected1" runat="server" />
        </div>
        <div id="Div_Select_1" class="dv_pageInformation dv_tabinfor">
            <uc1:TestpaperInfoSenior ID="TestpaperInfoSenior1" runat="server" />
        </div>
        <div id="Div_Select_2" class="dv_pageInformation dv_tabinfor">
            <uc2:ItemDB2BeSelect ID="ItemDB2BeSelect1" runat="server" />
        </div>
    </div>
</asp:Content>
