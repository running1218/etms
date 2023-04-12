<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Admin_Site_Student_Default"
    MasterPageFile="~/MasterPages/MPageAdmin.master" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<asp:Content runat="server" ID="PageContent" ContentPlaceHolderID="ContentPlaceHolder1">
    <div class="dv_searchbox">
        <table class="GridviewGray th80">
            <tr>
                <th width="120">
                    ѧԱ������
                </th>
                <td width="120">
                    <asp:TextBox ID="txtRealName" runat="server"></asp:TextBox>
                </td>
                <th width="120">
                    ѧԱ�˻���
                </th>
                <td>
                    <asp:TextBox ID="txtLoginName" runat="server"></asp:TextBox><asp:Button ID="btn_Search"

                        runat="server" Text="��ѯ" OnClick="btn_Search_Click" SkinID="Search" />
                    <a href="javascript:hideGridview()" class="dropdownico" id="Highsearch">�߼�����</a>
                </td>
            </tr>
            <tr>
                <th>
                   <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:UIResource, ui_workno%>"></asp:Literal>��
                </th>
                <td >
                    <asp:TextBox ID="txtWorkNo" runat="server" CssClass="inputbox_120"></asp:TextBox>
                </td>
                <th>�û�״̬��</th>
                <td>
                    <cc1:DictionaryDropDownList runat="server" ID="ddl_UserStatus" DictionaryType="Dic_Status" CssText="select_120" IsShowAll="true" />
                </td>
            </tr>
            <tr>
                <th>
                    <asp:Literal ID="ltlRank" runat="server" Text="ѧԱ���"></asp:Literal>��
                </th>
                <td colspan="3">
                    <cc1:DictionaryDropDownList runat="server" ID="ddlResettlementWay" DictionaryType="Dic_Sys_ResettlementWay"
                        CssClass="select_190" IsShowAll="true" />
                </td>
            </tr>
        </table>
    </div>
    <div class="dv_searchlist">
        <!--��ҳ-->
        <div class="dv_pagePanel">
            <div class="dv_selectAll">
                <%--<cc1:CheckBoxsController runat="server" ID="chkboxControler" ContainerID="GridViewList" />--%>
                <input type="button" class="btn_Add" value="����" onclick="showWindow('����ѧԱ��Ϣ','<%=this.ActionHref("View.aspx?op=add&id=0")%>', 650, 500)" />
                <input type="button" class="btn_Import" value="����" onclick="javascript:showWindow('����ѧԱ','<%=this.ActionHref("StudentImport.aspx")%>',500,360)" />
       <!--ѧԱ����������-->
                <asp:Button ID="btnExport" runat="server" CssClass="btn_Export" Text="����"  OnClick="btnExport_Click"/>
                <%--<cc1:CustomButton CssClass="btn_Del" Text="ɾ��" runat="server" ID="CustomButton1"
                    EnableConfirm="true" ConfirmMessage="ȷ��Ҫִ�С�����ɾ����������?" OnClick="btnDelete_Click" />--%>
            </div>
            <div class="dv_pageControl">
                <uc2:PageSet ID="PageSet1" runat="server" /> 
            </div>
        </div>
        <cc1:CustomGridView ID="GridViewList" runat="server" AutoGenerateColumns="False"
            DataKeyNames="UserID">
            <Columns>
                <%--<asp:TemplateField HeaderText="">
                    <ItemStyle HorizontalAlign="Center" Width="40" />
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:CheckBox ID="CheckBox1" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>--%>
                <asp:BoundField DataField="LoginName" HeaderText="ѧԱ�˻�"  ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft field14">
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="RealName" HeaderText="ѧԱ����" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft field14">
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle HorizontalAlign="Center"  />
                </asp:BoundField>
                <asp:BoundField DataField="WorkerNo" HeaderText="<%$ Resources:UIResource, ui_workno%>" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft field14">
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <%--<asp:TemplateField HeaderText="<%$ Resources:UIResource, ui_department%>" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                    <ItemTemplate>
                        <cc1:DictionaryLabel runat="server" ID="lblDepartment" DictionaryType="Site_DepartmentByOrgID"
                            FieldIDValue='<%#Eval("DepartmentID") %>' TextLength="4"/>
                    </ItemTemplate>
                </asp:TemplateField>--%>
                <%--<asp:TemplateField HeaderText="<%$ Resources:UIResource, ui_rank%>" HeaderStyle-CssClass="alignleft" ItemStyle-CssClass="alignleft">
                    <ItemTemplate>
                        <cc1:DictionaryLabel runat="server" ID="lblRank" DictionaryType="vw_Dic_Sys_Rank"
                            FieldIDValue='<%#Eval("RankID") %>' TextLength="4"/>
                    </ItemTemplate>
                </asp:TemplateField>--%>
                <asp:BoundField DataField="Email" HeaderText="����">
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="MobilePhone" HeaderText="�ֻ�">
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="CreateTime" HeaderText="����ʱ��">
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="״̬" HeaderStyle-Width="35px">
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label ID="LabStatus" runat="server" Text='<%#(int)Eval("Status")==1?"����":"ͣ��" %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="False" HeaderText="����" HeaderStyle-Width="200px">
                    <ItemTemplate>
                        <a href='javascript:showWindow("�༭ѧԱ��Ϣ","<%# this.ActionHref(String.Format("View.aspx?op=edit&id={0}", new Object[]{Eval("UserID")})) %>")'>
                            �༭</a> <a href='javascript:showWindow("�鿴ѧԱ��Ϣ","<%# this.ActionHref(String.Format("View.aspx?op=view&id={0}", new Object[]{Eval("UserID")})) %>")'>
                                �鿴</a>
                        <cc1:CustomLinkButton runat="server" ID="lkReset" Text="��������" OnCommand="UserOpeator_Command"
                            EnableConfirm="true" ConfirmMessage="ȷ��Ҫ���ø�ѧԱ���룿" CommandName="Reset" CommandArgument='<%#Eval("UserID") %>'></cc1:CustomLinkButton>
                        <cc1:CustomLinkButton runat="server" ID="lkSwitchStatus" Text='<%#0==(int)Eval("Status")?"����":"ͣ��" %>'
                            OnCommand="UserOpeator_Command" EnableConfirm="true" ConfirmMessage='<%#string.Format("ȷ��Ҫ{0}���û���",0==(int)Eval("Status")?"����":"ͣ��") %>'
                            CommandName="SwitchStatus" CommandArgument='<%#Eval("UserID") %>'></cc1:CustomLinkButton>
                        <cc1:CustomLinkButton runat="server" ID="CustomLinkButton1" Text='ɾ��' OnCommand="UserOpeator_Command"
                            EnableConfirm="true" ConfirmMessage='ȷ��Ҫɾ����ѧԱ��Ϣ��' CommandName="Remove" CommandArgument='<%#Eval("UserID") %>'></cc1:CustomLinkButton>
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
