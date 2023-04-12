<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPagePop.Master" AutoEventWireup="true" CodeFile="ClassRoomView.aspx.cs" Inherits="ETMS.WebApp.Manage.Resource.ClassRoomManage.ClassRoomView" %>
<%@ Register src="Controls/ClassRoomInfo.ascx" tagname="ClassRoomInfo" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:ClassRoomInfo ID="ClassRoomInfo1" runat="server" />
      <div class="dv_submit">
        <input value="关闭" type="button" onclick="javascript:closeWindow();" class="btn_Close">
    </div>
</asp:Content> 