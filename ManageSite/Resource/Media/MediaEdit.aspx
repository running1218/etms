<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPagePop.Master" AutoEventWireup="true" CodeFile="MediaEdit.aspx.cs" Inherits="Resource_Media_MediaEdit" %>

<%@ Register Src="~/Resource/Media/Controls/MediaInfo.ascx" TagPrefix="uc1" TagName="MediaInfo" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:MediaInfo runat="server" id="MediaInfo"  Action="Edit"  />
</asp:Content>

