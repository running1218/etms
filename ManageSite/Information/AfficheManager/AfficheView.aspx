<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPagePop.Master" AutoEventWireup="true"
    CodeFile="AfficheView.aspx.cs" Inherits="Information_AfficheManager_AfficheView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        body
        {
            background: url(../App_Themes/ThemeAdmin/Images/bjlad.gif) repeat;
        }
    </style>
    <div class="dv_questionTitle">
        公告信息</div>
    <p class="News_title">
        <asp:Literal ID="ltlMainHead" runat="server" /></p>
    <div class="cltlKeyword" id="cltlKeywords" runat="server">
        <asp:Literal ID="ltlBrief" runat="server" />
    </div>
    <div class="dv_CousreInformation">
        <div class="News_content">
            <asp:Literal ID="ltlArticleContent" runat="server" />
        </div>
        <div class="lastAuthor">
            <em>
                <asp:Literal ID="ltlOrg" runat="server" /></em> <em>
                    <asp:Literal ID="ltlCreateTime" runat="server" /></em>
        </div>
        <div class="boderDashed">
            <input type="button" value="关 闭" class="btn_Cancel" onclick="javascript:window.close()" /><br />
            <br />
        </div>
    </div>
</asp:Content>
