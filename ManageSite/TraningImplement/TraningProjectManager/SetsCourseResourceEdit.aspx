<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master" AutoEventWireup="true" CodeFile="SetsCourseResourceEdit.aspx.cs" Inherits="TraningImplement_TraningProjectManager_SetsCourseResourceEdit" %>
    
<%@ Register src="Controls/CourseResourceInfo.ascx" tagname="CourseResourceInfo" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <!--功能标题-->
        <h2 class="dv_title">
            编辑课程资源
         </h2>
         <uc1:CourseResourceInfo ID="CourseResourceInfo1" runat="server" />
    </div>
</asp:Content>
