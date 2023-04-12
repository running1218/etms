<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master"
    AutoEventWireup="true" CodeFile="ProjectCourseResourceList.aspx.cs" Inherits="TraningImplement_ProjectCourseResourceBatch_ProjectCourseResourceList" %>

<%@ Register Src="~/TraningImplement/ProjectCourseResourceBatch/Controls/ResourceList.ascx"
    TagName="ResourceList" TagPrefix="uc1" %>
<%@ Register Src="~/TraningImplement/ProjectCourseResourceBatch/Controls/ResourceAddList.ascx"
    TagName="ResourceAddList" TagPrefix="uc2" %>
<%@ Register Src="~/TraningImplement/ProjectCourseResourceBatch/Controls/ResourceStopList.ascx"
    TagName="ResourceStopList" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphBack" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="dv_serviceTab">
        <div class="dv_Tabmenus">
            <ul>
                <li id="Tab_0" onclick="showTab('Tab_0', 'Div_Select_0','selected')"><a onfocus="blur()"
                    href="javascript:void(0);"><span class="bj">已添加项目课程资源</span></a></li>
                <li id="Tab_1" onclick="showTab('Tab_1', 'Div_Select_1','selected')"><a onfocus="blur()"
                    href="javascript:void(0);"><span class="bj">未添加项目课程资源</span></a></li>
                <li id="Tab_2" onclick="showTab('Tab_2', 'Div_Select_2','selected')"><a onfocus="blur()"
                    href="javascript:void(0);"><span class="bj">已停用项目课程资源</span></a></li>
            </ul>
        </div>
    </div>
    <div class="info">
        <div id="Div_Select_0" class="" style="display: none">
            <uc1:ResourceList id="ResourceList1" runat="server" />
        </div>
        <div id="Div_Select_1" class=""  style="display: none">
            <uc2:ResourceAddList id="ResourceAddList1" runat="server" />
        </div>
        <div id="Div_Select_2" class=""  style="display: none">
            <uc3:ResourceStopList id="ResourceStopList1" runat="server" />
        </div>
    </div>
</asp:Content>
