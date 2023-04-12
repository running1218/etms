<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master" AutoEventWireup="true"
    CodeFile="EearningMapView.aspx.cs" Inherits="Resource_ElearningMap_EearningMapView" %>

<%@ Register Src="Controls/MapCourseList.ascx" TagName="MapCourseList" TagPrefix="uc1" %>
<%@ Register Src="Controls/ElearningMapInfoView.ascx" TagName="ElearningMapInfoView" TagPrefix="uc2" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBack" Runat="Server">
     <a href="javascript:history.go(-1);" class="btn_Return">返回</a>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!--学习地图信息-->
   <uc2:ElearningMapInfoView ID="ElearningMapInfoView1" runat="server" />
    <!--学习地图下课程-->
    
        <uc1:MapCourseList ID="MapCourseList1" runat="server" />
    
</asp:Content>
