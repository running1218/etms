<%@ Page Title="新增班级" Language="C#" MasterPageFile="~/MasterPages/MPagePop.Master"
    AutoEventWireup="true" CodeFile="ClassAdd.aspx.cs" Inherits="LearningManagement_ClassManager_ClassAdd" %>

<%@ Register Src="Controls/ClassInfo.ascx" TagName="ClassInfo" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!--功能标题-->
    <h2 class="dv_title">
        新增班级
    </h2>
    <uc1:ClassInfo ID="ClassInfo1" runat="server" />
</asp:Content>
