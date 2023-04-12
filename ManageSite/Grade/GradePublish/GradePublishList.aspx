<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master"
    AutoEventWireup="true" CodeFile="GradePublishList.aspx.cs" Inherits="Grade_GradePublish_GradePublishList" %>

<%@ Register src="~/Grade/GradeManage/Controls/GradeList.ascx" tagname="GradeList" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 
    <uc1:GradeList ID="GradeList1" runat="server"  Operation="Public" />
 
</asp:Content>
