<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Admin_Site_SysConfig_Default"
    MasterPageFile="~/MasterPages/MPageAdmin.master" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<asp:Content runat="server" ID="PageContent" ContentPlaceHolderID="ContentPlaceHolder1">
<div class="dv_pageInformation" style="margin-bottom:10px;">
    <a href="javascript:showWindow('系统配置组管理','<%=this.ActionHref("ConfigGroupList.aspx")%>')" class="link_colorRed">
        系统配置组管理</a></div>
    <div class="dv_serviceTab">
        <div class="dv_Tabmenus">
            <asp:Repeater ID="rptConfigGroup" runat="server">
                <HeaderTemplate>
                    <ul>
                </HeaderTemplate>
                <ItemTemplate>
                    <li id='<%#string.Format("Tab_{0}",Container.ItemIndex) %>' onmousemove="<%#string.Format("showTab('Tab_{0}', 'Div_Select_{0}','selected')",Container.ItemIndex)%>">
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
                <div id='<%#string.Format("Div_Select_{0}",Container.ItemIndex) %>' class="dv_pageInformation"
                    style='display:none;'>
                    <cc1:CustomGridView ID="gvConfigs" runat="server" AutoGenerateColumns="False" CustomAllowPaging="False"
                        DataKeyNames="ConfigID" ShowFooter="false">
                        <Columns>
                            <asp:TemplateField HeaderText="序号">
                                <HeaderStyle Width="40" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="LabNo" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="配置项名称" HeaderStyle-CssClass="aligncenter">
                                <ItemStyle />
                                <ItemTemplate>
                                    <asp:TextBox runat="server" ID="txtDisplayName" Text='<%#Eval("DisplayName") %>'
                                        cssClass="inputbox_100" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="配置项标识（唯一）" HeaderStyle-CssClass="aligncenter">
                                <ItemStyle  />
                                <ItemTemplate>
                                    <asp:TextBox runat="server" ID="txtName" Text='<%#Eval("Name") %>' cssClass="inputbox_100" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="系统默认值" HeaderStyle-CssClass="aligncenter">
                                <ItemStyle  />
                                <ItemTemplate>
                                    <asp:TextBox runat="server" ID="txtDefaultValue" cssClass="inputbox_100" Text='<%#Eval("DefaultValue") %>'
                                        />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="顺序" HeaderStyle-CssClass="aligncenter field8"  >
                                <ItemStyle  />
                                <ItemTemplate>
                                    <cc1:CustomTextBox ContentType="Number"  cssClass="inputbox_60" runat="server" ID="txtOrderNo" Text='<%#Eval("OrderNo") %>'
                                         />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="说明" HeaderStyle-CssClass="aligncenter">
                                <ItemStyle  />
                                <ItemTemplate>
                                    <asp:TextBox runat="server" ID="txtDescription"  cssClass="inputbox_100" Text='<%#Eval("Description") %>'
                                        />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ShowHeader="False" HeaderText="操作"  HeaderStyle-CssClass="aligncenter">
                                <ItemTemplate>
                                    <cc1:CustomLinkButton runat="server" ID="CustomLinkButton1" Text='删除' OnCommand="Opeator_Command"
                                        EnableConfirm="true" ConfirmMessage='确认要删除？' CommandName="Remove" CommandArgument='<%#Eval("ConfigID") %>'></cc1:CustomLinkButton>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                        </Columns>
                    </cc1:CustomGridView>
                    <div class="dv_submit">
                        <asp:Button ID="btnSave" runat="server" Text="保存" SkinID="Save" CommandName="Save"
                            OnCommand="btn_SaveHandle" CommandArgument='<%#Eval("ConfigGroupID") %>' />
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</asp:Content>
