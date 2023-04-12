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
                                ģ�����˵��
                            </th>
                            <th style="width: 120px; text-align: right">
                                ���ñ�����
                            </th>
                            <td style="padding-left: 8px;">
                                ${UserID} �˺�ID
                                <br />
                                ${LoginName} �˺�
                                <br />
                                ${UserName} �˺�����
                                <br />
                                ${Email} �����ַ
                                <br />
                                ${MobilePhone} �ֻ���
                            </td>
                        </tr>
                        <tr>
                            <th style="text-align: right">
                                ��չ������
                            </th>
                            <td>
                                <asp:Label runat="server" ID='txtTemplateVariableDefine'></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <th rowspan="2">
                                <asp:CheckBox runat="server" ID="ckboxIsEnableEmail" Text="�����ʼ�" />
                            </th>
                            <th style="width: 120px; text-align: right">
                                �ʼ�����ģ�壺
                            </th>
                            <td>
                                <asp:TextBox runat="server" ID="txtEmailSubjectTemplate" CssClass="inputbox_440"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th style="text-align: right">
                                �ʼ�����ģ�壺
                            </th>
                            <td style="padding-left: 8px;">
                                <wuc:UEditor runat="server" ID="txtEmailBodyTemplate" ToolType="Basic"
                                    Width="445" Height="200"></wuc:UEditor>
                            </td>
                        </tr>
                        <tr class="hide">
                            <th rowspan="2">
                                <asp:CheckBox runat="server" ID="ckboxIsEnableSMS" Text="���Ͷ���" />
                            </th>
                            <th style="width: 120px; text-align: right">
                                ���ű���ģ�壺
                            </th>
                            <td>
                                <asp:TextBox runat="server" ID="txtSMSSubjectTemplate" CssClass="inputbox_440"></asp:TextBox>
                            </td>
                        </tr>
                        <tr class="hide">
                            <th style="text-align: right">
                                ��������ģ�壺
                            </th>
                            <td>
                                <asp:TextBox runat="server" ID="txtSMSBodyTemplate" TextMode="MultiLine" CssClass="inputbox_area440" />
                            </td>
                        </tr>
                        <tr class="hide">
                            <th rowspan="2">
                                <asp:CheckBox runat="server" ID="ckboxIsEnableSiteInfo" Text="����վ����" />
                            </th>
                            <th style="width: 120px; text-align: right">
                                վ���ű���ģ�壺
                            </th>
                            <td>
                                <asp:TextBox runat="server" ID="txtSiteInfoSubjectTemplate" CssClass="inputbox_440"></asp:TextBox>
                            </td>
                        </tr>
                        <tr class="hide">
                            <th style="text-align: right">
                                վ��������ģ�壺
                            </th>
                            <td style="padding-left: 8px;">
                                <wuc:UEditor runat="server" ID="txtSiteInfoBodyTemplate" ToolType="Basic"
                                    Width="445" Height="200"></wuc:UEditor>
                            </td>
                        </tr>
                    </table>
                    <div class="dv_submit">
                        <asp:Button ID="btnSave" runat="server" Text="����" SkinID="Save" CommandName="Save"
                            OnCommand="btn_SaveHandle" CommandArgument='<%#Eval("MessageClassID") %>' />
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</asp:Content>
