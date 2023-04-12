<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master" AutoEventWireup="true" CodeFile="playcsf.aspx.cs" Inherits="CourseWare_playcsf" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<object id=objXPlayer classid="CLSID:8EF11386-FCAF-426D-88B0-62C68E9B5770" width=100% height=100% >
    	<param name="ValidationCode" value="">
	<param name="Url" value="<%=Url %>">
	<param name="ShowToolbar" value="1">
	<param name="BufferTime" value="5">
	<param name="AutoPlay" value="1">
	<param name="AutoReplay" value="0">
	<param name="AutoFullScreen" value="0">
	<param name="AutoScreenStretch" value="0">
	<param name="DisableVideoAccel" value="0">
	<param name="DisableOverlay" value="0">
	<param name="UseMoreMonitor" value="0">
	<param name="ConnectStyle" value="1">
	<param name="MonitorIndex" value="0">
	<param name="MainMonitorMode" value="1">
	<param name="MaxVideoNumPerMonitor" value="4">
</object>
</asp:Content>

