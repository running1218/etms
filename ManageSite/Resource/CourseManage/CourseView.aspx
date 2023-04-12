<%@ Page Title="课程基本信息" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master"
    AutoEventWireup="true" CodeFile="CourseView.aspx.cs" Inherits="ETMS.WebApp.Manage.CourseView" %>

<%@ Register Src="Controls/CourseInfoView.ascx" TagName="CourseInfoView" TagPrefix="uc1" %>
<%@ Register Src="Controls/CoursewareList.ascx" TagName="CoursewareList" TagPrefix="uc2" %>
<%@ Register Src="Controls/OnlineHomeworkListView.ascx" TagName="OnlineHomeworkListView" TagPrefix="uc3" %>
<%@ Register Src="Controls/OnlineTestListView.ascx" TagName="OnlineTestListView" TagPrefix="uc4" %>
<asp:Content ID="cphNav" runat="server" ContentPlaceHolderID="cphBack">
    <asp:LinkButton ID="lbnBack" runat="server" PostBackUrl="~/Resource/CourseManage/CourseList.aspx"
        CssClass="btn_Return">返回</asp:LinkButton>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="dv_serviceTab">
        <div class="dv_Tabmenus">
            <ul>
                <li id="Tab_0" onclick="showTab('Tab_0', 'Div_Select_0','selected');"><a onfocus="blur()"
                    href="javascript:void(0);"><span class="bj">课程信息</span></a></li>               
                <li id="Tab_1" onclick="showTab('Tab_1', 'Div_Select_1','selected');"><a onfocus="blur()"
                    href="javascript:void(0);"><span class="bj">在线作业</span></a></li>
                <li id="Tab_2" onclick="showTab('Tab_2', 'Div_Select_2','selected');"><a onfocus="blur()"
                    href="javascript:void(0);"><span class="bj">在线测试</span></a></li>
            </ul>
        </div>
    </div>
    <div class="info" align="center">
        <div id="Div_Select_0" style="display: none">
            <div id="Div_CourseInfo" class="dv_pageInformation">
                <uc1:CourseInfoView ID="CourseInfoView2" runat="server" />
            </div>
        </div>
        <div id="Div_Select_1" style="display: none">
            <uc3:OnlineHomeworkListView ID="OnlineHomeworkListView2" runat="server" />
        </div>
        <div id="Div_Select_2" style="display: none">            
            <uc4:OnlineTestListView ID="OnlineTestListView1" runat="server" />
        </div>
    </div>
</asp:Content>
