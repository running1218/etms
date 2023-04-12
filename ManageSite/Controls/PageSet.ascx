<%@ Control Language="C#" AutoEventWireup="true" Inherits="ETMS.WebApp.Manage.Controls.PageSet"
    CodeFile="PageSet.ascx.cs" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<asp:Panel ID="Panel1" runat="server" >
    <span class="pageTotal">总记录数：<span class="colorRed"><asp:Label ID="RecordNumber"
        runat="server"></asp:Label></span> </span>
    <a href='javascript:goClick_<%=this.ClientID %>(1)' class='btn_first'></a>
    <asp:ImageButton ID="Previous" CssClass="btn_prev" runat="server" ImageUrl="~/App_Themes/ThemeAdmin/Images/space.gif"
        ImageAlign="AbsMiddle" CausesValidation="False"></asp:ImageButton>
    <asp:Literal ID="Literal1" runat="server"></asp:Literal>
    <asp:ImageButton ID="Next" CssClass="btn_next" runat="server" ImageUrl="~/App_Themes/ThemeAdmin/Images/space.gif"
        ImageAlign="AbsMiddle" CausesValidation="False"></asp:ImageButton>
    <a href='javascript:goClick_<%=this.ClientID %>(<%= PageCount %>)' class='btn_last'></a>
    <div style="display:none;">
        <cc1:SubmitTextBox ID="SelectPage" runat="server" CssClass="inputbox_40" SubmitControl="ImageButton1"
            ContentType="Number"></cc1:SubmitTextBox>
        <asp:ImageButton ID="ImageButton1" runat="server" CssClass="btn_go padleft10" ImageUrl="~/App_Themes/ThemeAdmin/Images/space.gif"
            ImageAlign="AbsMiddle" CausesValidation="False" OnClick="Go_Click"></asp:ImageButton>
    </div>
    <script type="text/javascript">
        function goClick_<%=this.ClientID %>(i) {
            document.getElementById("<%= SelectPage.ClientID %>").value = i + '';

            document.getElementById("<%= ImageButton1.ClientID %>").click();
        }            
    </script>
</asp:Panel>
