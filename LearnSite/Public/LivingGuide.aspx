<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Default.master" AutoEventWireup="true" CodeBehind="LivingGuide.aspx.cs" Inherits="ETMS.Studying.Public.LivingGuide" %>
<asp:Content ID="Content1" ContentPlaceHolderID="DefaultPlaceHolder" runat="server">
    <style>
        .view-area {
            width: 1200px;
            margin: 0 auto;
            min-height:400px;
        }
        .living-msg {
            width:100%;
            text-align:center;
            padding-top:100px;
        }
    </style>
    <div class="view-area">
        <div class="living-msg">
            暂无直播信息，请耐心等待...
        </div>
    </div>
</asp:Content>
