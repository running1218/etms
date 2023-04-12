<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StatDefault.aspx.cs" Inherits="Poll_ResourceQuery_StatDefault"
    MasterPageFile="~/MasterPages/MPageAdmin.master" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<asp:Content runat="server" ID="PageContent" ContentPlaceHolderID="ContentPlaceHolder1">
    <div class="dv_searchbox">
        <table class="GridviewGray">
            <tr>
                <th>
                    ��������:
                </th>
                <td colspan="3">
                    <asp:TextBox ID="txtQueryName" runat="server" SkinID="Text300"></asp:TextBox>
                    <asp:Button ID="btn_Search" runat="server" Text="��ѯ" OnClick="btn_Search_Click" SkinID="Search" />
                    <a href="javascript:hideGridview()" class="dropdownico" id="Highsearch">�߼�����</a>
                </td>
            </tr>
            <tr>
                <th>
                    ����ʱ��:
                </th>
                <td style="padding: 8px;">
                    <cc1:DateTimeTextBox ID="txtCreateBeginTime" CssClass="date_format" runat="server"
                        EndTimeControlID="txtCreateEndTime"></cc1:DateTimeTextBox>
                    ��
                    <cc1:DateTimeTextBox ID="txtCreateEndTime" CssClass="date_format" runat="server"
                        BeginTimeControlID="txtCreateBeginTime"></cc1:DateTimeTextBox>
                </td>
                <th>
                    ����ʱ��:
                </th>
                <td>
                    <cc1:DateTimeTextBox ID="txtQueryBeginTime" CssClass="date_format" runat="server"
                        EndTimeControlID="txtQueryEndTime"></cc1:DateTimeTextBox>
                    ��
                    <cc1:DateTimeTextBox ID="txtQueryEndTime" CssClass="date_format" runat="server" BeginTimeControlID="txtQueryBeginTime"></cc1:DateTimeTextBox>
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
                <asp:Button runat="server" ID="btnExport" Text="����" OnClick="btnExport_Click" CssClass="btn_Export" />
            </div>
            <div class="dv_pageControl">
                <uc2:PageSet ID="PageSet1" runat="server" />
            </div>
        </div>
        <cc1:CustomGridView ID="GridViewList" runat="server" AutoGenerateColumns="False"
            OnRowDataBound="GridViewList_RowDataBound" DataKeyNames="QueryID" OnRowCommand="CustomGridView1_RowCommand">
            <Columns>
                <asp:TemplateField HeaderText="��������" HeaderStyle-CssClass="alignleft">
                    <ItemStyle CssClass="alignleft" HorizontalAlign="Left" />
                    <ItemTemplate>
                        <cc1:ShortTextLabel ID="ShortTextLabel1" runat="server" ShowTextNum="50" Text='<%#Eval("QueryName") %>'></cc1:ShortTextLabel>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="����ʱ��" HeaderStyle-Width="140">
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <cc1:DateTimeLabel runat="server" DateTimeValue=' <%#Eval("BeginTime")%>'></cc1:DateTimeLabel>~
                        <cc1:DateTimeLabel ID="DateTimeLabel1" runat="server" DateTimeValue=' <%#Eval("EndTime")%>'></cc1:DateTimeLabel>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="����������" HeaderStyle-CssClass="alignleft" HeaderStyle-Width="100">
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:HyperLink runat="server" ID="lblTotalCount" Text='<%#Eval("InvestNumber") %>'></asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="�ύ����" HeaderStyle-Width="60">
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:HyperLink runat="server" ID="lblCount" Text='<%#Eval("SubmitNumber") %>'></asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="δ�ύ����" HeaderStyle-Width="70">
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:HyperLink runat="server" ID="lblUnCount"></asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="��ɽ���" HeaderStyle-Width="70">
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblComplate"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="��������" HeaderStyle-CssClass="alignleft" ItemStyle-CssClass="alignleft">
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:HyperLink runat="server" ID="hlFileInfo"></asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="�ۺϷ���" HeaderStyle-Width="60" ItemStyle-CssClass="alignright"
                    HeaderStyle-CssClass="alignright" Visible="false">
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblScore"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="False" HeaderText="����" HeaderStyle-Width="180">
                    <ItemTemplate>
                        <a href='javascript:showWindow("�鿴�����ʾ�","<%# this.ActionHref(String.Format("View.aspx?op=view&id={0}", new Object[]{Eval("QueryID")})) %>",650,500)'>
                            �鿴</a> <a href='<%# this.ActionHref(String.Format("../QueryStatResult.aspx?QueryID={0}&ResourceType={1}", new Object[]{Eval("QueryID"),this.ResourceType})) %>'
                                target="QueryPreView">������</a>
                        <asp:HyperLink runat="server" ID="hlUpload">�ϴ���������</asp:HyperLink>
                        <asp:HyperLink runat="server" ID="hlSetScore" Visible="false">�����ۺϷ���</asp:HyperLink>
                        <%--                        <asp:LinkButton runat="server" ID="linkBtnImpot" CommandName='Import' CommandArgument='<%# Eval("QueryID")%>'>������ϸ</asp:LinkButton>--%>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle HorizontalAlign="Center" />
                </asp:TemplateField>
            </Columns>
        </cc1:CustomGridView>
        <!--�����б�-->
        <cc1:CustomGridView ID="CustomGridView1" runat="server" AutoGenerateColumns="False"
            DataKeyNames="QueryID" CustomAllowPaging="false" ShowFooter="false" OnRowDataBound="GridViewList_RowDataBound1">
            <Columns>
                <asp:TemplateField HeaderText="��������" HeaderStyle-CssClass="alignleft">
                    <ItemStyle HorizontalAlign="Left" />
                    <ItemTemplate>
                        <cc1:ShortTextLabel ID="ShortTextLabel1" runat="server" Text='<%#Eval("QueryName") %>'></cc1:ShortTextLabel>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="����ʱ��" HeaderStyle-Width="140">
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <cc1:DateTimeLabel ID="DateTimeLabel2" runat="server" DateTimeValue=' <%#Eval("BeginTime")%>'></cc1:DateTimeLabel>~
                        <cc1:DateTimeLabel ID="DateTimeLabel1" runat="server" DateTimeValue=' <%#Eval("EndTime")%>'></cc1:DateTimeLabel>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="����������" HeaderStyle-CssClass="alignleft" HeaderStyle-Width="100">
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:HyperLink runat="server" ID="lblTotalCount" Text='<%#Eval("InvestNumber") %>'></asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="�ύ����" HeaderStyle-Width="60">
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:HyperLink runat="server" ID="lblCount" Text='<%#Eval("SubmitNumber") %>'></asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="δ�ύ����" HeaderStyle-Width="70">
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:HyperLink runat="server" ID="lblUnCount"></asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="��ɽ���" HeaderStyle-Width="70">
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblComplate"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </cc1:CustomGridView>
        <!--���������Ϣ -->
        <cc1:CustomGridView ID="CustomGridView2" runat="server" AutoGenerateColumns="False"
            DataKeyNames="QueryAreaDetailID" CustomAllowPaging="false" ShowFooter="false">
            <Columns>
                <asp:TemplateField HeaderText="���">
                    <ItemStyle HorizontalAlign="Center" Width="60" />
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label ID="LabNo" runat="server" Text='<%#  ((this.PageSet1.PageIndex -1) * this.PageSet1.PageSize) + Container.DataItemIndex+1 %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="����" HeaderStyle-Width="100" HeaderStyle-CssClass="alignleft"
                    ItemStyle-CssClass="alignleft" DataField="loginName" />
                <asp:TemplateField HeaderText="��֯����">
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <cc1:DictionaryLabel ID="DictionaryLabel3" runat="server" DictionaryType='vw_Dic_Sys_Organization'
                            FieldIDValue='<%#Eval("OrganizationID") %>'></cc1:DictionaryLabel>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:UIResource, ui_department%>" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                    <ItemTemplate>
                        <cc1:DictionaryLabel ID="DictionaryLabel1" runat="server" DictionaryType='vw_Dic_Sys_Department'
                            FieldIDValue='<%#Eval("DepartmentID") %>'></cc1:DictionaryLabel>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:UIResource, ui_position%>" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <cc1:DictionaryLabel ID="DictionaryLabel2" runat="server" DictionaryType='vw_Dic_Sys_Post'
                            FieldIDValue='<%#Eval("PostID") %>'></cc1:DictionaryLabel>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="�ֻ�" HeaderStyle-CssClass="alignleft">
                    <ItemStyle CssClass="alignleft" />
                    <ItemTemplate>
                        <cc1:ShortTextLabel ID="ShortTextLabel1" runat="server" Text='<%#Eval("Telphone") %>'></cc1:ShortTextLabel>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="����" HeaderStyle-CssClass="alignleft">
                    <ItemStyle CssClass="alignleft" />
                    <ItemTemplate>
                        <cc1:ShortTextLabel ID="ShortTextLabel1" runat="server" Text='<%#Eval("Email") %>'></cc1:ShortTextLabel>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="�ύ״̬">
                    <ItemStyle HorizontalAlign="Center" Width="60" />
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# GetSubmitState(Eval("CreateTime"))%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="�ύʱ��" HeaderStyle-Width="100" HeaderStyle-CssClass="alignleft"
                    ItemStyle-CssClass="alignleft" DataField="CreateTime" DataFormatString="{0:d}"
                    HtmlEncode="false" />
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
