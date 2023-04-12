<%@ Page Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master" AutoEventWireup="true" CodeFile="ChooseCourse.aspx.cs" Inherits="Resource_ElearningMap_ChooseCourse" %>

<%@ Register src="~/Resource/ElearningMap/Controls/ChooseCourse.ascx" tagname="ChooseCourse" tagprefix="uc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBack" runat="Server">
    <a href='<%= this.ActionHref(string.Format("~/Resource/ElearningMap/MapCourseList.aspx?StudyMapID={0}", Request.QueryString["StudyMapID"]))%>' class="btn_Return">返回</a>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <uc1:ChooseCourse ID="ChooseCourse1" runat="server" />

</asp:Content>

