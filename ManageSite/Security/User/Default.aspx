<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Admin_Site_User_Default"
    MasterPageFile="~/MasterPages/MPageAdmin.master" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<asp:Content runat="server" ID="PageContent" ContentPlaceHolderID="ContentPlaceHolder1">
    <div class="dv_searchbox">
        <table class="GridviewGray">
            <tr>
                <th width="120">
                    �û��˻���
                </th>
                <td>
                    <cc1:CustomTextBox ButtonControlId="btn_Search" ID="txtLoginName" runat="server"
                        CssClass="inputbox_120" />
                    <asp:Button ID="btn_Search" runat="server" Text="��ѯ" OnClick="btn_Search_Click" SkinID="Search" />
                    <a href="javascript:hideGridview()" class="dropdownico" id="Highsearch">�߼�����</a>
                </td>
            </tr>
            <tr>
                <th>
                    �û�������
                </th>
                <td>
                    <cc1:CustomTextBox ButtonControlId="btn_Search" ID="txtRealName" CssClass="inputbox_120"
                        runat="server" />
                </td>
            </tr>
        </table>
        <div class="center">
        </div>
    </div>
    <div class="dv_searchlist">
        <!--��ҳ-->
        <div class="dv_pagePanel">
            <div class="dv_selectAll">
                <cc1:CheckBoxsController runat="server" ID="chkboxControler" ContainerID="GridViewList" />
                <%if (IsDisplay)
                  { %>
                <input type="button" class="btn_Add" value="����" onclick="showWindow('�û�������Ϣ','<%=this.ActionHref("View.aspx?op=add&id=0")%>', 600, 450)" />
                <%} %>
                <cc1:CustomButton CssClass="btn_Repassword" Text="��������" runat="server" ID="btnReset"
                    UseSubmitBehavior="false" EnableConfirm="true" ConfirmMessage="ȷ��Ҫִ�С��������á�������?"
                    OnClick="btnReset_Click" />
            </div>
            <div class="dv_pageControl">
                <uc2:PageSet ID="PageSet1" runat="server" />
            </div>
        </div>
        <cc1:CustomGridView ID="GridViewList" runat="server" AutoGenerateColumns="False"
            DataKeyNames="UserID" OnRowDataBound="GridViewList_RowDataBound">
            <Columns>
                <asp:TemplateField HeaderStyle-Width="18">
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:CheckBox ID="CheckBox1" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="�û��˻�" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                    <ItemTemplate>
                        <cc1:ShortTextLabel Text='<%#Eval("LoginName")%>' runat="server" ShowTextNum="20"></cc1:ShortTextLabel>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="�û�����" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft field17" >
                    <ItemTemplate>
                        <cc1:ShortTextLabel Text='<%#Eval("RealName")%>' runat="server" ShowTextNum="15"></cc1:ShortTextLabel>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="״̬" HeaderStyle-Width="40">
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label ID="LabStatus" runat="server" Text='<%#(int)Eval("Status")==1?"����":"ͣ��" %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="�Ƿ񳬼�����Ա" HeaderStyle-Width="100">
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label ID="LabIsAdmin" runat="server" Text='<%#(bool)Eval("IsSysAccount")?"��":"��" %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="False" HeaderText="�����ɫ" HeaderStyle-Width="70">
                    <ItemTemplate>
                        <a <%# (bool)Eval("IsSysAccount")?"disabled onclick='return false;' class='link_colorGray'":"" %>
                            href='javascript:showWindow("�û���ɫ��Ϣ","<%# this.ActionHref(String.Format("../UserRole/View.aspx?op=edit&id={0}", new Object[]{Eval("UserID")})) %>")'>
                            ����</a> <a href='javascript:showWindow("�û���ɫ��Ϣ","<%# this.ActionHref(String.Format("../UserRole/View.aspx?op=view&id={0}", new Object[]{Eval("UserID")})) %>")'>
                                �鿴</a>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="False" HeaderText="����Ȩ��" HeaderStyle-Width="70">
                    <ItemTemplate>
                        <a <%# (bool)Eval("IsSysAccount")?"disabled onclick='return false;' class='link_colorGray'":"" %>
                            href='javascript:showWindow("�û���ɫ��Ϣ","<%# this.ActionHref(String.Format("../User_FunctionGroup/default.aspx?id={0}", new Object[]{Eval("UserID")})) %>")'>
                            ��Ȩ</a> <a href='javascript:showWindow("�û���ɫ��Ϣ","<%# this.ActionHref(String.Format("../User_FunctionGroup/View.aspx?id={0}", new Object[]{Eval("UserID")})) %>")'>
                                �鿴</a>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="False" HeaderText="����" HeaderStyle-Width="200">
                    <ItemTemplate>
                        <a <%# (bool)Eval("IsSysAccount")?"disabled onclick='return false;' class='link_colorGray'":"" %>
                            href='javascript:showWindow("�û�������Ϣ","<%# this.ActionHref(String.Format("View.aspx?op=edit&id={0}", new Object[]{Eval("UserID")})) %>")'>
                            �༭</a> <a href='javascript:showWindow("�û�������Ϣ","<%# this.ActionHref(String.Format("View.aspx?op=view&id={0}", new Object[]{Eval("UserID")})) %>")'>
                                �鿴</a>
                        <cc1:CustomLinkButton runat="server" ID="lkReset" Text="��������" OnCommand="UserOpeator_Command"
                            EnableConfirm="true" ConfirmMessage="ȷ��Ҫ���ø��û����룿" CommandName="Reset" CommandArgument='<%#Eval("UserID") %>'></cc1:CustomLinkButton>
                        <cc1:CustomLinkButton runat="server" ID="lkSwitchStatus" Text='<%#0==(int)Eval("Status")?"����":"ͣ��" %>'
                            OnCommand="UserOpeator_Command" EnableConfirm="true" ConfirmMessage='<%#string.Format("ȷ��Ҫ{0}���û���",0==(int)Eval("Status")?"����":"ͣ��") %>'
                            CommandName="SwitchStatus" CommandArgument='<%#Eval("UserID") %>'></cc1:CustomLinkButton>
                        <cc1:CustomLinkButton runat="server" ID="CustomLinkButton1" Enabled='<%# !((bool)Eval("IsSysAccount"))%>'
                            CssClass='<%# ((bool)Eval("IsSysAccount"))?"link_colorGray":""%>' Text='ɾ��' OnCommand="UserOpeator_Command"
                            EnableConfirm="true" ConfirmMessage='ȷ��Ҫɾ�����û���Ϣ��' CommandName="Remove" CommandArgument='<%#Eval("UserID") %>'></cc1:CustomLinkButton>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle HorizontalAlign="Center" />
                </asp:TemplateField>
            </Columns>
        </cc1:CustomGridView>
        <!--�б� end-->
        <div class="dv_splitLine">
        </div>
        <!--��ҳ-->
        <div class="dv_pagePanel">
        </div>
    </div>
</asp:Content>
