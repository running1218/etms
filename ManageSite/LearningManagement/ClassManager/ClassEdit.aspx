<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPagePop.Master" AutoEventWireup="true" CodeFile="ClassEdit.aspx.cs" Inherits="LearningManagement_ClassManager_ClassEdit" %>

<%@ Register Src="Controls/ClassInfo.ascx" TagName="ClassInfo" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!--功能标题-->
    <h2 class="dv_title">
        修改班级
    </h2>
    <uc1:ClassInfo ID="ClassInfo1" runat="server" />
</asp:Content>

