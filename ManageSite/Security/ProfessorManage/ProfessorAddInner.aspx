<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master" AutoEventWireup="true"
    CodeFile="ProfessorAddInner.aspx.cs" Inherits="ETMS.WebApp.Manage.Resource.ProfessorManage.ProfessorAddInner" %>

<%@ Register src="Controls/ProfessorInfoInner.ascx" TagName="ProfessorInfoInner" TagPrefix="uc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBack" runat="Server">
     <!--导航路径-->
        <div class="dv_path" id="dv_path">
            当前位置：资源管理系统&gt;&gt;讲师资源库&gt;&gt;内部讲师管理&gt;&gt;新增内部讲师
        </div>
    <!--功能标题-->
    <h2 class="dv_title">
        新增内部讲师
    </h2>
    <a id="aBack" runat="server" class="btn_Return">返回</a>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:ProfessorInfoInner ID="ProfessorInfoInner1" runat="server" />
</asp:Content>
