<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master"
    AutoEventWireup="true" CodeFile="SetsCourseResourceAdd.aspx.cs" Inherits="TraningImplement_TraningProjectManager_SetsCourseResourceAdd" %>
    
<%@ Register src="Controls/CourseResourceInfo.ascx" tagname="CourseResourceInfo" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <!--功能标题-->
        <h2 class="dv_title">
            新增课程资源
         </h2>
         <uc1:CourseResourceInfo ID="CourseResourceInfo1" runat="server" />
    </div>
</asp:Content>
