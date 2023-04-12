<%@ Page Title="选择课程" Language="C#" MasterPageFile="~/MasterPages/MPagePop.Master" AutoEventWireup="true" CodeFile="SetsCourseAdd.aspx.cs" Inherits="SiteManage_RecommendCourse_SetsCourseAdd" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<%@ Register Src="~/SiteManage/Controls/SetsCourseAdd.ascx" TagPrefix="uc1" TagName="SetsCourseAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server"> 
    <div class="info">
            <uc1:SetsCourseAdd runat="server" id="SetsCourseAdd1" />
    </div>
 </asp:Content>
