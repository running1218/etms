<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPagePop.Master" AutoEventWireup="true" CodeFile="OnlinePlayingEdit.aspx.cs" Inherits="TraningImplement_ProjectCoursePeriod_OnlinePlayingEdit" %>

<%@ Register TagName="OnlinePlaying" TagPrefix="uc" Src="~/TraningImplement/ProjectCoursePeriod/Controls/OnlinePlayingInfo.ascx" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h2 class="dv_title">
        直播管理
    </h2>
    <uc:OnlinePlaying runat="server" ID="ucOnlinePlaying" Action="Edit" />
</asp:Content>

