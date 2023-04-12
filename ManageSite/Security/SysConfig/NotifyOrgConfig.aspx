<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NotifyOrgConfig.aspx.cs"
    ValidateRequest="false" Inherits="Admin_Site_SysConfig_NotifyOrgConfig" MasterPageFile="~/MasterPages/MPageAdmin.master" %>

<%@ Register Assembly="ETMS.Editor" Namespace="ETMS.Editor.UEditor" TagPrefix="wuc" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<asp:Content runat="server" ID="PageContent" ContentPlaceHolderID="ContentPlaceHolder1">
    <div class="dv_serviceTab">
        <div class="dv_Tabmenus" style="min-width: 800px;">
            <asp:Repeater ID="rptConfigGroup" runat="server">
                <HeaderTemplate>
                    <ul>
                </HeaderTemplate>
                <ItemTemplate>
                    <li id='<%#string.Format("Tab_{0}",Container.ItemIndex) %>' onclick="<%#string.Format("showTab('Tab_{0}', 'Div_Select_{0}','selected')",Container.ItemIndex)%>">
                        <a onfocus="blur()" href="javascript:void(0);"><span class="bj">
                            <%#Eval("MessageClassName")%></span></a></li>
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
                    style='display: none;'>
                    <asp:HiddenField runat="server" ID="hdConfigId" />
                    <table class="GridviewGray">
                        <tr>
                            <th rowspan="2" style="width: 120px; text-align: right">
                                模板变量说明
                            </th>
                            <th style="width: 120px; text-align: right">
                                内置变量：
                            </th>
                            <td style="padding-left: 8px;">
                                ${UserID} 账号ID
                                <br />
                                ${LoginName} 账号
                                <br />
                                ${UserName} 账号名称
                                <br />
                                ${Email} 邮箱地址
                                <br />
                                ${MobilePhone} 手机号
                            </td>
                        </tr>
                        <tr>
                            <th style="text-align: right">
                                扩展变量：
                            </th>
                            <td>
                                <asp:Label runat="server" ID='txtTemplateVariableDefine'></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <th rowspan="2">
                                <asp:CheckBox runat="server" ID="ckboxIsEnableEmail" Text="发送邮件" />
                            </th>
                            <th style="width: 120px; text-align: right">
                                邮件标题模板：
                            </th>
                            <td>
                                <asp:TextBox runat="server" ID="txtEmailSubjectTemplate" CssClass="inputbox_440"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th style="text-align: right">
                                邮件内容模板：
                            </th>
                            <td style="padding-left: 8px;">
                                <wuc:UEditor runat="server" ID="txtEmailBodyTemplate" ToolType="Basic"
                                    Width="445" Height="200"></wuc:UEditor>
                            </td>
                        </tr>
                        <tr class="hide">
                            <th rowspan="2">
                                <asp:CheckBox runat="server" ID="ckboxIsEnableSMS" Text="发送短信" />
                            </th>
                            <th style="width: 120px; text-align: right">
                                短信标题模板：
                            </th>
                            <td>
                                <asp:TextBox runat="server" ID="txtSMSSubjectTemplate" CssClass="inputbox_440"></asp:TextBox>
                            </td>
                        </tr>
                        <tr class="hide">
                            <th style="text-align: right">
                                短信内容模板：
                            </th>
                            <td>
                                <asp:TextBox runat="server" ID="txtSMSBodyTemplate" TextMode="MultiLine" CssClass="inputbox_area440" />
                            </td>
                        </tr>
                        <tr class="hide">
                            <th rowspan="2">
                                <asp:CheckBox runat="server" ID="ckboxIsEnableSiteInfo" Text="发送站内信" />
                            </th>
                            <th style="width: 120px; text-align: right">
                                站内信标题模板：
                            </th>
                            <td>
                                <asp:TextBox runat="server" ID="txtSiteInfoSubjectTemplate" CssClass="inputbox_440"></asp:TextBox>
                            </td>
                        </tr>
                        <tr class="hide">
                            <th style="text-align: right">
                                站内信内容模板：
                            </th>
                            <td style="padding-left: 8px;">
                                <wuc:UEditor runat="server" ID="txtSiteInfoBodyTemplate" ToolType="Basic"
                                    Width="445" Height="200"></wuc:UEditor>
                            </td>
                        </tr>
                    </table>
                    <div class="dv_submit">
                        <asp:Button ID="btnSave" runat="server" Text="保存" SkinID="Save" CommandName="Save"
                            OnCommand="btn_SaveHandle" CommandArgument='<%#Eval("MessageClassID") %>' />
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</asp:Content>
