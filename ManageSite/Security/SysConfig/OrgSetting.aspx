<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OrgSetting.aspx.cs" Inherits="Admin_Site_SysConfig_OrgSetting"
    MasterPageFile="~/MasterPages/MPageAdmin.master" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<asp:Content runat="server" ID="PageContent" ContentPlaceHolderID="ContentPlaceHolder1">
    <div class="dv_serviceTab">
        <div class="dv_Tabmenus">
            <asp:Repeater ID="rptConfigGroup" runat="server">
                <HeaderTemplate>
                    <ul>
                </HeaderTemplate>
                <ItemTemplate>
                    <li id='<%#string.Format("Tab_{0}",Container.ItemIndex) %>' onclick="<%#string.Format("showTab('Tab_{0}', 'Div_Select_{0}','selected')",Container.ItemIndex)%>">
                        <a onfocus="blur()" href="javascript:void(0);"><span class="bj">
                            <%#Eval("ConfigGroupName")%></span></a></li>
                </ItemTemplate>
                <FooterTemplate>
                    </ul>
                </FooterTemplate>
            </asp:Repeater>
        </div>
    </div>
    <div class="info">
        <asp:Repeater ID="rptConfigs" runat="server" OnItemDataBound="rptConfigs_ItemDataBound">
            <ItemTemplate>
                <div id='<%#string.Format("Div_Select_{0}",Container.ItemIndex) %>' class="dv_searchlist"
                    style='display: none;'>
                    <cc1:CustomGridView ID="gvConfigs" runat="server" AutoGenerateColumns="False" CustomAllowPaging="False"
                        DataKeyNames="ConfigID" ShowFooter="false" CssClass="GridviewGray" OnRowDataBound="gvConfigs_OnRowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="序号">
                                <HeaderStyle Width="40" CssClass="aligncenter" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="LabNo" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="配置项名称" DataField="DisplayName" DataFormatString="{0}："
                                ItemStyle-CssClass="alignright" HeaderStyle-Width="100" HeaderStyle-CssClass="alignright" />
                            <asp:TemplateField HeaderText="用户自定义值" HeaderStyle-CssClass="aligncenter" HeaderStyle-Width="140px">
                                <ItemStyle />
                                <ItemTemplate>
                                    <asp:HiddenField ID="hfConfigName" runat="server" Value= '<%#Eval("Name") %>' /> 
                                    <asp:TextBox runat="server" ID="txtUserValue" Text='<%#Eval("UserValue") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="系统默认值" DataField="DefaultValue" HeaderStyle-CssClass="alignleft" HeaderStyle-Width="170px"
                                ItemStyle-CssClass="alignleft" />
                            <asp:BoundField HeaderText="说明" DataField="Description" HeaderStyle-CssClass="alignleft"
                                ItemStyle-CssClass="alignleft" />
                        </Columns>
                    </cc1:CustomGridView>
                    <div class="dv_splitLine"> </div>
                    <div class="dv_submit">
                        <asp:Button ID="btnSave" runat="server" Text="保存" OnClick="btn_SaveHandle" SkinID="Save" />
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</asp:Content>
