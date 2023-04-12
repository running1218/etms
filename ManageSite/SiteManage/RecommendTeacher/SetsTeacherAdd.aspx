<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/MPagePop.Master" CodeFile="SetsTeacherAdd.aspx.cs" Inherits="SiteManage_RecommendTeacher_SetsTeacherAdd" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<%@ Register Src="~/SiteManage/Controls/SetsTeacherAdd.ascx" TagPrefix="uc1" TagName="SetsTeacherAdd" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="info">
        <uc1:SetsTeacherAdd runat="server" id="SetsTeacherAdd1" />
    </div>
</asp:Content>
