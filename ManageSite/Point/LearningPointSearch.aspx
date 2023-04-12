<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master"
    AutoEventWireup="true" CodeFile="LearningPointSearch.aspx.cs" Inherits="Point_LearningPointSearch" %>
<%@ Register Src="~/Point/Controls/LearningPointPublish.ascx" TagName="PointPublish" TagPrefix="uc1" %>
<%@ Register Src="~/Point/Controls/LearningPointUnpublish.ascx" TagName="PointUnPublish" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphBack" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<!--表单录入-->
    <div class="dv_serviceTab">
        <div class="dv_Tabmenus" style="min-width: 800px;">
            <ul>
                <li id="Tab_0" onclick="showTab('Tab_0', 'Div_Select_0','selected')">
                    <a onfocus="blur()" href="javascript:void(0);"><span class="bj">已发布</span></a></li>
                <li id="Tab_1" onclick="showTab('Tab_1', 'Div_Select_1','selected')"><a onfocus="blur()"
                    href="javascript:void(0);"><span class="bj">未发布</span></a></li>                
            </ul>
        </div>
    </div>
    <div class="info">      
        <div id="Div_Select_0" class="">
           <uc1:PointPublish ID="PointPublish" runat="server" />            
        </div>
        <div id="Div_Select_1" class="" style="display: none">
           <uc2:PointUnPublish ID="PointUnPublish" runat="server" />  
        </div>       
    </div>
</asp:Content>
