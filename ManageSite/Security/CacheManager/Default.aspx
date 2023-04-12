<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Admin_CacheManager_Default"
    MasterPageFile="~/MasterPages/MPageAdmin.master" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="../../Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<asp:Content runat="server" ID="PageContent" ContentPlaceHolderID="ContentPlaceHolder1">
    <div class="dv_searchlist">
        <!--翻页-->
        <div class="dv_pagePanel">
            <div class="dv_selectAll">
                <cc1:CheckBoxsController runat="server" ID="chkboxControler" ContainerID="GridViewList" />
                <input type="button" class="btn_refresh" value="刷新" onclick="window.location=window.location;" />
                <cc1:CustomButton UseSubmitBehavior="false" EnableConfirm="true" ConfirmMessage="确信清除选中的缓存吗?"
                    runat="server" ID="btnDelete" CssClass="btn_clearselected" Text="清除选中" OnClick="btnDelete_Click" />
                <cc1:CustomButton UseSubmitBehavior="false" EnableConfirm="true" ConfirmMessage="确信清除全部的缓存吗?"
                    runat="server" ID="CustomButton1" CssClass="btn_clearall" Text="清除全部" OnClick="btnDeleteAll_Click" />
            </div>
            <div class="dv_pageControl">
                <uc2:PageSet ID="PageSet1" runat="server" />
            </div>
        </div>
        <!--成员分级列表 -->
        <cc1:CustomGridView ID="gvRoles" runat="server" AutoGenerateColumns="False" DataKeyNames="CacheKey">
            <Columns>
                <asp:TemplateField HeaderStyle-Width="18">
                    <ItemTemplate>
                        <input id="Checkbox1" type="checkbox" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="缓存键标识" HeaderStyle-CssClass="alignleft" ItemStyle-CssClass="alignleft"
                    DataField="CacheKey"></asp:BoundField>
                <asp:BoundField HeaderText="缓存描述" HeaderStyle-CssClass="alignleft" ItemStyle-CssClass="alignleft"
                    DataField="Description"></asp:BoundField>
                <%--  <asp:TemplateField HeaderText="缓存明细">
                    <ItemTemplate>
                        <%#Eval("SubCacheItemCount") %>
                    </ItemTemplate>
                </asp:TemplateField>--%>
            </Columns>
        </cc1:CustomGridView>
        <!--列表 end-->
        <div class="dv_splitLine">
        </div>
        <!--翻页-->
        <div class="dv_pagePanel">
        </div>
    </div>
</asp:Content>
